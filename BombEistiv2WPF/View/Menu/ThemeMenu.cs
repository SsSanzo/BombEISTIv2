using System;
using System.Collections.Generic;
using System.IO;
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
        private Dictionary<string, int> _menuOrderDataList;
        private Dictionary<string, Label> _menuLabelList;
        private String PreviewSelected;
        private Dictionary<String, String> OptionMoved;
        private bool alreadyloaded;
        private bool thisistheend;
        private bool movelocked;

        public Dictionary<string, Image> MenuDataList
        {
            get { return _menuDataList; }
        }

        public Dictionary<string, int> MenuOrderDataList
        {
            get { return _menuOrderDataList; }
        }

        public Dictionary<string, Label> MenuLabelList
        {
            get { return _menuLabelList; }
        }

        public override void Show(Control.Wizard w, Screenv2 oldscreen)
        {
            thisistheend = false;
            movelocked = true;
            var pressstart = (OptionMenu)oldscreen;
            _wizard = w;
            OptionMoved = new Dictionary<string, string>();
            _menuOrderDataList = new Dictionary<string, int>();
            var theme = Texture._.IsRandom ? "Random" : Texture._.Theme;
            PreviewSelected = "Screen" + theme;
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
        }

        public void SwitchOption(String s)
        {
            if (OptionMoved.Where(c => c.Value != "").Count() == 0)
            {
                OptionMoved[PreviewSelected] = s;
                MenuLabelList["LabelScreen"].Content = s.Substring(6);
                PreviewSelected = s;
            }
        }

        public override void KeyUp(Key k) { }

        public override void KeyDown(Key k)
        {
            if(!movelocked)
            {
                if (KeyAction._.KeysMenu.ContainsKey(k) && KeyAction._.KeysMenu[k] == "Right")
                {
                    PlaySound._.TypeSoundList["Selection"].Play();
                    if (MenuOrderDataList[PreviewSelected] == MenuOrderDataList.Count - 1)
                    {
                        SwitchOption(MenuOrderDataList.FirstOrDefault(c => c.Value == 0).Key);
                    }
                    else
                    {
                        SwitchOption(MenuOrderDataList.FirstOrDefault(c => c.Value == MenuOrderDataList[PreviewSelected] + 1).Key);
                    }

                }
                else if (KeyAction._.KeysMenu.ContainsKey(k) && KeyAction._.KeysMenu[k] == "Left")
                {
                    PlaySound._.TypeSoundList["Selection"].Play();
                    if (MenuOrderDataList[PreviewSelected] == 0)
                    {
                        SwitchOption(MenuOrderDataList.FirstOrDefault(c => c.Value == MenuOrderDataList.Count - 1).Key);
                    }
                    else
                    {
                        SwitchOption(MenuOrderDataList.FirstOrDefault(c => c.Value == MenuOrderDataList[PreviewSelected] - 1).Key);
                    }
                }
                else if (KeyAction._.KeysMenu.ContainsKey(k) && KeyAction._.KeysMenu[k] == "Enter")
                {
                    PlaySound._.TypeSoundList["Valid"].Play();
                    if (PreviewSelected.Substring(6) == "Random")
                    {
                        //var rand = new Random();
                        //var theint = rand.Next(MenuOrderDataList.Count - 1);
                        //Texture._.SetTheme(MenuOrderDataList.FirstOrDefault(c => c.Value == theint).Key.Substring(6));
                        //PlaySound._.SetTheme(MenuOrderDataList.FirstOrDefault(c => c.Value == theint).Key.Substring(6));
                        Texture._.IsRandom = true;
                        Texture._.SetTheme("Basic");
                        PlaySound._.SetTheme("Basic");
                        PlaySound._.ClearEverything(_wizard.TheWindow);
                        PlaySound._.LoadAllMusic();
                        PlaySound._.LireBoucle("MenuAll");
                        foreach (var mus in PlaySound._.TypeMusicList)
                        {
                            _wizard.Grid.Children.Add(mus.Value);
                        }
                    }
                    else
                    {
                        Texture._.SetTheme(PreviewSelected.Substring(6));
                        PlaySound._.SetTheme(PreviewSelected.Substring(6));
                        Texture._.IsRandom = false;
                        PlaySound._.ClearEverything(_wizard.TheWindow);
                        PlaySound._.LoadAllMusic();
                        PlaySound._.LireBoucle("MenuAll");
                        foreach (var mus in PlaySound._.TypeMusicList)
                        {
                            _wizard.Grid.Children.Add(mus.Value);
                        }
                    }
                    //PlaySound._.ClearEverything(_wizard.TheWindow);
                    //PlaySound._.LoadAllMusic();
                    //PlaySound._.LireBoucle("MenuAll");
                    //foreach (var mus in PlaySound._.TypeMusicList)
                    //{
                    //    _wizard.Grid.Children.Add(mus.Value);
                    //}
                    thisistheend = true;
                    _wizard.NextScreen(ScreenType.Options);
                }
                else if (KeyAction._.KeysMenu.ContainsKey(k) && KeyAction._.KeysMenu[k] == "Escape")
                {
                    thisistheend = true;
                    PlaySound._.TypeSoundList["Cancel"].Play();
                    _wizard.NextScreen(ScreenType.Options);
                }
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
            var listDir = Directory.EnumerateDirectories(GameParameters.Path + @"\" + GameParameters._.GetThemeFolder());
            var order = 0;
            foreach (var dir in listDir)
            {
                if(File.Exists(dir + @"\preview.png"))
                {
                    var gb = new Image
                    {
                        Source = Texture.LoadTheImage(dir + @"\preview.png"),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Top,
                        Margin = new Thickness(0.0, 250, 0.0, 0.0),
                        Opacity = 0,
                        Width = 337,
                        Height = 250
                    };
                    var lt4 = new ScaleTransform { ScaleX = 0.5, ScaleY = 0.5 };
                    gb.LayoutTransform = lt4;
                    MenuDataList.Add("Screen" + dir.Split('\\')[dir.Split('\\').Length - 1], gb);
                    OptionMoved.Add("Screen" + dir.Split('\\')[dir.Split('\\').Length - 1], "");
                    MenuOrderDataList.Add("Screen" + dir.Split('\\')[dir.Split('\\').Length - 1], order);
                    order++;
                }else
                {
                    var gb = new Image
                    {
                        Source = GameParameters._.MenutextureList["Preview"],
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Top,
                        Margin = new Thickness(0.0, 250, 0.0, 0.0),
                        Opacity = 0,
                        Width = 337,
                        Height = 250
                    };
                    var lt4 = new ScaleTransform { ScaleX = 0.5, ScaleY = 0.5 };
                    gb.LayoutTransform = lt4;
                    MenuDataList.Add("Screen" + dir.Split('\\')[dir.Split('\\').Length - 1], gb);
                    OptionMoved.Add("Screen" + dir.Split('\\')[dir.Split('\\').Length - 1], "");
                    MenuOrderDataList.Add("Screen" + dir.Split('\\')[dir.Split('\\').Length - 1], order);
                    order++;
                }
            }

            var grand = new Image
            {
                Source = GameParameters._.MenutextureList["Random"],
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0.0, 250, 0.0, 0.0),
                Opacity = 0,
                Width = 337,
                Height = 250
            };
            var ltr = new ScaleTransform { ScaleX = 0.5, ScaleY = 0.5 };
            grand.LayoutTransform = ltr;
            MenuDataList.Add("ScreenRandom", grand);
            OptionMoved.Add("ScreenRandom", "");
            MenuOrderDataList.Add("ScreenRandom", order);
            var theme = Texture._.IsRandom ? "Random" : Texture._.Theme;
            var zero = MenuOrderDataList["Screen" + theme];
            var un = MenuOrderDataList["Screen" + theme] == MenuOrderDataList.Count - 1
                         ? 0
                         : MenuOrderDataList["Screen" + theme] + 1;
            var moinsun = MenuOrderDataList["Screen" + theme] == 0
                         ? MenuOrderDataList.Count - 1
                         : MenuOrderDataList["Screen" + theme] - 1;
            var thedata = MenuDataList.Where(c => c.Key.StartsWith("Screen"));
            thedata.ElementAt(zero).Value.Margin = new Thickness(0.0, 250, 0.0, 0.0);
            thedata.ElementAt(zero).Value.Opacity = 1;
            var ltp = (ScaleTransform)thedata.ElementAt(zero).Value.LayoutTransform;
            ltp.ScaleX = 1.0;
            ltp.ScaleY = 1.0;
            Canvas.SetZIndex(thedata.ElementAt(zero).Value, 1);
            var lt = (ScaleTransform) thedata.ElementAt(un).Value.LayoutTransform;
            lt.ScaleX = 0.5;
            lt.ScaleY = 0.5;
            thedata.ElementAt(un).Value.Margin = new Thickness(350, 250, 0.0, 0.0);
            thedata.ElementAt(un).Value.Opacity = 0.5;
            var lt2 = (ScaleTransform)thedata.ElementAt(moinsun).Value.LayoutTransform;
            lt2.ScaleX = 0.5;
            lt2.ScaleY = 0.5;
            thedata.ElementAt(moinsun).Value.Margin = new Thickness(-350, 250, 0.0, 0.0);
            thedata.ElementAt(moinsun).Value.Opacity = 0.5;

            var gr = new Image
            {
                Source = GameParameters._.MenutextureList["Bigleft"],
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(-200.0, 480, 0.0, 0.0),
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
                Margin = new Thickness(200.0, 480, 0.0, 0.0),
                Opacity = 1,
                Width = 49,
                Height = 80
            };
            MenuDataList.Add("Bigright", gl);

        }

        

        public void LoadMenuLabel()
        {
            var l = new Label { Content = PreviewSelected.Substring(6), FontSize = 32, HorizontalAlignment = HorizontalAlignment.Center, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(0.0, 500, 0, 0) };
            MenuLabelList.Add("LabelScreen", l);
            var l2 = new Label { Content = "(Appuyez sur entrée pour valider un thème)", FontSize = 25, HorizontalAlignment = HorizontalAlignment.Center, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(0.0, 570, 0, 0) };
            MenuLabelList.Add("LabelInfo", l2);
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
                }
                movelocked = false;
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
            var copy = OptionMoved.Where(c => c.Value != "");
            for(var i=0;i<copy.Count();i++)
            {
                //init
                var enavant = MenuDataList[copy.ElementAt(i).Key];
                var thenext = MenuDataList[copy.ElementAt(i).Value];
                var sens = 0;
                if(MenuOrderDataList[copy.ElementAt(i).Key] == 0 && MenuOrderDataList[copy.ElementAt(i).Value] == (MenuOrderDataList.Count - 1))
                {
                    sens = -1;
                }else if(MenuOrderDataList[copy.ElementAt(i).Value] == 0 && MenuOrderDataList[copy.ElementAt(i).Key] == (MenuOrderDataList.Count - 1))
                {
                    sens = 1;
                }else
                {
                    sens = MenuOrderDataList[copy.ElementAt(i).Value] - MenuOrderDataList[copy.ElementAt(i).Key];
                }
                var theexit =
                    MenuDataList[
                        MenuOrderDataList.FirstOrDefault(
                        c => (c.Value == 0 && sens == -1 ? MenuOrderDataList.Count + sens : (c.Value == MenuOrderDataList.Count - 1 && sens == 1 ? - 1 + sens : c.Value + sens)) == MenuOrderDataList[copy.ElementAt(i).Key]).Key];
                var thenewcomer =
                    MenuDataList[
                        MenuOrderDataList.FirstOrDefault(
                            c => (c.Value == 0 && sens == 1 ? MenuOrderDataList.Count - sens : (c.Value == MenuOrderDataList.Count - 1 && sens == -1 ? -1 - sens : c.Value - sens)) == MenuOrderDataList[copy.ElementAt(i).Value]).Key];

                if(thenewcomer.Opacity == 0 || thenewcomer.Margin.Left == -sens*1000)
                {
                    thenewcomer.Margin = new Thickness(sens * 1000, 250, 0.0, 0.0);
                    thenewcomer.Opacity = 0.5;
                }
                
                
                Canvas.SetZIndex(thenext, 1);
                Canvas.SetZIndex(enavant, 0);
                if(Math.Abs(enavant.Margin.Left + sens*350) <= 0.05)
                {
                    OptionMoved[copy.ElementAt(i).Key] = "";
                }else
                {
                    var lt = (ScaleTransform) enavant.LayoutTransform;
                    enavant.Margin = new Thickness(enavant.Margin.Left - ((sens*350.0)/10.0), 250, 0.0, 0.0);
                    lt.ScaleX -= 0.05;
                    lt.ScaleY -= 0.05;
                    enavant.Opacity -= 0.05;

                    var lt2 = (ScaleTransform)thenext.LayoutTransform;
                    thenext.Margin = new Thickness(thenext.Margin.Left - ((sens * 350.0) / 10.0), 250, 0.0, 0.0);
                    lt2.ScaleX += 0.05;
                    lt2.ScaleY += 0.05;
                    thenext.Opacity += 0.05;

                    theexit.Margin = new Thickness(theexit.Margin.Left - ((sens * 650.0) / 10.0), 250, 0.0, 0.0);
                    thenewcomer.Margin = new Thickness(thenewcomer.Margin.Left - ((sens * 650.0) / 10.0), 250, 0.0, 0.0);
                    
                }
                
            }
        }
    }
}
