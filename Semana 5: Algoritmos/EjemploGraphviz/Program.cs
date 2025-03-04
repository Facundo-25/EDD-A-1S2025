using Gtk;
using core.Interfaces;

class Program
{
    static void Main(string[] args)
    {
        Application.Init();
        
        Dashboard dashboard = new Dashboard();
        dashboard.Maximize();
        dashboard.ShowAll();
        
        Application.Run();
    }
}
