using cShop.Core.Domain;
using FluentValidation;
using MediatR;
using MongoDB.Driver.Linq;
using OfficeOpenXml;

namespace Application.UseCases.Command;

public record CreateCatalogsCommand(IFormFile File) : ICommand<IResult>
{
    public class Validator : AbstractValidator<CreateCatalogsCommand>
    {
        
    }
    internal class Handler(ISender mediator) : IRequestHandler<CreateCatalogsCommand, IResult>
    {

        public async Task<IResult> Handle(CreateCatalogsCommand request, CancellationToken cancellationToken)
        {
            
            var data = new List<Dictionary<string, string>>();

            using var stream = new MemoryStream();
            await request.File.CopyToAsync(stream, cancellationToken);  

            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets[0];  
                var rowCount = worksheet.Dimension.Rows;        
                var colCount = worksheet.Dimension.Columns;      

                
                var headers = new List<string>();
                for (int col = 1; col <= colCount; col++)
                {
                    headers.Add(worksheet.Cells[1, col].Text);
                }

                for (int row = 2; row <= rowCount; row++)
                {
                    var rowData = new Dictionary<string, string>();
                    for (int col = 1; col <= colCount; col++)
                    {
                        rowData[headers[col - 1]] = worksheet.Cells[row, col].Text;
                        //Console.WriteLine(worksheet.Cells[row, col].Text);
                    }

                    var price = worksheet.Cells[row, 2].Text.Replace(".", "").Replace("đ", "");
                    var listImageSrc = worksheet.Cells[row, 3].Text.Split(',').Where(e => e != string.Empty).ToList();
                    await mediator.Send(
                        new CreateCatalogCommand(worksheet.Cells[row, 1].Text, 10, decimal.Parse(price),
                            worksheet.Cells[row, 4].Text, listImageSrc,
                            new CreateCatalogCommand.CatalogTypeCreateModel(null, worksheet.Cells[row, 5].Text),
                            new CreateCatalogCommand.CatalogBrandCreateModel(null, worksheet.Cells[row, 6].Text)),
                        cancellationToken);
                    data.Add(rowData);
                    

                    
                }
            }

            return Results.Created();
        }
    }
}