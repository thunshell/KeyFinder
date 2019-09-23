using System.Windows;

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
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            InitializeComponent();
        }

        void btnSaveClick(object sendre, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
