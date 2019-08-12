using KeyFinder.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KeyFinder.ViewModel
{
    public class MainViewModel: BaseViewModel
    {
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "KeyFinder.info");
        List<Data> _list;
        public ObservableCollection<Data> DataCollection { get; set; }

        public SimpleCommand RefreshCommand { get; set; }

        public SimpleCommand FindCommand { get; set; }

        public SimpleCommand AddCommand { get; set; }

        public SimpleCommand EditCommand { get; set; }

        public SimpleCommand DeleteCommand { get; set; }

        public MainViewModel()
        {
            DataCollection = new ObservableCollection<Data>();
            RefreshCommand = new SimpleCommand(ExecuteRefresh);
            FindCommand = new SimpleCommand(ExecuteFind);
            AddCommand = new SimpleCommand(ExecuteAdd);
            EditCommand = new SimpleCommand(ExecuteEdit);
            DeleteCommand = new SimpleCommand(ExecuteDelete);

            Load();
        }

        private void ExecuteDelete(object obj)
        {
            try
            {
                if (obj == null) return;
                Data d = (Data)obj;
                if(MessageBox.Show("删除后将无法恢复，确定要删除吗？", "提问", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    this._list.Remove(d);
                    this.DataCollection.Remove(d);
                    Save(this._list);
                    Load();
                }
            }
            catch (Exception)
            {
            }
        }

        private void ExecuteEdit(object obj)
        {
            try
            {
                if (obj == null) return;
                Data s = (Data)obj;
                Data d = new Data
                {
                    Index = s.Index,
                    Value1 = s.Value1,
                    Value2 = s.Value2,
                    Value3 = s.Value3,
                    AddTime = s.AddTime,
                    UpdateTime = s.UpdateTime
                };
                DataItemWindow window = new DataItemWindow { DataContext = d };
                if (window.ShowDialog() == true)
                {
                    if (string.IsNullOrWhiteSpace(d.Value1))
                        return;
                    s.UpdateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    s.Value1 = d.Value1;
                    s.Value2 = d.Value2;
                    s.Value3 = d.Value3;
                    Save(this._list);
                }
            }
            catch (Exception)
            {
            }
        }

        private void ExecuteAdd(object obj)
        {
            try
            {
                Data d = new Data();
                DataItemWindow window = new DataItemWindow { DataContext = d };
                if(window.ShowDialog() == true)
                {
                    if (string.IsNullOrWhiteSpace(d.Value1))
                        return;
                    d.AddTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    d.UpdateTime = d.AddTime;
                    if (this._list == null)
                        this._list = new List<Data>();
                    this._list.Add(d);
                    DataCollection.Insert(0, d);
                    Save(this._list);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void ExecuteFind(object obj)
        {
            try
            {
                string k = obj?.ToString();
                if (string.IsNullOrWhiteSpace(k) || this._list == null || this._list.Count <= 0) return;
                ReSetDataCollection(this._list.Where(d => d.Value1.Contains(k)));
            }
            catch (Exception)
            {
            }
        }

        private void ExecuteRefresh(object obj)
        {
            try
            {
                Load();
            }
            catch (Exception)
            {
            }
        }

        private void Load()
        {
            if (File.Exists(path))
            {
                FileInfo fileInfo = new FileInfo(path);
                string data = "";
                using (StreamReader sr = fileInfo.OpenText())
                {
                    data = fileInfo.OpenText().ReadToEnd();
                }
                string xml = Utilities.CryptHelper.Decrypt(data);
                this._list = Utilities.XmlHelper.DeXMLSerialize<List<Data>>(xml);
                //ReSetDataCollection(this._list);
            }
        }

        void ReSetDataCollection( IEnumerable<Data> list)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                DataCollection.Clear();
                DataCollection.AddRange(list);
            });
        }

        void Save(List<Data> list)
        {
            string str = Utilities.XmlHelper.XMLSerialize(list);
            string encript = Utilities.CryptHelper.Encrypt(str);
            File.WriteAllText(path, encript);
        }
    }
}
