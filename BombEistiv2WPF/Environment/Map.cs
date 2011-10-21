using System;
using System.Collections.Generic;
using System.Linq;

namespace BombEistiv2WPF.Environment
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
                for(var j = 1;j<Game.Length;j+=2)
                {
                    ListOfHardBlock.Add(new HardBlock(i,j));
                }
            }
        }

        public void SetSoftBlockOnMap()
        {
            var numberOfCaseEmpty = (Game.Length*Game.Length) - ListOfHardBlock.Count;
            if(numberOfCaseEmpty < 0)
            {
                throw new Exception("Empty case are negative");
            }
            var allupgrades = GameParameters._.GetAllUpgrades();
            if (allupgrades.Count > GameParameters._.SoftBlocCount)
            {
                throw new Exception("Too much upgrades");
            }
            var theListOfEntityEmptry = new List<Entity>();
            for(var i = 0;i<Game.Length;i++)
            {
                for(var j=0;j<Game.Length;j++)
                {
                   if(!(((i <= 1 || i >= Game.Length - 2) && (j <= 1 || j >= Game.Length - 2))))
                   {
                       theListOfEntityEmptry.Add(new HardBlock(i,j));
                   }
                }
            }

            for (var i = numberOfCaseEmpty; i > 0; i--)
            {
                var rand = new Random();
                var selectionPick = rand.Next(theListOfEntityEmptry.Count);
                var thePick = theListOfEntityEmptry.ElementAt(selectionPick);
                if(allupgrades.Count > 0)
                {
                    var selectionUp = rand.Next(allupgrades.Count);
                    var theUp = allupgrades.ElementAt(selectionUp);
                    ListOfSoftBlock.Add(new SoftBlock(thePick.X, thePick.Y, new Upgrade(thePick.X, thePick.Y, theUp.Key)));
                    var key = theUp.Key;
                    var value = theUp.Value - 1;
                    allupgrades.Remove(key);
                    if(value != 0)
                    {
                        allupgrades.Add(key, value);
                    }
                }else
                {
                    ListOfSoftBlock.Add(new SoftBlock(thePick.X, thePick.Y));
                }
                theListOfEntityEmptry.RemoveAt(selectionPick);
                
            }
        }

        

        public Upgrade PickupUpgrade(int x, int y)
        {
            var u = ListOfUpgrade.FirstOrDefault(c => c.X == x && c.Y == y);
            ListOfUpgrade.Remove(u);
            return u;
        }

        public bool PickupUpgrade(Upgrade upgrade)
        {
            return ListOfUpgrade.Remove(upgrade);
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
