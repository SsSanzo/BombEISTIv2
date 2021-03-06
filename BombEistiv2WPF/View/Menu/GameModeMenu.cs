﻿using System;
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
    class GameModeMenu : Screenv2
    {
        private Wizard _wizard;
        private Dictionary<string, Image> _menuDataList;
        private Dictionary<string, Label> _menuLabelList;
        private String OptionSelected;
        private Dictionary<String, String> OptionMoved;
        private bool thisistheend;
        private bool alreadyloaded;
        private bool movelocked;
        private string mySelectedOption;
        private bool optionmode;

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
            if (oldscreen is MainMenuScreen)
            {
                thisistheend = false;
                movelocked = true;
                var pressstart = (MainMenuScreen) oldscreen;
                optionmode = false;
                _wizard = w;
                OptionMoved = new Dictionary<string, string>();
                OptionSelected = "Classic";
                mySelectedOption = "Classic";
                if (GameParameters._.Type == GameType.Crazy)
                {
                    mySelectedOption = "Crazy";
                }
                else if (GameParameters._.Type == GameType.Hardcore)
                {
                    mySelectedOption = "Hardcore";
                }
                OptionSelected = mySelectedOption;
                alreadyloaded = false;
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
                //var lt = new ScaleTransform { ScaleX = 1.1, ScaleY = 1.1, CenterX = 134, CenterY = 50 };
                //MenuDataList[OptionSelected].Margin = new Thickness(MenuDataList[OptionSelected].Margin.Left - 10, MenuDataList[OptionSelected].Margin.Top - 10, 0,0);
                //MenuDataList[OptionSelected].LayoutTransform = lt;
                //MenuDataList[OptionSelected].Opacity = 1;
                TimerManager._.AddNewTimer(true, 15, true, null, ActionDefil);
                TimerManager._.AddNewTimer(true, 15, true, null, FadeIn);

            }else if(oldscreen is PlayerSelectMenu)
            {
                thisistheend = false;
                movelocked = true;
                var pressstart = (PlayerSelectMenu)oldscreen;
                optionmode = false;
                _wizard = w;
                OptionMoved = new Dictionary<string, string>();
                OptionSelected = "Classic";
                mySelectedOption = "Classic";
                if (GameParameters._.Type == GameType.Crazy)
                {
                    mySelectedOption = "Crazy";
                }
                else if (GameParameters._.Type == GameType.Hardcore)
                {
                    mySelectedOption = "Hardcore";
                }
                OptionSelected = mySelectedOption;
                alreadyloaded = false;
                if (_menuDataList == null)
                {
                    _menuDataList = new Dictionary<string, Image>();
                    _menuLabelList = new Dictionary<string, Label>();
                    _wizard.WindowDispatcher.Invoke(DispatcherPriority.Normal,
                                                    new Action(() => LoadMenuImagePrevious(pressstart, mySelectedOption)));
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
            }
            else if (oldscreen is OptionMenu)
            {
                thisistheend = false;
                movelocked = true;
                var pressstart = (OptionMenu)oldscreen;
                optionmode = true;
                _wizard = w;
                OptionMoved = new Dictionary<string, string>();
                OptionSelected = "Classic";
                mySelectedOption = "Classic";
                OptionSelected = mySelectedOption;
                alreadyloaded = false;
                if (_menuDataList == null)
                {
                    _menuDataList = new Dictionary<string, Image>();
                    _menuLabelList = new Dictionary<string, Label>();
                    _wizard.WindowDispatcher.Invoke(DispatcherPriority.Normal,
                                                    new Action(() => LoadMenuImagePrevious(pressstart, mySelectedOption)));
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
                TimerManager._.AddNewTimer(true, 15, true, null, FadeInModeOption);
            }

        }

        public void SwitchOption(String s)
        {
            if(OptionMoved.Where(c => c.Value != "").Count() == 0)
            {
                OptionMoved[OptionSelected] = s;
                OptionSelected = s;
                MenuLabelList["Indisponible"].Opacity = OptionSelected != "Hardcore" ? 0 : 1;
            }
        }

        public override void KeyUp(Key k) { }

        public override void KeyDown(Key k)
        {
            if (!movelocked)
            {
                if (KeyAction._.KeysMenu.ContainsKey(k) && KeyAction._.KeysMenu[k] == "Right")
                {
                    PlaySound._.TypeSoundList["Selection"].Play();
                    switch (OptionSelected)
                    {
                        case "Classic":
                            SwitchOption("Crazy");
                            break;
                        case "Crazy":
                            SwitchOption("Hardcore");
                            break;
                        case "Hardcore":
                            SwitchOption("Classic");
                            break;
                    }

                }
                else if (KeyAction._.KeysMenu.ContainsKey(k) && KeyAction._.KeysMenu[k] == "Left")
                {
                    PlaySound._.TypeSoundList["Selection"].Play();
                    switch (OptionSelected)
                    {
                        case "Classic":
                            SwitchOption("Hardcore");
                            break;
                        case "Crazy":
                            SwitchOption("Classic");
                            break;
                        case "Hardcore":
                            SwitchOption("Crazy");
                            break;
                    }
                }
                else if (KeyAction._.KeysMenu.ContainsKey(k) && KeyAction._.KeysMenu[k] == "Enter")
                {
                    
                    if (OptionSelected == "Classic")
                    {
                        movelocked = true;
                        PlaySound._.TypeSoundList["Valid"].Play();
                        GameParameters._.Type = GameType.Classic;
                        thisistheend = true;
                        _wizard.NextScreen(optionmode ? ScreenType.GeneralOptions : ScreenType.PlayerCound);
                    }
                    else if (OptionSelected == "Crazy")
                    {
                        movelocked = true;
                        PlaySound._.TypeSoundList["Valid"].Play();
                        GameParameters._.Type = GameType.Crazy;
                        thisistheend = true;
                        _wizard.NextScreen(optionmode ? ScreenType.GeneralOptions : ScreenType.PlayerCound);
                    }
                    else
                    {
                        PlaySound._.TypeSoundList["Error"].Play();
                    }
                }
                else if (KeyAction._.KeysMenu.ContainsKey(k) && KeyAction._.KeysMenu[k] == "Escape")
                {
                    movelocked = true;
                    PlaySound._.TypeSoundList["Cancel"].Play();
                    thisistheend = true;
                    _wizard.NextScreen(optionmode ? ScreenType.Options : ScreenType.MainMenu);
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
            MenuDataList.Add("BoxGame", old.MenuDataList["BoxGame"]);
            MenuLabelList.Add("BoxGame", old.MenuLabelList["BoxGame"]);
        }

        public void LoadMenuImagePrevious(OptionMenu old, string mySelectedOption)
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

        public void LoadMenuImagePrevious(PlayerSelectMenu old, string mySelectedOption)
        {
            MenuDataList.Add("Sky", old.MenuDataList["Sky"]);
            MenuDataList.Add("Black", old.MenuDataList["Black"]);
            MenuDataList.Add("Bomb", old.MenuDataList["Bomb"]);
            MenuDataList.Add("Eisti", old.MenuDataList["Eisti"]);
            MenuDataList.Add("2", old.MenuDataList["2"]);
            MenuDataList.Add("BoxGame", old.MenuDataList["BoxGame"]);
            MenuLabelList.Add("BoxGame", old.MenuLabelList["BoxGame"]);
            MenuDataList.Add("BoxD" + mySelectedOption, old.MenuDataList["BoxD" + mySelectedOption]);
            MenuDataList.Add("BoxG" + mySelectedOption, old.MenuDataList["BoxG" + mySelectedOption]);
            MenuLabelList.Add("Box" + mySelectedOption, old.MenuLabelList["Box" + mySelectedOption]);
        }

        public void LoadMenuImage()
        {
            var theMiddle = "";
            var theLeft = "";
            var theRight = "";
            switch (mySelectedOption)
            {
                case "Classic":
                    theMiddle = "Classic";
                    theRight = "Crazy";
                    theLeft = "Hardcore";
                    break;
                case "Crazy":
                    theMiddle = "Crazy";
                    theRight = "Hardcore";
                    theLeft = "Classic";
                    break;
                case "Hardcore":
                    theMiddle = "Hardcore";
                    theRight = "Classic";
                    theLeft = "Crazy";
                    break;

            }
            for(var i = 0; i < 3; i++)
            {
                var g2 = new Image
                {
                    Source = GameParameters._.MenutextureList["Crocheleft"],
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top,
                    Margin = new Thickness((i == 0) ? -190 : -540 + (i-1)*700, (i == 0) ? 150 : 380 , 0.0, 0.0),
                    Opacity = (i == 0) ? 0.8 : 0.5,
                    Width = 80,
                    Height = 400
                };
                var lt = new ScaleTransform { ScaleX = 0.6, ScaleY = 0.7, CenterX = 134, CenterY = 50 };
                g2.LayoutTransform = lt;
                if (i == 0)
                {
                    MenuDataList.Add("BoxG" + theMiddle, g2);
                }
                else if (i == 1)
                {
                    MenuDataList.Add("BoxG" + theLeft, g2);
                }
                else
                {
                    MenuDataList.Add("BoxG" + theRight, g2);
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
                if(i == 0)
                {
                    MenuDataList.Add("BoxD" + theMiddle, g3);
                }else if(i == 1)
                {
                    MenuDataList.Add("BoxD" + theLeft, g3);
                }else
                {
                    MenuDataList.Add("BoxD" + theRight, g3);
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
                Source = GameParameters._.MenutextureList[theMiddle],
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0.0, 200, 0.0, 0.0),
                Opacity = 1,
                Width = 150,
                Height = 150
            };
            MenuDataList.Add("P" + theMiddle, gc1);

            var gc2 = new Image
            {
                Source = GameParameters._.MenutextureList[theRight],
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(370.0, 420, 0.0, 0.0),
                Opacity = 0.2,
                Width = 150,
                Height = 150
            };
            MenuDataList.Add("P" + theRight, gc2);

            var gc3 = new Image
            {
                Source = GameParameters._.MenutextureList[theLeft],
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(-370.0, 420, 0.0, 0.0),
                Opacity = 0.2,
                Width = 150,
                Height = 150
            };
            MenuDataList.Add("P" + theLeft, gc3);

            OptionMoved.Add("Classic", "");
            OptionMoved.Add("Crazy", "");
            OptionMoved.Add("Hardcore", "");

        }

        public void LoadMenuLabel()
        {
            var theMiddle = "";
            var theLeft = "";
            var theRight = "";
            switch (mySelectedOption)
            {
                case "Classic":
                    theMiddle = "Classic";
                    theRight = "Crazy";
                    theLeft = "Hardcore";
                    break;
                case "Crazy":
                    theMiddle = "Crazy";
                    theRight = "Hardcore";
                    theLeft = "Classic";
                    break;
                case "Hardcore":
                    theMiddle = "Hardcore";
                    theRight = "Classic";
                    theLeft = "Crazy";
                    break;

            }
            var l = new Label { Content = theMiddle, FontSize = 30, Foreground = new SolidColorBrush(Colors.White), HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(0.0, 320, 0, 0), FontFamily = new FontFamily(GameParameters._.Font) };
            MenuLabelList.Add("Box" + theMiddle, l);
            var l2 = new Label { Content = theRight, FontSize = 30, Foreground = new SolidColorBrush(Colors.Gray), HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(350.0, 550, 0, 0), FontFamily = new FontFamily(GameParameters._.Font) };
            MenuLabelList.Add("Box" + theRight, l2);
            var l3 = new Label { Content = theLeft, FontSize = 30, Foreground = new SolidColorBrush(Colors.Gray), HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(-350.0, 550, 0, 0), FontFamily = new FontFamily(GameParameters._.Font) };
            MenuLabelList.Add("Box" + theLeft, l3);
            var l4 = new Label { Content = "Indisponible", FontSize = 35, Opacity = 0, Foreground = new SolidColorBrush(Colors.Red), HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(0.0, 150, 0, 0), FontFamily = new FontFamily(GameParameters._.Font) };
            MenuLabelList.Add("Indisponible", l4);
            var l5 = new Label { Content = "Entrée pour confirmer / Echap pour retour", FontSize = 28, Foreground = new SolidColorBrush(Colors.White), HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(0.0, 600, 0, 0), FontFamily = new FontFamily(GameParameters._.Font) };
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
            MenuDataList["BoxGame"].Opacity = 1;
            var lt = (ScaleTransform)MenuDataList["BoxGame"].LayoutTransform;
            if (lt.ScaleX > 0.81)
            {
                MenuDataList["Bomb"].Margin = new Thickness(MenuDataList["Bomb"].Margin.Left + 270.0 / 21.0, MenuDataList["Bomb"].Margin.Top, 0.0, 0.0);
                MenuDataList["Eisti"].Margin = new Thickness(MenuDataList["Eisti"].Margin.Left + 270.0 / 21.0, MenuDataList["Eisti"].Margin.Top, 0.0, 0.0);
                MenuDataList["2"].Margin = new Thickness(MenuDataList["2"].Margin.Left + 270.0 / 21.0, MenuDataList["2"].Margin.Top, 0.0, 0.0);
                MenuDataList["BoxGame"].Margin = new Thickness(MenuDataList["BoxGame"].Margin.Left - 270.0 / 20.0, MenuDataList["BoxGame"].Margin.Top - 230.0 / 20.0, 0, 0);

                lt.ScaleX = lt.ScaleX - 0.4 / 20.0;
                lt.ScaleY = lt.ScaleY - 0.4 / 20.0;
                MenuLabelList["BoxGame"].Margin = new Thickness(MenuLabelList["BoxGame"].Margin.Left - 270.0 / 20.0, MenuLabelList["BoxGame"].Margin.Top - 240.0 / 20.0, 0, 0);
                MenuLabelList["BoxGame"].FontSize -= 0.7;
            }
            else
            {
                t.AutoReset = false;
                if(!alreadyloaded)
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
                    movelocked = false;
                }
            }

        }

        public void FadeOutOption(Timer t)
        {
            Canvas.SetZIndex(MenuDataList["BoxD" + mySelectedOption], 0);
            Canvas.SetZIndex(MenuDataList["BoxG" + mySelectedOption], 0);
            Canvas.SetZIndex(MenuLabelList["Box" + mySelectedOption], 0);
            var lt = (ScaleTransform)MenuDataList["BoxD" + mySelectedOption].LayoutTransform;
            var lt2 = (ScaleTransform)MenuDataList["BoxG" + mySelectedOption].LayoutTransform;
            if (lt.ScaleY < 0.69)
            {
                MenuDataList["BoxD" + mySelectedOption].Margin = new Thickness(MenuDataList["BoxD" + mySelectedOption].Margin.Left + 540.0 / 20.0, MenuDataList["BoxD" + mySelectedOption].Margin.Top + 70.0 / 20.0, 0, 0);
                MenuDataList["BoxG" + mySelectedOption].Margin = new Thickness(MenuDataList["BoxG" + mySelectedOption].Margin.Left + 350.0 / 20.0, MenuDataList["BoxG" + mySelectedOption].Margin.Top + 70.0 / 20.0, 0, 0);

                lt.ScaleY = lt.ScaleY + 0.5 / 20.0;
                lt2.ScaleY = lt2.ScaleY + 0.5 / 20.0;
                MenuLabelList["Box" + mySelectedOption].Margin = new Thickness(MenuLabelList["Box" + mySelectedOption].Margin.Left + 445.0 / 20.0, MenuLabelList["Box" + mySelectedOption].Margin.Top + 220.0 / 20.0, 0, 0);
                MenuLabelList["Box" + mySelectedOption].FontSize += 0.3;
            }
            else
            {
                t.AutoReset = false;
                if (!alreadyloaded)
                {
                    alreadyloaded = true;
                    MenuDataList.Remove("BoxD" + mySelectedOption);
                    MenuDataList.Remove("BoxG" + mySelectedOption);
                    MenuLabelList.Remove("Box" + mySelectedOption);
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
                    movelocked = false;
                }
            }

        }

        private void FadeInModeOption(object sender, ElapsedEventArgs e)
        {
            _wizard.WindowDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => FadeInModeOptionT((Timer)sender)));

        }

        public void FadeInModeOptionT(Timer t)
        {
            MenuDataList["BoxGeneral"].Opacity = 1;
            var lt = (ScaleTransform)MenuDataList["BoxGeneral"].LayoutTransform;
            if (lt.ScaleX > 0.81)
            {
                MenuDataList["BoxGeneral"].Margin = new Thickness(MenuDataList["BoxGeneral"].Margin.Left - 240.0 / 20.0, MenuDataList["BoxGeneral"].Margin.Top - 170.0 / 20.0, 0, 0);

                lt.ScaleX = lt.ScaleX - 0.4 / 20.0;
                lt.ScaleY = lt.ScaleY - 0.4 / 20.0;
                MenuLabelList["BoxGeneral"].Margin = new Thickness(MenuLabelList["BoxGeneral"].Margin.Left - 250.0 / 20.0, MenuLabelList["BoxGeneral"].Margin.Top - 180.0 / 20.0, 0, 0);
                MenuLabelList["BoxGeneral"].FontSize -= 0.7;
            }
            else if (!alreadyloaded)
            {
                t.AutoReset = false;
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
                //MenuLabelList.Remove("Loading");
                foreach (var img in MenuDataList)
                {
                    _wizard.Grid.Children.Add(img.Value);
                }
                foreach (var lab in MenuLabelList)
                {
                    _wizard.Grid.Children.Add(lab.Value);
                }
                //SwitchOption("Time", true);
                movelocked = false;
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
                if((copy.ElementAt(i).Key + copy.ElementAt(i).Value).Contains("Classic") && (copy.ElementAt(i).Key + copy.ElementAt(i).Value).Contains("Crazy"))
                {
                    theone = "Hardcore";
                }
                else if ((copy.ElementAt(i).Key + copy.ElementAt(i).Value).Contains("Crazy") && (copy.ElementAt(i).Key + copy.ElementAt(i).Value).Contains("Hardcore"))
                {
                    theone = "Classic";
                }else
                {
                    theone = "Crazy";
                }

                if(MenuLabelList["Box" + copy.ElementAt(i).Value].Margin.Left == 0)
                {
                    OptionMoved[copy.ElementAt(i).Key] = "";
                }else
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
