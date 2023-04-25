using System;
using System.ServiceModel;
using System.Threading.Tasks;
using WcfClient.Services;
using WcfClient.WcfService;

namespace WcfClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            MyData.Info();
            Console.WriteLine("... The client is started");
            
            DatabaseServiceHandler handler = new DatabaseServiceHandler();  

            

            int operation = 0;
            do
            {
                Console.WriteLine("Choose an operation:");
                Console.WriteLine("1. Show all users");
                Console.WriteLine("2. Add user");
                Console.WriteLine("3. Get user");
                Console.WriteLine("4. Update user");
                Console.WriteLine("5. Delete user");
                Console.WriteLine("6. Sort");
                Console.WriteLine("7. Size");
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
                        handler.GetAllUsers();
                        break;
                    case 2:
                        handler.AddUser();
                        break;
                    case 3:
                        handler.GetUser();
                        break;
                    case 4:
                        handler.UpdateUser();
                        break;
                    case 5:
                        handler.DeleteUser();
                        break;
                    case 6:
                        handler.SortUsers();
                        break;
                    case 7:
                        handler.GetSize();
                        break;
                    default:
                        Console.WriteLine("Invalid input.");
                        break;
                }
            } while (operation != 0);
        }
    }
}
