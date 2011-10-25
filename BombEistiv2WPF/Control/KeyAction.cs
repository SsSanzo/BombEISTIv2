using System.Collections.Generic;
using System.Windows.Forms;
using BombEistiv2WPF.Environment;

namespace BombEistiv2WPF.Control
{
    public class KeyAction
    {
        private Dictionary<string, Keys> keysPlayer;
        private Dictionary<string, Keys> keysMenu;

        private static KeyAction _this;

        private KeyAction()
        {
            keysPlayer = new Dictionary<string, Keys>();
            keysMenu = new Dictionary<string, Keys>();
            InitDefaultKeys();
        }

        public static KeyAction _
        {
            get { return _this ?? (_this = new KeyAction()); }
        }

        public void InitDefaultKeys()
        {
            keysPlayer.Add("1_Up", Keys.Up);
            keysPlayer.Add("1_Right", Keys.Right);
            keysPlayer.Add("1_Down", Keys.Down);
            keysPlayer.Add("1_Left", Keys.Left);
            keysPlayer.Add("1_None", Keys.RShiftKey);
            keysPlayer.Add("2_Up", Keys.Z);
            keysPlayer.Add("2_Right", Keys.D);
            keysPlayer.Add("2_Down", Keys.S);
            keysPlayer.Add("2_Left", Keys.Q);
            keysPlayer.Add("2_None", Keys.A);
            keysPlayer.Add("3_Up", Keys.O);
            keysPlayer.Add("3_Right", Keys.M);
            keysPlayer.Add("3_Down", Keys.L);
            keysPlayer.Add("3_Left", Keys.K);
            keysPlayer.Add("3_None", Keys.I);
            keysPlayer.Add("4_Up", Keys.Y);
            keysPlayer.Add("4_Right", Keys.J);
            keysPlayer.Add("4_Down", Keys.H);
            keysPlayer.Add("4_Left", Keys.G);
            keysPlayer.Add("4_Left", Keys.T);
            keysMenu.Add("Enter", Keys.Enter);
            keysMenu.Add("Up", Keys.Up);
            keysMenu.Add("Right", Keys.Right);
            keysMenu.Add("Down", Keys.Down);
            keysMenu.Add("Left", Keys.Left);
            keysMenu.Add("Escape", Keys.Escape);
        }

        public void ReplaceKey(Player p, Direction d, Keys k)
        {
            keysPlayer[p.Id + "_" + d.ToString()] = k;
        }

        public Keys GetKey(Player p, Direction d)
        {
            return keysPlayer[p.Id + "_" + d.ToString()];
        }
    }
}
