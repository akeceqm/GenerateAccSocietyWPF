using GeneratePasswordWPF.Model.DbTables;
using GeneratePasswordWPF.Model.Services;
using GeneratePasswordWPF.View;
using GeneratePasswordWPF.ViewModel;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using System.Security.Cryptography.Xml;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using Windows.System;
using static GeneratePasswordWPF.Model.Services.ApplicationDb;


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
            LoadSocietiesInfoBox(dataGrdiPopupLsit);
            LoadSocietiesInfoBox(dataGridSelectSociety);

            List<SocietyInfo> societyInfos = applicationDb.SelectInfoSociety();
            List<UserInfo> users = applicationDb.SelectInfoUser();
            dataGridSelectInfo.ItemsSource = users;
        }

        class LoadNameDescSocial
        {
            public int SocietyId { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }

        private void LoadSocietiesInfoBox(ItemsControl text)
        {
            var listDesc = new List<string>();
            List<string> societies = applicationDb.SelectSociety(out listDesc);
            List<LoadNameDescSocial> listClass = new List<LoadNameDescSocial>();

            for (int i = 0; i < societies.Count; i++)
            {
                listClass.Add(new LoadNameDescSocial
                {
                    SocietyId = i + 1,
                    Name = societies[i],
                    Description = listDesc[i]

                });
            }
            text.ItemsSource = null;
            text.ItemsSource = listClass;

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
                resultPassword.Text = password;
            }
        }

        private void varibleLoginLet(List<string> lettersList)
        {
            if (int.TryParse(amount.Text, out var amountPass))
            {
                string login = generatePassword.GenerateVariblePasswordLet(lettersList, amountPass);
                resultLogin.Text = login;
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
            if (resultPassword.Text.ToString() == "Тут будет находиться ваш пароль!")
            {

            }
            else
            {
                string originalTextLogin = resultPassword.Text.ToString();
                Clipboard.SetText(originalTextLogin);

            }
        }

        private void copyLogin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (resultLogin.Text.ToString() == "Тут будет находиться ваш логин!")
            {
            }
            else
            {
                string originalTextLogin = resultLogin.Text.ToString();
                Clipboard.SetText(originalTextLogin);
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

        private void borderSaveAcc_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (resultLogin.Text == null || resultLogin.Text.ToString() == "Тут будет находиться ваш логин!")
            {
                MessageBoxCustomManager.Show("Ошибка", "Сгенерируйте логин!");
            }
            else if (resultPassword.Text == null || resultPassword.Text.ToString() == "Тут будет находиться ваш пароль!")
            {
                MessageBoxCustomManager.Show("Ошибка", "Сгенерируйте пароль!");
            }
            else if (dataGrdiPopupLsit.SelectedItem == null)
            {
                MessageBoxCustomManager.Show("Ошибка", "Выберите соц-сеть");
            }
            else
            {
                LoadNameDescSocial selectedSociety = (LoadNameDescSocial)dataGrdiPopupLsit.SelectedItem;
                string selectedSocietyName = selectedSociety.Name;
                int societyId = applicationDb.GetSocietyId(selectedSocietyName);
                applicationDb.AddAcc(resultLogin.Text.ToString(), resultPassword.Text.ToString(), societyId);
                MessageBoxCustomManager.Show("Успешно", "Все добавлен");

                LoadSocietiesInfoBox(dataGrdiPopupLsit);
                List<UserInfo> users = applicationDb.SelectInfoUser();
                dataGridSelectInfo.ItemsSource = users;


            }
        }

        private void DeleteButton_MouseDown(object sender, RoutedEventArgs e)
        {
            // Получаем кнопку, которая была нажата
            Border border = sender as Border;

            // Получаем соответствующую строку DataGrid
            if (border != null && border.DataContext is UserInfo userInfo)
            {
                // Удаляем из базы данных
                applicationDb.DelInfoUser(userInfo.Id);

                // Получаем текущий список пользователей
                List<UserInfo> users = (List<UserInfo>)dataGridSelectInfo.ItemsSource;

                // Удаляем этот объект из списка
                users.Remove(userInfo);

                // Обновляем ItemsSource с обновленным списком
                dataGridSelectInfo.ItemsSource = null;
                dataGridSelectInfo.ItemsSource = users;
            }
        }

        private void popupSociety_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LoadSocietiesInfoBox(dataGrdiPopupLsit);
            popupSociety.IsOpen = true;

        }

        private void popupBorderClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            popupSociety.IsOpen = false;
        }

        private void textPlaceHolder(object sender, string message)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Введите описание для соц-сети" || textBox.Text == "Введите соц-сеть")
            {
                textBox.Text = "";
            }
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = message;
            }
        }


        private void societyNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textPlaceHolder(textBox, "");
        }

        private void societyNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {

            TextBox textBox = (TextBox)sender;
            textPlaceHolder(textBox, "Введите соц-сеть");

        }

        private void societyDescTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textPlaceHolder(textBox, "");
        }

        private void societyDescTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textPlaceHolder(textBox, "Введите описание для соц-сети");
        }

        private void borderClickAddSociety_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (societyNameTextBox.Text.ToString() == null || societyNameTextBox.Text.ToString() == "Введите соц-сеть")
            {
                MessageBoxCustomManager.Show("Ошибка", "Задайте название соц-сети!");
            }
            else
            {
                applicationDb.AddSociety(societyNameTextBox.Text.ToString(), societyDescTextBox.Text.ToString());
                MessageBoxCustomManager.Show("Успешно", "Данные успешно добавлены");
                LoadSocietiesInfoBox(dataGrdiPopupLsit);
            }
        }

        private void textPlaceHolderLoginAndPassword(object sender, string message)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Тут будет находиться ваш логин!" || textBox.Text == "Тут будет находиться ваш пароль!")
            {
                textBox.Text = "";
            }
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = message;
            }
        }

        private void TextBoxPlaceholderLoginAndPassword_GotFocus(object sender, RoutedEventArgs e)
        {

            TextBox textBox = (TextBox)sender;
            textPlaceHolderLoginAndPassword(textBox, "");
        }

        private void TextBoxPlaceholderLoginAndPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textPlaceHolderLoginAndPassword(textBox, "Тут будет находиться ваш логин!");
        }

        private void TextBoxPlaceholderPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textPlaceHolderLoginAndPassword(textBox, "");
        }

        private void TextBoxPlaceholderPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textPlaceHolderLoginAndPassword(textBox, "Тут будет находиться ваш пароль!");
        }

        private void borderClickMainUserGenerate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Border border = (Border)sender;
            border.Width = 50;
            border.Height = 50;
            AccGenerateGrid.Visibility = Visibility.Visible;
            SocietyGenerateGrid.Visibility = Visibility.Collapsed;
            GridSelectInfo.Visibility = Visibility.Collapsed;
            GridSelectSociety.Visibility = Visibility.Collapsed;
            SolidColorBrush selectedBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#a972fe"));
            SolidColorBrush defaultBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#C098FC"));
            borderClickMainUserGenerate.Background = selectedBackground;
            borderClickDbSelect.Background = defaultBackground;
            borderClickSocietyGenerate.Background = defaultBackground;
            borderClickSocietySelect.Background = defaultBackground;
        }

        private void borderClickSocietyGenerate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Border border = (Border)sender;
            AccGenerateGrid.Visibility = Visibility.Collapsed;
            SocietyGenerateGrid.Visibility = Visibility.Visible;
            GridSelectInfo.Visibility = Visibility.Collapsed;
            GridSelectSociety.Visibility = Visibility.Collapsed;
            SolidColorBrush selectedBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#a972fe"));
            SolidColorBrush defaultBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#C098FC"));
            borderClickMainUserGenerate.Background = defaultBackground;
            borderClickSocietyGenerate.Background = selectedBackground;
            borderClickDbSelect.Background = defaultBackground;
            borderClickSocietySelect.Background = defaultBackground;
            border.Width = 50;
            border.Height = 50;
        }

        private void borderClickDbSelect_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Border border = (Border)sender;
            AccGenerateGrid.Visibility = Visibility.Collapsed;
            SocietyGenerateGrid.Visibility = Visibility.Collapsed;
            GridSelectInfo.Visibility = Visibility.Visible;
            GridSelectSociety.Visibility = Visibility.Collapsed;
            SolidColorBrush selectedBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#a972fe"));
            SolidColorBrush defaultBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#C098FC"));
            borderClickMainUserGenerate.Background = defaultBackground;
            borderClickSocietyGenerate.Background = defaultBackground;
            borderClickDbSelect.Background = selectedBackground;
            borderClickSocietySelect.Background = defaultBackground;
            border.Width = 50;
            border.Height = 50;
        }

        private void borderClickSocietySelect_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Border border = (Border)sender;
            AccGenerateGrid.Visibility = Visibility.Collapsed;
            SocietyGenerateGrid.Visibility = Visibility.Collapsed;
            GridSelectInfo.Visibility = Visibility.Collapsed;
            GridSelectSociety.Visibility = Visibility.Visible;
            LoadSocietiesInfoBox(dataGridSelectSociety);
            SolidColorBrush selectedBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#a972fe"));
            SolidColorBrush defaultBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#C098FC"));
            borderClickMainUserGenerate.Background = defaultBackground;
            borderClickSocietyGenerate.Background = defaultBackground;
            borderClickDbSelect.Background = defaultBackground;
            borderClickSocietySelect.Background = selectedBackground;
            border.Width = 50;
            border.Height = 50;
        }

        private void DataGridCopyLoginAndPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock text = (TextBlock)sender;
            string originalText = text.Text.ToString();
            Clipboard.SetText(originalText);
        }

        private void societyChange_MouseDown(object sender, MouseButtonEventArgs e)
        {
            popupChangeNameDescription.IsOpen = true;
        }

        private void popupCloseChangeSociety_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            popupChangeNameDescription.IsOpen = false;

        }

        private void BorderChangeSociety_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!string.IsNullOrEmpty(societyNewNameTextBox.Text) && societyNewNameTextBox.Text != "Введите соц-сеть")
            {
                LoadNameDescSocial selectedSociety = (LoadNameDescSocial)dataGridSelectSociety.SelectedItem;

                int societyId = selectedSociety.SocietyId;
                string newName = societyNewNameTextBox.Text;
                string newDescription = societyNewDescTextBox.Text;
                applicationDb.UpdateSociety(newName, newDescription, societyId);
                LoadSocietiesInfoBox(dataGridSelectSociety);

                List<UserInfo> users = applicationDb.SelectInfoUser();
                dataGridSelectInfo.ItemsSource = users;

                popupChangeNameDescription.IsOpen = false;

            }
            else
            {
                popupChangeNameDescription.IsOpen = false;
                MessageBoxCustomManager.Show("Ошибка", "Введите название для соцсети");
            }
        }
    }
}
