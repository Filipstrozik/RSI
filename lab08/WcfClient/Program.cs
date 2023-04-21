using System;
using System.Threading.Tasks;
using WcfClient.Services;

namespace WcfClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyData.Info();

            int operation = 0;
            do
            {
                Console.WriteLine("Choose an operation:");
                Console.WriteLine("1. Show all users");
                Console.WriteLine("2. Add user");
                Console.WriteLine("3. Update user");
                Console.WriteLine("4. Delete user");
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
                    case 1:
                        DatabaseServiceHandler.GetAllUsers();
                        break;
                    case 2:
                        DatabaseServiceHandler.AddUser();
                        break;
                    case 3:
                        DatabaseServiceHandler.UpdateUser();
                        break;
                    case 4:
                        DatabaseServiceHandler.DeleteUser();
                        break;
                    default:
                        Console.WriteLine("Invalid input.");
                        break;
                }
            } while (operation != 0);
        }
    }
}
