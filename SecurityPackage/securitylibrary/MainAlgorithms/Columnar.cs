using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Columnar : ICryptographicTechnique<string, List<int>>
    {

        public List<int> Analyse(string plainText, string cipherText)
        {
            plainText = plainText.ToLower();
            cipherText = cipherText.ToLower();
            int size = plainText.Length;


            List<List<int>> Permute(int n)
            {
                List<List<int>> permutations = new List<List<int>>();

                List<int> nums = new List<int>();
                for (int i = 1; i <= n; i++)
                {
                    nums.Add(i);
                }

                GeneratePermutations(permutations, nums, 0, n - 1);

                return permutations;
            }

            void GeneratePermutations(List<List<int>> permutations, List<int> nums, int left, int right)
            {
                if (left == right)
                {
                    permutations.Add(new List<int>(nums));
                }
                else
                {
                    for (int i = left; i <= right; i++)
                    {
                        // Swap the current element with the first element
                        // and recursively generate permutations with the remaining elements
                        Swap(nums, left, i);
                        GeneratePermutations(permutations, nums, left + 1, right);
                        Swap(nums, left, i);
                    }
                }
            }

            void Swap(List<int> list, int i, int j)
            {
                int temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }


            string encrypted;
            for (int i = 1; i <= size; i++)
            {
                List<List<int>> permutation = new List<List<int>>();
                permutation = Permute(i);

                for (int j = 0; j < permutation.Count; j++)
                {
                    encrypted = Encrypt(plainText, permutation[j]);
                    if (encrypted == cipherText) return permutation[j];
                }
            }

            return null;

        }


        public string Decrypt(string cipherText, List<int> key)
        {
            string output = "";
            int lSize = key.Count, sSize = cipherText.Length;
            int depth = sSize / lSize;
            for (int i = 0; i < depth; i++)
            {
                for (int j = 0; j < lSize; j++)
                {
                    output += cipherText[((key[j] - 1) * depth) + i];
                }
            }
            return output;
        }

        public string Encrypt(string plainText, List<int> key)
        {
            int lSize = key.Count, sSize = plainText.Length;
            string output = "";

            for (int i = 1; i <= lSize; i++)
            {
                for (int j = 0; j < lSize; j++)
                {
                    if (key[j] == i)
                    {
                        for (int z = j; z < sSize; z += lSize)
                        {
                            output += plainText[z];
                        }
                    }
                }
            }
            return output;
        }
    }
}
