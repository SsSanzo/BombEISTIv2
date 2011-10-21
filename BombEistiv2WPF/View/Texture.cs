using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BombEistiv2WPF.Environment;

namespace BombEistiv2WPF.View
{
    public class Texture
    {
        private Dictionary<string, string> _themeData;
        private Dictionary<string, BitmapImage> _typetextureList;
        private Dictionary<Entity, System.Windows.Controls.Image> _textureList; 

        public Texture(Dictionary<string, string> themeData)
        {
            _themeData = themeData;
            _textureList = new Dictionary<Entity, Image>();
            _typetextureList = new Dictionary<string, BitmapImage>();
        }

        public Dictionary<string, string> ThemeData
        {
            get { return _themeData; }
        }

        public Dictionary<Entity, System.Windows.Controls.Image> TextureList
        {
            get { return _textureList; }
        }

        public Dictionary<string, BitmapImage> TypetextureList
        {
            get { return _typetextureList; }
        }

        public void LoadTextureList(List<Entity> l)
        {
            foreach (var entity in l)
            {
                var i = new Image();
                i.Source = TypetextureList[GetTextureKey(entity)];
                i.HorizontalAlignment = HorizontalAlignment.Left;
                i.VerticalAlignment = VerticalAlignment.Bottom;
                i.Margin = new Thickness(entity.X + (30 * ((double)entity.Percentx / 100.0)), 0.0, 0.0, entity.Y + (30 * ((double)entity.Percenty / 100.0)));
                TextureList.Add(entity,i);
            }
        }

        public string GetTextureKey(Entity e)
        {
            if(e is SoftBlock)
            {
                return "SoftBloc";
            }
            if(e is HardBlock)
            {
                return "HardBloc";
            }
            if(e is Upgrade)
            {
                var u = (Upgrade) e;
                return u.Type.ToString();
            }
            if(e is Bomb)
            {
                return "Bomb";
            }
            return "Player_1_up";

        }

        public void LoadAllTextures()
        {
            foreach (var td in ThemeData)
            {
                var u = new Uri(td.Value, UriKind.Relative);
                TypetextureList.Add(td.Key, new BitmapImage(u));
            }
        }
        
    }
}
