using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.MD5
{
    public class MD5
    {
        public string GetHash(string text)
        {
            int bitsSize;

            bitsSize = 512 * (((text.Length * 8) / 512) + 1);

            List<int> stringBitsRep = new List<int>();

            stringBitsRep = bitsConversion(text, bitsSize);

            /*for (int i = 0; i < stringBitsRep.Count; i++)
            {
                Console.WriteLine(stringBitsRep[i]);
            }
            */

            return "Ostorha 3lena ya rab";
        }

        List<int> bitsConversion(string message, int bitsSize) {

            List<int> bits = new List<int>();
            int messageSize = message.Length * 8;
            int padding = bitsSize - messageSize - 64;

            // Message + padding + message size represented in 64 bits
            // Message:
            List<int> singleChar = new List<int>();
            for (int i = 0; i < message.Length; i++)
            {
                int charAscii = (int)Convert.ToChar(message[i]);
                for (int j = 0; j < 8; j++)
                {
                    //Console.WriteLine(charAscii%2);
                    singleChar.Add(charAscii % 2);
                    charAscii /= 2;
                }
                for (int j = 7; j >= 0; j--)
                {
                    bits.Add(singleChar[j]);
                }
                singleChar.Clear();
            }
            //Console.WriteLine(padding);
            // Padding:

            for (int i = 0; i < padding; i++)
            {
                if (i == 0) bits.Add(1);
                else bits.Add(0);
            }

            //Message size in bits
            List<int> size = new List<int>();
            for (int i = 0; i < 64; i++)
            {
                size.Add(messageSize % 2);
                messageSize /= 2;
            }
            for (int i = 63; i >= 0; i--)
            {
                bits.Add(size[i]);
            }
            size.Clear();

            return bits;
        } 
    }
}
