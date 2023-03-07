using System;
using System.Collections.Generic;
using System.IO;
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
                if (char.IsUpper(plainText[i]))
                {
                    cipher += (char)((plainText[i] + key - 'A') % 26 + 'A');
                }else
                {
                    cipher += (char)((plainText[i] + key - 'a') % 26 + 'a');
                }

             }
            return cipher;
        }

        public string Decrypt(string cipherText, int key)
        {
            string plainText = "";
            key %= 26;

            for (int i = 0; i < cipherText.Length; i++)
            {
                if (char.IsUpper(cipherText[i]))
                {
                    plainText += (char)((cipherText[i] - key - 'A' + 26) % 26 + 'A');

                }else
                {
                    plainText += (char)((cipherText[i] - key - 'a' + 26) % 26 + 'a');
                }
            }
            return plainText;
        }

        public int Analyse(string plainText, string cipherText)
        {
            // it doesn't matter if the origin text were upperCase or lowerCase, we just need to the shift value in either.
            plainText = plainText.ToUpper();
            cipherText = cipherText.ToUpper();
            int diff = (int) plainText[0] - cipherText[0];
            if (diff > 0) {
                return 26 - diff;
            }else
            {
                return Math.Abs(diff);
            }       
        }
    }
}
