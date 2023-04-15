using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WcfClient.ServiceReference1;

namespace WcfClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CalculatorClient client1 = new CalculatorClient("BasicHttpBinding_ICalculator");

            try
            {
                int n1 = 2147483647;
                int n2 = 2147483647;

                int result = client1.iMul(n1, n2);
                Console.WriteLine($"{n1} / {n2} = {result}");
            }
            catch (FaultException<DivideByZeroException> ex)
            {
                Console.WriteLine(ex.Detail.Message);
            }
            catch (FaultException<OverflowException> ex)
            {
                Console.WriteLine(ex.Detail.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            client1.Close();
            Console.ReadKey();

        }
    }
}
