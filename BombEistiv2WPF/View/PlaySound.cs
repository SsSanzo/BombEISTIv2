using System;
using System.Collections.Generic;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using BombEistiv2WPF.Environment;

namespace BombEistiv2WPF.View
{
    public class PlaySound
    {
        private static PlaySound _this;
        private Dictionary<string, string> _themeData;
        private Dictionary<string, SoundPlayer> _typesoundList;
        private Dictionary<string, MediaElement> _typemusicList;
        private String _theme;

        private PlaySound()
        {
            _themeData = GameParameters._.GetThemeData("Basic");
            _typesoundList = new Dictionary<string, SoundPlayer>(); 
            _typemusicList = new Dictionary<string, MediaElement>();
        }

        public static PlaySound _
        {
            get { return _this ?? (_this = new PlaySound()); }
        }

        public Dictionary<string, SoundPlayer> TypeSoundList
        {
            get { return _typesoundList; }
            set { _typesoundList = value; }
        }

        public Dictionary<string, MediaElement> TypeMusicList
        {
            get { return _typemusicList; }
            set { _typemusicList = value; }
        }

        public Dictionary<string, string> ThemeData
        {
            get { return _themeData; }
        }

        public void SetTheme(string s)
        {
            _themeData = GameParameters._.GetThemeData(s);
            _theme = s;
        }


        public void LoadAllMusic()
        {
            TypeMusicList.Clear();
            TypeSoundList.Clear();
            foreach (var td in ThemeData)
            {
                if (td.Value.EndsWith(".wav"))
                {
                    var sound = new SoundPlayer(GameParameters.Path + td.Value);
                    TypeSoundList.Add(td.Key, sound);
                }
                else if (td.Value.EndsWith(".mp3"))
                {
                    var u = new Uri(GameParameters.Path + td.Value);
                    var sound = new MediaElement();
                    sound.Source = u;
                    sound.LoadedBehavior = MediaState.Manual;
                    sound.Volume = 0;
                    sound.Play();
                    sound.Stop();
                    sound.Volume = 75;
                    TypeMusicList.Add(td.Key, sound);
                }
            }
        }

        public void ClearEverything(MainWindow w)
        {
            foreach (var mediaElement in TypeMusicList)
            {
                mediaElement.Value.Stop();
                w.MenuGrid.Children.Remove(mediaElement.Value);
            }
            foreach (var mediaElement in TypeSoundList)
            {
                mediaElement.Value.Stop();
            }
            TypeMusicList.Clear();
            TypeSoundList.Clear();
        }



        public void LireBoucle(string value)
        {
            if (value == "MenuAll")
            {
                TypeMusicList["Ouverturemenu"].Play();
                TypeMusicList["Ouverturemenu"].MediaEnded += loopmenu;
            }else
            {
                TypeMusicList[value].Play();
                TypeMusicList[value].MediaEnded += loop;
            }
            
        }

        public void Stop(string value)
        {
            if(value == "MenuAll")
            {
                TypeMusicList["Ouverturemenu"].Stop();
                TypeMusicList["Ouverturemenu"].MediaEnded -= loopmenu;
                TypeMusicList["Menu"].Stop();
                TypeMusicList["Menu"].MediaEnded -= loop;
            }else
            {
                TypeMusicList[value].MediaEnded -= loop;
                TypeMusicList[value].Stop();
            }
            
        }

        public void loop(object sender, RoutedEventArgs routedEventArgs)
        {
            var u = (MediaElement) sender;
            u.Position = TimeSpan.Zero;
            u.Play();
        }


        public void loopmenu(object sender, RoutedEventArgs routedEventArgs)
        {
            var u = (MediaElement)sender;
            u.Stop();
            PlaySound._.TypeMusicList["Menu"].Volume = 100;
            PlaySound._.TypeMusicList["Menu"].Play();
            PlaySound._.TypeMusicList["Menu"].MediaEnded += loop;
        }
    }
}
