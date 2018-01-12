using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Collections;
using System.Web;
using System.Xml;
using System.Web.Mvc;

namespace DealMvc.Common
{

    /// <summary>
    /// SIteMap导航
    /// </summary>
    public class JSiteMap : Control
    {
        /// <summary>
        /// ViewContext
        /// </summary>
        public ViewContext _ViewContext { set; get; }
        private bool _isblank = false;
        /// <summary>
        /// 是否在新窗口打开链接
        /// </summary>
        public bool IsBlank { set { _isblank = value; } get { return _isblank; } }
        private string _nowtitle = "";
        /// <summary>
        /// 重写当前action的标题
        /// </summary>
        public string NowTitle { set { _nowtitle = NowTitle; } get { return _nowtitle; } }
        private string _sign = " > ";
        /// <summary>
        /// 分割符
        /// </summary>
        public string Sign { set { _sign = value; } get { return _sign; } }

        protected override void Render(HtmlTextWriter writer)
        {
            _nowtitle = _ViewContext.ViewData["___Now_SiteMap_Title"] == null ? "" : _ViewContext.ViewData["___Now_SiteMap_Title"].ToString();
            UrlHelper Url = new UrlHelper(_ViewContext.RequestContext);

            writer.WriteLine(string.Empty);
            string controller = _ViewContext.RouteData.Values["controller"].ToString();
            string action = _ViewContext.RouteData.Values["action"].ToString();

            HeaderBaseClass _HeaderBaseClass = TitltMap.GetNowHeaderBaseClass(controller, action);//当前对象
            if (_HeaderBaseClass == null) if (_HeaderBaseClass == null) { base.Render(writer); return; }
            List<HeaderBaseClass> ParentsAndMeList = _HeaderBaseClass.ParentsAndMeList(TitltMap.ReadTitleMapXMLToHeaderBaseClassList());//父级和当前对象list
            ParentsAndMeList = HeaderHelper.DealHeaderBaseClassList(ParentsAndMeList, _ViewContext);

            string Str = "<a " + (_isblank ? " target=\"_blank\" " : "") + " href=\"{0}\">{1}</a>";
            ArrayList output = new ArrayList();
            for (int i = 0; i < ParentsAndMeList.Count; i++)
            {
                HeaderBaseClass __HeaderBaseClass = ParentsAndMeList[i];
                if (i == ParentsAndMeList.Count - 1)
                    output.Add(string.IsNullOrEmpty(_nowtitle) ? __HeaderBaseClass.title : _nowtitle);
                else
                    output.Add(string.Format(Str, string.IsNullOrEmpty(__HeaderBaseClass.url) ? Url.Action(__HeaderBaseClass.a, __HeaderBaseClass.c) : __HeaderBaseClass.url, __HeaderBaseClass.title));
            }
            writer.Write(string.Join(_sign, (string[])output.ToArray(typeof(string))));
            base.Render(writer);
        }
    }

    /// <summary>
    /// 自动生成Title控件
    /// </summary>
    public class HeaderHelper : Control
    {

        

        /// <summary>
        /// ViewContext
        /// </summary>
        public ViewContext _ViewContext { set; get; }
        public static string BrowserTitleSeparator = WebCacheHelper.GetSiteInfo().WebDelimiter;
        public static string Key = "Title";
        public static string Key2 = "KeyWord";
        public static string Key3 = "Description";

        protected ArrayList C_Title = new ArrayList();

        protected override void Render(HtmlTextWriter writer)
        {
            writer.WriteLine(string.Empty);
            string controller = _ViewContext.RouteData.Values["controller"].ToString();
            string action = _ViewContext.RouteData.Values["action"].ToString();

            HeaderBaseClass _HeaderBaseClass = TitltMap.GetNowHeaderBaseClass(controller, action);//当前对象
            ArrayList _Title = new ArrayList();
            if (_HeaderBaseClass != null)
            {
                List<HeaderBaseClass> ParentsAndMeList = _HeaderBaseClass.ParentsAndMeList(TitltMap.ReadTitleMapXMLToHeaderBaseClassList());//父级和当前对象list
                ParentsAndMeList = DealHeaderBaseClassList(ParentsAndMeList, _ViewContext);
                _Title = GetTitle(ParentsAndMeList);
            }
            else
            {
                if (HttpContext.Current.Items[Key] != null)
                { _Title.InsertRange(0, (ArrayList)HttpContext.Current.Items[Key]); }
            }
            Model.SiteInfo m_site = WebCacheHelper.GetSiteInfo();
            ArrayList _KeyWord = (ArrayList)_Title.Clone();
            ArrayList _Description = (ArrayList)_Title.Clone();

            if (string.Join(BrowserTitleSeparator, (string[])_Title.ToArray(typeof(string))).IndexOf(m_site.WebTitle) < 0)
                _Title.Add(m_site.WebTitle);
            //_Title.Add(Common.Config.ConfigInfo<Common.Config.SiteInfo>.Instance().HomeUrl);
            writer.WriteLine("<title>{0}</title>", string.Join(BrowserTitleSeparator, (string[])_Title.ToArray(typeof(string))));

            if (HttpContext.Current.Items[Key2] != null)
            { _KeyWord.InsertRange(0, (ArrayList)HttpContext.Current.Items[Key2]); }
            _KeyWord.Add(m_site.WebKeyword);
            writer.WriteLine("<meta name=\"keywords\" content=\"{0}\" />", string.Join(BrowserTitleSeparator, (string[])_KeyWord.ToArray(typeof(string))));

            if (HttpContext.Current.Items[Key3] != null)
            { _Description.InsertRange(0, (ArrayList)HttpContext.Current.Items[Key3]); }
            writer.WriteLine("<meta name=\"description\" content=\"{0}\" />", string.Join(BrowserTitleSeparator, (string[])_Description.ToArray(typeof(string))));

            writer.WriteLine("<meta name=\"copyright\" content=\"{0}\" />", m_site.WebCopyright);

            writer.WriteLine("<meta name=\"author\" content=\"{0}\" />", "成都雷驰科技有限公司");

            base.Render(writer);


        }

        public static List<HeaderBaseClass> DealHeaderBaseClassList(List<HeaderBaseClass> L, ViewContext _ViewContext)
        {
            //修改title
            if (HttpContext.Current.Items[Key] != null)
            { L[L.Count - 1].title = string.Join(BrowserTitleSeparator, (string[])((ArrayList)HttpContext.Current.Items[Key]).ToArray(typeof(string))); }

            UrlHelper Url = new UrlHelper(_ViewContext.RequestContext);
            ViewDataDictionary _ViewDataDictionary = _ViewContext.ViewData;//当前页面附带参数
            foreach (HeaderBaseClass _HeaderBaseClass in L)
            {//赋值Url和Title
                string controllerName = _HeaderBaseClass.c;
                string actionName = _HeaderBaseClass.a;
                System.Web.Routing.RouteValueDictionary _RouteValueDictionary = new System.Web.Routing.RouteValueDictionary();

                foreach (string k in _ViewDataDictionary.Keys)
                {
                    string[] k_s = k.Split('_');
                    if (k_s.Length == 3)
                    {//附带参数
                        if (k_s[0].ToLower() == controllerName.ToLower() && k_s[1].ToLower() == actionName.ToLower())
                        { _RouteValueDictionary.Add(k_s[2], _ViewDataDictionary[k]); }
                    }
                    else if (k_s.Length == 2)
                    {
                        string[] n_s = k_s[1].Split('$');
                        if (n_s.Length == 2)
                        {
                            if (k_s[0].ToLower() == controllerName.ToLower() && n_s[0].ToLower() == actionName.ToLower() && n_s[1] == "name")//超链接文字
                            { _HeaderBaseClass.title = _ViewDataDictionary[k].ToString2(); }
                        }

                    }
                }
                string hrefUrl = Url.Action(actionName, controllerName, _RouteValueDictionary);
                _HeaderBaseClass.url = hrefUrl.Trim();
            }
            return L;
        }

        public static ArrayList GetTitle(List<HeaderBaseClass> L)
        {
            ArrayList l = new ArrayList();
            for (int i = L.Count - 1; i >= 0; i--)
            {
                string Title = L[i].title.ToString2();
                if (!string.IsNullOrEmpty(Title))
                    l.Add(Title);
            }
            return l;
        }

        /// <summary>
        /// 增加网站Title
        /// </summary>
        /// <param name="Tit"></param>
        public static void AddTitle(string Tit)
        {
            ArrayList _ArrayList = new ArrayList();
            if (HttpContext.Current.Items[Key] != null)
            { _ArrayList = (ArrayList)HttpContext.Current.Items[Key]; }
            _ArrayList.Insert(0, Tit);
            HttpContext.Current.Items[Key] = _ArrayList;
        }

        /// <summary>
        /// 增加网站KeyWord
        /// </summary>
        /// <param name="Tit"></param>
        public static void AddKeyWord(string KeyWord)
        {
            ArrayList _ArrayList = new ArrayList();
            if (HttpContext.Current.Items[Key2] != null)
            { _ArrayList = (ArrayList)HttpContext.Current.Items[Key2]; }
            _ArrayList.Insert(0, KeyWord);
            HttpContext.Current.Items[Key2] = _ArrayList;
        }

        /// <summary>
        /// 增加网站Description
        /// </summary>
        /// <param name="Tit"></param>
        public static void AddDescription(string Description)
        {
            ArrayList _ArrayList = new ArrayList();
            if (HttpContext.Current.Items[Key3] != null)
            { _ArrayList = (ArrayList)HttpContext.Current.Items[Key3]; }
            _ArrayList.Insert(0, Description);
            HttpContext.Current.Items[Key3] = _ArrayList;
        }

    }

    public class TitltMap
    {
        private static List<HeaderBaseClass> AllList = null;
        private static int? sign = null;
        public static List<HeaderBaseClass> ReadTitleMapXMLToHeaderBaseClassList()
        {
            if (AllList != null)
            { return AllList; }
            else
            {
                AllList = new List<HeaderBaseClass>();

                string XMLPath;
                if (HttpContext.Current != null)
                    XMLPath = HttpContext.Current.Server.MapPath("~/TitleMap.config");
                else
                    XMLPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TitleMap.config");

                XmlDocument doc = new XmlDocument();
                doc.Load(XMLPath);
                XmlElement _XmlElement = doc.DocumentElement;

                AllList.AddRange(ForeachXML("", _XmlElement));

                return AllList;
            }

        }
        /// <summary>
        /// 遍历XML
        /// </summary>
        /// <param name="_XmlElement"></param>
        /// <returns></returns>
        protected static List<HeaderBaseClass> ForeachXML(string TopID, XmlNode _XmlElement)
        {
            List<HeaderBaseClass> _L = new List<HeaderBaseClass>();
            XmlNodeList _XmlNodeList = _XmlElement.SelectNodes("item");
            foreach (XmlNode _XmlNode in _XmlNodeList)
            {
                string c = _XmlNode.Attributes["c"] == null ? "" : _XmlNode.Attributes["c"].Value.ToString();
                string a = _XmlNode.Attributes["a"] == null ? "" : _XmlNode.Attributes["a"].Value.ToString();
                string title = _XmlNode.Attributes["title"] == null ? "" : _XmlNode.Attributes["title"].Value.ToString();
                HeaderBaseClass _HeaderBaseClass = new HeaderBaseClass();
                _HeaderBaseClass.c = c;
                _HeaderBaseClass.a = a;
                _HeaderBaseClass.title = title;
                _HeaderBaseClass.ID = "Top" + sign.ToString2();
                _HeaderBaseClass.TopID = TopID;
                _L.Add(_HeaderBaseClass);
                sign = sign ?? 0;
                sign++;
                _L.AddRange(ForeachXML(_HeaderBaseClass.ID, _XmlNode));
            }
            return _L;
        }
        /// <summary>
        /// 获取当前的对象
        /// </summary>
        /// <param name="c"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static HeaderBaseClass GetNowHeaderBaseClass(string c, string a)
        {
            List<HeaderBaseClass> _AllList = ReadTitleMapXMLToHeaderBaseClassList();
            foreach (HeaderBaseClass _HeaderBaseClass in _AllList)
            {
                if (_HeaderBaseClass == null) continue;
                if (_HeaderBaseClass.a.ToLower() == a.ToLower() && _HeaderBaseClass.c.ToLower() == c.ToLower())
                    return (HeaderBaseClass)_HeaderBaseClass.Clone();
            }
            return null;
        }
    }

    public class HeaderBaseClass
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HeaderBaseClass() { }
        /// <summary>
        /// controller
        /// </summary>
        public string c { get; set; }
        /// <summary>
        /// action
        /// </summary>
        public string a { get; set; }
        /// <summary>
        /// title
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 链接地址
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 当前标识
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 父级标识
        /// </summary>
        public string TopID { get; set; }

        /// <summary>
        /// 父级对象到包括当前对象的list
        /// </summary>
        /// <returns></returns>
        public List<HeaderBaseClass> ParentsAndMeList(List<HeaderBaseClass> AllList)
        {
            List<HeaderBaseClass> result = P_HeaderBaseClass(AllList, this);
            result.Add(this);
            return result;
        }
        /// <summary>
        /// 获取父级对象
        /// </summary>
        /// <param name="AllList">总集合</param>
        /// <param name="_HeaderBaseClass">当前对象</param>
        /// <returns>返回所有的父级对象</returns>
        private List<HeaderBaseClass> P_HeaderBaseClass(List<HeaderBaseClass> AllList, HeaderBaseClass _HeaderBaseClass)
        {
            List<HeaderBaseClass> L = new List<HeaderBaseClass>();
            HeaderBaseClass parent = null;
            foreach (HeaderBaseClass HBC in AllList)
            {
                if (HBC == null) continue;
                if (HBC.ID == _HeaderBaseClass.TopID)
                { parent = HBC; break; }
            }
            if (parent == null)
            {
                return L;
                //throw new Exception("HeaderHelper Or [TitleMap.config]    Error !"); 
            }
            L.Insert(0, parent);
            if (parent.ID != "Top")
                L.InsertRange(0, P_HeaderBaseClass(AllList, parent));
            return L;
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <returns></returns>
        public HeaderBaseClass Clone()
        {
            HeaderBaseClass _HeaderBaseClass = new HeaderBaseClass();
            _HeaderBaseClass.c = c;
            _HeaderBaseClass.a = a;
            _HeaderBaseClass.title = title;
            _HeaderBaseClass.url = url;
            _HeaderBaseClass.ID = ID;
            _HeaderBaseClass.TopID = TopID;
            return _HeaderBaseClass;
        }

    }
}
