using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfService
{
    [ServiceContract(ProtectionLevel = System.Net.Security.ProtectionLevel.None)]
    public interface ICalculator
    {
        [OperationContract]
        int iAdd(int n1, int n2);

        [OperationContract]
        int iSub(int n1, int n2);

        [OperationContract]
        int iMul(int n1, int n2);

        [OperationContract]
        int iDiv(int n1, int n2);

        [OperationContract]
        int iMod(int n1, int n2);

        [OperationContract]
        Task<int> HMult(int n1, int n2);

        [OperationContract]
        Task<(int, int)> CountAndMaxPrimesInRangeAsync(int l1, int l2);
    }


}
