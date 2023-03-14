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
            /* plainText = plainText.ToLower();
             cipherText = cipherText.ToLower();
             int size = plainText.Length;
             List<List<int>> Permute(int n)
             {
                 var result = new List<List<int>>();
                 var nums = new List<int>();
                 for (int i = 1; i <= n; i++) nums.Add(i);
                 PermuteHelper(nums, 0, result);
                 return result;
             }

             void PermuteHelper(List<int> nums, int start, List<List<int>> result)
             {
                 if (start == nums.Count)
                 {
                     result.Add(new List<int>(nums));
                     return;
                 }
                 for (int i = start; i < nums.Count; i++)
                 {
                     Swap(nums, start, i);
                     PermuteHelper(nums, start + 1, result);
                     Swap(nums, start, i);
                 }
             }

             void Swap(List<int> nums, int i, int j)
             {
                 int temp = nums[i];
                 nums[i] = nums[j];
                 nums[j] = temp;
             }
             List<List<int>> permutation = new List<List<int>>();
             string encrypted;
             for (int i = 1; i <= size; i++) {
                 permutation = Permute(i);

                 for (int j = 0; j < permutation.Count; i++) {
                     encrypted = Encrypt(plainText, permutation[j]);
                     if (encrypted == cipherText) return permutation[j];
                 }
             }

             return null;
            */
            throw new NotImplementedException();

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
