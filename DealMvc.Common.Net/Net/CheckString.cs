using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DealMvc.Common.Net
{
    /// <summary>
    /// 检验字符串类
    /// </summary>
    public class CheckString
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        public static string _Message = null;

        #region 综合检查返回Bool

        /// <summary>
        /// 综合检查字符串(_String) [待检验的字符串、对待检验的字符串进行描述、是否有注入、是否为空、是否为邮箱、是否为数字型、是否有汉字]
        /// </summary>
        /// <param name="_String">待检验的字符串</param>
        /// <param name="_StringDes">对待检验的字符串进行描述</param>
        /// <param name="ChechStringHasSQL">是否检查防注(有注入返回true)</param>
        /// <param name="ChechStringIsEmpty">是否检查为空(空返回true)</param>
        /// <param name="ChechStringIsEmail">是否检查为邮箱(不是邮箱返回true)</param>
        /// <param name="ChechStringIsNumber">是否检查为数字型字符串(不是数字类型返回true)</param>
        /// <param name="ChechStringHasChinaese">是否检查含有汉字字符串(有汉字返回true)</param>
        /// <returns>有异常返回true,无异常返回false</returns>
        public static bool _CheckString(string _String, string _StringDes, bool ChechStringHasSQL, bool ChechStringIsEmpty, bool ChechStringIsEmail, bool ChechStringIsNumber, bool ChechStringHasChinaese)
        {
            string msg = null;
            if (!((msg = CheckString.CheckStringAll(_String, _StringDes, ChechStringHasSQL, ChechStringIsEmpty, ChechStringIsEmail, ChechStringIsNumber, ChechStringHasChinaese)).Equals("true")))
            {
                CheckString._Message = msg;
                return true;
            }
            else
            {
                CheckString._Message = null;
                return false;
            }
        }

        /// <summary>
        /// 综合检查字符串(_String) [待检验的字符串、对待检验的字符串进行描述、长度大于等于[">="](或小于等于["&lt;="])number、是否有注入、是否为空、是否为邮箱、是否为数字型、是否有汉字]
        /// </summary>
        /// <param name="_String">待检验的字符串</param>
        /// <param name="_StringDes">对待检验的字符串进行描述</param>
        /// <param name="Sign">标识符(为">=":长度大于等于Number)(为"&lt;=":长度小于等于Number)</param>
        /// <param name="Number">限制(上线或者下线)</param>
        /// <param name="ChechStringHasSQL">是否检查防注(有注入返回true)</param>
        /// <param name="ChechStringIsEmpty">是否检查为空(空返回true)</param>
        /// <param name="ChechStringIsEmail">是否检查为邮箱(不是邮箱返回true)</param>
        /// <param name="ChechStringIsNumber">是否检查为数字型字符串(不是数字类型返回true)</param>
        /// <param name="ChechStringHasChinaese">是否检查含有汉字字符串(有汉字返回true)</param>
        /// <returns>有异常返回true,无异常返回false</returns>
        public static bool _CheckString(string _String, string _StringDes, string Sign, int Number, bool ChechStringHasSQL, bool ChechStringIsEmpty, bool ChechStringIsEmail, bool ChechStringIsNumber, bool ChechStringHasChinaese)
        {
            string msg = null;
            if (!((msg = CheckString.CheckStringAll(_String, _StringDes, Sign, Number, ChechStringHasSQL, ChechStringIsEmpty, ChechStringIsEmail, ChechStringIsNumber, ChechStringHasChinaese)).Equals("true")))
            {
                CheckString._Message = msg;
                return true;
            }
            else
            {
                CheckString._Message = null;
                return false;
            }
        }

        /// <summary>
        /// 综合检查字符串(_String) [待检验的字符串、对待检验的字符串进行描述、长度在min和max之间、是否有注入、是否为空、是否为邮箱、是否为数字型、是否有汉字]
        /// </summary>
        /// <param name="_String">待检验的字符串</param>
        /// <param name="_StringDes">对待检验的字符串进行描述</param>
        /// <param name="min">最小数(可等于)</param>
        /// <param name="max">最大数(可等于)</param>
        /// <param name="ChechStringHasSQL">是否检查防注(有注入返回true)</param>
        /// <param name="ChechStringIsEmpty">是否检查为空(空返回true)</param>
        /// <param name="ChechStringIsEmail">是否检查为邮箱(不是邮箱返回true)</param>
        /// <param name="ChechStringIsNumber">是否检查为数字型字符串(不是数字类型返回true)</param>
        /// <param name="ChechStringHasChinaese">是否检查含有汉字字符串(有汉字返回true)</param>
        /// <returns>有异常返回true,无异常返回false</returns>
        public static bool _CheckString(string _String, string _StringDes, int min, int max, bool ChechStringHasSQL, bool ChechStringIsEmpty, bool ChechStringIsEmail, bool ChechStringIsNumber, bool ChechStringHasChinaese)
        {
            string msg = null;
            if (!((msg = CheckString.CheckStringAll(_String, _StringDes, min, max, ChechStringHasSQL, ChechStringIsEmpty, ChechStringIsEmail, ChechStringIsNumber, ChechStringHasChinaese)).Equals("true")))
            {
                CheckString._Message = msg;
                return true;
            }
            else
            {
                CheckString._Message = null;
                return false;
            }
        }

        #endregion

        #region 综合检查-私有

        /// <summary>
        /// 综合检查字符串(_String) [待检验的字符串、对待检验的字符串进行描述、是否有注入、是否为空、是否为邮箱、是否为数字型、是否有汉字]
        /// </summary>
        /// <param name="_String">待检验的字符串</param>
        /// <param name="_StringDes">对待检验的字符串进行描述</param>
        /// <param name="ChechStringHasSQL">是否检查防蛀</param>
        /// <param name="ChechStringIsEmpty">是否检查为空</param>
        /// <param name="ChechStringIsEmail">是否检查为邮箱</param>
        /// <param name="ChechStringIsNumber">是否检查为数字型字符串</param>
        /// <param name="ChechStringHasChinaese">是否检查含有汉字字符串</param>
        /// <returns>检验通过返回(字符串true),没有通过返回错误提示信息</returns>
        private static string CheckStringAll(string _String, string _StringDes, bool ChechStringHasSQL, bool ChechStringIsEmpty, bool ChechStringIsEmail, bool ChechStringIsNumber, bool ChechStringHasChinaese)
        {
            string S = null;

            if (ChechStringIsEmpty)
            {
                if (!((S = CheckString.ChechStringIsEmpty(_String, _StringDes)).Equals("true")))
                {
                    return S;
                }
            }
            else
            {
                if (_String.Equals(String.Empty))
                {
                    return "true";
                }
            }

            if (ChechStringHasSQL)
            {
                if (!((S = CheckString.ChechStringHasSQL(_String, _StringDes)).Equals("true")))
                {
                    return S;
                }
            }

            if (ChechStringIsEmail)
            {
                if (!((S = CheckString.ChechStringIsEmail(_String, _StringDes)).Equals("true")))
                {
                    return S;
                }
            }

            if (ChechStringIsNumber)
            {
                if (!((S = CheckString.ChechStringIsNumber(_String, _StringDes)).Equals("true")))
                {
                    return S;
                }
            }

            if (ChechStringHasChinaese)
            {
                if (!((S = CheckString.ChechStringHasChinaese(_String, _StringDes)).Equals("true")))
                {
                    return S;
                }
            }

            return "true";
        }

        /// <summary>
        /// 综合检查字符串(_String) [待检验的字符串、对待检验的字符串进行描述、长度大于等于[">="](或小于等于["&lt;="])number、是否有注入、是否为空、是否为邮箱、是否为数字型、是否有汉字]
        /// </summary>
        /// <param name="_String">待检验的字符串</param>
        /// <param name="_StringDes">对待检验的字符串进行描述</param>
        /// <param name="Sign">标识符(为">=":长度大于等于Number)(为"&lt;=":长度小于等于Number)</param>
        /// <param name="Number">限制(上线或者下线)</param>
        /// <param name="ChechStringHasSQL">是否检查防蛀</param>
        /// <param name="ChechStringIsEmpty">是否检查为空</param>
        /// <param name="ChechStringIsEmail">是否检查为邮箱</param>
        /// <param name="ChechStringIsNumber">是否检查为数字型字符串</param>
        /// <param name="ChechStringHasChinaese">是否检查含有汉字字符串</param>
        /// <returns>检验通过返回(字符串true),没有通过返回错误提示信息</returns>
        private static string CheckStringAll(string _String, string _StringDes, string Sign, int Number, bool ChechStringHasSQL, bool ChechStringIsEmpty, bool ChechStringIsEmail, bool ChechStringIsNumber, bool ChechStringHasChinaese)
        {
            string S = null;

            if (ChechStringIsEmpty)
            {
                if (!((S = CheckString.ChechStringIsEmpty(_String, _StringDes)).Equals("true")))
                {
                    return S;
                }
            }
            else
            {
                if (_String.Equals(String.Empty))
                {
                    return "true";
                }
            }

            //小于或等于Number
            if (Sign.Equals("<="))
            {
                if (!((S = CheckString.CheckStringLength_max(_String, _StringDes, Number)).Equals("true")))
                {
                    return S;
                }
            }
            //大于或等于Number
            else if (Sign.Equals(">="))
            {
                if (!((S = CheckString.CheckStringLength_min(_String, _StringDes, Number)).Equals("true")))
                {
                    if (ChechStringIsEmpty)
                    {//要检查为空
                        return S;
                    }
                    else
                    {//不检查为空
                        if (_String.Equals(String.Empty))
                        {//为空

                        }
                        else
                        {//不为空
                            return S;
                        }
                    }
                }
            }
            else
            {
                throw new Exception("Sign参数不正确只能为<=或者>=");
            }

            if (ChechStringHasSQL)
            {
                if (!((S = CheckString.ChechStringHasSQL(_String, _StringDes)).Equals("true")))
                {
                    return S;
                }
            }

            if (ChechStringIsEmail)
            {
                if (!((S = CheckString.ChechStringIsEmail(_String, _StringDes)).Equals("true")))
                {
                    return S;
                }
            }

            if (ChechStringIsNumber)
            {
                if (!((S = CheckString.ChechStringIsNumber(_String, _StringDes)).Equals("true")))
                {
                    return S;
                }
            }

            if (ChechStringHasChinaese)
            {
                if (!((S = CheckString.ChechStringHasChinaese(_String, _StringDes)).Equals("true")))
                {
                    return S;
                }
            }

            return "true";
        }

        /// <summary>
        /// 综合检查字符串(_String) [待检验的字符串、对待检验的字符串进行描述、长度在min和max之间、是否有注入、是否为空、是否为邮箱、是否为数字型、是否有汉字]
        /// </summary>
        /// <param name="_String">待检验的字符串</param>
        /// <param name="_StringDes">对待检验的字符串进行描述</param>
        /// <param name="min">最小数(可等于)</param>
        /// <param name="max">最大数(可等于)</param>
        /// <param name="ChechStringHasSQL">是否检查防蛀</param>
        /// <param name="ChechStringIsEmpty">是否检查为空</param>
        /// <param name="ChechStringIsEmail">是否检查为邮箱</param>
        /// <param name="ChechStringIsNumber">是否检查为数字型字符串</param>
        /// <param name="ChechStringHasChinaese">是否检查含有汉字字符串</param>
        /// <returns>检验通过返回(字符串true),没有通过返回错误提示信息</returns>
        private static string CheckStringAll(string _String, string _StringDes, int min, int max, bool ChechStringHasSQL, bool ChechStringIsEmpty, bool ChechStringIsEmail, bool ChechStringIsNumber, bool ChechStringHasChinaese)
        {
            string S = null;

            if (ChechStringIsEmpty)
            {
                if (!((S = CheckString.ChechStringIsEmpty(_String, _StringDes)).Equals("true")))
                {
                    return S;
                }
            }
            else
            {
                if (_String.Equals(String.Empty))
                {
                    return "true";
                }
            }

            if (!((S = CheckString.CheckStringLength_min_max(_String, _StringDes, min, max)).Equals("true")))
            {
                if (ChechStringIsEmpty)
                {//要检查为空
                    return S;
                }
                else
                {//不检查为空
                    if (_String.Equals(String.Empty))
                    {//为空

                    }
                    else
                    {//不为空
                        return S;
                    }
                }
            }

            if (ChechStringHasSQL)
            {
                if (!((S = CheckString.ChechStringHasSQL(_String, _StringDes)).Equals("true")))
                {
                    return S;
                }
            }

            if (ChechStringIsEmail)
            {
                if (!((S = CheckString.ChechStringIsEmail(_String, _StringDes)).Equals("true")))
                {
                    return S;
                }
            }

            if (ChechStringIsNumber)
            {
                if (!((S = CheckString.ChechStringIsNumber(_String, _StringDes)).Equals("true")))
                {
                    return S;
                }
            }

            if (ChechStringHasChinaese)
            {
                if (!((S = CheckString.ChechStringHasChinaese(_String, _StringDes)).Equals("true")))
                {
                    return S;
                }
            }

            return "true";
        }

        #endregion

        #region 单项检查字符串

        /// <summary>
        /// 对_String字符串进行是否为空检验(不算两边的空格)
        /// 不为空返回(字符串true)
        /// 否则返回错误提示信息
        /// </summary>
        /// <param name="_String">待检验的字符串</param>
        /// <param name="_StringDes">对待检验的字符串进行描述</param>
        /// <returns>检验通过返回(字符串true),没有通过返回错误提示信息</returns>
        public static string ChechStringIsEmpty(string _String, string _StringDes)
        {
            if (_String.Trim() == String.Empty)
            {
                return _StringDes + Msg.notEmpty;
            }
            else
            {
                return "true";
            }
        }


        /// <summary>
        /// 对_String字符串进行长度测试(一个汉字算两个字符)
        /// 长度在(大于等于min)和(小于等于max)返回(字符串true)
        /// 否则返回错误提示信息
        /// </summary>
        /// <param name="_String">待检验的字符串</param>
        /// <param name="_StringDes">对待检验的字符串进行描述</param>
        /// <param name="min">最小数</param>
        /// <param name="max">最大数</param>
        /// <returns>检验通过返回(字符串true),没有通过返回错误提示信息</returns>
        public static string CheckStringLength_min_max(string _String, string _StringDes, int min, int max)
        {
            string M_String = CheckString.ZhuanHuanStringASIIC(_String);
            if (M_String.Length >= min && M_String.Length <= max)
            {
                return "true";
            }
            else
            {
                return _StringDes + Msg.lengthError1 + min + "-" + max + Msg.lengthError2;
            }
        }

        /// <summary>
        /// 对_String字符串进行长度测试,
        /// (大于等于min)返回(字符串true)
        /// 否则返回错误提示信息
        /// </summary>
        /// <param name="_String">待检验的字符串</param>
        /// <param name="_StringDes">对待检验的字符串进行描述</param>
        /// <param name="min">最小数</param>        
        /// <returns>检验通过返回(字符串true),没有通过返回错误提示信息</returns>
        public static string CheckStringLength_min(string _String, string _StringDes, int min)
        {
            string M_String = CheckString.ZhuanHuanStringASIIC(_String);
            if (M_String.Length >= min)
            {
                return "true";
            }
            else
            {
                return _StringDes + Msg.lengthError3 + min + Msg.lengthError4;
            }
        }

        /// <summary>
        /// 对_String字符串进行长度测试,
        /// (小于等于max)返回(字符串true)
        /// 否则返回错误提示信息
        /// </summary>
        /// <param name="_String">待检验的字符串</param>
        /// <param name="_StringDes">对待检验的字符串进行描述</param>
        /// <param name="max">最大数</param>        
        /// <returns>检验通过返回(字符串true),没有通过返回错误提示信息</returns>
        public static string CheckStringLength_max(string _String, string _StringDes, int max)
        {
            string M_String = CheckString.ZhuanHuanStringASIIC(_String);
            if (M_String.Length <= max)
            {
                return "true";
            }
            else
            {
                return _StringDes + Msg.lengthError5 + max + Msg.lengthError4;
            }
        }

        /// <summary>
        /// 防注入-- 
        /// 没有含有非法字符返回(字符串true)
        /// 否则返回错误提示信息
        /// </summary>
        /// <param name="_String">验证字符串</param>
        /// <param name="_StringDes">对待检验的字符串进行描述</param>
        /// <returns>检验通过返回(字符串true),没有通过返回错误提示信息(证明有非法字符)</returns>
        public static string ChechStringHasSQL(string _String, string _StringDes)
        {
            string HaveSQL = "true";

            string SQL = Z_String.SQL_String;

            string[] SQLArr = SQL.Split(new char[] { '|' });

            for (int I = 0; I < SQLArr.Length; I++)
            {
                if (_String.IndexOf(" " + SQLArr[I].ToString() + " ") > -1 || _String.IndexOf(SQLArr[I].ToString() + " ") > -1 || _String.IndexOf(" " + SQLArr[I].ToString()) > -1)
                {
                    HaveSQL = _StringDes + Msg.hasSQL;
                    return HaveSQL;
                    //break;
                }
            }

            string SQL2 = Z_String.SQL_String2;

            string[] SQLArr2 = SQL2.Split(new char[] { '|' });

            for (int U = 0; U < SQLArr2.Length; U++)
            {
                if (_String.IndexOf(SQLArr2[U].ToString()) > -1)
                {
                    HaveSQL = _StringDes + Msg.hasSQL;
                    return HaveSQL;
                    //break;
                }
            }

            return HaveSQL;
        }

        /// <summary>
        /// 检验_String是否有中文字符
        /// 没有中文字符返回(字符串true)
        /// 否则返回错误提示信息
        /// </summary>
        /// <param name="_String">验证字符串</param>
        /// <param name="_StringDes">对待检验的字符串进行描述</param>
        /// <returns>检验通过返回(字符串true),没有通过返回错误提示信息(证明有非法字符)</returns>
        public static string ChechStringHasChinaese(string _String, string _StringDes)
        {
            Regex reg = new Regex(@"[^\x00-\xff]");
            if (reg.IsMatch(_String))
            {
                return _StringDes + Msg.hasChinese;
            }
            else
            {
                return "true";
            }
        }

        /// <summary>
        /// 检验_String是否是数字型的字符串
        /// 是数字型的返回(字符串true)
        /// 否则返回错误提示信息
        /// </summary>
        /// <param name="_String">验证字符串</param>
        /// <param name="_StringDes">对待检验的字符串进行描述</param>
        /// <returns>检验通过返回(字符串true),没有通过返回错误提示信息(证明有非法字符)</returns>
        public static string ChechStringIsNumber(string _String, string _StringDes)
        {
            Regex reg = new Regex(@"^\d*$");
            if (reg.IsMatch(_String))
            {
                return "true";
            }
            else
            {
                return _StringDes + Msg.NotNumber;
            }
        }

        /// <summary>
        /// 检验_String是否是邮箱类型
        /// 是邮箱型的返回(字符串true)
        /// 否则返回错误提示信息
        /// </summary>
        /// <param name="_String">验证字符串</param>
        /// <param name="_StringDes">对待检验的字符串进行描述</param>
        /// <returns>检验通过返回(字符串true),没有通过返回错误提示信息(证明有非法字符)</returns>
        public static string ChechStringIsEmail(string _String, string _StringDes)
        {
            Regex reg = new Regex(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
            if (reg.IsMatch(_String))
            {
                return "true";
            }
            else
            {
                return _StringDes + Msg.NotEmail;
            }
        }

        /// <summary>
        /// 将双字节字符转换成ww
        /// </summary>
        /// <param name="_string">待转换的字符串</param>
        /// <returns>返回转换后的字符串</returns>
        private static string ZhuanHuanStringASIIC(string _string)
        {
            Regex reg = new Regex(@"[^\x00-\xff]");
            return reg.Replace(_string, "ww");
        }

        #endregion

    }
}
