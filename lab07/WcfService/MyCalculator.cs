using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfService
{
    // UWAGA: możesz użyć polecenia „Zmień nazwę” w menu „Refaktoryzuj”, aby zmienić nazwę klasy „Service1” w kodzie i pliku konfiguracji.
    public class MyCalculator : ICalculator
    {
        public int iAdd(int n1, int n2)
        {
            int result = 0;
            try
            {
                checked
                {
                    result = n1 + n2;
                }
            }
            catch (OverflowException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;

        }

        public int iSub(int n1, int n2)
        {
            int result = 0;
            try
            {
                checked
                {
                    result = n1 - n2;
                }
            }
            catch (OverflowException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;

        }

        public int iMul(int n1, int n2)
        {
            int result = 0;
            try
            {
                checked
                {
                    result = n1 * n2;
                }
            }
            catch (OverflowException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        public int iDiv(int n1, int n2)
        {
            int result = 0;
            try
            {
                checked
                {
                    result = n1 / n2;
                }
            }
            catch(DivideByZeroException dex)
            {
                Console.WriteLine(dex.Message);
            }
            catch (OverflowException oex)
            {
                Console.WriteLine(oex.Message);
            }
            return result;
        }

        public int iMod(int n1, int n2)
        {
            int result = 0;
            try
            {
                checked
                {
                    result = n1 % n2;
                }
            }
            catch (DivideByZeroException dex)
            {
                Console.WriteLine(dex.Message);
            }
            catch (OverflowException oex)
            {
                Console.WriteLine(oex.Message);
            }
            return result;
        }
    }
}
