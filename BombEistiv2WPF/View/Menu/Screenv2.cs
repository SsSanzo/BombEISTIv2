using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using BombEistiv2WPF.Control;
using BombEistiv2WPF.Environment;

namespace BombEistiv2WPF.View.Menu
{
    public abstract class Screenv2
    {
        public abstract void Show(Wizard w, Screenv2 screen);

        public abstract void KeyUp(Key k);

        public abstract void KeyDown(Key k);

        public abstract void Hide();
    }
}
