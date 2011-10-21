using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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

        public MainWindow()
        {
            InitializeComponent();
        }

        public Game GameInProgress
        {
            get { return _gameInProgress; }
        }

        public void InitTexture()
        {
            texture.LoadAllTextures();
            texture.LoadTextureList(GameInProgress.TheCurrentMap.GetCompleteList());
            foreach (var tl in texture.TextureList)
            {
                MainGrid.Children.Add(tl.Value);
            }
            
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            _gameInProgress = new ClassicGame();
            texture = new Texture(GameParameters._.GetThemeData("Basic"));
            InitTexture();

        }
    }
}
