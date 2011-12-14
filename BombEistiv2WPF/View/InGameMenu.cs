using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using BombEistiv2WPF.Environment;

namespace BombEistiv2WPF.View
{
    public class InGameMenu
    {
        public Label thedisplayedTime;
        public Dictionary<int, Label> lifeLabel;
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
            var nbplayer = g.TheCurrentMap.ListOfPlayer.Count;
            thedisplayedTime = null;
            lifeLabel.Clear();
            thedisplayedTime = InitLabel();
            thedisplayedTime.Margin = new Thickness(0,4,0,0);
            thedisplayedTime.Content = GameParameters._.GameTime;
            var lifejoueur1 = InitLabel();
            lifejoueur1.Margin = new Thickness(-400, 4, 0, 0);
            lifejoueur1.Content = g.TheCurrentMap.ListOfPlayer.ElementAt(0).Lives.ToString();
            lifeLabel.Add(g.TheCurrentMap.ListOfPlayer.ElementAt(0).Id, lifejoueur1);
            var lifejoueur2 = InitLabel();
            lifejoueur2.Margin = new Thickness(-200, 4, 0, 0);
            lifejoueur2.Content = g.TheCurrentMap.ListOfPlayer.ElementAt(1).Lives.ToString();
            lifeLabel.Add(g.TheCurrentMap.ListOfPlayer.ElementAt(1).Id, lifejoueur2);
            if(nbplayer > 2)
            {
                var lifejoueur3 = InitLabel();
                lifejoueur3.Margin = new Thickness(300, 4, 0, 0);
                lifejoueur3.Content = g.TheCurrentMap.ListOfPlayer.ElementAt(2).Lives.ToString();
                lifeLabel.Add(g.TheCurrentMap.ListOfPlayer.ElementAt(2).Id, lifejoueur3);

            }
            if(nbplayer > 3)
            {
                var lifejoueur4 = InitLabel();
                lifejoueur4.Margin = new Thickness(500, 4, 0, 0);
                lifejoueur4.Content = g.TheCurrentMap.ListOfPlayer.ElementAt(3).Lives.ToString();
                lifeLabel.Add(g.TheCurrentMap.ListOfPlayer.ElementAt(3).Id, lifejoueur4);

            }
        }

        public List<Image> AllTheFace(Game g)
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
        } 

        public Label InitLabel()
        {
            var l = new Label
                        {
                            VerticalAlignment = VerticalAlignment.Top,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            FontSize = 25,
                            Foreground = new SolidColorBrush(Colors.White)
                        };
            return l;
        }

        public List<Label> AllTheLabel()
        {
            var l = new List<Label> {thedisplayedTime};
            l.AddRange(lifeLabel.Select(label => label.Value));
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
    }
}
