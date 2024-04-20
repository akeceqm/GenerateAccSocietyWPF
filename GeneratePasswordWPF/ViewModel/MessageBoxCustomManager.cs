using GeneratePasswordWPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratePasswordWPF.ViewModel
{
    public static class MessageBoxCustomManager
    {
        public static void Show(string message)
        {
            MessageBoxCustom messageBoxCustom = new MessageBoxCustom();
            messageBoxCustom.MessageError = message;
            messageBoxCustom.Show();
        }
    }
}
