using System;
using System.Linq;
using System.ServiceModel;
using WcfClient.WcfService;

namespace WcfClient
{
    public class DatabaseServiceHandler
    {
        private IDatabaseService _basicClient;
        private DatabaseServiceClient _wsClient;


        public DatabaseServiceHandler() 
        {
            Uri baseAddress;
            BasicHttpBinding myBinding = new BasicHttpBinding();
            baseAddress = new Uri("http://localhost:10000/DatabaseService/endpoint1");

            EndpointAddress eAddress = new EndpointAddress(baseAddress);
            ChannelFactory<IDatabaseService> myCF = new ChannelFactory<IDatabaseService>(myBinding, eAddress);
            _basicClient = myCF.CreateChannel();

            _wsClient = new DatabaseServiceClient("WSHttpBinding_IDatabaseService");
        }


        public void Close()
        {
            Console.WriteLine("Goodbye!");
            ((IClientChannel)_wsClient).Close();
        }

        public void GetAllUsers()
        {

            try
            {
                var a = _wsClient.GetAllUsers();
                foreach (User user in a)
                {
                    Console.WriteLine(user.Name);
                    Console.WriteLine(user.Age);
                    Console.WriteLine(user.Email);
                }
            }
            catch (FaultException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void GetUser()
        {
            Console.Write("Enter user name: ");
            string name = Console.ReadLine();
            try
            {
                var foundUser = _wsClient.GetUser(name) as User;
            }
            catch (FaultException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public int GetUserDatabaseSize()
        {
            try
            {
                return _wsClient.GetUserDatabaseSize();
            }
            catch (FaultException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return -1;
            }
        }

        public void AddUser()
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
                _wsClient.AddUser(user);
                Console.WriteLine($"User '{user.Name}' added to the database.");
            }
            catch (FaultException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void UpdateUser()
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
                _wsClient.UpdateUser(user);
                Console.WriteLine($"User '{user.Name}' updated in the database.");
            }
            catch (FaultException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void DeleteUser()
        {
            Console.Write("Enter user name: ");
            string name = Console.ReadLine();

            try
            {
                _wsClient.DeleteUser(name);
                Console.WriteLine($"User '{name}' deleted from the database.");
            }
            catch (FaultException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public async void SortUsers()
        {
            Console.Write("Enter property users should be sorted by: ");
            string propertyName = Console.ReadLine();

            try
            {
                var a = await _wsClient.SortByAsync(propertyName);
                Console.WriteLine($"Sorted Users by: {propertyName}");

                foreach (User user in a)
                {
                    Console.WriteLine(user.Name);
                    Console.WriteLine(user.Age);
                    Console.WriteLine(user.Email);
                }
                
            }
            catch (FaultException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
