using System.Collections.Generic;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
            var g = new Image
                        {
                            Source = MenutextureList["Teamblui"],
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                            Margin = new Thickness(0.0, 0.0, 0.0, 0.0),
                            Opacity = 0.0,
                            Width = 400,
                            Height = 400
                        };
            MenuDataList.Add("Teamblui", g);
        }

        public void LoadPressStart(MainWindow mw)
        {
            var g = new Image
                        {
                            Source = MenutextureList["Sky"],
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Stretch,
                            Margin = new Thickness(0.0, 0.0, 0.0, 0.0),
                            Height = 1000,
                            Width = mw.Width*2
                        };
            MenuDataList.Add("Sky", g);
            var g2 = new Image
                         {
                             Source = MenutextureList["Bomb"],
                             HorizontalAlignment = HorizontalAlignment.Left,
                             VerticalAlignment = VerticalAlignment.Top,
                             Margin = new Thickness(mw.Height, -70, 0.0, 0.0),
                             Width = 600,
                             Height = 200
                         };
            var lt = new RotateTransform();
            lt.Angle = -15;
            g2.LayoutTransform = lt;
            MenuDataList.Add("Bomb", g2);
            var g3 = new Image
                         {
                             Source = MenutextureList["Eisti"],
                             HorizontalAlignment = HorizontalAlignment.Left,
                             VerticalAlignment = VerticalAlignment.Top,
                             Margin = new Thickness(100, -300, 0.0, 0.0),
                             Width = 600,
                             Height = 200
                         };
            MenuDataList.Add("Eisti", g3);
            var g4 = new Image
                         {
                             Source = MenutextureList["2"],
                             HorizontalAlignment = HorizontalAlignment.Left,
                             VerticalAlignment = VerticalAlignment.Top,
                             Margin = new Thickness(30, 200, 0.0, 0.0),
                             Width = 300,
                             Height = 300,
                             Opacity = 0
                         };
            MenuDataList.Add("2", g4);
            var g5 = new Image
                         {
                             Source = MenutextureList["PressStart"],
                             HorizontalAlignment = HorizontalAlignment.Center,
                             VerticalAlignment = VerticalAlignment.Bottom,
                             Margin = new Thickness(0.0, 0.0, 0.0, 0.0),
                             Width = 300,
                             Height = 100,
                             Opacity = 0
                         };
            MenuDataList.Add("PressStart", g5);
            var g6 = new Image
                         {
                             Source = MenutextureList["White"],
                             HorizontalAlignment = HorizontalAlignment.Stretch,
                             VerticalAlignment = VerticalAlignment.Top,
                             Margin = new Thickness(0.0, 0.0, 0.0, 0.0),
                             Opacity = 0,
                             Width = 2000,
                             Height = 2000
                         };
            MenuDataList.Add("White", g6);
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
