using BombEistiv2WPF.Environment;
using BombEistiv2WPF.View;

namespace BombEistiv2WPF.Control
{
    public class Movement
    {

        public static void Move(string direction, Player p)
        {
            
            if(direction == Direction.Up.ToString())
            {
                p.Percenty -= p.Speed;
                p.Sens = Direction.Up;
            }else if(direction == Direction.Down.ToString())
            {
                p.Percenty += p.Speed;
                p.Sens = Direction.Down;
            }
            else if (direction == Direction.Right.ToString())
            {
                p.Percentx += p.Speed;
                p.Sens = Direction.Right;
            }
            else if (direction == Direction.Left.ToString())
            {
                p.Percentx -= p.Speed;
                p.Sens = Direction.Left;
            }
            p.Source = Texture._.TypetextureList[Texture._.GetTextureKey(p)];
        }
    }
}
