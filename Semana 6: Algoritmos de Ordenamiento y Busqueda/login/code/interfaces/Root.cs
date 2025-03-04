using Gtk;

namespace code.interfaces
{
    public class RootWindow : Window
    {
        private Button cargaMasivaButton;
        private Button reportesButton;
        private Button cerrarSesionButton;

        public RootWindow() : base("Root Window")
        {
            SetDefaultSize(300, 250);
            SetPosition(WindowPosition.Center);

            Box vbox = new Box(Orientation.Vertical, 5);

            cargaMasivaButton = new Button("Carga Masiva");
            cargaMasivaButton.Clicked += OnCargaMasivaButtonClicked;
            vbox.PackStart(cargaMasivaButton, false, false, 0);

            reportesButton = new Button("Reportes");
            reportesButton.Clicked += OnReportesButtonClicked;
            vbox.PackStart(reportesButton, false, false, 0);

            cerrarSesionButton = new Button("Cerrar Sesión");
            cerrarSesionButton.Clicked += OnCerrarSesionButtonClicked;
            vbox.PackStart(cerrarSesionButton, false, false, 0);

            Add(vbox);
            ShowAll();
        }

        private void OnCargaMasivaButtonClicked(object sender, EventArgs e)
        {
            Console.WriteLine("Carga Masiva seleccionada.");
        }

        private void OnReportesButtonClicked(object sender, EventArgs e)
        {
            Console.WriteLine("Reportes seleccionados.");
        }

        private void OnCerrarSesionButtonClicked(object sender, EventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Destroy();
        }
    }
}
