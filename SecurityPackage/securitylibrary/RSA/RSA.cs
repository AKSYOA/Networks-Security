using SecurityLibrary.AES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.RSA
{
    public class RSA
    {
        public int Encrypt(int p, int q, int M, int e)
        {
            int n = p * q;
            return FastPower(M, e, n);
        }

        public int Decrypt(int p, int q, int C, int e)
        {
            ExtendedEuclid extendedEculid = new ExtendedEuclid();

            int n = p * q;
            int d = extendedEculid.GetMultiplicativeInverse(e, (p - 1) * (q - 1));

            return FastPower(C, d, n);
        }

        private int FastPower(int M, int e, int n)
        {
            int res = 1;

            for (int i =0; i < e; i++)
                res = (res * M) % n;
            
            return res % n;
        }
    }
}
