using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DealMvc.Timer
{
    public interface ITimer
    {
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        DateTime? StartTime { get; set; }
        /// <summary>
        /// 介绍时间
        /// </summary>
        DateTime? EndTime { get; set; }
        /// <summary>
        /// 循环周期(秒)
        /// </summary>
        int GSeconds { get; set; }
        /// <summary>
        /// 执行方法
        /// </summary>
        void Execute();
    }
}
