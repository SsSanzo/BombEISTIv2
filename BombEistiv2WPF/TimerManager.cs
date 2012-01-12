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
            if (timerEvent == null) { timerEvent = new TimerEvent(); t.Elapsed += Destroy; }
            else { t.Elapsed += Elapsed;}
            timerEvent.Interval = delay;
            timerEvent.StartTime = DateTime.Now;
            if (e != null){ t.Elapsed += e; }
            timerEvent.Timer = t;
            _timers.Add(timerEvent);
            if (autoStart) t.Start();
        }

        private void Elapsed(object sender, EventArgs eventArgs)
        {
            
            try
            {
                //var copy = new List<TimerEvent>();
                //copy.AddRange(_timers);
                
                var copy = new List<TimerEvent>();
                for (var i = _timers.Count - 1; i >= 0; i--)
                {
                    //index = i;
                    if (i <= _timers.Count)
                    {
                        copy.Add(_timers[i]);
                    }
                }
                var v = copy.FirstOrDefault(c => c != null && c.Timer == (Timer)sender);
                if(v != null)
                {
                    _game.EventManager(v);
                    Destroy(sender, eventArgs);
                }
            }catch
            {
                var t = (Timer) sender;
                t.Stop();
                t.Interval = 20;
                t.Start();
               //Console.WriteLine("[" + DateTime.Now + "] - Error addrange fail");
            }
        }

        public void Start()
        {
            foreach (var timerEvent in _timers)
            {
                timerEvent.Timer.Start();
            }
        }


        //public void Stop()
        //{
        //    foreach (var timerEvent in _timers.Where(timerEvent => timerEvent != null))
        //    {
        //        timerEvent.Timer.Stop();
        //    }
        //}

        public void Stop()
        {
            for (var i = _timers.Count - 1; i > 0; i--)
            {
                var v = _timers[i];
                if (v != null)
                {
                    v.Timer.Stop();
                }
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

        public void Destroy(object sender, EventArgs eventArgs)
        {
            var s = (Timer)sender;
            if (s.AutoReset)
            {
                Dispatcher.CurrentDispatcher.Invoke(
                    DispatcherPriority.Normal,
                    (Action)(() =>
                    {
                        try
                        {
                            var copy = new List<TimerEvent>();
                            copy.AddRange(_timers);
                            var t = copy.FirstOrDefault(c => c != null && c.Timer == s);
                            if (t != null)
                            {
                                t.StartTime = DateTime.Now;
                                if (!t.ReinitInterval) return;
                                t.Timer.Interval = t.Interval;
                                t.ReinitInterval = false;
                            }
                        }catch{}
                    }
                             )
                );
            }
            else
            {
                s.Stop();
                Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal,
                                                    (Action) (() => removeTime(s)));
            
                
                
            }
        }
        public void removeTime(Timer t)
        {
            bool fail = false;
            int index = 0;
            do
            {
                try
                {
                    var copy = new List<TimerEvent>();
                    for(var i=_timers.Count - 1;i>=0;i--)
                    {
                        //index = i;
                        if(i <= _timers.Count)
                        {
                            copy.Add(_timers[i]);
                        }
                        
                    }
                    var a = copy.FirstOrDefault(c => c != null && c.Timer == t);
                    _timers.Remove(a);
                    fail = false;
                }
                catch(Exception e)
                {
                    //t.Close();
                    fail = true;
                    //Console.WriteLine("[" + DateTime.Now + "] - Error removetime fail : " + t.GetHashCode() + " // Ex : " + e.Message + " // index : " + index + " // " + _timers.Count);
                    //var a2 = _timers.Find(c => c != null && c.Timer == t);
                    //_timers.Remove(a2);
                }
            } while (fail);
            t.Close();


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