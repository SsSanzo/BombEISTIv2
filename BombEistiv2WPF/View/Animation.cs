using System.Windows;

namespace BombEistiv2WPF.View
{
    public class Animation
    {
//        private MainWindow mw;
//        private Animation _this;

//        private Animation()
//        {
            
//        }

//        private MainWindow theWindow
//        {
//            set { mw = value;  }
//            get { return mw;  }
//        }
//        public void defileSky()
//        {
//            if (mw.Menu.MenuDataList["Sky"].Margin.Left == -(mw.Menu.MenuDataList["Sky"].Width / 2.0))
//            {
//                mw.Menu.MenuDataList["Sky"].Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
//            }
//            mw.Menu.MenuDataList["Sky"].Margin = new Thickness(mw.Menu.MenuDataList["Sky"].Margin.Left - 1, 0.0, 0.0, 0.0);

//        }

//        public void bombIncoming()
//        {
//            if (mw.Menu.MenuDataList["Bomb"].Margin.Left > -120)
//            {
//                mw.Menu.MenuDataList["Bomb"].Margin = new Thickness(mw.Menu.MenuDataList["Bomb"].Margin.Left - 15, -70, 0.0, 0.0);
//            }
//            else
//            {
//                action = eistiIncoming;
//            }
//        }

//        public void eistiIncoming()
//        {
//            if (mw.Menu.MenuDataList["Eisti"].Margin.Top <= 120)
//            {
//                mw.Menu.MenuDataList["Eisti"].Margin = new Thickness(100, mw.Menu.MenuDataList["Eisti"].Margin.Top + 10, 0.0, 0.0);
//            }
//            else
//            {
//                mw.Menu.MenuDataList["White"].Opacity = 1;
//                mw.Menu.MenuDataList["2"].Opacity = 1;
//                time.Interval = 50;
//                action = flash;
//            }
//        }

//        public void flash()
//        {
//            if (mw.Menu.MenuDataList["White"].Opacity > 0)
//            {
//                mw.Menu.MenuDataList["White"].Opacity -= 0.02;
//            }
//            else
//            {
//                mw.Menu.MenuDataList["PressStart"].Opacity = 0.20;
//                time.Interval = 10;
//                action = PressStartCling;
//            }
//        }

//        public void PressStartCling()
//        {
//            if (mw.Menu.MenuDataList["PressStart"].Opacity >= 1 && !triggerOk)
//            {
//                time.Interval = 1500;
//                triggerOk = true;
//            }
//            else if (mw.Menu.MenuDataList["PressStart"].Opacity <= 0 && triggerOk)
//            {
//                time.Interval = 200;
//                triggerOk = false;
//            }
//            else if (!triggerOk)
//            {
//                time.Interval = 15;
//                mw.Menu.MenuDataList["PressStart"].Opacity += 0.05;
//            }
//            else if (triggerOk)
//            {
//                time.Interval = 15;
//                mw.Menu.MenuDataList["PressStart"].Opacity -= 0.05;
//            }
//        }

//        public void FonduDevScreen()
//        {
//            if (mw.Menu.MenuDataList["Teamblui"].Opacity < 1.0 && !triggerOk)
//            {
//                mw.Menu.MenuDataList["Teamblui"].Opacity += 0.02;
//            }
//            else if (mw.Menu.MenuDataList["Teamblui"].Opacity > 0 && triggerOk)
//            {
//                time.Interval = 28;
//                mw.Menu.MenuDataList["Teamblui"].Opacity -= 0.08;
//            }
//            else if (mw.Menu.MenuDataList["Teamblui"].Opacity >= 1.0)
//            {
//                triggerOk = true;
//                time.Interval = 3000;
//            }
//            else if (mw.Menu.MenuDataList["Teamblui"].Opacity <= 0)
//            {
//                time.Stop();
//                time.Close();
//                PressStartScreen();

//            }

//        }
    }
}
