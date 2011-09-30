using System.Collections.Generic;
using System.Linq;

namespace BombEISTIv2.Environment
{
    public class Bomb : Entity
    {

        private Player _owner;
        private int _power;

        public Bomb(int x, int y, int power, Player owner) : base(x, y)
        {
            Power = power;
            Owner = owner;
        }

        public Player Owner
        {
            get { return _owner; }
            private set
            {
                if(value != null)
                {
                    _owner = value;
                }
            }
        }

        public int Power
        {
            get { return _power; }
            private set
            {
                if (value >=1 && value <= Game.Length)
                {
                    _power = value;
                }
            }
        }

        public void Explode(Map m)
        {
            var toBeDestroy = new List<Entity>();
            var thecompletelist = new List<Entity>();
            m.ListOfBomb.Remove(this);
            thecompletelist.AddRange(m.ListOfBomb);
            thecompletelist.AddRange(m.ListOfHardBlock);
            thecompletelist.AddRange(m.ListOfPlayer);
            thecompletelist.AddRange(m.ListOfSoftBlock);
            thecompletelist.AddRange(m.ListOfUpgrade);
            var l = thecompletelist.Where(c => c.X == this.X || c.Y == this.Y);
            var theRightDestroyed = this.GiveTheFirstRight(l);
            if(theRightDestroyed != null)
            {
                toBeDestroy.Add(theRightDestroyed);
            }
            var theLeftDestroyed = this.GiveTheFirstLeft(l);
            if(theLeftDestroyed != null)
            {
                toBeDestroy.Add(theLeftDestroyed);
            }
            var theUpDestroyed = this.GiveTheFirstUp(l);
            if(theUpDestroyed != null)
            {
                toBeDestroy.Add(theUpDestroyed);
            }
            var theDownDestroyed = this.GiveTheFirstDown(l);
            if(theDownDestroyed != null)
            {
                toBeDestroy.Add(theDownDestroyed);
            }
            foreach (var e in toBeDestroy)
            {
                if (e is SoftBlock)
                {
                    Entity e1 = e;
                    var theSoftBlock = m.ListOfSoftBlock.First(c => c.X == e1.X && c.Y == e1.Y);
                    theSoftBlock.Destroy(m);
                    m.ListOfSoftBlock.Remove(theSoftBlock);
                }
                else if (e is Upgrade)
                {
                    var theUpgrade = m.ListOfUpgrade.First(c => c.X == e.X && c.Y == e.Y);
                    theUpgrade.Burn();
                    m.ListOfUpgrade.Remove(theUpgrade);
                }
                else if (e is Bomb)
                {
                    var theBomb = m.ListOfBomb.First(c => c.X == e.X && c.Y == e.Y);
                    theBomb.Explode(m);
                    m.ListOfBomb.Remove(theBomb);
                }
                else if (e is Player)
                {
                    var thePlayer = m.ListOfPlayer.First(c => c.X == e.X && c.Y == e.Y);
                    thePlayer.Die();
                    m.ListOfPlayer.Remove(thePlayer);
                }
            }
        }

        
    }
}
