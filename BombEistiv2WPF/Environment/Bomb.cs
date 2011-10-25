using System.Collections.Generic;
using System.Linq;

namespace BombEistiv2WPF.Environment
{
    public class Bomb : Entity
    {

        private Player _owner;
        private int _power;


        public Bomb(int x, int y, int power, Player owner)
            : base(x, y)
        {
            Power = power;
            Owner = owner;
            TimerManager._.AddNewTimer(false, GameParameters._.ExplosionDelay, true, new TimerEvent { InvolvedObject = this, Type = EventType.BombExplode });
        }


        public Player Owner
        {
            get { return _owner; }
            private set
            {
                if (value != null)
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
                if (value >= 1 && value <= Game.Length)
                {
                    _power = value;
                }
            }
        }

        public void Explode(Map m, Game g)
        {
            var toBeDestroyed = new List<Entity>();
            var thecompletelist = m.GetCompleteList();
            m.ListOfBomb.Remove(this);
            var l = thecompletelist.Where(c => c.X == this.X || c.Y == this.Y);
            var theRightDestroyed = this.GiveTheFirst(l, Direction.Right);
            if (theRightDestroyed != null && !g.ToDelete.Contains(theRightDestroyed))
            {
                toBeDestroyed.Add(theRightDestroyed);
            }
            var theLeftDestroyed = this.GiveTheFirst(l, Direction.Left);
            if (theLeftDestroyed != null && !g.ToDelete.Contains(theLeftDestroyed))
            {
                toBeDestroyed.Add(theLeftDestroyed);
            }
            var theUpDestroyed = this.GiveTheFirst(l, Direction.Up);
            if (theUpDestroyed != null && !g.ToDelete.Contains(theUpDestroyed))
            {
                toBeDestroyed.Add(theUpDestroyed);
            }
            var theDownDestroyed = this.GiveTheFirst(l, Direction.Down);
            if (theDownDestroyed != null && !g.ToDelete.Contains(theDownDestroyed))
            {
                toBeDestroyed.Add(theDownDestroyed);
            }

            foreach (var e in toBeDestroyed)
            {
                if (e is Bomb)
                {
                    var theBomb = m.ListOfBomb.First(c => c.X == e.X && c.Y == e.Y);
                    theBomb.Explode(m, g);
                }
            }
            g.ToDelete.AddRange(toBeDestroyed);
            if (!g.ToDelete.Contains(this))
            {
                g.ToDelete.Add(this);
            }
            Owner.BombExploded(this);
        }

        public void Move(Direction d)
        {
            // =>
        }

        protected override bool Move(int x, int y)
        {
            throw new System.NotImplementedException();
        }
    }
}