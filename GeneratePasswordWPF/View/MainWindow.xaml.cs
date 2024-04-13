using GeneratePasswordWPF.Model.DbTables;
using GeneratePasswordWPF.Model.Services;
using GeneratePasswordWPF.ViewModel;
using System;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Windows.UI.Notifications;

namespace GeneratePasswordWPF
{
    public partial class MainWindow : Window
    {
        GeneratePassword generatePassword = new GeneratePassword();
        Random random = new Random();
        ApplicationDb applicationDb = new ApplicationDb();
        public MainWindow()
        {
            InitializeComponent();
            applicationDb = new ApplicationDb();

        }

        public void FullScreenState()
        {
            if (WindowState == WindowState.Maximized)
            {
                borderWindowScreen.CornerRadius = new CornerRadius(20);
                WindowState = WindowState.Normal;
            }
            else
            {
                borderWindowScreen.CornerRadius = new CornerRadius(0);
                WindowState = WindowState.Maximized;
            }
        }

        private void borderClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void borderInFullScreen_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FullScreenState();
        }

        private void borderHide_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void borderMoveScreen_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.ClickCount == 2)
            {
                FullScreenState();
            }
            else
            {
                DragMove();
            }
        }

        // у левого мену все меняет типо там где будет меняться grid 
        public void mouseEnterAndLeaveBorderGrid(Border border, string backgroundColor, Size size)
        {
            Color colorBack = (Color)ColorConverter.ConvertFromString(backgroundColor);
            border.Background = new SolidColorBrush(colorBack);
            border.Width = size.Width;
            border.Height = size.Height;
        }

        private void borderLeftMenu_MouseEnter(object sender, MouseEventArgs e)
        {
            Border border = (Border)sender;
            mouseEnterAndLeaveBorderGrid(border, "#A972FE", new Size(50, 50));
        }

        private void borderLeftMenu_MouseLeave(object sender, MouseEventArgs e)
        {
            Border border = (Border)sender;
            mouseEnterAndLeaveBorderGrid(border, "#00000000", new Size(40, 40));
        }

        //метод для смены цвета border у кнопок под label
        public void mouseEnterAndLeaveLabelBorder(Border border, string borderBrush, Size size)
        {
            Color colorBrush = (Color)ColorConverter.ConvertFromString(borderBrush);
            border.BorderBrush = new SolidColorBrush(colorBrush);
            border.Width = size.Width;
            border.Height = size.Height;
        }

        private void LabelBorderClickVariblePassword_MouseEnter(object sender, MouseEventArgs e)
        {
            Border border = (Border)sender;
            mouseEnterAndLeaveLabelBorder(border, "#6600FF", new Size(100, 60));
        }

        private void LabelBorderClickVariblePassword_MouseLeave(object sender, MouseEventArgs e)
        {
            Border border = (Border)sender;
            mouseEnterAndLeaveLabelBorder(border, "#7163ba", new Size(80, 40));
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Border border = (Border)sender;
            if (border.BorderBrush.Equals(Brushes.White))
            {
                // Сейчас кнопка активна, делаем неактивной
                border.MouseEnter += LabelBorderClickVariblePassword_MouseEnter;
                border.MouseLeave += LabelBorderClickVariblePassword_MouseLeave;
            }
            else
            {
                // Сейчас кнопка неактивна, делаем активной
                border.BorderBrush = Brushes.White;
                border.MouseEnter -= LabelBorderClickVariblePassword_MouseEnter;
                border.MouseLeave -= LabelBorderClickVariblePassword_MouseLeave;
            }
        }

        private void variblePasswordLet(List<string> lettersList)
        {
            if (int.TryParse(amount.Text, out var amountPass))
            {
                string password = generatePassword.GenerateVariblePasswordLet(lettersList, amountPass);
                resultPassword.Content = password;
            }
        }

        private void varibleLoginLet(List<string> lettersList)
        {
            if (int.TryParse(amount.Text, out var amountPass))
            {
                string login = generatePassword.GenerateVariblePasswordLet(lettersList, amountPass);
                resultLogin.Content = login;
            }
        }

        private void borderClickLoginGenerate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            List<string> combinedList = new List<string>();
            if (borderAZ.BorderBrush == Brushes.White)
            {
                varibleLoginLet(ListPassSymbol.lettersLowerAndUpper);
            }
            if (border09.BorderBrush == Brushes.White)
            {
                varibleLoginLet(ListPassSymbol.numberList);
            }
            if (borderSymbol.BorderBrush == Brushes.White)
            {
                varibleLoginLet(ListPassSymbol.symbolList);
            }
            if (borderAZ.BorderBrush == Brushes.White && border09.BorderBrush == Brushes.White)
            {

                combinedList.AddRange(ListPassSymbol.lettersLowerAndUpper);
                combinedList.AddRange(ListPassSymbol.numberList);
                varibleLoginLet(combinedList);
            }
            if (borderAZ.BorderBrush == Brushes.White && borderSymbol.BorderBrush == Brushes.White)
            {

                combinedList.AddRange(ListPassSymbol.lettersLowerAndUpper);
                combinedList.AddRange(ListPassSymbol.symbolList);
                varibleLoginLet(combinedList);
            }
            if (border09.BorderBrush == Brushes.White && borderSymbol.BorderBrush == Brushes.White)
            {

                combinedList.AddRange(ListPassSymbol.numberList);
                combinedList.AddRange(ListPassSymbol.symbolList);
                varibleLoginLet(combinedList);
            }
            if (borderAZ.BorderBrush == Brushes.White && border09.BorderBrush == Brushes.White && borderSymbol.BorderBrush == Brushes.White)
            {
                combinedList.AddRange(ListPassSymbol.lettersLowerAndUpper);
                combinedList.AddRange(ListPassSymbol.numberList);
                combinedList.AddRange(ListPassSymbol.symbolList);
                varibleLoginLet(combinedList);
            }
        }

        private void borderClickPasswordGenerate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            List<string> combinedList = new List<string>();
            if (borderAZ.BorderBrush == Brushes.White)
            {
                variblePasswordLet(ListPassSymbol.lettersLowerAndUpper);
            }
            if (border09.BorderBrush == Brushes.White)
            {
                variblePasswordLet(ListPassSymbol.numberList);
            }
            if (borderSymbol.BorderBrush == Brushes.White)
            {
                variblePasswordLet(ListPassSymbol.symbolList);
            }
            if (borderAZ.BorderBrush == Brushes.White && border09.BorderBrush == Brushes.White)
            {

                combinedList.AddRange(ListPassSymbol.lettersLowerAndUpper);
                combinedList.AddRange(ListPassSymbol.numberList);
                variblePasswordLet(combinedList);
            }
            if (borderAZ.BorderBrush == Brushes.White && borderSymbol.BorderBrush == Brushes.White)
            {

                combinedList.AddRange(ListPassSymbol.lettersLowerAndUpper);
                combinedList.AddRange(ListPassSymbol.symbolList);
                variblePasswordLet(combinedList);
            }
            if (border09.BorderBrush == Brushes.White && borderSymbol.BorderBrush == Brushes.White)
            {

                combinedList.AddRange(ListPassSymbol.numberList);
                combinedList.AddRange(ListPassSymbol.symbolList);
                variblePasswordLet(combinedList);
            }
            if (borderAZ.BorderBrush == Brushes.White && border09.BorderBrush == Brushes.White && borderSymbol.BorderBrush == Brushes.White)
            {
                combinedList.AddRange(ListPassSymbol.lettersLowerAndUpper);
                combinedList.AddRange(ListPassSymbol.numberList);
                combinedList.AddRange(ListPassSymbol.symbolList);
                variblePasswordLet(combinedList);
            }
        }

        private void copyPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (resultPassword.Content.ToString() == "Тут будет находиться ваш пароль!")
            {

            }
            else
            {
                string originalTextPassword = resultPassword.ToString();
                string deleteText = "System.Windows.Controls.Label: ";
                Clipboard.SetText(originalTextPassword.Substring(deleteText.Length));

            }
        }

        private void copyLogin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (resultLogin.Content.ToString() == "Тут будет находиться ваш логин!")
            {
            }
            else
            {
                string originalTextLogin = resultLogin.ToString();
                string deleteText = "System.Windows.Controls.Label: ";
                Clipboard.SetText(originalTextLogin.Substring(deleteText.Length));
            }
        }

        private void borderSaveAcc_MouseEnter(object sender, MouseEventArgs e)
        {
            Border border = (Border)sender;
            mouseEnterAndLeaveLabelBorder(border, "#6600FF", new Size(220, 60));
        }

        private void borderSaveAcc_MouseLeave(object sender, MouseEventArgs e)
        {
            Border border = (Border)sender;
            mouseEnterAndLeaveLabelBorder(border, "#7163ba", new Size(200, 50));
        }

        private async void borderSaveAcc_MouseDown(object sender, MouseButtonEventArgs e)
        {
            applicationDb.AddAcc(resultLogin.Content.ToString(),resultPassword.Content.ToString());
        }
    }
}
