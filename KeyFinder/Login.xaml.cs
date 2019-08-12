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
            if (Utilities.CryptHelper.Encrypt(p) == "312351333348315291330321303363306315330300303342")
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
