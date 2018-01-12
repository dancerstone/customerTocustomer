using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DealMvc.Common.Config
{
    /// <summary>
    /// 网站的上传配置类
    /// </summary>
    public class UploadInfo : IConfigInfo
    { 
        public string UploadFolder { get; set; }
        public string UploadExtension { get; set; }
        public string UploadSize { get; set; }
    }
}