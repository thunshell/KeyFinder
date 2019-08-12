using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace KeyFinder
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            string s = Utilities.CryptHelper.Encrypt("keyfinder");
            Console.WriteLine(s);
            string s1 = Utilities.CryptHelper.Decrypt(s);
            Console.WriteLine(s1);
        }
    }
}
