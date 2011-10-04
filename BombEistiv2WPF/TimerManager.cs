using System;
using System.Collections.Generic;
using System.Timers;
using System.Linq;
using System.Text;

namespace BombEistiv2WPF
{
    class TimerManager
    {
        private List<Timer> _timers;
        private int _lastId;
        private static TimerManager _this;

        private TimerManager()
        {
            _timers = new List<Timer>();
            _lastId = 1;
        }

        public static TimerManager _
        {
            get { return _this ?? (_this = new TimerManager()); }
        }

        public int GetNewTimer()
        {
            var t = new Timer();
            _timers.Add(t);
            return (++_lastId);
        }
    }
}
