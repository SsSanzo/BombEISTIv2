using System;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Threading;
using BombEistiv2WPF.Environment;
using BombEistiv2WPF.View.Menu;
using Screen = BombEistiv2WPF.View.Menu.Screenv2;

namespace BombEistiv2WPF.Control
{
    public class Wizard
    {
        private ScreenType _currentScreenType;
        private Screenv2 _currentScreen;
        private Game _currentGame;
        private MainWindow _theWindow;
        private Key lastKey;
        private Key lastReleaseKey;

        public Wizard(MainWindow mw)
        {
            if (mw != null) _theWindow = mw;
            lastKey = Key.None;
            lastReleaseKey = Key.None;
        }

        public Grid Grid
        {
            get { return _theWindow.MenuGrid; }
        }

        public MainWindow TheWindow
        {
            get { return _theWindow; }
        }

        public Dispatcher WindowDispatcher
        {
            get { return _theWindow.Dispatcher; } 
        }

        public ScreenType Screen
        {
            get { return _currentScreenType; }
            set { _currentScreenType = value; }
        }

        public void Init()
        {
            _currentGame = _theWindow.GameInProgress;
            _currentScreen = null;
            _theWindow.KeyDown += KeyDown;
            _theWindow.KeyUp += KeyUp;
        }

        public void LaunchScreen()
        {
            NextScreen(ScreenType.DevScreen);
        }


        public void KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            lastReleaseKey = e.Key;
            _currentScreen.KeyUp(e.Key);
        }

        public void KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key != lastKey || (e.Key == lastKey && e.Key == lastReleaseKey))
            {
                lastKey = e.Key;
                _currentScreen.KeyDown(e.Key);
            }
            
        }

        public void NextScreen(ScreenType screen)
        {
            switch (screen)
            {
                case ScreenType.DevScreen:
                    var s = new DevScreen();
                    s.setImage("Teamblui");
                    s.Show(this, _currentScreen);
                    _currentScreen = s;
                    break;
                case ScreenType.PressStart:
                    var m = new MenuScreen();
                    m.Show(this, _currentScreen);
                    _currentScreen = m;
                    break;
                case ScreenType.MainMenu:
                    var mm = new MainMenuScreen();
                    mm.Show(this, _currentScreen);
                    _currentScreen = mm;
                    break;
                case ScreenType.Options:
                    var m2 = new OptionMenu();
                    m2.Show(this, _currentScreen);
                    _currentScreen = m2;
                    break;
                case ScreenType.GeneralOptions:
                    var m3 = new GeneralOptionMenu();
                    m3.Show(this, _currentScreen);
                    _currentScreen = m3;
                    break;
                case ScreenType.KeyConfig:
                    var m4 = new KeyConfigMenu();
                    m4.Show(this, _currentScreen);
                    _currentScreen = m4;
                    break;
                case ScreenType.Themes:
                    var m5 = new ThemeMenu();
                    m5.Show(this, _currentScreen);
                    _currentScreen = m5;
                    break;
            }
            //FadeIn();
        }
    }

    public enum ScreenType
    {
        DevScreen,
        PressStart,
        MainMenu,
        Options,
        KeyConfig,
        GeneralOptions,
        Themes,
        GameMode,
        PlayerCound,
        Characters,
        Game,
        Results
    }
}