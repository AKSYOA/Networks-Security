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
        bool[] curLPlain32 = new bool[32];
        bool[] curRPlain32 = new bool[32];
        bool[] curRPlain48 = new bool[48];
        bool[] binaryKey48 = new bool[48];
        bool[] nxtLPlain32 = new bool[32];

        static int[] expantionPermutation = {32,1, 2, 3, 4, 5, 4, 5, 6, 7, 8, 9,
                                             8 ,9 ,10,11,12,13,12,13,14,15,16,17,
                                             16,17,18,19,20,21,20,21,22,23,24,25,
                                             24,25,26,27,28,29,28,29,30,31,32, 1};

        int[] perAfterSBoxArray = { 16,  7, 20, 21, 29, 12, 28, 17,
                                     1, 15, 23, 26,  5, 18, 31, 10,
                                     2,  8, 24, 14, 32, 27,  3,  9,
                                    19, 13, 30,  6, 22, 11,  4, 25};


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

        public string[] permutedOne(string binary)
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


        public string[] LeftCircularShift(string[] C_D, int round)
        {
            // Left Circular Shift by One
            if (round == 1 || round == 2 || round == 9 || round == 16) {
                C_D[0] += C_D[0][0];
                C_D[0] = C_D[0].Remove(0, 1);
                C_D[1] += C_D[1][0];
                C_D[1] = C_D[1].Remove(0, 1);
            }
            // Left Circular Shift by Two
            else {
                C_D[0] += C_D[0][0];
                C_D[0] += C_D[0][1];
                C_D[0] = C_D[0].Remove(0, 2);
                C_D[1] += C_D[1][0];
                C_D[1] += C_D[1][1];
                C_D[1] = C_D[1].Remove(0, 2);
            }
            return C_D;
        }

        public string permutedTwo(string binary)
        {
            int[] permmatrix = {
                         14, 17, 11, 24,  1,  5,
                         3,  28, 15,  6, 21, 10,
                         23, 19, 12,  4, 26,  8,
                         16,  7, 27, 20, 13,  2,
                         41, 52, 31, 37, 47, 55,
                         30, 40, 51, 45, 33, 48,
                         44, 49, 39, 56, 34, 53,
                         46, 42, 50, 36, 29, 32};

            string result = "";
            for (int i = 0; i < 48; i++)
            {
                result += binary[permmatrix[i] - 1];
            }
            return result;
        }

        public void setBinaryPlain(string plain)
        {
            for (int i = 0; i < 32; i++)
            {
                curLPlain32[i] = (plain[i] == '1') ? true : false;
                curRPlain32[i] = (plain[i + 32] == '1') ? true : false;
            }
        }

        public void setBinarykey(string key)
        {
            for (int i = 0; i < 48; i++)
            {
                binaryKey48[i] = (key[i] == '1') ? true : false;
            }
        }

        public void setExpand48()
        {
            for (int i = 0; i < 48; i++)
                curRPlain48[i] = curRPlain32[expantionPermutation[i] - 1];   
        }

        public void XOR(ref bool[] temp1, ref bool[] temp2)
        {
            for (int i = 0; i < temp1.Length; i++)
            {
                temp1[i] ^= temp2[i];
            }
        }

        public void SBox()
        {

        }


        public void PermutationAfterSBox()
        {
            bool[] temp = new bool[32];
            for (int i = 0; i < 32; i++)
            {
                int idx = perAfterSBoxArray[i];
                temp[i] = curRPlain32[idx - 1];
            }
            for (int i = 0; i < 32; i++)
            {
                curRPlain32[i] = temp[i];
            }
        }


        public string InversPermutation(string cipher)
        {
            int[] arr = {40, 8, 48, 16, 56, 24, 64, 32,
                         39, 7, 47, 15, 55, 23, 63, 31,
                         38, 6, 46, 14, 54, 22, 62, 30,
                         37, 5, 45, 13, 53, 21, 61, 29,
                         36, 4, 44, 12, 52, 20, 60, 28,
                         35, 3, 43, 11, 51, 19, 59, 27,
                         34, 2, 42, 10, 50, 18, 58, 26,
                         33, 1, 41, 9, 49, 17, 57, 25};
            string temp = "";
            for (int i = 0; i < cipher.Length; i++)
            {
                temp += cipher[arr[i] - 1];
            }
            return temp;
        }


        public override string Decrypt(string cipherText, string key)
        {
            throw new NotImplementedException();
        }

        public override string Encrypt(string plainText, string key)
        {
            plainText = plainText.Remove(0, 2);
            key = key.Remove(0, 2);

            string plainbinary = "";
            string keybinary = "";
            string cipher;

            plainbinary = intialpermutation(createbinary(plainText));
            keybinary = createbinary(key);

            string[] C_D = permutedOne(keybinary);
            //C_D = permutedOne(keybinary);

            // Ruond One.....
            setBinaryPlain(plainbinary);

            for (int i = 1; i <= 16; i++)
            {
                C_D = LeftCircularShift(C_D, i);

                key = C_D[0]; key += C_D[1];
                key = permutedTwo(key);
                setBinarykey(key);

                Array.Copy(curRPlain32, nxtLPlain32, 32);
                setExpand48();
                XOR(ref curRPlain48, ref binaryKey48);
                SBox();
                PermutationAfterSBox();
                XOR(ref curRPlain32, ref curLPlain32);
                Array.Copy(nxtLPlain32, curLPlain32, 32);
            }


            return "";
        }
    }
}
