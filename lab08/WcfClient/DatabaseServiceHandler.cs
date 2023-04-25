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
            baseAddress = new Uri("http://10.182.36.179:10000/DatabaseService");

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
                    Console.Write("ID: " +  user.ID + "\t");
                    Console.Write("Name: " + user.Name + "\t");
                    Console.Write("Age: " +  user.Age + "\t");
                    Console.WriteLine("Email: " + user.Email + "\t");
                }
            }
            catch (FaultException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void GetSize()
        {

            try
            {
                var size = _wsClient.GetUserDatabaseSize();
                Console.WriteLine($"User Database size: {size}");
            }
            catch (FaultException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void GetUser()
        {
            Console.Write("Enter user ID: ");
            int ID = GetNumberFromUser();
            try
            {
                var foundUser = _wsClient.GetUser(ID) as User;
                Console.Write("ID: " + foundUser.ID + "\t");
                Console.Write("Name: " + foundUser.Name + "\t");
                Console.Write("Age: " + foundUser.Age + "\t");
                Console.WriteLine("Email: " + foundUser.Email + "\t");
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
            int age = GetNumberFromUser();
            Console.Write("Enter user email: ");
            string email = Console.ReadLine();


            User user = new User { Name = name, Age = age, Email = email};

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
            Console.Write("Enter user ID: ");
            int ID = GetNumberFromUser();
            Console.Write("Enter user name: ");
            string name = Console.ReadLine();
            Console.Write("Enter user age: ");
            int age = GetNumberFromUser();
            Console.Write("Enter user email: ");
            string email = Console.ReadLine();

            User user = new User { Name = name, Age = age, Email = email, ID = ID};

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
            Console.Write("Enter user ID: ");
            int ID = GetNumberFromUser();

            try
            {
                _wsClient.DeleteUser(ID);
                Console.WriteLine($"User '{ID}' deleted from the database.");
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
                    Console.Write("ID: " + user.ID + "\t");
                    Console.Write("Name: " + user.Name + "\t");
                    Console.Write("Age: " + user.Age + "\t");
                    Console.WriteLine("Email: " + user.Email + "\t");
                }
                
            }
            catch (FaultException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
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
    }
}
