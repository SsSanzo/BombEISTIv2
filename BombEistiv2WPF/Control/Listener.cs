using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows.Forms;
using BombEistiv2WPF.Environment;
using Timer = System.Timers.Timer;
using System.Xml.Linq;

namespace BombEistiv2WPF.Control
{
    public class Listener
    {
        private KeyAction ka;
        private bool modeMenu;
        private bool stop;
        private static Listener _this;
        private List<string> pushed;
        private Game _gameInProgress;

        private Listener(bool ModeMenu)
        {
            ka = KeyAction._;
            modeMenu = ModeMenu;
            stop = false;
            pushed = new List<string>();
        }

        public List<string> Pushed
        {
            get { return pushed; }
        }

        public Game GameInProgress
        {
            get { return _gameInProgress; }
            set { _gameInProgress = value; }
        }

        public static Listener _
        {
            get { return _this ?? (_this = new Listener(true)); }
        }

        public void StartTimers()
        {
            TimerManager._.AddNewTimer(true,15,true,null,Move);
        }

        public void StopTimers()
        {
            stop = true;
        }


        public void Move(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            if(stop || GameInProgress == null)
            {
                var t = (Timer) sender;
                t.Dispose();
            }else
            {
                foreach (var s in Pushed)
                {
                    var splitted = s.Split('_');
                    var thePlayer = GameInProgress.TheCurrentMap.ListOfPlayer.Find(t => t.Id == Convert.ToInt32(splitted[0]));
                    Movement.Move(splitted[1], thePlayer);
                }
            }
        }

        public void EventKey(Keys k)
        {
            if(modeMenu)
            {
                // ?
            }else
            {
                var id = ka.KeysPlayer[k].Split('_')[0];
                Pushed.RemoveAll(t => t.StartsWith(id));
                Pushed.Add(ka.KeysPlayer[k]);
            }
        }

        public void ReleaseKey(Keys k)
        {
            Pushed.Remove(ka.KeysPlayer[k]);
        }
    }
}
