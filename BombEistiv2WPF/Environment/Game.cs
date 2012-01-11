using System;
using System.Collections.Generic;
using System.Linq;
using BombEistiv2WPF.Properties;
using BombEistiv2WPF.View;

namespace BombEistiv2WPF.Environment
{
    public class Game
    {
        private static int _length = 0;
        //private List<Entity> _toDelete;
        public Map TheCurrentMap;

        public static int Length
        {
            get
            {
                if (_length == 0)
                {
                    _length = Convert.ToInt32(Resources.MapLength);
                }
                return _length;
            }
        }

        public void EventManager(TimerEvent timerEvent)
        {
            if(timerEvent != null)
            {
                switch (timerEvent.Type)
                {
                    case EventType.BombExplode:
                        var b = (Bomb)timerEvent.InvolvedObject;
                        if (TheCurrentMap.GetBomb(b.X, b.Y) != null)
                        {
                            b.Explode(this);
                            //EmptyTheTrash(TheCurrentMap);
                        }
                        break;
                    case EventType.BombMove:
                        var be = (Bomb)timerEvent.InvolvedObject;
                        if (!be.Move())
                        {
                            timerEvent.Timer.Stop();
                        }
                        break;
                    case EventType.Malediction:
                        var p = (Player)timerEvent.InvolvedObject;
                        p.InvertedDirections = false;
                        p.changeFace(Texture._.TypetextureList[Texture._.GetTextureKey(p)]);
                        break;
                }
            }
            
        }

        public void InitPlayers()
        {
            if (TheCurrentMap != null)
            {
                if (GameParameters._.PlayerCount > 0)
                {
                    TheCurrentMap.ListOfPlayer.Add(new Player(1,GameParameters._.PlayerSkin[1], 0, 0, TheCurrentMap));
                }
                if (GameParameters._.PlayerCount > 1)
                {
                    TheCurrentMap.ListOfPlayer.Add(new Player(2, GameParameters._.PlayerSkin[2], Length - 1, Length - 1, TheCurrentMap));
                }
                if (GameParameters._.PlayerCount > 2)
                {
                    TheCurrentMap.ListOfPlayer.Add(new Player(3, GameParameters._.PlayerSkin[3], 0, Length - 1, TheCurrentMap));
                }
                if (GameParameters._.PlayerCount > 3)
                {
                    TheCurrentMap.ListOfPlayer.Add(new Player(4, GameParameters._.PlayerSkin[4], Length - 1, 0, TheCurrentMap));
                }
            }
            else
            {
                throw new Exception("Problem : Map is not initialized");
            }

        }

        //public void EmptyTheTrash(Map m)
        //{
        //    foreach (var e in ToDelete)
        //    {
        //        //if (e is Bomb)
        //        //{
        //        //    var theBomb = m.ListOfBomb.First(c => c.X == e.X && c.Y == e.Y);
        //        //    m.ListOfBomb.Remove(theBomb);
                    
        //        //}
        //        //else
        //        //if (e is Player)
        //        //{
        //        //    var thePlayer = m.ListOfPlayer.First(c => c.X == e.X && c.Y == e.Y);
        //        //    if (thePlayer.Die())
        //        //    {
        //        //        m.ListOfPlayer.Remove(thePlayer);
        //        //        Texture._.DeleteTextureEntity(e);
        //        //    }
        //        //}
        //        //else 
        //        if (e is SoftBlock)
        //        {
        //            var theSoftBlock = m.ListOfSoftBlock.First(c => c.X == e.X && c.Y == e.Y);
        //            theSoftBlock.Destroy(m);
        //            m.ListOfSoftBlock.Remove(theSoftBlock);
        //            Texture._.DeleteTextureEntity(e);
        //        }
        //        else if (e is Upgrade)
        //        {
        //            var theUpgrade = m.ListOfUpgrade.First(c => c.X == e.X && c.Y == e.Y);
        //            theUpgrade.Burn();
        //            m.ListOfUpgrade.Remove(theUpgrade);
        //            Texture._.DeleteTextureEntity(e);
        //        }
                
        //    }
        //    ToDelete.Clear();
        //}
    }
}