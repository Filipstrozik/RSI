using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WcfClient.CallbackService;
using WcfClient.WcfService;

namespace WcfClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            int operation = 0;
            int n1, n2, result;



            do
            {
                Console.WriteLine("Choose an operation:");
                Console.WriteLine("1. Addition");
                Console.WriteLine("2. Subtraction");
                Console.WriteLine("3. Multiplication");
                Console.WriteLine("4. Division");
                Console.WriteLine("5. Modulo");
                Console.WriteLine("6. Count and find max prime numbers in range");
                Console.WriteLine("0. Exit");

                if (!int.TryParse(Console.ReadLine(), out operation))
                {
                    Console.WriteLine("Invalid input.");
                    continue;
                }

                switch (operation)
                {
                    case 0:
                        Console.WriteLine("Goodbye!");
                        break;
                    case 1:
                        MyCalculatorHandler.Addition();
                        break;
                    case 2:
                        MyCalculatorHandler.Subtraction();
                        break;
                    case 3:
                        MyCalculatorHandler.Multiplication();
                        break;
                    case 4:
                        MyCalculatorHandler.Division();
                        break;
                    case 5:
                        MyCalculatorHandler.Modulo();
                        break;
                    case 6:
                        MyCalculatorHandler.CountAndMaxPrimesInRangeAsync();
                        break;
                    default:
                        Console.WriteLine("Invalid input.");
                        break;
                }
            } while (operation != 0);
            MyCalculatorHandler.CloseConnection();
        }

        //static async Task<int> callHMultiplyAsync(int n1, int n2) {
        //    Console.WriteLine("2......called callHMultiplyAsync");
        //    int reply = await client.HMultAsync(n1, n2);
        //    Console.WriteLine("2......finished HMultipleAsync");
        //    return reply; 
        //}




        //static async Task Main(string[] args)
        //{
        //    CalculatorClient client1 = new CalculatorClient("BasicHttpBinding_ICalculator");
        //    int result = 0;
        //    try
        //    {
        //        int n1 = -int.MaxValue;
        //        int n2 = -1;

        //        result = client1.iDiv(n1, n2);
        //        Console.WriteLine($"{n1} * {n2} = {result}");
        //    }
        //    catch (FaultException<DivideByZeroException> ex)
        //    {
        //        Console.WriteLine(ex.Detail.Message);
        //    }
        //    catch (FaultException<OverflowException> ex)
        //    {
        //        Console.WriteLine(ex.Detail.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }

        //    client1.Close();

        //    CalculatorClient client2 = new CalculatorClient("WSHttpBinding_ICalculator");
        //    Console.WriteLine("...calling Multiply (for endpoint2) - ");
        //    result = client2.iMul(-3, 9); //just example values
        //    Console.WriteLine("Result = " + result);
        //    client2.Close();

        //    client = new CalculatorClient("WSHttpBinding_ICalculator");
        //    Console.WriteLine("2...calling HMultiply ASYNCHRONOUSLY !!!");
        //    Task<int> asyResult = callHMultiplyAsync(1, -3);

        //    SuperCalcCallback myCbHandler = new SuperCalcCallback();
        //    InstanceContext instanceContext = new InstanceContext(myCbHandler);
        //    SuperCalcClient myClient3 = new SuperCalcClient(instanceContext);
        //    double value1 = 10;
        //    Console.WriteLine($"...calling Factorial({value1})...");
        //    myClient3.Factorial(value1);


        //    await Task.Delay(100); // Uśpienie na 2 sekund
        //    Console.WriteLine("...calling Add (for endpoint2) - ");
        //    result = client.iAdd(-3, 9); //just example values
        //    Console.WriteLine("Result = " + result);

        //    result = asyResult.Result;
        //    Console.WriteLine("2.. HMultiplyAsync Result = " + result);

        //    myClient3.Close();
        //    Console.WriteLine("CLIENT3 - STOP");

        //    int l1 = 100000001;
        //    int l2 = 100000007;

        //    var resultATM = await client.CountAndMaxPrimesInRangeAsync(l1, l2);

        //    // Wyświetlenie wyników
        //    Console.WriteLine($"Liczba znalezionych liczb pierwszych w zakresie [{l1},{l2}]: {resultATM.Item1}");
        //    Console.WriteLine($"Największa liczba pierwsza w zakresie [{l1} , {l2}]: {resultATM.Item2}");

        //    client.Close();
        //    Console.ReadKey();
        //}
    }
}
