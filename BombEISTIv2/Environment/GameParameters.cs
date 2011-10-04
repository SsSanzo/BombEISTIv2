using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace BombEISTIv2.Environment
{
    public class GameParameters
    {
        private const string ParameterPath = "config.xml";
        private readonly XDocument _xmlDoc;
        private readonly IEnumerable<XElement> _root;

        private XElement _theme;
        
        private static GameParameters _gameParameters;

        private int _numberOfPlayer; // Entre 2 et 4
        private int _gameTime; // en secondes


        private GameParameters()
        {
            _xmlDoc = XDocument.Load(ParameterPath);
            _root = _xmlDoc.Descendants("GameParameter");
            Type = GameType.Classic;
            GameTime = 300;
            Network = Network.Local;
        }

        public static GameParameters Parameters
        {
            get { return _gameParameters ?? (_gameParameters = new GameParameters()); }
        }

        public Network Network { get; set; }

        public int GameTime
        {
            get { return _gameTime; }
            set
            {
                if(0 < value && value < 600)
                {
                    _gameTime = value;
                }
            }
        }

        public int NumberOfPlayer
        {
            get { return _numberOfPlayer; }
            set
            {
                if (1 < value && value <5)
                {
                    _numberOfPlayer = value;
                }
            }
        }

        public GameType Type { get; set; }

        public Dictionary<UpgradeType,int> GetAllUpgrades()
        {
            var upgrades = new Dictionary<UpgradeType,int>();
            var e = _root.Where(c => String.Compare(Type.ToString(), c.Attribute("type").Value) == 0).Descendants("Upgrades");
            foreach (var element in e)
            {
                UpgradeType ut;
                if(Enum.TryParse(element.Value, out ut))
                {
                    var i = Convert.ToInt32(element.Attribute("currentFreq"));
                    upgrades.Add(ut, i);
                }
            }
            return upgrades;
        }

        public void ResetUpgradeFrequence()
        {
            var e = _root.Where(c => String.Compare(Type.ToString(), c.Attribute("type").Value) == 0).Descendants("Upgrades");
            foreach (var element in e)
            {
                element.Attribute("currentFreq").Value = element.Attribute("defaultFreq").Value;
            }
        }

        public void ChangeUpgradeFrequence(UpgradeType ut, int freq)
        {
            var e = _root.Where(c => String.Compare(Type.ToString(), c.Attribute("type").Value) == 0).Descendants("Upgrades").FirstOrDefault(c => String.Compare(ut.ToString(),c.Element("Upgrade").Value) == 0);
            e.Attribute("currentFreq").Value = freq + "";
        }

        public Dictionary<string,string> GetThemeData()
        {
            var e = _theme.Descendants();
            var folder = _theme.Attribute("folder");
            var data = new Dictionary<string, string>();
            foreach (var element in e)
            {
                data.Add(element.Attribute("object").Value,folder + @"\" + element.Attribute("source").Value);
            }
            return data;
        }

        public List<string> GetThemes()
        {
            return _root.Descendants("Themes").Select(element => element.Attribute("name").Value).ToList();
        }

        public void Save()
        {
            _xmlDoc.Save(ParameterPath);
        }

        public string Theme
        {
            get { return _theme.Attribute("name").Value; }
            set
            {
                var e =
                    _root.Descendants("Themes").Where(c => String.Compare(value, c.Attribute("name").Value) == 0).
                        FirstOrDefault();
                _theme = e;
            }
        }

        public int SoftBlocCount
        {
            get
            {
                var e = _root.Descendants("CommonParameter").Elements("SoftBloc").FirstOrDefault();
                return Convert.ToInt32(e.Attribute("count").Value);
            }
            set
            {
                var e = _root.Descendants("CommonParameter").Elements("SoftBloc").FirstOrDefault();
                e.Attribute("count").Value = value+"";
            }
        }

        public int LivesCount
        {
            get
            {
                var e = _root.Where(c => String.Compare(Type.ToString(), c.Attribute("type").Value) == 0).DescendantsAndSelf().FirstOrDefault();
                return Convert.ToInt32(e.Attribute("lives").Value);
            }
            set
            {
                var e = _root.Where(c => String.Compare(Type.ToString(), c.Attribute("type").Value) == 0).DescendantsAndSelf().FirstOrDefault();
                e.Attribute("lives").Value = value + "";
            }
        }
    }

    public enum GameType
    {
        Crazy, Hardcore, Classic
    }

    public enum Network
    {
        Local, Lan, Internet
    }
}
