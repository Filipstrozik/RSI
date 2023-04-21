using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.ServiceModel;

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
                return _users;
            }
        }

        public int GetUserDatabaseSize()
        {
            Console.WriteLine($"...called int GetUserDatabaseSize()");

            return _users.Count;
        }

        public User GetUser(string username)
        {
            Console.WriteLine($"...called User GetUser(string username)");
            foreach (User user in _users)
            {
                if (user.Name == username)
                {
                    return user;
                }
            }
            throw new ArgumentException("User does not exists in database.");
        }

        public User AddUser(User user)
        {
            Console.WriteLine($"...called User AddUser(User user)");

            if (_users.Contains(user))
            {
                throw new ArgumentException("User already exists in database.");
            }

            _users.Add(user);
            return user;
        }

        public User UpdateUser(User user)
        {
            Console.WriteLine($"...called User UpdateUser(User user)");

            if (!_users.Contains(user))
            {
                throw new ArgumentException("User does not exist in database.");
            }

            _users[_users.IndexOf(user)] = user;
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
                    return user;
                }
            }
            throw new ArgumentException("User does not exists in database.");
        }

    }
}