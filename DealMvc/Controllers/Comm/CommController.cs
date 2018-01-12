using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using System.Text;
using System.IO;
using DealMvc.Common;
using System.Net;
using System.Globalization;
using System.Text.RegularExpressions;

namespace DealMvc.Controllers.Comm
{
    /// <summary>
    /// 
    /// </summary>
    public class CommController : ControllerBase
    {
        #region 前台 获取城市联动

        /// <summary>
        /// 获取城市信息
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="Level"></param>
        /// <returns></returns> 
        public ContentResult GetSiteCityList(string Value, int? Level, int? v_id)
        {
            StringBuilder sb = new StringBuilder();
            //Model.SiteCity m_city = Model.SiteCity.GetModel(v_id ?? 0);
            //List<Model.SiteCity> city_list = new List<Model.SiteCity>();
            //if (!m_city.IsNull)
            //{
            //    city_list = Model.SiteCity.GetModelList(t => t.CitysTopID == m_city.id && t.C_Level == Level).List;
            //}
            //sb.Append("<option value=''>请选择</option>");
            //foreach (var item in city_list)
            //{
            //    sb.AppendFormat("<option value='{0}' v_id={2}>{1}</option>", item.C_Title, item.C_Title, item.id);
            //}
            return Content(sb.ToString2());
        }

        #endregion

        #region 获取城市下级分类
        /// <summary>
        /// 获取城市下级分类
        /// </summary>
        /// <param name="topid"></param>
        /// <returns></returns>
        public ContentResult GetNextCitys(int? topid, int? level, int? cate, string gc_cate)
        {
            StringBuilder sb = new StringBuilder();
            switch (cate)
            {
                case 1://城市下级分类
                    //Model.SiteCity m_city = Model.SiteCity.GetModel(topid);
                    //List<Model.SiteCity> NextCitys = new List<Model.SiteCity>();
                    //if (!m_city.IsNull)
                    //{
                    //    NextCitys = Model.SiteCity.GetModelList(t => t.CitysTopID == m_city.id && t.C_Level == level).List;
                    //}
                    //sb.AppendLine("<option value=''>请选择</option>");
                    //foreach (Model.SiteCity item in NextCitys)
                    //{
                    //    sb.AppendFormat("<option value='{0}'>{1}</option>", item.id, item.C_Title);
                    //}
                    break;
                case 2:
                    //Model.GoodsCate m_GoodsCate = Model.GoodsCate.GetModel(topid);
                    //List<Model.GoodsCate> NextGoodsCates = new List<Model.GoodsCate>();
                    //if (!m_GoodsCate.IsNull)
                    //{
                    //    NextGoodsCates = string.IsNullOrEmpty(gc_cate) ? Model.GoodsCate.GetModelList(t => t.GoodsCateTopID == m_GoodsCate.id && t.GC_Level == level).List : Model.GoodsCate.GetModelList(t => t.GoodsCateTopID == m_GoodsCate.id && t.GC_Level == level && t.GC_Cate == gc_cate).List;
                    //}
                    //sb.AppendLine("<option value=''>请选择</option>");
                    //foreach (Model.GoodsCate item in NextGoodsCates)
                    //{
                    //    sb.AppendFormat("<option value='{0}'>{1}</option>", item.id, item.GC_Title);
                    //}
                    break;
                default:
                    break;
            }

            return Content(sb.ToString());
        }
        #endregion

        #region 上传图片 && Flash文件

        public static int? UpGPicId = 1;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult UpGPic(string dir)
        {
            Hashtable hash = new Hashtable();
            try
            {
                //文件保存目录路径
                String savePath = "~/UploadFolder/";

                //文件保存目录URL
                String saveUrl = "/UploadFolder/";

                //定义允许上传的文件扩展名
                Hashtable extTable = new Hashtable();
                extTable.Add("image", "gif,jpg,jpeg,png,bmp");
                //extTable.Add("flash", "swf,flv");
                //extTable.Add("media", "swf,flv,mp3,wav,wma,wmv,mid,avi,mpg,asf,rm,rmvb");
                //extTable.Add("file", "doc,docx,xls,xlsx,ppt,htm,html,txt,zip,rar,gz,bz2");
                //最大文件大小:2MB
                int maxSize = 50000000;

                HttpPostedFileBase imgFile = Request.Files["imgFile"];
                if (imgFile == null)
                {
                    hash["error"] = 1;
                    hash["message"] = "请选择文件。";
                    return Json(hash, "text/html; charset=UTF-8");
                }
                //throw new Exception("请选择文件。");
                String dirPath = Server.MapPath(savePath);
                if (!Directory.Exists(dirPath))
                {
                    hash["error"] = 1;
                    hash["message"] = "上传目录不存在。";
                    return Json(hash, "text/html; charset=UTF-8");
                }
                //throw new Exception("上传目录不存在。");
                String dirName = dir;
                if (String.IsNullOrEmpty(dirName))
                {
                    dirName = "image";
                }
                if (!extTable.ContainsKey(dirName))
                {
                    hash["error"] = 1;
                    hash["message"] = "目录名不正确。";
                    return Json(hash, "text/html; charset=UTF-8");
                }
                //throw new Exception("目录名不正确。");
                String fileName = imgFile.FileName;
                String fileExt = Path.GetExtension(fileName).ToLower();

                if (imgFile.InputStream == null || imgFile.InputStream.Length > maxSize)
                {
                    hash["error"] = 1;
                    hash["message"] = "上传文件大小超过限制。";
                    return Json(hash, "text/html; charset=UTF-8");
                }
                //throw new Exception("上传文件大小超过限制。");
                if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(((String)extTable[dirName]).Split(','), fileExt.Substring(1).ToLower()) == -1)
                {

                    hash["error"] = 1;
                    hash["message"] = "上传文件扩展名是不允许的扩展名。\n只允许" + ((String)extTable[dirName]) + "格式。。";
                    return Json(hash, "text/html; charset=UTF-8");
                }
                //throw new Exception("上传文件扩展名是不允许的扩展名。\n只允许" + ((String)extTable[dirName]) + "格式。");

                //创建文件夹
                dirPath += dirName + "/";
                saveUrl += dirName + "/";
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                String ymd = DateTime.Now.ToString("yyyyMM", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                dirPath += ymd + "/";
                saveUrl += ymd + "/";
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                string UID = DealMvc.Common.Globals.CreateNewUniqueID();
                String newFileName = UID.ToLower() + fileExt;
                String filePath = dirPath + newFileName;

                imgFile.SaveAs(filePath);

                String fileUrl = saveUrl + newFileName;




                #region 创建商品图片
                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(Server.MapPath("~" + fileUrl));
                DateTime? dtime = DateTime.Now;

                string SmallPic = Url.ImageAutoSize(300, 300, fileUrl);        //SmallPic[Type=string] - 小图
                string BigPic = fileUrl;        //BigPic[Type=string] - 大图
                string ThumbnailPic = Url.ImageAutoSize(100, 100, fileUrl);        //ThumbnailPic[Type=string] - 缩略图

                #endregion

                hash = new Hashtable();
                hash["error"] = 0;
                hash["url"] = BigPic;
                hash["dtime"] = dtime.ToStrDateTime(Estatic.ForDateTime.类型三);

                hash["id"] = UpGPicId;
                hash["tsrc"] = ThumbnailPic;
                hash["ssrc"] = SmallPic;
                UpGPicId = UpGPicId + 1;

            }
            catch (Exception message)
            {
                hash = new Hashtable();
                hash["error"] = 1;
                hash["message"] = message;
            }

            return Json(hash, "text/html; charset=UTF-8");
        }


        /*
        uploadJson: '/HtmlEditor/UploadImage',
        allowPreviewEmoticons: false,
        allowImageUpload: true
         */
        #region 上传flash
        [HttpPost]
        public ActionResult UploadImage()
        {

            string savePath = "/swf/";
            string saveUrl = "/swf/";
            string fileTypes = "swf,flv,jpg,png,gif";
            double maxSize = 50 * 1024 * 1024;
            //Convert.ToDouble(file.ContentLength) > Convert.ToDouble(ConfigInfo<UploadInfo>.Instance().UploadSize) * 1024 * 1024
            Hashtable hash = new Hashtable();

            HttpPostedFileBase file = Request.Files["imgFile"];
            if (file == null)
            {
                hash = new Hashtable();
                hash["error"] = 0;
                hash["url"] = "请选择文件";
                return Json(hash);
            }

            string dirPath = Server.MapPath(savePath);
            if (!Directory.Exists(dirPath))
            {
                hash = new Hashtable();
                hash["error"] = 0;
                hash["url"] = "上传目录不存在";
                return Json(hash);
            }

            string fileName = file.FileName;
            string fileExt = Path.GetExtension(fileName).ToLower();

            ArrayList fileTypeList = ArrayList.Adapter(fileTypes.Split(','));

            if (file.InputStream == null || file.InputStream.Length > maxSize)
            {
                hash = new Hashtable();
                hash["error"] = 0;
                hash["url"] = "上传文件大小超过限制";
                return Json(hash);
            }

            if (string.IsNullOrEmpty(fileExt) || Array.IndexOf(fileTypes.Split(','), fileExt.Substring(1).ToLower()) == -1)
            {
                hash = new Hashtable();
                hash["error"] = 0;
                hash["url"] = "上传文件扩展名是不允许的扩展名";
                return Json(hash);
            }

            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt;
            string filePath = dirPath + newFileName;
            file.SaveAs(filePath);
            string fileUrl = saveUrl + newFileName;

            //fileUrl = DealMvc.Common.Globals.UpFileZY(fileUrl);

            hash = new Hashtable();
            hash["error"] = 0;
            hash["url"] = fileUrl;

            return Json(hash, "text/html;charset=UTF-8"); ;
        }

        #endregion

        #endregion

        #region 订单类 事物处理
        ///// <summary>
        ///// 订单业务逻辑
        ///// </summary>
        //DealMvc.Core.Orders.BLL_Orders _BLL_Orders = new Core.Orders.BLL_Orders();

        ///// <summary>
        ///// 取消订单
        ///// </summary>
        ///// <param name="Order_UID"></param>
        ///// <returns></returns>
        //public ContentResult CancelOrder(string Order_UID)
        //{
        //    return Content(_BLL_Orders.CancelOrder(Order_UID).ToString2().ToLower());
        //}

        ///// <summary>
        /////确认收货 
        ///// </summary>
        ///// <param name="Order_UID"></param>
        ///// <returns></returns>
        //public ContentResult ConfirmReceiptOrder(string Order_UID)
        //{
        //    return Content(_BLL_Orders.ConfirmReceiptOrder(Order_UID).ToString2().ToLower());
        //}

        ///// <summary>
        ///// 判断会员卡输入的抵扣金额
        ///// </summary>
        ///// <param name="m_id"></param>
        ///// <param name="input_money"></param>
        ///// <param name="order_money"></param>
        ///// <returns></returns>
        //public ContentResult JudgeDeductibleMoney(int? m_id, double? input_money, float? order_money)
        //{
        //    string msg = string.Empty;
        //    Model.MemberGetUseCard m_Card_model = Model.MemberGetUseCard.GetModel(m_id ?? 0);
        //    if (m_Card_model.IsNull)
        //    {
        //        msg = "0";//会员卡编号不存在，请重新核对后再试
        //    }
        //    else
        //    {
        //        if (input_money <= 0)
        //        {
        //            msg = "1";//输入的抵扣金额必须大于等于1元
        //        }
        //        else
        //        {
        //            if (input_money > m_Card_model.MC_CardBanlance)
        //            {
        //                msg = "2";//输入的抵扣金额已超出卡上可用余额
        //            }
        //            else
        //            {
        //                if (input_money > order_money)
        //                {
        //                    msg = "3";//输入的抵扣金额已超出本次购物的交易金额
        //                }
        //                else
        //                {
        //                    msg = "4";//ok
        //                }
        //            }
        //        }
        //    }
        //    return Content(msg);
        //}
        ///// <summary>
        ///// 确认收货并退款
        ///// </summary>
        ///// <param name="R_LogID"></param>
        ///// <returns></returns>
        //public ContentResult ConfirmReceiptAndRefund(int? R_LogID)
        //{
        //    return Content(_BLL_Orders.ConfirmReceiptAndRefund(R_LogID).ToString2().ToLower());
        //}

        ///// <summary>
        ///// 网站介入退货处理
        ///// </summary>
        ///// <param name="R_LogID"></param>
        ///// <returns></returns>
        //public ContentResult ApplicationSiteIntervene(int? R_LogID)
        //{
        //    return Content(_BLL_Orders.ApplicationSiteIntervene(R_LogID).ToString2().ToLower());
        //}
        ///// <summary>
        ///// 取消此次退货处理
        ///// </summary>
        ///// <param name="R_LogID"></param>
        ///// <returns></returns>
        //public ContentResult CancerRetutnIntervene(int? R_LogID)
        //{
        //    return Content(_BLL_Orders.CancerRetutnIntervene(R_LogID).ToString2().ToLower());
        //}

        ///// <summary>
        ///// 确认 返修退换货
        ///// </summary>
        ///// <param name="cid"></param>
        ///// <returns></returns>
        //public ContentResult SureReturnOrderModel(int? cid)
        //{
        //    return Content(_BLL_Orders.SureReturnOrderModel(cid).ToString2().ToLower());
        //}


        #endregion

        #region 快递接口

        #region 快递类型转换

        /// <summary>
        /// 快递类型转换
        /// </summary>
        /// <param name="typeCom"></param>
        /// <returns></returns>
        public string TypeChange(string typeCom)
        {
            if (typeCom == "AAE全球专递")
            {
                typeCom = "aae";
            }
            if (typeCom == "安捷快递")
            {
                typeCom = "anjiekuaidi";
            }
            if (typeCom == "安信达快递")
            {
                typeCom = "anxindakuaixi";
            }
            if (typeCom == "百福东方")
            {
                typeCom = "baifudongfang";
            }
            if (typeCom == "彪记快递")
            {
                typeCom = "biaojikuaidi";
            }
            if (typeCom == "BHT")
            {
                typeCom = "bht";
            }
            if (typeCom == "BHT")
            {
                typeCom = "bht";
            }
            if (typeCom == "希伊艾斯快递")
            {
                typeCom = "cces";
            }
            if (typeCom == "中国东方")
            {
                typeCom = "Coe";
            }
            if (typeCom == "长宇物流")
            {
                typeCom = "changyuwuliu";
            }
            if (typeCom == "大田物流")
            {
                typeCom = "datianwuliu";
            }
            if (typeCom == "德邦物流")
            {
                typeCom = "debangwuliu";
            }
            if (typeCom == "DPEX")
            {
                typeCom = "dpex";
            }
            if (typeCom == "DHL")
            {
                typeCom = "dhl";
            }
            if (typeCom == "D速快递")
            {
                typeCom = "dsukuaidi";
            }
            if (typeCom == "fedex")
            {
                typeCom = "fedex";
            }
            if (typeCom == "飞康达物流")
            {
                typeCom = "feikangda";
            }
            if (typeCom == "凤凰快递")
            {
                typeCom = "fenghuangkuaidi";
            }
            if (typeCom == "港中能达物流")
            {
                typeCom = "ganzhongnengda";
            }
            if (typeCom == "广东邮政物流")
            {
                typeCom = "guangdongyouzhengwuliu";
            }
            if (typeCom == "汇通快运")
            {
                typeCom = "huitongkuaidi";
            }
            if (typeCom == "恒路物流")
            {
                typeCom = "hengluwuliu";
            }
            if (typeCom == "华夏龙物流")
            {
                typeCom = "huaxialongwuliu";
            }
            if (typeCom == "佳怡物流")
            {
                typeCom = "jiayiwuliu";
            }
            if (typeCom == "京广速递")
            {
                typeCom = "jinguangsudikuaijian";
            }
            if (typeCom == "急先达")
            {
                typeCom = "jixianda";
            }
            if (typeCom == "佳吉物流")
            {
                typeCom = "jiajiwuliu";
            }
            if (typeCom == "加运美")
            {
                typeCom = "jiayunmeiwuliu";
            }
            if (typeCom == "快捷速递")
            {
                typeCom = "kuaijiesudi";
            }
            if (typeCom == "联昊通物流")
            {
                typeCom = "lianhaowuliu";
            }
            if (typeCom == "龙邦物流")
            {
                typeCom = "longbanwuliu";
            }
            if (typeCom == "民航快递")
            {
                typeCom = "minghangkuaidi";
            }
            if (typeCom == "配思货运")
            {
                typeCom = "peisihuoyunkuaidi";
            }
            if (typeCom == "全晨快递")
            {
                typeCom = "quanchenkuaidi";
            }
            if (typeCom == "全际通物流")
            {
                typeCom = "quanjitong";
            }
            if (typeCom == "全日通快递")
            {
                typeCom = "quanritongkuaidi";
            }
            if (typeCom == "全一快递")
            {
                typeCom = "quanyikuaidi";
            }
            if (typeCom == "盛辉物流")
            {
                typeCom = "shenghuiwuliu";
            }
            if (typeCom == "速尔物流")
            {
                typeCom = "suer";
            }
            if (typeCom == "盛丰物流")
            {
                typeCom = "shengfengwuliu";
            }
            if (typeCom == "天地华宇")
            {
                typeCom = "tiandihuayu";
            }
            if (typeCom == "天天快递")
            {
                typeCom = "tiantian";
            }
            if (typeCom == "TNT")
            {
                typeCom = "tnt";
            }
            if (typeCom == "UPS")
            {
                typeCom = "ups";
            }
            if (typeCom == "万家物流")
            {
                typeCom = "wanjiawuliu";
            }
            if (typeCom == "文捷航空速递")
            {
                typeCom = "wenjiesudi";
            }
            if (typeCom == "伍圆速递")
            {
                typeCom = "wuyuansudi";
            }
            if (typeCom == "万象物流")
            {
                typeCom = "wanxiangwuliu";
            }
            if (typeCom == "新邦物流")
            {
                typeCom = "xinbangwuliu";
            }
            if (typeCom == "信丰物流")
            {
                typeCom = "xinfengwuliu";
            }
            if (typeCom == "星晨急便")
            {
                typeCom = "xingchengjibian";
            }
            if (typeCom == "鑫飞鸿物流")
            {
                typeCom = "xinhongyukuaidi";
            }
            if (typeCom == "亚风速递")
            {
                typeCom = "yafengsudi";
            }
            if (typeCom == "一邦速递")
            {
                typeCom = "yibangwuliu";
            }
            if (typeCom == "优速物流")
            {
                typeCom = "youshuwuliu";
            }
            if (typeCom == "远成物流")
            {
                typeCom = "yuanchengwuliu";
            }
            if (typeCom == "圆通速递")
            {
                typeCom = "yuantong";
            }
            if (typeCom == "源伟丰快递")
            {
                typeCom = "yuanweifeng";
            }
            if (typeCom == "元智捷诚快递")
            {
                typeCom = "yuanzhijiecheng";
            }
            if (typeCom == "越丰物流")
            {
                typeCom = "yuefengwuliu";
            }
            if (typeCom == "韵达快递")
            {
                typeCom = "yunda";
            }
            if (typeCom == "源安达")
            {
                typeCom = "yuananda";
            }
            if (typeCom == "运通快递")
            {
                typeCom = "yuntongkuaidi";
            }
            if (typeCom == "宅急送")
            {
                typeCom = "zhaijisong";
            }
            if (typeCom == "中铁快运")
            {
                typeCom = "zhongtiewuliu";
            }
            if (typeCom == "中通速递")
            {
                typeCom = "zhongtong";
            }
            if (typeCom == "中邮物流")
            {
                typeCom = "zhongyouwuliu";
            }

            return typeCom;
        }

        #endregion

        #region 返回运单详情

        /// <summary>
        /// 返回运单详情
        /// </summary>
        /// <param name="typeCom">快递公司</param>
        /// <param name="nu">运单号</param>
        /// <returns></returns>
        public string KuaiDiAPINoVali(string typeCom, string nu)
        {

            Model.SiteInfo m_SiteInfo = Model.SiteInfo.GetModel(t => t.id != 0);

            //请把ea81cb6341791a62修改成您在快递100网站申请的APIKey
            string ApiKey = "ea81cb6341791a62";

            string powered = "快递数据由: <a href=\"http://www.kuaidi100.com/\" target=\"_blank\">快递100</a> 提供";
            string apiurl = "http://api.kuaidi100.com/api?id=" + ApiKey + "&com=" + TypeChange(typeCom) + "&nu=" + nu + "&show=2&muti=1&order=asc";
            //Response.Write (apiurl);
            WebRequest request = WebRequest.Create(@apiurl);
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            Encoding encode = Encoding.UTF8;
            StreamReader reader = new StreamReader(stream, encode);
            string detail = reader.ReadToEnd();

            return (detail + "<br/>" + powered).ToString2();
        }

        #endregion

        #endregion

        #region 获取帮助分类
        /// <summary>
        /// 获取帮助分类
        /// </summary>
        /// <param name="topid"></param>
        /// <returns></returns>
        public ContentResult GetHelpCate(int? topid)
        {
            StringBuilder sb = new StringBuilder();
            List<Model.HelpCenterCate> helpcates = Model.HelpCenterCate.GetModelList(h => h.HCC_ParentID == topid).List;
            foreach (Model.HelpCenterCate item in helpcates)
            {
                sb.AppendFormat("<option value='{0}'>{1}</option>", item.id, item.HCC_Name);
            }
            return Content(sb.ToString2());
        }
        #endregion


        #region 浏览
        public ActionResult ProcessRequest()
        {
            //String aspxUrl = context.Request.Path.Substring(0, context.Request.Path.LastIndexOf("/") + 1);

            //根目录路径，相对路径
            String rootPath = "/keupload/";
            //根目录URL，可以指定绝对路径，比如 http://www.yoursite.com/attached/
            String rootUrl = "/keupload/";
            //图片扩展名
            String fileTypes = "gif,jpg,jpeg,png,bmp";

            String currentPath = "";
            String currentUrl = "";
            String currentDirPath = "";
            String moveupDirPath = "";

            //根据path参数，设置各路径和URL
            String path = Request.QueryString["path"];
            path = String.IsNullOrEmpty(path) ? "" : path;
            if (path == "")
            {
                currentPath = Server.MapPath(rootPath);
                currentUrl = rootUrl;
                currentDirPath = "";
                moveupDirPath = "";
            }
            else
            {
                currentPath = Server.MapPath(rootPath) + path;
                currentUrl = rootUrl + path;
                currentDirPath = path;
                moveupDirPath = Regex.Replace(currentDirPath, @"(.*?)[^\/]+\/$", "$1");
            }

            //排序形式，name or size or type
            String order = Request.QueryString["order"];
            order = String.IsNullOrEmpty(order) ? "" : order.ToLower();

            //不允许使用..移动到上一级目录
            if (Regex.IsMatch(path, @"\.\."))
            {
                Response.Write("Access is not allowed.");
                Response.End();
            }
            //最后一个字符不是/
            if (path != "" && !path.EndsWith("/"))
            {
                Response.Write("Parameter is not valid.");
                Response.End();
            }
            //目录不存在或不是目录
            if (!Directory.Exists(currentPath))
            {
                Response.Write("Directory does not exist.");
                Response.End();
            }

            //遍历目录取得文件信息
            string[] dirList = Directory.GetDirectories(currentPath);
            string[] fileList = Directory.GetFiles(currentPath);

            switch (order)
            {
                case "size":
                    Array.Sort(dirList, new NameSorter());
                    Array.Sort(fileList, new SizeSorter());
                    break;
                case "type":
                    Array.Sort(dirList, new NameSorter());
                    Array.Sort(fileList, new TypeSorter());
                    break;
                case "name":
                default:
                    Array.Sort(dirList, new NameSorter());
                    Array.Sort(fileList, new NameSorter());
                    break;
            }

            Hashtable result = new Hashtable();
            result["moveup_dir_path"] = moveupDirPath;
            result["current_dir_path"] = currentDirPath;
            result["current_url"] = currentUrl;
            result["total_count"] = dirList.Length + fileList.Length;
            List<Hashtable> dirFileList = new List<Hashtable>();
            result["file_list"] = dirFileList;
            for (int i = 0; i < dirList.Length; i++)
            {
                DirectoryInfo dir = new DirectoryInfo(dirList[i]);
                Hashtable hash = new Hashtable();
                hash["is_dir"] = true;
                hash["has_file"] = (dir.GetFileSystemInfos().Length > 0);
                hash["filesize"] = 0;
                hash["is_photo"] = false;
                hash["filetype"] = "";
                hash["filename"] = dir.Name;
                hash["datetime"] = dir.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                dirFileList.Add(hash);
            }
            for (int i = 0; i < fileList.Length; i++)
            {
                FileInfo file = new FileInfo(fileList[i]);
                Hashtable hash = new Hashtable();
                hash["is_dir"] = false;
                hash["has_file"] = false;
                hash["filesize"] = file.Length;
                hash["is_photo"] = (Array.IndexOf(fileTypes.Split(','), file.Extension.Substring(1).ToLower()) >= 0);
                hash["filetype"] = file.Extension.Substring(1);
                hash["filename"] = file.Name;
                hash["datetime"] = file.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                dirFileList.Add(hash);
            }
            //Response.AddHeader("Content-Type", "application/json; charset=UTF-8");
            //context.Response.Write(JsonMapper.ToJson(result));
            //context.Response.End();
            return Json(result, "text/html;charset=UTF-8", JsonRequestBehavior.AllowGet);
        }

        public class NameSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }
                if (x == null)
                {
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                FileInfo xInfo = new FileInfo(x.ToString());
                FileInfo yInfo = new FileInfo(y.ToString());

                return xInfo.FullName.CompareTo(yInfo.FullName);
            }
        }

        public class SizeSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }
                if (x == null)
                {
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                FileInfo xInfo = new FileInfo(x.ToString());
                FileInfo yInfo = new FileInfo(y.ToString());

                return xInfo.Length.CompareTo(yInfo.Length);
            }
        }

        public class TypeSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }
                if (x == null)
                {
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                FileInfo xInfo = new FileInfo(x.ToString());
                FileInfo yInfo = new FileInfo(y.ToString());

                return xInfo.Extension.CompareTo(yInfo.Extension);
            }
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
        #endregion

    }
}
