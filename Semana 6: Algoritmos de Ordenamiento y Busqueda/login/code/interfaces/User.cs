using Gtk;

namespace code.interfaces
{
    public class UserWindow : Window
    {
        private Button verVehiculosButton;
        private Button cerrarSesionButton;
        private Label bienvenidaLabel;

        public UserWindow() : base("User Window")
        {
            SetDefaultSize(300, 200);
            SetPosition(WindowPosition.Center);

            Box vbox = new Box(Orientation.Vertical, 5);

            bienvenidaLabel = new Label();
            vbox.PackStart(bienvenidaLabel, false, false, 0);

            // Mostrar el mensaje de bienvenida
            var usuario = code.data.Variables.usuarioActual;
            bienvenidaLabel.Text = $"Bienvenido, {usuario.Nombres} {usuario.Apellidos}";

            verVehiculosButton = new Button("Ver Vehículos");
            verVehiculosButton.Clicked += OnVerVehiculosButtonClicked;
            vbox.PackStart(verVehiculosButton, false, false, 0);

            cerrarSesionButton = new Button("Cerrar Sesión");
            cerrarSesionButton.Clicked += OnCerrarSesionButtonClicked;
            vbox.PackStart(cerrarSesionButton, false, false, 0);

            Add(vbox);
            ShowAll();
        }

        private void OnVerVehiculosButtonClicked(object sender, EventArgs e)
        {
            Console.WriteLine("TENGO VEHÍCULOS");
        }

        private void OnCerrarSesionButtonClicked(object sender, EventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Destroy();
        }
    }
}
