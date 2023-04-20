using WcfClient.Services;
using System;
using System.Threading.Tasks;

namespace WcfClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyData.Info();

            int operation = 0;
            int n1, n2, result;
            do
            {
                Console.WriteLine("Choose an operation:");
                Console.WriteLine("1. Show all users");
                Console.WriteLine("2. Delete user");
                Console.WriteLine("3. ");
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
