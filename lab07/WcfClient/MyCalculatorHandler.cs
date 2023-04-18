using System;
using System.ServiceModel;
using WcfClient.WcfService;

namespace WcfClient
{
    public class MyCalculatorHandler
    {
        static CalculatorClient _client = new CalculatorClient("WSHttpBinding_ICalculator");

        static public void CloseConnection()
        {
            Console.WriteLine("Goodbye!");
            _client.Close();
        }
        static private int GetNumberFromUser()
        {
            int number;
            if (!int.TryParse(Console.ReadLine(), out number))
            {
                Console.WriteLine("Invalid input.");
                return GetNumberFromUser();
            }
            return number;
        }

        static public void Addition()
        {
            Console.WriteLine("Enter first number:");
            int n1 = GetNumberFromUser();
            Console.WriteLine("Enter second number:");
            int n2 = GetNumberFromUser();

            try
            {
                int result = _client.iAdd(n1, n2);
                Console.WriteLine($"{n1} + {n2} = {result}");
            }
            catch (FaultException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static public void Subtraction()
        {
            Console.WriteLine("Enter first number:");
            int n1 = GetNumberFromUser();
            Console.WriteLine("Enter second number:");
            int n2 = GetNumberFromUser();

            try
            {
                int result = _client.iSub(n1, n2);
                Console.WriteLine($"{n1} - {n2} = {result}");
            }
            catch (FaultException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static public void Multiplication()
        {
            Console.WriteLine("Enter first number:");
            int n1 = GetNumberFromUser();
            Console.WriteLine("Enter second number:");
            int n2 = GetNumberFromUser();

            try
            {
                int result = _client.iMul(n1, n2);
                Console.WriteLine($"{n1} * {n2} = {result}");
            }
            catch (FaultException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static public void Division()
        {
            Console.WriteLine("Enter first number:");
            int n1 = GetNumberFromUser();
            Console.WriteLine("Enter second number:");
            int n2 = GetNumberFromUser();

            try
            {
                int result = _client.iDiv(n1, n2);
                Console.WriteLine($"{n1} / {n2} = {result}");
            }
            catch (FaultException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static public void Modulo()
        {
            Console.WriteLine("Enter first number:");
            int n1 = GetNumberFromUser();
            Console.WriteLine("Enter second number:");
            int n2 = GetNumberFromUser();

            try
            {
                int result = _client.iMod(n1, n2);
                Console.WriteLine($"{n1} % {n2} = {result}");
            }
            catch (FaultException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static public async void HMultiply()
        {
            Console.WriteLine("Enter first number:");
            int n1 = GetNumberFromUser();
            Console.WriteLine("Enter second number:");
            int n2 = GetNumberFromUser();

            try
            {
                var result = await _client.HMultAsync(n1, n2);
                Console.WriteLine($"HMultiply asyncronous: {n1} * {n2} = {result}"); ;
            }
            catch (FaultException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static public async void CountAndMaxPrimesInRangeAsync()
        {
            Console.WriteLine("Enter first number:");
            int l1 = GetNumberFromUser();
            Console.WriteLine("Enter second number:");
            int l2 = GetNumberFromUser();

            try
            {
                var result = await _client.CountAndMaxPrimesInRangeAsync(l1, l2);
                Console.WriteLine($"Number of primes: {result.Item1}");
                Console.WriteLine($"Max prime: {result.Item2}");
            }
            catch (FaultException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
