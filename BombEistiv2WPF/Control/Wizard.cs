using System.Windows.Forms;
using System.Windows.Input;
using BombEistiv2WPF.Environment;

namespace BombEistiv2WPF.Control
{
    public class Wizard
    {
        private Screen _currentScreen;
        private Game _currentGame;
        private ListenerGame _currentKeyListener;
        private MainWindow _theWindow;
        private Key lastKey;
        private Key lastReleaseKey;

        public Wizard(MainWindow mw)
        {
            if (mw != null) _theWindow = mw;
            lastKey = Key.None;
            lastReleaseKey = Key.None;
        }

        public Screen Screen
        {
            get { return _currentScreen; }
            set
            {
                //fadeout
                _currentScreen = value;
                //fadein
            }
        }

        public void Init()
        {
            _currentScreen = Screen.DevScreen;
            _currentGame = _theWindow.GameInProgress;
            _currentKeyListener = null;
            _theWindow.KeyDown += WindowKeyDownGame;
            _theWindow.KeyUp += WindowKeyUpGame;
        }

        public void LaunchScreen()
        {

        }

        public void WindowKeyUpMenu(object sender, System.Windows.Input.KeyEventArgs e)
        {
            _currentKeyListener.EventKey(e.Key);
        }

        public void WindowKeyDownMenu(object sender, System.Windows.Input.KeyEventArgs e)
        {
            _currentKeyListener.ReleaseKey(e.Key);
        }



        public void WindowKeyUpGame(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key != lastKey || (e.Key == lastKey && e.Key == lastReleaseKey))
            {
                lastKey = e.Key;
                _currentKeyListener.EventKey(e.Key);
            }
        }

        public void WindowKeyDownGame(object sender, System.Windows.Input.KeyEventArgs e)
        {
            lastReleaseKey = e.Key;
            _currentKeyListener.ReleaseKey(e.Key);
        }

        public void FadeIn()
        {

        }

        public void FadeOut()
        {

        }
    }

    public enum Screen
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