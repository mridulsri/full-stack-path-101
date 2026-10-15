// See https://aka.ms/new-console-template for more information


using Strategy;

var CcPayment = new PaymentProcessor(new CreditCardPayment());
var response = CcPayment.ExecutePayment(200);

Console.WriteLine(response.Status);
