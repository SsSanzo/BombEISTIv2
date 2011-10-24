using System;
using System.Collections.Generic;
using System.Timers;

namespace BombEistiv2WPF
{
    class TimerManager
    {
        private readonly List<Timer> _timers;
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

        public void Start()
        {
            foreach (var timer in _timers)
            {
                timer.Start();
            }
        }

        public void Stop()
        {
            foreach (var timer in _timers)
            {
                timer.Stop();
            }
        }

        public int GetNewTimer(bool autoReset, int interval, bool startNow = false)
        {
            var t = new Timer {Interval = interval, AutoReset = autoReset};
            _timers.Add(t);
            if(startNow){t.Start();}
            return (++_lastId);
        }

        public Timer GetTimer(int id)
        {
            Timer e;
            try {e = _timers[id];}
            catch(Exception ex) {return null;}
            return e;
        }

        public void EventDeath(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            var e = (Timer) sender;
            _timers.Remove(e);
        }
    }
}
