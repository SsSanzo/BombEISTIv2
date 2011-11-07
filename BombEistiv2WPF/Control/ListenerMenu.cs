using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace BombEistiv2WPF.Control
{
    public class ListenerMenu
    {
        private Key keypressed;
        private static ListenerMenu _this;

        private ListenerMenu()
        {
            keypressed = Key.None;
        }

        public static ListenerMenu _
        {
            get { return _this ?? (_this = new ListenerMenu()); }
        }


        public void EventPressStart(Key k)
        {
            
        }

        public void ReleaseKey(Key k)
        {

        }
    }
}
