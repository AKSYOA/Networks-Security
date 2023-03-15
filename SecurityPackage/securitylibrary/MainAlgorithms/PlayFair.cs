using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class PlayFair : ICryptographic_Technique<string, string>
    {
        public string Decrypt(string cipherText, string key)
        {
            char[,] Table = generateTable(key);
            cipherText = cipherText.ToLower();
            
            string plain = "";

            for (int i = 0; i < cipherText.Length; i += 2)
            {
                plain += inverseSearch(cipherText[i], cipherText[i + 1], Table);
            }

            Console.WriteLine(postprocessPlainText(plain));
            return postprocessPlainText(plain);
        }

        public string Encrypt(string plainText, string key)
        {

            char[,] Table = generateTable(key);
            string newPlainText = preprocessPlainText(plainText);

            string cipher = "";

            for (int i =0; i <  newPlainText.Length; i += 2)
            {
                cipher += search(newPlainText[i], newPlainText[i + 1], Table);
            }

            return cipher;
        }


        private char[,] generateTable(string key) {

            string alphabet = "abcdefghiklmnopqrstuvwxyz";
            string restAlphabet = key + alphabet;
            restAlphabet = string.Join("", restAlphabet.ToCharArray().Distinct()); // remove duplicates

            char[,] Table = new char[5, 5];

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Table[i, j] = restAlphabet[ (i * 5) + j];
                }
            }

            return Table;
        }
       
        private string preprocessPlainText(string plainText)
        {
            for (int i = 0; i < plainText.Length; i++)
            {

                if (i + 1 < plainText.Length && plainText[i] == plainText[i + 1])
                {
                    if (i % 2 == 0)
                    {
                        plainText = plainText.Insert(i + 1, "x");
                    }
                }          
            }

            if (plainText.Length % 2  != 0)
                plainText += "x";

            return plainText;
        }

        private string search(char A, char B, char[,] Table)
        {
            int indexArow = -1, indexAcol = -1, indexBrow = -1, indexBcol = -1;
            string answer = "";
            for (int i = 0; i < 5; i++)
            {
                for (int j =0; j < 5; j++)
                {
                    if (indexArow != -1 && indexBrow != -1)
                    {
                        break;
                    }

                    if (Table[i,j] == A)
                    {
                        indexArow = i;
                        indexAcol = j;
                    }else if (Table[i,j] == B)
                    {
                        indexBrow = i;
                        indexBcol = j;
                    }

                }
            }

            if (indexArow == indexBrow)
            {
                answer += Table[indexArow, (indexAcol + 1) % 5];
                answer += Table[indexBrow, (indexBcol + 1) % 5];

            }
            else if (indexAcol == indexBcol)
            {
                answer += Table[ (indexArow + 1) % 5, indexAcol];
                answer += Table[ (indexBrow + 1) % 5, indexBcol];
            }else
            {
                answer += Table[indexArow , indexBcol];
                answer += Table[indexBrow, indexAcol];
            }

            return answer;
        }

        private string inverseSearch(char A, char B, char[,] Table)
        {
            int indexArow = -1, indexAcol = -1, indexBrow = -1, indexBcol = -1;
            string answer = "";
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (indexArow != -1 && indexBrow != -1)
                    {
                        break;
                    }

                    if (Table[i, j] == A)
                    {
                        indexArow = i;
                        indexAcol = j;
                    }
                    else if (Table[i, j] == B)
                    {
                        indexBrow = i;
                        indexBcol = j;
                    }

                }
            }

            if (indexArow == indexBrow)
            {
                answer += Table[indexArow, ((indexAcol - 1) < 0 ? (indexAcol - 1 + 5) : (indexAcol - 1)) % 5];
                answer += Table[indexBrow, ((indexBcol - 1) < 0 ? (indexBcol - 1 + 5) : (indexBcol - 1)) % 5];

            }
            else if (indexAcol == indexBcol)
            {
                answer += Table[((indexArow - 1) < 0 ? (indexArow - 1 + 5) : (indexArow - 1)) % 5, indexAcol];
                answer += Table[((indexBrow - 1) < 0 ? (indexBrow - 1 + 5) : (indexBrow - 1)) % 5, indexBcol];
            }
            else
            {
                answer += Table[indexArow, indexBcol];
                answer += Table[indexBrow, indexAcol];
            }

            return answer;
        }

        private string postprocessPlainText(string plainText) // postProcess huh? patent here
        {
            if (plainText[plainText.Length -1] == 'x')
                plainText = plainText.Remove(plainText.Length - 1, 1);

            for (int i = 0; i < plainText.Length; i+=2)
            {
                if (i+2 < plainText.Length && plainText[i] == plainText[i + 2] && plainText[i + 1] == 'x')
                {
                    plainText = plainText.Remove(i + 1, 1);
                    i--;
                }
            }

            return plainText;
        }
    }
}
