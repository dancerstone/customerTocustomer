using System;
using System.Configuration;
using DealMvc.Common;

namespace DealMvc.DBUtility
{
    /// <summary>
    /// 
    /// </summary>
    public class PubConstant
    {
        /// <summary>
        /// ��ȡ�����ַ���
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                string _connectionString = ConfigurationSettings.AppSettings["ConnectionString"];
                string ConStringEncrypt = ConfigurationSettings.AppSettings["ConStringEncrypt"];
                if (ConStringEncrypt == "true")
                {
                    _connectionString = Common.Base.DESEncrypt.Decrypt(_connectionString, "000000");
                }
                return _connectionString;
            }
        }

        /// <summary>
        /// �õ�web.config������������ݿ������ַ�����
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        public static string GetConnectionString(string configName)
        {
            string connectionString = ConfigurationManager.AppSettings[configName];
            string ConStringEncrypt = ConfigurationManager.AppSettings["ConStringEncrypt"];
            if (ConStringEncrypt == "true")
            {
                connectionString = Common.Base.DESEncrypt.Decrypt(connectionString, "000000");
            }
            return connectionString;
        }


    }
}
