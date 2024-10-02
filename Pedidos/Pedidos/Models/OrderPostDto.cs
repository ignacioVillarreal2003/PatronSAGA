using System.ComponentModel.DataAnnotations;

namespace Pedidos.Models;

public class OrderPostDto
{
    [Required] public string CustomerId { get; set; }
    [Required] public string ShippingAddress { get; set; }
    [Required] public string TransactionId { get; set; }
    [Required] public string ProductName { get; set; }
    [Required] public int Quantity { get; set; }
    [Required] public decimal Price { get; set; }
}