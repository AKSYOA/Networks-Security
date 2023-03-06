using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Monoalphabetic : ICryptographicTechnique<string, string>
    {
        string alphabetic = "abcdefghijklmnopqrstuvwxyz";

        public string Analyse(string plainText, string cipherText)
        {
            List<char> alphabet = new List<char>
            {
                'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z'
            };

            cipherText = cipherText.ToLower();
            var key = new char[26];
            
            for (int i = 0; i < cipherText.Length; i++)
            {
                int index = plainText[i] - 'a';
                key[index] = cipherText[i];
                alphabet.Remove(cipherText[i]);
            }

            for (int i = 0; i < 26; i++)
            {
                if (key[i] != '\0')
                    continue;
                key[i] = alphabet[0];
                alphabet.RemoveAt(0);
            }
            return new string(key);
        }


        public string Decrypt(string cipherText, string key)
        {
            cipherText = cipherText.ToLower();
            key = key.ToLower();

            String plainText = "";
            SortedList Pair_list = new SortedList();

            for (int i = 0; i < key.Length; i++)
            {
                Pair_list.Add(key[i], alphabetic[i]);
            }

            for (int i = 0; i < cipherText.Length; i++)
            {
                for (int j = 0; j < alphabetic.Length; j++)
                {
                    if (cipherText[i].Equals(Pair_list.GetKey(j)))
                    {
                        plainText += Pair_list.GetByIndex(j);
                        break;
                    }
                }
            }
            return plainText;
        }

        public string Encrypt(string plainText, string key)
        {
  

            String cipher = "";
            SortedList Pair_list = new SortedList();

            if (plainText.Equals(plainText.ToUpper()))
            {
                alphabetic = alphabetic.ToUpper();
            }

            for (int i=0; i < alphabetic.Length; i++){
                Pair_list.Add(alphabetic[i], key[i]);
            }

            for (int i = 0; i < plainText.Length; i++) {
                for (int j = 0; j < alphabetic.Length; j++)
                {
                    if (plainText[i].Equals(Pair_list.GetKey(j)))
                    {
                        cipher += Pair_list.GetByIndex(j);
                        break;
                    }
                }   
            }
            return cipher;          
        }




        /// <summary>
        /// Frequency Information:
        /// E   12.51%
        /// T	9.25
        /// A	8.04
        /// O	7.60
        /// I	7.26
        /// N	7.09
        /// S	6.54
        /// R	6.12
        /// H	5.49
        /// L	4.14
        /// D	3.99
        /// C	3.06
        /// U	2.71
        /// M	2.53
        /// F	2.30
        /// P	2.00
        /// G	1.96
        /// W	1.92
        /// Y	1.73
        /// B	1.54
        /// V	0.99
        /// K	0.67
        /// X	0.19
        /// J	0.16
        /// Q	0.11
        /// Z	0.09
        /// </summary>
        /// <param name="cipher"></param>
        /// <returns>Plain text</returns>
        /// 

        public Dictionary<char, int> Num_Of_Char = new Dictionary<char, int>();
        
        public string AnalyseUsingCharFrequency(string cipher)
        {

            string Char_frequency = "ETAOINSRHLDCUMFBGWYBVKXJZ";
            Char_frequency = Char_frequency.ToLower();
            int Char_Count = 0;
            int index = 0;

            for (int i = 0; i < cipher.Length; i++)
            {
                for (int j = 0; j < cipher.Length; j++)
                {
                    if (cipher[i] == cipher[j])
                        Char_Count += 1;
                }
                if (!Num_Of_Char.ContainsKey(cipher[i]))
                    Num_Of_Char.Add(cipher[i], Char_Count);

                Char_Count = 0;
            }


            for (int i = 0; i < Num_Of_Char.Count; i++) 
            {
                int maxVal = Num_Of_Char.Values.Max();
                char maxKey = MaxKey(Num_Of_Char, maxVal);

                for (int j = 0; j < cipher.Length; j++) 
                {
                    if(maxKey.Equals(cipher[j]))
                    {
                        cipher = cipher.Replace(maxKey, Char_frequency[index]);
                        index++;
                        break;
                    }
                }
                Num_Of_Char.Remove(maxKey);
            }
            return cipher;
        }






        public char MaxKey(Dictionary<char, int> Char, int max)
        {
            char M_key = new char();
            foreach (KeyValuePair<char, int> pair in Char)
            {
                if (pair.Value == max)
                {
                    M_key = pair.Key;
                    break;
                }
            }
            return M_key;
        }
    }


}
