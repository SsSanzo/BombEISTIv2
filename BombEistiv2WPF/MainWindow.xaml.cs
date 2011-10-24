using System;
using System.Timers;
using System.Windows;
using BombEistiv2WPF.Environment;
using BombEistiv2WPF.View;

namespace BombEistiv2WPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Game _gameInProgress;
        private Texture texture;
        private Timer time;

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

        public void moving(object sender, ElapsedEventArgs elapsedEventArgs)
        {

            Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(reload));

        }

        public void reload()
        {
            _gameInProgress.TheCurrentMap.ListOfPlayer[0].Percenty--;
            _gameInProgress.TheCurrentMap.ListOfPlayer[1].Percenty++;
            _gameInProgress.TheCurrentMap.ListOfPlayer[2].Percentx++;
            _gameInProgress.TheCurrentMap.ListOfPlayer[3].Percentx--;
        }

        public void InitTexture()
        {
            texture.LoadAllTextures();
            var l = texture.LoadBackground();
            foreach (var tl in l)
            {
                MainGrid.Children.Add(tl);
            }
            texture.LoadTextureList(GameInProgress.TheCurrentMap.GetCompleteList(), this);
            
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            _gameInProgress = new ClassicGame();
            texture = new Texture(GameParameters._.GetThemeData("Basic"));
            InitTexture();
            time.Start();
        }
    }
}
