using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using BombEistiv2WPF.Control;
using BombEistiv2WPF.Environment;

namespace BombEistiv2WPF.View.Menu
{
    class MenuScreen : Screenv2
    {
        private Wizard _wizard;
        private Dictionary<string, Image> _menuDataList;
        private Action _actionInProgress;
        private Timer time;
        private bool triggerOk;
        private bool thisIsTheEnd;

        public Dictionary<string, Image> MenuDataList
        {
            get { return _menuDataList; }
        }

        public override void Show(Control.Wizard w, Screenv2 screen)
        {
            thisIsTheEnd = false;
            _wizard = w;
            _wizard.Grid.Children.RemoveRange(0, _wizard.Grid.Children.Count);
            if(_menuDataList == null)
            {
                _menuDataList = new Dictionary<string, Image>();
                _wizard.WindowDispatcher.Invoke(DispatcherPriority.Normal, new Action(LoadMenuImage));
            }
            foreach (var img in MenuDataList)
            {
                _wizard.Grid.Children.Add(img.Value);
            }
            triggerOk = false;
            TimerManager._.AddNewTimer(true, 15, true, null, ActionDefil);
            _actionInProgress = bombIncoming;
            TimerManager._.AddNewTimer(true, 15, true, null, ActionPressStart);
        }

        public override void KeyUp(Key k) { }

        public override void KeyDown(Key k)
        {
            if(KeyAction._.KeysMenu.ContainsKey(k) && KeyAction._.KeysMenu[k] == "Enter")
            {
                Hide();
            }
        }

        public override void Hide()
        {
            if (_actionInProgress == PressStartCling || (_actionInProgress == flash && time.Interval == 15))
            {
                thisIsTheEnd = true;
                MenuDataList["White"].Opacity = 1;
                //_wizard.NextScreen(ScreenType.PressStart);
            }else
            {
                SetFinalState();
            }
            
        }

        public void LoadMenuImage()
        {
            var g = new Image
            {
                Source = GameParameters._.MenutextureList["Sky"],
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Stretch,
                Margin = new Thickness(0.0, 0.0, 0.0, 0.0),
                Height = 1000,
                Width = _wizard.Grid.Width * 2
            };
            MenuDataList.Add("Sky", g);
            var g2 = new Image
            {
                Source = GameParameters._.MenutextureList["Bomb"],
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(_wizard.Grid.Height, -70, 0.0, 0.0),
                Width = 600,
                Height = 200
            };
            var lt = new RotateTransform {Angle = -15};
            g2.LayoutTransform = lt;
            MenuDataList.Add("Bomb", g2);
            var g3 = new Image
            {
                Source = GameParameters._.MenutextureList["Eisti"],
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(100, -300, 0.0, 0.0),
                Width = 600,
                Height = 200
            };
            MenuDataList.Add("Eisti", g3);
            var g4 = new Image
            {
                Source = GameParameters._.MenutextureList["2"],
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
                Source = GameParameters._.MenutextureList["PressStart"],
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
                Source = GameParameters._.MenutextureList["White"],
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0.0, 0.0, 0.0, 0.0),
                Opacity = 0,
                Width = 2000,
                Height = 2000
            };
            MenuDataList.Add("White", g6);

            
        }

        private void ActionDefil(object sender, ElapsedEventArgs e)
        {
            _wizard.WindowDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(defileSky));
            if(thisIsTheEnd)
            {
                var t = (Timer) sender;
                t.AutoReset = false;
            }
        }

        private void ActionPressStart(object sender, ElapsedEventArgs e)
        {
            if (time == null) {time = (Timer) sender;}
            _wizard.WindowDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                                                new Action(_actionInProgress));
            if (thisIsTheEnd)
            {
                var t = (Timer)sender;
                t.AutoReset = false;
            }
        }

        public void defileSky()
        {
            if (MenuDataList["Sky"].Margin.Left == -(MenuDataList["Sky"].Width / 2.0))
            {
                MenuDataList["Sky"].Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
            }
            MenuDataList["Sky"].Margin = new Thickness(MenuDataList["Sky"].Margin.Left - 1, 0.0, 0.0, 0.0);

        }

        public void bombIncoming()
        {
            if (MenuDataList["Bomb"].Margin.Left > -120)
            {
                MenuDataList["Bomb"].Margin = new Thickness(MenuDataList["Bomb"].Margin.Left - 15, -70, 0.0, 0.0);
            }
            else if (_actionInProgress != flash && _actionInProgress != PressStartCling)
            {
                _actionInProgress = eistiIncoming;
            }
        }

        public void eistiIncoming()
        {
            if (MenuDataList["Eisti"].Margin.Top <= 120)
            {
                MenuDataList["Eisti"].Margin = new Thickness(100, MenuDataList["Eisti"].Margin.Top + 10, 0.0, 0.0);
            }
            else if (_actionInProgress != flash && _actionInProgress != PressStartCling)
            {
                MenuDataList["White"].Opacity = 1;
                MenuDataList["2"].Opacity = 1;
                time.Interval = 50;
                _actionInProgress = flash;
            }
        }

        public void flash()
        {
            if (MenuDataList["White"].Opacity > 0)
            {
                MenuDataList["White"].Opacity -= 0.02;
            }
            else
            {
                MenuDataList["PressStart"].Opacity = 0.20;
                time.Interval = 10;
                _actionInProgress = PressStartCling;
            }
        }

        public void PressStartCling()
        {
            if (MenuDataList["PressStart"].Opacity >= 1 && !triggerOk)
            {
                time.Interval = 1500;
                triggerOk = true;
            }
            else if (MenuDataList["PressStart"].Opacity <= 0 && triggerOk)
            {
                time.Interval = 200;
                triggerOk = false;
            }
            else if (!triggerOk)
            {
                time.Interval = 15;
                MenuDataList["PressStart"].Opacity += 0.05;
            }
            else if (triggerOk)
            {
                time.Interval = 15;
                MenuDataList["PressStart"].Opacity -= 0.05;
            }
        }

        public void SetFinalState()
        {
            time.Interval = 15;
            MenuDataList["White"].Opacity = 1;
            _actionInProgress = flash;
            MenuDataList["Bomb"].Margin = new Thickness(-130, -70, 0.0, 0.0);
            MenuDataList["Eisti"].Margin = new Thickness(100, 120 + 10, 0.0, 0.0);
            MenuDataList["2"].Opacity = 1;
            
            
        }
    }
}
