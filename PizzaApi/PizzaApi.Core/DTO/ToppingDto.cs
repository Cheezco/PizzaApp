namespace PizzaApi.Core.DTO;

public record ToppingDto(int Id, int CategoryId, string Name, int Limit, decimal Price);

public record SelectedToppingDto()
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CategoryId { get; set; }
    public int Count { get; set; }
}