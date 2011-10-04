using System;
using System.Timers;

namespace BombEISTIv2.Environment
{
    public class ClassicGame : BombEISTIv2.Environment.Game
    {

        private GameParameters gp;
        private Timer _timer;

        public ClassicGame()
        {
            gp = GameParameters.Parameters;
            gp.Type = GameType.Classic;
            TheCurrentMap = new Map();
            TheCurrentMap.SetHardBlockOnMap();
            TheCurrentMap.SetSoftBlockOnMap(gp);
            InitPlayers(gp.NumberOfPlayer);
            _timer = new Timer();
            _timer.Elapsed += HurryUp;
            _timer.AutoReset = false;
            _timer.Interval = gp.GameTime - 30000;
            Start();
        }


        public void Start()
        {
            _timer.Start();
        }

        public void HurryUp(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            _timer.Stop();
            _timer.Elapsed += EndOfTheGame;
            _timer.Interval = 30000;
            _timer.Start();
        }

        public void EndOfTheGame(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            _timer.Stop();
        }
    }
}
