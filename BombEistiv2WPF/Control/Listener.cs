using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using BombEistiv2WPF.Environment;
using Timer = System.Timers.Timer;


namespace BombEistiv2WPF.Control
{
    public class Listener
    {
        private KeyAction ka;
        private bool modeMenu;
        private bool stop;
        private static Listener _this;
        private List<string> incoming;
        private List<string> pushed;
        private List<string> pulled; 
        private Game _gameInProgress;

        private Listener(bool ModeMenu)
        {
            ka = KeyAction._;
            modeMenu = ModeMenu;
            stop = false;
            incoming = new List<string>();
            pushed = new List<string>();
            pulled = new List<string>();
        }

        public List<string> Incoming
        {
            get { return incoming; }
        }

        public List<string> Pushed
        {
            get { return pushed; }
        }

        public List<string> Pulled
        {
            get { return pulled; }
        }

        public bool ModeMenu
        {
            get { return modeMenu; }
            set { modeMenu = value;  }
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
            TimerManager._.AddNewTimer(true,1,true,null,Move);
        }

        public void StopTimers()
        {
            stop = true;
        }


        private void Move(object sender, ElapsedEventArgs elapsedEventArgs)
        {
                if (stop)
                {
                    var t = (Timer) sender;
                    t.Dispose();
                }
                else
                {
                    //if (Incoming.Count != 0)
                    //{
                    //    foreach (var i in Incoming)
                    //    {
                    //        Pushed.Add(i);
                    //    }
                    //    Incoming.Clear();
                    //}
                    var l = new List<string>();
                    l.AddRange(Pushed);
                    foreach (var s in l)
                    {
                        var splitted = s.Split('_');
                        var thePlayer =
                            GameInProgress.TheCurrentMap.ListOfPlayer.Find(t => t.Id == Convert.ToInt32(splitted[0]));
                        Movement.Move(splitted[1], thePlayer);
                    }
                    if (Pulled.Count != 0)
                    {
                        foreach (var p in Pulled)
                        {
                            Pushed.Remove(p);
                        }
                        Pulled.Clear();
                    }
                }
            
        }

        public void EventKey(Key k)
        {
            if(modeMenu)
            {
                // ?
            }else
            {
                if (ka.KeysPlayer.ContainsKey(k) && !Pushed.Contains(ka.KeysPlayer[k]))
                {
                    //var id = ka.KeysPlayer[k].Split('_')[0];
                    //Pulled.AddRange(Pushed.FindAll(t => t.StartsWith(id)));
                    //Pulled.AddRange(Incoming.FindAll(t => t.StartsWith(id)));
                    //Incoming.Add(ka.KeysPlayer[k]);
                    Pushed.Add(ka.KeysPlayer[k]);
                }
                
            }
        }

        public void ReleaseKey(Key k)
        {
            if (modeMenu)
            {
                // ?
            }
            else
            {
                if (ka.KeysPlayer.ContainsKey(k))
                {
                    Pulled.Add(ka.KeysPlayer[k]);
                    //Pushed.Add(ka.KeysPlayer[k]);
                }
            }

        }
    }
}
