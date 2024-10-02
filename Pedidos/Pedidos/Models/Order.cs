using System.ComponentModel.DataAnnotations;

namespace Pedidos.Models;

public class Order
{
    [Key] [Required] public string OrderId { get; set; }

    [Required] public string CustomerId { get; set; }

    [Required] public string OrderStatus { get; set; } // "Pending", "Confirmed", "Cancelled", "Failed", "Shipped"

    [Required] public DateTime OrderDate { get; set; }

    [Required] public string ShippingAddress { get; set; }

    [Required] public string PaymentStatus { get; set; } // "Pending", "Completed", "Failed"

    [Required] public string TransactionId { get; set; }

    [Required] public string InventoryStatus { get; set; } // "Pending", "Reserved", "Failed"

    [Required] public string ShippingStatus { get; set; } // "Pending", "Shipped", "In Process", "Failed"

    [Required] public string ProductName { get; set; }

    [Required] public int Quantity { get; set; }

    [Required] public decimal Price { get; set; }

    public Order(string customerId, string shippingAddress, string transactionId, string productName, int quantity, decimal price)
    {
        OrderId = Guid.NewGuid().ToString();
        OrderDate = DateTime.UtcNow;
        OrderStatus = "Pending";
        PaymentStatus = "Pending";
        InventoryStatus = "Pending";
        ShippingStatus = "Pending";
        CustomerId = customerId;
        ShippingAddress = shippingAddress;
        TransactionId = transactionId;
        ProductName = productName;
        Quantity = quantity;
        Price = price;
    }
}