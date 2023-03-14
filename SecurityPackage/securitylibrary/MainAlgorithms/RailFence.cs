using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class RailFence : ICryptographicTechnique<string, int>
    {
        public int Analyse(string plainText, string cipherText)
        {
            plainText = plainText.ToLower();
            cipherText = cipherText.ToLower();
            int n = plainText.Length;
            string encrypted;
            for (int i = 2; i <= n; i++)
            {
                encrypted = Encrypt(plainText, i);
                if (encrypted == cipherText) return i;
            }
            return 0;
        }

        public string Decrypt(string cipherText, int key)
        {
            decimal PT_length = Math.Ceiling(cipherText.Length / (decimal)key);
            string plainText = "";

            for (int i = 0; i < PT_length; i++)
            {

                for (int j = i; j < cipherText.Length; j += (int)PT_length)
                {
                    plainText += cipherText[j];
                }
            }
            return plainText;
        }

        public string Encrypt(string plainText, int key)
        {
            string cipher = "";
            for (int i = 0; i < key; i++)
            {

                for (int j = i; j < plainText.Length; j += key)
                {
                    cipher += plainText[j];
                }
            }
            return cipher;
        }
    }
}
