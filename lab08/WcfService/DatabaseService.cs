using System;
using System.Collections;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;

namespace WcfService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
    ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class DatabaseService : IDatabaseService
    {
        private ArrayList _users = new ArrayList();
        private int _highestID = 2;

        public DatabaseService()
        {
            User newUser = new User { Name = "Filip", Age = 21, Email = "filip@god.pl", ID = 0};
            _users.Add(newUser);
            _users.Add(new User { Name = "Piotr", Age = 22, Email = "piotrG@god.pl", ID = 1 });
        }

        public ArrayList GetAllUsers()
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

        public User GetUser(int ID)
        {
            Console.WriteLine($"...called User GetUser(string username)");
            foreach (User user in _users)
            {
                if (user.ID == ID)
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

            user.ID = _highestID++;
            _users.Add(user);
            Console.WriteLine($"...returning {user}");
            return user;
        }

        public User UpdateUser(User user)
        {
            Console.WriteLine($"...called User UpdateUser(User user)");

            foreach(User curr_user in _users)
            {
                if (curr_user.ID == user.ID)
                {
                    _users[_users.IndexOf(curr_user)] = user;
                    Console.WriteLine($"...returning {user}");
                    return user;
                }
            }
            throw new FaultException("User does not exist in database.");

        }

        public User DeleteUser(int ID)
        {
            Console.WriteLine($"...called User DeleteUser(string username)");

            foreach (User user in _users)
            {
                if (user.ID == ID)
                {
                    _users.Remove(user);
                    Console.WriteLine($"...returning {user}");
                    return user;
                }
            }
            throw new FaultException("User does not exists in database.");
        }

        public async Task<ArrayList> SortBy(string property)
        {
            Console.WriteLine($"...called Task<List<User>> Sort(string property)");

            switch (property)
            {
                case "Name":
                    _users.Sort(new UserComparerByName());
                    break;
                case "Age":
                    _users.Sort(new UserComparerByAge());
                    break;
                case "Email":
                    _users.Sort(new UserComparerByEmail());
                    break;
                default:
                    throw new FaultException($"Invalid property name: {property}");
            }
            await Task.Delay(2000);
            Console.WriteLine($"...returning {_users}");
            return _users;
        }

        public int GetHighestUserID()
        {
            return _highestID;
        }
    }

    public class UserComparerByName : IComparer
    {
        public int Compare(object x, object y)
        {
            User u1 = (User)x;
            User u2 = (User)y;
            return u1.Name.CompareTo(u2.Name);
        }
    }

    public class UserComparerByAge : IComparer
    {
        public int Compare(object x, object y)
        {
            User u1 = (User)x;
            User u2 = (User)y;
            return u1.Age.CompareTo(u2.Age);
        }
    }

    public class UserComparerByEmail : IComparer
    {
        public int Compare(object x, object y)
        {
            User u1 = (User)x;
            User u2 = (User)y;
            return u1.Email.CompareTo(u2.Email);
        }
    }


}