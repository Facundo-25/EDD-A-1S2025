using Gtk;

class Program
{
    public static void Main(string[] args)

    {
        Application.Init();

        Menu menu = new Menu();
        menu.ShowAll();


        Application.Run();
    }
}