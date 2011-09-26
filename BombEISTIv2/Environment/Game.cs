using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BombEISTIv2.Properties;

namespace BombEISTIv2.Environment
{
    public class Game
    {
        private static int _length = 0;

        public static int Length
        {
            get
            {
                if(_length == 0)
                {
                    _length = Convert.ToInt32(Resources.MapLength);
                }
                return _length;
            }
        }
    }
}
