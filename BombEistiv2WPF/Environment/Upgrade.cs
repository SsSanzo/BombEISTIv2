namespace BombEistiv2WPF.Environment
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

        public void Burn()
        {
            
        }

        protected override bool Move(int x, int y)
        {
            return false;
        }
    }



}
