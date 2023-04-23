using SecurityLibrary.AES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.ElGamal
{
    public class ElGamal
    {
        /// <summary>
        /// Encryption
        /// </summary>
        /// <param name="alpha"></param>
        /// <param name="q"></param>
        /// <param name="y"></param>
        /// <param name="k"></param>
        /// <returns>list[0] = C1, List[1] = C2</returns>
        public List<long> Encrypt(int q, int alpha, int y, int k, int m)
        {
            long c1 = FastPower(alpha, k, q);
            long K = FastPower(y, k, q);
            long c2 = (m * K) % q;

            return new List<long> { c1, c2 };
        }
        public int Decrypt(int c1, int c2, int x, int q)
        {
            ExtendedEuclid extendedEculid = new ExtendedEuclid();

            int K = FastPower(c1, x, q);
            long Kminus = extendedEculid.GetMultiplicativeInverse(K, q);

            return (int)(c2 * Kminus) % q; 
        }

        private int FastPower(int M, int e, int n)
        {
            int res = 1;

            for (int i = 0; i < e; i++)
                res = (res * M) % n;

            return res % n;
        }
    }
}
