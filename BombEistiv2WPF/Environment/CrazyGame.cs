using System;
using System.Timers;
using System.Windows.Threading;
using BombEistiv2WPF.View;
using BombEistiv2WPF.View.Menu;

namespace BombEistiv2WPF.Environment
{
    public class CrazyGame : Game
    {
        public HardBlock hb;
        public int tour;
        public GameScreen w;
        public bool theGameIsOver;

        public CrazyGame(GameScreen wiz = null)
        {
            w = wiz;
            theGameIsOver = false;
            GameParameters._.Type = GameType.Crazy;
            TheCurrentMap = new Map();
            TheCurrentMap.SetHardBlockOnMap();
            TheCurrentMap.SetSoftBlockOnMapCrazy();
            InitPlayersCrazy();
            TimerManager._.AddNewTimer(true, 1000, false, null, ChangeTime);
            TimerManager._.AddNewTimer(false, GameParameters._.GameTime * 1000, false, null, EndOfTheGame);
            
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


        public void EndOfTheGame(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            if (!theGameIsOver)
            {
                theGameIsOver = true;
                TimerManager._.Stop();
                var l = TheCurrentMap.ListOfPlayer.FindAll(c => c.Lives >= 0);
                foreach (var p in l)
                {
                    Score._.Survived(p);
                }
                Texture._.Mw.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(Ending));
            }
        }

        public void EndOfTheGameUntimed()
        {
            if(!theGameIsOver)
            {
                theGameIsOver = true;
                TimerManager._.Stop();
                var l = TheCurrentMap.ListOfPlayer.FindAll(c => c.Lives >= 0);
                foreach (var p in l)
                {
                    Score._.Survived(p);
                }
                Texture._.Mw.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(Ending));
            }
        }

        public void Ending()
        {
            w.Ending();
        }
    }
}
