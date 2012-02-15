using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Threading;
using BombEistiv2WPF.Control;
using BombEistiv2WPF.View;

namespace BombEistiv2WPF.Environment
{
    public class Bomb : Entity
    {

        private Player _owner;
        private int _power;
        private Direction directionMoving;
        private BombType _type;
        private int itération;

        public Bomb(int x, int y, int power, Player owner, bool autoStart = true, BombType t = BombType.Normal)
            : base(x, y)
        {
            Power = power;
            Owner = owner;
            directionMoving = Direction.None;
            _type = t;
            itération = 0;
            if (!autoStart) return;
            StartBomb();
        }

        public void StartBomb()
        {
            switch (_type)
            {
                case BombType.Normal:
                    TimerManager._.AddNewTimer(false, GameParameters._.ExplosionDelay * 1000, true, new TimerEvent { InvolvedObject = this, Type = EventType.BombExplode });
                    break;
                case BombType.Mine:
                    break;
                case BombType.Fly:
                    TimerManager._.AddNewTimer(true, 15, true, null, LeaveBomb);
                    TimerManager._.AddNewTimer(false, 9200, true, new TimerEvent { InvolvedObject = this, Type = EventType.BombReturns });
                    TimerManager._.AddNewTimer(false, 10000, true, new TimerEvent { InvolvedObject = this, Type = EventType.BombExplode });
                    break;
                case BombType.Teleport:
                    TimerManager._.AddNewTimer(true, 700, true, new TimerEvent { InvolvedObject = this, Type = EventType.BombTeleport });
                    TimerManager._.AddNewTimer(false, GameParameters._.ExplosionDelay * 1000, true, new TimerEvent { InvolvedObject = this, Type = EventType.BombExplode });
                    break;
                default:
                    TimerManager._.AddNewTimer(false, GameParameters._.ExplosionDelay * 1000, true, new TimerEvent { InvolvedObject = this, Type = EventType.BombExplode });
                    break;
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

        public BombType Type
        {
            get { return _type; }
        }

        

        public void Explode(Game g)
        {
            PlaySound._.TypeSoundList["Explosionwav"].Play();
            g.TheCurrentMap.ListOfBomb.Remove(this);
            var thecompletelist = g.TheCurrentMap.GetCompleteList(true, true);
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

        public void ExplodeFreeze(Game g)
        {
            PlaySound._.TypeSoundList["Explosionwav"].Play();
            g.TheCurrentMap.ListOfBomb.Remove(this);
            var thecompletelist = g.TheCurrentMap.GetCompleteList(true, true);
            thecompletelist.AddRange(g.TheCurrentMap.ListOfEntityOfDeath.Where(c => c != null && c.Blocus));
            Texture._.DeleteTextureEntity(this);

            var l = thecompletelist.Where(c => (this.Y == c.Y && (c.X <= (this.X + this.Power) && (c.X >= (this.X - this.Power)))) || (this.X == c.X && (c.Y <= (this.Y + this.Power) && (c.Y >= (this.Y - this.Power)))));
            var theRightDestroyed = this.GiveTheFirst(l, Direction.Right);
            var theLeftDestroyed = this.GiveTheFirst(l, Direction.Left);
            var theUpDestroyed = this.GiveTheFirst(l, Direction.Up);
            var theDownDestroyed = this.GiveTheFirst(l, Direction.Down);
            var theNoneDestroyed = this.GiveTheFirst(l, Direction.None);
            Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => Texture._.ExplosionFreeze(this, g, theLeftDestroyed, theUpDestroyed, theRightDestroyed, theDownDestroyed, theNoneDestroyed)));

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

        public void ExplodeDark(Game g)
        {
            PlaySound._.TypeSoundList["Explosionwav"].Play();
            g.TheCurrentMap.ListOfBomb.Remove(this);
            var thecompletelist = g.TheCurrentMap.GetCompleteList(true, true);
            thecompletelist.AddRange(g.TheCurrentMap.ListOfEntityOfDeath.Where(c => c != null && c.Blocus));
            Texture._.DeleteTextureEntity(this);

            var l = thecompletelist.Where(c => (this.Y == c.Y && (c.X <= (this.X + this.Power) && (c.X >= (this.X - this.Power)))) || (this.X == c.X && (c.Y <= (this.Y + this.Power) && (c.Y >= (this.Y - this.Power)))));
            var theRightDestroyed = this.GiveTheFirst(l, Direction.Right);
            var theLeftDestroyed = this.GiveTheFirst(l, Direction.Left);
            var theUpDestroyed = this.GiveTheFirst(l, Direction.Up);
            var theDownDestroyed = this.GiveTheFirst(l, Direction.Down);
            var theNoneDestroyed = this.GiveTheFirst(l, Direction.None);
            Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => Texture._.ExplosionDark(this, g, theLeftDestroyed, theUpDestroyed, theRightDestroyed, theDownDestroyed, theNoneDestroyed)));

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

        public void ExplodeCata(Game g)
        {
            PlaySound._.TypeSoundList["Explosionwav"].Play();
            g.TheCurrentMap.ListOfBomb.Remove(this);
            var thecompletelist = g.TheCurrentMap.GetCompleteList(true, true);
            thecompletelist.AddRange(g.TheCurrentMap.ListOfEntityOfDeath.Where(c => c != null && c.Blocus));
            Texture._.DeleteTextureEntity(this);
            Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => CheckDestroyed(g)));

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

        public void ExplodeFlower(Game g)
        {
            PlaySound._.TypeSoundList["Explosionwav"].Play();
            g.TheCurrentMap.ListOfBomb.Remove(this);
            var thecompletelist = g.TheCurrentMap.GetCompleteList(true, true);
            thecompletelist.AddRange(g.TheCurrentMap.ListOfEntityOfDeath.Where(c => c != null && c.Blocus));
            Texture._.DeleteTextureEntity(this);

            var l = thecompletelist.Where(c => (this.Y == c.Y && (c.X <= (this.X + this.Power) && (c.X >= (this.X - this.Power)))) || (this.X == c.X && (c.Y <= (this.Y + this.Power) && (c.Y >= (this.Y - this.Power)))));
            Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => PutFlowerLimit(g, l)));

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
        
        public void PutFlowerLimit(Game g, IEnumerable<Entity> l)
        {
            var theRightDestroyed = this.GiveTheFirst(l, Direction.Right);
            var theTrueRight = theRightDestroyed != null
                                   ? new HardBlock(theRightDestroyed.X, theRightDestroyed.Y)
                                   : null;
            var theLeftDestroyed = this.GiveTheFirst(l, Direction.Left);
            var theTrueLeft = theLeftDestroyed != null
                                   ? new HardBlock(theLeftDestroyed.X, theLeftDestroyed.Y)
                                   : null;
            var theUpDestroyed = this.GiveTheFirst(l, Direction.Up);
            var theTrueUp = theUpDestroyed != null
                                   ? new HardBlock(theUpDestroyed.X, theUpDestroyed.Y)
                                   : null;
            var theDownDestroyed = this.GiveTheFirst(l, Direction.Down);
            var theTrueDown = theDownDestroyed != null
                                   ? new HardBlock(theDownDestroyed.X, theDownDestroyed.Y)
                                   : null;
            Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => Texture._.ExplosionFlower(this, g, theTrueLeft, theTrueUp, theTrueRight, theTrueDown, null)));
        }

        public void CheckDestroyed(Game g)
        {
            Entity theRightDestroyed = new SoftBlock(this.X + this.Power, this.Y);
            if(this.X + this.Power >= Game.Length)
            {
                theRightDestroyed = new HardBlock(Game.Length, this.Y);
            }
            Entity theLeftDestroyed = new SoftBlock(this.X - this.Power, this.Y);
            if(this.X - this.Power <= -1)
            {
                theLeftDestroyed = new HardBlock(-1, this.Y);
            }
            Entity theUpDestroyed = new SoftBlock(this.X, this.Y - this.Power);
            if(this.Y - this.Power <= -1)
            {
                theUpDestroyed = new HardBlock(this.X, -1);
            }
            Entity theDownDestroyed = new SoftBlock(this.X, this.Y + this.Power);
            if(this.Y + this.Power >= Game.Length)
            {
                theDownDestroyed = new HardBlock(this.X, Game.Length);
            }
            Texture._.Explosion(this, g, theLeftDestroyed, theUpDestroyed, theRightDestroyed, theDownDestroyed, null);
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
                    Percentx -= 5;
                    break;
                case Direction.Right:
                    Percentx += 5;
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

        public bool Teleport(Map m)
        {
            if(itération < 3)
            {
                var list = m.GetCompleteList().ToList();
                var l = new List<HardBlock>();
                for (var i = 0; i < Game.Length; i++)
                {
                    for (var j = 0; j < Game.Length; j++)
                    {
                        if (list.FirstOrDefault(c => c != null && c.X == i && c.Y == j) == null)
                        {
                            Texture._.Mw.Dispatcher.Invoke(DispatcherPriority.Normal,
                                                       (Action)(() => l.Add(new HardBlock(i, j))));
                        }
                    }
                }

                if (l == null || l.Count() == 0) return true;
                var rand = new Random();
                var n = rand.Next(l.Count);
                this.X = l.ElementAt(n).X;
                this.Y = l.ElementAt(n).Y;
                Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action((reloadTickLeft)));
                Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action((reloadTickTop)));
                itération++;
                return true;
            }
            return false;

        }

        public void LeaveBomb(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            Texture._.Mw.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => LeavingBomb((Timer)sender)));
        }

        public void LeavingBomb(Timer t)
        {
            if((this.Margin.Top > 0))
            {
                this.Margin = new Thickness(this.Margin.Left, this.Margin.Top - 20, 0.0, 0.0);
            }else
            {
                this.Margin = new Thickness(this.Margin.Left, 0, 0.0, 0.0);
                this.Opacity = 0;
                t.AutoReset = false;
            }
        }

        public void LandBomb(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            Texture._.Mw.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => LandingBomb((Timer)sender)));
        }

        public void LandingBomb(Timer t)
        {
            if (this.Margin.Top < ((Y * 40) - 1))
            {
                Texture._.Mw.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>this.Margin = new Thickness(this.Margin.Left, this.Margin.Top + 20, 0.0, 0.0)));
            }
            else
            {
                Texture._.Mw.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => this.Margin = new Thickness(this.Margin.Left, Y * 40, 0.0, 0.0)));
                Texture._.Mw.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => ListenerGame._.GameInProgress.TheCurrentMap.ListOfBomb.Add(this)));
                //Texture._.Mw.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(reloadTickTop));
                t.AutoReset = false;
            }
        }

        protected override bool Move(int x, int y)
        {
            if (this.Type != BombType.Ghost)
            {
                var e = Owner.Map.GetEntityWithoutMine(x, y);
                return (e == null) || (e is Bomb && ((Bomb)e).Type == BombType.Mine);
            }else
            {
                return Owner.Map.GetPlayer(x,y) == null;
            }
            
        }
    }
}