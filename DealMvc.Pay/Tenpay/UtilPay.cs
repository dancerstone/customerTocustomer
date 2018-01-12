using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using JumbotCms.API.Tenpay;
using DealMvc.Common.Config;

namespace DealMvc.Pay.Tenpay
{
    public class UtilPay
    {
        public string Yinhang { get; set; }
        HttpContext context;
        public UtilPay(HttpContext context)
        {
            this.context = context;
        }



        /// <summary>
        /// 财付通接口
        /// </summary>
        /// <param name="desc">商品名</param>
        /// <param name="sp_billno">订单号</param>
        /// <param name="total_fee">货品总价格</param>
        /// <returns></returns>
        public string GetTenpayUrl(string desc, string orderid, string total_fee,string returnurl)
        {
            //创建PayRequestHandler实例
            PayRequestHandler reqHandler = new PayRequestHandler(context);
            reqHandler.YinHang = Yinhang;
            Model.SitePayAPI m_entity = Model.SitePayAPI.GetModel(t => t.ApiType =="财付通");
            //设置密钥
            reqHandler.setKey(m_entity.AppKey);
            //初始化
            reqHandler.init();
            //当前时间 yyyyMMdd
            string date = DateTime.Now.ToString("yyyyMMdd");

            //生成订单10位序列号，此处用时间和随机数生成，商户根据自己调整，保证唯一
            string sp_billno = DateTime.Now.ToString("HHmmss") + JumbotCms.API.Tenpay.TenpayUtil.BuildRandomStr(4);
            //商户号
            string bargainor_id = m_entity.AppIdentity;// "1208389301";

            //财付通订单号，10位商户号+8位日期+10位序列号，需保证全局唯一
            string transaction_id = bargainor_id + date + sp_billno;

            //test******
            //double PayMoney_test = 0.01;


            //订单编号
            string TenpaySN = transaction_id;

            //-----------------------------
            //设置支付参数
            //-----------------------------
            reqHandler.setParameter("bargainor_id", m_entity.AppIdentity);			//商户号
            reqHandler.setParameter("sp_billno", sp_billno);				//商家订单号
            reqHandler.setParameter("transaction_id", transaction_id);		//财付通交易单号
            reqHandler.setParameter("return_url", Common.Globals.GetHostUrlWeb() + returnurl);				//支付通知url
            reqHandler.setParameter("desc", desc);	//商品名称
            reqHandler.setParameter("attach", orderid);	//订单ID
            reqHandler.setParameter("total_fee", ((total_fee.ToDouble2() * 100).ToInt32()).ToString());	//商品金额,以分为单位


            //用户ip,测试环境时不要加这个ip参数，正式环境再加此参数
            reqHandler.setParameter("spbill_create_ip", DealMvc.Common.Globals.GetUserIP());

            //获取请求带参数的url
            return reqHandler.getRequestURL();
        }
    }
}
