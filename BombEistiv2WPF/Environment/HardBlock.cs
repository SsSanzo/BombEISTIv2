namespace BombEistiv2WPF.Environment
{
    public class HardBlock : Entity
    {
        public HardBlock(int x, int y) : base(x, y)
        {
        }

        protected override bool Move(int x, int y)
        {
            return false;
        }
    }
}
