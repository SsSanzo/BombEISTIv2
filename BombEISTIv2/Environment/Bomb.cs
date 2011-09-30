namespace BombEISTIv2.Environment
{
    public class Bomb : Entity
    {

        private Player _owner;
        private int _power;

        public Bomb(int x, int y, int power, Player owner) : base(x, y)
        {
            Power = power;
            Owner = owner;
        }

        public Player Owner
        {
            get { return _owner; }
            private set
            {
                if(value != null)
                {
                    _owner = value;
                }
            }
        }

        public int Power
        {
            get { return _power; }
            private set
            {
                if (value >=1 && value <= Game.Length)
                {
                    _power = value;
                }
            }
        }
    }
}
