namespace Application.UseCases.Dtos;

public record CustomerDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
}