using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using JumbotCms.API.Tenpay;
using DealMvc.Common.Config;

namespace DealMvc.Pay.Tenpay
{
    public class TenPay 
    {
        HttpContext context;
        public TenPay(HttpContext context)
        {
            this.context = context;
        }
        public string RequestPay(string ordernum,string return_url,double? PayMoney,string ProductName,string Remark)
        {


        //创建PayRequestHandler实例
        PayRequestHandler reqHandler = new PayRequestHandler(context);

        Model.SitePayAPI m_entity = Model.SitePayAPI.GetModel(t => t.ApiType == "财付通");
        //商户号
        string bargainor_id = m_entity.AppIdentity;
        //设置密钥
        reqHandler.setKey(m_entity.AppKey);

        //初始化
        reqHandler.init();

        //当前时间 yyyyMMdd
        string date = DateTime.Now.ToString("yyyyMMdd");

        //生成订单10位序列号，此处用时间和随机数生成，商户根据自己调整，保证唯一
        string strReq = DateTime.Now.ToString("HHmmss") + TenpayUtil.BuildRandomStr(4);


        //财付通订单号，10位商户号+8位日期+10位序列号，需保证全局唯一
        string transaction_id = bargainor_id + date + strReq;


        //-----------------------------
        //设置支付参数
        //-----------------------------
        reqHandler.setParameter("bargainor_id", bargainor_id);			//商户号
        reqHandler.setParameter("sp_billno", ordernum);				//商家订单号
        reqHandler.setParameter("transaction_id", transaction_id);		//财付通交易单号
        reqHandler.setParameter("return_url", return_url);				//支付通知url
        reqHandler.setParameter("desc", ProductName);	//商品名称
        reqHandler.setParameter("attach", Remark==null?"":Remark);	//会员ID
        reqHandler.setParameter("total_fee", ((int)(PayMoney*100)).ToString());						//商品金额,以分为单位


        //用户ip,测试环境时不要加这个ip参数，正式环境再加此参数
        reqHandler.setParameter("spbill_create_ip",DealMvc.Common.Globals.GetUserIP());

        //获取请求带参数的url
        string requestUrl = reqHandler.getRequestURL();
        return requestUrl;
        //FinalMessage("正在进入财付通网站...", requestUrl, 0, 4);
        }

        public bool ReturnPay()
        {
            //密钥
            String key = Model.SitePayAPI.GetModel(t => t.ApiType == "财付通").AppKey;


            PayResponseHandler resHandler = new PayResponseHandler(context);

            resHandler.setKey(key);

            //判断签名
            if (resHandler.isTenpaySign())
            {
                //交易单号
                string transaction_id = resHandler.getParameter("transaction_id");

                //金额金额,以分为单位
                string total_fee = resHandler.getParameter("total_fee");

                //支付结果
                string pay_result = resHandler.getParameter("pay_result");

                //会员ID
                string userid = resHandler.getParameter("attach");

                if ("0".Equals(pay_result))
                {

                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false; 
            }
        }
    }
}
