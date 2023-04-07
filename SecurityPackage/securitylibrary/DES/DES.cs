using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.DES
{
    /// <summary>
    /// If the string starts with 0x.... then it's Hexadecimal not string
    /// </summary>
    public class DES : CryptographicTechnique
    {

        public static string ToBinary(string val)
        {
            string result = Convert.ToString(Convert.ToInt32(val, 16), 2);
            int end = 4 - result.Length;

            if (result.Length < 4)
                for (int i = 0; i < end; i++)
                    result = result.Insert(0, "0");

            return result;
        }

        public static string Tohexa(string val)
        {
            string result = Convert.ToString(Convert.ToInt32(val, 2), 16);
            return result;
        }

        public string createbinary(string temp)
        {
            string binary = "";
            for (int i = 0; i < temp.Length; i++)
            {
                binary += ToBinary(temp[i].ToString());
            }
            return binary;
        }

        public string intialpermutation(string binary)
        {
            int[] intialpermutation = {
                         58, 50, 42, 34, 26, 18, 10, 2,
                         60, 52, 44, 36, 28, 20, 12, 4,
                         62, 54, 46, 38, 30, 22, 14, 6,
                         64, 56, 48, 40, 32, 24, 16, 8,
                         57, 49, 41, 33, 25, 17, 9,  1,
                         59, 51, 43, 35, 27, 19, 11, 3,
                         61, 53, 45, 37, 29, 21, 13, 5,
                         63, 55, 47, 39, 31, 23, 15, 7};
            string result = "";
            for (int i = 0; i < binary.Length; i++)
            {
                result += binary[intialpermutation[i] - 1];
            }
            return result;
        }

        public string[] permuted1(string binary)
        {
            int[] permu_C = {57, 49, 41, 33, 25, 17, 9,
                             1, 58, 50, 42, 34, 26, 18,
                             10, 2, 59, 51, 43, 35, 27,
                             19, 11, 3, 60, 52, 44, 36};
            int[] permu_D = {63, 55, 47, 39, 31, 23, 15,
                             7, 62, 54, 46, 38, 30, 22,
                             14, 6, 61, 53, 45, 37, 29,
                             21, 13, 5, 28, 20, 12, 4};

            string c = "", d = "";
            for (int i = 0; i < 28; i++)
            {
                c += binary[permu_C[i] - 1];
                d += binary[permu_D[i] - 1];
            }
            string[] temp = new string[2];
            temp[0] += c;
            temp[1] += d;
            return temp;
        }









        public override string Decrypt(string cipherText, string key)
        {
            throw new NotImplementedException();
        }

        public override string Encrypt(string plainText, string key)
        {
            throw new NotImplementedException();
        }
    }
}
