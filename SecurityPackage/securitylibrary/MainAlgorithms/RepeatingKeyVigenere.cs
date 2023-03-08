using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class RepeatingkeyVigenere : ICryptographicTechnique<string, string>
    {
        public string Analyse(string plainText, string cipherText)
        {
            throw new NotImplementedException();
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
                            key += key[i % key.Length];

                        break;
                    }
                }
            }
            return plainText;
        }

        public string Encrypt(string plainText, string key)
        {
            char[,] tabulaRecta = generateTabulaRecta();
            string cipher = "";

            for (int i = 0; i < plainText.Length; i++)
            {
                cipher += tabulaRecta[plainText[i] - 'a', key[i % key.Length] - 'a'];
            }
            return cipher;
        }

        private char[,] generateTabulaRecta()
        {
            char[,] tabulaRecta = new char[26, 26];

            for (int i = 0; i < 26; i++)
            {
                char firstChar = (char)('A' + i);
                for (int j = 0; j < 26; j++)
                {
                    if (firstChar + j > 'Z')
                    {
                        tabulaRecta[i, j] = (char)(((firstChar + j) % ('Z' + 1)) + 'A');
                    }
                    else
                    {
                        tabulaRecta[i, j] = (char)(firstChar + j);
                    }
                }
            }

            return tabulaRecta;
        }

    }
}