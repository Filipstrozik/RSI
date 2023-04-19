//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.Serialization;
//using System.ServiceModel;
//using System.Text;
//using System.Threading.Tasks;

//namespace WcfClient
//{
//    // UWAGA: możesz użyć polecenia „Zmień nazwę” w menu „Refaktoryzuj”, aby zmienić nazwę interfejsu „IService1” w kodzie i pliku konfiguracji.
//    [ServiceContract(ProtectionLevel = System.Net.Security.ProtectionLevel.None)]
//    public interface ICalculator
//    {
//        [OperationContract]
//        int iAdd(int n1, int n2);

//        [OperationContract]
//        int iSub(int n1, int n2);

//        [OperationContract]
//        int iMul(int n1, int n2);

//        [OperationContract]
//        int iDiv(int n1, int n2);

//        [OperationContract]
//        int iMod(int n1, int n2);

//        [OperationContract]
//        Task<int> HMult(int n1, int n2);
//    }
//}
