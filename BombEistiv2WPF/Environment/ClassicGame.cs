using System.Timers;

namespace BombEistiv2WPF.Environment
{
    public class ClassicGame : Game
    {

        public ClassicGame()
        {
            GameParameters._.Type = GameType.Classic;
            Map = new Map();
            Map.SetHardBlockOnMap();
            Map.SetSoftBlockOnMap();
            InitPlayers(GameParameters._.PlayerCount);
            var timer = TimerManager._.GetNewTimer(false, GameParameters._.GameTime - 30000);
            TimerManager._.GetTimer(timer).Elapsed += HurryUp;
            Start();
        }

        public Map Map { get; private set; }

        public void Start()
        {
            TimerManager._.Start();
        }

        public void HurryUp(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            var e = TimerManager._.GetNewTimer(false, 30000, true);
            TimerManager._.GetTimer(e).Elapsed += EndOfTheGame;
        }

        public void EndOfTheGame(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            TimerManager._.Stop();
        }
    }
}
