using System;
using System.Linq;
using System.ServiceModel;
using WcfClient.WcfService;

namespace WcfClient
{
    public class DatabaseServiceHandler
    {

        static DatabaseServiceClient _client = new DatabaseServiceClient("BasicHttpBinding_IDatabaseService");


        static public void Close()
        {
            Console.WriteLine("Goodbye!");
            _client.Close();
        }

        static public void GetAllUsers()
        {

            try
            {
                var a = _client.GetAllUsers();
                Console.WriteLine(a.ToString());
                foreach (var user in a.ToList())
                {
                    Console.WriteLine(user);
                }
            }
            catch (FaultException<string> ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static public void GetUser()
        {
            Console.Write("Enter user name: ");
            string name = Console.ReadLine();
            try
            {
                var foundUser = _client.GetUser(name) as User;
            }
            catch (FaultException<string> ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static public int GetUserDatabaseSize()
        {
            try
            {
                return _client.GetUserDatabaseSize();
            }
            catch (FaultException<string> ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return -1;
            }
        }

        static public void AddUser()
        {
            Console.Write("Enter user name: ");
            string name = Console.ReadLine();
            Console.Write("Enter user age: ");
            int age = int.Parse(Console.ReadLine());
            Console.Write("Enter user email: ");
            string email = Console.ReadLine();

            User user = new User { Name = name, Age = age, Email = email };

            try
            {
                _client.AddUser(user);
                Console.WriteLine($"User '{user.Name}' added to the database.");
            }
            catch (FaultException<string> ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static public void UpdateUser()
        {
            Console.Write("Enter user name: ");
            string name = Console.ReadLine();
            Console.Write("Enter user age: ");
            int age = int.Parse(Console.ReadLine());
            Console.Write("Enter user email: ");
            string email = Console.ReadLine();

            User user = new User { Name = name, Age = age, Email = email };

            try
            {
                _client.UpdateUser(user);
                Console.WriteLine($"User '{user.Name}' updated in the database.");
            }
            catch (FaultException<string> ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static public void DeleteUser()
        {
            Console.Write("Enter user name: ");
            string name = Console.ReadLine();

            try
            {
                _client.DeleteUser(name);
                Console.WriteLine($"User '{name}' deleted from the database.");
            }
            catch (FaultException<string> ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
