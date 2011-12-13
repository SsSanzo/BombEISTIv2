using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace BombEistiv2WPF.Environment
{
    public abstract class Entity : Image
    {
        private int _x;
        private int _y;
        protected int _percentx;
        protected int _percenty;
        private Thickness tick;

        protected Entity(int x, int y)
        {
            X = x;
            Y = y;
            Percentx = 0;
            Percenty = 0;
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Top;
            tick = new Thickness
                       {
                           Left = (X * 40) + (40 * ((double)Percentx / 100.0)),
                           Top = (Y*40) + (40*((double) Percenty/100.0)),
                           Right = 0.0,
                           Bottom = 0.0
                       };
            Margin = tick;
        }

        public int X
        {
            get { return _x; }
            set
            {
                //if (value >= 0 && value < Game.Length)
                //{
                    _x = value;
                    //Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action((reloadTickLeft)));
                //}
            }
        }

        public int Y
        {
            get { return _y; }
            set
            {
                //if (value >= 0 && value < Game.Length)
                //{
                    _y = value;
                    //Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action((reloadTickTop)));
                //}
            }
        }

        public int Percentx
        {
            get { return _percentx; }
            set
            {
                if (!((value < 0 && X == 0) || (value > 0 && X == Game.Length - 1)))
                {
                    if (value > 0 && ((value - Percentx) > 0))
                    {
                        var x = X + 1;
                        if (Move(x, Y))
                        {
                            if (value > 50)
                            {
                                X = x;
                                _percentx = -49 + (value - 51);
                            }
                            else
                            {
                                _percentx = value;
                            }
                        }
                    }
                    else if (value < 0 && ((value - Percentx) < 0))
                    {

                        var x = X - 1;
                        if (Move(x, Y))
                        {
                            if (value <= -50)
                            {
                                X = x;
                                _percentx = 50 + (50 + value);
                            }
                            else
                            {
                                _percentx = value;
                            }
                        }
                    }
                    else
                    {
                        _percentx = value;
                    }
                    Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action((reloadTickLeft)));
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
                    if (value > 0 && ((value - Percenty) > 0))
                    {
                        var y = Y + 1;
                        if (Move(X, y))
                        {
                            if (value > 50)
                            {
                                Y = y;
                                _percenty = -49 + (value - 51);
                            }else
                            {
                                _percenty = value;
                            }
                        }
                    }
                    else if (value < 0 && ((value - Percenty) < 0))
                    {
                        
                        var y = Y - 1;
                        if (Move(X, y))
                        {
                            if (value <= -50)
                            {
                                Y = y;
                                _percenty = 50 + (50 + value);
                            }else
                            {
                                _percenty = value;
                            }
                        }
                    }
                    else
                    {
                        _percenty = value;
                    }
                    Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action((reloadTickTop)));
                }
            }
        }

        public Entity SetPlaceEntity(int x, int y)
        {
            X = x;
            Y = y;
            Percentx = 0;
            Percenty = 0;
            reloadTickLeft();
            reloadTickTop();
            return this;
        }

        public Entity GiveTheFirst(IEnumerable<Entity> l, Direction d)
        {
            if (l == null) return null;
            switch (d)
            {
                case Direction.Left: return l.Where(e => e.Y == this.Y && e.X < this.X).Count() != 0 ? l.FirstOrDefault(c => c.Y == this.Y && c.X == l.Where(e => e.Y == this.Y && e.X < this.X).Max(e => e.X)) : null;
                case Direction.Right: return l.Where(e => e.Y == this.Y && e.X > this.X).Count() != 0 ? l.FirstOrDefault(c => c.Y == this.Y && c.X == l.Where(e => e.Y == this.Y && e.X > this.X).Min(e => e.X)) : null;
                case Direction.Up: return l.Where(e => e.X == this.X && e.Y < this.Y).Count() != 0 ? l.FirstOrDefault(c => c.X == this.X && c.Y == l.Where(e => e.X == this.X && e.Y < this.Y).Max(e => e.Y)) : null;
                case Direction.Down: return l.Where(e => e.X == this.X && e.Y > this.Y).Count() != 0 ? l.FirstOrDefault(c => c.X == this.X && c.Y == l.Where(e => e.X == this.X && e.Y > this.Y).Min(e => e.Y)) : null;
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

        public void reloadTickLeft()
        {
            tick.Left = (X * 40) + (40 * ((double)Percentx / 100.0));
            Margin = tick;
        }

        public void reloadTickTop()
        {
            tick.Top = (Y * 40) + (40 * ((double)Percenty / 100.0));
            Margin = tick;
        }

        protected abstract bool Move(int x,int y);
    }

    public enum Direction
    {
        Right, Left, Up, Down, None
    }
}
