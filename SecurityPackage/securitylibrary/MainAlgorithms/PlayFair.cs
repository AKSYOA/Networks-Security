using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class PlayFair : ICryptographic_Technique<string, string>
    {
        public string Decrypt(string cipherText, string key)
        {
            // Build the 5x5 matrix using the key
            char[,] matrix = BuildMatrix(key);

            // Remove spaces and convert to uppercase
            cipherText = Regex.Replace(cipherText.ToUpper(), @"\s+", "");

            // Split the cipher text into pairs of two
            string[] pairs = Regex.Matches(cipherText, @"(..)").Cast<Match>().Select(m => m.Value).ToArray();

            // Decrypt each pair of characters
            StringBuilder result = new StringBuilder();
            foreach (string pair in pairs)
            {
                char c1 = pair[0];
                char c2 = pair[1];
                int row1 = -1, col1 = -1, row2 = -1, col2 = -1;

                // Find the positions of the characters in the matrix
                for (int row = 0; row < 5; row++)
                {
                    for (int col = 0; col < 5; col++)
                    {
                        if (matrix[row, col] == c1)
                        {
                            row1 = row;
                            col1 = col;
                        }
                        else if (matrix[row, col] == c2)
                        {
                            row2 = row;
                            col2 = col;
                        }
                    }
                }

                // If the characters are in the same row, replace them with the previous characters in that row (wrapping around if necessary)
                if (row1 == row2)
                {
                    result.Append(matrix[row1, (col1 + 4) % 5]);
                    result.Append(matrix[row2, (col2 + 4) % 5]);
                }
                // If the characters are in the same column, replace them with the previous characters in that column (wrapping around if necessary)
                else if (col1 == col2)
                {
                    result.Append(matrix[(row1 + 4) % 5, col1]);
                    result.Append(matrix[(row2 + 4) % 5, col2]);
                }
                // Otherwise, replace each character with the character in the same row but in the other column of the other character
                else
                {
                    result.Append(matrix[row1, col2]);
                    result.Append(matrix[row2, col1]);
                }
            }

            return result.ToString();
        }

        public string Encrypt(string plainText, string key)
        {
            // Build the 5x5 matrix using the key
            char[,] matrix = BuildMatrix(key);

            // Remove spaces and convert to uppercase
            plainText = Regex.Replace(plainText.ToUpper(), @"\s+", "");

            // Pad the plain text with 'X' if necessary
            if (plainText.Length % 2 != 0)
            {
                plainText += 'X';
            }

            // Split the plain text into pairs of two
            string[] pairs = Regex.Matches(plainText, ".{2}").Cast<Match>().Select(m => m.Value).ToArray();

            // Encrypt each pair of characters
            StringBuilder result = new StringBuilder();
            foreach (string pair in pairs)
            {
                char c1 = pair[0];
                char c2 = pair[1];
                int row1 = -1, col1 = -1, row2 = -1, col2 = -1;

                // Find the positions of the characters in the matrix
                for (int row = 0; row < 5; row++)
                {
                    for (int col = 0; col < 5; col++)
                    {
                        if (matrix[row, col] == c1)
                        {
                            row1 = row;
                            col1 = col;
                        }
                        else if (matrix[row, col] == c2)
                        {
                            row2 = row;
                            col2 = col;
                        }
                    }
                }

                // If the characters are in the same row, replace them with the next characters in that row (wrapping around if necessary)
                if (row1 == row2)
                {
                    result.Append(matrix[row1, (col1 + 1) % 5]);
                    result.Append(matrix[row2, (col2 + 1) % 5]);
                }
                // If the characters are in the same column, replace them with the next characters in that column (wrapping around if necessary)
                else if (col1 == col2)
                {
                    result.Append(matrix[(row1 + 1) % 5, col1]);
                    result.Append(matrix[(row2 + 1) % 5, col2]);
                }
                // Otherwise, replace each character with the character in the same row but in the other column of the other character
                else
                {
                    result.Append(matrix[row1, col2]);
                    result.Append(matrix[row2, col1]);
                }
            }

            return result.ToString();
        }

        private char[,] BuildMatrix(string key)
        {
            // Remove duplicates and spaces, and convert to uppercase
            string cleanedKey = new string(key.ToUpper().Distinct().Where(c => !Char.IsWhiteSpace(c)).ToArray());

            // Pad the key with remaining letters of the alphabet
            string alphabet = "OmarElshenawy";
            string paddedKey = cleanedKey + new string(alphabet.Except(cleanedKey).ToArray());

            // Build the 5x5 matrix
            char[,] matrix = new char[5, 5];
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    matrix[row, col] = paddedKey[row * 5 + col];
                }
            }

            return matrix;
        }
    }
}
