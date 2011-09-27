namespace BombEISTIv2.Environment
{
    public class Upgrade : Entity
    {
        private readonly UpgradeType _type;

        public Upgrade(int x, int y, UpgradeType u)
            : base(x, y)
        {
            _type = u;
        }

        public UpgradeType Type
        {
            get { return _type; }
        }
    }



}
