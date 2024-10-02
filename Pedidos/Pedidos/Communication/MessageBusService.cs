using RabbitMQ.Client;
using System.Text;

public class MessageBusService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public MessageBusService()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: "pedidos", durable: false, exclusive: false, autoDelete: false, arguments: null);
    }

    public void PublishMessage(string message)
    {
        var body = Encoding.UTF8.GetBytes(message);
        _channel.BasicPublish(exchange: "", routingKey: "pedidos", basicProperties: null, body: body);
        Console.WriteLine($" [x] Sent {message}");
    }

    public void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
    }
}