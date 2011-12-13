using System;
using System.Timers;
using System.Windows.Threading;
using BombEistiv2WPF.Control;
using BombEistiv2WPF.View;
using BombEistiv2WPF.View.Menu;

namespace BombEistiv2WPF.Environment
{
    public class ClassicGame : Game
    {
        public HardBlock hb;
        public int tour;
        public GameScreen w;

        public ClassicGame(GameScreen wiz = null)
        {
            w = wiz;
            GameParameters._.Type = GameType.Classic;
            TheCurrentMap = new Map();
            TheCurrentMap.SetHardBlockOnMap();
            TheCurrentMap.SetSoftBlockOnMap();
            InitPlayers();
            TimerManager._.AddNewTimer(true, 1000, false, null, ChangeTime);
            TimerManager._.AddNewTimer(false, GameParameters._.GameTime * 1000 - 45000, false, null, HurryUp);
            
        }

        public void Start()
        {
            TimerManager._.Start();
        }

        public void ChangeTime(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            var t = (Timer) sender;
            Texture._.Mw.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => InGameMenu._.ChangeLabelTime(t)));
            
            
        }

        public void InsertEndingBlock()
        {
            HardBlock entity = null;
            if(hb == null)
            {
                entity = new HardBlock(0,0);
                TheCurrentMap.DestroyEverythingHere(entity.X,entity.Y);
            }else
            {
                if (hb.Y == tour && hb.X != Length - 1 - tour)
                {
                    entity = new HardBlock(hb.X + 1, hb.Y);
                }
                else if (hb.X == Length - 1 - tour && hb.Y != Length - 1 - tour)
                {
                    entity = new HardBlock(hb.X, hb.Y + 1);
                }
                else if (hb.Y == Length - 1 - tour && hb.X != tour)
                {
                    entity = new HardBlock(hb.X - 1, hb.Y);
                }
                else if (hb.X == tour && hb.Y != tour + 1)
                {
                    entity = new HardBlock(hb.X, hb.Y - 1);
                    
                }
                if (entity.X == tour && entity.Y == tour + 1)
                {
                    tour++;
                }
                TheCurrentMap.DestroyEverythingHere(entity.X, entity.Y);
            }
            entity.Source = Texture._.TypetextureList["EndBlock"];
            entity.Width = 40;
            entity.Height = 40;
            Texture._.Mw.MainGrid.Children.Add(entity);
            TheCurrentMap.ListOfHardBlock.Add(entity);
            hb = entity;
        }

        public void InsertEndingBlockThread(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            if(tour < 4)
            {
                Texture._.Mw.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(InsertEndingBlock));
            }else
            {
                var t = (Timer) sender;
                t.AutoReset = false;
            }
           
        }

        public void HurryUp(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            Texture._.Mw.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => PlaySound._.Stop("Game")));
            Texture._.Mw.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => PlaySound._.TypeMusicList["Quick"].Play()));
            PlaySound._.TypeSoundList["Hurry"].Play();
            TimerManager._.AddNewTimer(false, 45000, true, null, EndOfTheGame);
            TimerManager._.AddNewTimer(true, 15, true, null, w.Hurry);
            hb = null;
            tour = 0;
            TimerManager._.AddNewTimer(true,80,true,null, InsertEndingBlockThread);
        }

        public void EndOfTheGame(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            TimerManager._.Stop();
            var l = TheCurrentMap.ListOfPlayer.FindAll(c => c.Lives >= 0);
            foreach (var p in l)
            {
                Score._.Survived(p);
            }
            Texture._.Mw.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(Ending));
        }

        public void EndOfTheGameUntimed()
        {
            TimerManager._.Stop();
            var l = TheCurrentMap.ListOfPlayer.FindAll(c => c.Lives >= 0);
            foreach (var p in l)
            {
                Score._.Survived(p);
            }
            Texture._.Mw.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(Ending));
        }

        public void Ending()
        {
            w.Ending();
        }
    }
}