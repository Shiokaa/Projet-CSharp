using System;
using System.Windows.Forms;
using BibliothequeApp.Forms;

namespace BibliothequeApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}