using System;
using System.Resources;
using BombEISTIv2.Properties;

namespace BombEISTIv2.Environment
{
    public class Entity
    {
        private int _x;
        private int _y;

        public int X {
            get { return _x; }
            set{
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
    }
}
