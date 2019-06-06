using System;

namespace AudioPlayer
{
    class Program
    {
        private static Skin _skin;
        private static Player _player;
        static void Main(string[] args)
        {
            _skin = new ClassicSkin();
            _player = new Player(_skin);

            
            _player.PlayerStarted += (Player, arg) =>{_skin.Render(arg.Message);};
            _player.PlayerStopped += (Player, arg) => { _skin.Render(arg.Message);};
            _player.PlayerLocked += (Player, arg) => { _skin.Render(arg.Message);};
            _player.PlayerUnLocked += (Player, arg) => { _skin.Render(arg.Message);};
            _player.SongStarted += (Player, arg) => { _skin.Render(arg.Message);};
            _player.SongsListChanged += (Player, arg) => { _skin.Render(arg.Message);};
            _player.VolumeChanged += (Player, arg) => { _skin.Render(arg.Message);};
            PlayerStatus();
            _player.Load();
            Here:
            Navigation();
            int j = 0;
            while (j == 0)
            {
                if (_player.Locked==false)
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.P:
                        {
                            try
                            {
                                _skin.Render("Enter the song number");
                                int number = int.Parse(Console.ReadLine());
                                _player.Songs[number - 1].Play = true;
                                _player.Play(_player.Songs[number - 1]);
                                _player.Songs[number - 1].Play = false;
                            }
                            catch (Exception arg)
                            {
                                _skin.Render(arg.Message);
                            }
                            break;
                        }
                        case ConsoleKey.A:
                        {
                            foreach (var song in _player.Songs)
                            {
                                song.Play = true;
                                _player.Play(song);
                                song.Play = false;
                            }
                            break;
                        }
                        case ConsoleKey.S:
                        {
                            _player.Stop();
                            break;
                        }
                        case ConsoleKey.Add:
                        {
                            _player.VolumeUp();
                            break;
                        }
                        case ConsoleKey.Subtract:
                        {
                            _player.VolumeDown();
                            break;
                        }
                        case ConsoleKey.L:
                        {
                            _player.Lock();
                            Console.WriteLine();
                            break;
                        }
                        case ConsoleKey.C:
                        {
                            _player.Clear();
                            break;
                        }
                        case ConsoleKey.F1:
                        {
                            _player.LoadPlayList();
                            CurrentPlaylist();
                            break;
                        }
                        case ConsoleKey.F2:
                        {
                            _player.SaveAsPlaylist();
                            CurrentPlaylist();
                            break;
                        }
                        case ConsoleKey.Escape:
                        {
                            j++;
                            Navigation();
                            break;
                        }
                    }
                }
                else
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.U:
                            {
                                _player.Unlock();
                                goto Here;
                            }
                    }
                }

            }
        }
        private static void PlayerStatus()
        {
            if (_player.Locked)
            {
                _skin.Render("Player locked");
                _skin.Render($"Volume: {_player.Volume}");
                Console.WriteLine();
            }
            else
            {
                _skin.Render("Player unlocked");
                _skin.Render($"Volume: {_player.Volume}");
                Console.WriteLine();
            }
        }

        private static void Navigation()
        {
            _skin.Render("Navigation in player:");
            _skin.Render("P - play song");
            _skin.Render("A - play all songs");
            _skin.Render("S - stop playing");
            _skin.Render("+ - volume up");
            _skin.Render("- - volume down");
            _skin.Render("L - lock player");
            _skin.Render("U - unlock player");
            _skin.Render("C - clear playlist");
            _skin.Render("F1 - load playlist");
            _skin.Render("F2 - save playlist");
            _skin.Render("ESC - close player");
            Console.WriteLine();
        }

        private static void CurrentPlaylist()
        {
            int i = 0;
            _skin.Render("Current playlist:");
            foreach (var song in _player.Songs)
            {
                if (song.Artist.Name == null)
                {
                    song.Artist.Name = "Unknwon artist";
                }

                if (song.Title == null)
                {
                    song.Title = "No title";
                }
                i++;

                if (song.Play)
                {
                    _skin.Render($"{i}. {song.Artist.Name} - {song.Title}" + "" + "- Playing now");
                }

                else if (song.Liked.HasValue)
                {
                    if (song.Liked == true)
                    {
                        _skin.Render($"{i}. {song.Artist.Name} - {song.Title}" + "" + "- Liked");
                    }

                    else
                    {
                        _skin.Render($"{i}. {song.Artist.Name} - {song.Title}" + "" + "- Disliked");
                    }
                }

                else
                {
                    _skin.Render($"{i}. {song.Artist.Name} - {song.Title}" + "" + "- Undefined");
                }
                Console.WriteLine();

            }
        }
    }
}
