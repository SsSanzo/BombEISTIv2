using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace BombEistiv2WPF.Environment
{
    public class GameParameters
    {
        //Path Clement
        //public static string Path = @"C:\Users\auffraycle\Documents\Visual Studio 2010\Projects\BombEISTIv2\BombEISTIv2\BombEistiv2WPF";
        //Path Benou
        public static string Path = @"D:\My Documents\BombEISTIv2\BombEistiv2WPF";

        private const string ParameterPath = "../../config.xml";
        private readonly XDocument _xmlDoc;
        private readonly IEnumerable<XElement> _root;

        private XElement _theme;

        private static GameParameters _this;

        private int _playerCount; // Entre 2 et 4
        private int _gameTime; // en secondes
        private int _explosionDelay;


        private GameParameters()
        {
            _xmlDoc = XDocument.Load(ParameterPath);
            _root = _xmlDoc.Descendants("GameParameters");
            Type = GameType.Classic;
            GameTime = 300;
            Network = Network.Local;
            Theme = "Basic";
        }

        public static GameParameters _
        {
            get { return _this ?? (_this = new GameParameters()); }
        }

        public Network Network { get; set; }

        public int ExplosionDelay
        {
            get { return _explosionDelay; }
            set
            {
                if (0 < value && value < 6)
                {
                    _explosionDelay = value;
                }
            }
        }

        public int GameTime
        {
            get { return _gameTime; }
            set
            {
                if (0 < value && value < 600)
                {
                    _gameTime = value;
                }
            }
        }

        public int PlayerCount
        {
            get { return _playerCount; }
            set
            {
                if (1 < value && value < 5)
                {
                    _playerCount = value;
                }
            }
        }

        public GameType Type { get; set; }

        public Dictionary<UpgradeType, int> GetAllUpgrades()
        {
            var upgrades = new Dictionary<UpgradeType, int>();
            var e = _root.Where(c => String.Compare(Type.ToString(), c.Element("Game").Attribute("type").Value) == 0).Descendants("Upgrades");
            foreach (var element in e)
            {
                UpgradeType ut;
                if (Enum.TryParse(element.Value, out ut))
                {
                    var i = Convert.ToInt32(element.Attribute("currentFreq"));
                    upgrades.Add(ut, i);
                }
            }
            return upgrades;
        }

        public Dictionary<string, string> GetMenuTemplate()
        {
            var menu = _root.Descendants("CommonParameter").Elements("Menu").FirstOrDefault();
            var e = menu.Descendants();
            var folder = menu.Attribute("folder").Value;
            return e.ToDictionary(element => element.Attribute("object").Value, element => @"\" + folder + element.Attribute("source").Value);

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
            var e = _root.Where(c => String.Compare(Type.ToString(), c.Attribute("type").Value) == 0).Descendants("Upgrades").FirstOrDefault(c => String.Compare(ut.ToString(), c.Element("Upgrade").Value) == 0);
            e.Attribute("currentFreq").Value = freq + "";
        }

        public Dictionary<string, string> GetThemeData(string Theme)
        {
            var e = _theme.Descendants();
            var folder = _theme.Attribute("folder").Value;
            return e.ToDictionary(element => element.Attribute("object").Value, element => @"\" + folder + Theme + @"\" + element.Attribute("source").Value);
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
                    _root.Descendants("Themes").Elements("Theme").Where(c => String.Compare(value, c.Attribute("name").Value) == 0).
                        FirstOrDefault();
                _theme = e;
            }
        }

        public int SoftBlocCount
        {
            get
            {
                var e = _root.Descendants("CommonParameter").Elements("SoftBlock").FirstOrDefault();
                return Convert.ToInt32(e.Attribute("count").Value);
            }
            set
            {
                var e = _root.Descendants("CommonParameter").Elements("SoftBlock").FirstOrDefault();
                e.Attribute("count").Value = value + "";
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