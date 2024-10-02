using Pedidos.Models;

namespace Pedidos.Repositories;

public interface IOrderRepository
{
    Task<Order> GetOrderByIdAsync(string orderId);
    Task<List<Order>> GetOrdersAsync();
    Task AddOrderAsync(Order order);
    Task UpdateOrderAsync(Order order);
    Task DeleteOrderAsync(string orderId);
    Task SaveChangesAsync();
}