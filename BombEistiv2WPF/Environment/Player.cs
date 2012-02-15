using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BombEistiv2WPF.Control;
using BombEistiv2WPF.View;

namespace BombEistiv2WPF.Environment
{
    public class Player : Entity
    {

        private readonly int _id;
        private readonly List<Upgrade> _upgrades;
        private readonly List<UpgradeBomb> _upgradesBomb;
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
        private bool _invincible;
        private bool teleportme;
        private BombType bomb;
        private bool secAvaliable;
        private int stack;
        private bool freeze;
        private ImageSource oldSource;
        private bool cursed;
        private Bomb myBombTeleguide;

        public Player(int id, int skinid, int x, int y, Map map, Score score = null) : base(x, y)
        {
            _id = id;
            _skinid = skinid;
            _upgrades = new List<Upgrade>();
            _upgradesBomb = new List<UpgradeBomb>();
            _map = map;
            InitSkills();
        }

        public int Skinid
        {
            get { return _skinid; }
            set { if (value >= 1 && value <= 8) _skinid = value; }
        }

        public BombType BombSecond
        {
            get { return bomb; }
            set { bomb = value; }
        }

        public Bomb MyBombTeleguide
        {
            get { return myBombTeleguide; }
            set { myBombTeleguide = value; }
        }

        public int Stack
        {
            get { return stack; }
            set { stack = value; }
        }

        public bool Cursed
        {
            get { return cursed; }
            set { cursed = value; }
        }

        private void InitSkills()
        {
            Speed = 3;
            BombCount = 1;
            BombPower = 1;
            CanKick = false;
            InvertedDirections = false;
            AvailableBombCount = 1;
            Lives = GameParameters._.LivesCount;
            sens = Direction.Down;
            newsens = Direction.Down;
            _invincible = false;
            teleportme = false;
            bomb = BombType.None;
            secAvaliable = false;
            freeze = false;
            cursed = false;
        }

        public void InitCrazySkills()
        {
            Speed = 7;
            BombCount = 9;
            BombPower = 1;
            CanKick = true;
            InvertedDirections = false;
            AvailableBombCount = 9;
            Lives = GameParameters._.LivesCount;
            sens = Direction.Down;
            newsens = Direction.Down;
            _invincible = false;
            teleportme = false;
            bomb = BombType.None;
            secAvaliable = true;
            stack = 8;
            freeze = false;
            cursed = false;
        }

        //private void InitSkills()
        //{
        //    Speed = 9;
        //    BombCount = 9;
        //    BombPower = 10;
        //    CanKick = true;
        //    InvertedDirections = false;
        //    AvailableBombCount = 9;
        //    Lives = GameParameters._.LivesCount;
        //    sens = Direction.Down;
        //    newsens = Direction.Down;
        //    _invincible = false;
        //    teleportme = false;
        //}

        public bool Invincible
        {
            get { return _invincible; }
            set { _invincible = value;  }
        }

        public bool Freeze
        {
            get { return freeze; }
            set { freeze = value; }
        }

        public new int Percentx
        {
            set
            {
                if (InvertedDirections)
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
                                HaveToDie(X, Y);
                                GetMyUpgrade(X,Y);
                                _percentx = -49 + (value - 51);
                            }
                            else
                            {
                                _percentx = value;
                            }
                            if (teleportme)
                            {
                                teleportme = false;
                                Map.Teleport(this);
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
                                GetMyUpgrade(X, Y);
                                _percentx = 50 + (50 + value);
                            }
                            else
                            {
                                _percentx = value;
                            }
                            if (teleportme)
                            {
                                teleportme = false;
                                Map.Teleport(this);
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
                                GetMyUpgrade(X, Y);
                                _percenty = -49 + (value - 51);
                            }
                            else
                            {
                                _percenty = value;
                            }
                            if(teleportme)
                            {
                                teleportme = false;
                                Map.Teleport(this);
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
                                GetMyUpgrade(X, Y);
                                _percenty = 50 + (50 + value);
                            }
                            else
                            {
                                _percenty = value;
                            }
                            if (teleportme)
                            {
                                teleportme = false;
                                Map.Teleport(this);
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
                if(value >= 1 && value <= 9)
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
                if (value >= 1 && value <= 9)
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

        public Player AddAndApplyUpgradeBomb(UpgradeBomb u)
        {
            _upgradesBomb.Add(u);
            return ApplyUpgradeBomb(u);
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
                        teleportme = true;
                        break;
                    case UpgradeType.Life:
                        Lives++;
                        InGameMenu._.changeLabel(Id, Lives);
                        break;
            }
            return this;
        }

        public Player ApplyUpgradeBomb(UpgradeBomb u)
        {
            if(BombSecond == u.Type && this.stack > 2)
            {
                stack = stack/2;
                Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => InGameMenu._.BombStack[this.Id].Source = Texture._.TypetextureList["Stack_" + stack]));
                Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>InGameMenu._.BombStack[this.Id].Opacity = 1));
            }else
            {
                stack = 8;
                Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>InGameMenu._.BombStack[this.Id].Source = Texture._.TypetextureList["Stack_" + stack]));
                Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>InGameMenu._.BombStack[this.Id].Opacity = 1));
                this.BombSecond = u.Type;
                Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => InGameMenu._.BombImage[this.Id].Source = Texture._.TypetextureList["UpgradeBomb." + this.BombSecond]));
            }
            return this;
        }

        public Player BombExploded(Bomb b)
        {
            if(b.Owner == this && b.Type == BombType.Normal)
            {
                AvailableBombCount++;
            }
            return this;
        }

        public Bomb PutABomb(bool spec = false)
        {
            Bomb b = null;
            
            if(!spec)
            {
                var bm = (Bomb) _map.GetBomb(X, Y);
                if (AvailableBombCount >= 1 && (bm == null))
                {
                    b = new Bomb(X, Y, BombPower, this);
                    AvailableBombCount--;
                }
            }else if(secAvaliable && bomb != BombType.None)
            {
                if(bomb != BombType.Teleguide)
                {
                    b = new Bomb(X, Y, BombPower, this, true, BombSecond);
                    Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => InGameMenu._.BombImage[this.Id].Opacity = 0.4));
                    Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => InGameMenu._.cdLabel[this.Id].Content = Stack));
                    Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => InGameMenu._.cdLabel[this.Id].Opacity = 1));
                    secAvaliable = false;
                    TimerManager._.AddNewTimer(false, this.stack * 1000, true, null, beAvaliable);
                    TimerManager._.AddNewTimer(true, 1000, true, null, ChangeTheCD);
                }else
                {
                    b = new Bomb(X, Y, BombPower, this, false, BombSecond);
                    Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => InGameMenu._.BombImage[this.Id].Opacity = 0.4));
                    Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => InGameMenu._.cdLabel[this.Id].Content = Stack));
                    Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => InGameMenu._.cdLabel[this.Id].Opacity = 1));
                    secAvaliable = false;
                    MyBombTeleguide = b;
                    //TimerManager._.AddNewTimer(false, this.stack * 1000, true, null, beAvaliable);
                }
                
            }
            return b;
        }

        public void beAvaliableLag()
        {
            TimerManager._.AddNewTimer(false, this.stack * 1000, true, null, beAvaliable);
            TimerManager._.AddNewTimer(true, 1000, true, null, ChangeTheCD);
        }

        public void PutMultipleBomb()
        {
            if(secAvaliable)
            {
                var lb = _map.GetAroundEntity(X, Y);
                if (lb.FirstOrDefault(c => c.X == X - 1 && c.Y == Y) == null && X - 1 > -1)
                {
                    var bi = new Bomb(X - 1, Y, BombPower, this, true, BombType.Move);
                    ListenerGame._.GameInProgress.TheCurrentMap.ListOfBomb.Add(bi);
                    Texture._.InsertTextureEntity(bi);
                    bi.Move(Direction.Left);
                }
                if (lb.FirstOrDefault(c => c.X == X + 1 && c.Y == Y) == null && X + 1 < Game.Length)
                {
                    var bi = new Bomb(X + 1, Y, BombPower, this, true, BombType.Move);
                    ListenerGame._.GameInProgress.TheCurrentMap.ListOfBomb.Add(bi);
                    Texture._.InsertTextureEntity(bi);
                    bi.Move(Direction.Right);
                }
                if (lb.FirstOrDefault(c => c.X == X && c.Y == Y - 1) == null && Y - 1 > -1)
                {
                    var bi = new Bomb(X, Y - 1, BombPower, this, true, BombType.Move);
                    ListenerGame._.GameInProgress.TheCurrentMap.ListOfBomb.Add(bi);
                    Texture._.InsertTextureEntity(bi);
                    bi.Move(Direction.Up);

                }
                if (lb.FirstOrDefault(c => c.X == X && c.Y == Y + 1) == null && Y + 1 < Game.Length)
                {
                    var bi = new Bomb(X, Y + 1, BombPower, this, true, BombType.Move);
                    ListenerGame._.GameInProgress.TheCurrentMap.ListOfBomb.Add(bi);
                    Texture._.InsertTextureEntity(bi);
                    bi.Move(Direction.Down);
                }
                Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => InGameMenu._.BombImage[this.Id].Opacity = 0.4));
                Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => InGameMenu._.cdLabel[this.Id].Content = Stack));
                Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => InGameMenu._.cdLabel[this.Id].Opacity = 1));
                secAvaliable = false;
                TimerManager._.AddNewTimer(false, this.stack * 1000, true, null, beAvaliable);
                TimerManager._.AddNewTimer(true, 1000, true, null, ChangeTheCD);

            }    
            
        }

        public void beAvaliable(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            secAvaliable = true;
            Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => InGameMenu._.BombImage[this.Id].Opacity = 1));
        }

        public void ChangeTheCD(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => InGameMenu._.ChangeLabelTime((Timer)sender, this)));
        }

        public bool Die()
        {
            this.Lives--;
            if(cursed)
            {
                this.Lives = 0;
            }
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
                var b = (Bomb)e;
                if (CanKick && (b.Type == BombType.Normal || b.Type == BombType.Cataclysm || b.Type == BombType.Dark 
                    || b.Type == BombType.Flower || b.Type == BombType.Freeze || b.Type == BombType.Ghost))
                {
                    //b.Move(this.GetDirectionTo(x, y));
                    if(b.DirectionMoving == Direction.None)
                    {
                        b.Move(this.Sens);
                    }
                }
                if(b.Type != BombType.Mine)
                {
                    return false;
                }else if(b.Type == BombType.Mine)
                {
                    b.Explode(Texture._.Mw.GameInProgress);
                    return true;
                }
                
            }else if(e is EntityOfDeath)
            {
                return true;
            }
            else if(e is HardBlock || e is SoftBlock)
            {
                return false;
            }
            //else if(e is Upgrade)
            //{
            //    var u = (Upgrade) e;
            //    _map.PickupUpgrade(u);
            //    AddAndApplyUpgrade(u);
            //    u.Burn();
            //    return true;
            //}
            return true;
        }

        protected void HaveToDie(int x, int y)
        {
            if(!Invincible)
            {
                var e = _map.GetEntityOfDeath(x, y);
                if (e != null && e.IsHurting && e.Mode == 0)
                {
                    Score._.KilledBy(e.Owner, this);
                    if (Die())
                    {
                        var thePlayer = _map.ListOfPlayer.FirstOrDefault(c => c != null && c.X == this.X && c.Y == this.Y);
                        if(thePlayer != null)
                        {
                            _map.ListOfPlayer.Remove(thePlayer);
                            Texture._.DeleteTextureEntity(thePlayer);
                            if(GameParameters._.Type == GameType.Crazy)
                            {
                                Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => InGameMenu._.cdLabel[this.Id].Opacity = 0));
                                Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>InGameMenu._.BombImage[this.Id].Opacity = 0));
                                Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => InGameMenu._.BombStack[this.Id].Opacity = 0));
                            }
                            _map.CheckForAllDead();
                        }
                        
                    }else
                    {
                        cling();
                    }
                }
            }
            var b = (Bomb)_map.GetBomb(x, y);
            if(b != null && b.Type == BombType.Mine)
            {
                b.Explode(Texture._.Mw.GameInProgress);
            }
        }

        protected void GetMyUpgrade(int x, int y)
        {

            var e = _map.GetUpgrade(x, y);
            if(e != null)
            {
                if(e is Upgrade)
                {
                    var u = (Upgrade) e;
                    _map.PickupUpgrade(u);
                    AddAndApplyUpgrade(u);
                    u.Burn();
                }else if(e is UpgradeBomb)
                {
                    var bt = (UpgradeBomb)e;
                    _map.PickupUpgrade(bt);
                    AddAndApplyUpgradeBomb(bt);
                    bt.Burn();
                }
                    
            }
            
        }

        public void changeFace(BitmapImage bi)
        {
            Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => Source = bi));
        }

        public void cling()
        {
            _invincible = true;
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
                _invincible = false;
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

        public void freezePlayer()
        {
            if(!freeze)
            {
                this.freeze = true;
                oldSource = this.Source;
                this.Source = Texture._.TypetextureList["IceCube"];
                TimerManager._.AddNewTimer(false, 3000, true, null, unfreezeDispatcher);
            }
            
        }

        public void unfreezeDispatcher(object sender, ElapsedEventArgs elapsedEventArgs)
        {

            Texture._.Mw.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(unfreeze));
        }

        public void unfreeze()
        {
            this.Source = oldSource;
            this.freeze = false;
        }

        public void cursePlayer()
        {
            if (!cursed)
            {
                this.cursed = true;
                TimerManager._.AddNewTimer(false, 5000, true, null, uncurseDispatcher);
            }

        }

        public void uncurseDispatcher(object sender, ElapsedEventArgs elapsedEventArgs)
        {

            Texture._.Mw.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(uncurse));
        }

        public void uncurse()
        {
            this.Opacity = 1;
            this.cursed = false;
        }
    }
}