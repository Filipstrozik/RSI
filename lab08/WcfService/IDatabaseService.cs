﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading.Tasks;

namespace WcfService
{
    [ServiceContract(ProtectionLevel = System.Net.Security.ProtectionLevel.None)]
    [ServiceKnownType(typeof(User))]
    public interface IDatabaseService
    {
        [OperationContract]
        List<User> GetAllUsers();

        [OperationContract]
        int GetUserDatabaseSize();

        [OperationContract]
        User GetUser(string username);

        [OperationContract]
        User AddUser(User user);

        [OperationContract]
        User UpdateUser(User user);

        [OperationContract]
        User DeleteUser(string username);

        [OperationContract]
        Task<List<User>> SortBy(string property);
    }

    [DataContract]
    public class User
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Age { get; set; }

        [DataMember]
        public string Email { get; set; }
    }
}
