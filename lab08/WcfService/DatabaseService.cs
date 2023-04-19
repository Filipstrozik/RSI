using System;
using System.Collections;
using System.ServiceModel;

namespace WcfService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
    ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class DatabaseService : IDatabaseService
    {
        private ArrayList _users = new ArrayList();

        public ArrayList getAllUsers()
        {
            return _users;
        }

        public int getUserDatabaseSize()
        {
            return _users.Count;
        }

        public void addUser(User user)
        {
            if (_users.Contains(user))
            {
                throw new ArgumentException("User already exists in database.");
            }

            _users.Add(user);
        }

        public void updateUser(User user)
        {
            if (!_users.Contains(user))
            {
                throw new ArgumentException("User does not exist in database.");
            }

            _users[_users.IndexOf(user)] = user;
        }

        public void deleteUser(User user)
        {
            if (!_users.Contains(user))
            {
                throw new ArgumentException("User does not exist in database.");
            }

            _users.Remove(user);
        }
    }
}