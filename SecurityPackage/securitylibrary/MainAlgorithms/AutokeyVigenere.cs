using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class AutokeyVigenere : ICryptographicTechnique<string, string>
    {
        public string Analyse(string plainText, string cipherText)
        {
            string keyStream = "";
            char[,] tabulaRecta = generateTabulaRecta();

            for (int i = 0; i < plainText.Length; i++)
            {
                int row = plainText[i] - 'a';
                for (int j = 0; j < 26; j++)
                {
                    if (tabulaRecta[row, j] == cipherText[i])
                    {
                        keyStream += (char)('a' + j);
                        break;
                    }
                }
            }
            return extractKey(keyStream, plainText);
        }

        public string Decrypt(string cipherText, string key)
        {
            char[,] tabulaRecta = generateTabulaRecta();
            string plainText = "";

            for (int i = 0; i < key.Length; i++)
            {
                int raw = key[i] - 'a';
                for (int j = 0; j < 26; j++)
                {
                    if (tabulaRecta[raw, j] == cipherText[i])
                    {
                        plainText += (char)('a' + j);
                        if (cipherText.Length != key.Length)
                            key += (char)('a' + j);
                        break;
                    }
                }
            }
            return plainText;
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

        private string extractKey(string KeyStream, string plainText)
        {
            int end = KeyStream.Length - 1;
            string Key = "";
            while (end >= 0)
            {
                string p = plainText.Substring(0, end);

                if (KeyStream.EndsWith(p) && p != "")
                {
                    Key = KeyStream.Substring(0, KeyStream.Length - end);
                    break;
                }
                end--;
            }
            return Key;
        }
    }
}
