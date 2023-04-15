using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WcfClient.WcfService;

namespace WcfClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CalculatorClient client1 = new CalculatorClient("BasicHttpBinding_ICalculator");
            int result = 0;
            try
            {
                int n1 = 3;
                int n2 = 2;

                result = client1.iMul(n1, n2);
                Console.WriteLine($"{n1} * {n2} = {result}");
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

            CalculatorClient client2 = new CalculatorClient("WSHttpBinding_ICalculator");
            Console.WriteLine("...calling Multiply (for endpoint2) - ");
            result = client2.iMul(-3, 9); //just example values
            Console.WriteLine("Result = " + result);
            client2.Close();
            Console.ReadKey();

        }
    }
}
