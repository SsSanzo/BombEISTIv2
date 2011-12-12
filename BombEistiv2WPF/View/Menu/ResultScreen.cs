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
    class ResultScreen : Screenv2
    {
        private Wizard _wizard;
        private Dictionary<string, Image> _menuDataList;
        private Dictionary<string, Label> _menuLabelList;
        private String OptionSelected;
        private Dictionary<String, String> OptionMoved;
        private bool thisistheend;
        private bool alreadyloaded;
        private bool movelocked;
        private int anim;

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
            movelocked = true;
            _wizard = w;
            anim = 0;
            OptionMoved = new Dictionary<string, string>();
            OptionSelected = "Rejouer";
            alreadyloaded = false;
            var pressstart = (GameScreen) oldscreen;
            if (_menuDataList == null)
            {
                _menuDataList = new Dictionary<string, Image>();
                _menuLabelList = new Dictionary<string, Label>();
                _wizard.WindowDispatcher.Invoke(DispatcherPriority.Normal, new Action(() => LoadMenuImagePrevious(pressstart)));
            }
            LoadMenuImage();
            LoadMenuLabel();
            foreach (var img in MenuDataList)
            {
                if (img.Key != "Over" && img.Key != "Hurry")
                {
                    _wizard.Grid.Children.Add(img.Value);
                }
                
            }
            foreach (var lab in MenuLabelList)
            {
                _wizard.Grid.Children.Add(lab.Value);
            }
            TimerManager._.AddNewTimer(true, 15, true, null, ActionDefil);
            TimerManager._.AddNewTimer(true, 15, true, null, FadeIn);
        }

        private void LoadMenuImagePrevious(GameScreen old)
        {
            MenuDataList.Add("Hurry", old.MenuDataList["Hurry"]);
            MenuDataList.Add("Over", old.MenuDataList["Over"]);
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
                    SwitchOption("Retour");

                }
                else if (KeyAction._.KeysMenu.ContainsKey(k) && KeyAction._.KeysMenu[k] == "Left")
                {
                    SwitchOption("Rejouer");
                }
                else if (KeyAction._.KeysMenu.ContainsKey(k) && KeyAction._.KeysMenu[k] == "Enter")
                {
                    if(OptionSelected == "Rejouer")
                    {
                        thisistheend = true;
                        _wizard.NextScreen(ScreenType.Game);
                    }else
                    {
                        thisistheend = true;
                        _wizard.NextScreen(ScreenType.PressStart);
                    }
                }
            }
        }

        public override void Hide()
        {


        }


        public void LoadMenuImage()
        {
            var gc1 = new Image
            {
                Source = GameParameters._.MenutextureList["Black"],
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0.0, 0.0, 0.0, 0.0),
                Opacity = 0,
                Width = 1000,
                Height = 1000
            };
            MenuDataList.Add("Black", gc1);

            var gc2 = new Image
            {
                Source = GameParameters._.MenutextureList["Score"],
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0.0, 50, 0.0, 0.0),
                Opacity = 0,
                Width = 400,
                Height = 200
            };
            MenuDataList.Add("Score", gc2);
            var index = 0;
            foreach (var v in Score._.Id_Victory)
            {
                var gc = new Image
                {
                    Source = GameParameters._.MenutextureList["Ray"],
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness((v % 2 == 0) ? 100 : -400, (v < 3) ? 30 : 230, 0.0, 0.0),
                    Opacity = 0,
                    Width = 240,
                    Height = 240
                };
                var lt = new RotateTransform();
                gc.LayoutTransform = lt;
                MenuDataList.Add("Ray_" + v, gc);

                var gp = new Image
                {
                    Source = Texture._.TypeavatarList["Player_" + GameParameters._.PlayerSkin[v]],
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top,
                    Opacity = 0,
                    Width = 60,
                    Height = 60
                };
                if (Score._.Id_Victory.Count == 1)
                {
                    gp.Margin = new Thickness(0.0, 400, 0.0, 0.0);
                }else if (Score._.Id_Victory.Count == 2)
                {
                    gp.Margin = new Thickness(-200 + (index*400), 400, 0.0, 0.0);
                }else if (Score._.Id_Victory.Count == 3)
                {
                    gp.Margin = new Thickness(-300 + (index*300), 400, 0.0, 0.0);
                }else
                {
                    gp.Margin = new Thickness(-450 + (index * 300), 400, 0.0, 0.0);
                }
                MenuDataList.Add("Victoire_" + v, gp);
                index++;
            }

            for(var i = 0;i<GameParameters._.PlayerCount;i++)
            {
                var gc = new Image
                {
                    Source = Texture._.TypeavatarList["Player_" + GameParameters._.PlayerSkin[(i + 1)]],
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top,
                    Margin = new Thickness((i%2 == 1) ? 100 : -400, (i < 2) ? 310 : 410, 0.0, 0.0),
                    Opacity = 0,
                    Width = 60,
                    Height = 60
                };
                MenuDataList.Add("Player_" + (i+1), gc);
            }

            var g6 = new Image
            {
                Source = GameParameters._.MenutextureList["Box"],
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(-305, 550, 0.0, 0.0),
                Opacity = 0,
                Width = 268,
                Height = 100
            };
            var lt5 = new ScaleTransform { ScaleX = 0.5, ScaleY = 0.5, CenterX = 134, CenterY = 50 };
            g6.LayoutTransform = lt5;
            MenuDataList.Add("Box", g6);

            OptionMoved.Add("Rejouer", "");
            OptionMoved.Add("Retour", "");

        }

        public void LoadMenuLabel()
        {
            if(Score._.Id_Victory.Count == 0)
            {
                var l3 = new Label { Content = "Aucun survivant !", Opacity = 0, FontSize = 35, HorizontalAlignment = HorizontalAlignment.Center, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(0.0, 300, 0.0, 0.0) };
                MenuLabelList.Add("Condition", l3);
            }
            else if (Score._.Id_Victory.Count > 1)
            {
                var l5 = new Label { Content = "Survivants", Opacity = 0, FontSize = 35, HorizontalAlignment = HorizontalAlignment.Center, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(0.0, 300, 0.0, 0.0) };
                MenuLabelList.Add("Condition", l5);
            }else
            {
                var l4 = new Label { Content = "Victoire de", Opacity = 0, FontSize = 35, HorizontalAlignment = HorizontalAlignment.Center, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(0.0, 300, 0.0, 0.0) };
                MenuLabelList.Add("Condition", l4);
            }

            for (var i = 0; i < GameParameters._.PlayerCount; i++)
            {
                var l = new Label { Content = "Tués : " + Score._.GetScore(i+1), Opacity = 0, FontSize = 25, HorizontalAlignment = HorizontalAlignment.Center, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness((i % 2 == 1) ? 280 : -220, (i < 2) ? 300 : 400, 0.0, 0.0) };
                MenuLabelList.Add("scoreP" + (i+1), l);
                var l2 = new Label { Content = "Victoire : " + Score._.GetSurvive(i + 1), Opacity = 0, FontSize = 25, HorizontalAlignment = HorizontalAlignment.Center, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness((i % 2 == 1) ? 310 : -190, (i < 2) ? 330 : 430, 0.0, 0.0) };
                MenuLabelList.Add("surviveP" + (i + 1), l2);
            }

            var lp1 = new Label { Content = "Rejouer", Opacity = 0, FontSize = 25, HorizontalAlignment = HorizontalAlignment.Center, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(-300, 550, 0.0, 0.0) };
            MenuLabelList.Add("Rejouer", lp1);
            var lp2 = new Label { Content = "Retour", Opacity = 0, FontSize = 25, HorizontalAlignment = HorizontalAlignment.Center, Foreground = new SolidColorBrush(Colors.White), Margin = new Thickness(300, 550, 0.0, 0.0) };
            MenuLabelList.Add("Retour", lp2);
        }

        private void ActionDefil(object sender, ElapsedEventArgs e)
        {
            if (!thisistheend)
            {
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

        private void Tournoiement(object sender, ElapsedEventArgs e)
        {
            if(!thisistheend)
            {
                _wizard.WindowDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(Tourne));
            }else
            {
                var t = (Timer) sender;
                t.AutoReset = false;
            }
            
        }

        private void FadeOut(object sender, ElapsedEventArgs e)
        {
            _wizard.WindowDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => FadeOutOption((Timer)sender)));

        }


        public void FadeInOption(Timer t)
        {
            if(anim < 100)
            {
                MenuDataList["Hurry"].Opacity = 0;
                MenuDataList["Over"].Opacity = 1;
                anim++;
            }else if(anim == 100 && MenuDataList["Black"].Opacity < 0.6)
            {
                MenuDataList["Over"].Opacity = 0;
                MenuDataList["Black"].Opacity += 0.05;
            }else if(anim < 130 && MenuDataList["Black"].Opacity >= 0.6)
            {
                anim++;
            }
            else if (anim == 130 && MenuDataList["Score"].Opacity < 1)
            {
                MenuDataList["Score"].Opacity += 0.05;
            }
            else if (anim < 150 && MenuDataList["Score"].Opacity >= 1)
            {
                anim++;
            }
            else if (anim == 150 && MenuLabelList["Condition"].Opacity < 1)
            {
                MenuLabelList["Condition"].Opacity += 0.1;
                foreach (var image in MenuDataList.Where(c => c.Key.Contains("Victoire_")))
                {
                    image.Value.Opacity += 0.1;
                }
            }else if(anim < 350 && MenuLabelList["Condition"].Opacity >= 1)
            {
                anim++;
            }
            else if (anim == 350 && MenuLabelList["Condition"].Opacity > 0)
            {
                MenuLabelList["Condition"].Opacity -= 0.1;
                foreach (var image in MenuDataList.Where(c => c.Key.Contains("Victoire_")))
                {
                    image.Value.Opacity -= 0.1;
                }
            }
            else if (anim < 359 && MenuLabelList["Condition"].Opacity <= 0)
            {
                anim++;
            }else if(anim == 359)
            {
                TimerManager._.AddNewTimer(true, 15, true, null, Tournoiement);
                anim++;
            }
            else if (anim == 360 && MenuDataList["Player_1"].Opacity < 1)
            {
                foreach (var im in MenuDataList.Where(c => c.Key.Contains("Player_")))
                {
                    im.Value.Opacity += 0.1;
                }
                foreach (var im in MenuLabelList.Where(c => c.Key.Contains("P")))
                {
                    im.Value.Opacity += 0.1;
                }
                foreach (var im in MenuDataList.Where(c => c.Key.Contains("Ray_")))
                {
                    im.Value.Opacity += 0.1;
                }
            }
            else if (anim < 420 && MenuDataList["Player_1"].Opacity >= 1)
            {
                anim++;
            }else if(anim == 420)
            {
                MenuDataList["Box"].Opacity = 0.6;
                MenuLabelList["Rejouer"].Opacity = 1;
                MenuLabelList["Retour"].Opacity = 1;
                movelocked = false;
                t.AutoReset = false;
            }
        }

        public void Tourne()
        {
            foreach (var image in MenuDataList.Where(c => c.Key.Contains("Ray")))
            {
                var rt = (RotateTransform) image.Value.LayoutTransform;
                //Canvas.SetTop(image.Value, 100);
                //Canvas.SetLeft(image.Value, 100);
                
                rt.CenterX = 100;
                rt.CenterY = 100;
                rt.Angle += 1;
                image.Value.LayoutTransform = rt;
                
                //image.Value.Margin = new Thickness(image.Value.Margin.Left, image.Value.Margin.Top - ((45 - (rt.Angle % 90)) / 30.0), 0.0, 0.0);
            }
        }

        public void FadeOutOption(Timer t)
        {

        }



        public void zoomin()
        {

            var copy = OptionMoved.Where(c => c.Value != "");
            for (var i = 0; i < copy.Count(); i++)
            {
                if (Math.Abs(MenuDataList["Box"].Margin.Left - MenuLabelList[copy.ElementAt(i).Value].Margin.Left) - 20 <= 0.05)
                {
                    OptionMoved[copy.ElementAt(i).Key] = "";
                }else if(copy.ElementAt(i).Value == "Retour")
                {
                    MenuDataList["Box"].Margin = new Thickness(MenuDataList["Box"].Margin.Left + 600/10.0, 550, 0.0, 0.0);
                }
                else if (copy.ElementAt(i).Value == "Rejouer")
                {
                    MenuDataList["Box"].Margin = new Thickness(MenuDataList["Box"].Margin.Left - 600 / 10.0, 550, 0.0, 0.0);
                }
            }
        }
    }
}
