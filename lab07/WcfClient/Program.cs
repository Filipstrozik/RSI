using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WcfClient.CallbackService;
using WcfClient.WcfService;

namespace WcfClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            int operation = 0;
            int n1, n2, result;

            do
            {
                Console.WriteLine("Choose an operation:");
                Console.WriteLine("1. Addition");
                Console.WriteLine("2. Subtraction");
                Console.WriteLine("3. Multiplication");
                Console.WriteLine("4. Division");
                Console.WriteLine("5. Modulo");
                Console.WriteLine("6. HMultiply");
                Console.WriteLine("7. Count and find max prime numbers in range");
                Console.WriteLine("0. Exit");

                if (!int.TryParse(Console.ReadLine(), out operation))
                {
                    Console.WriteLine("Invalid input.");
                    continue;
                }

                switch (operation)
                {
                    case 0:
                        MyCalculatorHandler.CloseConnection();
                        break;
                    case 1:
                        MyCalculatorHandler.Addition();
                        break;
                    case 2:
                        MyCalculatorHandler.Subtraction();
                        break;
                    case 3:
                        MyCalculatorHandler.Multiplication();
                        break;
                    case 4:
                        MyCalculatorHandler.Division();
                        break;
                    case 5:
                        MyCalculatorHandler.Modulo();
                        break;
                    case 6:
                        MyCalculatorHandler.HMultiply();
                        break;
                    case 7:
                        MyCalculatorHandler.CountAndMaxPrimesInRangeAsync();
                        break;
                    default:
                        Console.WriteLine("Invalid input.");
                        break;
                }
            } while (operation != 0);
            Console.ReadKey();
        }
    }
}
