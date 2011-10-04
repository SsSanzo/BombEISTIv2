using System.Collections.Generic;
using System.Linq;

namespace BombEistiv2WPF.Environment
{
    public abstract class Entity
    {
        private int _x;
        private int _y;
        protected int _percentx;
        protected int _percenty;

        protected Entity(int x, int y)
        {
            X = x;
            Y = y;
            Percentx = 0;
            Percenty = 0;
        }

        public int X
        {
            get { return _x; }
            set
            {
                if (value >= 0 && value < Game.Length) _x = value;
            }
        }

        public int Y
        {
            get { return _y; }
            set
            {
                if (value >= 0 && value < Game.Length) _y = value;
            }
        }

        public int Percentx
        {
            get { return _percentx; }
            set
            {
                if (!((value < 0 && X == 0) || (value > 0 && X == Game.Length - 1)))
                {
                    if (value > 50)
                    {
                        
                        var x = X + 1;
                        if (Move(x, Y))
                        {
                            X = x;
                            _percentx = -49;
                        }
                         
                    }
                    else if (value <= -50)
                    {
                        
                        var x = X - 1;
                        if (Move(x, Y)){ X = x; _percentx = 50;}
                    }
                    else
                    {
                        _percentx = value;
                    }
                }
            }
        }

        public int Percenty
        {
            get { return _percenty; }
            set
            {
                if (!((value < 0 && Y == 0) || (value > 0 && Y == Game.Length - 1)))
                {
                    if (value > 50)
                    {
                        
                        var y = Y + 1;
                        if (Move(X, y)){ Y = y; _percenty = -49;}
                    }
                    else if (value <= -50)
                    {
                        
                        var y = Y - 1;
                        if (Move(X, y)){ Y = y; _percenty = 50;}
                    }
                    else
                    {
                        _percentx = value;
                    }
                }
            }
        }

        public Entity SetPlaceEntity(int x, int y)
        {
            X = x;
            Y = y;
            Percentx = 0;
            Percenty = 0;
            return this;
        }

        public Entity GiveTheFirst(IEnumerable<Entity> l, Direction d)
        {
            if (l == null) return null;
            switch (d)
            {
                case Direction.Left: return l.FirstOrDefault(c => c.Y == this.Y && c.X == l.Where(e => e.Y == this.Y && e.X < this.X).Max(e => e.X));
                case Direction.Right: return l.FirstOrDefault(c => c.Y == this.Y && c.X == l.Where(e => e.Y == this.Y && e.X > this.X).Min(e => e.X));
                case Direction.Down: return l.FirstOrDefault(c => c.X == this.X && c.Y == l.Where(e => e.X == this.X && e.Y < this.Y).Max(e => e.Y));
                case Direction.Up: return l.FirstOrDefault(c => c.X == this.X && c.Y == l.Where(e => e.X == this.X && e.Y > this.Y).Min(e => e.Y));
                case Direction.None: return l.FirstOrDefault(c => c.X == this.X && c.Y == this.Y);
            }
            return null;
        }
        
        public Direction GetDirectionTo(int x, int y)
        {
            if(X < x)
            {
                return Direction.Left;
            }
            else if (x < X)
            {
                return Direction.Right;
            }
            else if (y < Y)
            {
                return Direction.Down;
            }
            else if (Y < y)
            {
                return Direction.Up;
            }
            return Direction.None;
        }

        protected abstract bool Move(int x,int y);
    }

    public enum Direction
    {
        Right, Left, Up, Down, None
    }
}
