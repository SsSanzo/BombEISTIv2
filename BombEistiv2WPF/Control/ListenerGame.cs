using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using BombEistiv2WPF.Environment;
using BombEistiv2WPF.View;
using Timer = System.Timers.Timer;


namespace BombEistiv2WPF.Control
{
    public class ListenerGame
    {
        private KeyAction ka;
        private bool stop;
        private static ListenerGame _this;
        //private List<string> incoming;
        private List<string> pushed;
        private List<string> pulled;
        private Game _gameInProgress;

        private ListenerGame()
        {
            ka = KeyAction._;
            stop = false;
            //incoming = new List<string>();
            pushed = new List<string>();
            pulled = new List<string>();
        }

        //public List<string> Incoming
        //{
        //    get { return incoming; }
        //}

        public List<string> Pushed
        {
            get { return pushed; }
        }

        public List<string> Pulled
        {
            get { return pulled; }
        }


        public Game GameInProgress
        {
            get { return _gameInProgress; }
            set { _gameInProgress = value; }
        }

        public static ListenerGame _
        {
            get { return _this ?? (_this = new ListenerGame()); }
        }

        public void StartTimers(GameType g = GameType.Classic)
        {
            if(g != GameType.Crazy)
            {
                TimerManager._.AddNewTimer(true, 15, true, null, Move);
            }else
            {
                TimerManager._.AddNewTimer(true, 15, true, null, MoveCrazy);
            }
            
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
                    try
                    {
                        l.AddRange(Pushed);
                    }catch
                    {
                        
                    }
                    foreach (var s in l)
                    {
                        if(s != null)
                        {
                            var splitted = s.Split('_');
                            var thePlayer =
                                GameInProgress.TheCurrentMap.ListOfPlayer.Find(t => t.Id == Convert.ToInt32(splitted[0]));
                            if(thePlayer != null)
                            {
                                    Movement.Move(splitted[1], thePlayer);
                            }
                            
                        }
                        
                    }
                    Movement.ChangeFace(GameInProgress.TheCurrentMap.ListOfPlayer);
                    var c = Pulled.Count;
                    if (c != 0)
                    {
                        try
                        {
                            for (var i = c - 1; i >= 0; i--)
                            {
                                Pushed.Remove(Pulled[i]);
                                pulled.RemoveAt(i);
                            }
                        }catch{}
                    }
                }
            
        }

        private void MoveCrazy(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            if (stop)
            {
                var t = (Timer)sender;
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
                try
                {
                    l.AddRange(Pushed);
                }
                catch
                {

                }
                foreach (var s in l)
                {
                    if (s != null)
                    {
                        var splitted = s.Split('_');
                        var thePlayer =
                            GameInProgress.TheCurrentMap.ListOfPlayer.Find(t => t.Id == Convert.ToInt32(splitted[0]));
                        if (thePlayer != null)
                        {
                            if (!thePlayer.Freeze && thePlayer.MyBombTeleguide == null)
                            {
                                Movement.Move(splitted[1], thePlayer);
                            }
                            else if (thePlayer.MyBombTeleguide != null)
                            {
                                Movement.MoveBomb(splitted[1], thePlayer);
                            }
                        }

                    }

                }
                Movement.ChangeFace(GameInProgress.TheCurrentMap.ListOfPlayer);
                var c = Pulled.Count;
                if (c != 0)
                {
                    try
                    {
                        for (var i = c - 1; i >= 0; i--)
                        {
                            Pushed.Remove(Pulled[i]);
                            pulled.RemoveAt(i);
                        }
                    }
                    catch { }
                }
            }

        }

        public void EventKey(Key k)
        {
                if (ka.KeysPlayer.ContainsKey(k) && !Pushed.Contains(ka.KeysPlayer[k]))
                {
                    //var id = ka.KeysPlayer[k].Split('_')[0];
                    //Pulled.AddRange(Pushed.FindAll(t => t.StartsWith(id)));
                    //Pulled.AddRange(Incoming.FindAll(t => t.StartsWith(id)));
                    //Incoming.Add(ka.KeysPlayer[k]);
                    if (ka.KeysPlayer[k].Split('_')[1] != "None" && ka.KeysPlayer[k].Split('_')[1] != "Switch") { Pushed.Add(ka.KeysPlayer[k]); }
                    else
                    {
                        var thePlayer =
                            GameInProgress.TheCurrentMap.ListOfPlayer.Find(t => t.Id == Convert.ToInt32(ka.KeysPlayer[k].Split('_')[0]));
                        if(thePlayer != null)
                        {
                            if(GameParameters._.Type == GameType.Crazy)
                            {
                                if (thePlayer.BombSecond == BombType.Move && ka.KeysPlayer[k].Split('_')[1] == "Switch")
                                {
                                    Movement.PutMultipleBomb(thePlayer);
                                }else if(thePlayer.MyBombTeleguide == null)
                                {
                                    var e = Movement.PutABomb(thePlayer, ((ka.KeysPlayer[k].Split('_')[1] == "Switch")));
                                    if (e != null)
                                    {
                                        if (e.Type != BombType.Fly)
                                        {
                                            GameInProgress.TheCurrentMap.ListOfBomb.Add(e);
                                        }
                                        Texture._.InsertTextureEntity(e);
                                    }
                                }
                                else
                                {
                                    thePlayer.MyBombTeleguide.Explode(GameInProgress);
                                    thePlayer.MyBombTeleguide = null;
                                    thePlayer.beAvaliableLag();
                                }
                            }
                            else if (ka.KeysPlayer[k].Split('_')[1] == "None")
                            {
                                var e = Movement.PutABomb(thePlayer, false);
                                if (e != null)
                                {
                                    
                                    GameInProgress.TheCurrentMap.ListOfBomb.Add(e);
                                    Texture._.InsertTextureEntity(e);
                                }
                            }
                            
                        }
                    }
                }
                
            
        }

        public void ReleaseKey(Key k)
        {

                if (ka.KeysPlayer.ContainsKey(k))
                {
                    Pulled.Add(ka.KeysPlayer[k]);
                    //Pushed.Add(ka.KeysPlayer[k]);
                }
            

        }

        public void EmptyTheCache()
        {
            pulled.Clear();
            pushed.Clear();
        }
    }
}
