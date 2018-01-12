using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DealMvc.Timer
{
    public class Main
    {
        public static void Do()
        {
            //new NewTimer("首页静态化", null, null, 500, new StaticHomePage());

            new NewTimer("订单类事物处理", null, null, 500, new AboutOrderThings());

        }
    }

    public class NewTimer
    {
        private DateTime? StartTime;
        private DateTime? EndTime;
        private int GSeconds;
        private string Name;
        private ITimer _ITimer;

        private System.Timers.Timer _T;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name">定时器名称</param>
        /// <param name="StartTime">开始时间,null表示网站启动的自动开始</param>
        /// <param name="EndTime">结束时间,null表示不会结束</param>
        /// <param name="GSeconds">执行间隔时间(秒)</param>
        /// <param name="_ITimer">对象</param>
        public NewTimer(string Name, DateTime? StartTime, DateTime? EndTime, int GSeconds, ITimer _ITimer)
        {
            try
            {
                this.Name = Name;
                this.StartTime = StartTime;
                this.EndTime = EndTime;
                this.GSeconds = GSeconds;
                this._ITimer = _ITimer;

                _T = new System.Timers.Timer();
                _T.Interval = this.GSeconds * 1000;
                _T.Elapsed += new System.Timers.ElapsedEventHandler(_T_Elapsed);
                _T.Start();
            }
            catch { }
        }

        void _T_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                StartTime = StartTime ?? new DateTime(1900, 1, 1);
                EndTime = EndTime ?? new DateTime(2020, 1, 1);

                if ((DateTime.Now - (DateTime)StartTime).TotalSeconds >= 0 && (DateTime.Now - (DateTime)EndTime).TotalSeconds <= 0)
                {
                    _ITimer.Execute();
                }
                else
                    _T.Stop();
            }
            catch { }
        }
    }
}
