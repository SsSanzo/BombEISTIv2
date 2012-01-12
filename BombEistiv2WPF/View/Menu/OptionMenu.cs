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
    class OptionMenu : Screenv2
    {
        private Wizard _wizard;
        private Dictionary<string, Image> _menuDataList;
        private Dictionary<string, Label> _menuLabelList;
        private String OptionSelected;
        private Dictionary<String, int> OptionZommed;
        private bool thisistheend;
        private bool movelocked;
        private bool alreadyloaded;

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
            if(oldscreen is MainMenuScreen)
            {
                thisistheend = false;
                movelocked = true;
                alreadyloaded = false;
                var pressstart = (MainMenuScreen)oldscreen;
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
                TimerManager._.AddNewTimer(true, 15, true, null, ActionDefil);
                TimerManager._.AddNewTimer(true, 15, true, null, FadeIn);
            }else if(oldscreen is GeneralOptionMenu)
            {
                thisistheend = false;
                movelocked = true;
                alreadyloaded = false;
                var pressstart = (GeneralOptionMenu)oldscreen;
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
                TimerManager._.AddNewTimer(true, 15, true, null, ActionDefil);
                TimerManager._.AddNewTimer(true, 15, true, null, FadeOut);
            }else if(oldscreen is KeyOption)
            {
                thisistheend = false;
                movelocked = true;
                alreadyloaded = false;
                var pressstart = (KeyOption)oldscreen;
                _wizard = w;
                OptionZommed = new Dictionary<string, int>();
                OptionSelected = "BoxTouche";
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
                TimerManager._.AddNewTimer(true, 15, true, null, ActionDefil);
                TimerManager._.AddNewTimer(true, 15, true, null, FadeOutTouche);
            }
            else if (oldscreen is ThemeMenu)
            {
                thisistheend = false;
                movelocked = true;
                alreadyloaded = false;
                var pressstart = (ThemeMenu)oldscreen;
                _wizard = w;
                OptionZommed = new Dictionary<string, int>();
                OptionSelected = "BoxTheme";
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
                TimerManager._.AddNewTimer(true, 15, true, null, ActionDefil);
                TimerManager._.AddNewTimer(true, 15, true, null, FadeOutTheme);
            }
            
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
            if (!movelocked)
            {
                if (KeyAction._.KeysMenu.ContainsKey(k) && KeyAction._.KeysMenu[k] == "Down")
                {
                    OptionZommed[OptionSelected] = -1;
                    Canvas.SetZIndex(MenuDataList[OptionSelected], 0);
                    Canvas.SetZIndex(MenuLabelList[OptionSelected], 1);
                    MenuDataList[OptionSelected].Opacity = 0.6;
                    PlaySound._.TypeSoundList["Selection"].Play();
                    switch (OptionSelected)
                    {
                        case "BoxGeneral":
                            SwitchOption("BoxTouche");
                            break;
                        case "BoxTouche":
                            SwitchOption("BoxTheme");
                            break;
                        case "BoxTheme":
                            SwitchOption("BoxCancel");
                            break;
                        case "BoxCancel":
                            SwitchOption("BoxGeneral");
                            break;
                    }
                }
                else if (KeyAction._.KeysMenu.ContainsKey(k) && KeyAction._.KeysMenu[k] == "Up")
                {
                    OptionZommed[OptionSelected] = -1;
                    Canvas.SetZIndex(MenuDataList[OptionSelected], 0);
                    Canvas.SetZIndex(MenuLabelList[OptionSelected], 1);
                    MenuDataList[OptionSelected].Opacity = 0.6;
                    PlaySound._.TypeSoundList["Selection"].Play();
                    switch (OptionSelected)
                    {
                        case "BoxGeneral":
                            SwitchOption("BoxCancel");
                            break;
                        case "BoxTouche":
                            SwitchOption("BoxGeneral");
                            break;
                        case "BoxTheme":
                            SwitchOption("BoxTouche");
                            break;
                        case "BoxCancel":
                            SwitchOption("BoxTheme");
                            break;
                    }
                }
                else if (KeyAction._.KeysMenu.ContainsKey(k) && KeyAction._.KeysMenu[k] == "Enter")
                {
                    thisistheend = true;
                    switch (OptionSelected)
                    {
                        case "BoxGeneral":
                            movelocked = true;
                            PlaySound._.TypeSoundList["Valid"].Play();
                            _wizard.NextScreen(ScreenType.GeneralOptions);
                            break;
                        case "BoxTouche":
                            movelocked = true;
                            PlaySound._.TypeSoundList["Valid"].Play();
                            _wizard.NextScreen(ScreenType.KeyConfig);
                            break;
                        case "BoxTheme":
                            movelocked = true;
                            PlaySound._.TypeSoundList["Valid"].Play();
                            _wizard.NextScreen(ScreenType.Themes);
                            break;
                        case "BoxCancel":
                            movelocked = true;
                            PlaySound._.TypeSoundList["Cancel"].Play();
                            _wizard.NextScreen(ScreenType.MainMenu);
                            break;
                    }
                }
                else if (KeyAction._.KeysMenu.ContainsKey(k) && KeyAction._.KeysMenu[k] == "Escape")
                {
                    thisistheend = true;
                    movelocked = true;
                    PlaySound._.TypeSoundList["Cancel"].Play();
                    _wizard.NextScreen(ScreenType.MainMenu);
                }
            }
        }

        public override void Hide()
        {


        }

        public void LoadMenuImagePrevious(MainMenuScreen old)
        {
            MenuDataList.Add("Sky", old.MenuDataList["Sky"]);
            MenuDataList.Add("Black", old.MenuDataList["Black"]);
            MenuDataList.Add("Bomb", old.MenuDataList["Bomb"]);
            MenuDataList.Add("Eisti", old.MenuDataList["Eisti"]);
            MenuDataList.Add("2", old.MenuDataList["2"]);
            MenuDataList.Add("BoxOption", old.MenuDataList["BoxOption"]);
            MenuLabelList.Add("BoxOption", old.MenuLabelList["BoxOption"]);
        }

        public void LoadMenuImagePrevious(GeneralOptionMenu old)
        {
            MenuDataList.Add("Sky", old.MenuDataList["Sky"]);
            MenuDataList.Add("Black", old.MenuDataList["Black"]);
            MenuDataList.Add("Bomb", old.MenuDataList["Bomb"]);
            MenuDataList.Add("Eisti", old.MenuDataList["Eisti"]);
            MenuDataList.Add("2", old.MenuDataList["2"]);
            MenuDataList.Add("BoxOption", old.MenuDataList["BoxOption"]);
            MenuLabelList.Add("BoxOption", old.MenuLabelList["BoxOption"]);
            MenuDataList.Add("BoxGeneral", old.MenuDataList["BoxGeneral"]);
            MenuLabelList.Add("BoxGeneral", old.MenuLabelList["BoxGeneral"]);
        }

        public void LoadMenuImagePrevious(KeyOption old)
        {
            MenuDataList.Add("Sky", old.MenuDataList["Sky"]);
            MenuDataList.Add("Black", old.MenuDataList["Black"]);
            MenuDataList.Add("Bomb", old.MenuDataList["Bomb"]);
            MenuDataList.Add("Eisti", old.MenuDataList["Eisti"]);
            MenuDataList.Add("2", old.MenuDataList["2"]);
            MenuDataList.Add("BoxOption", old.MenuDataList["BoxOption"]);
            MenuLabelList.Add("BoxOption", old.MenuLabelList["BoxOption"]);
            MenuDataList.Add("BoxTouche", old.MenuDataList["BoxTouche"]);
            MenuLabelList.Add("BoxTouche", old.MenuLabelList["BoxTouche"]);
        }

        public void LoadMenuImagePrevious(ThemeMenu old)
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

        var g2 = new Image
            {
                Source = GameParameters._.MenutextureList["Box"],
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(300, 275, 0.0, 0.0),
                Opacity = 0.6,
                Width = 268,
                Height = 100
            };
            var lt = new ScaleTransform { ScaleX = 1.0, ScaleY = 1.0, CenterX = 134, CenterY = 50 };
            g2.LayoutTransform = lt;
            OptionZommed.Add("BoxGeneral", 0);
            
            var g3 = new Image
            {
                Source = GameParameters._.MenutextureList["Box"],
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(215, 350, 0.0, 0.0),
                Opacity = 0.6,
                Width = 268,
                Height = 100
            };
            var lt2 = new ScaleTransform { ScaleX = 1.0, ScaleY = 1.0, CenterX = 134, CenterY = 50 };
            g3.LayoutTransform = lt2;
            OptionZommed.Add("BoxTouche", 0);
            
            
            
            var g5 = new Image
            {
                Source = GameParameters._.MenutextureList["Box"],
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(150, 425, 0.0, 0.0),
                Opacity = 0.6,
                Width = 268,
                Height = 100
            };
            var lt4 = new ScaleTransform { ScaleX = 1.0, ScaleY = 1.0, CenterX = 134, CenterY = 50 };
            g5.LayoutTransform = lt4;
            OptionZommed.Add("BoxTheme", 0);
            

            // erfierjg

            var g6 = new Image
            {
                Source = GameParameters._.MenutextureList["Box"],
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(10, 570, 0.0, 0.0),
                Opacity = 0.6,
                Width = 268,
                Height = 100
            };
            var lt5 = new ScaleTransform { ScaleX = 0.7, ScaleY = 0.7, CenterX = 134, CenterY = 50 };
            g6.LayoutTransform = lt5;
            OptionZommed.Add("BoxCancel", 0);

            MenuDataList.Add("BoxCancel", g6);
            MenuDataList.Add("BoxTheme", g5);
            MenuDataList.Add("BoxTouche", g3);
            MenuDataList.Add("BoxGeneral", g2);
        }

        public void LoadMenuLabel()
        {
            var l = new Label { Content = "Paramètres", FontSize = 30, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(360, 300, 0, 0) };
            MenuLabelList.Add("BoxGeneral", l);
            var l2 = new Label { Content = "Commandes", FontSize = 30, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(260, 370, 0, 0) };
            MenuLabelList.Add("BoxTouche", l2);
            var l4 = new Label { Content = "Theme", FontSize = 30, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(190, 450, 0, 0) };
            MenuLabelList.Add("BoxTheme", l4);
            var l3 = new Label { Content = "Retour", FontSize = 30, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(45, 575, 0, 0) };
            MenuLabelList.Add("BoxCancel", l3);
        }

        private void ActionDefil(object sender, ElapsedEventArgs e)
        {
            if(!thisistheend)
            {
                _wizard.WindowDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(defileSky));
                _wizard.WindowDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(zoomin));
            }else
            {
                var t = (Timer) sender;
                t.AutoReset = false;
            }
            


        }

        private void FadeIn(object sender, ElapsedEventArgs e)
        {
            _wizard.WindowDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => FadeInOption((Timer) sender)));

        }

        private void FadeOut(object sender, ElapsedEventArgs e)
        {
            _wizard.WindowDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => FadeOutOption((Timer)sender)));

        }

        private void FadeOutTouche(object sender, ElapsedEventArgs e)
        {
            _wizard.WindowDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => FadeOutOptionTouche((Timer)sender)));

        }

        private void FadeOutTheme(object sender, ElapsedEventArgs e)
        {
            _wizard.WindowDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => FadeOutOptionTheme((Timer)sender)));

        }

        public void FadeInOption(Timer t)
        {
            MenuDataList["BoxOption"].Opacity = 1;
            var lt = (ScaleTransform)MenuDataList["BoxOption"].LayoutTransform;
            if(lt.ScaleX > 0.81)
            {
                MenuDataList["Bomb"].Margin = new Thickness(MenuDataList["Bomb"].Margin.Left + 270.0 / 21.0, MenuDataList["Bomb"].Margin.Top, 0.0, 0.0);
                MenuDataList["Eisti"].Margin = new Thickness(MenuDataList["Eisti"].Margin.Left + 270.0 / 21.0, MenuDataList["Eisti"].Margin.Top, 0.0, 0.0);
                MenuDataList["2"].Margin = new Thickness(MenuDataList["2"].Margin.Left + 270.0 / 21.0, MenuDataList["2"].Margin.Top, 0.0, 0.0);
                MenuDataList["BoxOption"].Margin = new Thickness(MenuDataList["BoxOption"].Margin.Left - 100.0 / 20.0, MenuDataList["BoxOption"].Margin.Top - 380.0 / 20.0, 0, 0);

                lt.ScaleX = lt.ScaleX - 0.4 / 20.0;
                lt.ScaleY = lt.ScaleY - 0.4 / 20.0;
                MenuLabelList["BoxOption"].Margin = new Thickness(MenuLabelList["BoxOption"].Margin.Left - 100.0 / 20.0, MenuLabelList["BoxOption"].Margin.Top - 390.0 / 20.0, 0, 0);
                MenuLabelList["BoxOption"].FontSize -= 0.7;
            }else
            {
                t.AutoReset = false;
                if (!alreadyloaded)
                {
                    alreadyloaded = true;
                    _wizard.WindowDispatcher.Invoke(DispatcherPriority.Normal, new Action(LoadMenuImage));
                    _wizard.WindowDispatcher.Invoke(DispatcherPriority.Normal, new Action(LoadMenuLabel));
                    for (var i = _wizard.Grid.Children.Count - 1; i > -1; i--)
                    {
                        if (!(_wizard.Grid.Children[i] is Grid || _wizard.Grid.Children[i] is MediaElement))
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
                    SwitchOption("BoxGeneral");
                    movelocked = false;
                }
            }
            
        }

        public void FadeOutOption(Timer t)
        {
            MenuDataList["BoxGeneral"].Opacity = 1;
            var lt = (ScaleTransform)MenuDataList["BoxGeneral"].LayoutTransform;
            if (lt.ScaleX < 1.19)
            {
                MenuDataList["BoxGeneral"].Margin = new Thickness(MenuDataList["BoxGeneral"].Margin.Left + 240.0 / 20.0, MenuDataList["BoxGeneral"].Margin.Top + 170.0 / 20.0, 0, 0);

                lt.ScaleX = lt.ScaleX + 0.4 / 20.0;
                lt.ScaleY = lt.ScaleY + 0.4 / 20.0;
                MenuLabelList["BoxGeneral"].Margin = new Thickness(MenuLabelList["BoxGeneral"].Margin.Left + 250.0 / 20.0, MenuLabelList["BoxGeneral"].Margin.Top + 180.0 / 20.0, 0, 0);
                MenuLabelList["BoxGeneral"].FontSize += 0.7;
            }
            else
            {
                t.AutoReset = false;
                if (!alreadyloaded)
                {
                    MenuDataList.Remove("BoxGeneral");
                    MenuLabelList.Remove("BoxGeneral");
                    alreadyloaded = true;
                    _wizard.WindowDispatcher.Invoke(DispatcherPriority.Normal, new Action(LoadMenuImage));
                    _wizard.WindowDispatcher.Invoke(DispatcherPriority.Normal, new Action(LoadMenuLabel));
                    for (var i = _wizard.Grid.Children.Count - 1; i > -1; i--)
                    {
                        if (!(_wizard.Grid.Children[i] is Grid || _wizard.Grid.Children[i] is MediaElement))
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
                    SwitchOption("BoxGeneral");
                    movelocked = false;
                }
            }

        }

        public void FadeOutOptionTouche(Timer t)
        {
            MenuDataList["BoxTouche"].Opacity = 1;
            var lt = (ScaleTransform)MenuDataList["BoxTouche"].LayoutTransform;
            if (lt.ScaleX < 1.19)
            {
                MenuDataList["BoxTouche"].Margin = new Thickness(MenuDataList["BoxTouche"].Margin.Left + 165.0 / 20.0, MenuDataList["BoxTouche"].Margin.Top + 245.0 / 20.0, 0, 0);

                lt.ScaleX = lt.ScaleX + 0.4 / 20.0;
                lt.ScaleY = lt.ScaleY + 0.4 / 20.0;
                MenuLabelList["BoxTouche"].Margin = new Thickness(MenuLabelList["BoxTouche"].Margin.Left + 175.0 / 20.0, MenuLabelList["BoxTouche"].Margin.Top + 255.0 / 20.0, 0, 0);
                MenuLabelList["BoxTouche"].FontSize += 0.7;
            }
            else
            {
                t.AutoReset = false;
                if (!alreadyloaded)
                {
                    MenuDataList.Remove("BoxTouche");
                    MenuLabelList.Remove("BoxTouche");
                    alreadyloaded = true;
                    _wizard.WindowDispatcher.Invoke(DispatcherPriority.Normal, new Action(LoadMenuImage));
                    _wizard.WindowDispatcher.Invoke(DispatcherPriority.Normal, new Action(LoadMenuLabel));
                    for (var i = _wizard.Grid.Children.Count - 1; i > -1; i--)
                    {
                        if (!(_wizard.Grid.Children[i] is Grid || _wizard.Grid.Children[i] is MediaElement))
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
                    SwitchOption("BoxTouche");
                    movelocked = false;
                }
            }

        }

        public void FadeOutOptionTheme(Timer t)
        {
            MenuDataList["BoxTheme"].Opacity = 1;
            var lt = (ScaleTransform)MenuDataList["BoxTheme"].LayoutTransform;
            if (lt.ScaleX < 1.19)
            {
                MenuDataList["BoxTheme"].Margin = new Thickness(MenuDataList["BoxTheme"].Margin.Left + 95.0 / 20.0, MenuDataList["BoxTheme"].Margin.Top + 320.0 / 20.0, 0, 0);

                lt.ScaleX = lt.ScaleX + 0.4 / 20.0;
                lt.ScaleY = lt.ScaleY + 0.4 / 20.0;
                MenuLabelList["BoxTheme"].Margin = new Thickness(MenuLabelList["BoxTheme"].Margin.Left + 100.0 / 20.0, MenuLabelList["BoxTheme"].Margin.Top + 330.0 / 20.0, 0, 0);
                MenuLabelList["BoxTheme"].FontSize += 0.7;
            }
            else
            {
                t.AutoReset = false;
                if (!alreadyloaded)
                {
                    MenuDataList.Remove("BoxTheme");
                    MenuLabelList.Remove("BoxTheme");
                    alreadyloaded = true;
                    _wizard.WindowDispatcher.Invoke(DispatcherPriority.Normal, new Action(LoadMenuImage));
                    _wizard.WindowDispatcher.Invoke(DispatcherPriority.Normal, new Action(LoadMenuLabel));
                    for (var i = _wizard.Grid.Children.Count - 1; i > -1; i--)
                    {
                        if (!(_wizard.Grid.Children[i] is Grid || _wizard.Grid.Children[i] is MediaElement))
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
                    SwitchOption("BoxTheme");
                    movelocked = false;
                }
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
