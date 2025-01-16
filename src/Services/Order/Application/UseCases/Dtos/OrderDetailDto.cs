namespace Application.UseCases.Dtos;

public class OrderDetailDto
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}