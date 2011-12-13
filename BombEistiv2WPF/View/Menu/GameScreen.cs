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
    public class GameScreen : Screenv2
    {
        private Wizard _wizard;
        private Dictionary<string, Image> _menuDataList;
        private Dictionary<string, Label> _menuLabelList;
        private Key lastKey;
        private Key lastReleaseKey;
        private int start;
        private bool movelocked;

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
            PlaySound._.Stop("Result");
            
            lastKey = Key.None;
            lastReleaseKey = Key.None;
            _wizard = w;
            movelocked = true;
            for (var i = w.Grid.Children.Count - 1; i > -1; i--)
            {
                if (!(w.Grid.Children[i] is Grid || w.Grid.Children[i] is MediaElement))
                {
                    w.Grid.Children.RemoveAt(i);
                }
            }
            TimerManager._.Reset();
            if (_menuDataList == null)
            {
                _menuDataList = new Dictionary<string, Image>();
                _wizard.WindowDispatcher.Invoke(DispatcherPriority.Normal,
                                                new Action(LoadImage));
            }
            foreach (var img in MenuDataList)
            {
                _wizard.TheWindow.MainGrid.Children.Add(img.Value);
                Canvas.SetZIndex(img.Value,2);
            }
            Canvas.SetZIndex(MenuDataList["Hurry"], 3);
            Canvas.SetZIndex(MenuDataList["Over"], 4);
            _wizard.TheWindow.GameInProgress = new ClassicGame(this);
            TimerManager._.Game = _wizard.TheWindow.GameInProgress;
            ListenerGame._.GameInProgress = _wizard.TheWindow.GameInProgress;
            InitTextureGame();
            InitInGameMenu();
            ListenerGame._.EmptyTheCache();
            Score._.ResetSurvived();
            start = 4;
            PlaySound._.LireBoucle("Game");
            TimerManager._.AddNewTimer(true, 15, true, null, FadeIn);
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
            if(!movelocked)
            {
                lastReleaseKey = k;
                ListenerGame._.ReleaseKey(k);
            }
            
        }

        public override void KeyDown(Key k)
        {
            if(!movelocked)
            {
                if (k != lastKey || (k == lastKey && k == lastReleaseKey))
                {
                    lastKey = k;
                    ListenerGame._.EventKey(k);
                }
            }
            
        }

        public override void Hide()
        {


        }

        public void LoadImage()
        {
            var g = new Image
            {
                Source = GameParameters._.MenutextureList["Start3"],
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0.0, 0.0, 0.0, 0.0),
                Opacity = 0,
                Width = 300,
                Height = 300
            };
            MenuDataList.Add("Start3", g);
            var g2 = new Image
            {
                Source = GameParameters._.MenutextureList["Start2"],
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0.0, 0.0, 0.0, 0.0),
                Opacity = 0,
                Width = 300,
                Height = 300
            };
            MenuDataList.Add("Start2", g2);
            var g3 = new Image
            {
                Source = GameParameters._.MenutextureList["Start1"],
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0.0, 0.0, 0.0, 0.0),
                Opacity = 0,
                Width = 300,
                Height = 300
            };
            MenuDataList.Add("Start1", g3);
            var g4 = new Image
            {
                Source = GameParameters._.MenutextureList["StartGo"],
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0.0, 0.0, 0.0, 0.0),
                Opacity = 0,
                Width = 350,
                Height = 350
            };
            MenuDataList.Add("StartGo", g4);

            var g5 = new Image
            {
                Source = GameParameters._.MenutextureList["Hurry"],
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(-400, 0.0, 0.0, 0.0),
                Opacity = 1,
                Width = 400,
                Height = 200
            };
            MenuDataList.Add("Hurry", g5);

            var g6 = new Image
            {
                Source = GameParameters._.MenutextureList["Over"],
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0.0, 0.0, 0.0, 0.0),
                Opacity = 0,
                Width = 500,
                Height = 500
            };
            MenuDataList.Add("Over", g6);

        }


        private void FadeIn(object sender, ElapsedEventArgs e)
        {
            _wizard.WindowDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => FadeInOption((Timer)sender)));

        }

        public void Hurry(object sender, ElapsedEventArgs e)
        {
            _wizard.WindowDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => HurryUp((Timer)sender)));
        }

        public void HurryUp(Timer t)
        {
            if(MenuDataList["Hurry"].Margin.Left <= 800)
            {
                MenuDataList["Hurry"].Margin = new Thickness(MenuDataList["Hurry"].Margin.Left + 5, 0.0, 0.0, 0.0);
            }
        }


        public void FadeInOption(Timer t)
        {
            if(start == 4)
            {
                MenuDataList["Start3"].Opacity = 1;
                start = 3;
            }else if(start == 3 && MenuDataList["Start3"].Opacity > 0.4)
            {
                MenuDataList["Start3"].Opacity -= 0.009;
            }else if(start == 3 && MenuDataList["Start3"].Opacity <= 0.4)
            {
                MenuDataList["Start3"].Opacity = 0;
                MenuDataList["Start2"].Opacity = 1;
                start = 2;
            }
            else if (start == 2 && MenuDataList["Start2"].Opacity > 0.4)
            {
                MenuDataList["Start2"].Opacity -= 0.009;
            }
            else if (start == 2 && MenuDataList["Start2"].Opacity <= 0.4)
            {
                MenuDataList["Start2"].Opacity = 0;
                MenuDataList["Start1"].Opacity = 1;
                start = 1;
            }
            else if (start == 1 && MenuDataList["Start1"].Opacity > 0.4)
            {
                MenuDataList["Start1"].Opacity -= 0.009;
            }
            else if (start == 1 && MenuDataList["Start1"].Opacity <= 0.4)
            {
                MenuDataList["Start1"].Opacity = 0;
                MenuDataList["StartGo"].Opacity = 1;
                start = 0;
            }else if(start == 0)
            {
                ListenerGame._.StartTimers();
                var g = (ClassicGame) _wizard.TheWindow.GameInProgress;
                g.Start();
                movelocked = false;
                start--;
            }else if(start < 0 && start > -100)
            {
                start--;
            }
            else if (start <= -100)
            {
                MenuDataList["StartGo"].Opacity = 0;
                t.AutoReset = false;
            }
        }

        public void Ending()
        {
            TimerManager._.Reset();
            _wizard.NextScreen(ScreenType.Results);
        }
    }
}
