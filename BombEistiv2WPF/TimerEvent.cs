using System.Timers;

namespace BombEistiv2WPF
{
    public class TimerEvent
    {
        public object InvolvedObject { get; set; }

        public EventType Type { get; set; }

        public Timer Timer { get; set; }
    }
}