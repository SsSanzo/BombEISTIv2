using System.Collections.Generic;

namespace BombEISTIv2.Environment
{
    public class Player : Entity
    {

        private readonly int _id;
        private readonly List<Upgrade> _upgrades;
        private int _speed;
        private int _bombCount;
        private int _availableBombCount;
        private int _bombPower;
        private bool _invertedDirections;
        private readonly Score _score;

        public Player(int id, int x, int y,Score score = null) : base(x, y)
        {
            _id = id;
            if(score == null)
            {
                _score = new Score(id);
            }
            _upgrades = new List<Upgrade>();
            InitSkills();
        }

        private void InitSkills()
        {
            Speed = 1;
            BombCount = 1;
            BombPower = 1;
            CanKick = false;
            InvertedDirections = false;
            AvailableBombCount = 1;
        }

        public Score Score
        {
            get { return _score; }
        }

        public int Id
        {
            get { return _id; }
        }

        public int Speed
        {
            get { return _speed; }
            private set
            {
                if(value >= 1)
                {
                    _speed = value;
                }
            }
        }

        public int BombCount
        {
            get { return _bombCount; }
            private set
            {
                if (value >= 1)
                {
                    _bombCount = value;
                }
            }
        }

        public int BombPower
        {
            get { return _bombPower; }
            private set
            {
                if (value >= 1 && value <= Game.Length)
                {
                    _bombPower = value;
                }
            }
        }

        public bool CanKick { get; private set; }

        private bool InvertedDirections { get; set; }

        public int AvailableBombCount
        {
            get { return _availableBombCount; }
            private set
            {
                if (value >= 0 && value <= BombCount)
                {
                   _availableBombCount = value;
                }
            }
        }

        public Player AddAndApplyUpgrade(Upgrade u)
        {
            _upgrades.Add(u);
            return ApplyUpgrade(u);
        }

        public Player ApplyUpgrade(Upgrade u)
        {
            UpgradeType type = u.Type;
            switch (type)
            {
                    case UpgradeType.BombUp:
                        BombCount++;
                        break;
                    case UpgradeType.ChangeDirection:
                        InvertedDirections = true;
                        break;
                    case UpgradeType.Kick:
                        CanKick = true;
                        break;
                    case UpgradeType.PowerDown:
                        BombPower--;
                        break;
                    case UpgradeType.PowerMax:
                        BombPower = Game.Length;
                        break;
                    case UpgradeType.PowerUp:
                        BombPower++;
                        break;
                    case UpgradeType.SpeedDown:
                        Speed--;
                        break;
                    case UpgradeType.SpeedMax:
                        //TODO: choose a maximum speed
                        break;
                    case UpgradeType.SpeedUp:
                        Speed++;
                        break;
                    case UpgradeType.Teleport:
                        //Managed by the Game class
                        break;
            }
            return this;
        }

        public Player Move(Direction d)
        {
            if(InvertedDirections)
            {
                switch (d)
                {
                    case Direction.Down:
                        d = Direction.Up;
                        break;
                    case Direction.Up:
                        d = Direction.Down;
                        break;
                    case Direction.Left:
                        d = Direction.Right;
                        break;
                    case Direction.Right:
                        d = Direction.Left;
                        break;
                }
            }
            switch (d)
            {
                case Direction.Down:
                    Percenty -=_speed;
                    break;
                case Direction.Up:
                    Percenty += _speed;
                    break;
                case Direction.Left:
                    Percentx -= _speed;
                    break;
                case Direction.Right:
                    Percentx += _speed;
                    break;
            }
            return this;
        }

        public Player BombExploded(Bomb b)
        {
            if(b.Owner == this)
            {
                AvailableBombCount++;
            }
            return this;
        }

        public Bomb PutABomb()
        {
            Bomb b = null;
            if(AvailableBombCount >= 1){
                b = new Bomb(X,Y,BombPower,this);
                AvailableBombCount--;
            }
            return b;
        }

        public void Die(Map m)
        {
            
        }
    }
}