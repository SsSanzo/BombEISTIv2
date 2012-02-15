using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using BombEistiv2WPF.View;

namespace BombEistiv2WPF.Environment
{
    public class EntityOfDeath : Entity
    {
        private Game G;
        private bool blocus;
        private bool hurt;
        private Player owner;
        private bool thereIsOne;
        private int mode;

        public bool Blocus
        {
            get { return blocus; }
            set { blocus = value; }
        }

        public int Mode
        {
            get { return mode; }
            set { mode = value; }
        }

        public bool IsHurting
        {
            get { return hurt; }
            set { hurt = value; }
        }

        public Player Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        public EntityOfDeath(int x, int y, Game g, Player o, bool b = false, int mod = 0) : base(x, y)
        {
            thereIsOne = false;
            blocus = b;
            hurt = true;
            G = g;
            owner = o;
            mode = mod;
            if (G.TheCurrentMap.GetEntityOfDeath(x,y) == null || (G.TheCurrentMap.GetEntityOfDeath(x,y) != null && !G.TheCurrentMap.GetEntityOfDeath(x,y).Blocus))
            {
                if(G.TheCurrentMap.GetEntityOfDeath(x,y) != null)
                {
                    try
                    {
                        G.TheCurrentMap.ListOfEntityOfDeath.Remove(G.TheCurrentMap.GetEntityOfDeath(x, y));
                    }catch(Exception e)
                    {
                        
                    }
                }
                G.TheCurrentMap.ListOfEntityOfDeath.Add(this);
                var listEntity = new List<Entity>();
                listEntity.AddRange(G.TheCurrentMap.GetCompleteList());
                foreach (var e in listEntity.Where(play => play.X == x && play.Y == y))
                {
                    if (e is Bomb)
                    {
                        var bt = (Bomb) e;
                        if(GameParameters._.Type == GameType.Classic)
                        {
                            var theBomb = G.TheCurrentMap.ListOfBomb.FirstOrDefault(c => c.X == e.X && c.Y == e.Y);
                            if (theBomb != null) theBomb.Explode(G);
                        }else
                        if(bt.Type != BombType.Mine)
                        {
                            switch (bt.Type)
                            {
                                case BombType.Normal:
                                    var theBomb = G.TheCurrentMap.ListOfBomb.FirstOrDefault(c => c.X == e.X && c.Y == e.Y);
                                    if (theBomb != null) theBomb.Explode(G);
                                    break;
                                case BombType.Teleguide:
                                    var theBomb7 = G.TheCurrentMap.ListOfBomb.FirstOrDefault(c => c.X == e.X && c.Y == e.Y);
                                    if (theBomb7 != null)
                                    {
                                        theBomb7.Owner.MyBombTeleguide = null;
                                        theBomb7.Owner.beAvaliableLag();
                                        theBomb7.Explode(G);
                                    }
                                    break;
                                case BombType.Cataclysm:
                                    var theBomb2 = G.TheCurrentMap.ListOfBomb.FirstOrDefault(c => c.X == e.X && c.Y == e.Y);
                                    if (theBomb2 != null) theBomb2.ExplodeCata(G);
                                    break;
                                case BombType.Flower:
                                    var theBomb3 = G.TheCurrentMap.ListOfBomb.FirstOrDefault(c => c.X == e.X && c.Y == e.Y);
                                    if (theBomb3 != null) theBomb3.ExplodeFlower(G);
                                    break;
                                case BombType.Freeze:
                                    var theBomb4 = G.TheCurrentMap.ListOfBomb.FirstOrDefault(c => c.X == e.X && c.Y == e.Y);
                                    if (theBomb4 != null) theBomb4.ExplodeFreeze(G);
                                    break;
                                case BombType.Dark:
                                    var theBomb6 = G.TheCurrentMap.ListOfBomb.FirstOrDefault(c => c.X == e.X && c.Y == e.Y);
                                    if (theBomb6 != null) theBomb6.ExplodeDark(G);
                                    break;
                                default:
                                    var theBomb5 = G.TheCurrentMap.ListOfBomb.FirstOrDefault(c => c.X == e.X && c.Y == e.Y);
                                    if (theBomb5 != null) theBomb5.Explode(G);
                                    break;
                            }
                            
                        }
                        
                    }
                    else
                        if (e is SoftBlock)
                        {
                            var theSoftBlock = G.TheCurrentMap.ListOfSoftBlock.First(c => c.X == e.X && c.Y == e.Y);
                            theSoftBlock.Destroy(G.TheCurrentMap);
                            G.TheCurrentMap.ListOfSoftBlock.Remove(theSoftBlock);
                            Texture._.DeleteTextureEntity(e);
                        }
                        else if (e is Upgrade)
                        {
                            var theUpgrade = G.TheCurrentMap.ListOfUpgrade.First(c => c.X == e.X && c.Y == e.Y);
                            theUpgrade.Burn();
                            G.TheCurrentMap.ListOfUpgrade.Remove(theUpgrade);
                            Texture._.DeleteTextureEntity(e);
                        }
                        else if (e is UpgradeBomb)
                        {
                            var theUpgradeBomb = G.TheCurrentMap.ListOfUpgradeBomb.First(c => c.X == e.X && c.Y == e.Y);
                            theUpgradeBomb.Burn();
                            G.TheCurrentMap.ListOfUpgradeBomb.Remove(theUpgradeBomb);
                            Texture._.DeleteTextureEntity(e);
                        }
                        else if (e is Player)
                        {
                            if(mode == 0)
                            {
                                var play = (Player)e;
                                if (!(play.Invincible))
                                {
                                    Score._.KilledBy(owner, play);
                                    if (play.Die())
                                    {
                                        var thePlayer = G.TheCurrentMap.ListOfPlayer.FirstOrDefault(c => c.X == play.X && c.Y == play.Y);
                                        if (thePlayer != null)
                                        {
                                            G.TheCurrentMap.ListOfPlayer.Remove(thePlayer);
                                            Texture._.DeleteTextureEntity(thePlayer);
                                            thereIsOne = true;
                                        }
                                    }
                                    else
                                    {
                                        play.cling();
                                    }
                                }
                            }
                            else if (mode == 1)
                            {
                                var play = (Player)e;
                                if (e != null)
                                {
                                    play.freezePlayer();
                                }
                            }
                            else if (mode == 2)
                            {
                                var play = (Player)e;
                                if (e != null)
                                {
                                    play.cursePlayer();
                                }
                            }
                            
                        }
                }
                if (thereIsOne)
                {
                    G.TheCurrentMap.CheckForAllDead();
                }
            }
            TimerManager._.AddNewTimer(false, 500, true, null, SupressBlocus);
            TimerManager._.AddNewTimer(false, 700, true, null, Supress);
        }

        public void SupressBlocus(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            this.Blocus = false;
        }

        public void Supress(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            hurt = false;
            if(mode != 0)
            {
                Texture._.DeleteTextureEntity(this);
            }
            try
            {
                G.TheCurrentMap.ListOfEntityOfDeath.Remove(this);
            }catch
            {
                TimerManager._.AddNewTimer(false, 50, true, null, Supress);
            }
            
        }

        protected override bool Move(int x, int y)
        {
            return true;
        }
    }
}
