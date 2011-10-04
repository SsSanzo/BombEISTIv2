using System;
using System.Timers;

namespace BombEISTIv2.Environment
{
    public class ClassicGame : BombEISTIv2.Environment.Game
    {

        private GameParameters gp;
        private Timer timer;

        public ClassicGame()
        {
            gp = GameParameters.Parameters;
            gp.Type = GameType.Classic;
            TheCurrentMap = new Map();
            TheCurrentMap.SetHardBlockOnMap();
            TheCurrentMap.SetSoftBlockOnMap(gp);
            InitPlayers(gp.NumberOfPlayer);
            timer = new Timer();
            timer.Elapsed += HurryUp;
            timer.AutoReset = false;
            timer.Interval = gp.GameTime - 30000;
            Start();
        }


        public void Start()
        {
            timer.Start();
        }

        public void HurryUp(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            timer.Stop();
            timer.Elapsed += EndOfTheGame;
            timer.Interval = 30000;
            timer.Start();
        }

        public void EndOfTheGame(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            timer.Stop();
        }
    }
}
