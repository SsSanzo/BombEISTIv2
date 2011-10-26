using System.Timers;

namespace BombEistiv2WPF.Environment
{
    public class ClassicGame : Game
    {

        public ClassicGame()
        {
            GameParameters._.Type = GameType.Classic;
            TheCurrentMap = new Map();
            TheCurrentMap.SetHardBlockOnMap();
            TheCurrentMap.SetSoftBlockOnMap();
            InitPlayers(GameParameters._.PlayerCount);
            TimerManager._.AddNewTimer(false, GameParameters._.GameTime * 1000 - 30000, false, null, HurryUp);
            Start();
        }

        public void Start()
        {
            TimerManager._.Start();
        }

        public void HurryUp(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            TimerManager._.AddNewTimer(false, 30000, true, null, EndOfTheGame);
        }

        public void EndOfTheGame(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            TimerManager._.Stop();
        }
    }
}