using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using BombEistiv2WPF.Environment;

namespace BombEistiv2WPF.Control
{
    public class KeyAction
    {
        private Dictionary<Key, string> _keysPlayer;
        private Dictionary<Key, string> _keysMenu;

        private static KeyAction _this;

        private KeyAction()
        {
            _keysPlayer = new Dictionary<Key, string>();
            _keysMenu = new Dictionary<Key, string>();
            InitDefaultKeys();
        }

        public static KeyAction _
        {
            get { return _this ?? (_this = new KeyAction()); }
        }

        public Dictionary<Key, string> KeysPlayer
        {
            get { return _keysPlayer; }
        }

        public Dictionary<Key, string> KeysMenu
        {
            get { return _keysMenu; }
        }

        public void InitDefaultKeys()
        {
            KeysPlayer.Add(Key.Up, "1_Up");
            KeysPlayer.Add(Key.Right, "1_Right");
            KeysPlayer.Add(Key.Down, "1_Down");
            KeysPlayer.Add(Key.Left, "1_Left");
            //KeysPlayer.Add(Key.F, "1_Up");
            //KeysPlayer.Add(Key.V, "1_Right");
            //KeysPlayer.Add(Key.C, "1_Down");
            //KeysPlayer.Add(Key.X, "1_Left");
            KeysPlayer.Add(Key.RightShift, "1_None");
            KeysPlayer.Add(Key.Z, "2_Up");
            KeysPlayer.Add(Key.D, "2_Right");
            KeysPlayer.Add(Key.S, "2_Down");
            KeysPlayer.Add(Key.Q, "2_Left");
            KeysPlayer.Add(Key.A, "2_None");
            KeysPlayer.Add(Key.O, "3_Up");
            KeysPlayer.Add(Key.M, "3_Right");
            KeysPlayer.Add(Key.L, "3_Down");
            KeysPlayer.Add(Key.K, "3_Left");
            KeysPlayer.Add(Key.I, "3_None");
            KeysPlayer.Add(Key.Y, "4_Up");
            KeysPlayer.Add(Key.J, "4_Right");
            KeysPlayer.Add(Key.H, "4_Down");
            KeysPlayer.Add(Key.G, "4_Left");
            KeysPlayer.Add(Key.T, "4_None");
            KeysMenu.Add(Key.Enter, "Enter");
            KeysMenu.Add(Key.Up, "Up");
            KeysMenu.Add(Key.Right, "Right");
            KeysMenu.Add(Key.Down, "Down");
            KeysMenu.Add(Key.Left, "Left");
            KeysMenu.Add(Key.Escape, "Escape");
        }

        public void ReplaceKey(Player p, Direction d, Key k)
        {
            var key = KeysPlayer.FirstOrDefault(keys => keys.Value == p.Id + "_" + d.ToString()).Key;
            KeysPlayer.Remove(key);
            KeysPlayer.Add(k, p.Id + "_" + d.ToString());
        }

        public Key GetKey(Player p, Direction d)
        {
            return KeysPlayer.FirstOrDefault(keys => keys.Value == p.Id + "_" + d.ToString()).Key; ;
        }

        
    }
}
