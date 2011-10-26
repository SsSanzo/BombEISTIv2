using System;
using System.Collections.Generic;
using System.Windows.Threading;
using BombEistiv2WPF.Environment;
using BombEistiv2WPF.View;

namespace BombEistiv2WPF.Control
{
    public class Movement
    {

        public static void Move(string direction, Player p)
        {
            var previousd = p.Sens;
            Direction nextdirect = Direction.None;
            if(direction == Direction.Up.ToString())
            {
                p.Percenty -= p.Speed;
                nextdirect = Direction.Up;
            }else if(direction == Direction.Down.ToString())
            {
                p.Percenty += p.Speed;
                nextdirect = Direction.Down;
            }
            else if (direction == Direction.Right.ToString())
            {
                p.Percentx += p.Speed;
                nextdirect = Direction.Right;
            }
            else if (direction == Direction.Left.ToString())
            {
                p.Percentx -= p.Speed;
                nextdirect = Direction.Left;
            }
            if (nextdirect != previousd)
            {
                p.Sens = nextdirect;
                p.changeFace(Texture._.TypetextureList[Texture._.GetTextureKey(p)]);
            }
        }
    }
}
