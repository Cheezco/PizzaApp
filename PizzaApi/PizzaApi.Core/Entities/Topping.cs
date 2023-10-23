namespace PizzaApi.Core.Entities;

public class Topping
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Limit { get; set; }
    public decimal Price { get; set; }
}