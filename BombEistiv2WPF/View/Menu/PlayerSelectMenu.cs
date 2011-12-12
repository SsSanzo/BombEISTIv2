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
    class PlayerSelectMenu : Screenv2
    {
        private Wizard _wizard;
        private Dictionary<string, Image> _menuDataList;
        private Dictionary<string, Label> _menuLabelList;
        private String OptionSelected;
        private Dictionary<String, String> OptionMoved;
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

            if(oldscreen is GameModeMenu)
            {

                thisistheend = false;
                movelocked = true;
                var pressstart = (GameModeMenu)oldscreen;
                _wizard = w;
                OptionMoved = new Dictionary<string, string>();
                OptionSelected = "2P";
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
            }else if(oldscreen is SkinSelectMenu)
            {

                thisistheend = false;
                movelocked = true;
                var pressstart = (SkinSelectMenu)oldscreen;
                _wizard = w;
                OptionMoved = new Dictionary<string, string>();
                if(pressstart.MenuLabelList["BoxLevel"].Content == "Couple")
                {
                    OptionSelected = "2P";
                }
                else if (pressstart.MenuLabelList["BoxLevel"].Content == "Party")
                {
                    OptionSelected = "3P";
                }
                else if (pressstart.MenuLabelList["BoxLevel"].Content == "Super Party")
                {
                    OptionSelected = "4P";
                }
                
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
                TimerManager._.AddNewTimer(true, 15, true, null, FadeOut);
            }


        }

        public void SwitchOption(String s)
        {
            if (OptionMoved.Where(c => c.Value != "").Count() == 0)
            {
                OptionMoved[OptionSelected] = s;
                OptionSelected = s;
            }
        }

        public override void KeyUp(Key k) { }

        public override void KeyDown(Key k)
        {
            if (!movelocked)
            {
                if (KeyAction._.KeysMenu.ContainsKey(k) && KeyAction._.KeysMenu[k] == "Right")
                {
                    switch (OptionSelected)
                    {
                        case "2P":
                            SwitchOption("3P");
                            break;
                        case "3P":
                            SwitchOption("4P");
                            break;
                        case "4P":
                            SwitchOption("2P");
                            break;
                    }

                }
                else if (KeyAction._.KeysMenu.ContainsKey(k) && KeyAction._.KeysMenu[k] == "Left")
                {
                    switch (OptionSelected)
                    {
                        case "2P":
                            SwitchOption("4P");
                            break;
                        case "3P":
                            SwitchOption("2P");
                            break;
                        case "4P":
                            SwitchOption("3P");
                            break;
                    }
                }
                else if (KeyAction._.KeysMenu.ContainsKey(k) && KeyAction._.KeysMenu[k] == "Enter")
                {
                    thisistheend = true;
                    GameParameters._.PlayerCount = Convert.ToInt32(OptionSelected.Substring(0, 1));
                    _wizard.NextScreen(ScreenType.Characters);
                }
                else if (KeyAction._.KeysMenu.ContainsKey(k) && KeyAction._.KeysMenu[k] == "Escape")
                {
                    thisistheend = true;
                    _wizard.NextScreen(ScreenType.GameMode);
                }
            }
        }

        public override void Hide()
        {


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
            MenuDataList.Add("BoxDClassic", old.MenuDataList["BoxDClassic"]);
            MenuDataList.Add("BoxGClassic", old.MenuDataList["BoxGClassic"]);
            MenuLabelList.Add("BoxClassic", old.MenuLabelList["BoxClassic"]);
        }

        public void LoadMenuImagePrevious(SkinSelectMenu old)
        {
            MenuDataList.Add("Sky", old.MenuDataList["Sky"]);
            MenuDataList.Add("Black", old.MenuDataList["Black"]);
            MenuDataList.Add("Bomb", old.MenuDataList["Bomb"]);
            MenuDataList.Add("Eisti", old.MenuDataList["Eisti"]);
            MenuDataList.Add("2", old.MenuDataList["2"]);
            MenuDataList.Add("BoxGame", old.MenuDataList["BoxGame"]);
            MenuLabelList.Add("BoxGame", old.MenuLabelList["BoxGame"]);
            MenuDataList.Add("BoxDClassic", old.MenuDataList["BoxDClassic"]);
            MenuDataList.Add("BoxGClassic", old.MenuDataList["BoxGClassic"]);
            MenuLabelList.Add("BoxClassic", old.MenuLabelList["BoxClassic"]);
            MenuDataList.Add("BoxDLevel", old.MenuDataList["BoxDLevel"]);
            MenuDataList.Add("BoxGLevel", old.MenuDataList["BoxGLevel"]);
            MenuLabelList.Add("BoxLevel", old.MenuLabelList["BoxLevel"]);
        }

        public void LoadMenuImage()
        {
            var right = "";
            var left = "";
            if(OptionSelected == "2P")
            {
                right = "3P";
                left = "4P";
            }else if(OptionSelected == "3P")
            {
                right = "4P";
                left = "2P";
            }else if(OptionSelected == "4P")
            {
                right = "2P";
                left = "3P";
            }
            for (var i = 0; i < 3; i++)
            {
                var g2 = new Image
                {
                    Source = GameParameters._.MenutextureList["Crocheleft"],
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top,
                    Margin = new Thickness((i == 0) ? -190 : -540 + (i - 1) * 700, (i == 0) ? 150 : 380, 0.0, 0.0),
                    Opacity = (i == 0) ? 0.8 : 0.5,
                    Width = 80,
                    Height = 400
                };
                var lt = new ScaleTransform { ScaleX = 0.6, ScaleY = 0.7, CenterX = 134, CenterY = 50 };
                g2.LayoutTransform = lt;
                if (i == 0)
                {
                    MenuDataList.Add("BoxG" + OptionSelected, g2);
                }
                else if (i == 1)
                {
                    MenuDataList.Add("BoxG" + left, g2);
                }
                else
                {
                    MenuDataList.Add("BoxG" + right, g2);
                }

                var g3 = new Image
                {
                    Source = GameParameters._.MenutextureList["Crocheright"],
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top,
                    Margin = new Thickness((i == 0) ? 190 : -160 + (i - 1) * 700, (i == 0) ? 150 : 380, 0.0, 0.0),
                    Opacity = (i == 0) ? 0.8 : 0.5,
                    Width = 80,
                    Height = 400
                };
                var lt2 = new ScaleTransform { ScaleX = 0.6, ScaleY = 0.7, CenterX = 134, CenterY = 50 };
                g3.LayoutTransform = lt2;
                if (i == 0)
                {
                    MenuDataList.Add("BoxD" + OptionSelected, g3);
                }
                else if (i == 1)
                {
                    MenuDataList.Add("BoxD" + left, g3);
                }
                else
                {
                    MenuDataList.Add("BoxD" + right, g3);
                }

            }

            var gr = new Image
            {
                Source = GameParameters._.MenutextureList["Bigleft"],
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(-300.0, 250, 0.0, 0.0),
                Opacity = 1,
                Width = 49,
                Height = 80
            };
            MenuDataList.Add("Bigleft", gr);

            var gl = new Image
            {
                Source = GameParameters._.MenutextureList["Bigright"],
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(300.0, 250, 0.0, 0.0),
                Opacity = 1,
                Width = 49,
                Height = 80
            };
            MenuDataList.Add("Bigright", gl);

            var gc1 = new Image
            {
                Source = GameParameters._.MenutextureList[OptionSelected],
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness((OptionSelected == "3P") ? -20.0 : 0.0, 200, 0.0, 0.0),
                Opacity = 1,
                Width = 150,
                Height = 150
            };
            MenuDataList.Add("P" + OptionSelected, gc1);

            var gc2 = new Image
            {
                Source = GameParameters._.MenutextureList[right],
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness((OptionSelected == "3P") ? 350.0 : 370.0, 420, 0.0, 0.0),
                Opacity = 0.2,
                Width = 180,
                Height = 180
            };
            MenuDataList.Add("P" + right, gc2);

            var gc3 = new Image
            {
                Source = GameParameters._.MenutextureList[left],
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness((OptionSelected == "3P") ? -390.0 : -370.0, 420, 0.0, 0.0),
                Opacity = 0.2,
                Width = 150,
                Height = 150
            };
            MenuDataList.Add("P" + left, gc3);
            
            OptionMoved.Add("2P", "");
            OptionMoved.Add("3P", "");
            OptionMoved.Add("4P", "");

        }

        public void LoadMenuLabel()
        {
            var select = "";
            var right = "";
            var left = "";
            var rights = "";
            var lefts = "";
            if (OptionSelected == "2P")
            {
                select = "Couple";
                right = "Party";
                left = "Super Party";
                rights = "3P";
                lefts = "4P";
            }else if(OptionSelected == "3P")
            {
                select = "Party";
                right = "Super Party";
                left = "Couple";
                rights = "4P";
                lefts = "2P";
            }else
            {
                select = "Super Party";
                right = "Couple";
                left = "Party";
                rights = "2P";
                lefts = "3P";
            }
            var l = new Label { Content = select, FontSize = 30, Foreground = new SolidColorBrush(Colors.White), HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(0.0, 320, 0, 0) };
            MenuLabelList.Add("Box" + OptionSelected, l);
            var l2 = new Label { Content = right, FontSize = 30, Foreground = new SolidColorBrush(Colors.Gray), HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(350.0, 550, 0, 0) };
            MenuLabelList.Add("Box" + rights, l2);
            var l3 = new Label { Content = left, FontSize = 30, Foreground = new SolidColorBrush(Colors.Gray), HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(-350.0, 550, 0, 0) };
            MenuLabelList.Add("Box" + lefts, l3);
            var l5 = new Label { Content = "Entrée pour confirmer / Echap pour retour", FontSize = 28, Foreground = new SolidColorBrush(Colors.White), HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(0.0, 600, 0, 0) };
            MenuLabelList.Add("Confirm", l5);
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

        private void FadeOut(object sender, ElapsedEventArgs e)
        {
            _wizard.WindowDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => FadeOutOption((Timer)sender)));

        }


        public void FadeInOption(Timer t)
        {
            Canvas.SetZIndex(MenuDataList["BoxDClassic"], 2);
            Canvas.SetZIndex(MenuDataList["BoxGClassic"], 2);
            Canvas.SetZIndex(MenuLabelList["BoxClassic"], 2);
            var lt = (ScaleTransform)MenuDataList["BoxDClassic"].LayoutTransform;
            var lt2 = (ScaleTransform)MenuDataList["BoxGClassic"].LayoutTransform;
            if (lt.ScaleY > 0.21)
            {
                MenuDataList["BoxDClassic"].Margin = new Thickness(MenuDataList["BoxDClassic"].Margin.Left - 540.0 / 20.0, MenuDataList["BoxDClassic"].Margin.Top - 70.0 / 20.0, 0, 0);
                MenuDataList["BoxGClassic"].Margin = new Thickness(MenuDataList["BoxGClassic"].Margin.Left - 350.0 / 20.0, MenuDataList["BoxGClassic"].Margin.Top - 70.0 / 20.0, 0, 0);

                lt.ScaleY = lt.ScaleY - 0.5 / 20.0;
                lt2.ScaleY = lt2.ScaleY - 0.5 / 20.0;
                MenuLabelList["BoxClassic"].Margin = new Thickness(MenuLabelList["BoxClassic"].Margin.Left - 445.0 / 20.0, MenuLabelList["BoxClassic"].Margin.Top - 220.0 / 20.0, 0, 0);
                MenuLabelList["BoxClassic"].FontSize -= 0.3;
            }
            else 
            {
                t.AutoReset = false;
                if (!alreadyloaded)
                {
                    alreadyloaded = true;
                    //MenuDataList.Remove("BoxDClassic");
                    //MenuDataList.Remove("BoxGClassic");
                    //MenuLabelList.Remove("BoxClassic");
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
                    movelocked = false;
                }
            }

        }

        public void FadeOutOption(Timer t)
        {
            Canvas.SetZIndex(MenuDataList["BoxDLevel"], 0);
            Canvas.SetZIndex(MenuDataList["BoxGLevel"], 0);
            Canvas.SetZIndex(MenuLabelList["BoxLevel"], 0);
            var lt = (ScaleTransform)MenuDataList["BoxDLevel"].LayoutTransform;
            var lt2 = (ScaleTransform)MenuDataList["BoxGLevel"].LayoutTransform;
            if (lt.ScaleY < 0.69)
            {
                MenuDataList["BoxDLevel"].Margin = new Thickness(MenuDataList["BoxDLevel"].Margin.Left + 150.0 / 20.0, MenuDataList["BoxDLevel"].Margin.Top + 70.0 / 20.0, 0, 0);
                MenuDataList["BoxGLevel"].Margin = new Thickness(MenuDataList["BoxGLevel"].Margin.Left + 60.0 / 20.0, MenuDataList["BoxGLevel"].Margin.Top + 70.0 / 20.0, 0, 0);

                lt.ScaleY = lt.ScaleY + 0.5 / 20.0;
                lt2.ScaleY = lt2.ScaleY + 0.5 / 20.0;
                MenuLabelList["BoxLevel"].Margin = new Thickness(MenuLabelList["BoxLevel"].Margin.Left + 100.0 / 20.0, MenuLabelList["BoxLevel"].Margin.Top + 220.0 / 20.0, 0, 0);
                MenuLabelList["BoxLevel"].FontSize += 0.3;
            }
            else
            {
                t.AutoReset = false;
                if (!alreadyloaded)
                {
                    alreadyloaded = true;
                    MenuDataList.Remove("BoxDLevel");
                    MenuDataList.Remove("BoxGLevel");
                    MenuLabelList.Remove("BoxLevel");
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

            var copy = OptionMoved.Where(c => c.Value != "");
            for (var i = 0; i < copy.Count(); i++)
            {
                var sens = (MenuLabelList["Box" + copy.ElementAt(i).Value].Margin.Left - MenuLabelList["Box" + copy.ElementAt(i).Key].Margin.Left) / Math.Abs(MenuLabelList["Box" + copy.ElementAt(i).Value].Margin.Left - MenuLabelList["Box" + copy.ElementAt(i).Key].Margin.Left);
                var theone = "";
                if ((copy.ElementAt(i).Key + copy.ElementAt(i).Value).Contains("2P") && (copy.ElementAt(i).Key + copy.ElementAt(i).Value).Contains("3P"))
                {
                    theone = "4P";
                }
                else if ((copy.ElementAt(i).Key + copy.ElementAt(i).Value).Contains("3P") && (copy.ElementAt(i).Key + copy.ElementAt(i).Value).Contains("4P"))
                {
                    theone = "2P";
                }
                else
                {
                    theone = "3P";
                }

                if (MenuLabelList["Box" + copy.ElementAt(i).Value].Margin.Left == 0)
                {
                    OptionMoved[copy.ElementAt(i).Key] = "";
                }
                else
                {

                    //label
                    MenuLabelList["Box" + copy.ElementAt(i).Key].Margin =
                    new Thickness(MenuLabelList["Box" + copy.ElementAt(i).Key].Margin.Left - sens * (350.0) / 10.0,
                        MenuLabelList["Box" + copy.ElementAt(i).Key].Margin.Top + 230.0 / 10.0, 0.0, 0.0);

                    MenuLabelList["Box" + copy.ElementAt(i).Value].Margin =
                        new Thickness(MenuLabelList["Box" + copy.ElementAt(i).Value].Margin.Left - sens * (350.0) / 10.0,
                                      MenuLabelList["Box" + copy.ElementAt(i).Value].Margin.Top - 230.0 / 10.0, 0.0, 0.0);
                    MenuLabelList["Box" + theone].Margin =
                        new Thickness(MenuLabelList["Box" + theone].Margin.Left + sens * (700.0) / 10.0,
                             MenuLabelList["Box" + theone].Margin.Top, 0.0, 0.0);


                    //droite
                    MenuDataList["BoxD" + copy.ElementAt(i).Key].Margin =
                    new Thickness(MenuDataList["BoxD" + copy.ElementAt(i).Key].Margin.Left - sens * (350.0) / 10.0,
                        MenuDataList["BoxD" + copy.ElementAt(i).Key].Margin.Top + 230.0 / 10.0, 0.0, 0.0);

                    MenuDataList["BoxD" + copy.ElementAt(i).Value].Margin =
                        new Thickness(MenuDataList["BoxD" + copy.ElementAt(i).Value].Margin.Left - sens * (350.0) / 10.0,
                                      MenuDataList["BoxD" + copy.ElementAt(i).Value].Margin.Top - 230.0 / 10.0, 0.0, 0.0);
                    MenuDataList["BoxD" + theone].Margin =
                        new Thickness(MenuDataList["BoxD" + theone].Margin.Left + sens * (700.0) / 10.0,
                             MenuDataList["BoxD" + theone].Margin.Top, 0.0, 0.0);

                    //gauche
                    MenuDataList["BoxG" + copy.ElementAt(i).Key].Margin =
                    new Thickness(MenuDataList["BoxG" + copy.ElementAt(i).Key].Margin.Left - sens * (350.0) / 10.0,
                        MenuDataList["BoxG" + copy.ElementAt(i).Key].Margin.Top + 230.0 / 10.0, 0.0, 0.0);

                    MenuDataList["BoxG" + copy.ElementAt(i).Value].Margin =
                        new Thickness(MenuDataList["BoxG" + copy.ElementAt(i).Value].Margin.Left - sens * (350.0) / 10.0,
                                      MenuDataList["BoxG" + copy.ElementAt(i).Value].Margin.Top - 230.0 / 10.0, 0.0, 0.0);
                    MenuDataList["BoxG" + theone].Margin =
                        new Thickness(MenuDataList["BoxG" + theone].Margin.Left + sens * (700.0) / 10.0,
                             MenuDataList["BoxG" + theone].Margin.Top, 0.0, 0.0);

                    //image
                    MenuDataList["P" + copy.ElementAt(i).Key].Margin =
                    new Thickness(MenuDataList["P" + copy.ElementAt(i).Key].Margin.Left - sens * (350.0) / 10.0,
                        MenuDataList["P" + copy.ElementAt(i).Key].Margin.Top + 230.0 / 10.0, 0.0, 0.0);
                    MenuDataList["P" + copy.ElementAt(i).Value].Margin =
                        new Thickness(MenuDataList["P" + copy.ElementAt(i).Value].Margin.Left - sens * (350.0) / 10.0,
                                      MenuDataList["P" + copy.ElementAt(i).Value].Margin.Top - 230.0 / 10.0, 0.0, 0.0);
                    MenuDataList["P" + theone].Margin =
                        new Thickness(MenuDataList["P" + theone].Margin.Left + sens * (700.0) / 10.0,
                             MenuDataList["P" + theone].Margin.Top, 0.0, 0.0);

                    MenuLabelList["Box" + copy.ElementAt(i).Key].Foreground = new SolidColorBrush(Colors.Gray);
                    MenuLabelList["Box" + copy.ElementAt(i).Value].Foreground = new SolidColorBrush(Colors.White);
                    MenuLabelList["Box" + theone].Foreground = new SolidColorBrush(Colors.Gray);

                    MenuDataList["BoxD" + copy.ElementAt(i).Key].Opacity -= 0.03;
                    MenuDataList["BoxD" + copy.ElementAt(i).Value].Opacity += 0.03;

                    MenuDataList["P" + copy.ElementAt(i).Key].Opacity -= 0.08;
                    MenuDataList["P" + copy.ElementAt(i).Value].Opacity += 0.08;

                    MenuDataList["BoxG" + copy.ElementAt(i).Key].Opacity -= 0.03;
                    MenuDataList["BoxG" + copy.ElementAt(i).Value].Opacity += 0.03;
                }

            }
        }
    }
}
