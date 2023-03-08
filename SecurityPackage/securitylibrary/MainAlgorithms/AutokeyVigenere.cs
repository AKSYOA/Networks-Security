using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class AutokeyVigenere : ICryptographicTechnique<string, string>
    {
        public string Analyse(string plainText, string cipherText)
        {
            throw new NotImplementedException();
        }

        public string Decrypt(string cipherText, string key)
        {
            throw new NotImplementedException();
        }

        public string Encrypt(string plainText, string key)
        {
           string keyStream = key + plainText.Substring(0, plainText.Length - key.Length);
           char[,] tabulaRecta = generateTabulaRecta();
           string cipher = "";

           for (int i = 0; i < plainText.Length; i++)
           {
                cipher += tabulaRecta[plainText[i] - 'a', keyStream[i] - 'a'];
           }
            Console.WriteLine(cipher);
           return cipher;
        }

        private char[,] generateTabulaRecta()
        {
            char[,] tabulaRecta = new char[26, 26];

            for (int i = 0; i < 26; i++)
            {
                char firstChar =  (char) ('A' + i);
                for (int j = 0; j < 26; j++)
                {
                    if (firstChar + j > 'Z')
                    {
                        tabulaRecta[i, j] = (char) (((firstChar + j) % ('Z' + 1)) + 'A');
                    }else
                    {
                        tabulaRecta[i, j] = (char)(firstChar + j);
                    }
                }
            }

            return tabulaRecta;
        }
    }
}
