using Microsoft.AspNetCore.Mvc;
using Pedidos.Models;
using Pedidos.Services;

namespace Pedidos.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private IOrderService _orderService;
    private readonly MessageBusService _messageBusService;
 
    public OrderController(IOrderService orderService, MessageBusService messageBusService)
    {
        _orderService = orderService;
        _messageBusService = messageBusService;
    }
    
    [HttpGet("{orderId}")]
    public async Task<ActionResult> GetOrder(string orderId)
    {
        var order = await _orderService.GetOrder(orderId);
        if (order == null)
        {
            return NotFound(new { message = "Order not found" });
        }
        return Ok(new { order });
    }
    
    [HttpGet]
    public async Task<ActionResult> GetOrders()
    {
        List<Order> orders = await _orderService.GetOrders();
        return Ok(new { orders } );
    }

    [HttpPost]
    public async Task<ActionResult> PostOrder(OrderPostDto orderPostDto)
    {
        var order = await _orderService.PostOrder(orderPostDto);
        if (order == null)
        {
            return BadRequest(new { message = "Failed to create order" });
        }
    
        var message = $"Order Created: {order.OrderId}, Products: {string.Join(",", order.ProductName)}";
    
        try
        {
            _messageBusService.PublishMessage(message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Failed to send message to RabbitMQ", error = ex.Message });
        }

        return Ok(new { message = "Order created successfully" });
    }
    
    [HttpPut("{orderId}")]
    public async Task<ActionResult> PutOrder(string orderId, OrderPutDto orderPutDto)
    {
        var order = await _orderService.PutOrder(orderId, orderPutDto);
        if (order == null)
        {
            return NotFound(new { message = "Order not found" });
        }
    
        var message = $"Order Updated: {orderId}, Status: {order.OrderStatus}";
    
        try
        {
            _messageBusService.PublishMessage(message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Failed to send message to RabbitMQ", error = ex.Message });
        }

        return Ok(new { message = "Order updated successfully" });
    }
    
    [HttpDelete("{orderId}")]
    public async Task<ActionResult> DeleteOrder(String orderId)
    {
        Boolean isCorrect = await _orderService.DeleteOrder(orderId);
        if (!isCorrect)
        {
            return NotFound(new { message = "Order not found" });
        }

        var message = $"Order Deleted: {orderId}";
    
        try
        {
            _messageBusService.PublishMessage(message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Failed to send message to RabbitMQ", error = ex.Message });
        }

        return Ok(new { message = "Order deleted successfully" });
    }
}