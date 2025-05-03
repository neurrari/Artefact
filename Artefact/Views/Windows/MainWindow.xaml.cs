using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using Artefact.Views.Pages;

namespace Artefact
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly UIElement _controlContentDefault;
        public MainWindow()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            MainFrame.Navigate(new WelcomePage());
        }

        private void logoutBtn_Click(object sender, RoutedEventArgs e)
        {
            Views.Login loginWindow = new Views.Login();
            loginWindow.Show();
            this.Close();
        }

        #region Control bar buttons
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

        private void maximizeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal) this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
        }

        private void minimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        #endregion

        private void MenuButton_Checked(object sender, RoutedEventArgs e)
        {
            if (mainPageRb.IsChecked == true) MainFrame.Navigate(new DashboardPage());
            else if (fundsPageRb.IsChecked == true) MainFrame.Navigate(new FundsPage());
            else if (tempStorePageRb.IsChecked == true) MainFrame.Navigate(new TempStorePage());
            else if (reportPageRb.IsChecked == true) MainFrame.Navigate(new ReportsPage());
            else if (roomsPageRb.IsChecked == true) MainFrame.Navigate(new RoomsPage());
            else if (helpPageRb.IsChecked == true) MainFrame.Navigate(new HelpPage());
            else if (usersPageRb.IsChecked == true) MainFrame.Navigate(new UsersPage());
        }

        private void MainFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            if (e.Content is WelcomePage)
            {
                closePageBtn.Visibility = Visibility.Collapsed;
                headerMainWindow.Text = "";
                UncheckAllMenuButtons();
            }
            else
            {
                closePageBtn.Visibility = Visibility.Visible;

                // Обновляем заголовок для текущей страницы
                if (e.Content is Page page && !string.IsNullOrEmpty(page.Title))
                {
                    headerMainWindow.Text = page.Title;
                }
            }
        }

        private void UncheckAllMenuButtons()
        {
            mainPageRb.IsChecked = false;
            fundsPageRb.IsChecked = false;
            tempStorePageRb.IsChecked = false;
            reportPageRb.IsChecked = false;
            roomsPageRb.IsChecked = false;
            helpPageRb.IsChecked = false;
            usersPageRb.IsChecked = false;
        }

        private void closePageBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new WelcomePage());
        }
    }
}
