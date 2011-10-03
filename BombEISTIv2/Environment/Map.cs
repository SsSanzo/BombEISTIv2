using System.Collections.Generic;
using System.Linq;

namespace BombEISTIv2.Environment
{
    public class Map
    {
        private readonly List<HardBlock> _listOfHardBlock;
        private readonly List<SoftBlock> _listOfSoftBlock;
        private readonly List<Player> _listOfPlayer;
        private readonly List<Bomb> _listOfBomb;
        private readonly List<Upgrade> _listOfUpgrade;

        public Map()
        {
            _listOfHardBlock = new List<HardBlock>();
            _listOfSoftBlock = new List<SoftBlock>();
            _listOfPlayer = new List<Player>();
            _listOfBomb = new List<Bomb>();
            _listOfUpgrade = new List<Upgrade>();
        }

        public List<Upgrade> ListOfUpgrade
        {
            get { return _listOfUpgrade; }
        }

        public List<Bomb> ListOfBomb
        {
            get { return _listOfBomb; }
        }

        public List<Player> ListOfPlayer
        {
            get { return _listOfPlayer; }
        }

        public List<SoftBlock> ListOfSoftBlock
        {
            get { return _listOfSoftBlock; }
        }

        public List<HardBlock> ListOfHardBlock
        {
            get { return _listOfHardBlock; }
        }

        public void SetHardBlockOnMap()
        {
            for(var i = 1;i<Game.Length;i+=2)
            {
                for(var j = 1;i<Game.Length;j+=2)
                {
                    ListOfHardBlock.Add(new HardBlock(i,j));
                }
            }
        }

        public List<Entity> GetCompleteList()
        {
            var thecompletelist = new List<Entity>();
            thecompletelist.AddRange(ListOfHardBlock);
            thecompletelist.AddRange(ListOfSoftBlock);
            thecompletelist.AddRange(ListOfBomb);
            thecompletelist.AddRange(ListOfUpgrade);
            thecompletelist.AddRange(ListOfPlayer);
            return thecompletelist;
        }

        public Entity GetEntity(int x,int y)
        {
            var list = GetCompleteList();
            return list.FirstOrDefault(c => c.X == x && c.Y == y);
        }


    }
}
