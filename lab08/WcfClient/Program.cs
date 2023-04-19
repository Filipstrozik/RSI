using WcfClient.Services;
using System;
using System.Threading.Tasks;

namespace WcfClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            int operation = 0;
            int n1, n2, result;
            MyData.Info();
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
                        DatabaseServiceHandler.CloseConnection();
                        break;
                    case 1:
                        DatabaseServiceHandler.Addition();
                        break;
                    case 2:
                        DatabaseServiceHandler.Subtraction();
                        break;
                    case 3:
                        DatabaseServiceHandler.Multiplication();
                        break;
                    case 4:
                        DatabaseServiceHandler.Division();
                        break;
                    case 5:
                        DatabaseServiceHandler.Modulo();
                        break;
                    case 6:
                        DatabaseServiceHandler.HMultiply();
                        break;
                    case 7:
                        DatabaseServiceHandler.CountAndMaxPrimesInRangeAsync();
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
