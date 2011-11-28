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
    class ThemeMenu : Screenv2
    {
        private Wizard _wizard;
        private Dictionary<string, Image> _menuDataList;
        private Dictionary<string, Label> _menuLabelList;
        private String OptionSelected;
        private Dictionary<String, int> OptionZommed;
        private bool thisistheend;

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
            thisistheend = false;
            var pressstart = (OptionMenu)oldscreen;
            _wizard = w;
            OptionZommed = new Dictionary<string, int>();
            OptionSelected = "BoxGeneral";
            if (_menuDataList == null)
            {
                _menuDataList = new Dictionary<string, Image>();
                _menuLabelList = new Dictionary<string, Label>();
                _wizard.WindowDispatcher.Invoke(DispatcherPriority.Normal, new Action(() => LoadMenuImagePrevious(pressstart)));
                //_wizard.WindowDispatcher.Invoke(DispatcherPriority.Normal, new Action(LoadMenuLabel));
            }
            for (var i = w.Grid.Children.Count - 1; i > -1; i--)
            {
                if (!(w.Grid.Children[i] is Grid))
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
            TimerManager._.AddNewTimer(true, 15, true, null, ActionDefil);
            TimerManager._.AddNewTimer(true, 15, true, null, FadeIn);
        }

        public void SwitchOption(String s)
        {
            OptionSelected = s;
            MenuDataList[s].Opacity = 1;
            Canvas.SetZIndex(MenuDataList[s], 2);
            Canvas.SetZIndex(MenuLabelList[s], 3);
            OptionZommed[s] = 1;
        }

        public override void KeyUp(Key k) { }

        public override void KeyDown(Key k)
        {
            if (KeyAction._.KeysMenu.ContainsKey(k) && KeyAction._.KeysMenu[k] == "Down")
            {

            }
            else if (KeyAction._.KeysMenu.ContainsKey(k) && KeyAction._.KeysMenu[k] == "Up")
            {

            }
            else if (KeyAction._.KeysMenu.ContainsKey(k) && KeyAction._.KeysMenu[k] == "Enter")
            {

            }
        }

        public override void Hide()
        {


        }

        public void LoadMenuImagePrevious(OptionMenu old)
        {
            MenuDataList.Add("Sky", old.MenuDataList["Sky"]);
            MenuDataList.Add("Black", old.MenuDataList["Black"]);
            MenuDataList.Add("Bomb", old.MenuDataList["Bomb"]);
            MenuDataList.Add("Eisti", old.MenuDataList["Eisti"]);
            MenuDataList.Add("2", old.MenuDataList["2"]);
            MenuDataList.Add("BoxOption", old.MenuDataList["BoxOption"]);
            MenuLabelList.Add("BoxOption", old.MenuLabelList["BoxOption"]);
            MenuDataList.Add("BoxTheme", old.MenuDataList["BoxTheme"]);
            MenuLabelList.Add("BoxTheme", old.MenuLabelList["BoxTheme"]);
        }

        public void LoadMenuImage()
        {

        }

        public void LoadMenuLabel()
        {

        }

        private void ActionDefil(object sender, ElapsedEventArgs e)
        {
            if (!thisistheend)
            {
                _wizard.WindowDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(defileSky));
                _wizard.WindowDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(zoomin));
            }
            else
            {
                var t = (Timer)sender;
                t.AutoReset = false;
            }



        }

        private void FadeIn(object sender, ElapsedEventArgs e)
        {
            _wizard.WindowDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => FadeInOption((Timer)sender)));

        }

        public void FadeInOption(Timer t)
        {
            MenuDataList["BoxTheme"].Opacity = 1;
            var lt = (ScaleTransform)MenuDataList["BoxTheme"].LayoutTransform;
            if (lt.ScaleX > 0.81)
            {
                MenuDataList["BoxTheme"].Margin = new Thickness(MenuDataList["BoxTheme"].Margin.Left - 90.0 / 20.0, MenuDataList["BoxTheme"].Margin.Top - 320.0 / 20.0, 0, 0);

                lt.ScaleX = lt.ScaleX - 0.4 / 20.0;
                lt.ScaleY = lt.ScaleY - 0.4 / 20.0;
                MenuLabelList["BoxTheme"].Margin = new Thickness(MenuLabelList["BoxTheme"].Margin.Left - 100.0 / 20.0, MenuLabelList["BoxTheme"].Margin.Top - 330.0 / 20.0, 0, 0);
                MenuLabelList["BoxTheme"].FontSize -= 0.7;
            }
            else
            {
                t.AutoReset = false;
                _wizard.WindowDispatcher.Invoke(DispatcherPriority.Normal, new Action(LoadMenuImage));
                _wizard.WindowDispatcher.Invoke(DispatcherPriority.Normal, new Action(LoadMenuLabel));
                for (var i = _wizard.Grid.Children.Count - 1; i > -1; i--)
                {
                    if (!(_wizard.Grid.Children[i] is Grid))
                    {
                        _wizard.Grid.Children.RemoveAt(i);
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
                //SwitchOption("BoxGeneral");
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

        public void zoomin()
        {

            var copy = OptionZommed.Where(c => c.Value != 0);
            for (var i = 0; i < copy.Count(); i++)
            {
                if (copy.ElementAt(i).Value == 1)
                {
                    var lt = (ScaleTransform)MenuDataList[copy.ElementAt(i).Key].LayoutTransform;
                    if (((copy.ElementAt(i).Key != "BoxCancel") && lt.ScaleX >= 1.2) || ((copy.ElementAt(i).Key == "BoxCancel") && lt.ScaleX >= 0.9))
                    {
                        OptionZommed[copy.ElementAt(i).Key] = 0;
                    }
                    else
                    {
                        MenuDataList[copy.ElementAt(i).Key].Margin = new Thickness(MenuDataList[copy.ElementAt(i).Key].Margin.Left - 2, MenuDataList[copy.ElementAt(i).Key].Margin.Top - 2, 0, 0);
                        MenuLabelList[copy.ElementAt(i).Key].FontSize += 1;
                        MenuLabelList[copy.ElementAt(i).Key].Margin = new Thickness(MenuLabelList[copy.ElementAt(i).Key].Margin.Left - 2, MenuLabelList[copy.ElementAt(i).Key].Margin.Top - 2, 0, 0);
                        lt.ScaleX += 0.02;
                        lt.ScaleY += 0.02;
                    }
                }
                else
                {
                    var lt = (ScaleTransform)MenuDataList[copy.ElementAt(i).Key].LayoutTransform;
                    if (((copy.ElementAt(i).Key != "BoxCancel") && lt.ScaleX <= 1.0) || ((copy.ElementAt(i).Key == "BoxCancel") && lt.ScaleX <= 0.7))
                    {
                        OptionZommed[copy.ElementAt(i).Key] = 0;
                    }
                    else
                    {
                        MenuDataList[copy.ElementAt(i).Key].Margin = new Thickness(MenuDataList[copy.ElementAt(i).Key].Margin.Left + 2, MenuDataList[copy.ElementAt(i).Key].Margin.Top + 2, 0, 0);
                        MenuLabelList[copy.ElementAt(i).Key].FontSize -= 1;
                        MenuLabelList[copy.ElementAt(i).Key].Margin = new Thickness(MenuLabelList[copy.ElementAt(i).Key].Margin.Left + 2, MenuLabelList[copy.ElementAt(i).Key].Margin.Top + 2, 0, 0);
                        lt.ScaleX -= 0.02;
                        lt.ScaleY -= 0.02;
                    }
                }
            }
        }
    }
}
