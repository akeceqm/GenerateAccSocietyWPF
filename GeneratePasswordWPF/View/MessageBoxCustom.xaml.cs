using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GeneratePasswordWPF.View
{
    public partial class MessageBoxCustom : Window
    {
        public string MessageError { get; set; }
        public MessageBoxCustom()
        {
            InitializeComponent();
            DataContext = this;

        }
        private void closeMessageBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
