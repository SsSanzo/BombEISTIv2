



using System;
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
            _keysPlayer = GameParameters._.GetGameKeyMap();

            //KeysPlayer.Add(Key.Up, "1_Up");
            //KeysPlayer.Add(Key.Right, "1_Right");
            //KeysPlayer.Add(Key.Down, "1_Down");
            //KeysPlayer.Add(Key.Left, "1_Left");

            //KeysPlayer.Add(Key.F, "1_Up");
            //KeysPlayer.Add(Key.V, "1_Right");
            //KeysPlayer.Add(Key.C, "1_Down");
            //KeysPlayer.Add(Key.X, "1_Left");

            //KeysPlayer.Add(Key.RightShift, "1_None");
            //KeysPlayer.Add(Key.Z, "2_Up");
            //KeysPlayer.Add(Key.D, "2_Right");
            //KeysPlayer.Add(Key.S, "2_Down");
            //KeysPlayer.Add(Key.Q, "2_Left");
            //KeysPlayer.Add(Key.A, "2_None");
            //KeysPlayer.Add(Key.O, "3_Up");
            //KeysPlayer.Add(Key.M, "3_Right");
            //KeysPlayer.Add(Key.L, "3_Down");
            //KeysPlayer.Add(Key.K, "3_Left");
            //KeysPlayer.Add(Key.I, "3_None");
            //KeysPlayer.Add(Key.Y, "4_Up");
            //KeysPlayer.Add(Key.J, "4_Right");
            //KeysPlayer.Add(Key.H, "4_Down");
            //KeysPlayer.Add(Key.G, "4_Left");
            //KeysPlayer.Add(Key.T, "4_None");
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

        public void ReplaceKey(String s, Key k)
        {
            var p = s.Split('_')[0];
            var d = s.Split('_')[1];
            if (KeysPlayer.ContainsValue(p + "_" + d))
            {
                KeysPlayer.Remove(KeysPlayer.FirstOrDefault(keys => keys.Value == p + "_" + d).Key);
            }
            KeysPlayer.Add(k, p + "_" + d);
        }

        public bool checkAllKeys()
        {
            for(var i=1;i<5;i++)
            {
                if (!(KeysPlayer.ContainsValue(i + "_Switch") && KeysPlayer.ContainsValue(i + "_None") && KeysPlayer.ContainsValue(i + "_Up") && KeysPlayer.ContainsValue(i + "_Down") && KeysPlayer.ContainsValue(i + "_Right") && KeysPlayer.ContainsValue(i + "_Left")))
                {
                    return false;
                }
            }
            return true;
        }

        public void resetDefaultKey()
        {
            KeysPlayer.Clear();
            KeysPlayer.Add(Key.Up, "1_Up");
            KeysPlayer.Add(Key.Right, "1_Right");
            KeysPlayer.Add(Key.Down, "1_Down");
            KeysPlayer.Add(Key.Left, "1_Left");
            KeysPlayer.Add(Key.RightShift, "1_None");
            KeysPlayer.Add(Key.RightCtrl, "1_Switch");
            KeysPlayer.Add(Key.Z, "2_Up");
            KeysPlayer.Add(Key.D, "2_Right");
            KeysPlayer.Add(Key.S, "2_Down");
            KeysPlayer.Add(Key.Q, "2_Left");
            KeysPlayer.Add(Key.A, "2_None");
            KeysPlayer.Add(Key.E, "2_Switch");
            KeysPlayer.Add(Key.O, "3_Up");
            KeysPlayer.Add(Key.M, "3_Right");
            KeysPlayer.Add(Key.L, "3_Down");
            KeysPlayer.Add(Key.K, "3_Left");
            KeysPlayer.Add(Key.I, "3_None");
            KeysPlayer.Add(Key.P, "3_Switch");
            KeysPlayer.Add(Key.Y, "4_Up");
            KeysPlayer.Add(Key.J, "4_Right");
            KeysPlayer.Add(Key.H, "4_Down");
            KeysPlayer.Add(Key.G, "4_Left");
            KeysPlayer.Add(Key.T, "4_None");
            KeysPlayer.Add(Key.U, "4_Switch");
        }

        public Key GetKey(Player p, Direction d)
        {
            return KeysPlayer.FirstOrDefault(keys => keys.Value == p.Id + "_" + d.ToString()).Key; ;
        }

        public Key GetKey(String s)
        {
            return KeysPlayer.FirstOrDefault(keys => keys.Value == s.Split('_')[0] + "_" + s.Split('_')[1]).Key; ;
        }

        public void SaveKey()
        {
            GameParameters._.SaveGameKeyMap(KeysPlayer);
        }
    }
}