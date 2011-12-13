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
    class PreGame : Screenv2
    {
        private Wizard _wizard;
        private Dictionary<string, Image> _menuDataList;
        private Dictionary<string, Label> _menuLabelList;
        private bool thisistheend;
        private bool alreadyloaded;
        private bool onthepop;

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
            PlaySound._.Stop("Select");
            PlaySound._.TypeSoundList["Versus"].Play();
            thisistheend = false;
            onthepop = false;
            var pressstart = (SkinSelectMenu)oldscreen;
            _wizard = w;
            alreadyloaded = false;
            if (_menuDataList == null)
            {
                _menuDataList = new Dictionary<string, Image>();
                _menuLabelList = new Dictionary<string, Label>();
                _wizard.WindowDispatcher.Invoke(DispatcherPriority.Normal, new Action(() => LoadMenuImagePrevious(pressstart)));
                //_wizard.WindowDispatcher.Invoke(DispatcherPriority.Normal, new Action(LoadMenuLabel));
            }
            for (var i = w.Grid.Children.Count - 1; i > -1; i--)
            {
                if (!(w.Grid.Children[i] is Grid || w.Grid.Children[i] is MediaElement))
                {
                    w.Grid.Children.RemoveAt(i);
                }
            }
            foreach (var img in MenuDataList)
            {
                _wizard.Grid.Children.Add(img.Value);
            }
            foreach (var lab in MenuLabelList)
            {
                _wizard.Grid.Children.Add(lab.Value);
            }
            //var lt = new ScaleTransform { ScaleX = 1.1, ScaleY = 1.1, CenterX = 134, CenterY = 50 };
            //MenuDataList[OptionSelected].Margin = new Thickness(MenuDataList[OptionSelected].Margin.Left - 10, MenuDataList[OptionSelected].Margin.Top - 10, 0,0);
            //MenuDataList[OptionSelected].LayoutTransform = lt;
            //MenuDataList[OptionSelected].Opacity = 1;
            TimerManager._.AddNewTimer(true, 15, true, null, FadeIn);
        }


        public override void KeyUp(Key k) { }

        public override void KeyDown(Key k)
        {
        }

        public override void Hide()
        {


        }

        public void LoadMenuImagePrevious(SkinSelectMenu old)
        {
            MenuDataList.Add("Black", old.MenuDataList["Black"]);
            MenuDataList["Black"].Opacity = 1;
            var gl1 = new Image
            {
                Source = GameParameters._.MenutextureList["Versus"],
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0.0, 0.0, 0.0, 0.0),
                Opacity = 0,
                Width = 500,
                Height = 500
            };
            MenuDataList.Add("Versus", gl1);
            
            if (GameParameters._.PlayerCount == 3)
            {
                MenuDataList.Add("P1", old.MenuDataList["Preview_1"]);
                MenuDataList["P1"].Margin = new Thickness(MenuDataList["P1"].Margin.Left, MenuDataList["P1"].Margin.Top + 160, 0.0, 0.0);
                MenuDataList.Add("P2", old.MenuDataList["Preview_2"]);
                MenuDataList["P2"].Margin = new Thickness(MenuDataList["P2"].Margin.Left + 60, MenuDataList["P2"].Margin.Top + 160, 0.0, 0.0);
                MenuDataList.Add("P3", old.MenuDataList["Preview_3"]);
                MenuDataList["P3"].Margin = new Thickness(MenuDataList["P3"].Margin.Left + 120, MenuDataList["P3"].Margin.Top + 160, 0.0, 0.0);
            }else
            if (GameParameters._.PlayerCount == 4)
            {
                MenuDataList.Add("P1", old.MenuDataList["Preview_1"]);
                MenuDataList["P1"].Margin = new Thickness(MenuDataList["P1"].Margin.Left, MenuDataList["P1"].Margin.Top + 160, 0.0, 0.0);
                MenuDataList.Add("P2", old.MenuDataList["Preview_2"]);
                MenuDataList["P2"].Margin = new Thickness(MenuDataList["P2"].Margin.Left, MenuDataList["P2"].Margin.Top + 160, 0.0, 0.0);
                MenuDataList.Add("P3", old.MenuDataList["Preview_3"]);
                MenuDataList["P3"].Margin = new Thickness(MenuDataList["P3"].Margin.Left, MenuDataList["P3"].Margin.Top + 160, 0.0, 0.0);
                MenuDataList.Add("P4", old.MenuDataList["Preview_4"]);
                MenuDataList["P4"].Margin = new Thickness(MenuDataList["P4"].Margin.Left, MenuDataList["P4"].Margin.Top + 160, 0.0, 0.0);
            }else
            {
                MenuDataList.Add("P1", old.MenuDataList["Preview_1"]);
                MenuDataList["P1"].Margin = new Thickness(MenuDataList["P1"].Margin.Left, MenuDataList["P1"].Margin.Top + 160, 0.0, 0.0);
                MenuDataList.Add("P2", old.MenuDataList["Preview_2"]);
                MenuDataList["P2"].Margin = new Thickness(MenuDataList["P2"].Margin.Left + 240, MenuDataList["P2"].Margin.Top + 160, 0.0, 0.0);
            }
            var gl = new Image
            {
                Source = GameParameters._.MenutextureList["White"],
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0.0,0.0, 0.0, 0.0),
                Opacity = 1,
                Width = 1000,
                Height = 1000
            };
            MenuDataList.Add("White", gl);
            

        }

        public void LoadMenuImage()
        {
        }

        public void LoadMenuLabel()
        {

        }


        private void FadeIn(object sender, ElapsedEventArgs e)
        {
            _wizard.WindowDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => FadeInOption((Timer)sender)));

        }


        public void FadeInOption(Timer t)
        {
           
            if(MenuDataList["White"].Opacity <= 0)
            {
                MenuDataList["Versus"].Opacity += 0.005;
                if(MenuDataList["Versus"].Opacity >= 1 && !onthepop)
                {
                    t.Interval = 2000;
                    onthepop = true;
                }else if(MenuDataList["Versus"].Opacity >= 1 && onthepop)
                {
                    t.AutoReset = false;
                    if(!thisistheend)
                    {
                        thisistheend = true;
                        _wizard.NextScreen(ScreenType.Game);
                    }
                    
                }

            }else
            {
                MenuDataList["White"].Opacity -= 0.01; 
            }
        }


        
    }
}
