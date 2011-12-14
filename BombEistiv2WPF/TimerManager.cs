using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Windows.Threading;
using BombEistiv2WPF.Environment;

namespace BombEistiv2WPF
{
    internal class TimerManager
    {
        private readonly List<TimerEvent> _timers;
        private static TimerManager _this;
        private Game _game;
        private bool _pause = false;

        private TimerManager()
        {
            _timers = new List<TimerEvent>();
        }

        public static TimerManager _
        {
            get { return _this ?? (_this = new TimerManager()); }
        }

        public Game Game
        {
            set
            {
                if (value == null) return;
                foreach (var timerEvent in _timers.Where(timerEvent => timerEvent.Type != EventType.None))
                {
                    timerEvent.Timer.Stop();
                    timerEvent.Timer.Close();
                    _timers.Remove(timerEvent);
                }
                _game = value;
            }

            get { return _game; }
        }

        public bool Pause
        {
            get { return _pause; }
            set
            {
                if (value == _pause) return;
                _pause = value;
                if (value) // pause
                {
                    foreach (var timerEvent in _timers)
                    {
                        timerEvent.Timer.Stop();
                        var d = DateTime.Now - timerEvent.StartTime;
                        timerEvent.ReinitInterval = true;
                        timerEvent.Timer.Interval = d.TotalMilliseconds;
                    }
                }
                else // unpause
                {
                    Start();
                }
            }
        }

        public void AddNewTimer(bool autoReset, int delay, bool autoStart = false, TimerEvent timerEvent = null,
                                ElapsedEventHandler e = null)
        {
            var t = new Timer { AutoReset = autoReset, Interval = delay };
            if (timerEvent == null){ timerEvent = new TimerEvent();}
            else{ t.Elapsed += Elapsed;}
            timerEvent.Interval = delay;
            timerEvent.StartTime = DateTime.Now;
            if (e != null) t.Elapsed += e;
            t.Elapsed += Destroy;
            timerEvent.Timer = t;
            _timers.Add(timerEvent);
            if (autoStart) t.Start();
        }

        private void Elapsed(object sender, EventArgs eventArgs)
        {
            _game.EventManager(_timers.Find(c => c.Timer == (Timer)sender));
        }

        public void Start()
        {
            foreach (var timerEvent in _timers)
            {
                timerEvent.Timer.Start();
            }
        }


        public void Stop()
        {
            foreach (var timerEvent in _timers)
            {
                timerEvent.Timer.Stop();
            }
        }

        public void Reset()
        {
            foreach (var timerEvent in _timers)
            {
                if(timerEvent != null)
                {
                    timerEvent.Timer.Stop();
                    timerEvent.Timer.Close();
                }
            }
            _timers.Clear();
        }

        private void Destroy(object sender, EventArgs eventArgs)
        {
            var s = (Timer)sender;
            if (s.AutoReset)
            {
                Dispatcher.CurrentDispatcher.Invoke(
                    DispatcherPriority.Normal,
                    (Action)(() =>
                    {
                        var t = _timers.Find(c => c != null && c.Timer == s);
                        if(t != null){
                        t.StartTime = DateTime.Now;
                        if (!t.ReinitInterval) return;
                        t.Timer.Interval = t.Interval;
                        t.ReinitInterval = false;
                        }
                    }
                             )
                );
            }
            else
            {
                Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal,
                                                    (Action) (() => removeTime(s)));
            
                s.Stop();
                s.Close();
            }
        }
        public void removeTime(Timer t)
        {
            var a = _timers.Find(c => c != null && c.Timer == t);
            try
            {
                _timers.Remove(a);
            }catch
            {
                var a2 = _timers.Find(c => c != null && c.Timer == t);
                _timers.Remove(a2);
            }
            
        }
    }

    

    public enum EventType
    {
        None,
        BombExplode,
        BombMove,
        Malediction
    }
}