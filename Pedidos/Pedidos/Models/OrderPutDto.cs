namespace Pedidos.Models;

public class OrderPutDto
{
    public string OrderStatus { get; set; }
    public string ShippingAddress { get; set; }
    public string PaymentStatus  { get; set; }
    public string TransactionId { get; set; }
    public string InventoryStatus { get; set; }
    public string ShippingStatus { get; set; }
}