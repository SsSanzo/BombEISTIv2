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

namespace BombEistiv2WPF.View.Menu
{
    class MainMenuScreen : Screenv2
    {
        private Wizard _wizard;
        private Dictionary<string, Image> _menuDataList;
        private Dictionary<string, Label> _menuLabelList;
        private String OptionSelected;
        private Dictionary<String, int> OptionZommed;
        private bool thisistheend;
        private bool alreadyloaded;
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
            thisistheend = false;
            alreadyloaded = false;
            
            if(oldscreen is MenuScreen)
            {
                PlaySound._.LireBoucle("MenuAll");
                var pressstart = (MenuScreen)oldscreen;
                movelocked = true;
                _wizard = w;
                OptionZommed = new Dictionary<string, int>();
                OptionSelected = "BoxGame";
                if (_menuDataList == null)
                {
                    _menuDataList = new Dictionary<string, Image>();
                    _menuLabelList = new Dictionary<string, Label>();
                    _wizard.WindowDispatcher.Invoke(DispatcherPriority.Normal, new Action(() => LoadNewMenuImage(pressstart)));
                    _wizard.WindowDispatcher.Invoke(DispatcherPriority.Normal, new Action(LoadMenuLabel));
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
                TimerManager._.AddNewTimer(true, 15, true, null, ActionDefil);
                TimerManager._.AddNewTimer(true, 15, true, null, ActionBlack);
                SwitchOption("BoxGame");
                movelocked = false;



            }else if(oldscreen is OptionMenu)
            {
                var pressstart = (OptionMenu)oldscreen;
                _wizard = w;
                movelocked = true;
                OptionZommed = new Dictionary<string, int>();
                OptionSelected = "BoxGame";
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
                TimerManager._.AddNewTimer(true, 15, true, null, ActionDefil);
                TimerManager._.AddNewTimer(true, 15, true, null, FadeOut);
                //SwitchOption("BoxGame");
            }
            else if (oldscreen is GameModeMenu)
            {
                var pressstart = (GameModeMenu) oldscreen;
                _wizard = w;
                movelocked = true;
                OptionZommed = new Dictionary<string, int>();
                OptionSelected = "BoxGame";
                if (_menuDataList == null)
                {
                    _menuDataList = new Dictionary<string, Image>();
                    _menuLabelList = new Dictionary<string, Label>();
                    _wizard.WindowDispatcher.Invoke(DispatcherPriority.Normal,
                                                    new Action(() => LoadMenuImagePrevious(pressstart)));
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
                TimerManager._.AddNewTimer(true, 15, true, null, ActionDefil);
                TimerManager._.AddNewTimer(true, 15, true, null, FadeOutPlay);
            }

        }

        public void SwitchOption(String s)
        {
            OptionSelected = s;
            MenuDataList[s].Opacity = 1;
            Canvas.SetZIndex(MenuDataList[s],2);
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
                    switch (OptionSelected)
                    {
                        case "BoxGame":
                            SwitchOption("BoxGameLan");
                            break;
                        case "BoxGameLan":
                            SwitchOption("BoxOption");
                            break;
                        case "BoxOption":
                            SwitchOption("BoxQuit");
                            break;
                        case "BoxQuit":
                            SwitchOption("BoxGame");
                            break;
                    }
                }
                else if (KeyAction._.KeysMenu.ContainsKey(k) && KeyAction._.KeysMenu[k] == "Up")
                {
                    OptionZommed[OptionSelected] = -1;
                    Canvas.SetZIndex(MenuDataList[OptionSelected], 0);
                    Canvas.SetZIndex(MenuLabelList[OptionSelected], 1);
                    MenuDataList[OptionSelected].Opacity = 0.6;
                    switch (OptionSelected)
                    {
                        case "BoxGame":
                            SwitchOption("BoxQuit");
                            break;
                        case "BoxGameLan":
                            SwitchOption("BoxGame");
                            break;
                        case "BoxOption":
                            SwitchOption("BoxGameLan");
                            break;
                        case "BoxQuit":
                            SwitchOption("BoxOption");
                            break;
                    }
                }
                else if (KeyAction._.KeysMenu.ContainsKey(k) && KeyAction._.KeysMenu[k] == "Enter")
                {
                    thisistheend = true;
                    switch (OptionSelected)
                    {
                        case "BoxGame":
                            thisistheend = true;
                            _wizard.NextScreen(ScreenType.GameMode);
                            break;
                        case "BoxOption":
                            thisistheend = true;
                            _wizard.NextScreen(ScreenType.Options);
                            break;
                        case "BoxQuit":
                            Application.Current.Shutdown();
                            break;
                    }
                }
            }
        }

        public override void Hide()
        {
            

        }

        public void LoadNewMenuImage(MenuScreen old)
        {
            MenuDataList.Add("Sky", old.MenuDataList["Sky"]);
            var g = new Image
            {
                Source = GameParameters._.MenutextureList["Black"],
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0.0, 0.0, 0.0, 0.0),
                Opacity = 0,
                Width = 2000,
                Height = 2000
            };
            MenuDataList.Add("Black", g);
            MenuDataList.Add("Bomb", old.MenuDataList["Bomb"]);
            MenuDataList.Add("Eisti", old.MenuDataList["Eisti"]);
            MenuDataList.Add("2", old.MenuDataList["2"]);
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
            OptionZommed.Add("BoxGame", 0);
            
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
            OptionZommed.Add("BoxGameLan", 0);
            
            var g4 = new Image
            {
                Source = GameParameters._.MenutextureList["Box"],
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(130, 425, 0.0, 0.0),
                Opacity = 0.6,
                Width = 268,
                Height = 100
            };
            var lt3 = new ScaleTransform { ScaleX = 1.0, ScaleY = 1.0, CenterX = 134, CenterY = 50 };
            g4.LayoutTransform = lt3;
            OptionZommed.Add("BoxOption", 0);
            
            var g5 = new Image
            {
                Source = GameParameters._.MenutextureList["Box"],
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(45, 500, 0.0, 0.0),
                Opacity = 0.6,
                Width = 268,
                Height = 100
            };
            var lt4 = new ScaleTransform { ScaleX = 1.0, ScaleY = 1.0, CenterX = 134, CenterY = 50 };
            g5.LayoutTransform = lt4;
            OptionZommed.Add("BoxQuit", 0);
            MenuDataList.Add("BoxQuit", g5);
            MenuDataList.Add("BoxOption", g4);
            MenuDataList.Add("BoxGameLan", g3);
            MenuDataList.Add("BoxGame", g2);
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
        }

        public void LoadMenuImagePrevious(GameModeMenu old)
        {
            MenuDataList.Add("Sky", old.MenuDataList["Sky"]);
            MenuDataList.Add("Black", old.MenuDataList["Black"]);
            MenuDataList.Add("Bomb", old.MenuDataList["Bomb"]);
            MenuDataList.Add("Eisti", old.MenuDataList["Eisti"]);
            MenuDataList.Add("2", old.MenuDataList["2"]);
            MenuDataList.Add("BoxGame", old.MenuDataList["BoxGame"]);
            MenuLabelList.Add("BoxGame", old.MenuLabelList["BoxGame"]);
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
            OptionZommed.Add("BoxGame", 0);

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
            OptionZommed.Add("BoxGameLan", 0);

            var g4 = new Image
            {
                Source = GameParameters._.MenutextureList["Box"],
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(130, 425, 0.0, 0.0),
                Opacity = 0.6,
                Width = 268,
                Height = 100
            };
            var lt3 = new ScaleTransform { ScaleX = 1.0, ScaleY = 1.0, CenterX = 134, CenterY = 50 };
            g4.LayoutTransform = lt3;
            OptionZommed.Add("BoxOption", 0);

            var g5 = new Image
            {
                Source = GameParameters._.MenutextureList["Box"],
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(45, 500, 0.0, 0.0),
                Opacity = 0.6,
                Width = 268,
                Height = 100
            };
            var lt4 = new ScaleTransform { ScaleX = 1.0, ScaleY = 1.0, CenterX = 134, CenterY = 50 };
            g5.LayoutTransform = lt4;
            OptionZommed.Add("BoxQuit", 0);
            MenuDataList.Add("BoxQuit", g5);
            MenuDataList.Add("BoxOption", g4);
            MenuDataList.Add("BoxGameLan", g3);
            MenuDataList.Add("BoxGame", g2);
        }

        public void LoadMenuLabel()
        {
            var l = new Label { Content = "Jouer !", FontSize = 30, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(360, 300, 0, 0) };
            MenuLabelList.Add("BoxGame",l);
            var l2 = new Label { Content = "Jouer en ligne", FontSize = 30, Foreground = new SolidColorBrush(Colors.LightSlateGray), Margin = new Thickness(260, 370, 0, 0) };
            MenuLabelList.Add("BoxGameLan", l2);
            var l4 = new Label { Content = "Option", FontSize = 30, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(190, 450, 0, 0) };
            MenuLabelList.Add("BoxOption", l4);
            var l3 = new Label { Content = "Quitter", FontSize = 30, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(105, 525, 0, 0) };
            MenuLabelList.Add("BoxQuit", l3);
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

        private void ActionBlack(object sender, ElapsedEventArgs e)
        {
            
                _wizard.WindowDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                                                new Action(() => OpacityBlack((Timer) sender)));
            
        }

        private void FadeOut(object sender, ElapsedEventArgs e)
        {
            _wizard.WindowDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => FadeOutOption((Timer)sender)));

        }

        private void FadeOutPlay(object sender, ElapsedEventArgs e)
        {
            _wizard.WindowDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => FadeOutPlayed((Timer)sender)));

        }

        public void FadeOutOption(Timer t)
        {
            var lt = (ScaleTransform)MenuDataList["BoxOption"].LayoutTransform;
            if (lt.ScaleX < 0.99)
            {
                MenuDataList["Bomb"].Margin = new Thickness(MenuDataList["Bomb"].Margin.Left - 270.0 / 20.0, MenuDataList["Bomb"].Margin.Top, 0.0, 0.0);
                MenuDataList["Eisti"].Margin = new Thickness(MenuDataList["Eisti"].Margin.Left - 270.0 / 20.0, MenuDataList["Eisti"].Margin.Top, 0.0, 0.0);
                MenuDataList["2"].Margin = new Thickness(MenuDataList["2"].Margin.Left - 270.0 / 20.0, MenuDataList["2"].Margin.Top, 0.0, 0.0);
                MenuDataList["BoxOption"].Margin = new Thickness(MenuDataList["BoxOption"].Margin.Left + 100.0 / 20.0, MenuDataList["BoxOption"].Margin.Top + 380.0 / 20.0, 0, 0);
                MenuDataList["BoxOption"].Opacity -=  0.6 / 20.0;
                lt.ScaleX = lt.ScaleX + 0.2 / 20.0;
                lt.ScaleY = lt.ScaleY + 0.2 / 20.0;
                MenuLabelList["BoxOption"].Margin = new Thickness(MenuLabelList["BoxOption"].Margin.Left + 120.0 / 20.0, MenuLabelList["BoxOption"].Margin.Top + 390.0 / 20.0, 0, 0);
            }
            else
            {
                t.AutoReset = false;
                if (!alreadyloaded)
                {
                    alreadyloaded = true;
                    MenuDataList.Remove("BoxOption");
                    MenuLabelList.Remove("BoxOption");
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
                    SwitchOption("BoxGame");
                    movelocked = false;
                }
            }

        }

        public void FadeOutPlayed(Timer t)
        {
            var lt = (ScaleTransform)MenuDataList["BoxGame"].LayoutTransform;
            if (lt.ScaleX < 0.99)
            {
                MenuDataList["Bomb"].Margin = new Thickness(MenuDataList["Bomb"].Margin.Left - 270.0 / 20.0, MenuDataList["Bomb"].Margin.Top, 0.0, 0.0);
                MenuDataList["Eisti"].Margin = new Thickness(MenuDataList["Eisti"].Margin.Left - 270.0 / 20.0, MenuDataList["Eisti"].Margin.Top, 0.0, 0.0);
                MenuDataList["2"].Margin = new Thickness(MenuDataList["2"].Margin.Left - 270.0 / 20.0, MenuDataList["2"].Margin.Top, 0.0, 0.0);
                MenuDataList["BoxGame"].Margin = new Thickness(MenuDataList["BoxGame"].Margin.Left + 320.0 / 20.0, MenuDataList["BoxGame"].Margin.Top + 300.0 / 20.0, 0, 0);
                //MenuDataList["BoxGame"].Opacity -= 0.6 / 20.0;
                lt.ScaleX = lt.ScaleX + 0.2 / 20.0;
                lt.ScaleY = lt.ScaleY + 0.2 / 20.0;
                MenuLabelList["BoxGame"].Margin = new Thickness(MenuLabelList["BoxGame"].Margin.Left + 330.0 / 20.0, MenuLabelList["BoxGame"].Margin.Top + 310.0 / 20.0, 0, 0);
            }
            else
            {
                t.AutoReset = false;
                if(!alreadyloaded)
                {
                    alreadyloaded = true;
                    MenuDataList.Remove("BoxGame");
                    MenuLabelList.Remove("BoxGame");
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
                    SwitchOption("BoxGame");
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

        public void OpacityBlack(Timer t)
        {
            if (MenuDataList["Black"].Opacity <= 0.5)
            {
                MenuDataList["Black"].Opacity += 0.05;
            }
            else
            {
                t.AutoReset = false;
            }
        }

        public void zoomin()
        {

            var copy = OptionZommed.Where(c => c.Value != 0);
            for (var i = 0; i < copy.Count();i++)
            {
                if (copy.ElementAt(i).Value == 1)
                {
                    var lt = (ScaleTransform)MenuDataList[copy.ElementAt(i).Key].LayoutTransform;
                    if (lt.ScaleX >= 1.2)
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
                    if (lt.ScaleX <= 1.0)
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
