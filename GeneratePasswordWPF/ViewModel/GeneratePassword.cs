using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratePasswordWPF.ViewModel
{
    public class GeneratePassword
    {
        private Random random = new Random();
        public string GenerateVariblePasswordLet(List<string> lettersList, int amountPass)
        {
            StringBuilder passwordBuilder = new StringBuilder();
            int listSize = lettersList.Count;
            for (int i = 0; i < amountPass; i++)
            {
                int randomIndex = random.Next(listSize);
                string randomChar = lettersList[randomIndex];
                passwordBuilder.Append(randomChar);
            }
            return passwordBuilder.ToString();
        }

    }
}
