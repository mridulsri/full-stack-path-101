namespace MessageBroker.RabbitMQBroker;

public class RabbitMQSetting
{
    public string? HostName { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
}

//RabbitMQ Queue name
public static class RabbitMQQueues
{
    public const string EmailQueue = "emailQueue";
    public const string SmsQueue = "smsQueue";
    public const string WhatsAppQueue = "whatsAppQueue";

    public const string OrderQueue = "orderQueue";
    public const string ChefQueue = "chefQueue";
    public const string PickupQueue = "pickupQueue";
}