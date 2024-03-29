﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.AES
{
    /// <summary>
    /// If the string starts with 0x.... then it's Hexadecimal not string
    /// </summary>
    public class AES : CryptographicTechnique
    {
        int[,] Key = new int[4, 4];
        int[,] matrix = new int[4, 4];
        int[,] ColMatrix = new int[4, 4];


        int[,] s_box = {
              { 0x63, 0x7c, 0x77, 0x7b, 0xf2, 0x6b, 0x6f, 0xc5, 0x30, 0x01, 0x67, 0x2b, 0xfe, 0xd7, 0xab, 0x76 },
              { 0xca, 0x82, 0xc9, 0x7d, 0xfa, 0x59, 0x47, 0xf0, 0xad, 0xd4, 0xa2, 0xaf, 0x9c, 0xa4, 0x72, 0xc0 },
              { 0xb7, 0xfd, 0x93, 0x26, 0x36, 0x3f, 0xf7, 0xcc, 0x34, 0xa5, 0xe5, 0xf1, 0x71, 0xd8, 0x31, 0x15 },
              { 0x04, 0xc7, 0x23, 0xc3, 0x18, 0x96, 0x05, 0x9a, 0x07, 0x12, 0x80, 0xe2, 0xeb, 0x27, 0xb2, 0x75 },
              { 0x09, 0x83, 0x2c, 0x1a, 0x1b, 0x6e, 0x5a, 0xa0, 0x52, 0x3b, 0xd6, 0xb3, 0x29, 0xe3, 0x2f, 0x84},
              { 0x53, 0xd1, 0x00, 0xed, 0x20, 0xfc, 0xb1, 0x5b, 0x6a, 0xcb, 0xbe, 0x39, 0x4a, 0x4c, 0x58, 0xcf},
              { 0xd0, 0xef, 0xaa, 0xfb, 0x43, 0x4d, 0x33, 0x85, 0x45, 0xf9, 0x02, 0x7f, 0x50, 0x3c, 0x9f, 0xa8 },
              { 0x51, 0xa3, 0x40, 0x8f, 0x92, 0x9d, 0x38, 0xf5, 0xbc, 0xb6, 0xda, 0x21, 0x10, 0xff, 0xf3, 0xd2 },
              {  0xcd, 0x0c, 0x13, 0xec, 0x5f, 0x97, 0x44, 0x17, 0xc4, 0xa7, 0x7e, 0x3d, 0x64, 0x5d, 0x19, 0x73 },
              { 0x60, 0x81, 0x4f, 0xdc, 0x22, 0x2a, 0x90, 0x88, 0x46, 0xee, 0xb8, 0x14, 0xde, 0x5e, 0x0b, 0xdb },
              { 0xe0, 0x32, 0x3a, 0x0a, 0x49, 0x06, 0x24, 0x5c, 0xc2, 0xd3, 0xac, 0x62, 0x91, 0x95, 0xe4, 0x79 },
              { 0xe7, 0xc8, 0x37, 0x6d, 0x8d, 0xd5, 0x4e, 0xa9, 0x6c, 0x56, 0xf4, 0xea, 0x65, 0x7a, 0xae, 0x08 },
              { 0xba, 0x78, 0x25, 0x2e, 0x1c, 0xa6, 0xb4, 0xc6, 0xe8, 0xdd, 0x74, 0x1f, 0x4b, 0xbd, 0x8b, 0x8a },
              { 0x70, 0x3e, 0xb5, 0x66, 0x48, 0x03, 0xf6, 0x0e, 0x61, 0x35, 0x57, 0xb9, 0x86, 0xc1, 0x1d, 0x9e },
              { 0xe1, 0xf8, 0x98, 0x11, 0x69, 0xd9, 0x8e, 0x94, 0x9b, 0x1e, 0x87, 0xe9, 0xce, 0x55, 0x28, 0xdf },
              { 0x8c, 0xa1, 0x89, 0x0d, 0xbf, 0xe6, 0x42, 0x68, 0x41, 0x99, 0x2d, 0x0f, 0xb0, 0x54, 0xbb, 0x16 },
       };
        public void MakeMatrix(string plain, int num)
        {
            int index = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    string hex = "0x" + plain[index] + plain[index + 1];
                    int val = Convert.ToInt32(hex, 16);
                    index += 2;
                    if (num == 1)
                        matrix[j, i] = val;
                    else if (num == 2)
                        ColMatrix[i, j] = val;
                    else if (num == 3)
                        Key[j,i] = val;
                }
            }
        }
        public void RoundKey()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    matrix[i, j] = Key[i, j] ^ matrix[i, j];
                }
            }
        }
        public void SubBytes(int[,] matrix, int rows, int columns)
        {
            int x = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    x = matrix[i, j];
                    int a = x / 16;
                    int b = x % 16;
                    matrix[i, j] = s_box[a, b];
                }
            }
        }
        public void ShiftRows(int n)
        {
            int x = 0;
            for (int i = 1; i < n; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    x = matrix[i, 0];
                    for (int k = 0; k < n; k++)
                    {
                        if (k < 3)
                        {
                            matrix[i, k] = matrix[i, k + 1];
                        }
                        else if (k == 3)
                        {
                            matrix[i, k] = x;
                        }
                    }
                }
            }
        }
        public int SSize(int number)
        {
            number = number << 1;
            return (number % 256);
        }
        public bool Check(int num)
        {
            return (num >> 7 & 1) == 1;
        }
        public int power(int n, int p)
        {
            if (p == 1)
                return n;
            int x = 0;
            if (p % 2 == 1)
            {
                x ^= n;
            }

            int val = power(n, p / 2);
            if (Check(val) == true)
            {
                val = SSize(val);
                val = val ^ 27;
            }
            //0
            else
            {
                val = SSize(val);
            }
            return val ^ x;
        }
        
        public void MixColumns()
        {
            for (int a = 0; a < 4; a++)
            {
                int[,] col = new int[4, 1];
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        col[i, 0] ^= power(matrix[j, a], ColMatrix[i, j]);
                    }
                }

                for (int i = 0; i < 4; ++i)
                {
                    matrix[i, a] = col[i, 0];
                }
            }
        }
        public void keySchedule(int round)
        {
            int[,] Rconmat = new int[4, 1];
            string Rcon = "01020408102040801b36";
            int val = Convert.ToInt32(Rcon.Substring(round * 2, 2), 16);
            Rconmat[0, 0] = val;
            int[,] arr = new int[4, 1];
            arr[3, 0] = Key[0, 3];
            for (int i = 0; i < 3; i++)
                arr[i, 0] = Key[i + 1, 3];
            SubBytes(arr, 4, 1);
            for (int k = 0; k < 4; k++)
            {
                Key[k, 0] = Key[k, 0] ^ arr[k, 0] ^ Rconmat[k, 0];
            }
            for (int i = 0; i < 4; i++)
            {
                for (int j = 1; j < 4; j++)
                {
                    Key[i, j] = Key[i, j - 1] ^ Key[i, j];
                }
            }
        }

        public void shiftrows_Dec()
        {
            for (int i = 1; i < 4; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    int x = matrix[i, 3];
                    for (int k = 2; k >= 0; k--)
                    {
                        matrix[i, k + 1] = matrix[i, k];
                    }
                    matrix[i, 0] = x;
                }
            }
        }

        public void keyschedule_Dec(int round)
        {
            int[,] Rconmat = new int[4, 1];
            string Rcon = "01020408102040801b36";
            int val = Convert.ToInt32(Rcon.Substring(round * 2, 2), 16);
            Rconmat[0, 0] = val;
            int[,] arr = new int[4, 1];
            for (int i = 3; i >= 1; i--)
            {
                for (int j = 0; j < 4; j++)
                {
                    Key[j, i] = Key[j, i] ^ Key[j, i - 1];
                }
            }
            arr[3, 0] = Key[0, 3];
            for (int i = 0; i < 3; i++)
                arr[i, 0] = Key[i + 1, 3];
            SubBytes(arr, 4, 1);
            for (int i = 0; i < 4; i++)
            {
                Key[i, 0] = Key[i, 0] ^ arr[i, 0] ^ Rconmat[i, 0];
            }
        }
        public void SubBytes_Dec(int[,] matrix, int n)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    int a = matrix[i, j] / 16;
                    int b = matrix[i, j] % 16;
                    matrix[i, j] = ss_box[a, b];
                }
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.WriteLine(matrix[i, j].ToString("x"));
                }
            }
        }
        int[,] ss_box = new int[16, 16];

        public void inverseSbox()
        {
            int x = 0;
            for (int i = 0; i < 16; i++)
            {

                for (int j = 0; j < 16; j++)
                {
                    x = s_box[i, j];
                    int a = x / 16;
                    int b = x % 16;
                    ss_box[a,b] = i * 16 + j;
                }
            }
        }
        public string Res()
        {
            string S = "";
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    string S2 = matrix[j, i].ToString("x");
                    if (S2.Length < 2)
                        S += "0" + S2;
                    else
                        S += S2;
                }

            }
            S = S.ToUpper();
            S = "0x" + S;
            return S;
        }
        public override string Decrypt(string cipherText, string key)
        {
            cipherText = cipherText.Remove(0, 2);
            cipherText = cipherText.ToLower();
            key = key.Remove(0, 2);
            key = key.ToLower();
            string inverse_Mcolumn = "0E0B0D09090E0B0D0D090E0B0B0D090E";
            MakeMatrix(cipherText, 1);
            MakeMatrix(inverse_Mcolumn, 2);
            MakeMatrix(key, 3);
            inverseSbox();
            for (int i = 0; i < 10; i++)
                keySchedule(i);

            RoundKey();
            shiftrows_Dec();
            SubBytes_Dec(matrix, 4);

            for (int i = 9; i >= 1; i--)
            {
                keyschedule_Dec(i);
                RoundKey();
                MixColumns();
                shiftrows_Dec();
                SubBytes_Dec(matrix, 4);
            }
            keyschedule_Dec(0);
            RoundKey();
            return Res();
        }

        public override string Encrypt(string plainText, string key)
        {
            plainText = plainText.Remove(0, 2);
            plainText = plainText.ToLower();
            key = key.Remove(0, 2);
            key = key.ToLower();
            MakeMatrix(plainText, 1);
            string ColMat = "02030101010203010101020303010102";
            MakeMatrix(ColMat, 2);
            MakeMatrix(key, 3);

            RoundKey();
            keySchedule(0);

            for (int n = 1; n < 10; n++)
            {
                SubBytes(matrix, 4, 4);
                ShiftRows(4);
                MixColumns();
                RoundKey();
                keySchedule(n);
            }
            SubBytes(matrix, 4, 4);
            ShiftRows(4);
            RoundKey();
            return Res();
        }
    }
}
