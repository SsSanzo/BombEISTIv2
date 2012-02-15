using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using BombEistiv2WPF.Environment;

namespace BombEistiv2WPF.View
{
    public class InGameMenu
    {
        public Label thedisplayedTime;
        public Dictionary<int, Label> lifeLabel;
        public Dictionary<int, Label> cdLabel;
        public Dictionary<int, Image> BombImage;
        public Dictionary<int, Image> BombStack;
        //public Label lifejoueur1;
        //public Label lifejoueur2;
        //public Label lifejoueur3;
        //public Label lifejoueur4;
        private static InGameMenu _this;

        private InGameMenu()
        {
            thedisplayedTime = null;
            lifeLabel = new Dictionary<int, Label>();
        }
        public static InGameMenu _
        {
            get { return _this ?? (_this = new InGameMenu()); }
        } 

        public void InitInGameMenu(Game g)
        {
            if(GameParameters._.Type != GameType.Crazy)
            {
                var nbplayer = g.TheCurrentMap.ListOfPlayer.Count;
                thedisplayedTime = null;
                lifeLabel.Clear();
                thedisplayedTime = InitLabel();
                thedisplayedTime.Margin = new Thickness(0, 4, 0, 0);
                thedisplayedTime.Content = GameParameters._.GameTime;
                var lifejoueur1 = InitLabel();
                lifejoueur1.Margin = new Thickness(-400, 4, 0, 0);
                lifejoueur1.Content = g.TheCurrentMap.ListOfPlayer.ElementAt(0).Lives.ToString();
                lifeLabel.Add(g.TheCurrentMap.ListOfPlayer.ElementAt(0).Id, lifejoueur1);
                var lifejoueur2 = InitLabel();
                lifejoueur2.Margin = new Thickness(-200, 4, 0, 0);
                lifejoueur2.Content = g.TheCurrentMap.ListOfPlayer.ElementAt(1).Lives.ToString();
                lifeLabel.Add(g.TheCurrentMap.ListOfPlayer.ElementAt(1).Id, lifejoueur2);
                if (nbplayer > 2)
                {
                    var lifejoueur3 = InitLabel();
                    lifejoueur3.Margin = new Thickness(300, 4, 0, 0);
                    lifejoueur3.Content = g.TheCurrentMap.ListOfPlayer.ElementAt(2).Lives.ToString();
                    lifeLabel.Add(g.TheCurrentMap.ListOfPlayer.ElementAt(2).Id, lifejoueur3);

                }
                if (nbplayer > 3)
                {
                    var lifejoueur4 = InitLabel();
                    lifejoueur4.Margin = new Thickness(500, 4, 0, 0);
                    lifejoueur4.Content = g.TheCurrentMap.ListOfPlayer.ElementAt(3).Lives.ToString();
                    lifeLabel.Add(g.TheCurrentMap.ListOfPlayer.ElementAt(3).Id, lifejoueur4);

                }
            }else
            {
                cdLabel = new Dictionary<int, Label>();
                var nbplayer = g.TheCurrentMap.ListOfPlayer.Count;
                thedisplayedTime = null;
                lifeLabel.Clear();
                thedisplayedTime = InitLabel();
                thedisplayedTime.Margin = new Thickness(0, 4, 0, 0);
                thedisplayedTime.Content = GameParameters._.GameTime;
                var lifejoueur1 = InitLabel();
                lifejoueur1.Margin = new Thickness(-490, 4, 0, 0);
                lifejoueur1.Content = g.TheCurrentMap.ListOfPlayer.ElementAt(0).Lives.ToString();
                lifeLabel.Add(g.TheCurrentMap.ListOfPlayer.ElementAt(0).Id, lifejoueur1);
                var cdjoueur1 = InitLabelCD();
                cdjoueur1.Margin = new Thickness(-400, 9, 0, 0);
                cdLabel.Add(g.TheCurrentMap.ListOfPlayer.ElementAt(0).Id, cdjoueur1);

                var lifejoueur2 = InitLabel();
                lifejoueur2.Margin = new Thickness(-200, 4, 0, 0);
                lifejoueur2.Content = g.TheCurrentMap.ListOfPlayer.ElementAt(1).Lives.ToString();
                lifeLabel.Add(g.TheCurrentMap.ListOfPlayer.ElementAt(1).Id, lifejoueur2);
                var cdjoueur2 = InitLabelCD();
                cdjoueur2.Margin = new Thickness(-110, 9, 0, 0);
                cdLabel.Add(g.TheCurrentMap.ListOfPlayer.ElementAt(1).Id, cdjoueur2);
                if (nbplayer > 2)
                {
                    var lifejoueur3 = InitLabel();
                    lifejoueur3.Margin = new Thickness(180, 4, 0, 0);
                    lifejoueur3.Content = g.TheCurrentMap.ListOfPlayer.ElementAt(2).Lives.ToString();
                    lifeLabel.Add(g.TheCurrentMap.ListOfPlayer.ElementAt(2).Id, lifejoueur3);
                    var cdjoueur3 = InitLabelCD();
                    cdjoueur3.Margin = new Thickness(270, 9, 0, 0);
                    cdLabel.Add(g.TheCurrentMap.ListOfPlayer.ElementAt(2).Id, cdjoueur3);
                }
                if (nbplayer > 3)
                {
                    var lifejoueur4 = InitLabel();
                    lifejoueur4.Margin = new Thickness(440, 4, 0, 0);
                    lifejoueur4.Content = g.TheCurrentMap.ListOfPlayer.ElementAt(3).Lives.ToString();
                    lifeLabel.Add(g.TheCurrentMap.ListOfPlayer.ElementAt(3).Id, lifejoueur4);
                    var cdjoueur4 = InitLabelCD();
                    cdjoueur4.Margin = new Thickness(530, 9, 0, 0);
                    cdLabel.Add(g.TheCurrentMap.ListOfPlayer.ElementAt(3).Id, cdjoueur4);
                }
                BombImage = new Dictionary<int, Image>();
                BombStack = new Dictionary<int, Image>();

            }
            
        }

        public List<Image> AllTheFace(Game g)
        {
            if(GameParameters._.Type != GameType.Crazy)
            {
                var nbplayer = g.TheCurrentMap.ListOfPlayer.Count;
                var l = new List<Image>();
                var f1 = new Image
                {
                    Source =
                        Texture._.TypetextureList[
                            "Player_" + g.TheCurrentMap.ListOfPlayer.ElementAt(0).Skinid + "_down"],
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Width = 40,
                    Height = 40,
                    Margin = new Thickness(-460, 4, 0, 0)
                };
                l.Add(f1);
                var f2 = new Image
                {
                    Source =
                        Texture._.TypetextureList[
                            "Player_" + g.TheCurrentMap.ListOfPlayer.ElementAt(1).Skinid + "_down"],
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Width = 40,
                    Height = 40,
                    Margin = new Thickness(-260, 4, 0, 0)
                };
                l.Add(f2);
                if (nbplayer > 2)
                {
                    var f3 = new Image
                    {
                        Source =
                            Texture._.TypetextureList[
                                "Player_" + g.TheCurrentMap.ListOfPlayer.ElementAt(2).Skinid + "_down"],
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Width = 40,
                        Height = 40,
                        Margin = new Thickness(240, 4, 0, 0)
                    };
                    l.Add(f3);
                }
                if (nbplayer > 3)
                {
                    var f4 = new Image
                    {
                        Source =
                            Texture._.TypetextureList[
                                "Player_" + g.TheCurrentMap.ListOfPlayer.ElementAt(3).Skinid + "_down"],
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Width = 40,
                        Height = 40,
                        Margin = new Thickness(440, 4, 0, 0)
                    };
                    l.Add(f4);
                }
                return l;
            }else
            {
                var nbplayer = g.TheCurrentMap.ListOfPlayer.Count;
                var l = new List<Image>();
                var f1 = new Image
                {
                    Source =
                        Texture._.TypetextureList[
                            "Player_" + g.TheCurrentMap.ListOfPlayer.ElementAt(0).Skinid + "_down"],
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Width = 40,
                    Height = 40,
                    Margin = new Thickness(-550, 4, 0, 0)
                };
                l.Add(f1);
                var f2 = new Image
                {
                    Source =
                        Texture._.TypetextureList[
                            "Player_" + g.TheCurrentMap.ListOfPlayer.ElementAt(1).Skinid + "_down"],
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Width = 40,
                    Height = 40,
                    Margin = new Thickness(-260, 4, 0, 0)
                };
                l.Add(f2);
                if (nbplayer > 2)
                {
                    var f3 = new Image
                    {
                        Source =
                            Texture._.TypetextureList[
                                "Player_" + g.TheCurrentMap.ListOfPlayer.ElementAt(2).Skinid + "_down"],
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Width = 40,
                        Height = 40,
                        Margin = new Thickness(120, 4, 0, 0)
                    };
                    l.Add(f3);
                }
                if (nbplayer > 3)
                {
                    var f4 = new Image
                    {
                        Source =
                            Texture._.TypetextureList[
                                "Player_" + g.TheCurrentMap.ListOfPlayer.ElementAt(3).Skinid + "_down"],
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Width = 40,
                        Height = 40,
                        Margin = new Thickness(380, 4, 0, 0)
                    };
                    l.Add(f4);
                }
                return l;
            }
            
        }

        public List<Image> AllTheBomb(Game g)
        {
            var nbplayer = g.TheCurrentMap.ListOfPlayer.Count;
            var l = new List<Image>();
            var f1 = new Image
            {
                Source =
                    Texture._.TypetextureList["Bomb.Normal"],
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = 40,
                Height = 40,
                Margin = new Thickness(-430, 4, 0, 0)
            };
            l.Add(f1);
            BombImage.Add(g.TheCurrentMap.ListOfPlayer.ElementAt(0).Id, f1);
            var f2 = new Image
            {
                Source =
                    Texture._.TypetextureList["Bomb.Normal"],
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = 40,
                Height = 40,
                Margin = new Thickness(-140, 4, 0, 0)
            };
            l.Add(f2);
            BombImage.Add(g.TheCurrentMap.ListOfPlayer.ElementAt(1).Id, f2);
            if (nbplayer > 2)
            {
                var f3 = new Image
                {
                    Source =
                        Texture._.TypetextureList["Bomb.Normal"],
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Width = 40,
                    Height = 40,
                    Margin = new Thickness(240, 4, 0, 0)
                };
                l.Add(f3);
                BombImage.Add(g.TheCurrentMap.ListOfPlayer.ElementAt(2).Id, f3);
            }
            if (nbplayer > 3)
            {
                var f4 = new Image
                {
                    Source =
                        Texture._.TypetextureList["Bomb.Normal"],
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Width = 40,
                    Height = 40,
                    Margin = new Thickness(500, 4, 0, 0)
                };
                l.Add(f4);
                BombImage.Add(g.TheCurrentMap.ListOfPlayer.ElementAt(3).Id, f4);
            }
            return l;
        }

        public List<Image> AllTheStack(Game g)
        {
            var nbplayer = g.TheCurrentMap.ListOfPlayer.Count;
            var l = new List<Image>();
            var f1 = new Image
            {
                Source =
                    Texture._.TypetextureList["Stack_8"],
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = 40,
                Height = 40,
                Opacity = 0,
                Margin = new Thickness(-430, 4, 0, 0)
            };
            l.Add(f1);
            BombStack.Add(g.TheCurrentMap.ListOfPlayer.ElementAt(0).Id, f1);
            var f2 = new Image
            {
                Source =
                    Texture._.TypetextureList["Stack_8"],
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = 40,
                Height = 40,
                Opacity = 0,
                Margin = new Thickness(-140, 4, 0, 0)
            };
            l.Add(f2);
            BombStack.Add(g.TheCurrentMap.ListOfPlayer.ElementAt(1).Id, f2);
            if (nbplayer > 2)
            {
                var f3 = new Image
                {
                    Source =
                        Texture._.TypetextureList["Stack_8"],
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Width = 40,
                    Height = 40,
                    Opacity = 0,
                    Margin = new Thickness(240, 4, 0, 0)
                };
                l.Add(f3);
                BombStack.Add(g.TheCurrentMap.ListOfPlayer.ElementAt(2).Id, f3);
            }
            if (nbplayer > 3)
            {
                var f4 = new Image
                {
                    Source =
                        Texture._.TypetextureList["Stack_8"],
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Width = 40,
                    Height = 40,
                    Opacity = 0,
                    Margin = new Thickness(500, 4, 0, 0)
                };
                l.Add(f4);
                BombStack.Add(g.TheCurrentMap.ListOfPlayer.ElementAt(3).Id, f4);
            }
            return l;
        }

        public Label InitLabel()
        {
            var l = new Label
                        {
                            VerticalAlignment = VerticalAlignment.Top,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            FontSize = 25,
                            Foreground = new SolidColorBrush(Colors.White),
                            FontFamily = new FontFamily(GameParameters._.Font)
                        };
            return l;
        }

        public Label InitLabelCD()
        {
            var l = new Label
            {
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 12,
                Opacity = 0,
                Foreground = new SolidColorBrush(Colors.Red),
                FontFamily = new FontFamily(GameParameters._.Font)
            };
            return l;
        }

        public List<Label> AllTheLabel()
        {
            var l = new List<Label> {thedisplayedTime};
            l.AddRange(lifeLabel.Select(label => label.Value));
            return l;
        }

        public List<Label> AllTheLabelCD()
        {
            var l = new List<Label>();
            l.AddRange(cdLabel.Select(label => label.Value));
            return l;
        }

        public void changeLabel(int id, int lives)
        {
            if(lives <= 0)
            {
                Texture._.Mw.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => lifeLabel[id].Content = 0));
            }else
            {
                Texture._.Mw.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => lifeLabel[id].Content = lives));
            }
            
        }

        public void ChangeLabelTime(Timer t)
        {
            if (Convert.ToInt32(thedisplayedTime.Content) > 0)
            {
                var s = Convert.ToInt32(thedisplayedTime.Content.ToString()) - 1;
                if(s <= 45)
                {
                    thedisplayedTime.Foreground = new SolidColorBrush(Colors.Red);
                }
                
                thedisplayedTime.Content = s;
            }else
            {
                t.AutoReset = false;
            }
        }

        public void ChangeLabelTime(Timer t, Player p)
        {
            var id = p.Id;
            if (Convert.ToInt32(cdLabel[id].Content) > 1)
            {
                cdLabel[id].Opacity = 1;
                var s = Convert.ToInt32(cdLabel[id].Content.ToString()) - 1;
                cdLabel[id].Content = s;
            }
            else
            {
                cdLabel[id].Opacity = 0;
                t.AutoReset = false;
            }
        }
    }
}
