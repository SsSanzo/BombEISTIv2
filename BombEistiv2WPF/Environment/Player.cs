using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace BombEistiv2WPF.Environment
{
    public class Player : Entity
    {

        private readonly int _id;
        private readonly List<Upgrade> _upgrades;
        private int _skinid;
        private int _speed;
        private int _bombCount;
        private int _availableBombCount;
        private int _bombPower;
        private int _lives;
        private readonly Score _score;
        private readonly Map _map;
        private Direction sens;
        private Direction newsens;

        public Player(int id, int skinid, int x, int y, Map map, Score score = null) : base(x, y)
        {
            _id = id;
            _skinid = skinid;
            if(score == null)
            {
                _score = new Score(id);
            }
            _upgrades = new List<Upgrade>();
            _map = map;
            InitSkills();
        }

        public int Skinid
        {
            get { return _skinid; }
            set { if (value >= 1 && value <= 8) _skinid = value; }
        }

        private void InitSkills()
        {
            Speed = 3;
            BombCount = 1;
            BombPower = 2;
            CanKick = false;
            InvertedDirections = false;
            AvailableBombCount = 1;
            Lives = 1;
            sens = Direction.Down;
            newsens = Direction.Down;
        }

        public new int Percentx
        {
            set
            {
                if(InvertedDirections)
                {
                    var d = value - _percentx;
                    value = _percentx - d;
                }
                if (!((value < 0 && X == 0) || (value > 0 && X == Game.Length - 1)))
                {
                    if (value > 0 && ((value - Percentx) > 0))
                    {
                        var x = X + 1;
                        if (Move(x, Y))
                        {
                            if (value > 50)
                            {
                                X = x;
                                _percentx = -49;
                            }
                            else
                            {
                                _percentx = value;
                            }
                        }
                    }
                    else if (value < 0 && ((value - Percentx) < 0))
                    {
                        var x = X - 1;
                        if (Move(x, Y))
                        {
                            if (value <= -50)
                            {
                                X = x;
                                _percentx = 50;
                            }
                            else
                            {
                                _percentx = value;
                            }

                        }
                    }
                    else
                    {
                        _percentx = value;
                    }
                    Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action((reloadTickLeft)));
                }
            }

            get { return _percentx; }
        }

        public new int Percenty
        {
            set
            {
                if (InvertedDirections)
                {
                    var d = value - _percenty;
                    value = _percenty - d;
                }
                if (!((value < 0 && Y == 0) || (value > 0 && Y == Game.Length - 1)))
                {
                    if (value > 0 && ((value - Percenty) > 0))
                    {
                        var y = Y + 1;
                        if (Move(X, y))
                        {
                            if (value > 50)
                            {
                                Y = y;
                                _percenty = -49;
                            }
                            else
                            {
                                _percenty = value;
                            }
                        }
                    }
                    else if (value < 0 && ((value - Percenty) < 0))
                    {

                        var y = Y - 1;
                        if (Move(X, y))
                        {
                            if (value <= -50)
                            {
                                Y = y;
                                _percenty = 50;
                            }
                            else
                            {
                                _percenty = value;
                            }

                        }
                    }
                    else
                    {
                        _percenty = value;
                    }
                    Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action((reloadTickTop)));
                }
            }

            get { return _percenty; }
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

        public int Lives
        {
            get { return _lives; }
            private set
            {
                _lives = value;
            }
        }

        public Direction Sens
        {
            get { return sens; }
            set { if(value != Direction.None) sens = value; }
        }

        public Direction NewSens
        {
            get { return newsens; }
            set { if (value != Direction.None) newsens = value; }
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
                    case UpgradeType.Life:
                        Lives++;
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

        public bool Die()
        {
            this.Lives--;
            return Lives <= 0;
        }

        public bool IsDead()
        {
            return Lives <= 0;
        }

        protected override bool Move(int x, int y)
        {
            var e = _map.GetEntity(x, y);
            if(e is Bomb)
            {
                if (CanKick)
                {
                    var b = (Bomb)e;
                    b.Move(this.GetDirectionTo(x, y));
                    return true;
                }
                else return false;
            }
            else if(e is HardBlock || e is SoftBlock)
            {
                return false;
            }
            else if(e is Upgrade)
            {
                var u = (Upgrade) e;
                _map.PickupUpgrade(u);
                AddAndApplyUpgrade(u);
                u.Burn();
                return true;
            }
            return true;
        }

        public void changeFace(BitmapImage bi)
        {
            Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => Source = bi));
        }
    }
}