using Pedidos.Models;

namespace Pedidos.Services;

public interface IOrderService
{
    public Task<Order> GetOrder(string orderId);
    public Task<List<Order>> GetOrders();
    public Task<Order> PostOrder(OrderPostDto orderPostDto);
    public Task<Order> PutOrder(String orderId, OrderPutDto orderPutDto);
    public Task<Boolean> DeleteOrder(String orderId);
}