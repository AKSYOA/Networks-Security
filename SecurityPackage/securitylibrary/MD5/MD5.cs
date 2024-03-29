﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.MD5
{
    public class MD5
    {
        /*
         * Shift values
         */ 
        static int[] s = new int[64] {
            7, 12, 17, 22,  7, 12, 17, 22,  7, 12, 17, 22,  7, 12, 17, 22,
            5,  9, 14, 20,  5,  9, 14, 20,  5,  9, 14, 20,  5,  9, 14, 20,
            4, 11, 16, 23,  4, 11, 16, 23,  4, 11, 16, 23,  4, 11, 16, 23,
            6, 10, 15, 21,  6, 10, 15, 21,  6, 10, 15, 21,  6, 10, 15, 21
        };

        /*
         * Constant K Values
         */
        static uint[] K = new uint[64] {
            0xd76aa478, 0xe8c7b756, 0x242070db, 0xc1bdceee,
            0xf57c0faf, 0x4787c62a, 0xa8304613, 0xfd469501,
            0x698098d8, 0x8b44f7af, 0xffff5bb1, 0x895cd7be,
            0x6b901122, 0xfd987193, 0xa679438e, 0x49b40821,
            0xf61e2562, 0xc040b340, 0x265e5a51, 0xe9b6c7aa,
            0xd62f105d, 0x02441453, 0xd8a1e681, 0xe7d3fbc8,
            0x21e1cde6, 0xc33707d6, 0xf4d50d87, 0x455a14ed,
            0xa9e3e905, 0xfcefa3f8, 0x676f02d9, 0x8d2a4c8a,
            0xfffa3942, 0x8771f681, 0x6d9d6122, 0xfde5380c,
            0xa4beea44, 0x4bdecfa9, 0xf6bb4b60, 0xbebfbc70,
            0x289b7ec6, 0xeaa127fa, 0xd4ef3085, 0x04881d05,
            0xd9d4d039, 0xe6db99e5, 0x1fa27cf8, 0xc4ac5665,
            0xf4292244, 0x432aff97, 0xab9423a7, 0xfc93a039,
            0x655b59c3, 0x8f0ccc92, 0xffeff47d, 0x85845dd1,
            0x6fa87e4f, 0xfe2ce6e0, 0xa3014314, 0x4e0811a1,
            0xf7537e82, 0xbd3af235, 0x2ad7d2bb, 0xeb86d391
        };

        public static uint shiftAndRotate(uint x, int c)
        {
            return (x << c) | (x >> (32 - c));
        }
        private static string byteToString(uint x)
        {
            return String.Join("", BitConverter.GetBytes(x).Select(y => y.ToString("x2")));
        }
        public string GetHash(string text)
        {
            //Buffers initialization
            uint OIV_A = 0x67452301;   
            uint OIV_B = 0xefcdab89;   
            uint OIV_C = 0x98badcfe;   
            uint OIV_D = 0x10325476;
            byte[] input = Encoding.UTF8.GetBytes(text);
            
            //Console.WriteLine(input.Length);
            //Console.WriteLine(text);
       
            BitConverter.ToString(input);
            //foreach(byte x in input) Console.WriteLine(x);
            int zerosPadding = (64 - ((input.Length + 9) % 64)) % 64; // calculate the new length with padding

            byte[] byteRepresentation = new byte[input.Length + 1 + zerosPadding + 8];
            Array.Copy(input, byteRepresentation, input.Length);

            byteRepresentation[input.Length] = 0x80; // add 1

            byte[] lengthInBits = BitConverter.GetBytes(input.Length * 8); // bit converter returns little-endian
            Array.Copy(lengthInBits, 0, byteRepresentation, byteRepresentation.Length - 8, 4); // add length in bits

            for (int i = 0; i < byteRepresentation.Length / 64; i++)
            {
                uint[] M = new uint[16];
                for (int j = 0; j < 16; ++j)
                    M[j] = BitConverter.ToUInt32(byteRepresentation, (i * 64) + (j * 4));

                // initialize round variables
                uint A = OIV_A, B = OIV_B, C = OIV_C, D = OIV_D, F, g;

                for (uint k = 0; k < 64; ++k)
                {
                    if (k <= 15)
                    {
                        F = (B & C) | (~B & D);
                        g = k;
                    }
                    else if (k >= 16 && k <= 31)
                    {
                        F = (D & B) | (~D & C);
                        g = ((5 * k) + 1) % 16;
                    }
                    else if (k >= 32 && k <= 47)
                    {
                        F = B ^ C ^ D;
                        g = ((3 * k) + 5) % 16;
                    }
                    else 
                    {
                        F = C ^ (B | ~D);
                        g = (7 * k) % 16;
                    }

                    uint dtemp = D;
                    D = C;
                    C = B;
                    B = B + shiftAndRotate((A + F + K[k] + M[g]), s[k]);
                    A = dtemp;
                }

                OIV_A += A;
                OIV_B += B;
                OIV_C += C;
                OIV_D += D;
            }
            return byteToString(OIV_A) + byteToString(OIV_B) + byteToString(OIV_C) + byteToString(OIV_D);
        }
    }
}
