using System.Collections;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace WcfService
{
    [ServiceContract(ProtectionLevel = System.Net.Security.ProtectionLevel.None)]
    public interface IDatabaseService
    {
        [OperationContract]
        ArrayList getAllUsers();

        [OperationContract]
        int getUserDatabaseSize();

        [OperationContract]
        void addUser(User user);

        [OperationContract]
        void updateUser(User user);

        [OperationContract]
        void deleteUser(User user);
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
