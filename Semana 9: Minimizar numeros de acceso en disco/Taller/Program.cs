using code.interfaces;
using Gtk;

public class Program
{
    public static void Main(string[] args)
    {
        Application.Init();

        // Crear archivo json
        code.utils.json.Jsons.CrearJson("registros");

        LoginWindow loginWindow = new LoginWindow();
        loginWindow.Show();

        Application.Run();
    }
}
