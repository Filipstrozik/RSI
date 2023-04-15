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
            checked // kontrola przepełnienia
            {
                return n1 + n2;
            }
        }

        public int iSub(int n1, int n2)
        {
            checked // kontrola przepełnienia
            {
                return n1 - n2;
            }
        }

        public int iMul(int n1, int n2)
        {
            checked // kontrola przepełnienia
            {
                return n1 * n2;
            }
        }

        public int iDiv(int n1, int n2)
        {
            if (n2 == 0)
            {
                throw new FaultException<DivideByZeroException>(new DivideByZeroException(), "Cannot divide by zero");
            }
            return n1 / n2;
        }

        public int iMod(int n1, int n2)
        {
            if (n2 == 0)
            {
                throw new ArgumentException("Cannot modulo by zero");
            }
            return n1 % n2;
        }
    }
}
