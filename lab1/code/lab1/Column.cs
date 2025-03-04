using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    internal class Column
    {
        internal string keyMain { get; set; }

        internal string keyExtra { get; set; }

        internal string inputText { get; set; }

        internal string outputText { get; set; }

        private Int16[] getReadingOrder(string key)
        {
            char prev = '\0';
            Int16 count = 1;
            Int16[] order = new Int16[key.Length];
            List<Char> sortedOrder = new List<Char>(key);
            sortedOrder.Sort();

            foreach (Char symbol in sortedOrder)
            {
                if (prev != symbol)
                {
                    for (byte i = 0; i < key.Length; i++)
                    {
                        if (key[i] == symbol)
                        {
                            order[i] = count++;
                        }
                    }

                    prev = symbol;
                }
            }

            Int16[] origOrder = new Int16[order.Length];
            for (Int16 i = 0; i < order.Length; i++)
            {
                origOrder[order[i] - 1] = (Int16) (i + 1);
            }

            return origOrder;
        }

        private Int16 getMatrixSize(string text, string key)
        {
            return text.Length % key.Length > 0 ? (Int16) (text.Length / key.Length + 1) : (Int16) (text.Length / key.Length);
        }

        public string encryptText(string plainText, string key)
        {
            Int16 i, j, count = 0;
            Int16 size = getMatrixSize(plainText, key);
            Int16[] order = getReadingOrder(key);
            string encryptedText = "";
            char[,] matrix = new char[size, key.Length];

            for (i = 0; i < size; i++)
            {
                for (j = 0; j < key.Length; j++)
                {
                    matrix[i, j] = count == plainText.Length ? ' ' : plainText[count++];
                }
            }

            foreach (Int16 number in order)
            {
                i = 0;
                while (i < size && matrix[i, number - 1] != ' ')
                {
                    encryptedText = encryptedText + matrix[i++, number - 1];
                }
            }

            return encryptedText;
        }
    
        public string decryptText(string encText, string key)
        {
            Int16 i, j, empty = (Int16) (encText.Length % key.Length), count = 0;
            if (empty == 0)
            {
                empty = (Int16)key.Length;
            }
            Int16 size = getMatrixSize(encText, key);
            Int16[] order = getReadingOrder(key);
            string decryptedText = "";
            char[,] matrix = new char[size, key.Length];

            foreach (Int16 number in order)
            {
                i = 0;
                while (count < encText.Length && (number <= empty ? i < size : i < size - 1))
                {
                    matrix[i++, number - 1] = encText[count++];
                }
            }

            for (i = 0; i < size; i++)
            {
                for (j = 0; j < key.Length; j++)
                {
                    if (matrix[i, j] != '\0')
                    {
                        decryptedText += matrix[i, j];

                    }
                }
            }

            return decryptedText;
        }
    }
}
