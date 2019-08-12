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
    /// DataItemWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DataItemWindow : Window
    {
        public DataItemWindow()
        {
            Owner = App.Current.MainWindow;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            InitializeComponent();
        }

        void btnSaveClick(object sendre, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
