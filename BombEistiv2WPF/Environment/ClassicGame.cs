using System;
using System.Timers;
using System.Windows.Threading;
using BombEistiv2WPF.View;

namespace BombEistiv2WPF.Environment
{
    public class ClassicGame : Game
    {
        public HardBlock hb;
        public int tour;

        public ClassicGame()
        {
            GameParameters._.Type = GameType.Classic;
            TheCurrentMap = new Map();
            TheCurrentMap.SetHardBlockOnMap();
            TheCurrentMap.SetSoftBlockOnMap();
            InitPlayers(GameParameters._.PlayerCount);
            TimerManager._.AddNewTimer(false, GameParameters._.GameTime * 1000 - 45000, false, null, HurryUp);
            TimerManager._.AddNewTimer(true,1000,true,null,ChangeTime);
            Start();
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
            TimerManager._.AddNewTimer(false, 45000, true, null, EndOfTheGame);
            hb = null;
            tour = 0;
            TimerManager._.AddNewTimer(true,80,true,null, InsertEndingBlockThread);
        }

        public void EndOfTheGame(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            TimerManager._.Stop();
        }
    }
}