using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using BombEistiv2WPF.Control;
using BombEistiv2WPF.Environment;
using BombEistiv2WPF.View;
using BombEistiv2WPF.View.Menu;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using Menu = BombEistiv2WPF.View.Menu;
using Timer = System.Timers.Timer;

namespace BombEistiv2WPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Game _gameInProgress;
        private Texture texture;
        private Screen menu;
        private ListenerGame listener;
        private Action action;
        private Key lastKey;
        private Key lastReleaseKey;
        private Label thedisplayedTime;


        public MainWindow()
        {
            InitializeComponent();
            lastKey = Key.None;
            lastReleaseKey = Key.None;
        }

        public Screen Menu
        {
            get { return menu; }
        }

        public Game GameInProgress
        {
            get { return _gameInProgress; }
        }

        public void InvokeThread(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, action);
        }

        public void InvokeThread()
        {
            Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, action);
        }

        public void InvokeThread(Action a)
        {
            Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, a);
        }

        public void InitTextureGame()
        {
            texture.LoadAllTextures();
            var l = texture.LoadBackground();
            foreach (var tl in l)
            {
                MainGrid.Children.Add(tl);
            }
            texture.LoadTextureList(GameInProgress.TheCurrentMap.GetCompleteList(), this);
        }

        public void InitInGameMenu()
        {
            InGameMenu._.InitInGameMenu(_gameInProgress);
            foreach (var lab in InGameMenu._.AllTheLabel())
            {
                MenuGrid.Children.Add(lab);
            }
            foreach (var face in InGameMenu._.AllTheFace(_gameInProgress))
            {
                MenuGrid.Children.Add(face);
            }
        }

        

        public void InitTextureSystem()
        {
            menu.LoadAllSystemResources();
        }

        public void explosion(GifImage g)
        {
            MainGrid.Children.Add(g);
        }

        public void DeleteExplosion(GifImage gf)
        {
            MainGrid.Children.Remove(gf);
        }

        public void RemoveEntity(Entity entity)
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => MainGrid.Children.Remove(entity)));
        }

        public void InsertEntity(Entity entity)
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => InsertEntityDispatched(entity)));
        }

        public void InsertEntityDispatched(Entity entity)
        {
            entity.Source = Texture._.TypetextureList[Texture._.GetTextureKey(entity)];
            entity.Width = 40;
            entity.Height = 40;
            MainGrid.Children.Insert(MainGrid.Children.Count - 1 - GameParameters._.PlayerCount, entity);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //button relou
            MainGrid.Children.RemoveAt(0);
            //var w = new Wizard(this);
            //w.Init();
            //w.LaunchScreen();

            //thewizard = new Wizard(this);
            //thewizard.Init();


            texture = Texture._;
            texture.SetTheme("Basic");

            //a modif ?
            listener = ListenerGame._;
            this.KeyDown += Window_KeyDown;
            this.KeyUp += Window_KeyUp;

            //testing
            GameParameters._.ExplosionDelay = 3;
            GameParameters._.PlayerCount = 4;
            _gameInProgress = new ClassicGame();
            TimerManager._.Game = _gameInProgress;
            listener.GameInProgress = _gameInProgress;
            InitTextureGame();
            InitInGameMenu();
            listener.StartTimers();

            //time.Start();
        }


        private void Window_KeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            if (keyEventArgs.Key != lastKey || (keyEventArgs.Key == lastKey && keyEventArgs.Key == lastReleaseKey))
            {
                lastKey = keyEventArgs.Key;
                listener.EventKey(keyEventArgs.Key);
            }

        }

        private void Window_KeyUp(object sender, KeyEventArgs keyEventArgs)
        {
            lastReleaseKey = keyEventArgs.Key;
            listener.ReleaseKey(keyEventArgs.Key);
        }



        // VIEUX CODE : POUBELLE







        //public void DevelopperScreen()
        //{
        //    ResetAllImages();
        //    //menu.LoadTeamBlui();
        //    MenuGrid.Children.Add(menu.MenuDataList["Teamblui"]);
        //    time = new Timer(28);
        //    action = FonduDevScreen;
        //    time.Elapsed += InvokeThread;
        //    triggerOk = false;
        //    time.Start();
        //}

        //public void PressStartScreen()
        //{
        //    ResetAllImages();
        //    menu.LoadPressStart(this);
        //    foreach (var mdt in menu.MenuDataList)
        //    {
        //        MenuGrid.Children.Add(mdt.Value);
        //    }
        //    time = new Timer(10);
        //    timeSky = new Timer(10);
        //    action = bombIncoming;
        //    time.Elapsed += InvokeThread;
        //    timeSky.Elapsed += ActionDefil;
        //    triggerOk = false;
        //    time.Start();
        //    timeSky.Start();
        //}


        

        //private void ActionDefil(object sender, ElapsedEventArgs e)
        //{
        //    Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(defileSky));
        //}

        //public void defileSky()
        //{
        //    if (menu.MenuDataList["Sky"].Margin.Left == -(menu.MenuDataList["Sky"].Width/2.0))
        //    {
        //        menu.MenuDataList["Sky"].Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
        //    }
        //    menu.MenuDataList["Sky"].Margin = new Thickness(menu.MenuDataList["Sky"].Margin.Left - 1, 0.0, 0.0, 0.0);

        //}

        //public void bombIncoming()
        //{
        //    if (menu.MenuDataList["Bomb"].Margin.Left > -120)
        //    {
        //        menu.MenuDataList["Bomb"].Margin = new Thickness(menu.MenuDataList["Bomb"].Margin.Left - 15, -70, 0.0, 0.0);
        //    }else
        //    {
        //        action = eistiIncoming;
        //    }
        //}

        //public void eistiIncoming()
        //{
        //    if (menu.MenuDataList["Eisti"].Margin.Top <= 120)
        //    {
        //        menu.MenuDataList["Eisti"].Margin = new Thickness(100, menu.MenuDataList["Eisti"].Margin.Top + 10, 0.0, 0.0);
        //    }
        //    else
        //    {
        //        menu.MenuDataList["White"].Opacity = 1;
        //        menu.MenuDataList["2"].Opacity = 1;
        //        time.Interval = 50;
        //        action = flash;
        //    }
        //}

        //public void flash()
        //{
        //    if(menu.MenuDataList["White"].Opacity > 0)
        //    {
        //        menu.MenuDataList["White"].Opacity -= 0.02;
        //    }else
        //    {
        //        menu.MenuDataList["PressStart"].Opacity = 0.20;
        //        time.Interval = 10;
        //        action = PressStartCling;
        //    }
        //}

        //public void PressStartCling()
        //{
        //    if (menu.MenuDataList["PressStart"].Opacity >= 1 && !triggerOk)
        //    {
        //        time.Interval = 1500;
        //        triggerOk = true;
        //    }else if(menu.MenuDataList["PressStart"].Opacity <= 0 && triggerOk)
        //    {
        //        time.Interval = 200;
        //        triggerOk = false;
        //    }else if (!triggerOk)
        //    {
        //        time.Interval = 15;
        //        menu.MenuDataList["PressStart"].Opacity += 0.05;
        //    }else if(triggerOk)
        //    {
        //        time.Interval = 15;
        //        menu.MenuDataList["PressStart"].Opacity -= 0.05;
        //    }
        //}

        //public void FonduDevScreen()
        //{
        //    if (menu.MenuDataList["Teamblui"].Opacity < 1.0 && !triggerOk)
        //    {
        //        menu.MenuDataList["Teamblui"].Opacity += 0.02;
        //    }
        //    else if (menu.MenuDataList["Teamblui"].Opacity > 0 && triggerOk)
        //    {
        //        time.Interval = 28;
        //        menu.MenuDataList["Teamblui"].Opacity -= 0.08;
        //    }
        //    else if (menu.MenuDataList["Teamblui"].Opacity >= 1.0)
        //    {
        //        triggerOk = true;
        //        time.Interval = 3000;
        //    }
        //    else if (menu.MenuDataList["Teamblui"].Opacity <= 0)
        //    {
        //        time.Stop();
        //        time.Close();
        //        PressStartScreen();
                
        //    }

        //}

        //public void ResetAllImages()
        //{
        //    if(MenuGrid.Children.Count > 0)
        //    {
        //        for (int i = MenuGrid.Children.Count - 1; i > -1; i--)
        //        {
        //            if (!(MenuGrid.Children[i] is Grid))
        //            {
        //                MenuGrid.Children.RemoveAt(i);
        //            }
        //        }
        //    }
        //    menu.MenuDataList.Clear();
        //}

        

        

        
    }
}
