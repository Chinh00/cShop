using cShop.Core.Domain;
using FluentValidation;
using MediatR;
using OfficeOpenXml;

namespace Application.UseCases.Command;

public record CreateCatalogsCommand(IFormFile File) : ICommand<IResult>
{
    public class Validator : AbstractValidator<CreateCatalogsCommand>
    {
        
    }
    internal class Handler : IRequestHandler<CreateCatalogsCommand, IResult>
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
                        Console.WriteLine(worksheet.Cells[row, col].Text);
                    }
                    data.Add(rowData);
                    

                    
                }
            }

            return Results.Created();
        }
    }
}