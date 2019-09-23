using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities
{
    public class CryptHelper
    {
        static string Key = "huotian";

        public static string Encrypt(string str)
        {
            if (string.IsNullOrEmpty(str)) return str;

            StringBuilder r = new StringBuilder();
            string s = Key + str;
            byte[] s1 = Encoding.UTF8.GetBytes(s);
            for (int i = 0; i < s1.Length; i++)
            {
                r.Append((s1[i] * 3).ToString().PadLeft(3, '0'));
                //r.AppendFormat("{0}%", s1[i]);
            }
            return r.ToString();
        }

        public static string Decrypt(string str)
        {
            if (string.IsNullOrEmpty(str)) return str;
            
            List<byte> bs = new List<byte>();
            //string[] ss = str.Split('%');
            //for (int i = 0; i < ss.Length - 1; i++)
            //{
            //    bs.Add(Convert.ToByte(ss[i]));
            //}
            for (int i = 0; i < str.Length; i +=3)
            {
                int b = Convert.ToInt32(str.Substring(i, 3));
                bs.Add(Convert.ToByte(b / 3));
            }
            byte[] s1 = bs.ToArray();
            string s = Encoding.UTF8.GetString(s1);
            string r = s.Substring(Key.Length);
            return r;
        }
    }
}
