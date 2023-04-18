using System.ServiceModel;
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
