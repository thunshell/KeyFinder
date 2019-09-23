using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyFinder.Models
{
    public class Data:BindingBase
    {
        private int index;

        public int Index
        {
            get { return index; }
            set { base.SetProperty(ref index, value); }
        }


        private string value1;

        public string Value1
        {
            get { return value1; }
            set { base.SetProperty(ref value1, value); }
        }


        private string value2;

        public string Value2
        {
            get { return value2; }
            set { base.SetProperty(ref value2, value); }
        }

        private string value3;

        public string Value3
        {
            get { return value3; }
            set { base.SetProperty(ref value3, value); }
        }

        private string value4;

        public string Value4
        {
            get { return value4; }
            set { base.SetProperty(ref value4, value); }
        }


        public string AddTime { get; set; }

        public string UpdateTime { get; set; }
    }
}
