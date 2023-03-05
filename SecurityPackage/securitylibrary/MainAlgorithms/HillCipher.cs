using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    /// <summary>
    /// The List<int> is row based. Which means that the key is given in row based manner.
    /// </summary>
    public class HillCipher : ICryptographicTechnique<List<int>, List<int>>
    {

        public List<int> Analyse(List<int> plainText, List<int> cipherText)
        {
            throw new InvalidAnlysisException();
        }
        public int CalcDet(List<int> cell, bool check)
        {
            int det;
            if (cell.Count == 4)
                det = (cell[0] * cell[3]) - (cell[1] * cell[2]);
            else
            {
                det = cell[0] * (cell[4] * cell[8] - cell[5] * cell[7]) -
                      cell[1] * (cell[3] * cell[8] - cell[5] * cell[6]) +
                      cell[2] * (cell[3] * cell[7] - cell[4] * cell[6]);
            }
            if (!check)
            {
                return det;
            }
            while (det < 0)
                det += 26;

            return det % 26;
        }

        public int CalB(int det)
        {
            for (int i = 1; i < 26; ++i)
            {
                if (i * det % 26 == 1)
                    return i;
            }
            return -1;
        }

        public bool Check(List<int> key)
        {
            for (int i = 0; i < key.Count; ++i)
            {
                if (key[i] >= 0 || key[i] <= 25)
                    return true;
            }


            return false;
        }


        public bool CheckDet(int det)
        {
            int a = 26, b = det;
            while (b != 0)
            {
                int temp = a % b;
                a = b;
                b = temp;

            }

            return a == 1;
        }

        public int CalcMod(int val)
        {
            while (val < 0)
            {
                val += 26;
            }
            return val % 26;
        }
        public List<int> Decrypt(List<int> cipherText, List<int> key)
        {
            int m = (int)Math.Sqrt(key.Count);
            int det = CalcDet(key, true);
            int b = CalB(det);
            bool GCD = CheckDet(det);
            bool valid = Check(key);
            if (!GCD || !valid)
                throw new NotImplementedException();

            List<int> list = new List<int>();
            if (key.Count == 4)
            {
                int inverse = (1 / CalcDet(key, false));
                list.Add(CalcMod(inverse * key[3]));
                list.Add(CalcMod(key[1] * inverse * -1));
                list.Add(CalcMod(key[2] * inverse * -1));
                list.Add(CalcMod(inverse * key[0]));


                return Encrypt(cipherText, list);
            }
            else
            {
                for (int i = 0; i < key.Count; ++i)
                {
                    list.Add(0);
                }
                List<int> L = new List<int>();

                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        L.Clear();
                        for (int k = 0; k < m; k++)
                        {
                            if (k == i)
                                continue;
                            for (int a = 0; a < m; a++)
                            {
                                if (a == j)
                                    continue;
                                L.Add(key[k * m + a]);
                            }
                        }
                        list[i * m + j] = (b * (int)Math.Pow(-1, i + j) * CalcDet(L, true)) % 26;
                        while (list[i * m + j] < 0)
                        {
                            list[i * m + j] += 26;
                        }
                    }
                }

                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        key[j * m + i] = (int)list[i * m + j];
                    }
                }


                return Encrypt(cipherText, key);
            }

        }


        public List<int> Encrypt(List<int> plainText, List<int> key)
        {
            int m = (int)Math.Sqrt(key.Count());
            List<int> cipher = new List<int>();
            for (int i = 0; i < plainText.Count; i += m)
            {
                for (int j = 0; j < m; j++)
                {
                    int sum = 0;
                    for (int k = 0; k < m; k++)
                    {
                        sum += plainText[i + k] * key[j * m + k];
                    }
                    cipher.Add(sum % 26);
                }

            }
            return cipher;
        }


        public List<int> Analyse3By3Key(List<int> plainText, List<int> cipherText)
        {
            throw new NotImplementedException();
        }

    }
}
