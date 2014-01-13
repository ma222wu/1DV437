using System;

namespace LevelEditor
{
#if WINDOWS || XBOX
    static class Program
    {

        static Game1 game;

        [STAThread]
        static void Main(string[] args)
        {
            MainForm baseWindow = new MainForm();
            baseWindow.Disposed += new EventHandler(baseWindow_Disposed);

            using (game = new Game1(baseWindow))
            {
                baseWindow.Game = game;
                baseWindow.TopMost = true;
                baseWindow.Show();

                game.Run();
            }
        }

        static void baseWindow_Disposed(object sender, EventArgs e)
        {
            game.Exit();
        }
    }
#endif
}

