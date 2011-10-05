using System.Collections.Generic;
using System.Windows.Media;
using BombEistiv2WPF.Environment;

namespace BombEistiv2WPF.View
{
    public class Texture
    {
        private Dictionary<string, string> _themeData;

        public Texture(Dictionary<string, string> themeData)
        {
            _themeData = themeData;
        }

        public Dictionary<string, string> ThemeData
        {
            get { return _themeData; }
        }

        public ImageSource GetTextures(Entity e)
        {
            if(e is Bomb)
            {
                
            }
        }
        
    }
}
