using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfClient.ServiceReference1;

namespace WcfClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n1 = 3;
            int n2 = 4;



            CalculatorClient client2 = new CalculatorClient("WSHttpBinding_ICalculator");
            int resultMul = client2.iMul(n1, n2);
            //Console.WriteLine("Calling iMul for endpoint2: ", resultMul);

            //Console.ReadLine();

        }
    }
}
