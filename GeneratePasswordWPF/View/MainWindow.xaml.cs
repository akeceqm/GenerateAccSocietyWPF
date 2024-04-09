using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GeneratePasswordWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void FullScreenState(Border border)
        {
            if (WindowState == WindowState.Maximized)
            {
                border.CornerRadius = new CornerRadius(20);
                WindowState = WindowState.Normal;
            }
            else
            {
                border.CornerRadius = new CornerRadius(0);
                WindowState = WindowState.Maximized;
            }
        }

        private void borderClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void borderInFullScreen_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Border border = (Border)sender;
            FullScreenState(border);
        }

        private void borderHide_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void borderMoveScreen_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.ClickCount == 2)
            {
                Border border = (Border)sender;
                FullScreenState(border);
            }
            else
            {
                DragMove();
            }
        }

        public void mouseEnterAndLeaveBorderGrid(Border border, string backgroundColor, Size size)
        {
            Color colorBack = (Color)ColorConverter.ConvertFromString(backgroundColor);
            border.Background = new SolidColorBrush(colorBack);
            border.Width = size.Width;
            border.Height = size.Height;
        }

        private void borderClickMainPasswordGenerate_MouseEnter(object sender, MouseEventArgs e)
        {
            Border border = (Border)sender;
            mouseEnterAndLeaveBorderGrid(border, "#A972FE", new Size(50, 50));
        }

        private void borderClickMainPasswordGenerate_MouseLeave(object sender, MouseEventArgs e)
        {
            Border border = (Border)sender;
            mouseEnterAndLeaveBorderGrid(border, "#00000000", new Size(40, 40));
        }

        private void borderClickPasswordDbList_MouseEnter(object sender, MouseEventArgs e)
        {
            Border border = (Border)sender;
            mouseEnterAndLeaveBorderGrid(border, "#A972FE", new Size(50, 50));
        }

        private void borderClickPasswordDbList_MouseLeave(object sender, MouseEventArgs e)
        {
            Border border = (Border)sender;
            mouseEnterAndLeaveBorderGrid(border, "#00000000", new Size(40, 40));
        }
    }
}
