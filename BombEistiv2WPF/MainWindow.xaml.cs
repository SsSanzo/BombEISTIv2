



using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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
        //private Screen menu;
        //private Action action;


        public MainWindow()
        {
            InitializeComponent();
            GameParameters._.instanciate();
            var u = new Uri(GameParameters.Path + @"\\icone.ico");
            var bitmanimg = new BitmapImage();
            bitmanimg.BeginInit();
            bitmanimg.UriSource = u;
            bitmanimg.EndInit();
            Icon = bitmanimg;
            GameParameters._.LoadTheFont();
            MainGrid.Children.RemoveAt(0);
            var w = new Wizard(this);
            w.Init();
            w.LaunchScreen();
        }

        //public Screen Menu
        //{
        //    get { return menu; }
        //}

        public Game GameInProgress
        {
            get { return _gameInProgress; }
            set { _gameInProgress = value; }
        }

        //public void InvokeThread(object sender, ElapsedEventArgs elapsedEventArgs)
        //{
        //    Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, action);
        //}

        //public void InvokeThread()
        //{
        //    Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, action);
        //}

        public void InvokeThread(Action a)
        {
            Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, a);
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
            var w = new Wizard(this);
            w.Init();
            w.LaunchScreen();


        }
    }
}