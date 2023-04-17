using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
    ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class MyCalculator : ICalculator
    {
        public int iAdd(int n1, int n2)
        {
            Console.WriteLine($"...called iAdd({n1}, {n2})");
            try
            {
                checked
                {
                    return n1 + n2;
                }
            }
            catch (OverflowException ex)
            {
                throw new FaultException<OverflowException>(ex, new FaultReason($"Addition overflow when adding {n1},{n2}"));
            }
        }

        public int iSub(int n1, int n2)
        {
            Console.WriteLine($"...called iSub({n1}, {n2})");
            try
            {
                checked
                {
                    return n1 - n2;
                }
            }
            catch (OverflowException e)
            {
                throw new FaultException<OverflowException>(e, $"Subtract overflow when subtracting {n1},{n2}");
            }
        }

        public int iMul(int n1, int n2)
        {
            Console.WriteLine($"...called iMul({n1}, {n2})");

            try
            {
                checked // kontrola przepełnienia
                {
                    return n1 * n2;
                }
            }
            catch (OverflowException e)
            {
                throw new FaultException<OverflowException>(e, $"Multiply overflow when multipling {n1},{n2}");
            }

        }

        public int iDiv(int n1, int n2)
        {
            Console.WriteLine($"...called iDiv({n1}, {n2})");
            try
            {
                checked
                {
                    return n1 / n2;
                }
            }
            catch (DivideByZeroException de)
            {
                throw new FaultException<DivideByZeroException>(de, "Cannot divide by zero");
            }
        }

        public int iMod(int n1, int n2)
        {
            Console.WriteLine($"...called iMod({n1}, {n2})");
            if (n2 == 0)
            {
                throw new FaultException<ArgumentException>(new ArgumentException() ,"Cannot modulo by zero");
            }
            return n1 % n2;
        }

        public async Task<int> HMult(int n1, int n2)
        {
            Console.WriteLine($"...called HMult({n1}, {n2})");
            try
            {
                checked
                {
                    await Task.Delay(5000); // Uśpienie na 5 sekund
                    return n1 * n2;
                }
            }
            catch (OverflowException e)
            {
                throw new FaultException<OverflowException>(e, $"Multiply overflow when async multipling {n1},{n2}");
            }
        }

        public async Task<(int, int)> CountAndMaxPrimesInRangeAsync(int l1, int l2)
        {
            Console.WriteLine($"...called CountAndMaxPrimesInRangeAsync({l1}, {l2})");
            int count = 0;
            int maxPrime = 0;

            await Task.Run(() =>
            {
                for (int i = l1; i <= l2; i++)
                {
                    if (IsPrime(i))
                    {
                        count++;
                        if (i > maxPrime)
                        {
                            maxPrime = i;
                        }
                    }
                }
            });

            return (count, maxPrime);
        }


        private bool IsPrime(int n)
        {
            if (n <= 1)
            {
                return false;
            }
            if (n == 2)
            {
                return true;
            }
            if (n % 2 == 0)
            {
                return false;
            }
            for (int i = 3; i <= Math.Sqrt(n); i += 2)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
