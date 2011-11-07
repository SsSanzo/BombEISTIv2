using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using BombEistiv2WPF.Control;
using BombEistiv2WPF.Environment;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Controls;
using System.Timers;
using System.Windows.Threading;

namespace BombEistiv2WPF.View.Menu
{
    public class DevScreen : Screenv2
    {

        private Wizard _wizard;
        private Image img;

        public override void Show(Control.Wizard w)
        {
            _wizard = w;
            _wizard.Grid.Children.Add(img);
            TimerManager._.AddNewTimer(false, 4000, true, null, HideTimerEvent);
        }

        public override void KeyUp(Key k) { }

        public override void KeyDown(Key k) { }

        public override void Hide()
        {
            _wizard.NextScreen(ScreenType.PressStart);
        }

        public Image Img
        {
            get { return img; }
        }

        public void setImage(String s)
        {
            img = new Image
                        {
                            Source = GameParameters._.MenutextureList[s],
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                            Margin = new Thickness(0.0, 0.0, 0.0, 0.0),
                            Opacity = 0.0,
                            Width = 400,
                            Height = 400
                        };
        }

        public void HideTimerEvent(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            Dispatcher.CurrentDispatcher.Invoke((Action) Hide, DispatcherPriority.Normal);
        }
    }
}
