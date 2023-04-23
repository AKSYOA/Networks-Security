using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.DiffieHellman
{
    public class DiffieHellman 
    {
        public List<int> GetKeys(int q, int alpha, int xa, int xb)
        {
            int ya = FastPower(alpha, xa, q);
            int yb = FastPower(alpha, xb, q);

            int KA = FastPower(yb, xa, q);
            int KB = FastPower(ya, xb, q);

            return new List<int> { KA, KB };
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
