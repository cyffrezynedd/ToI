using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    internal class Vigenera
    {
        private const byte alphabetLen = 33;
        private Dictionary<Char, Byte> rusAlphabet { get; set; }

        private Dictionary<Byte, Char> reversedRusAlphabet { get; set; }

        internal string inputText { get; set; }

        internal string key { get; set; }
        
        private void fillAlphabet()
        {
            byte count = 0;
            rusAlphabet = new Dictionary<char, byte>();
            reversedRusAlphabet = new Dictionary<byte, char>();
            for (char i = 'А'; i <= 'Я'; i++)
            {
                if (count == 6)
                {
                    rusAlphabet.Add('Ё', count);
                    reversedRusAlphabet.Add(count, 'Ё');
                    i--;
                }
                else
                {
                    rusAlphabet.Add(i, count);
                    reversedRusAlphabet.Add(count, i);
                }
                count++;
            }
        }

        private void generateKey()
        {
            Int16 count = 0;
            while (key.Length <= inputText.Length)
            {
                key += inputText[count++];
                if (count == inputText.Length)
                {
                    count = 0;
                }
            }
        }

        public string encryptText()
        {
            string encryptedText = "";
            Int16 count = 0;

            fillAlphabet();
            generateKey();

            foreach(char symbol in inputText)
            {
                encryptedText += reversedRusAlphabet[(byte) ((rusAlphabet[symbol] + rusAlphabet[key[count++]]) % alphabetLen)];
            }

            return encryptedText;
        }

        public string decryptText()
        {
            string decryptedText = "";
            Int16 count = 0;

            fillAlphabet();

            foreach(char symbol in inputText)
            {
                decryptedText += reversedRusAlphabet[(byte) ((alphabetLen + rusAlphabet[symbol] - rusAlphabet[key[count]]) % alphabetLen)];
                key += decryptedText[count++];
            }

            return decryptedText;
        }
    }
}
