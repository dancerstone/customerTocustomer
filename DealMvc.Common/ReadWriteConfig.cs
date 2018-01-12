using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace DealMvc.Common
{
    /// <summary>
    /// 操作web.config文件
    ///       修改例子
    ///       ReadWriteConfig config = new ReadWriteConfig();
    ///       config.ConfigType = (int)ConfigFileType.WebConfig;
    ///       config.SingleNode = "//QQSectionGroup//QzoneSection";
    ///       config.SetValue("abcd", "123123");
    /// </summary>
    public class ReadWriteConfig
    {
        public string docName = String.Empty;
        private XmlNode node = null;
        private int _configType;
        public int ConfigType
        {
            get { return _configType; }
            set { _configType = value; }
        }
        private string _singlenode = "//appSettings"; // //configSections//sectionGroup
        public string SingleNode
        {
            get { return _singlenode; }
            set { _singlenode = value; }
        }

        #region SetValue
        public bool SetValue(string key, string value)
        {
            XmlDocument cfgDoc = new XmlDocument();
            loadConfigDoc(cfgDoc);
            // retrieve the appSettings node   
            node = cfgDoc.SelectSingleNode(_singlenode);
            if (node == null)
            {
                throw new InvalidOperationException("web.config " + _singlenode + " section not found");
            }
            try
            {
                // XPath select setting "add" element that contains this key       
                XmlElement addElem = (XmlElement)node.SelectSingleNode("//add[@key='" + key + "']");
                if (addElem != null)
                {
                    addElem.SetAttribute("value", value);
                }
                // not found, so we need to add the element, key and value   
                else
                {
                    XmlElement entry = cfgDoc.CreateElement("add");
                    entry.SetAttribute("key", key);
                    entry.SetAttribute("value", value);
                    node.AppendChild(entry);
                }
                //save it   
                saveConfigDoc(cfgDoc, docName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region saveConfigDoc
        private void saveConfigDoc(XmlDocument cfgDoc, string cfgDocPath)
        {
            try
            {
                XmlTextWriter writer = new XmlTextWriter(cfgDocPath, null);
                writer.Formatting = Formatting.Indented;
                cfgDoc.WriteTo(writer);
                writer.Flush();
                writer.Close();
                return;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region removeElement
        public bool removeElement(string elementKey)
        {
            try
            {
                XmlDocument cfgDoc = new XmlDocument();
                loadConfigDoc(cfgDoc);
                // retrieve the appSettings node  
                node = cfgDoc.SelectSingleNode(_singlenode);
                if (node == null)
                {
                    throw new InvalidOperationException("web.config " + _singlenode + " section not found");
                }
                // XPath select setting "add" element that contains this key to remove      
                node.RemoveChild(node.SelectSingleNode("//add[@key='" + elementKey + "']"));
                saveConfigDoc(cfgDoc, docName);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region modifyElement
        public bool modifyElement(string elementKey)
        {
            try
            {
                XmlDocument cfgDoc = new XmlDocument();
                loadConfigDoc(cfgDoc);
                // retrieve the appSettings node  
                node = cfgDoc.SelectSingleNode(_singlenode);
                if (node == null)
                {
                    throw new InvalidOperationException("web.config " + _singlenode + " section not found");
                }
                // XPath select setting "add" element that contains this key to remove      
                node.RemoveChild(node.SelectSingleNode("//add[@key='" + elementKey + "']"));
                saveConfigDoc(cfgDoc, docName);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region loadConfigDoc
        private XmlDocument loadConfigDoc(XmlDocument cfgDoc)
        {
            // load the config file   
            if (false)//这里只用于网站的配置文件修改 (Convert.ToInt32(ConfigType) == Convert.ToInt32(ConfigFileType.AppConfig))
            {
                //docName = ((Assembly.GetEntryAssembly()).GetName()).Name;
                //docName += ".exe.config";
            }
            else
            {
                docName = System.Web.HttpContext.Current.Server.MapPath("web.config");
            }
            cfgDoc.Load(docName);
            return cfgDoc;
        }
        #endregion
    }
}