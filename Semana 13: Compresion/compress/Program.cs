using System;
using Gtk;
using source.interfaces;

class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        Application.Init();
        BackupApp app = new BackupApp();
        Application.Run();
    }
}
