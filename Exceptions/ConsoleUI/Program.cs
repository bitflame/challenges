using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoLibrary;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            PaymentProcessor paymentProcessor = new PaymentProcessor();
            for (int i = 0; i <= 10; i++)
            {
                if (i == 0) Console.WriteLine("payment skipped for payment with 0 items");
                else
                    {

                    try
                    {
                        var result = paymentProcessor.MakePayment($"Demo{ i }", i);
                        if (result == null) Console.WriteLine("Null value for item {0}", i);
                        else
                        Console.WriteLine(result.TransactionAmount);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Payment skipped for payment with {0} items", i);
                        //throw;
                    }
                    catch (ArithmeticException) {
                        Console.WriteLine("Payment skipped for payment with {0} items", i);
                        //throw;
                    }
                    }

            }
            Console.ReadLine();
        }
    }
}
