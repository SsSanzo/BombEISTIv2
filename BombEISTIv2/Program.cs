using System;
using System.Windows.Forms;
using BombEISTIv2.View;

namespace BombEISTIv2.Environment
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Frame());
        }
    }
}
