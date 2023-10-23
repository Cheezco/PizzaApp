namespace PizzaApi.Core.Entities;

public class ToppingCategory
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Topping> Toppings { get; set; } = new();
}