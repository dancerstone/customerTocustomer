using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DealMvc.API.Sina
{
    /// <summary>
    /// 得到Sina Config
    /// </summary>
    public class SinaConfig
    {
        /// <summary>
        /// 得到Sina的AppKey
        /// </summary>
        /// <returns>string AppKey</returns>
        public static string GetAppKey
        {
            get { return ""; }//["AppKey"]; }
        }

        /// <summary>
        /// 得到Sina的AppSecret
        /// </summary>
        /// <returns>string AppSecret</returns>
        public static string GetAppSecret
        {
            get { return ""; }//["AppSecret"]; }
        }

        /// <summary>
        /// 得到回调地址
        /// </summary>
        /// <returns></returns>
        public static string GetCallBackURI
        {
            get { return ""; }//["CallBackURI"]; }
        }
    }
}
