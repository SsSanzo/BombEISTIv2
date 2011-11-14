using System;
using System.Collections.Generic;
using System.Linq;
using BombEistiv2WPF.View;

namespace BombEistiv2WPF.Environment
{
    public class Map
    {
        private readonly List<HardBlock> _listOfHardBlock;
        private readonly List<SoftBlock> _listOfSoftBlock;
        private readonly List<Player> _listOfPlayer;
        private readonly List<Bomb> _listOfBomb;
        private readonly List<Upgrade> _listOfUpgrade;
        private readonly List<EntityOfDeath> _listOfEntityOfDeath;

        public Map()
        {
            _listOfHardBlock = new List<HardBlock>();
            _listOfSoftBlock = new List<SoftBlock>();
            _listOfPlayer = new List<Player>();
            _listOfBomb = new List<Bomb>();
            _listOfUpgrade = new List<Upgrade>();
            _listOfEntityOfDeath = new List<EntityOfDeath>();
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

        public List<EntityOfDeath> ListOfEntityOfDeath
        {
            get { return _listOfEntityOfDeath; }
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
                   if (!(((i <= 1 || i >= Game.Length - 2) && (j <= 1 || j >= Game.Length - 2))))
                   {
                       if((i%2 == 0) || (j%2 == 0))
                       {
                           theListOfEntityEmptry.Add(new HardBlock(i,j));
                       }
                       
                   }
                }
            }

            var rand = new Random();
            for (var i = GameParameters._.SoftBlocCount; i > 0; i--)
            {
                
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

        public List<Entity> GetCompleteList(bool withoutplayer = false)
        {
            var thecompletelist = new List<Entity>();
            thecompletelist.AddRange(ListOfHardBlock);
            thecompletelist.AddRange(ListOfSoftBlock);
            thecompletelist.AddRange(ListOfBomb);
            thecompletelist.AddRange(ListOfUpgrade);
            if(!withoutplayer){thecompletelist.AddRange(ListOfPlayer);}
            return thecompletelist;
        }

        public Entity GetEntity(int x,int y)
        {
            var list = GetCompleteList();
            return list.FirstOrDefault(c => c.X == x && c.Y == y);
        }

        public EntityOfDeath GetEntityOfDeath(int x, int y)
        {
            var list = ListOfEntityOfDeath;
            return list.FirstOrDefault(c => c.X == x && c.Y == y);
        }

        public Entity GetBomb(int x, int y)
        {
            return ListOfBomb.FirstOrDefault(c => c.X == x && c.Y == y);
        }

        public void DestroyEverythingHere(int x, int y)
        {
            var p = new List<Player>();
            p.AddRange(ListOfPlayer.Where(c => c.X == x && c.Y == y));
            if (p.Count != 0)
            {
                foreach (var player in p)
                {
                    player.DieFinal();
                    Texture._.DeleteTextureEntity(player);
                    ListOfPlayer.Remove(player);
                }
            }
            var b = ListOfBomb.FirstOrDefault(c => c.X == x && c.Y == y);
            if(b != null)
            {
                b.Explode(Texture._.Mw.GameInProgress);
                ListOfBomb.Remove(b);
            }
            var h = ListOfHardBlock.FirstOrDefault(c => c.X == x && c.Y == y);
            if (h != null)
            {
                ListOfHardBlock.Remove(h);
            }
            var s = ListOfSoftBlock.FirstOrDefault(c => c.X == x && c.Y == y);
            if (s != null)
            {
                ListOfSoftBlock.Remove(s);
            }
            var u = ListOfUpgrade.FirstOrDefault(c => c.X == x && c.Y == y);
            if (u != null)
            {
                ListOfUpgrade.Remove(u);
            }
        }


    }
}
