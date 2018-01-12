using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web;
using System.Xml;
namespace DealMvc.Common
{
    /// <summary>
    /// 国家, 省/州, 市
    /// </summary>
    public class WordCountry
    {
        /// <summary>
        /// 静态国家list
        /// </summary>
        public static List<Country> CountryList = new List<Country>();

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static WordCountry()
        {
            if (CountryList.Count > 0) return;

            string XMLPath;
            if (HttpContext.Current != null)
            {
                XMLPath = HttpContext.Current.Server.MapPath("~/WordCountry.xml");
            }
            else
            {
                XMLPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WordCountry.xml");
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(XMLPath);
            XmlElement _XmlElement = doc.DocumentElement;

            foreach (XmlNode _XmlNode in _XmlElement.SelectNodes("CountryRegion"))
            {
                string Name = ""; string Code = "";
                try
                {
                    Name = _XmlNode.Attributes["Name"].Value == null ? "" : _XmlNode.Attributes["Name"].Value;
                    Code = _XmlNode.Attributes["Code"].Value == null ? "" : _XmlNode.Attributes["Code"].Value;
                }
                catch { }
                Country _Country = new Country(Name, Code);
                foreach (XmlNode _XmlNode2 in _XmlNode.SelectNodes("State"))
                {
                    string Name2 = "-"; string Code2 = "-";
                    try
                    {
                        Name2 = _XmlNode2.Attributes["Name"].Value == null ? "" : _XmlNode2.Attributes["Name"].Value;
                        Code2 = _XmlNode2.Attributes["Code"].Value == null ? "" : _XmlNode2.Attributes["Code"].Value;
                    }
                    catch { }
                    State _State = new State(Name2, Code2);
                    foreach (XmlNode _XmlNode3 in _XmlNode2.SelectNodes("City"))
                    {
                        string Name3 = ""; string Code3 = "";
                        try
                        {
                            Name3 = _XmlNode3.Attributes["Name"].Value == null ? "" : _XmlNode3.Attributes["Name"].Value;
                            Code3 = _XmlNode3.Attributes["Code"].Value == null ? "" : _XmlNode3.Attributes["Code"].Value;
                        }
                        catch { }
                        City _City = new City(Name3, Code3);
                        if (_State.CityList == null) _State.CityList = new List<City>();
                        _State.CityList.Add(_City);
                    }
                    if (_Country.StateList == null) _Country.StateList = new List<State>();
                    _Country.StateList.Add(_State);
                }
                CountryList.Add(_Country);
            }
        }

        /// <summary>
        /// 返回国家的list
        /// </summary>
        /// <returns></returns>
        public static List<Country> getCountryList()
        {
            //IEnumerable<Country> __C = CountryList.Where(Country => { if (Country.Name == "中国") { return true; } else { return false; } });

            //foreach (Country _Country in __C)
            //{
            //    string name = _Country.Name;
            //}
            return CountryList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static StringBuilder getCountryOptionList()
        {
            List<Common.Country> _CountryList = getCountryList();
            string temp = "<option value='{0}'>{1}</option>";
            StringBuilder output = new StringBuilder();
            foreach (Common.Country _Country in _CountryList)
            {
                output.AppendFormat(temp, _Country.Name, _Country.Name);
            }
            return output;
        }
        public static StringBuilder getStateOptionList(string CountryName)
        {
            StringBuilder output = new StringBuilder();
            List<Common.Country> _CountryList = getCountryList();
            IEnumerable<Country> __C = _CountryList.Where(Country => { if (Country.Name == CountryName) { return true; } else { return false; } });
            foreach (Country _Country in __C)
            {
                List<Common.State> _StateList = _Country.StateList;
                string temp = "<option value='{0}'>{1}</option>";

                foreach (Common.State _State in _StateList)
                {
                    output.AppendFormat(temp, _State.Name, _State.Name);
                }
                break;
            }
            return output;
        }
        public static StringBuilder getCityOptionList(string CountryName, string StateName)
        {
            StringBuilder output = new StringBuilder();
            List<Common.Country> _CountryList = getCountryList();
            IEnumerable<Country> __C = _CountryList.Where(Country => { if (Country.Name == CountryName) { return true; } else { return false; } });
            foreach (Country _Country in __C)
            {
                List<Common.State> _StateList = _Country.StateList;
                IEnumerable<State> __CC = _StateList.Where(State => { if (State.Name == StateName) { return true; } else { return false; } });
                foreach (State _State in __CC)
                {
                    List<Common.City> _CityList = _State.CityList;
                    string temp = "<option value='{0}'>{1}</option>";

                    foreach (Common.City _City in _CityList)
                    {
                        output.AppendFormat(temp, _City.Name, _City.Name);
                    }
                    break;
                }
                break;
            }
            return output;
        }
    }

    /// <summary>
    /// 国家
    /// </summary>
    public class Country
    {
        public Country(string name, string code)
        {
            Name = name; Code = code;
        }
        /// <summary>
        /// 名陈
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 属于国家的所有省/州
        /// </summary>
        public List<State> StateList { get; set; }
    }
    /// <summary>
    /// 省/州
    /// </summary>
    public class State
    {
        public State(string name, string code)
        {
            Name = name; Code = code;
        }
        /// <summary>
        /// 名陈
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 属于省/州的所有市
        /// </summary>
        public List<City> CityList { get; set; }
    }
    /// <summary>
    /// 市
    /// </summary>
    public class City
    {
        public City(string name, string code)
        {
            Name = name; Code = code;
        }
        /// <summary>
        /// 名陈
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; }
    }
}
