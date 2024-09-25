namespace Application.UseCases.Command;

public class Command
{
    public record CreateCatalog(string Name, string Price, string Quantity, Guid CategoryId)
    {
        public CreateCatalog Command (string name, string price, string quantity, Guid categoryId) => new(name, price, quantity, categoryId);
    }
}