using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace ExceptionEx
{
    /// <summary>
    /// 错误等级
    /// </summary>
    public enum ExRank
    {
        /// <summary>
        /// 严重
        /// </summary>
        Serious,
        /// <summary>
        /// 紧急
        /// </summary>
        Emergency,
        /// <summary>
        /// 一般
        /// </summary>
        General,
        /// <summary>
        /// 提示警告
        /// </summary>
        Warning,
    }

    /// <summary>
    /// 异常类别
    /// </summary>
    public enum ExceptionType
    {
        /// <summary>
        /// 系统
        /// </summary>
        System,
        /// <summary>
        /// 注册表读写
        /// </summary>
        Register,
        /// <summary>
        /// 文件读写
        /// </summary>
        IO,
        /// <summary>
        /// 未知类型
        /// </summary>
        None,
    }
    /// <summary>
    /// 自定义异常A
    /// </summary>
    public class MyExceptionMessageBox : Exception
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        public MyExceptionMessageBox(string message)
            : base(message)
        { }
    }
    /// <summary>
    /// 自定义异常A
    /// </summary>
    public class ExceptionMe : Exception
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        public ExceptionMe(string message)
            : base(message)
        { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public ExceptionMe(string message, Exception ex)
            : base(message, ex)
        { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="_method"></param>
        /// <param name="_rank"></param>
        public ExceptionMe(string message, Exception ex, string _method, ExRank _rank)
            : base(message, ex)
        {
            Rank = _rank;
            Method = _method;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="_method"></param>
        /// <param name="_rank"></param>
        /// <param name="_extype"></param>
        public ExceptionMe(string message, Exception ex, string _method, ExRank _rank, ExceptionType _extype)
            : base(message, ex)
        {
            Rank = _rank;
            ExType = _extype;
            Method = _method;
        }

        /// <summary>
        /// 异常发生函数
        /// </summary>
        public string method = null;
        public string Method
        {
            get { return method; }
            set
            {
                method = value;
            }
        }

        /// <summary>
        /// 异常等级
        /// </summary>
        public ExRank rank = ExRank.General;
        public ExRank Rank
        {
            get { return rank; }
            set
            {
                rank = value;
            }
        }

        /// <summary>
        /// 异常类别
        /// </summary>
        public ExceptionType extype = ExceptionType.System;
        public ExceptionType ExType
        {
            get { return extype; }
            set
            {
                extype = value;
            }
        }
    }

    /// <summary>
    /// 自定义异常ExceptionRegister
    /// </summary>
    public class ExceptionRegister : Exception
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        public ExceptionRegister(string message)
            : base(message)
        { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public ExceptionRegister(string message, Exception ex)
            : base(message, ex)
        { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="_method"></param>
        /// <param name="_rank"></param>
        public ExceptionRegister(string message, Exception ex, string _method, ExRank _rank)
            : base(message, ex)
        {
            Rank = _rank;
            Method = _method;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="_method"></param>
        /// <param name="_rank"></param>
        /// <param name="_extype"></param>
        public ExceptionRegister(string message, Exception ex, string _method, ExRank _rank, ExceptionType _extype)
            : base(message, ex)
        {
            Rank = _rank;
            ExType = _extype;
            Method = _method;
        }

        /// <summary>
        /// 异常发生函数
        /// </summary>
        public string method = null;
        public string Method
        {
            get { return method; }
            set
            {
                method = value;
            }
        }

        /// <summary>
        /// 异常等级
        /// </summary>
        public ExRank rank = ExRank.General;
        public ExRank Rank
        {
            get { return rank; }
            set
            {
                rank = value;
            }
        }

        /// <summary>
        /// 异常类别
        /// </summary>
        public ExceptionType extype = ExceptionType.Register;
        public ExceptionType ExType
        {
            get { return extype; }
            set
            {
                extype = value;
            }
        }
    }

    /// <summary>
    /// 自定义异常ExceptionIniFile
    /// </summary>
    public class ExceptionIniFile : Exception
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        public ExceptionIniFile(string message)
            : base(message)
        { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public ExceptionIniFile(string message, Exception ex)
            : base(message, ex)
        { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="_method"></param>
        /// <param name="_rank"></param>
        public ExceptionIniFile(string message, Exception ex, string _method, ExRank _rank)
            : base(message, ex)
        {
            Rank = _rank;
            Method = _method;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="_method"></param>
        /// <param name="_rank"></param>
        /// <param name="_extype"></param>
        public ExceptionIniFile(string message, Exception ex, string _method, ExRank _rank, ExceptionType _extype)
            : base(message, ex)
        {
            Rank = _rank;
            ExType = _extype;
            Method = _method;
        }

        /// <summary>
        /// 异常发生函数
        /// </summary>
        public string method = null;
        public string Method
        {
            get { return method; }
            set
            {
                method = value;
            }
        }

        /// <summary>
        /// 异常等级
        /// </summary>
        public ExRank rank = ExRank.General;
        public ExRank Rank
        {
            get { return rank; }
            set
            {
                rank = value;
            }
        }

        /// <summary>
        /// 异常类别
        /// </summary>
        public ExceptionType extype = ExceptionType.Register;
        public ExceptionType ExType
        {
            get { return extype; }
            set
            {
                extype = value;
            }
        }
    }

    /// <summary>
    /// 自定义异常ExceptionXml
    /// </summary>
    public class ExceptionXml : Exception
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        public ExceptionXml(string message)
            : base(message)
        { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public ExceptionXml(string message, Exception ex)
            : base(message, ex)
        { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="_method"></param>
        /// <param name="_rank"></param>
        public ExceptionXml(string message, Exception ex, string _method, ExRank _rank)
            : base(message, ex)
        {
            Rank = _rank;
            Method = _method;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="_method"></param>
        /// <param name="_rank"></param>
        /// <param name="_extype"></param>
        public ExceptionXml(string message, Exception ex, string _method, ExRank _rank, ExceptionType _extype)
            : base(message, ex)
        {
            Rank = _rank;
            ExType = _extype;
            Method = _method;
        }

        /// <summary>
        /// 异常发生函数
        /// </summary>
        public string method = null;
        public string Method
        {
            get { return method; }
            set
            {
                method = value;
            }
        }

        /// <summary>
        /// 异常等级
        /// </summary>
        public ExRank rank = ExRank.General;
        public ExRank Rank
        {
            get { return rank; }
            set
            {
                rank = value;
            }
        }

        /// <summary>
        /// 异常类别
        /// </summary>
        public ExceptionType extype = ExceptionType.Register;
        public ExceptionType ExType
        {
            get { return extype; }
            set
            {
                extype = value;
            }
        }
    }
}