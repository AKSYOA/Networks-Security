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
    public class HillCipher :  ICryptographicTechnique<List<int>, List<int>>
    {
        public List<int> list(List<int> plainText, List<int> cipherText)
        {
            List<int> newlist = new List<int>();
            bool check = true;
            for (int i = 0; i < 26; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    check = true;
                    for (int k = 0; k < plainText.Count - 1; k++)
                    {
                        int x;
                        x = (i * plainText[k]) + (j * plainText[k + 1]);
                        x %= 26;
                        if (x != cipherText[k])
                        {
                            check = false;
                            break;
                        }
                    }
                    if (check)
                    {
                        newlist.Add(i);
                        newlist.Add(j);
                        return newlist;
                    }
                }
            }
            return newlist;
        }
        public List<int> Analyse(List<int> plainText, List<int> cipherText)
        {
            throw new InvalidAnlysisException();
        }


        public List<int> Decrypt(List<int> cipherText, List<int> key)
        {
            throw new NotImplementedException();
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
