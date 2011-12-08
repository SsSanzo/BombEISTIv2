using System;
using System.Collections.Generic;
using System.Linq;
using BombEistiv2WPF.Control;
using BombEistiv2WPF.View;

namespace BombEistiv2WPF.Environment
{
    public class Bomb : Entity
    {

        private Player _owner;
        private int _power;
        private Direction directionMoving;


        public Bomb(int x, int y, int power, Player owner, bool autoStart = true)
            : base(x, y)
        {
            Power = power;
            Owner = owner;
            directionMoving = Direction.None;
            if(autoStart)
            {
                TimerManager._.AddNewTimer(false, GameParameters._.ExplosionDelay * 1000, true, new TimerEvent { InvolvedObject = this, Type = EventType.BombExplode });
            }
            
        }


        public Direction DirectionMoving
        {
            get { return directionMoving; }
            set { directionMoving = value;  }
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

        //public void Explode(Game g)
        //{
        //    var toBeDestroyed = new List<Entity>();
        //    g.TheCurrentMap.ListOfBomb.Remove(this);
        //    var thecompletelist = g.TheCurrentMap.GetCompleteList();
        //    Texture._.DeleteTextureEntity(this);
            
        //    var l = thecompletelist.Where(c => (this.Y == c.Y && (c.X <= (this.X + this.Power) && (c.X >= (this.X - this.Power)))) || (this.X == c.X && (c.Y <= (this.Y + this.Power) && (c.Y >= (this.Y - this.Power)))));
        //    var theRightDestroyed = this.GiveTheFirst(l, Direction.Right);
        //    if (theRightDestroyed != null && !g.ToDelete.Contains(theRightDestroyed))
        //    {
        //        toBeDestroyed.Add(theRightDestroyed);
        //    }
        //    var theLeftDestroyed = this.GiveTheFirst(l, Direction.Left);
        //    if (theLeftDestroyed != null && !g.ToDelete.Contains(theLeftDestroyed))
        //    {
        //        toBeDestroyed.Add(theLeftDestroyed);
        //    }
        //    var theUpDestroyed = this.GiveTheFirst(l, Direction.Up);
        //    if (theUpDestroyed != null && !g.ToDelete.Contains(theUpDestroyed))
        //    {
        //        toBeDestroyed.Add(theUpDestroyed);
        //    }
        //    var theDownDestroyed = this.GiveTheFirst(l, Direction.Down);
        //    if (theDownDestroyed != null && !g.ToDelete.Contains(theDownDestroyed))
        //    {
        //        toBeDestroyed.Add(theDownDestroyed);
        //    }
        //    var theNoneDestroyed = this.GiveTheFirst(l, Direction.None);
        //    if (theNoneDestroyed != null && !g.ToDelete.Contains(theNoneDestroyed))
        //    {
        //        toBeDestroyed.Add(theNoneDestroyed);
        //    }
        //    Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => Texture._.Explosion(this, g, theLeftDestroyed, theUpDestroyed, theRightDestroyed, theDownDestroyed)));

        //    foreach (var e in toBeDestroyed)
        //    {
        //        if (e is Bomb)
        //        {
        //            var theBomb = g.TheCurrentMap.ListOfBomb.First(c => c.X == e.X && c.Y == e.Y);
        //            theBomb.Explode(g);
        //        }else if(!g.ToDelete.Contains(e))
        //        {
        //            g.ToDelete.Add(e);
        //        }
        //    }
        //    //g.ToDelete.AddRange(toBeDestroyed.FindAll(e => !(e is Bomb)));
        //    //if (!g.ToDelete.Contains(this))
        //    //{
        //    //    g.ToDelete.Add(this);
        //    //}
        //    Owner.BombExploded(this);
        //}

        public void Explode(Game g)
        {
            g.TheCurrentMap.ListOfBomb.Remove(this);
            var thecompletelist = g.TheCurrentMap.GetCompleteList();
            thecompletelist.AddRange(g.TheCurrentMap.ListOfEntityOfDeath.Where(c => c != null && c.Blocus));
            Texture._.DeleteTextureEntity(this);

            var l = thecompletelist.Where(c => (this.Y == c.Y && (c.X <= (this.X + this.Power) && (c.X >= (this.X - this.Power)))) || (this.X == c.X && (c.Y <= (this.Y + this.Power) && (c.Y >= (this.Y - this.Power)))));
            var theRightDestroyed = this.GiveTheFirst(l, Direction.Right);
            var theLeftDestroyed = this.GiveTheFirst(l, Direction.Left);
            var theUpDestroyed = this.GiveTheFirst(l, Direction.Up);
            var theDownDestroyed = this.GiveTheFirst(l, Direction.Down);
            var theNoneDestroyed = this.GiveTheFirst(l, Direction.None);
            Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => Texture._.Explosion(this, g, theLeftDestroyed, theUpDestroyed, theRightDestroyed, theDownDestroyed, theNoneDestroyed)));

            //foreach (var e in toBeDestroyed)
            //{
            //    if (e is Bomb)
            //    {
            //        var theBomb = g.TheCurrentMap.ListOfBomb.First(c => c.X == e.X && c.Y == e.Y);
            //        theBomb.Explode(g);
            //    }
            //}
            //g.ToDelete.AddRange(toBeDestroyed.FindAll(e => !(e is Bomb)));
            //if (!g.ToDelete.Contains(this))
            //{
            //    g.ToDelete.Add(this);
            //}
            Owner.BombExploded(this);
        }

        public void CheckEntityOfDeath(int x, int y)
        {
            if(Owner.Map.ListOfEntityOfDeath.First(c => c.X == x && c.Y == y) != null)
            {
                directionMoving = Direction.None;
                this.Explode(TimerManager._.Game);
            }
        }

        public void Move(Direction d)
        {
            directionMoving = d;
            TimerManager._.AddNewTimer(true, 15, true, new TimerEvent { InvolvedObject = this, Type = EventType.BombMove });
        }

        public bool Move()
        {
            var oldposperx = Percentx;
            var oldpospery = Percenty;
            switch (directionMoving)
            {
                case Direction.Left:
                    Percentx += 5;
                    break;
                case Direction.Right:
                    Percentx -= 5;
                    break;
                case Direction.Up:
                    Percenty -= 5;
                    break;
                case Direction.Down:
                    Percenty += 5;
                    break;
            }
            if ((oldposperx == Percentx) && (oldpospery == Percenty))
            {
                directionMoving = Direction.None;
                return false;
            }
            return true;
        }

        protected override bool Move(int x, int y)
        {
            return Owner.Map.GetEntity(x, y) == null;
        }
    }
}