using System.Collections.Generic;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using BombEistiv2WPF.Environment;

namespace BombEistiv2WPF.View
{
    public class Menu
    {
        private Dictionary<string, string> _menuData;
        private Dictionary<string, BitmapImage> _menutextureList;
        private Dictionary<string, Image> _menuDataList;

        

        public Menu()
        {
            _menuData = GameParameters._.GetMenuTemplate();
            _menutextureList = new Dictionary<string, BitmapImage>();
            _menuDataList = new Dictionary<string, Image>();
        }


        public Dictionary<string, string> MenuData
        {
            get { return _menuData; }
        }

        public Dictionary<string, BitmapImage> MenutextureList
        {
            get { return _menutextureList; }
        }

        public Dictionary<string, Image> MenuDataList
        {
            get { return _menuDataList; }
        }

        public void LoadTeamBlui()
        {
            var g = new Image();
            g.Source = MenutextureList["Teamblui"];
            g.HorizontalAlignment = HorizontalAlignment.Center;
            g.VerticalAlignment = VerticalAlignment.Center;
            g.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
            g.Opacity = 0.0;
            g.Width = 400;
            g.Height = 400;
            MenuDataList.Add("Teamblui", g);
        }

        public void LoadAllSystemResources()
        {
            foreach (var td in MenuData)
            {
                if (td.Value.EndsWith(".png"))
                {
                    var u = new Uri(GameParameters.Path + td.Value);
                    var bitmanimg = new BitmapImage();
                    bitmanimg.BeginInit();
                    bitmanimg.UriSource = u;
                    bitmanimg.EndInit();
                    MenutextureList.Add(td.Key, bitmanimg);
                }
            }
        }
    }
}
