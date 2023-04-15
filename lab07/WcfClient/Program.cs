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
        static CalculatorClient client;
        static async Task Main(string[] args)
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
            
            client = new CalculatorClient("WSHttpBinding_ICalculator");
            Console.WriteLine("2...calling HMultiply ASYNCHRONOUSLY !!!"); 
            Task<int> asyResult = callHMultiplyAsync(1, -3);

            await Task.Delay(4000); // Uśpienie na 2 sekund
            Console.WriteLine("...calling Add (for endpoint2) - ");
            result = client.iAdd(-3, 9); //just example values
            Console.WriteLine("Result = " + result);

            result = asyResult.Result;
            Console.WriteLine("2.. HMultiplyAsync Result = " + result);


            Console.ReadKey();

        }

        static async Task<int> callHMultiplyAsync(int n1, int n2) {
            Console.WriteLine("2......called callHMultiplyAsync");
            int reply = await client.HMultAsync(n1, n2);
            Console.WriteLine("2......finished HMultipleAsync");
            return reply; }
    }
}
