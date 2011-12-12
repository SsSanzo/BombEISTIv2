using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Windows.Media.Imaging;
using BombEistiv2WPF.View;

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
        private readonly Map _map;
        private Direction sens;
        private Direction newsens;
        private int clingnotement;

        public Player(int id, int skinid, int x, int y, Map map, Score score = null) : base(x, y)
        {
            _id = id;
            _skinid = skinid;
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
            Lives = GameParameters._.LivesCount;
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
                                HaveToDie(X,Y);
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
                                HaveToDie(X, Y);
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
                                HaveToDie(X, Y);
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
                                HaveToDie(X, Y);
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

        public Map Map
        {
            get { return _map; }
        }

        public int Id
        {
            get { return _id; }
        }

        public int Clingnotement
        {
            get { return clingnotement; }
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

        public Direction InvertedSens(Direction d)
        {
            if(d == Direction.Up)
            {
                return Direction.Down;
            }
            if(d == Direction.Down)
            {
                return Direction.Up;
            }
            if (d == Direction.Left)
            {
                return Direction.Right;
            }
            if (d == Direction.Right)
            {
                return Direction.Left;
            }
            return Direction.None;
        }

        public Direction NewSens
        {
            get { return newsens; }
            set { if (value != Direction.None) newsens = value; }
        }

        public bool CanKick { get; private set; }

        public bool InvertedDirections { get; set; }

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
                        AvailableBombCount++;
                        break;
                    case UpgradeType.ChangeDirection:
                        InvertedDirections = true;
                        this.changeFace(Texture._.TypetextureList[Texture._.GetTextureKey(this)]);
                        TimerManager._.AddNewTimer(false, 10000, true, new TimerEvent { InvolvedObject = this, Type = EventType.Malediction });
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
                        Speed = 9;
                        break;
                    case UpgradeType.SpeedUp:
                        Speed++;
                        break;
                    case UpgradeType.Teleport:
                        Map.Teleport(this);
                        break;
                    case UpgradeType.Life:
                        Lives++;
                        InGameMenu._.changeLabel(Id, Lives);
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
            if(AvailableBombCount >= 1 && _map.GetBomb(X,Y) == null){
                b = new Bomb(X,Y,BombPower,this);
                AvailableBombCount--;
            }
            return b;
        }

        public bool Die()
        {
            this.Lives--;
            InGameMenu._.changeLabel(Id,Lives);
            return Lives <= 0;
        }

        public bool DieFinal()
        {
            this.Lives = 0;
            InGameMenu._.changeLabel(Id, 0);
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
                    //b.Move(this.GetDirectionTo(x, y));
                    if(b.DirectionMoving == Direction.None)
                    {
                        b.Move(this.Sens);
                    }
                }
                return false;
            }else if(e is EntityOfDeath)
            {
                return true;
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

        protected void HaveToDie(int x, int y)
        {
            if(clingnotement == 0 || clingnotement > 19)
            {
                var e = _map.GetEntityOfDeath(x, y);
                if (e != null)
                {
                    if (Die())
                    {
                        var thePlayer = _map.ListOfPlayer.First(c => c.X == this.X && c.Y == this.Y);
                        _map.ListOfPlayer.Remove(thePlayer);
                        Texture._.DeleteTextureEntity(thePlayer);
                        Score._.KilledBy(e.Owner, thePlayer);
                        _map.CheckForAllDead();
                    }else
                    {
                        cling();
                    }
                }
            }
        }

        public void changeFace(BitmapImage bi)
        {
            Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => Source = bi));
        }

        public void cling()
        {
            clingnotement = 0;
            TimerManager._.AddNewTimer(true,100,true,null,theclingthread);
        }

        public void theclingthread(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            if(clingnotement <= 19)
            {
                Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(thecling));
            }else
            {
                var t = (Timer) sender;
                t.AutoReset = false;
            }
            
        }

        public void thecling()
        {
            if(clingnotement%2 == 1)
            {
                Opacity = 1;
            }else
            {
                Opacity = 0;
            }
            clingnotement++;
        }
    }
}