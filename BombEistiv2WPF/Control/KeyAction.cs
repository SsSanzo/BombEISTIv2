using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BombEistiv2WPF.Environment;

namespace BombEistiv2WPF.Control
{
    public class KeyAction
    {
        private Dictionary<Keys, string> _keysPlayer;
        private Dictionary<Keys, string> _keysMenu;

        private static KeyAction _this;

        private KeyAction()
        {
            _keysPlayer = new Dictionary<Keys, string>();
            _keysMenu = new Dictionary<Keys, string>();
            InitDefaultKeys();
        }

        public static KeyAction _
        {
            get { return _this ?? (_this = new KeyAction()); }
        }

        public Dictionary<Keys, string> KeysPlayer
        {
            get { return _keysPlayer; }
        }

        public Dictionary<Keys, string> KeysMenu
        {
            get { return _keysMenu; }
        }

        public void InitDefaultKeys()
        {
            KeysPlayer.Add(Keys.Up, "1_Up");
            KeysPlayer.Add(Keys.Right, "1_Right");
            KeysPlayer.Add(Keys.Down, "1_Down");
            KeysPlayer.Add(Keys.Left, "1_Left");
            KeysPlayer.Add(Keys.RShiftKey, "1_None");
            KeysPlayer.Add(Keys.Z, "2_Up");
            KeysPlayer.Add(Keys.D, "2_Right");
            KeysPlayer.Add(Keys.S, "2_Down");
            KeysPlayer.Add(Keys.Q, "2_Left");
            KeysPlayer.Add(Keys.A, "2_None");
            KeysPlayer.Add(Keys.O, "3_Up");
            KeysPlayer.Add(Keys.M, "3_Right");
            KeysPlayer.Add(Keys.L, "3_Down");
            KeysPlayer.Add(Keys.K, "3_Left");
            KeysPlayer.Add(Keys.I, "3_None");
            KeysPlayer.Add(Keys.Y, "4_Up");
            KeysPlayer.Add(Keys.J, "4_Right");
            KeysPlayer.Add(Keys.H, "4_Down");
            KeysPlayer.Add(Keys.G, "4_Left");
            KeysPlayer.Add(Keys.T, "4_None");
            KeysMenu.Add(Keys.Enter, "Enter");
            KeysMenu.Add(Keys.Up, "Up");
            KeysMenu.Add(Keys.Right, "Right");
            KeysMenu.Add(Keys.Down, "Down");
            KeysMenu.Add(Keys.Left, "Left");
            KeysMenu.Add(Keys.Escape, "Escape");
        }

        public void ReplaceKey(Player p, Direction d, Keys k)
        {
            var key = KeysPlayer.FirstOrDefault(keys => keys.Value == p.Id + "_" + d.ToString()).Key;
            KeysPlayer.Remove(key);
            KeysPlayer.Add(k, p.Id + "_" + d.ToString());
        }

        public Keys GetKey(Player p, Direction d)
        {
            return KeysPlayer.FirstOrDefault(keys => keys.Value == p.Id + "_" + d.ToString()).Key; ;
        }

        
    }
}
