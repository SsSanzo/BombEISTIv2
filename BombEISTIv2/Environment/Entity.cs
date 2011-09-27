using System;
using System.Resources;
using BombEISTIv2.Properties;

namespace BombEISTIv2.Environment
{
    public class Entity
    {
        private int _x;
        private int _y;
        private int _percentx;
        private int _percenty;

        public Entity(int x, int y)
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
                if (value < 0) throw new Exception("Location Error (x<0) : x=" + value);
                if (value > Game.Length) throw new Exception("Location Error (x>" + Game.Length + ") : x=" + value);
                _x = value;
            }
        }

        public int Y
        {
            get { return _y; }
            set
            {
                if (value < 0) throw new Exception("Location Error (y<0) : y=" + value);
                if (value > Game.Length) throw new Exception("Location Error (y>" + Game.Length + ") : y=" + value);
                _y = value;
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
                        _percentx = -49;
                        X++;
                    }
                    else if (value <= -50)
                    {
                        _percentx = 50;
                        X--;
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
                        _percenty = -49;
                        Y++;
                    }
                    else if (value <= -50)
                    {
                        _percenty = 50;
                        Y--;
                    }
                    else
                    {
                        _percentx = value;
                    }
                }
            }
        }

        public void SetPlaceEntity(int x, int y)
        {
            X = x;
            Y = y;
            Percentx = 0;
            Percenty = 0;
        }

        public void movingEntityByPixel(Direction d)
        {
            switch (d)
            {
                    case Direction.Down:
                    Percenty--;
                    break;
                    case Direction.Up:
                    Percenty++;
                    break;
                    case Direction.Left:
                    Percentx--;
                    break;
                    case Direction.Right:
                    Percentx++;
                    break;
            }

        }
    }

    public enum Direction
    {
        Right, Left, Up, Down
    }
}
