using System.Timers;

namespace BombEistiv2WPF.Environment
{
    public class ClassicGame : Game
    {

        private GameParameters gp;
        private Timer _timer;

        public ClassicGame()
        {
            GameParameters._.Type = GameType.Classic;
            Map = new Map();
            Map.SetHardBlockOnMap();
            Map.SetSoftBlockOnMap();
            InitPlayers(GameParameters._.PlayerCount);
            _timer = new Timer();
            _timer.Elapsed += HurryUp;
            _timer.AutoReset = false;
            _timer.Interval = GameParameters._.GameTime - 30000;
            Start();
        }

        public Map Map { get; private set; }

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
