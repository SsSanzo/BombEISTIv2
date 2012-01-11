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

        public bool Blocus
        {
            get { return blocus; }
            set { blocus = value; }
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

        public EntityOfDeath(int x, int y, Game g, Player o, bool b = false) : base(x, y)
        {
            thereIsOne = false;
            blocus = b;
            hurt = true;
            G = g;
            owner = o;
            if (G.TheCurrentMap.GetEntityOfDeath(x,y) == null || (G.TheCurrentMap.GetEntityOfDeath(x,y) != null && !G.TheCurrentMap.GetEntityOfDeath(x,y).Blocus))
            {
                if(G.TheCurrentMap.GetEntityOfDeath(x,y) != null)
                {
                    G.TheCurrentMap.ListOfEntityOfDeath.Remove(G.TheCurrentMap.GetEntityOfDeath(x, y));
                }
                G.TheCurrentMap.ListOfEntityOfDeath.Add(this);
                var listEntity = new List<Entity>();
                listEntity.AddRange(G.TheCurrentMap.GetCompleteList());
                foreach (var e in listEntity.Where(play => play.X == x && play.Y == y))
                {
                    if (e is Bomb)
                    {
                        var theBomb = G.TheCurrentMap.ListOfBomb.First(c => c.X == e.X && c.Y == e.Y);
                        theBomb.Explode(G);
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
                        else if (e is Player)
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
                    if(thereIsOne)
                    {
                        G.TheCurrentMap.CheckForAllDead();
                    }
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
