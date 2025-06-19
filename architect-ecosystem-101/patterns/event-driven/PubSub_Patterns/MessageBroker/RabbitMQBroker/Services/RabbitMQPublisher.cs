

using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace MessageBroker.RabbitMQBroker.Services;
public interface IRabbitMQPublisher<T>
{
    Task PublishMessageAsync(T message, string queueName);
}

public class RabbitMQPublisher<T> : IRabbitMQPublisher<T>
{
    private readonly RabbitMQSetting _rabbitMqSetting;

    public RabbitMQPublisher(IOptions<RabbitMQSetting> rabbitMqSetting)
    {
        _rabbitMqSetting = rabbitMqSetting.Value;
    }

    public async Task PublishMessageAsync(T message, string queueName)
    {

        var factory = new ConnectionFactory
        {
            HostName = _rabbitMqSetting.HostName,
            UserName = _rabbitMqSetting.UserName,
            Password = _rabbitMqSetting.Password
        };

        await using var connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync(); //.CreateModel();
        await channel.QueueDeclareAsync(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

        var messageJson = JsonConvert.SerializeObject(message);
        var body = Encoding.UTF8.GetBytes(messageJson);

        //await Task.Run(() => channel.BasicPublishAsync(exchange: "", routingKey: queueName, basicProperties: null, body: body));
        await Task.Run(() => channel.BasicPublishAsync(exchange: "", routingKey: queueName, body: body));
        await channel.CloseAsync();
        await connection.CloseAsync();
    }
}