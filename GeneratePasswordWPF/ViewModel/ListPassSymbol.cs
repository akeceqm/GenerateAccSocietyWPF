using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratePasswordWPF.ViewModel
{
    public static class ListPassSymbol
    {
        private static List<string> letters = new List<string> { "q", "w", "e", "r", "t", "y", "u", "i", "o", "p", "a", "s", "d", "f", "g", "h", "j", "k", "l", "z", "x", "c", "v", "b", "n", "m" };
        private static List<string> lettersLower = new();
        private static List<string> lettersUpper = new();
        public static List<string> numberList = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
        public static List<string> symbolList = new List<string> { "!", "@", "#", "$", "%", "^", "&", "*", "!", "@", "#", "$", "%", "^", "&", "*", "!", "@", "#", "$", "%", "^", "&", "*" };
        public static List<string> lettersLowerAndUpper = new();
        static ListPassSymbol()
        {
            GetLettersLower();
            GetLettersUpper();
            GetLettersLowerAndUpper();
        }

        private static List<string> GetLettersLower()
        {
            foreach (string letter in letters)
            {
                lettersLower.Add(letter.ToLower());
            }
            return lettersLower;
        }

        private static List<string> GetLettersUpper()
        {
            foreach (string letter in letters)
            {
                lettersUpper.Add(letter.ToUpper());
            }
            return lettersUpper;
        }

        private static List<string> GetLettersLowerAndUpper()
        {
            lettersLowerAndUpper.AddRange(lettersUpper);
            lettersLowerAndUpper.AddRange(lettersLower);
            return lettersLowerAndUpper;
        }

    }
}
