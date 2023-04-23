using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Threading.Tasks;

namespace WcfService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
    ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class DatabaseService : IDatabaseService
    {
        private List<User> _users = new List<User>();

        public DatabaseService()
        {
            User newUser = new User { Name = "Filip", Age = 21, Email = "filip@god.pl" };
            _users.Add(newUser);
        }

        public List<User> GetAllUsers()
        {
            Console.WriteLine($"...called List<User> GetAllUsers()");

            lock (_users)
            {
                Console.WriteLine($"...returning {_users}");
                return _users;
            }
        }

        public int GetUserDatabaseSize()
        {
            Console.WriteLine($"...called int GetUserDatabaseSize()");
            Console.WriteLine($"...returning {_users.Count}");
            return _users.Count;
        }

        public User GetUser(string username)
        {
            Console.WriteLine($"...called User GetUser(string username)");
            foreach (User user in _users)
            {
                if (user.Name == username)
                {
                    Console.WriteLine($"...returning {user}");
                    return user;
                }
            }
            throw new FaultException("User does not exists in database.");
        }

        public User AddUser(User user)
        {
            Console.WriteLine($"...called User AddUser(User user)");

            if (_users.Contains(user))
            {
                throw new FaultException("User already exists in database.");
            }

            _users.Add(user);
            Console.WriteLine($"...returning {user}");
            return user;
        }

        public User UpdateUser(User user)
        {
            Console.WriteLine($"...called User UpdateUser(User user)");

            if (!_users.Contains(user))
            {
                throw new FaultException("User does not exist in database.");
            }

            _users[_users.IndexOf(user)] = user;
            Console.WriteLine($"...returning {user}");
            return user;
        }

        public User DeleteUser(string username)
        {
            Console.WriteLine($"...called User DeleteUser(string username)");

            foreach (User user in _users)
            {
                if (user.Name == username)
                {
                    _users.Remove(user);
                    Console.WriteLine($"...returning {user}");
                    return user;
                }
            }
            throw new FaultException("User does not exists in database.");
        }

        public async Task<List<User>> SortBy(string property)
        {
            Console.WriteLine($"...called Task<List<User>> Sort(string property)");

            switch (property)
            {
                case "Name":
                    _users.Sort((u1, u2) => u1.Name.CompareTo(u2.Name));
                    break;
                case "Age":
                    _users.Sort((u1, u2) => u1.Age.CompareTo(u2.Age));
                    break;
                case "Email":
                    _users.Sort((u1, u2) => u1.Email.CompareTo(u2.Email));
                    break;
                default:
                    throw new FaultException($"Invalid property name: {property}");
            }
            await Task.Delay(2000);
            Console.WriteLine($"...returning {_users}");
            return _users;
        }

    }
}