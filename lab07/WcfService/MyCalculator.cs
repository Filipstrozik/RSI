using System;
using System.ServiceModel;
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

        public async Task<(int, int)> CountAndMaxPrimesInRangeAsync(int lowerBound, int upperBound)
        {
            Console.WriteLine($"...called CountAndMaxPrimesInRangeAsync({lowerBound}, {upperBound})");


            if (upperBound < lowerBound)
            {
                throw new FaultException<ArgumentException>(new ArgumentException() , "Upper bound cannot be smaller than lower bound.");
            }

            if (lowerBound <= 0 || upperBound <= 0)
            {
                throw new FaultException<ArgumentException>(new ArgumentException() ,"Lower bound and upper bound must be greater than 0.");
            }

            var isPrime = new bool[upperBound + 1];
            for (var i = 0; i < isPrime.Length; i++)
            {
                isPrime[i] = true;
            }

            isPrime[0] = false;
            isPrime[1] = false;

            for (var p = 2; p * p <= upperBound; p++)
            {
                if (isPrime[p])
                {
                    for (var i = p * p; i <= upperBound; i += p)
                    {
                        isPrime[i] = false;
                    }
                }
            }

            var count = 0;
            var maxPrime = -1;
            for (var p = lowerBound; p <= upperBound; p++)
            {
                if (isPrime[p])
                {
                    count++;
                    maxPrime = p;
                }
            }

            return (count, maxPrime);

        }
    }
}