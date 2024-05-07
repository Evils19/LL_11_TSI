using System;
using System.Numerics;
using System.Text;

namespace Algoritm
{
    class Program
    {
        static void Main()
        {
            Algoritm_RSA algoritmRsa = new Algoritm_RSA();
            BigInteger[] cripto = algoritmRsa.CriptoRSA("Hello World!");
           string text= algoritmRsa.DecriptoRSA(cripto);
           Console.WriteLine(text);
            
        }
    }

    class Algoritm_RSA
    {
        private BigInteger p;
        private BigInteger q;
        private BigInteger n;
        private BigInteger Fn;
        private BigInteger e; 
        private BigInteger d;
        private Random random = new Random();

        public Algoritm_RSA()
        {
            do
            {
                p = random.Next(1000000, 10000000);
            } while (!IsPrime((int)p));

            do
            {
                q = random.Next(1000000, 10000000);
            } while (!IsPrime((int)q));

            SetN();

            Console.WriteLine("Cheia publica: " + e + " " + n);
            Console.WriteLine("Cheia privata: " + d + " " + n);
        }

        private bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
            {
                if (number % i == 0)
                    return false;
            }

            return true;
        }

        private void SetN()
        {
            n = p * q;
            Fn = (p - 1) * (q - 1);
            BigInteger temp;
            do
            {
                e = new BigInteger(random.Next(1000000, 10000000));
                temp = BigInteger.GreatestCommonDivisor(e, Fn);
            } while (temp != 1);

            d = ModInverse(e, Fn);
        }

        private BigInteger ModInverse(BigInteger a, BigInteger m)
        {
            BigInteger m0 = m;
            BigInteger y = 0, x = 1;

            while (a > 1)
            {
                BigInteger q = a / m;
                BigInteger t = m;

                m = a % m;
                a = t;
                t = y;

                y = x - q * y;
                x = t;
            }

            if (x < 0)
                x += m0;

            return x;
        }

        public BigInteger[] CriptoRSA(string text)
        { 
            BigInteger[] RSACripto = new BigInteger[text.Length];
            byte[] asciiBytes = Encoding.ASCII.GetBytes(text);
            int i = 0;

            
            foreach (var Byte in asciiBytes)
            {
                RSACripto[i] = BigInteger.ModPow(new BigInteger(Byte), e, n);
                i++;
            }

            return RSACripto;
        }

        public string DecriptoRSA(BigInteger[] RSACripto)
        {
            string text = "";
            byte[] asciiBytes = new byte[RSACripto.Length];
            int i = 0;
            foreach (var Byte in RSACripto)
            {
                asciiBytes[i] = (byte)BigInteger.ModPow(Byte, d, n);
                i++;
            }
            
            foreach (var Byte in asciiBytes)
            {
              
                text += (char)Byte;
            }

            return text;

        }
    }
}
