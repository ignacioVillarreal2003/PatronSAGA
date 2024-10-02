using Microsoft.EntityFrameworkCore;
using Pedidos.Context;
using Pedidos.Models;

namespace Pedidos.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly DataBaseContext _context;

    public OrderRepository(DataBaseContext context)
    {
        _context = context;
    }

    public async Task<Order> GetOrderByIdAsync(string orderId)
    {
        return await _context.Orders
            .FirstOrDefaultAsync(o => o.OrderId == orderId);
    }

    public async Task<List<Order>> GetOrdersAsync()
    {
        return await _context.Orders.ToListAsync();
    }

    public async Task AddOrderAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
    }

    public async Task UpdateOrderAsync(Order order)
    {
        _context.Orders.Update(order);
    }

    public async Task DeleteOrderAsync(string orderId)
    {
        var order = await GetOrderByIdAsync(orderId);
        if (order != null)
        {
            _context.Orders.Remove(order);
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}