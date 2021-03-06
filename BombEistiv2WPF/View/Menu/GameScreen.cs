﻿using System;
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
    public class GameScreen : Screenv2
    {
        private Wizard _wizard;
        private Dictionary<string, Image> _menuDataList;
        //private Dictionary<string, Label> _menuLabelList;
        private Key lastKey;
        private Key lastReleaseKey;
        private int start;
        private bool movelocked;
        private int animbomb;
        private int animcurse;
        private int sens;
        private bool openedanim;
        private bool openedanimcurse;

        public Dictionary<string, Image> MenuDataList
        {
            get { return _menuDataList; }
        }

        //public Dictionary<string, Label> MenuLabelList
        //{
        //    get { return _menuLabelList; }
        //}

        public override void Show(Control.Wizard w, Screenv2 oldscreen)
        {
            if(GameParameters._.Type == GameType.Classic)
            {
                PlaySound._.Stop("Result");
                animbomb = 0;
                animcurse = 0;
                sens = 1;
                openedanim = true;
                openedanimcurse = true;
                lastKey = Key.None;
                lastReleaseKey = Key.None;
                _wizard = w;
                movelocked = true;
                TimerManager._.Reset();
                for (var i = w.Grid.Children.Count - 1; i > -1; i--)
                {
                    if (!(w.Grid.Children[i] is Grid || w.Grid.Children[i] is MediaElement))
                    {
                        w.Grid.Children.RemoveAt(i);
                    }
                }
                if (Texture._.IsRandom)
                {
                    var listDir = Directory.EnumerateDirectories(GameParameters.Path + @"\" + GameParameters._.GetThemeFolder());
                    var listScreen = new List<String>();
                    foreach (var dir in listDir)
                    {
                        listScreen.Add(dir.Split('\\')[dir.Split('\\').Length - 1]);
                    }
                    var rand = new Random();
                    var theint = rand.Next(listScreen.Count - 1);
                    Texture._.SetTheme(listScreen.ElementAt(theint));
                    PlaySound._.SetTheme(listScreen.ElementAt(theint));
                    PlaySound._.ClearEverything(_wizard.TheWindow);
                    PlaySound._.LoadAllMusic();
                    foreach (var mus in PlaySound._.TypeMusicList)
                    {
                        _wizard.Grid.Children.Add(mus.Value);
                    }
                }

                if (_menuDataList == null)
                {
                    _menuDataList = new Dictionary<string, Image>();
                    _wizard.WindowDispatcher.Invoke(DispatcherPriority.Normal,
                                                    new Action(LoadImage));
                }
                foreach (var img in MenuDataList)
                {
                    _wizard.TheWindow.MainGrid.Children.Add(img.Value);
                    Canvas.SetZIndex(img.Value, 2);
                }
                Canvas.SetZIndex(MenuDataList["Hurry"], 3);
                Canvas.SetZIndex(MenuDataList["Over"], 4);
                _wizard.TheWindow.GameInProgress = new ClassicGame(this);
                TimerManager._.Game = _wizard.TheWindow.GameInProgress;
                ListenerGame._.GameInProgress = _wizard.TheWindow.GameInProgress;
                InitTextureGame();
                InitInGameMenu();
                ListenerGame._.EmptyTheCache();
                Score._.ResetSurvived();
                start = 4;
                PlaySound._.LireBoucle("Game");
                TimerManager._.AddNewTimer(true, 15, true, null, FadeIn);
            }else if(GameParameters._.Type == GameType.Crazy)
            {
                PlaySound._.Stop("Result");
                animbomb = 0;
                animcurse = 0;
                sens = 1;
                openedanim = true;
                openedanimcurse = true;
                lastKey = Key.None;
                lastReleaseKey = Key.None;
                _wizard = w;
                movelocked = true;
                TimerManager._.Reset();
                for (var i = w.Grid.Children.Count - 1; i > -1; i--)
                {
                    if (!(w.Grid.Children[i] is Grid || w.Grid.Children[i] is MediaElement))
                    {
                        w.Grid.Children.RemoveAt(i);
                    }
                }
                if (Texture._.IsRandom)
                {
                    var listDir = Directory.EnumerateDirectories(GameParameters.Path + @"\" + GameParameters._.GetThemeFolder());
                    var listScreen = new List<String>();
                    foreach (var dir in listDir)
                    {
                        listScreen.Add(dir.Split('\\')[dir.Split('\\').Length - 1]);
                    }
                    var rand = new Random();
                    var theint = rand.Next(listScreen.Count - 1);
                    Texture._.SetTheme(listScreen.ElementAt(theint));
                    PlaySound._.SetTheme(listScreen.ElementAt(theint));
                    PlaySound._.ClearEverything(_wizard.TheWindow);
                    PlaySound._.LoadAllMusic();
                    foreach (var mus in PlaySound._.TypeMusicList)
                    {
                        _wizard.Grid.Children.Add(mus.Value);
                    }
                }

                if (_menuDataList == null)
                {
                    _menuDataList = new Dictionary<string, Image>();
                    _wizard.WindowDispatcher.Invoke(DispatcherPriority.Normal,
                                                    new Action(LoadImage));
                }
                foreach (var img in MenuDataList)
                {
                    _wizard.TheWindow.MainGrid.Children.Add(img.Value);
                    Canvas.SetZIndex(img.Value, 2);
                }
                Canvas.SetZIndex(MenuDataList["Hurry"], 3);
                Canvas.SetZIndex(MenuDataList["Over"], 4);
                _wizard.TheWindow.GameInProgress = new CrazyGame(this);
                TimerManager._.Game = _wizard.TheWindow.GameInProgress;
                ListenerGame._.GameInProgress = _wizard.TheWindow.GameInProgress;
                InitTextureGame();
                InitInGameMenuCrazy();
                ListenerGame._.EmptyTheCache();
                Score._.ResetSurvived();
                start = 4;
                PlaySound._.LireBoucle("Game");
                TimerManager._.AddNewTimer(true, 15, true, null, FadeIn);
            }
            
        }

        public void InitTextureGame()
        {
            Texture._.LoadAllTextures();
            var l = Texture._.LoadBackground();
            foreach (var tl in l)
            {
                _wizard.TheWindow.MainGrid.Children.Add(tl);
            }
            Texture._.LoadTextureList(_wizard.TheWindow.GameInProgress.TheCurrentMap.GetCompleteList(), _wizard.TheWindow);
        }

        public void InitInGameMenu()
        {
            InGameMenu._.InitInGameMenu(_wizard.TheWindow.GameInProgress);
            foreach (var lab in InGameMenu._.AllTheLabel())
            {
                _wizard.TheWindow.MenuGrid.Children.Add(lab);
            }
            foreach (var face in InGameMenu._.AllTheFace(_wizard.TheWindow.GameInProgress))
            {
                _wizard.TheWindow.MenuGrid.Children.Add(face);
            }
        }

        public void InitInGameMenuCrazy()
        {
            InGameMenu._.InitInGameMenu(_wizard.TheWindow.GameInProgress);
            foreach (var lab in InGameMenu._.AllTheLabel())
            {
                _wizard.TheWindow.MenuGrid.Children.Add(lab);
            }
            foreach (var face in InGameMenu._.AllTheFace(_wizard.TheWindow.GameInProgress))
            {
                _wizard.TheWindow.MenuGrid.Children.Add(face);
            }
            foreach (var bomb in InGameMenu._.AllTheBomb(_wizard.TheWindow.GameInProgress))
            {
                _wizard.TheWindow.MenuGrid.Children.Add(bomb);
            }
            foreach (var stack in InGameMenu._.AllTheStack(_wizard.TheWindow.GameInProgress))
            {
                _wizard.TheWindow.MenuGrid.Children.Add(stack);
            }
            foreach (var cd in InGameMenu._.AllTheLabelCD())
            {
                _wizard.TheWindow.MenuGrid.Children.Add(cd);
            }
        }

        public override void KeyUp(Key k)
        {
            if(!movelocked)
            {
                lastReleaseKey = k;
                ListenerGame._.ReleaseKey(k);
            }
            
        }

        public override void KeyDown(Key k)
        {
            if(!movelocked)
            {
                if (k != lastKey || (k == lastKey && k == lastReleaseKey))
                {
                    lastKey = k;
                    ListenerGame._.EventKey(k);
                }
            }
            
        }

        public override void Hide()
        {


        }

        public void LoadImage()
        {
            var g = new Image
            {
                Source = GameParameters._.MenutextureList["Start3"],
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0.0, 0.0, 0.0, 0.0),
                Opacity = 0,
                Width = 300,
                Height = 300
            };
            MenuDataList.Add("Start3", g);
            var g2 = new Image
            {
                Source = GameParameters._.MenutextureList["Start2"],
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0.0, 0.0, 0.0, 0.0),
                Opacity = 0,
                Width = 300,
                Height = 300
            };
            MenuDataList.Add("Start2", g2);
            var g3 = new Image
            {
                Source = GameParameters._.MenutextureList["Start1"],
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0.0, 0.0, 0.0, 0.0),
                Opacity = 0,
                Width = 300,
                Height = 300
            };
            MenuDataList.Add("Start1", g3);
            var g4 = new Image
            {
                Source = GameParameters._.MenutextureList["StartGo"],
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0.0, 0.0, 0.0, 0.0),
                Opacity = 0,
                Width = 350,
                Height = 350
            };
            MenuDataList.Add("StartGo", g4);

            var g5 = new Image
            {
                Source = GameParameters._.MenutextureList["Hurry"],
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(-400, 0.0, 0.0, 0.0),
                Opacity = 1,
                Width = 400,
                Height = 200
            };
            MenuDataList.Add("Hurry", g5);

            var g6 = new Image
            {
                Source = GameParameters._.MenutextureList["Over"],
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0.0, 0.0, 0.0, 0.0),
                Opacity = 0,
                Width = 500,
                Height = 500
            };
            MenuDataList.Add("Over", g6);

        }


        private void FadeIn(object sender, ElapsedEventArgs e)
        {
            _wizard.WindowDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => FadeInOption((Timer)sender)));

        }

        private void BombAnim(object sender, ElapsedEventArgs e)
        {
            if(openedanim)
            {
                openedanim = false;
                try
                {
                    _wizard.WindowDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                                                    new Action(AnimBomb));
                }catch
                {
                    
                }
            }
            

        }

        private void CurseAnim(object sender, ElapsedEventArgs e)
        {
            if (openedanimcurse)
            {
                openedanimcurse = false;
                try
                {
                    _wizard.WindowDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                                                    new Action(AnimCurse));
                }
                catch
                {

                }
            }


        }

        public void Hurry(object sender, ElapsedEventArgs e)
        {
            _wizard.WindowDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => HurryUp((Timer)sender)));
        }

        public void HurryUp(Timer t)
        {
            if(MenuDataList["Hurry"].Margin.Left <= 800)
            {
                MenuDataList["Hurry"].Margin = new Thickness(MenuDataList["Hurry"].Margin.Left + 5, 0.0, 0.0, 0.0);
            }
        }

        public void AnimBomb()
        {
            try
            {

                var copy = _wizard.TheWindow.GameInProgress.TheCurrentMap.ListOfBomb;

                for (var i = 0; i < copy.Count; i++)
                {
                    var b = copy.ElementAt(i);
                    if (b != null && b.Type != BombType.Mine && b.Type != BombType.Ninja)
                    {
                        if (!(b.LayoutTransform is ScaleTransform))
                        {
                            var lol = new ScaleTransform { CenterX = 20, CenterY = 20, ScaleX = 1.0, ScaleY = 1.0 };
                            b.LayoutTransform = lol;
                        }
                        var rt = (ScaleTransform)b.LayoutTransform;
                        rt.ScaleX = rt.ScaleX + sens * (0.002);
                        rt.ScaleY = rt.ScaleY + sens * (0.002);
                        b.Margin = new Thickness(b.Margin.Left - sens * 0.05, b.Margin.Top - sens * 0.05, 0.0, 0.0);
                    }
                }
            
                animbomb += sens;
                if(animbomb > 20)
                {
                    sens = -1;
                    animbomb = 20;
                }
                else if (animbomb < 0)
                {
                    sens = 1;
                    animbomb = 0;
                }
                openedanim = true;
                }
            catch { }
        }

        public void AnimCurse()
        {
            try
            {
                var copy = _wizard.TheWindow.GameInProgress.TheCurrentMap.ListOfPlayer;

                for (var i = 0; i < copy.Count; i++)
                {
                    var b = copy.ElementAt(i);
                    if (b != null && b.Cursed)
                    {
                        b.Opacity = b.Opacity - sens * 0.037;
                    }
                }

                animcurse += sens;
                if (animcurse > 20)
                {
                    sens = -1;
                    animcurse = 20;
                }
                else if (animcurse < 0)
                {
                    sens = 1;
                    animcurse = 0;
                }
                openedanimcurse = true;
            }
            catch { }
        }


        public void FadeInOption(Timer t)
        {
            if(start == 4)
            {
                MenuDataList["Start3"].Opacity = 1;
                start = 3;
            }else if(start == 3 && MenuDataList["Start3"].Opacity > 0.4)
            {
                MenuDataList["Start3"].Opacity -= 0.009;
            }else if(start == 3 && MenuDataList["Start3"].Opacity <= 0.4)
            {
                MenuDataList["Start3"].Opacity = 0;
                MenuDataList["Start2"].Opacity = 1;
                start = 2;
            }
            else if (start == 2 && MenuDataList["Start2"].Opacity > 0.4)
            {
                MenuDataList["Start2"].Opacity -= 0.009;
            }
            else if (start == 2 && MenuDataList["Start2"].Opacity <= 0.4)
            {
                MenuDataList["Start2"].Opacity = 0;
                MenuDataList["Start1"].Opacity = 1;
                start = 1;
            }
            else if (start == 1 && MenuDataList["Start1"].Opacity > 0.4)
            {
                MenuDataList["Start1"].Opacity -= 0.009;
            }
            else if (start == 1 && MenuDataList["Start1"].Opacity <= 0.4)
            {
                MenuDataList["Start1"].Opacity = 0;
                MenuDataList["StartGo"].Opacity = 1;
                start = 0;
            }else if(start == 0)
            {
                ListenerGame._.StartTimers(GameParameters._.Type);
                if(GameParameters._.Type == GameType.Classic)
                {
                    var g = (ClassicGame)_wizard.TheWindow.GameInProgress;
                    g.Start();
                }
                else if (GameParameters._.Type == GameType.Crazy)
                {
                    var g = (CrazyGame)_wizard.TheWindow.GameInProgress;
                    g.Start();
                    TimerManager._.AddNewTimer(true, 15, true, null, CurseAnim);
                }
                
                TimerManager._.AddNewTimer(true, 15, true, null, BombAnim);
                movelocked = false;
                start--;
            }else if(start < 0 && start > -100)
            {
                start--;
            }
            else if (start <= -100)
            {
                MenuDataList["StartGo"].Opacity = 0;
                t.AutoReset = false;
            }
        }

        public void Ending()
        {
            TimerManager._.Reset();
            _wizard.NextScreen(ScreenType.Results);
        }
    }
}
