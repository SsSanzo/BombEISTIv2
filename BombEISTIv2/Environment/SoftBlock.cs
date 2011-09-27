namespace BombEISTIv2.Environment
{
    public class SoftBlock : Entity
    {
        private readonly Upgrade _myUpgrade;

        public SoftBlock(int x, int y, Upgrade u = null)
            : base(x, y)
        {
            _myUpgrade = u;
        }


        public Upgrade MyUpgrade
        {
            get { return _myUpgrade; }
        }
    }

    public class Class1
    {
    }

}
