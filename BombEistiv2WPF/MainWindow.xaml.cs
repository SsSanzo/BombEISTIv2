using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using BombEistiv2WPF.Environment;
using BombEistiv2WPF.View;
using Menu = BombEistiv2WPF.View.Menu;

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
        private Timer time;
        private Action action;
        private bool triggerOk;

        public MainWindow()
        {
            InitializeComponent();
            time = new Timer(16);
            time.Elapsed += moving;
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
            _gameInProgress.TheCurrentMap.ListOfPlayer[0].Percenty--;
            _gameInProgress.TheCurrentMap.ListOfPlayer[1].Percenty++;
            _gameInProgress.TheCurrentMap.ListOfPlayer[2].Percentx++;
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
            texture = new Texture("Basic");
            _gameInProgress = new ClassicGame();
            InitTextureGame();
            time.Start();
        }
    }
}
