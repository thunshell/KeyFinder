using System.Windows;
using Utilities;

namespace KeyFinder
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            this.password.Focus();
        }

        void LoginClick(object sender, RoutedEventArgs e)
        {
            string p = this.password.Password;
            if (string.IsNullOrWhiteSpace(p))
                return;
            if (CryptHelper.Encrypt(p) == "312351333348315291330321303363306315330300303342")
            {
                MainWindow mainWindow = new MainWindow();
                App.Current.MainWindow = mainWindow;
                this.Close();
                mainWindow.Show();
            }
        }

         void CloseClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
