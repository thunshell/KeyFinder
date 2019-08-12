using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Security.Cryptography;
using System.Xml;
using System.Xml.Serialization;

namespace Utilities
{
    /// <summary>
    /// Xml操作相關方法
    /// </summary>
    public class XmlHelper
    {
        #region 創建實例
        private XmlHelper()
        { }
        private static XmlHelper instance;
        private static object lockObject = new object();
        /// <summary>
        /// 實例
        /// </summary>
        public static XmlHelper Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        lock (lockObject)
                        {
                            instance = new XmlHelper();
                        }
                    }
                }

                return instance;
            }
        }
        #endregion

        #region 獲取節點值
        /// <summary>
        /// 獲取節點值
        /// </summary>
        /// <param name="xe">節點</param>
        /// <returns></returns>
        public string XElementToString(XElement xe)
        {
            string val = string.Empty;

            if (xe != null && xe.Value.Trim().Length > 0)
            {
                val = xe.Value.Trim();
            }

            return val;
        }

        /// <summary>
        /// 獲取節點值
        /// </summary>
        /// <param name="xe">節點</param>
        /// <returns></returns>
        public bool XElementToBool(XElement xe)
        {
            bool val = false;

            if (xe != null && xe.Value.Trim().Length > 0)
            {
                val = xe.Value.Trim().Equals("1") ? true : false;
            }

            return val;
        }

        /// <summary>
        /// 獲取節點值
        /// </summary>
        /// <param name="xe">節點</param>
        /// <returns></returns>
        public int XElementToInt(XElement xe)
        {
            int val = 0;

            if (xe != null && xe.Value.Trim().Length > 0)
            {
                val = int.Parse(xe.Value.Trim());
            }

            return val;
        }

        /// <summary>
        /// 獲取節點值
        /// </summary>
        /// <param name="xe">節點</param>
        /// <returns></returns>
        public long XElementToLong(XElement xe)
        {
            long val = 0;

            if (xe != null && xe.Value.Trim().Length > 0)
            {
                val = long.Parse(xe.Value.Trim());
            }

            return val;
        }

        /// <summary>
        /// 獲取節點值
        /// </summary>
        /// <param name="xe">節點</param>
        /// <returns></returns>
        public double XElementToDouble(XElement xe)
        {
            double val = 0.0;

            if (xe != null && xe.Value.Trim().Length > 0)
            {
                val = double.Parse(xe.Value.Trim());
            }

            return val;
        }

        /// <summary>
        /// 獲取節點值
        /// </summary>
        /// <param name="xe">節點</param>
        /// <returns></returns>
        public double? XElementToNullDouble(XElement xe)
        {
            double? val = 0.0;

            if (xe != null && xe.Value.Trim().Length > 0)
            {
                val = double.Parse(xe.Value.Trim());
            }
            else
            {
                val = null;
            }

            return val;
        }
        #endregion

        /// <summary>
        /// 加载本地XML文件
        /// </summary>
        /// <param name="str_Path"></param>
        /// <returns></returns>
        public XDocument Load(string str_Path)
        {
            int faildTimes = 0;
            if (File.Exists(str_Path))
                while (true)
                {
                    try
                    {
                        return XDocument.Load(str_Path);
                    }
                    catch (Exception ex)
                    {
                        if (faildTimes++ >= 4) throw ex;
                        System.Threading.Thread.Sleep(50);
                    }
                }
            else
                return null;
        }

        /// <summary>
        /// 獲取服務器返回操作成功與否
        /// </summary> 
        /// <returns></returns>
        public bool GetWebReturnValue(Stream stream)
        {
            bool bl_OK = false;

            XDocument xDoc = XDocument.Load(stream);

            bl_OK = new XmlHelper().XElementToBool(xDoc.Root.Element("value"));

            return bl_OK;
        }

        /// <summary>
        /// 獲取服務器返回操作成功與否
        /// </summary>
        /// <returns></returns>
        public int GetWebReturnIntValue(Stream stream)
        {
            int int_Value = -1;

            XDocument xDoc = XDocument.Load(stream);

            int_Value = new XmlHelper().XElementToInt(xDoc.Root.Element("value"));

            return int_Value;
        }

        /// <summary>
        /// 獲取服務器返回操作成功與否
        /// </summary>
        /// <returns></returns>
        public object[] GetLoginReturnValue(Stream stream)
        {
            object[] obj_Info = new object[2];
            int int_Value = -1;

            XDocument xDoc = XDocument.Load(stream);

            int_Value = new XmlHelper().XElementToInt(xDoc.Root.Element("value"));

            obj_Info[0] = int_Value;
            obj_Info[1] = new XmlHelper().XElementToString(xDoc.Root.Element("customerid"));

            return obj_Info;
        }

        /// <summary>
        /// 獲取服務器返回操作成功與否
        /// </summary>
        /// <returns></returns>
        public bool GetWebReturnValue(XDocument xDoc)
        {
            bool bl_OK = false;

            bl_OK = new XmlHelper().XElementToBool(xDoc.Root.Element("value"));

            return bl_OK;
        }

        #region 創建Xml節點
        /// <summary>
        /// 創建Xml節點
        /// </summary>
        /// <param name="xeName">節點名</param>
        /// <param name="xeValue">節點值</param>
        /// <param name="xeParent">父節點</param>
        public void CreateXElement(string xeName, string xeValue, XElement xeParent)
        {
            if (xeValue == null)
                xeValue = "";

            XElement xe = new XElement(xeName);
            xe.Value = xeValue;

            xeParent.Add(xe);
        }

        /// <summary>
        /// 創建Xml節點
        /// </summary>
        /// <param name="xeName">節點名</param>
        /// <param name="xeParent">父節點</param>
        public XElement CreateXElement(string xeName, XElement xeParent)
        {
            XElement xe = new XElement(xeName);
            xeParent.Add(xe);

            return xe;
        }
        #endregion

        #region XML序列化
        public static void SaveToXml<T>(string filePath, T sourceObj, string xmlRootName)
        {
            if (!string.IsNullOrWhiteSpace(filePath) && sourceObj != null)
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    XmlSerializer xmlSerializer = string.IsNullOrWhiteSpace(xmlRootName) ?
                        new XmlSerializer(typeof(T)) :
                        new XmlSerializer(typeof(T), new XmlRootAttribute(xmlRootName));
                    xmlSerializer.Serialize(writer, sourceObj);
                }
            }
        }

        public static T LoadFromXml<T>(string filePath)
        {
            object result = null;

            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    result = xmlSerializer.Deserialize(reader);
                }
            }

            return (T)result;
        }

        public string ObjectToXml<T>(T source)
        {
            if (source == null) return null;
            XmlSerializerNamespaces xsn = new XmlSerializerNamespaces();
            xsn.Add(string.Empty, string.Empty);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (StringWriter sw = new StringWriter())
            {
                xmlSerializer.Serialize(sw, source, xsn);
                return sw.ToString();
            }
        }

        public static string XMLSerialize<T>(T entity, Encoding code = null)
        {
            StringBuilder buffer = new StringBuilder();
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            code = code ?? Encoding.UTF8;
            using (MemoryStream ms = new MemoryStream())
            {
                using (StreamWriter sw = new StreamWriter(ms, code))
                {
                    serializer.Serialize(sw, entity);
                    byte[] bytes = ms.ToArray();
                    return code.GetString(bytes, 0, bytes.Length);
                }
            }
        }

        public static T DeXMLSerialize<T>(string xmlString, Encoding code = null)
        {
            T cloneObject = default(T);
            code = code ?? Encoding.UTF8;

            using (MemoryStream ms = new MemoryStream(code.GetBytes(xmlString)))
            {
                ms.Position = 0;
                using (StreamReader reader = new StreamReader(ms))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    Object obj = serializer.Deserialize(reader);
                    cloneObject = (T)obj;
                }
            }
            return cloneObject;
        }
        #endregion
    }
}
