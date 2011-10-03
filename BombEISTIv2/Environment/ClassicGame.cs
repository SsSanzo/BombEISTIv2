namespace BombEISTIv2.Environment
{
    public class ClassicGame : BombEISTIv2.Environment.Game
    {
        public ClassicGame()
        {
            TheCurrentMap = new Map();
            TheCurrentMap.SetHardBlockOnMap();
            TheCurrentMap.SetSoftBlockOnMap();
            InitPlayers(numberOfPlayer);
        }
    }
}
