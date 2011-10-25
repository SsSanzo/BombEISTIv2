using System;
using System.Collections.Generic;
using System.Linq;
using BombEistiv2WPF.Properties;

namespace BombEistiv2WPF.Environment
{
    public class Game
    {
        private static int _length = 0;
        private List<Entity> _toDelete;
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
            switch (timerEvent.Type)
            {
                case EventType.BombExplode:
                    var b = (Bomb)timerEvent.InvolvedObject;
                    b.Explode(TheCurrentMap, this);
                    break;
            }
        }

        public List<Entity> ToDelete
        {
            set { _toDelete = value; }
            get { return _toDelete; }
        }

        public void InitPlayers(int numberOPlayer)
        {
            if (TheCurrentMap != null)
            {
                if (numberOPlayer > 0)
                {
                    TheCurrentMap.ListOfPlayer.Add(new Player(1,1, 0, 0, TheCurrentMap));
                }
                if (numberOPlayer > 1)
                {
                    TheCurrentMap.ListOfPlayer.Add(new Player(2,2, Length - 1, 0, TheCurrentMap));
                }
                if (numberOPlayer > 2)
                {
                    TheCurrentMap.ListOfPlayer.Add(new Player(3, 3, 0, Length - 1, TheCurrentMap));
                }
                if (numberOPlayer > 3)
                {
                    TheCurrentMap.ListOfPlayer.Add(new Player(4,4, Length - 1, Length - 1, TheCurrentMap));
                }
            }
            else
            {
                throw new Exception("Problem : Map is not initialized");
            }

        }

        public void EmptyTheTrash(Map m)
        {
            foreach (var e in ToDelete)
            {
                if (e is Bomb)
                {
                    var theBomb = m.ListOfBomb.First(c => c.X == e.X && c.Y == e.Y);
                    m.ListOfBomb.Remove(theBomb);

                }
                else if (e is Player)
                {
                    var thePlayer = m.ListOfPlayer.First(c => c.X == e.X && c.Y == e.Y);
                    thePlayer.Die();
                    m.ListOfPlayer.Remove(thePlayer);
                }
                else if (e is SoftBlock)
                {
                    var theSoftBlock = m.ListOfSoftBlock.First(c => c.X == e.X && c.Y == e.Y);
                    theSoftBlock.Destroy(m);
                    m.ListOfSoftBlock.Remove(theSoftBlock);
                }
                else if (e is Upgrade)
                {
                    var theUpgrade = m.ListOfUpgrade.First(c => c.X == e.X && c.Y == e.Y);
                    theUpgrade.Burn();
                    m.ListOfUpgrade.Remove(theUpgrade);
                }
            }
            ToDelete.Clear();
        }
    }
}