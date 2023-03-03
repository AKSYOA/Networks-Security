using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Ceaser : ICryptographicTechnique<string, int>
    {
        public string Encrypt(string plainText, int key)
        {
            String cipher = "";
            key %= 26;
             for (int i = 0; i < plainText.Length; i ++)
             {
                cipher += (char)((plainText[i] + key - 'a') % 26 + 'a');

            }
            Console.WriteLine(cipher);
            return cipher;
        }

        public string Decrypt(string cipherText, int key)
        {
            string plainText = "";
            key %= 26;

            for (int i = 0; i < cipherText.Length; i++)
            {
                plainText += (char)((cipherText[i] - key - 'A' + 26) % 26 + 'A');
            }
            Console.WriteLine(plainText);
            return plainText;
        }

        public int Analyse(string plainText, string cipherText)
        {
            throw new NotImplementedException();
        }
    }
}
