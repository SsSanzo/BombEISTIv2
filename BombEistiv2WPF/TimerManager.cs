using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows.Threading;
using BombEistiv2WPF.Environment;

namespace BombEistiv2WPF
{
    class TimerManager
    {
        private readonly List<TimerEvent> _timers;
        private int _lastId;
        private static TimerManager _this;
        private Game _game;

        private TimerManager()
        {
            _timers = new List<TimerEvent>();
            _lastId = 1;
        }

        public static TimerManager _
        {
            get { return _this ?? (_this = new TimerManager()); }
        }

        public Game Game
        {
            set { if (value != null) _game = value; }
        }

        public int GetNewTimer(bool autoReset, int delay, bool autoStart, TimerEvent timerEvent)
        {
            var t = new Timer { AutoReset = autoReset, Interval = delay };
            timerEvent.Timer = t;
            if (!autoReset) t.Elapsed += Elapsed;
            t.Disposed += Destroy;
            _timers.Add(timerEvent);
            if (autoReset) t.Start();
            return (++_lastId);
        }

        public Timer GetTimer(int id)
        {
            return _timers[id].Timer;
        }

        private void Elapsed(object sender, EventArgs eventArgs)
        {
            _game.EventManager(_timers.Find(c => c.Timer == (Timer)sender));
        }

        private void Destroy(object sender, EventArgs eventArgs)
        {
            Elapsed(sender, eventArgs);
            Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, (Action)(() => _timers.RemoveAll(c => c.Timer == (Timer)sender)));
        }
    }

    public enum EventType
    {
        BombExplode
    }
}