using Pedidos.Models;
using Pedidos.Repositories;

namespace Pedidos.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Order> GetOrder(string orderId)
    {
        return await _orderRepository.GetOrderByIdAsync(orderId);
    }

    public async Task<List<Order>> GetOrders()
    {
        return await _orderRepository.GetOrdersAsync();
    }

    public async Task<Order> PostOrder(OrderPostDto orderPostDto)
    {
        var order = new Order(
            orderPostDto.CustomerId,
            orderPostDto.ShippingAddress,
            orderPostDto.TransactionId,
            orderPostDto.ProductName,
            orderPostDto.Quantity,
            orderPostDto.Price
        );

        await _orderRepository.AddOrderAsync(order);
        await _orderRepository.SaveChangesAsync();
        return order;
    }

    public async Task<Order> PutOrder(string orderId, OrderPutDto orderPutDto)
    {
        var order = await _orderRepository.GetOrderByIdAsync(orderId);
        if (order == null) return null;

        order.OrderStatus = orderPutDto.OrderStatus ?? order.OrderStatus;
        order.ShippingAddress = orderPutDto.ShippingAddress ?? order.ShippingAddress;
        order.PaymentStatus = orderPutDto.PaymentStatus ?? order.PaymentStatus;
        order.InventoryStatus = orderPutDto.InventoryStatus ?? order.InventoryStatus;
        order.ShippingStatus = orderPutDto.ShippingStatus ?? order.ShippingStatus;

        _orderRepository.UpdateOrderAsync(order);
        await _orderRepository.SaveChangesAsync();
        return order;
    }

    public async Task<bool> DeleteOrder(string orderId)
    {
        await _orderRepository.DeleteOrderAsync(orderId);
        await _orderRepository.SaveChangesAsync();
        return true;
    }
}