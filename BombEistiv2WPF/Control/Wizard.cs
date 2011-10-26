using System.Windows.Forms;
using System.Windows.Input;
using BombEistiv2WPF.Environment;

namespace BombEistiv2WPF.Control
{
    public class Wizard
    {
        private Screen _currentScreen;
        private Game _currentGame;
        private Listener _currentKeyListener;
        private MainWindow _theWindow;

        public Wizard(MainWindow mw)
        {
            if (mw != null) _theWindow = mw;
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
            _currentKeyListener = null;
            _theWindow.KeyDown += WindowKeyDown;
            _theWindow.KeyUp += WindowKeyUp;
        }

        public void LaunchScreen()
        {

        }

        public void WindowKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            _currentKeyListener.EventKey(e.Key);
        }

        public void WindowKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
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