using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace WcfService
{
    [ServiceContract(ProtectionLevel = System.Net.Security.ProtectionLevel.None)]
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
