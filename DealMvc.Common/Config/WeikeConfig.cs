using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Web.Caching;
using System.Web;
using System.IO;
using System.Collections;

namespace DealMvc.Common.Config
{
    public class WeikeConfig
    {
        private XmlDocument xmlDoc = null;
        public string configFullFilePath;
        private string defaultLanguage = "zh-CN";
        public static string key = "WeikeConfig";

        public WeikeConfig(string configFilePath)
        {
            this.configFullFilePath = configFilePath;
            this.xmlDoc = new XmlDocument();
            this.xmlDoc.Load(this.configFullFilePath);
            this.Initialize(this.xmlDoc.DocumentElement);
        }

        public XmlNode GetConfigSection(string nodePath)
        {
            return this.xmlDoc.SelectSingleNode(nodePath);
        }

        private void Initialize(XmlNode node)
        {
            XmlAttributeCollection attributes = node.Attributes;
            XmlAttribute attribute = attributes["defaultLanguage"];
            if (attribute == null)
            {
                this.defaultLanguage = "zh-CN";
            }
            else
            {
                this.defaultLanguage = attribute.Value;
            }
        }

        public static WeikeConfig Instance()
        {
            WeikeConfig _instance = HttpRuntime.Cache.Get(key) as WeikeConfig;

            if (_instance == null)
            {
                string str2;
                if (HttpContext.Current != null)
                {
                    str2 = HttpContext.Current.Server.MapPath("~/Weike.Config");
                }
                else
                {
                    str2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Weike.Config");
                }
                _instance = new WeikeConfig(str2);
                CacheDependency dep = new CacheDependency(str2);
                HttpRuntime.Cache.Add(key, _instance, dep, DateTime.Now.AddYears(1), TimeSpan.Zero, CacheItemPriority.High, null);
            }
            return _instance;
        }

        public void Node(IConfigInfo _instance, string nodeName)
        {
            nodeName = "Weike/" + nodeName;
            WeikeConfig config = WeikeConfig.Instance();
            XmlNode configSection = config.GetConfigSection(nodeName);

            if (configSection != null)
            {
                IEnumerator enumerator = configSection.ChildNodes.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    XmlNode node_current = enumerator.Current as XmlNode;
                    if (node_current.NodeType == XmlNodeType.Comment) continue;
                    Type t = _instance.GetType();
                    System.Reflection.PropertyInfo p = t.GetProperty(node_current.Attributes["key"].Value);
                    if (p != null)
                        p.SetValue(_instance, Convert.ChangeType(node_current.Attributes["value"].Value, p.PropertyType), null);
                }
            }
        }

        public void Save(IConfigInfo _instance, string nodeName)
        {
            nodeName = "Weike/" + nodeName;

            if (this.xmlDoc != null)
            {
                string str2;
                if (HttpContext.Current != null)
                {
                    str2 = HttpContext.Current.Server.MapPath("~/Weike.Config");
                }
                else
                {
                    str2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Weike.Config");
                }

                WeikeConfig config = WeikeConfig.Instance();
                XmlNode configSection = config.GetConfigSection(nodeName);

                if (configSection != null)
                {
                    IEnumerator enumerator = configSection.ChildNodes.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        XmlNode node_current = enumerator.Current as XmlNode;
                        if (node_current.NodeType == XmlNodeType.Comment) continue;
                        Type t = _instance.GetType();
                        System.Reflection.PropertyInfo p = t.GetProperty(node_current.Attributes["key"].Value);
                        if (p != null)
                            node_current.Attributes["value"].Value = p.GetValue(_instance, null).ToString();
                    }
                }

                this.xmlDoc.Save(str2);

                HttpRuntime.Cache.Remove(key);
            }
        }
    }
}
