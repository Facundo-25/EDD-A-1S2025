using Gtk;

class Program
{
    public static void Main(string[] args)
    {
        Application.Init();

        // Crear la ventana principal
        Interface1 interface1 = new Interface1();
        interface1.ShowAll();

        Application.Run();
    }
}