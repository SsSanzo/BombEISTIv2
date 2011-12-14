



using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace BombEistiv2WPF.Environment
{
    public class GameParameters
    {
        //Path Clement
        //public static string Path = @"C:\Users\auffraycle\Documents\Visual Studio 2010\Projects\BombEISTIv2\BombEISTIv2\BombEistiv2WPF";
        //Path Benou

        //public static string Path = @"D:\My Documents\BombEISTIv2\BombEistiv2WPF";
        //private const string ParameterPath = "../../config.xml";

        public static string Path;
        private const string ParameterPath = "config.xml";

        private readonly XDocument _xmlDoc;
        private readonly IEnumerable<XElement> _root;

        private XElement _theme;

        private static GameParameters _this;

        private int _playerCount; // Entre 2 et 4
        private Dictionary<int, int> _playerSkin; // Entre 2 et 4
        private int _gameTime; // en secondes
        private int _explosionDelay;

        private Dictionary<string, BitmapImage> _menutextureList;

        private GameParameters()
        {
            Path = System.Environment.CurrentDirectory;
            _xmlDoc = XDocument.Load(ParameterPath);
            _root = _xmlDoc.Descendants("GameParameters");
            Type = GameType.Classic;
            GameTime = Convert.ToInt32(_root.Elements("CommonParameter").Elements("GameTime").FirstOrDefault().Attribute("count").Value);
            Network = Network.Local;
            Theme = "Basic";
            _menutextureList = new Dictionary<string, BitmapImage>();
            ExplosionDelay = Convert.ToInt32(_root.Elements("CommonParameter").Elements("ExplosionDelay").FirstOrDefault().Attribute("count").Value);
            LoadAllSystemResources();
        }

        public static GameParameters _
        {
            get { return _this ?? (_this = new GameParameters()); }
        }

        public Network Network { get; set; }

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

        public Dictionary<int, int> PlayerSkin
        {
            get { return _playerSkin; }
            set
            {
                _playerSkin = value;
            }
        }

        public Dictionary<Key, string> GetGameKeyMap()
        {
            var d = new Dictionary<Key, string>();
            var e = _root.Descendants("keysMap");
            foreach (var x in e.Elements("Map"))
            {
                Key k;
                Key.TryParse(x.Attribute("key").Value, true, out k);
                d.Add(k, x.Attribute("player").Value + "_" + x.Attribute("action").Value);
            }
            return d;
        }

        

        public void SaveGameKeyMap(Dictionary<Key, string> keyMap)
        {
            var e = _root.Descendants("keysMap");
            foreach (var x in e.Elements("Map"))
            {
                foreach (var i in keyMap)
                {
                    if (i.Value == x.Attribute("player").Value + "_" + x.Attribute("action").Value)
                    {
                        x.Attribute("key").Value = i.Key.ToString();
                    }
                }
            }
            Save();
        }

        public Dictionary<string, BitmapImage> MenutextureList
        {
            get { return _menutextureList; }
        }

        public GameType Type { get; set; }

        public Dictionary<UpgradeType, int> GetAllUpgrades()
        {
            var upgrades = new Dictionary<UpgradeType, int>();
            var d = _root.Descendants("Game").Where(c => c.Attribute("type").Value == Type.ToString());
            var e = d.Descendants("Upgrades").Elements("Upgrade");
            foreach (var element in e)
            {
                UpgradeType ut;
                if (Enum.TryParse(element.Value, out ut))
                {
                    var i = Convert.ToInt32(element.Attribute("currentFreq").Value);
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

        public void LoadAllSystemResources()
        {
            foreach (var td in GetMenuTemplate())
            {
                if (td.Value.EndsWith(".png"))
                {
                    var u = new Uri(Path + td.Value);
                    var bitmanimg = new BitmapImage();
                    bitmanimg.BeginInit();
                    bitmanimg.UriSource = u;
                    bitmanimg.EndInit();
                    MenutextureList.Add(td.Key, bitmanimg);
                }
            }
        }

        public void ResetUpgradeFrequence()
        {
            var e = _root.Elements("Game").Where(c => String.Compare(Type.ToString(), c.Attribute("type").Value) == 0).Descendants("Upgrades");
            foreach (var element in e.Elements("Upgrade"))
            {
                element.Attribute("currentFreq").Value = element.Attribute("defaultFreq").Value;
            }
        }

        public void ChangeUpgradeFrequence(UpgradeType ut, int freq)
        {
            var e = _root.Elements("Game").Where(c => String.Compare(Type.ToString(), c.Attribute("type").Value) == 0).Descendants("Upgrades");//.FirstOrDefault(c => String.Compare(ut.ToString(), c.Element("Upgrade").Value) == 0);
            XElement theut = null;
            foreach (var element in e.Elements("Upgrade").Where(element => String.Compare(ut.ToString(), element.Value) == 0))
            {
                theut = element;
            }
            if(theut != null)
            {
                theut.Attribute("currentFreq").Value = freq + "";
            }
            
        }

        public Dictionary<string, string> GetThemeData(string Theme)
        {
            var e = _theme.Descendants();
            var folder = _theme.Attribute("folder").Value;
            var dic = new Dictionary<string, string>();
            foreach (var element in e)
            {
                //File.OpenRead(Path + @"\" + folder + Theme + @"\" + element.Attribute("source").Value);
                if (File.Exists(Path + @"\" + folder + Theme + @"\" + element.Attribute("source").Value))
                {
                    dic.Add(element.Attribute("object").Value, @"\" + folder + Theme + @"\" + element.Attribute("source").Value);
                }else
                {
                    dic.Add(element.Attribute("object").Value, @"\" + folder + "Basic" + @"\" + element.Attribute("source").Value);
                }
            }
            return dic;
            //return e.ToDictionary(element => element.Attribute("object").Value, element => @"\" + folder + Theme + @"\" + element.Attribute("source").Value);
        }

        public String GetThemeFolder()
        {
            return _theme.Attribute("folder").Value;
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
                var e = _root.Elements("Game").Where(c => String.Compare(Type.ToString(), c.Attribute("type").Value) == 0).DescendantsAndSelf().FirstOrDefault();
                return Convert.ToInt32(e.Element("Player").Attribute("lives").Value);
            }
            set
            {
                var e = _root.Elements("Game").Where(c => String.Compare(Type.ToString(), c.Attribute("type").Value) == 0).DescendantsAndSelf().FirstOrDefault();
                e.Element("Player").Attribute("lives").Value = value + "";
            }
        }

        public int ExplosionDelay
        {
            get
            {
                var e = _root.Descendants("CommonParameter").Elements("ExplosionDelay").FirstOrDefault();
                _explosionDelay = Convert.ToInt32(e.Attribute("count").Value); 
                return _explosionDelay;
            }
            set
            {
                if (0 < value && value < 6)
                {
                    var e = _root.Descendants("CommonParameter").Elements("ExplosionDelay").FirstOrDefault();
                    e.Attribute("count").Value = value + "";
                    _explosionDelay = value;
                }
            }
        }

        public int GameTime
        {
            get
            {
                var e = _root.Descendants("CommonParameter").Elements("GameTime").FirstOrDefault();
                _gameTime = Convert.ToInt32(e.Attribute("count").Value); 
                return _gameTime;
            }
            set
            {
                if (0 < value && value < 600)
                {
                    var e = _root.Descendants("CommonParameter").Elements("GameTime").FirstOrDefault();
                    e.Attribute("count").Value = value + "";
                    _gameTime = value;
                }
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