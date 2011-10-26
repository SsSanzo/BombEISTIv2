using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BombEistiv2WPF.Control;
using BombEistiv2WPF.Environment;
using BombEistiv2WPF.View;
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
        private Menu menu;
        private Listener listener;
        private Timer time;
        private Action action;
        private bool triggerOk;
        private Key lastKey;
        private Key lastReleaseKey;


        public MainWindow()
        {
            InitializeComponent();
            time = new Timer(16);
            time.Elapsed += moving;
            lastKey = Key.None;
            lastReleaseKey = Key.None;
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

        public void moving(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            action = reload;
            InvokeThread();
        }

        public void reload()
        {
            _gameInProgress.TheCurrentMap.ListOfPlayer[0].Percentx++;
            _gameInProgress.TheCurrentMap.ListOfPlayer[1].Percenty++;
            _gameInProgress.TheCurrentMap.ListOfPlayer[2].Percenty--;
            _gameInProgress.TheCurrentMap.ListOfPlayer[3].Percentx--;
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

        public void InitTextureSystem()
        {
            menu.LoadAllSystemResources();
        }

        public void DevelopperScreen()
        {
            ResetAllImages();
            menu.LoadTeamBlui();
            MainGrid.Children.Add(menu.MenuDataList["Teamblui"]);
            time = new Timer(28);
            action = FonduDevScreen;
            time.Elapsed += InvokeThread;
            triggerOk = false;
        }

        public void FonduDevScreen()
        {
            if (menu.MenuDataList["Teamblui"].Opacity < 1.0 && !triggerOk)
            {
                menu.MenuDataList["Teamblui"].Opacity += 0.02;
            }
            else if (menu.MenuDataList["Teamblui"].Opacity > 0 && triggerOk)
            {
                time.Interval = 28;
                menu.MenuDataList["Teamblui"].Opacity -= 0.08;
            }
            else if (menu.MenuDataList["Teamblui"].Opacity >= 1.0)
            {
                triggerOk = true;
                time.Interval = 3000;
            }
            else if (menu.MenuDataList["Teamblui"].Opacity <= 0)
            {
                time.Stop();
                time.Close();
            }
            
        }

        public void ResetAllImages()
        {
            if(MainGrid.Children.Count > 0)
            {
                for (int i = MainGrid.Children.Count - 1; i > -1; i--)
                {
                    if (!(MainGrid.Children[i] is MediaElement))
                    {
                        MainGrid.Children.RemoveAt(i);
                        menu.MenuDataList.Clear();
                    }
                }
            }
            
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            
            //menu = new Menu();
            //InitTextureSystem();
            //DevelopperScreen();
            //time.Start();

            listener = Listener._;
            texture = Texture._;
            texture.SetTheme("Basic");
            GameParameters._.PlayerCount = 4;
            _gameInProgress = new ClassicGame();
            listener.GameInProgress = _gameInProgress;
            InitTextureGame();
            listener.ModeMenu = false;
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
    }
}
