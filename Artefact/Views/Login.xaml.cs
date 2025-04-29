using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Artefact.Views
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void loginBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tbLogin.Focus();
        }

        private void tbLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbLogin.Text) && tbLogin.Text.Length > 0)
            {
                loginBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                loginBox.Visibility = Visibility.Visible;
            }
        }

        private void passwordBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tbPassword.Focus();
        }

        private void tbPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbPassword.Password) && tbPassword.Password.Length > 0)
            {
                passwordBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                passwordBox.Visibility = Visibility.Visible;
            }
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbLogin.Text) && !string.IsNullOrEmpty(tbPassword.Password))
            {
                MessageBox.Show("Success!");
            }
        }
    }
}
