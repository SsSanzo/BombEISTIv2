using System;
using System.Collections.Generic;
using System.IO;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using BombEistiv2WPF.Control;
using BombEistiv2WPF.Environment;

namespace BombEistiv2WPF.View
{
    public class Texture
    {
        private Dictionary<string, string> _themeData;
        private Dictionary<string, BitmapImage> _typetextureList;
        private MainWindow mw;
        private int offsetglobal = 20;
        private int timeoffset = 20;

        private static Texture _this;

        private Texture()
        {
            _themeData = GameParameters._.GetThemeData("Basic");
            _typetextureList = new Dictionary<string, BitmapImage>();
            
        }

        public MainWindow Mw
        {
            get { return mw; }
            set { mw = value; }
        }

        public static Texture _
        {
            get { return _this ?? (_this = new Texture()); }
        }

        public Dictionary<string, string> ThemeData
        {
            get { return _themeData; }
        }

        public void SetTheme(string s)
        {
            _themeData = GameParameters._.GetThemeData(s);
        }

        public Dictionary<string, BitmapImage> TypetextureList
        {
            get { return _typetextureList; }
        }

        public void LoadTextureList(List<Entity> l , MainWindow w)
        {
                Mw = w;
                foreach (var entity in l)
                {
                    entity.Source = TypetextureList[GetTextureKey(entity)];
                    entity.Width = 40;
                    entity.Height = 40;
                    Mw.MainGrid.Children.Add(entity);
                }
        }

        public void InsertTextureEntity(Entity entity)
        {
            Mw.InsertEntity(entity);
        }

        public void DeleteTextureEntity(Entity entity)
        {
            Mw.RemoveEntity(entity);
        }

        public GifImage newgif()
        {
            var g = new GifImage(TypetextureList["Explosion"].UriSource, Mw);

            g.HorizontalAlignment = HorizontalAlignment.Left;
            g.VerticalAlignment = VerticalAlignment.Top;
            g.Width = 40;
            g.Height = 40;
            return g;
        }

        public void Explosion(Bomb b2, Game game, Entity Left, Entity Up, Entity Right, Entity Down, Entity None)
        {
            var b = new Bomb(b2.X, b2.Y, b2.Power, b2.Owner, false);
            var g = newgif();
            g.Margin = new Thickness(b.X * 40, b.Y * 40, 0.0, 0.0);
            Mw.explosion(g);
            new EntityOfDeath(b.X, b.Y, game, true);
            if (Left == null)
            {
                if(b.X - b.Power < 0)
                {
                    Left = new HardBlock( -1  , 0);
                }else
                {
                    Left = new SoftBlock(b.X - b.Power, 0);
                } 
            }
            if (Right == null)
            {
                if(b.X + b.Power >= Game.Length)
                {
                    Right = new HardBlock(Game.Length, 0);
                }else
                {
                    Right = new SoftBlock(b.X + b.Power, 0);
                }
                
            }
            if (Down == null)
            {
                if(b.Y + b.Power >= Game.Length)
                {
                    Down = new HardBlock(0, Game.Length);
                }else
                {
                    Down = new SoftBlock(0, b.Y + b.Power);
                }
                
            }
            if (Up == null)
            {
                if(b.Y - b.Power < 0)
                {
                    Up = new HardBlock(0, -1);
                }else
                {
                    Up = new SoftBlock(0, b.Y - b.Power);
                }
                
            }
            if (!(Left is HardBlock && b.X == Left.X + 1))
            {
                TimerManager._.AddNewTimer(false, timeoffset, true, null,
                    delegate
                                                {
                                                    Mw.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => 
                                                        Explosion(b, game, Left, Direction.Left, offsetglobal
                                                        )));
                                                });
                 
            }
            if (!(Right is HardBlock && b.X == Right.X - 1))
            {
                TimerManager._.AddNewTimer(false, timeoffset, true, null,
                    delegate
                    {
                        Mw.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                            Explosion(b, game, Right, Direction.Right, offsetglobal
                            )));
                    });
                
            }
            if (!(Up is HardBlock && b.Y == Up.Y + 1))
            {
                TimerManager._.AddNewTimer(false, timeoffset, true, null,
                     delegate
                     {
                         Mw.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                             Explosion(b, game, Up, Direction.Up, offsetglobal
                             )));
                     });
                
            }
            if (!(Down is HardBlock && b.Y == Down.Y - 1))
            {
                TimerManager._.AddNewTimer(false, timeoffset, true, null,
                     delegate
                     {
                         Mw.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                             Explosion(b, game, Down, Direction.Down, offsetglobal
                             )));
                     });
            }
        }

        public void Explosion(Bomb b, Game game, Entity e, Direction d, int offset)
        {
            var g = newgif();
            
            switch(d)
            {
                case Direction.Left:
                    g.Margin = new Thickness(b.X * 40 - offset, b.Y * 40, 0.0, 0.0);
                    if (offset % 40 == 0)
                    {
                        new EntityOfDeath(b.X - offset/40, b.Y, game);
                    }
                    Mw.explosion(g);
                    if(e is HardBlock)
                    {
                        if (!(((double)b.X - ((double)offset) / 40.0) <= (double)(e.X + 1)))
                        {
                            TimerManager._.AddNewTimer(false, timeoffset, true, null,
                             delegate
                             {
                                 Mw.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                                     Explosion(b, game, e, d, offset + offsetglobal
                                     )));
                             });
                        }
                    }else
                    {
                        if (!(((double)b.X - ((double)offset) / 40.0) <= (double)(e.X)))
                        {
                            TimerManager._.AddNewTimer(false, timeoffset, true, null,
                             delegate
                             {
                                 Mw.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                                     Explosion(b, game, e, d, offset + offsetglobal
                                     )));
                             });
                            
                        }
                    }
                    
                    break;
                case Direction.Right:
                    g.Margin = new Thickness(b.X * 40 + offset, b.Y * 40, 0.0, 0.0);
                    if (offset % 40 == 0)
                    {
                        new EntityOfDeath(b.X + offset / 40, b.Y, game);
                    }
                    Mw.explosion(g);
                    if(e is HardBlock)
                    {
                        if (!(((double)b.X + ((double)offset) / 40.0) >= (double)(e.X - 1)))
                        {
                            TimerManager._.AddNewTimer(false, timeoffset, true, null,
                             delegate
                             {
                                 Mw.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                                     Explosion(b, game, e, d, offset + offsetglobal
                                     )));
                             });
                        }
                    }else
                    {
                        if (!(((double)b.X + ((double)offset) / 40.0) >= (double)e.X))
                        {
                            TimerManager._.AddNewTimer(false, timeoffset, true, null,
                             delegate
                             {
                                 Mw.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                                     Explosion(b, game, e, d, offset + offsetglobal
                                     )));
                             });
                        }
                    }
                    break;
                case Direction.Up:
                    g.Margin = new Thickness(b.X * 40, b.Y * 40 - offset, 0.0, 0.0);
                    if (offset % 40 == 0)
                    {
                        new EntityOfDeath(b.X, b.Y - offset / 40, game);
                    }
                    Mw.explosion(g);
                    if(e is HardBlock)
                    {
                        if (!(((double)b.Y - ((double)offset) / 40.0) <= (double)(e.Y + 1)))
                        {
                            TimerManager._.AddNewTimer(false, timeoffset, true, null,
                             delegate
                             {
                                 Mw.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                                     Explosion(b, game, e, d, offset + offsetglobal
                                     )));
                             });
                        }
                    }else
                    {
                        if (!(((double)b.Y - ((double)offset) / 40.0) <= (double)(e.Y)))
                        {
                            TimerManager._.AddNewTimer(false, timeoffset, true, null,
                             delegate
                             {
                                 Mw.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                                     Explosion(b, game, e, d, offset + offsetglobal
                                     )));
                             });
                        }
                    }
                    break;
                case Direction.Down:
                    g.Margin = new Thickness(b.X * 40, b.Y * 40 + offset, 0.0, 0.0);
                    if (offset % 40 == 0)
                    {
                        new EntityOfDeath(b.X, b.Y + offset / 40, game);
                    }
                    Mw.explosion(g);
                    if (e is HardBlock)
                    {
                        if (!(((double)b.Y + ((double)offset) / 40.0) >= (double)(e.Y - 1)))
                        {
                            TimerManager._.AddNewTimer(false, timeoffset, true, null,
                             delegate
                             {
                                 Mw.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                                     Explosion(b, game, e, d, offset + offsetglobal
                                     )));
                             });
                        }
                    }else
                    {
                        if (!(((double)b.Y + ((double)offset) / 40.0) >= (double)e.Y))
                        {
                            TimerManager._.AddNewTimer(false, timeoffset, true, null,
                             delegate
                             {
                                 Mw.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                                     Explosion(b, game, e, d, offset + offsetglobal
                                     )));
                             });
                        }
                    }
                    break;
            }
            
            
            
        }

        public List<Image> LoadBackground()
        {
            var l = new List<Image>();
            for(var i=0;i<Game.Length;i++)
            {
                for(var j=0;j<Game.Length;j++)
                {
                    var g = new Image();
                    g.Source = TypetextureList["Background"];
                    g.HorizontalAlignment = HorizontalAlignment.Left;
                    g.VerticalAlignment = VerticalAlignment.Top;
                    g.Width = 40;
                    g.Height = 40;
                    g.Margin = new Thickness((i* 40), (j * 40), 0.0, 0.0);
                    l.Add(g);
                }
            }
            return l;
        }

        public string GetTextureKey(Entity e)
        {
            if(e is SoftBlock)
            {
                return "SoftBloc";
            }
            if(e is HardBlock)
            {
                return "HardBloc";
            }
            if(e is Upgrade)
            {
                var u = (Upgrade) e;
                return "Upgrade." + u.Type.ToString();
            }
            if(e is Bomb)
            {
                return "Bomb";
            }
            if(e is Player)
            {
                var p = (Player) e;
                return "Player_" + p.Skinid + "_" + p.Sens.ToString().ToLower();
            }
            return "Background";
        }


        public void LoadAllTextures()
        {
            foreach (var td in ThemeData)
            {
                if (td.Value.EndsWith(".png") || td.Value.EndsWith(".gif"))
                {
                    var u = new Uri(GameParameters.Path + td.Value);
                    var bitmanimg = new BitmapImage();
                    bitmanimg.BeginInit();
                    bitmanimg.UriSource = u;
                    bitmanimg.EndInit();
                    TypetextureList.Add(td.Key, bitmanimg);
                }
                
            }
        }
        
    }
}
