using code.interfaces;
using Gtk;

public class Program
{
    public static void Main(string[] args)
    {
        Application.Init();

        LoginWindow loginWindow = new LoginWindow();
        loginWindow.Show();

        Application.Run();
    }
}
