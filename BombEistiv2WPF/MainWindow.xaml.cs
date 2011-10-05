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

namespace BombEistiv2WPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Game _gameInProgress;

        public MainWindow()
        {
            InitializeComponent();
            _gameInProgress = new ClassicGame();
        }

        public Game GameInProgress
        {
            get { return _gameInProgress; }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var b = new Button {Margin = new Thickness(180, 150, 0, 0), Width = 75, Height = 23};
        }
    }
}
