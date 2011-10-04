using System.Collections.Generic;

namespace BombEistiv2WPF.Environment
{
    public class Score
    {
        private Dictionary<int,int> _scoreMap;
        private int _owner;

        public Score(int id)
        {
            _owner = id;
            _scoreMap = new Dictionary<int, int>();
        }

        public int Owner
        {
            get { return _owner; }
        }

        public void Killed(Player p)
        {
            _scoreMap[p.Id] += 1;
        }

        public int GetScore()
        {
            var score = 0;
            foreach (var i in _scoreMap)
            {
                if (i.Key == Owner)
                {
                    score -= i.Value;
                }
                else
                {
                    score += i.Value;
                }
            }
            return score;
        }
    }
}