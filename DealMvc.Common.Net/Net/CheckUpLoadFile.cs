using System;
using System.Collections.Generic;
using System.Text;

namespace DealMvc.Common.Net
{
    /// <summary>
    /// 检查上传文件
    /// </summary>
    public class CheckUpLoadFile
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        public static string _Message = null;

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="_FileUpload">上传控件</param>
        /// <param name="Des">控件内容描述</param>
        /// <returns></returns>
        public static bool checkFileHas(System.Web.UI.WebControls.FileUpload _FileUpload, string
Des)
        {
            string S = null;
            if ((S = _checkFileHas(_FileUpload, Des)) != "true")
            {
                _Message = S;
                return true;
            }
            else
            {
                _Message = null;
                return false;
            }
        }

        /// <summary>
        /// 检查文件大小
        /// </summary>
        /// <param name="_FileUpload">上传控件</param>
        /// <param name="Des">控件内容描述</param>
        /// <param name="MB">允许最大值</param>
        /// <returns></returns>
        public static bool checkFileSize(System.Web.UI.WebControls.FileUpload _FileUpload, string
Des, int MB)
        {
            if (_FileUpload.HasFile)
            {
                string S = null;
                if ((S = _checkFileSize(_FileUpload, Des, MB)) != "true")
                {
                    _Message = S;
                    return true;
                }
                else
                {
                    _Message = null;
                    return false;
                }
            }
            else
            {
                _Message = null;
                return false;
            }
        }

        /// <summary>
        /// 检查文件格式
        /// </summary>
        /// <param name="_FileUpload">上传控件</param>
        /// <param name="Des">控件内容描述</param>
        /// <param name="Extensions">允许的格式 jpg|gif|bmp</param>
        /// <returns></returns>
        public static bool checkFileExtension(System.Web.UI.WebControls.FileUpload _FileUpload, string Des, string Extensions)
        {
            if (_FileUpload.HasFile)
            {
                string S = null;
                if ((S = _checkFileExtension(_FileUpload, Des, Extensions)) != "true")
                {
                    _Message = S;
                    return true;
                }
                else
                {
                    _Message = null;
                    return false;
                }
            }
            else
            {
                _Message = null;
                return false;
            }
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="_FileUpload">上传控件</param>
        /// <param name="Des">控件内容描述</param>
        /// <returns></returns>
        private static string _checkFileHas(System.Web.UI.WebControls.FileUpload _FileUpload, string
 Des)
        {
            if (_FileUpload.HasFile)
            {
                return "true";
            }
            else
            {
                return Des + Msg.fileNotEmpty;
            }
        }

        /// <summary>
        /// 检查文件大小
        /// </summary>
        /// <param name="_FileUpload">上传控件</param>
        /// <param name="Des">控件内容描述</param>
        /// <param name="MB">允许最大值</param>
        /// <returns></returns>
        private static string _checkFileSize(System.Web.UI.WebControls.FileUpload _FileUpload, string
Des, int MB)
        {
            if (_FileUpload.PostedFile.ContentLength <= (MB * 1024 * 1024))
            {
                return "true";
            }
            else
            {
                return Des + Msg.fileSize + MB.ToString() + "MB";
            }
        }

        /// <summary>
        /// 检查文件格式
        /// </summary>
        /// <param name="_FileUpload">上传控件</param>
        /// <param name="Des">控件内容描述</param>
        /// <param name="Extensions">允许的格式(小写) jpg|gif|bmp</param>
        /// <returns></returns>
        private static string _checkFileExtension(System.Web.UI.WebControls.FileUpload _FileUpload, string Des, string Extensions)
        {
            string output = Des + Msg.fileEx;

            string fileExtension = DealString.getExtension(_FileUpload.FileName);
            string[] _string = Extensions.Split(new char[] { '|' });
            for (int i = 0; i < _string.Length; i++)
            {
                if (_string[i].ToString().Equals(fileExtension.ToLower()))
                {
                    output = "true";
                    break;
                }
            }
            return output;
        }
    }
}
