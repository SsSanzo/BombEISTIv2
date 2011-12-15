using System.Collections.Generic;

namespace BombEistiv2WPF.Environment
{
    public class Score
    {
        private Dictionary<int,int> _scoreMap;
        private Dictionary<int, int> _surviveMap;
        private static Score _this;
        private List<int> _id_Victory;

        private Score()
        {
            _scoreMap = new Dictionary<int, int>();
            _surviveMap = new Dictionary<int, int>();
            _id_Victory = new List<int>(); ;
        }

        public static Score _
        {
            get { return _this ?? (_this = new Score()); }
        }

        public List<int> Id_Victory
        {
            get { return _id_Victory; }
        }


        public void KilledBy(Player killer, Player killed)
        {
            CheckExistance(killer.Id);
            if(killer.Id != killed.Id)
            {
                _scoreMap[killer.Id] += 1;
            }
            
        }

        public void Survived(Player p)
        {
            CheckExistanceSurvive(p.Id);
            _surviveMap[p.Id] += 1;
            _id_Victory.Add(p.Id);
        }

        public void ResetSurvived()
        {
            _id_Victory.Clear();
        }

        public void ResetScore()
        {
            _scoreMap.Clear();
            _surviveMap.Clear();
        }

        public void CheckExistance(int id)
        {
            if(!_scoreMap.ContainsKey(id))
            {
                _scoreMap.Add(id, 0);
            }
        }

        public void CheckExistanceSurvive(int id)
        {
            if (!_surviveMap.ContainsKey(id))
            {
                _surviveMap.Add(id, 0);
            }
        }

        public int GetScore(Player p)
        {
            CheckExistance(p.Id);
            return _scoreMap[p.Id];
        }

        public int GetScore(int id)
        {
            CheckExistance(id);
            return _scoreMap[id];
        }

        public int GetSurvive(Player p)
        {
            CheckExistanceSurvive(p.Id);
            return _surviveMap[p.Id];
        }

        public int GetSurvive(int id)
        {
            CheckExistanceSurvive(id);
            return _surviveMap[id];
        }
    }
}