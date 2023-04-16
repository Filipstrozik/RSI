using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfClient.CallbackService;

namespace WcfClient
{
    public class SuperCalcCallback : ISuperCalcCallback
    {
        public void FactorialResult(double result)
        {
            //here the result is consumed
            Console.WriteLine(" Factorial = {0}", result);
        }
    }
}
