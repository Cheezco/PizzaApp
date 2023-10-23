using PizzaApi.Core.Enums;

namespace PizzaApi.Core.Entities;

public class Order
{
    public int Id { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public OrderState State { get; set; }
    public PizzaSize Size { get; set; } = new();
    public List<Topping> Toppings { get; set; } = new();
    public decimal Price { get; set; }
}