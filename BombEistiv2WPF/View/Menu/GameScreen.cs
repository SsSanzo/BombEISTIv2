using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using BombEistiv2WPF.Control;
using BombEistiv2WPF.Environment;
using Timer = System.Timers.Timer;
namespace BombEistiv2WPF.View.Menu
{
    class GameScreen : Screenv2
    {
        private Wizard _wizard;
        private Dictionary<string, Image> _menuDataList;
        private Dictionary<string, Label> _menuLabelList;
        private Key lastKey;
        private Key lastReleaseKey;

        public Dictionary<string, Image> MenuDataList
        {
            get { return _menuDataList; }
        }

        public Dictionary<string, Label> MenuLabelList
        {
            get { return _menuLabelList; }
        }

        public override void Show(Control.Wizard w, Screenv2 oldscreen)
        {
            lastKey = Key.None;
            lastReleaseKey = Key.None;
            _wizard = w;
            for (var i = w.Grid.Children.Count - 1; i > -1; i--)
            {
                if (!(w.Grid.Children[i] is Grid))
                {
                    w.Grid.Children.RemoveAt(i);
                }
            }
            _wizard.TheWindow.GameInProgress = new ClassicGame();
            TimerManager._.Game = _wizard.TheWindow.GameInProgress;
            ListenerGame._.GameInProgress = _wizard.TheWindow.GameInProgress;
            InitTextureGame();
            InitInGameMenu();
            ListenerGame._.StartTimers();
            var g = (ClassicGame) _wizard.TheWindow.GameInProgress;
            g.Start();
        }

        public void InitTextureGame()
        {
            Texture._.LoadAllTextures();
            var l = Texture._.LoadBackground();
            foreach (var tl in l)
            {
                _wizard.TheWindow.MainGrid.Children.Add(tl);
            }
            Texture._.LoadTextureList(_wizard.TheWindow.GameInProgress.TheCurrentMap.GetCompleteList(), _wizard.TheWindow);
        }

        public void InitInGameMenu()
        {
            InGameMenu._.InitInGameMenu(_wizard.TheWindow.GameInProgress);
            foreach (var lab in InGameMenu._.AllTheLabel())
            {
                _wizard.TheWindow.MenuGrid.Children.Add(lab);
            }
            foreach (var face in InGameMenu._.AllTheFace(_wizard.TheWindow.GameInProgress))
            {
                _wizard.TheWindow.MenuGrid.Children.Add(face);
            }
        }

        public override void KeyUp(Key k)
        {
            lastReleaseKey = k;
            ListenerGame._.ReleaseKey(k);
        }

        public override void KeyDown(Key k)
        {
            if (k != lastKey || (k == lastKey && k == lastReleaseKey))
            {
                lastKey = k;
                ListenerGame._.EventKey(k);
            }
        }

        public override void Hide()
        {


        }


        private void FadeIn(object sender, ElapsedEventArgs e)
        {
            _wizard.WindowDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => FadeInOption((Timer)sender)));

        }


        public void FadeInOption(Timer t)
        {

        }
    }
}
