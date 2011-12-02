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
    class KeyOption : Screenv2
    {
        private Wizard _wizard;
        private Dictionary<string, Image> _menuDataList;
        private Dictionary<string, Label> _menuLabelList;
        private String OptionSelected;
        private Dictionary<String, String> OptionMoved;
        private bool thisistheend;
        private bool alreadyloaded;
        private String ItemSelected;

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
            var pressstart = (OptionMenu)oldscreen;
            _wizard = w;
            OptionMoved = new Dictionary<String, String>();
            OptionSelected = "1_None";
            ItemSelected = "";
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
            if (OptionMoved.Where(c => c.Value != "").Count() == 0)
            {
                OptionMoved[OptionSelected] = s;
                OptionSelected = s;
            }
        }

        public override void KeyUp(Key k) { }

        public override void KeyDown(Key k)
        {
            if (ItemSelected == "")
            {
                MenuLabelList["LabelInfo"].Foreground = new SolidColorBrush(Colors.White);
                MenuLabelList["LabelInfo"].Content = "Appuyez sur entrée pour modifier une touche";
                if (KeyAction._.KeysMenu.ContainsKey(k) && KeyAction._.KeysMenu[k] == "Down")
                {
                    if (OptionSelected.Split('_').Count() > 1)
                    {
                        switch (OptionSelected.Split('_')[1])
                        {
                            case "None":
                                SwitchOption(OptionSelected.Split('_')[0] + "_Up");
                                break;
                            case "Up":
                                SwitchOption(OptionSelected.Split('_')[0] + "_Down");
                                break;
                            case "Down":
                                SwitchOption(OptionSelected.Split('_')[0] + "_Right");
                                break;
                            case "Right":
                                SwitchOption(OptionSelected.Split('_')[0] + "_Left");
                                break;
                            case "Left":
                                SwitchOption("Confirm");
                                break;
                        }
                    }
                }

                else if (KeyAction._.KeysMenu.ContainsKey(k) && KeyAction._.KeysMenu[k] == "Up")
                {
                    if (OptionSelected.Split('_').Count() > 1)
                    {
                        switch (OptionSelected.Split('_')[1])
                        {
                            case "Up":
                                SwitchOption(OptionSelected.Split('_')[0] + "_None");
                                break;
                            case "Down":
                                SwitchOption(OptionSelected.Split('_')[0] + "_Up");
                                break;
                            case "Right":
                                SwitchOption(OptionSelected.Split('_')[0] + "_Down");
                                break;
                            case "Left":
                                SwitchOption(OptionSelected.Split('_')[0] + "_Right");
                                break;
                        }
                    }
                    else
                    {
                        switch (OptionSelected)
                        {
                            case "Confirm":
                                SwitchOption("1_Left");
                                break;
                            case "Quit":
                                SwitchOption("1_Left");
                                break;
                        }
                    }
                }
                else if (KeyAction._.KeysMenu.ContainsKey(k) && KeyAction._.KeysMenu[k] == "Right")
                {
                    if (OptionSelected.Split('_').Count() > 1)
                    {
                        switch (OptionSelected.Split('_')[0])
                        {
                            case "1":
                                SwitchOption("2_" + OptionSelected.Split('_')[1]);
                                break;
                            case "2":
                                SwitchOption("3_" + OptionSelected.Split('_')[1]);
                                break;
                            case "3":
                                SwitchOption("4_" + OptionSelected.Split('_')[1]);
                                break;
                        }
                    }
                    else
                    {
                        switch (OptionSelected)
                        {
                            case "Confirm":
                                SwitchOption("Quit");
                                break;
                        }
                    }

                }
                else if (KeyAction._.KeysMenu.ContainsKey(k) && KeyAction._.KeysMenu[k] == "Left")
                {
                    if (OptionSelected.Split('_').Count() > 1)
                    {
                        switch (OptionSelected.Split('_')[0])
                        {
                            case "2":
                                SwitchOption("1_" + OptionSelected.Split('_')[1]);
                                break;
                            case "3":
                                SwitchOption("2_" + OptionSelected.Split('_')[1]);
                                break;
                            case "4":
                                SwitchOption("3_" + OptionSelected.Split('_')[1]);
                                break;
                        }
                    }
                    else
                    {
                        switch (OptionSelected)
                        {
                            case "Quit":
                                SwitchOption("Confirm");
                                break;
                        }
                    }
                }
                else if (KeyAction._.KeysMenu.ContainsKey(k) && KeyAction._.KeysMenu[k] == "Enter")
                {
                    if (OptionSelected == "Quit")
                    {
                        resetLabel();
                    }
                    else if (OptionSelected == "Confirm")
                    {
                        if(KeyAction._.checkAllKeys())
                        {
                            validLabel();
                            thisistheend = true;
                            _wizard.NextScreen(ScreenType.Options);
                        }else
                        {
                            MenuLabelList["LabelInfo"].Foreground = new SolidColorBrush(Colors.Red);
                            MenuLabelList["LabelInfo"].Content = "Impossible : Certaines touches sont vides !!";
                        }
                        
                    }else
                    {
                        ItemSelected = OptionSelected;
                        Canvas.SetZIndex(MenuDataList["BlackActive"], 1);
                        Canvas.SetZIndex(MenuLabelList["LabelActive"], 2);
                        MenuDataList["BlackActive"].Opacity = 0.75;
                        MenuLabelList["LabelActive"].Opacity = 1;
                    }
                }
            }else
            {
                if(KeyAction._.KeysPlayer.ContainsKey(k))
                {
                    var theact = KeyAction._.KeysPlayer[k];
                    KeyAction._.KeysPlayer.Remove(k);
                    MenuLabelList["Value" + theact].Content = "Vide";
                    MenuLabelList["Value" + theact].Foreground = new SolidColorBrush(Colors.Red);
                }
                KeyAction._.ReplaceKey(ItemSelected, k);
                var oldlenght = MenuLabelList["Value" + ItemSelected].Content.ToString().Length;
                MenuLabelList["Value" + ItemSelected].Content = k.ToString();
                MenuLabelList["Value" + ItemSelected].Foreground = new SolidColorBrush(Colors.White);
                if (oldlenght > 5)
                {
                    MenuLabelList["Value" + ItemSelected].Margin = new Thickness(MenuLabelList["Value" + ItemSelected].Margin.Left, MenuLabelList["Value" + ItemSelected].Margin.Top - (oldlenght - 5), 0.0, 0.0);
                }
                if (MenuLabelList["Value" + ItemSelected].Content.ToString().Length > 5)
                {
                    MenuLabelList["Value" + ItemSelected].FontSize -= MenuLabelList["Value" + ItemSelected].Content.ToString().Length - 5;
                    MenuLabelList["Value" + ItemSelected].Margin = new Thickness(MenuLabelList["Value" + ItemSelected].Margin.Left, MenuLabelList["Value" + ItemSelected].Margin.Top + (MenuLabelList["Value" + ItemSelected].Content.ToString().Length - 5), 0.0, 0.0);
                }
                
                MenuDataList["BlackActive"].Opacity = 0;
                MenuLabelList["LabelActive"].Opacity = 0;
                ItemSelected = "";
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
            MenuDataList.Add("BoxTouche", old.MenuDataList["BoxTouche"]);
            MenuLabelList.Add("BoxTouche", old.MenuLabelList["BoxTouche"]);
            var l = new Label { Content = "Chargement...", FontSize = 40, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(150, 500, 0, 0) };
            MenuLabelList.Add("Loading", l);
            OptionMoved.Add("1_None", "");
            OptionMoved.Add("1_Right", "");
            OptionMoved.Add("1_Left", "");
            OptionMoved.Add("1_Up", "");
            OptionMoved.Add("1_Down", "");
            OptionMoved.Add("2_None", "");
            OptionMoved.Add("2_Right", "");
            OptionMoved.Add("2_Left", "");
            OptionMoved.Add("2_Up", "");
            OptionMoved.Add("2_Down", "");
            OptionMoved.Add("3_None", "");
            OptionMoved.Add("3_Right", "");
            OptionMoved.Add("3_Left", "");
            OptionMoved.Add("3_Up", "");
            OptionMoved.Add("3_Down", "");
            OptionMoved.Add("4_None", "");
            OptionMoved.Add("4_Right", "");
            OptionMoved.Add("4_Left", "");
            OptionMoved.Add("4_Up", "");
            OptionMoved.Add("4_Down", "");
            OptionMoved.Add("Confirm", "");
            OptionMoved.Add("Quit", "");
        }

        public void LoadMenuImage()
        {
            var g = new Image
            {
                Source = GameParameters._.MenutextureList["Box"],
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(100, 575, 0.0, 0.0),
                Opacity = 0.6,
                Width = 268,
                Height = 100
            };
            var lt2 = new ScaleTransform { ScaleX = 0.6, ScaleY = 0.6, CenterX = 134, CenterY = 50 };
            g.LayoutTransform = lt2;
            MenuDataList.Add("BoxConfirm", g);

            var g3 = new Image
            {
                Source = GameParameters._.MenutextureList["Box"],
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(350, 575, 0.0, 0.0),
                Opacity = 0.6,
                Width = 268,
                Height = 100
            };
            var lt3 = new ScaleTransform { ScaleX = 0.6, ScaleY = 0.6, CenterX = 134, CenterY = 50 };
            g3.LayoutTransform = lt3;
            MenuDataList.Add("BoxQuit", g3);

            var gp1 = new Image
            {
                Source = GameParameters._.MenutextureList["Box"],
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(13, 280, 0.0, 0.0),
                Opacity = 0.4,
                Width = 538,
                Height = 200
            };
            var ltp1 = new ScaleTransform { ScaleX = 0.27, ScaleY = 1.4, CenterX = 134, CenterY = 50 };
            gp1.LayoutTransform = ltp1;
            MenuDataList.Add("BoxJoueur1", gp1);

            var gp2 = new Image
            {
                Source = GameParameters._.MenutextureList["Box"],
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(13 + 150, 280, 0.0, 0.0),
                Opacity = 0.4,
                Width = 538,
                Height = 200
            };
            var ltp2 = new ScaleTransform { ScaleX = 0.27, ScaleY = 1.4, CenterX = 134, CenterY = 50 };
            gp2.LayoutTransform = ltp2;
            MenuDataList.Add("BoxJoueur2", gp2);

            var gp3 = new Image
            {
                Source = GameParameters._.MenutextureList["Box"],
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(13 + 300, 280, 0.0, 0.0),
                Opacity = 0.4,
                Width = 538,
                Height = 200
            };
            var ltp3 = new ScaleTransform { ScaleX = 0.27, ScaleY = 1.4, CenterX = 134, CenterY = 50 };
            gp3.LayoutTransform = ltp3;
            MenuDataList.Add("BoxJoueur3", gp3);

            var gp4 = new Image
            {
                Source = GameParameters._.MenutextureList["Box"],
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(13 + 450, 280, 0.0, 0.0),
                Opacity = 0.4,
                Width = 538,
                Height = 200
            };
            var ltp4 = new ScaleTransform { ScaleX = 0.27, ScaleY = 1.4, CenterX = 134, CenterY = 50 };
            gp4.LayoutTransform = ltp4;
            MenuDataList.Add("BoxJoueur4", gp4);


            var g4 = new Image
            {
                Source = GameParameters._.MenutextureList["Box"],
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(95, 340, 0.0, 0.0),
                Opacity = 0.7,
                Width = 268,
                Height = 100
            };
            var lt4 = new ScaleTransform { ScaleX = 0.25, ScaleY = 0.4, CenterX = 134, CenterY = 50 };
            g4.LayoutTransform = lt4;
            MenuDataList.Add("BoxMovable", g4);

            var gb = new Image
            {
                Source = GameParameters._.MenutextureList["Black"],
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0.0, 0.0, 0.0, 0.0),
                Opacity = 0,
                Width = 2000,
                Height = 2000
            };
            MenuDataList.Add("BlackActive", gb);
        }

        public void resetLabel()
        {
            KeyAction._.resetDefaultKey();
            foreach (var label in MenuLabelList)
            {
                if (label.Key.Contains("Value") && !label.Key.Contains("Confirm") && !label.Key.Contains("Quit"))
                {
                    label.Value.Foreground = new SolidColorBrush(Colors.White);
                    label.Value.Content = KeyAction._.GetKey(label.Key.Substring(5));
                }
            }
        }

        public void validLabel()
        {
            KeyAction._.SaveKey();
            GameParameters._.Save();
        }

        public void LoadMenuLabel()
        {
            const int seedleft = 20;
            const int seedtop = 200;
            var l = new Label { Content = "Appuyez sur entrée pour modifier une touche", FontSize = 22, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(seedleft + 60, seedtop, 0, 0) };
            MenuLabelList.Add("LabelInfo", l);
            var lp1 = new Label { Content = "Joueur 1", FontSize = 20, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(seedleft + 15, seedtop + 100, 0, 0) };
            MenuLabelList.Add("LabelJoueur1", lp1);
            var lp2 = new Label { Content = "Joueur 2", FontSize = 20, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(seedleft + 165, seedtop + 100, 0, 0) };
            MenuLabelList.Add("LabelJoueur2", lp2);
            var lp3 = new Label { Content = "Joueur 3", FontSize = 20, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(seedleft + 315, seedtop + 100, 0, 0) };
            MenuLabelList.Add("LabelJoueur3", lp3);
            var lp4 = new Label { Content = "Joueur 4", FontSize = 20, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(seedleft + 465, seedtop + 100, 0, 0) };
            MenuLabelList.Add("LabelJoueur4", lp4);
            
            for(var i=0;i<4;i++)
            {
                var lac1 = new Label { Content = "Bombe :", FontSize = 18, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(seedleft + 0 + 150*i, seedtop + 140, 0, 0) };
                MenuLabelList.Add("Label" + (i + 1) + "_None", lac1);
                var ltou1 = new Label { Content = KeyAction._.GetKey((i + 1) + "_None"), FontSize = 18, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(seedleft + 75 + 150 * i, seedtop + 140, 0, 0)};
                MenuLabelList.Add("Value" + (i + 1) + "_None", ltou1);
                var lac3 = new Label { Content = "Haut :", FontSize = 18, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(seedleft + 0 + 150 * i, seedtop + 180, 0, 0) };
                MenuLabelList.Add("Label" + (i + 1) + "_Up", lac3);
                var ltou3 = new Label { Content = KeyAction._.GetKey((i + 1) + "_Up"), FontSize = 18, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(seedleft + 75 + 150 * i, seedtop + 180, 0, 0) };
                MenuLabelList.Add("Value" + (i + 1) + "_Up", ltou3);
                var lac2 = new Label { Content = "Bas :", FontSize = 18, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(seedleft + 0 + 150 * i, seedtop + 220, 0, 0) };
                MenuLabelList.Add("Label" + (i + 1) + "_Down", lac2);
                var ltou2 = new Label { Content = KeyAction._.GetKey((i + 1) + "_Down"), FontSize = 18, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(seedleft + 75 + 150 * i, seedtop + 220, 0, 0)};
                MenuLabelList.Add("Value" + (i + 1) + "_Down", ltou2);
                var lac4 = new Label { Content = "Droite :", FontSize = 18, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(seedleft + 0 + 150 * i, seedtop + 260, 0, 0) };
                MenuLabelList.Add("Label" + (i + 1) + "_Right", lac4);
                var ltou4 = new Label { Content = KeyAction._.GetKey((i + 1) + "_Right"), FontSize = 18, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(seedleft + 75 + 150 * i, seedtop + 260, 0, 0)};
                MenuLabelList.Add("Value" + (i + 1) + "_Right", ltou4);
                var lac5 = new Label { Content = "Gauche :", FontSize = 18, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(seedleft + 0 + 150 * i, seedtop + 300, 0, 0) };
                MenuLabelList.Add("Label" + (i + 1) + "_Left", lac5);
                var ltou5 = new Label { Content = KeyAction._.GetKey((i + 1) + "_Left"), FontSize = 18, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(seedleft + 75 + 150 * i, seedtop + 300, 0, 0)};
                MenuLabelList.Add("Value" + (i + 1) + "_Left", ltou5);
            }

            foreach (var label in MenuLabelList)
            {
                if(label.Key.Contains("Value") && label.Value.Content.ToString().Length > 5)
                {
                    label.Value.FontSize -= label.Value.Content.ToString().Length - 5;
                    label.Value.Margin = new Thickness(label.Value.Margin.Left, label.Value.Margin.Top + (label.Value.Content.ToString().Length - 5), 0.0, 0.0);
                }
            }

            var conf = new Label { Content = "Confirmer", FontSize = 20, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(130, 585, 0.0, 0.0) };
            MenuLabelList.Add("ValueConfirm", conf);

            var quit = new Label { Content = "Par défaut", FontSize = 20, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(380, 585, 0.0, 0.0) };
            MenuLabelList.Add("ValueQuit", quit);

            var la = new Label { Content = "Appuyez sur une touche", FontSize = 30, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(150, 300, 0, 0), Opacity = 0};
            MenuLabelList.Add("LabelActive", la);
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
            MenuDataList["BoxTouche"].Opacity = 1;
            var lt = (ScaleTransform)MenuDataList["BoxTouche"].LayoutTransform;
            if (lt.ScaleX > 0.81)
            {
                MenuDataList["BoxTouche"].Margin = new Thickness(MenuDataList["BoxTouche"].Margin.Left - 165.0 / 20.0, MenuDataList["BoxTouche"].Margin.Top - 245.0 / 20.0, 0, 0);

                lt.ScaleX = lt.ScaleX - 0.4 / 20.0;
                lt.ScaleY = lt.ScaleY - 0.4 / 20.0;
                MenuLabelList["BoxTouche"].Margin = new Thickness(MenuLabelList["BoxTouche"].Margin.Left - 175.0 / 20.0, MenuLabelList["BoxTouche"].Margin.Top - 255.0 / 20.0, 0, 0);
                MenuLabelList["BoxTouche"].FontSize -= 0.7;
            }
            else if (!alreadyloaded)
            {
                t.AutoReset = false;
                alreadyloaded = true;
                _wizard.WindowDispatcher.Invoke(DispatcherPriority.Normal, new Action(LoadMenuImage));
                _wizard.WindowDispatcher.Invoke(DispatcherPriority.Normal, new Action(LoadMenuLabel));
                for (var i = _wizard.Grid.Children.Count - 1; i > -1; i--)
                {
                    if (!(_wizard.Grid.Children[i] is Grid))
                    {
                        _wizard.Grid.Children.RemoveAt(i);
                    }
                }
                MenuLabelList.Remove("Loading");
                foreach (var img in MenuDataList)
                {
                    _wizard.Grid.Children.Add(img.Value);
                }
                foreach (var lab in MenuLabelList)
                {
                    _wizard.Grid.Children.Add(lab.Value);
                }
                //SwitchOption("1_None");
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
                var blui = "Value" + OptionMoved[copy.ElementAt(i).Key];
                var t1 = MenuLabelList["Value" + OptionMoved[copy.ElementAt(i).Key]].Margin.Left;
                var t2 = MenuLabelList["Value" + OptionMoved[copy.ElementAt(i).Key]].Margin.Top;
                if (Math.Abs((MenuDataList["BoxMovable"].Margin.Left - (MenuLabelList["Value" + OptionMoved[copy.ElementAt(i).Key]].Margin.Left))) <= 0.05 && (Math.Abs((MenuDataList["BoxMovable"].Margin.Top - MenuLabelList["Value" + OptionMoved[copy.ElementAt(i).Key]].Margin.Top) + 5)) <= 0.05)
                {
                    OptionMoved[copy.ElementAt(i).Key] = "";
                }
                else
                {
                    var thenewleft = MenuDataList["BoxMovable"].Margin.Left -
                                     (((MenuLabelList["Value" + copy.ElementAt(i).Key].Margin.Left) -
                                      MenuLabelList["Value" + OptionMoved[copy.ElementAt(i).Key]].Margin.Left) / 5.0);
                    //var t3 = MenuLabelList["Value" + copy.ElementAt(i).Key].Margin.Top;
                    //var t4 = MenuLabelList["Value" + OptionMoved[copy.ElementAt(i).Key]].Margin.Top;
                    var thenewtop = MenuDataList["BoxMovable"].Margin.Top - (
                                     (MenuLabelList["Value" + copy.ElementAt(i).Key].Margin.Top -
                                      MenuLabelList["Value" + OptionMoved[copy.ElementAt(i).Key]].Margin.Top) / 5.0);
                    if (OptionMoved[copy.ElementAt(i).Key] == "Confirm" || OptionMoved[copy.ElementAt(i).Key] == "Quit")
                    {
                        var lt = (ScaleTransform)MenuDataList["BoxMovable"].LayoutTransform;
                        if (lt.ScaleX < 0.4)
                        {
                            lt.ScaleX += (0.4 - 0.25) / 5.0;
                        }
                    }
                    else
                    {
                        var lt = (ScaleTransform)MenuDataList["BoxMovable"].LayoutTransform;
                        if (lt.ScaleX > 0.25)
                        {
                            lt.ScaleX -= (0.4 - 0.25) / 5.0;
                        }
                    }
                    MenuDataList["BoxMovable"].Margin = new Thickness(thenewleft, thenewtop, 0.0, 0.0);
                }

            }
        }
    }
}
