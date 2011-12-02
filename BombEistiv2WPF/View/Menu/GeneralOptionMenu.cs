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
    class GeneralOptionMenu : Screenv2
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
            OptionSelected = "Time";
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

        public void SwitchOption(String s, bool withoutRefresh = false)
        {
            MenuLabelList["Help"].Foreground = new SolidColorBrush(Colors.White);
            var theaddition = "\n(<Entrée> pour modifier)";
            switch (s)
            {
                case "Time":
                    MenuLabelList["Help"].Content = "Règle le temps de jeu \n(En secondes)";
                    break;
                case "Explode":
                    MenuLabelList["Help"].Content = "Règle le délai d'explosion \n(En secondes)";
                    break;
                case "Lives":
                    MenuLabelList["Help"].Content = "Règle le nombre de vie \npar défault";
                    break;
                case "Soft":
                    MenuLabelList["Help"].Content = "Règle le nombre de bloc \ndestructible dans une partie. \nDetermine aussi le nombre \nmaximum de bonus";
                    break;
                case "PowerUp":
                    MenuLabelList["Help"].Content = "Augmenter la portée de \nla bombe de 1";
                    MenuLabelList["Help"].Content += theaddition;
                    break;
                case "PowerMax":
                    MenuLabelList["Help"].Content = "Augmente au maximum la \nportée de la bombe";
                    MenuLabelList["Help"].Content += theaddition;
                    break;
                case "BombUp":
                    MenuLabelList["Help"].Content = "Augmente le nombre de \nbombes disponibles de 1";
                    MenuLabelList["Help"].Content += theaddition;
                    break;
                case "Kick":
                    MenuLabelList["Help"].Content = "Permet au joueur de \npousser une bombe";
                    MenuLabelList["Help"].Content += theaddition;
                    break;
                case "SpeedUp":
                    MenuLabelList["Help"].Content = "Le joueur se déplace \nun petit peu plus rapidement";
                    MenuLabelList["Help"].Content += theaddition;
                    break;
                case "SpeedMax":
                    MenuLabelList["Help"].Content = "Le joueur se déplace \nà la vitesse maximum";
                    MenuLabelList["Help"].Content += theaddition;
                    break;
                case "Life":
                    MenuLabelList["Help"].Content = "Le joueur gagne \nune vie";
                    MenuLabelList["Help"].Content += theaddition;
                    break;
                case "Teleport":
                    MenuLabelList["Help"].Content = "Téléporte le joueur à un \nendroit aléatoire sur le \nterrain.";
                    MenuLabelList["Help"].Content += theaddition;
                    break;
                case "PowerDown":
                    MenuLabelList["Help"].Content = "Diminue la portée de \nla bombe de 1";
                    MenuLabelList["Help"].Content += theaddition;
                    break;
                case "BombDown":
                    MenuLabelList["Help"].Content = "Diminue le nombre de \nbombes disponibles de 1";
                    MenuLabelList["Help"].Content += theaddition;
                    break;
                case "SpeedDown":
                    MenuLabelList["Help"].Content = "Le joueur devient \ntrès lent";
                    MenuLabelList["Help"].Content += theaddition;
                    break;
                case "ChangeDirection":
                    MenuLabelList["Help"].Content = "Les commandes du joueur \nsont inversés";
                    MenuLabelList["Help"].Content += theaddition;
                    break;
                case "Confirm":
                    MenuLabelList["Help"].Content = "Confirmez vos changements";
                    break;
                case "Quit":
                    MenuLabelList["Help"].Content = "Revenez aux valeurs \npar défaut";
                    break;
                default:
                    MenuLabelList["Help"].Content = "";
                    break;
            }
            if(OptionMoved.Where(c => c.Value != "").Count() == 0 && !withoutRefresh)
            {
                OptionMoved[OptionSelected] = s;
                OptionSelected = s;
            }
        }

        public void ChangeOption(String s, int value)
        {
            switch (s)
            {
                case "Time":
                    if (Convert.ToInt32(MenuLabelList["ValueTime"].Content) + value * 10 <= 300 && Convert.ToInt32(MenuLabelList["ValueTime"].Content) + value * 10 >= 60)
                    {
                        MenuLabelList["ValueTime"].Content = Convert.ToInt32(MenuLabelList["ValueTime"].Content) + value * 10;
                    }
                    
                    break;
                case "Explode":
                    if (Convert.ToInt32(MenuLabelList["ValueExplode"].Content) + value <= 5 && Convert.ToInt32(MenuLabelList["ValueExplode"].Content) + value >= 1)
                    {
                        MenuLabelList["ValueExplode"].Content = Convert.ToInt32(MenuLabelList["ValueExplode"].Content) + value;
                    }
                    break;
                case "Lives":
                    if (Convert.ToInt32(MenuLabelList["ValueLives"].Content) + value <= 9 && Convert.ToInt32(MenuLabelList["ValueLives"].Content) + value >= 1)
                    {
                        MenuLabelList["ValueLives"].Content = Convert.ToInt32(MenuLabelList["ValueLives"].Content) + value;
                    }
                    break;
                case "Soft":
                    var total = 0;
                    foreach (var ut in Enum.GetValues(typeof(UpgradeType)))
                    {
                        total += Convert.ToInt32(MenuLabelList["Value"+ut].Content);
                    }

                    if (Convert.ToInt32(MenuLabelList["ValueSoft"].Content) + value >= total && Convert.ToInt32(MenuLabelList["ValueSoft"].Content) + value <= 164)
                    {
                        SwitchOption("Soft",true);
                        MenuLabelList["ValueSoft"].Foreground = new SolidColorBrush(Colors.White);
                        MenuLabelList["ValueSoft"].Content = Convert.ToInt32(MenuLabelList["ValueSoft"].Content) + value;
                    }
                    else if (Convert.ToInt32(MenuLabelList["ValueSoft"].Content) + value < total)
                    {
                        MenuLabelList["Help"].Foreground = new SolidColorBrush(Colors.Red);
                        MenuLabelList["ValueSoft"].Foreground = new SolidColorBrush(Colors.Red);
                        MenuLabelList["Help"].Content = "ATTENTION ! \nPlus assez de destructibles \npar rapport au nombre total \nde bonus !";
                    }else
                    {
                        MenuLabelList["Help"].Foreground = new SolidColorBrush(Colors.Red);
                        MenuLabelList["ValueSoft"].Foreground = new SolidColorBrush(Colors.Red);
                        MenuLabelList["Help"].Content = "Limite maximum atteinte !";
                    }
                    break;
                default:
                    var total2 = 0;
                    foreach (var ut in Enum.GetValues(typeof(UpgradeType)))
                    {
                        total2 += Convert.ToInt32(MenuLabelList["Value"+ut].Content);
                    }

                    if (Convert.ToInt32(MenuLabelList["ValueSoft"].Content) >= total2 + value && Convert.ToInt32(MenuLabelList["Value" + s].Content) + value >= 0)
                    {
                        SwitchOption(s, true);
                        MenuLabelList["ValueSoft"].Foreground = new SolidColorBrush(Colors.White);
                        MenuLabelList["Value" + s].Content = Convert.ToInt32(MenuLabelList["Value" + s].Content) + value;
                    }
                    else if (Convert.ToInt32(MenuLabelList["Value" + s].Content) < total2 + value && Convert.ToInt32(MenuLabelList["Value" + s].Content) + value > 0)
                    {
                        MenuLabelList["Help"].Foreground = new SolidColorBrush(Colors.Red);
                        MenuLabelList["ValueSoft"].Foreground = new SolidColorBrush(Colors.Red);
                        MenuLabelList["Help"].Content = "ATTENTION ! \nPlus assez de destructibles \npar rapport au nombre total \nde bonus !";
                    }
                    break;
            }
        }

        public override void KeyUp(Key k) { }

        public override void KeyDown(Key k)
        {
            if (KeyAction._.KeysMenu.ContainsKey(k) && KeyAction._.KeysMenu[k] == "Down")
            {
                if(ItemSelected == "")
                {
                    switch(OptionSelected)
                    {
                        case "Time":
                            SwitchOption("Explode");
                            break;
                        case "Explode":
                            SwitchOption("Lives");
                            break;
                        case "Lives":
                            SwitchOption("Soft");
                            break;
                        case "Soft":
                            SwitchOption("PowerUp");
                            break;
                        case "PowerUp":
                            SwitchOption("PowerMax");
                            break;
                        case "PowerMax":
                            SwitchOption("BombUp");
                            break;
                        case "BombUp":
                            SwitchOption("Kick");
                            break;
                        case "Kick":
                            SwitchOption("Confirm");
                            break;
                        case "SpeedUp":
                            SwitchOption("SpeedMax");
                            break;
                        case "SpeedMax":
                            SwitchOption("Life");
                            break;
                        case "Life":
                            SwitchOption("Teleport");
                            break;
                        case "Teleport":
                            SwitchOption("Confirm");
                            break;
                        case "PowerDown":
                            SwitchOption("BombDown");
                            break;
                        case "BombDown":
                            SwitchOption("SpeedDown");
                            break;
                        case "SpeedDown":
                            SwitchOption("ChangeDirection");
                            break;
                        case "ChangeDirection":
                            SwitchOption("Confirm");
                            break;
                    }
                }
            }
            else if (KeyAction._.KeysMenu.ContainsKey(k) && KeyAction._.KeysMenu[k] == "Up")
            {
                if(ItemSelected == "")
                {
                    switch (OptionSelected)
                    {
                        case "Explode":
                            SwitchOption("Time");
                            break;
                        case "Lives":
                            SwitchOption("Explode");
                            break;
                        case "Soft":
                            SwitchOption("Lives");
                            break;
                        case "PowerUp":
                            SwitchOption("Soft");
                            break;
                        case "PowerMax":
                            SwitchOption("PowerUp");
                            break;
                        case "BombUp":
                            SwitchOption("PowerMax");
                            break;
                        case "Kick":
                            SwitchOption("BombUp");
                            break;
                        case "SpeedUp":
                            SwitchOption("Soft");
                            break;
                        case "SpeedMax":
                            SwitchOption("SpeedUp");
                            break;
                        case "Life":
                            SwitchOption("SpeedMax");
                            break;
                        case "Teleport":
                            SwitchOption("Life");
                            break;
                        case "PowerDown":
                            SwitchOption("Soft");
                            break;
                        case "BombDown":
                            SwitchOption("PowerDown");
                            break;
                        case "SpeedDown":
                            SwitchOption("BombDown");
                            break;
                        case "ChangeDirection":
                            SwitchOption("SpeedDown");
                            break;
                        case "Confirm":
                            SwitchOption("Kick");
                            break;
                        case "Quit":
                            SwitchOption("Kick");
                            break;
                    }
                }
            }
            else if (KeyAction._.KeysMenu.ContainsKey(k) && KeyAction._.KeysMenu[k] == "Right")
            {
                if (ItemSelected == "")
                {
                    switch (OptionSelected)
                    {

                        case "Time":
                            ChangeOption("Time", 1);
                            break;
                        case "Explode":
                            ChangeOption("Explode", 1);
                            break;
                        case "Lives":
                            ChangeOption("Lives", 1);
                            break;
                        case "Soft":
                            ChangeOption("Soft", 1);
                            break;
                        case "PowerUp":
                            SwitchOption("SpeedUp");
                            break;
                        case "PowerMax":
                            SwitchOption("SpeedMax");
                            break;
                        case "BombUp":
                            SwitchOption("Life");
                            break;
                        case "Kick":
                            SwitchOption("Teleport");
                            break;
                        case "SpeedUp":
                            SwitchOption("PowerDown");
                            break;
                        case "SpeedMax":
                            SwitchOption("BombDown");
                            break;
                        case "Life":
                            SwitchOption("SpeedDown");
                            break;
                        case "Teleport":
                            SwitchOption("ChangeDirection");
                            break;
                        case "Confirm":
                            SwitchOption("Quit");
                            break;
                    }
                }else
                {
                    ChangeOption(OptionSelected, 1);
                }
            }
            else if (KeyAction._.KeysMenu.ContainsKey(k) && KeyAction._.KeysMenu[k] == "Left")
            {
                if (ItemSelected == "")
                {
                    switch (OptionSelected)
                    {
                        case "Time":
                            ChangeOption("Time", -1);
                            break;
                        case "Explode":
                            ChangeOption("Explode", -1);
                            break;
                        case "Lives":
                            ChangeOption("Lives", -1);
                            break;
                        case "Soft":
                            ChangeOption("Soft", -1);
                            break;
                        case "SpeedUp":
                            SwitchOption("PowerUp");
                            break;
                        case "SpeedMax":
                            SwitchOption("PowerMax");
                            break;
                        case "Life":
                            SwitchOption("BombUp");
                            break;
                        case "Teleport":
                            SwitchOption("Kick");
                            break;
                        case "PowerDown":
                            SwitchOption("SpeedUp");
                            break;
                        case "BombDown":
                            SwitchOption("SpeedMax");
                            break;
                        case "SpeedDown":
                            SwitchOption("Life");
                            break;
                        case "ChangeDirection":
                            SwitchOption("Teleport");
                            break;
                        case "Quit":
                            SwitchOption("Confirm");
                            break;
                    }
                }else
                {
                    ChangeOption(OptionSelected, -1);
                }
            }
            //else if (KeyAction._.KeysMenu[k] == "Enter")
            else if (KeyAction._.KeysMenu.ContainsKey(k) && KeyAction._.KeysMenu[k] == "Enter")
            {
                if(Enum.IsDefined(typeof(UpgradeType),OptionSelected))
                {
                    if(ItemSelected == "")
                    {
                        ItemSelected = OptionSelected;
                        MenuDataList["Left"].Opacity = 1;
                        MenuDataList["Right"].Opacity = 1;
                    }else
                    {
                        ItemSelected = "";
                        MenuDataList["Left"].Opacity = 0;
                        MenuDataList["Right"].Opacity = 0;
                    }
                }else if(OptionSelected == "Quit")
                {
                    resetLabel();
                }else if(OptionSelected == "Confirm")
                {
                    validLabel();
                    thisistheend = true;
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
            MenuDataList.Add("BoxGeneral", old.MenuDataList["BoxGeneral"]);
            MenuLabelList.Add("BoxGeneral", old.MenuLabelList["BoxGeneral"]);
            var l = new Label { Content = "Chargement...", FontSize = 40, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(150, 500, 0, 0) };
            MenuLabelList.Add("Loading", l);
            OptionMoved.Add("Time","");
            OptionMoved.Add("Explode", "");
            OptionMoved.Add("Lives", "");
            OptionMoved.Add("Soft", "");
            OptionMoved.Add("PowerUp", "");
            OptionMoved.Add("PowerMax", "");
            OptionMoved.Add("BombUp", "");
            OptionMoved.Add("Kick", "");
            OptionMoved.Add("SpeedUp", "");
            OptionMoved.Add("SpeedMax", "");
            OptionMoved.Add("Life", "");
            OptionMoved.Add("Teleport", "");
            OptionMoved.Add("PowerDown", "");
            OptionMoved.Add("BombDown", "");
            OptionMoved.Add("SpeedDown", "");
            OptionMoved.Add("ChangeDirection", "");
            OptionMoved.Add("Confirm", "");
            OptionMoved.Add("Quit", "");
        }

        public void LoadMenuImage()
        {
            var g2 = new Image
            {
                Source = GameParameters._.MenutextureList["Box"],
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(320, 180, 0.0, 0.0),
                Opacity = 0.7,
                Width = 348.4,
                Height = 130
            };
            var lt = new ScaleTransform { ScaleX = 0.8, ScaleY = 1.0, CenterX = 134, CenterY = 50 };
            g2.LayoutTransform = lt;
            MenuDataList.Add("BoxHelp", g2);

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

            var g4 = new Image
            {
                Source = GameParameters._.MenutextureList["Box"],
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(230, 200, 0.0, 0.0),
                Opacity = 0.7,
                Width = 268,
                Height = 100
            };
            var lt4 = new ScaleTransform { ScaleX = 0.25, ScaleY = 0.4, CenterX = 134, CenterY = 50 };
            g4.LayoutTransform = lt4;
            MenuDataList.Add("BoxMovable", g4);

            var g5 = new Image
            {
                Source = GameParameters._.MenutextureList["Left"],
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(220, 210, 0.0, 0.0),
                Opacity = 1,
                Width = 18,
                Height = 18
            };
            MenuDataList.Add("Left", g5);

            var g6 = new Image
            {
                Source = GameParameters._.MenutextureList["Right"],
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(290, 210, 0.0, 0.0),
                Opacity = 1,
                Width = 18,
                Height = 18
            };
            MenuDataList.Add("Right", g6);
        }

        public void resetLabel()
        {
            MenuLabelList["ValueTime"].Content = 180;
            MenuLabelList["ValueExplode"].Content = 3;
            MenuLabelList["ValueLives"].Content = 2;
            MenuLabelList["ValueSoft"].Content = 120;
            GameParameters._.ResetUpgradeFrequence();
            var dic = GameParameters._.GetAllUpgrades();
            for (var i = 0; i < dic.Count; i++)
            {
                MenuLabelList["Value" + dic.ElementAt(i).Key].Content = dic.ElementAt(i).Value;
            }
        }

        public void validLabel()
        {
            GameParameters._.GameTime = Convert.ToInt32(MenuLabelList["ValueTime"].Content);
            GameParameters._.ExplosionDelay = Convert.ToInt32(MenuLabelList["ValueExplode"].Content) ;
            GameParameters._.LivesCount = Convert.ToInt32(MenuLabelList["ValueLives"].Content) ;
            GameParameters._.SoftBlocCount = Convert.ToInt32(MenuLabelList["ValueSoft"].Content) ;
            var dic = GameParameters._.GetAllUpgrades();
            for (var i = 0; i < dic.Count; i++)
            {
                GameParameters._.ChangeUpgradeFrequence(dic.ElementAt(i).Key, Convert.ToInt32(MenuLabelList["Value" + dic.ElementAt(i).Key].Content));
            }
            GameParameters._.Save();
        }

        public void LoadMenuLabel()
        {
            const int seedleft = 20;
            const int seedtop = 200;
            var l = new Label { Content = "Temps de jeu :", FontSize = 22, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(seedleft, seedtop, 0, 0) };
            MenuLabelList.Add("LabelTime", l);
            var l2 = new Label { Content = GameParameters._.GameTime, FontSize = 22, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(seedleft + 220, seedtop, 0, 0) };
            MenuLabelList.Add("ValueTime", l2);
            var l3 = new Label { Content = "Temps d'explosion :", FontSize = 22, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(seedleft, seedtop + 30, 0, 0) };
            MenuLabelList.Add("LabelExplode", l3);
            var l4 = new Label { Content = GameParameters._.ExplosionDelay, FontSize = 22, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(seedleft + 220, seedtop + 30, 0, 0) };
            MenuLabelList.Add("ValueExplode", l4);
            var l5 = new Label { Content = "Nombre de vie :", FontSize = 22, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(seedleft, seedtop + 60, 0, 0) };
            MenuLabelList.Add("LabelLives", l5);
            var l6 = new Label { Content = GameParameters._.LivesCount, FontSize = 22, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(seedleft + 220, seedtop + 60, 0, 0) };
            MenuLabelList.Add("ValueLives", l6);
            var l7 = new Label { Content = "Destructibles restants :", FontSize = 25, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(seedleft + 100, seedtop + 140, 0, 0) };
            MenuLabelList.Add("LabelSoft", l7);
            var l8 = new Label { Content = GameParameters._.SoftBlocCount, FontSize = 25, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(seedleft + 370, seedtop + 140, 0, 0) };
            MenuLabelList.Add("ValueSoft", l8);
            var dic = GameParameters._.GetAllUpgrades();
            var decal = 0;
            Texture._.LoadBonusTextures();
            for (var i = 0; i < dic.Count; i++)
            {
                if(i != 0 && i%4 == 0)
                {
                    decal++;
                }
                var g = new Image
                            {
                                Source = Texture._.TypebonusList["Upgrade." + dic.ElementAt(i).Key],
                                VerticalAlignment = VerticalAlignment.Top,
                                HorizontalAlignment = HorizontalAlignment.Left,
                                Height = 40,
                                Width = 40,
                                Margin = new Thickness(seedleft + (180*decal), seedtop + 200 + (i%4)*40, 0, 0)
                            };
                MenuDataList.Add("Upgrade." + dic.ElementAt(i).Key, g);
                var lv = new Label { Content = dic.ElementAt(i).Value, FontSize = 20, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(seedleft + (180 * decal) + 75, seedtop + 200 + (i % 4) * 40, 0, 0) };
                MenuLabelList.Add("Value" + dic.ElementAt(i).Key, lv);
            }

            var ll = new Label { Content = "", FontSize = 18, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(330, 200, 0.0, 0.0) };
            MenuLabelList.Add("Help", ll);

            var conf = new Label { Content = "Confirmer", FontSize = 20, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(130, 585, 0.0, 0.0) };
            MenuLabelList.Add("ValueConfirm", conf);

            var quit = new Label { Content = "Par défaut", FontSize = 20, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(380, 585, 0.0, 0.0) };
            MenuLabelList.Add("ValueQuit", quit);
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
            else if(!alreadyloaded)
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
                SwitchOption("Time", true);
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
                var t1 = MenuLabelList["Value" + OptionMoved[copy.ElementAt(i).Key]].Margin.Left - 10;
                var t2 = MenuLabelList["Value" + OptionMoved[copy.ElementAt(i).Key]].Margin.Top;
                if (Math.Abs((MenuDataList["BoxMovable"].Margin.Left - (MenuLabelList["Value" + OptionMoved[copy.ElementAt(i).Key]].Margin.Left - 10))) <= 0.05 && Math.Abs((MenuDataList["BoxMovable"].Margin.Top - MenuLabelList["Value" + OptionMoved[copy.ElementAt(i).Key]].Margin.Top)) <= 0.05)
                {
                        var thecheck = OptionMoved[copy.ElementAt(i).Key].ToString();
                        OptionMoved[copy.ElementAt(i).Key] = "";
                        MenuDataList["Left"].Margin = new Thickness(MenuDataList["BoxMovable"].Margin.Left - 10, MenuDataList["BoxMovable"].Margin.Top + 10, 0.0, 0.0);
                        MenuDataList["Right"].Margin = new Thickness(MenuDataList["BoxMovable"].Margin.Left + 60, MenuDataList["BoxMovable"].Margin.Top + 10, 0.0, 0.0);
                        if (!(thecheck == "PowerUp" || thecheck == "PowerMax" || thecheck == "PowerDown" || thecheck == "BombUp"
                            || thecheck == "BombDown" || thecheck == "Life" || thecheck == "SpeedUp" || thecheck == "SpeedMax"
                            || thecheck == "SpeedDown" || thecheck == "Teleport" || thecheck == "ChangeDirection" || thecheck == "Kick"
                            || thecheck == "Confirm" || thecheck == "Quit"))
                        {
                            MenuDataList["Left"].Opacity = 1;
                            MenuDataList["Right"].Opacity = 1;
                        }
                        
                    }
                    else
                {
                    MenuDataList["Left"].Opacity = 0;
                    MenuDataList["Right"].Opacity = 0;
                    var thenewleft = MenuDataList["BoxMovable"].Margin.Left -
                                     (((MenuLabelList["Value" + copy.ElementAt(i).Key].Margin.Left) -
                                      MenuLabelList["Value" + OptionMoved[copy.ElementAt(i).Key]].Margin.Left)/5.0);
                    var thenewtop = MenuDataList["BoxMovable"].Margin.Top - (
                                     (MenuLabelList["Value" + copy.ElementAt(i).Key].Margin.Top -
                                      MenuLabelList["Value" + OptionMoved[copy.ElementAt(i).Key]].Margin.Top)/5.0);
                    if (OptionMoved[copy.ElementAt(i).Key] == "Confirm" || OptionMoved[copy.ElementAt(i).Key] == "Quit")
                    {
                        var lt = (ScaleTransform) MenuDataList["BoxMovable"].LayoutTransform;
                        if(lt.ScaleX < 0.4)
                        {
                            lt.ScaleX += (0.4 - 0.25)/5.0;
                        }
                    }else
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
