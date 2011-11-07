using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace BombEistiv2WPF.Control
{
    public class GifImage : Image
    {

        public int FrameIndex
        {
            get { return (int)GetValue(FrameIndexProperty); }
            set { SetValue(FrameIndexProperty, value); }
        }

        public static readonly DependencyProperty FrameIndexProperty =
            DependencyProperty.Register("FrameIndex", typeof(int), typeof(GifImage), new UIPropertyMetadata(0, new PropertyChangedCallback(ChangingFrameIndex)));

        static void ChangingFrameIndex(DependencyObject obj, DependencyPropertyChangedEventArgs ev)
        {
            GifImage ob = obj as GifImage;
            if(ob.gf != null && (int)ev.NewValue != ob.gf.Frames.Count - 1)
            {
                ob.Source = ob.gf.Frames[(int)ev.NewValue];
            }else
            {
                ob.Mw.DeleteExplosion(ob);
            }
            
            ob.InvalidateVisual();
        }
        GifBitmapDecoder gf;
        Int32Animation anim;
        private MainWindow Mw;

        public GifImage(Uri uri, MainWindow mw)
        {
            Mw = mw;
            gf = new GifBitmapDecoder(uri, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            anim = new Int32Animation(0, gf.Frames.Count - 1, new Duration(new TimeSpan(0, 0, 0, gf.Frames.Count / 10, (int)((gf.Frames.Count / 10.0 - gf.Frames.Count / 10) * 1000))));
            anim.RepeatBehavior = RepeatBehavior.Forever;
            Source = gf.Frames[0];
        }

        bool animationIsWorking = false;
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            if (!animationIsWorking)
            {
                BeginAnimation(FrameIndexProperty, anim);
                animationIsWorking = true;
            }
        }
    }
}
