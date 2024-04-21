using GeneratePasswordWPF.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GeneratePasswordWPF.ViewModel
{
    public static class MessageBoxCustomManager
    {
        public static void Show(string label, string message)
        {
            MessageBoxCustom messageBoxCustom = new MessageBoxCustom();
            messageBoxCustom.MessageLabel = label;
            messageBoxCustom.MessageError = message;
            if (label == "Успешно")
            {
                messageBoxCustom.Foreground = new SolidColorBrush(Colors.Green);
            }
            else
            {
                messageBoxCustom.Foreground = new SolidColorBrush(Colors.Red);
            }
            messageBoxCustom.Show();
        }
    }
}
