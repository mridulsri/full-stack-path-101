namespace Strategy;

public enum PaymentStatus
{
    Success,
    Failed,
    Declined,
    YetToProcess
}

public class Response
{
    public PaymentStatus Status { get; set; }
    public object Result { get; set; }
    public IList<string> Errors { get; set; }
    public IList<string> MetaInfo { get; set; }

    public bool HasError => Errors.Count > 0;
}

public interface IPaymentStrategy
{
    Response ProcessPayment(decimal amount);
}

public class CreditCardPayment : IPaymentStrategy
{
    public Response ProcessPayment(decimal amount)
    {
        Console.WriteLine("Processing credit Card payment ");

        return new Response()
        {
            Status = PaymentStatus.Success
        };
    }
}

public class UpiPayment : IPaymentStrategy
{
    public Response ProcessPayment(decimal amount)
    {
        Console.WriteLine("Processing UPI payment ");

        return new Response()
        {
            Status = PaymentStatus.Success
        };
    }
}

public class InternetPayment : IPaymentStrategy
{
    public Response ProcessPayment(decimal amount)
    {
        Console.WriteLine("Processing Internet payment ");

        return new Response()
        {
            Status = PaymentStatus.Success
        };
    }
}

public class PayPalPayment : IPaymentStrategy
{
    public Response ProcessPayment(decimal amount)
    {
        Console.WriteLine("Processing Paypal payment ");

        return new Response()
        {
            Status = PaymentStatus.Success
        };
    }
}

public class PaymentProcessor
{
    private IPaymentStrategy _paymentStrategy;

    public PaymentProcessor(IPaymentStrategy paymentStrategy)
    {
        _paymentStrategy = paymentStrategy;
    }

    public void SetPaymentStrategy(IPaymentStrategy paymentStrategy)
    {
        _paymentStrategy = paymentStrategy;
    }

    public Response ExecutePayment(decimal amount)
    {
       return _paymentStrategy.ProcessPayment(amount);
    }
}