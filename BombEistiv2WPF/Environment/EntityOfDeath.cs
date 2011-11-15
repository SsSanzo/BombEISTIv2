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

        public bool Blocus
        {
            get { return blocus; }
            set { blocus = value; }
        }

        public EntityOfDeath(int x, int y, Game g, bool b = false) : base(x, y)
        {

            blocus = b;
            G = g;
            if (G.TheCurrentMap.GetEntityOfDeath(x,y) == null || (G.TheCurrentMap.GetEntityOfDeath(x,y) != null && !G.TheCurrentMap.GetEntityOfDeath(x,y).Blocus))
            {
                
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
                            if ((play.Clingnotement == 0 || play.Clingnotement > 19) && play.Die())
                            {
                                var thePlayer = G.TheCurrentMap.ListOfPlayer.First(c => c.X == play.X && c.Y == play.Y);
                                G.TheCurrentMap.ListOfPlayer.Remove(thePlayer);
                                Texture._.DeleteTextureEntity(thePlayer);
                            }else
                            {
                                play.cling();
                            }
                        }
                }
                TimerManager._.AddNewTimer(false, 500, true, null, Supress);
                TimerManager._.AddNewTimer(false, 900, true, null, Supress);
            }
            
        }

        public void SupressBlocus(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            this.Blocus = false;
        }

        public void Supress(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            G.TheCurrentMap.ListOfEntityOfDeath.Remove(this);
        }

        protected override bool Move(int x, int y)
        {
            return true;
        }
    }
}
