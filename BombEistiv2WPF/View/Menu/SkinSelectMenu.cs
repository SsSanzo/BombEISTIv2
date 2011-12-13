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
    class SkinSelectMenu : Screenv2
    {
        private Wizard _wizard;
        private Dictionary<string, Image> _menuDataList;
        private Dictionary<string, Label> _menuLabelList;
        private Dictionary<int, int> OptionSelected;
        private Dictionary<int, int> OptionValidate; 
        private Dictionary<int, int> OptionError;
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
            PlaySound._.Stop("MenuAll");
            PlaySound._.LireBoucle("Select");
            thisistheend = false;
            movelocked = true;
            var pressstart = (PlayerSelectMenu)oldscreen;
            _wizard = w;
            OptionMoved = new Dictionary<string, string>();
            OptionSelected = new Dictionary<int, int>();
            OptionValidate = new Dictionary<int, int>();
            OptionError = new Dictionary<int, int>();
            GameParameters._.PlayerSkin = new Dictionary<int, int>();
            GameParameters._.PlayerSkin.Add(1, 1);
            GameParameters._.PlayerSkin.Add(2, 2);
            OptionSelected.Add(1,1);
            OptionValidate.Add(1,0);
            OptionSelected.Add(2,2);
            OptionValidate.Add(2, 0);
            if(GameParameters._.PlayerCount > 2)
            {
                OptionSelected.Add(3, 3);
                OptionValidate.Add(3, 0);
                GameParameters._.PlayerSkin.Add(3, 3);
            }
            if (GameParameters._.PlayerCount > 3)
            {
                OptionSelected.Add(4, 4);
                OptionValidate.Add(4, 0);
                GameParameters._.PlayerSkin.Add(4, 4);
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

        public void SwitchOption(int p, int previous)
        {
            if (OptionValidate[p] > 0)
            {
                MenuDataList["Preview_" + p].Opacity = 0.4;
                MenuDataList["Player_" + previous].Opacity = 1;
                MenuDataList["Corner_" + p].Opacity = 1;
            }
            MenuDataList["Preview_" + p].Source = Texture._.TypeavatarList["Player_" + OptionSelected[p]];
            MenuDataList["Corner_" + p].Margin = new Thickness(MenuDataList["Player_" + OptionSelected[p]].Margin.Left, MenuDataList["Player_" + OptionSelected[p]].Margin.Top, 0.0, 0.0);
        }

        public void ConfirmOption(int p)
        {
            if (MenuDataList["Player_" + OptionSelected[p]].Opacity != 0.4)
            {
                MenuDataList["Preview_" + p].Opacity = 1;
                MenuDataList["Player_" + OptionSelected[p]].Opacity = 0.4;
                MenuDataList["Corner_" + p].Opacity = 0.4;
                OptionValidate[p] = OptionSelected[p];
                GameParameters._.PlayerSkin[p] = OptionValidate[p];
            }else
            {
                OptionValidate[p] = -1 * OptionSelected[p];
            }

            if(OptionValidate.Where(c => c.Value <= 0).Count() == 0)
            {
                thisistheend = true;
                _wizard.NextScreen(ScreenType.PreGame);
            }
            
        }

        public override void KeyUp(Key k) { }

        public override void KeyDown(Key k)
        {
            if (!movelocked)
            {
                if (KeyAction._.KeysPlayer.ContainsKey(k) && KeyAction._.KeysPlayer[k].EndsWith("Up"))
                {
                    var p = Convert.ToInt32(KeyAction._.KeysPlayer[k].Substring(0, 1));
                    if (p <= GameParameters._.PlayerCount && OptionSelected[p] > 4)
                    {
                        OptionSelected[p] = OptionSelected[p] - 4;
                        SwitchOption(p, OptionSelected[p] + 4);
                    }
                }
                else if (KeyAction._.KeysPlayer.ContainsKey(k) && KeyAction._.KeysPlayer[k].EndsWith("Down"))
                {
                    var p = Convert.ToInt32(KeyAction._.KeysPlayer[k].Substring(0, 1));
                    if (p <= GameParameters._.PlayerCount && OptionSelected[p] < 5)
                    {
                        OptionSelected[p] = OptionSelected[p] + 4;
                        SwitchOption(p, OptionSelected[p] - 4);
                    }
                }
                else if (KeyAction._.KeysPlayer.ContainsKey(k) && KeyAction._.KeysPlayer[k].EndsWith("Right"))
                {
                    var p = Convert.ToInt32(KeyAction._.KeysPlayer[k].Substring(0, 1));
                    if (p <= GameParameters._.PlayerCount && OptionSelected[p] != 4 && OptionSelected[p] != 8)
                    {
                        OptionSelected[p] = OptionSelected[p] + 1;
                        SwitchOption(p, OptionSelected[p] - 1);
                    }
                }
                else if (KeyAction._.KeysPlayer.ContainsKey(k) && KeyAction._.KeysPlayer[k].EndsWith("Left"))
                {
                    var p = Convert.ToInt32(KeyAction._.KeysPlayer[k].Substring(0, 1));
                    if (p <= GameParameters._.PlayerCount && OptionSelected[p] != 1 && OptionSelected[p] != 5)
                    {
                        OptionSelected[p] = OptionSelected[p] - 1;
                        SwitchOption(p, OptionSelected[p] + 1);
                    }
                }
                else if (KeyAction._.KeysPlayer.ContainsKey(k) && KeyAction._.KeysPlayer[k].EndsWith("None"))
                {
                    var p = Convert.ToInt32(KeyAction._.KeysPlayer[k].Substring(0, 1));
                    if (p <= GameParameters._.PlayerCount)
                    {
                        ConfirmOption(p);
                    }
                }
                else if (KeyAction._.KeysMenu.ContainsKey(k) && KeyAction._.KeysMenu[k] == "Escape")
                {
                    thisistheend = true;
                    _wizard.NextScreen(ScreenType.PlayerCound);
                }
            }
        }

        public override void Hide()
        {


        }

        public void LoadMenuImagePrevious(PlayerSelectMenu old)
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
            var togo = "";
            if(old.MenuDataList["BoxD2P"].Opacity >= 0.8)
            {
                togo = "2P";
            }
            else if (old.MenuDataList["BoxD3P"].Opacity >= 0.8)
            {
                togo = "3P";
            }
            else if (old.MenuDataList["BoxD4P"].Opacity >= 0.8)
            {
                togo = "4P";
            }

            MenuDataList.Add("BoxDLevel", old.MenuDataList["BoxD" + togo]);
            MenuDataList.Add("BoxGLevel", old.MenuDataList["BoxG" + togo]);
            MenuLabelList.Add("BoxLevel", old.MenuLabelList["Box" + togo]);
        }

        public void LoadMenuImage()
        {
            Texture._.LoadAvatarTextures();
            for(var i=0;i<8;i++)
            {
                var gl = new Image
                {
                    Source = Texture._.TypeavatarList["Player_" + (i+1)],
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Margin = new Thickness((i < 4) ? 90 + i * 120 : 90 + (i-4) * 120, (i < 4) ? 400 : 500, 0.0, 0.0),
                    Opacity = 1,
                    Width = 60,
                    Height = 60
                };
                MenuDataList.Add("Player_"+(i+1), gl);
            }

            for (var i = 0; i < GameParameters._.PlayerCount; i++)
            {
                var gl = new Image
                {
                    Source = Texture._.TypeavatarList["Player_" + OptionSelected[i + 1]],
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Margin = new Thickness(90 + i*120, 210, 0.0, 0.0),
                    Opacity = 0.4,
                    Width = 60,
                    Height = 60
                };
                MenuDataList.Add("Preview_" + (i + 1), gl);

                var ga = new Image
                {
                    Source = GameParameters._.MenutextureList["CornerP" + (i+1)],
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Margin = new Thickness(MenuDataList["Player_" + OptionSelected[(i + 1)]].Margin.Left, MenuDataList["Player_" + OptionSelected[(i + 1)]].Margin.Top, 0.0, 0.0),
                    Opacity = 1,
                    Width = 60,
                    Height = 60
                };
                MenuDataList.Add("Corner_" + (i + 1), ga);
            }

            var er = new Image
            {
                Source = GameParameters._.MenutextureList["Error"],
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0.0, 0.0, 0.0, 0.0),
                Opacity = 0,
                Width = 60,
                Height = 60
            };
            MenuDataList.Add("Error", er);
        }

        public void LoadMenuLabel()
        {

            var l = new Label { Content = "P1", FontSize = 30, Foreground = new SolidColorBrush(Colors.Red), Margin = new Thickness(100, 270, 0, 0) };
            MenuLabelList.Add("LabelP1", l);
            var l2 = new Label { Content = "P2", FontSize = 30, Foreground = new SolidColorBrush(Colors.Blue), Margin = new Thickness(220, 270, 0, 0) };
            MenuLabelList.Add("LabelP2", l2);
            var l3 = new Label { Content = "P3", FontSize = 30, Foreground = new SolidColorBrush(Colors.LimeGreen), Margin = new Thickness(340, 270, 0, 0), Opacity = (GameParameters._.PlayerCount > 2) ? 1 : 0 };
            MenuLabelList.Add("LabelP3", l3);
            var l4 = new Label { Content = "P4", FontSize = 30, Foreground = new SolidColorBrush(Colors.Orange), Margin = new Thickness(460, 270, 0, 0), Opacity = (GameParameters._.PlayerCount > 3) ? 1 : 0 };
            MenuLabelList.Add("LabelP4", l4);
            var d = KeyAction._.GetKey("1_Down").ToString();
            var u = KeyAction._.GetKey("1_Up").ToString();
            var r = KeyAction._.GetKey("1_Right").ToString();
            var le = KeyAction._.GetKey("1_Left").ToString();
            var b = KeyAction._.GetKey("1_None").ToString();
            var l5 = new Label { Content = "Utilisez les touches de jeu pour naviguer ! \\ Touche bombe : Valider\n\nExemple : Joueur 1 : " + u + ", " + d + ", " + r + ", " + le + ", bombe : " + b, FontSize = 18, HorizontalAlignment = HorizontalAlignment.Center, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(0, 570, 0, 0) };
            MenuLabelList.Add("Info", l5);
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
            Canvas.SetZIndex(MenuDataList["BoxDLevel"], 2);
            Canvas.SetZIndex(MenuDataList["BoxGLevel"], 2);
            Canvas.SetZIndex(MenuLabelList["BoxLevel"], 2);
            var lt = (ScaleTransform)MenuDataList["BoxDLevel"].LayoutTransform;
            var lt2 = (ScaleTransform)MenuDataList["BoxGLevel"].LayoutTransform;
            if (lt.ScaleY > 0.21)
            {
                MenuDataList["BoxDLevel"].Margin = new Thickness(MenuDataList["BoxDLevel"].Margin.Left - 150.0 / 20.0, MenuDataList["BoxDLevel"].Margin.Top - 70.0 / 20.0, 0, 0);
                MenuDataList["BoxGLevel"].Margin = new Thickness(MenuDataList["BoxGLevel"].Margin.Left - 60.0 / 20.0, MenuDataList["BoxGLevel"].Margin.Top - 70.0 / 20.0, 0, 0);

                lt.ScaleY = lt.ScaleY - 0.5 / 20.0;
                lt2.ScaleY = lt2.ScaleY - 0.5 / 20.0;
                MenuLabelList["BoxLevel"].Margin = new Thickness(MenuLabelList["BoxLevel"].Margin.Left - 100.0 / 20.0, MenuLabelList["BoxLevel"].Margin.Top - 220.0 / 20.0, 0, 0);
                MenuLabelList["BoxLevel"].FontSize -= 0.3;
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

            var copy = OptionValidate.Where(c => c.Value < 0);
            for (var i = 0; i < copy.Count(); i++)
            {
                if(MenuDataList["Error"].Opacity != 1)
                {
                    Canvas.SetZIndex(MenuDataList["Error"], 1);
                    MenuDataList["Error"].Margin = new Thickness(MenuDataList["Player_" + (-1 * copy.ElementAt(i).Value)].Margin.Left, MenuDataList["Player_" + (-1 * copy.ElementAt(i).Value)].Margin.Top, 0.0, 0.0);
                    MenuDataList["Error"].Opacity = 1;
                    OptionValidate[copy.ElementAt(i).Key] = 0;
                    TimerManager._.AddNewTimer(false, 150, true, null, Error);
                }
            }
        }

        public void ErrorDown()
        {
            MenuDataList["Error"].Opacity = 0;
        }

        private void Error(object sender, ElapsedEventArgs e)
        {
            _wizard.WindowDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(ErrorDown));

        }
    }
}
