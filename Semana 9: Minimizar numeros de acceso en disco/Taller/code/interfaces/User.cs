using Gtk;
using code.structures.tree_binary;
using code.structures.tree_b;

namespace code.interfaces
{
    public class UserWindow : Window
    {
        private Button verVehiculosButton;
        private Button cerrarSesionButton;
        private Button verFacturasButton;
        private Button ElimianrFacturaButton;
        private Label bienvenidaLabel;

        public UserWindow() : base("User Window")
        {
            //Hora de inicio de sesión
            code.data.Variables.entrada = code.utils.time.TimeUtils.ObtenerHoraActual();

            SetDefaultSize(300, 200);
            SetPosition(WindowPosition.Center);

            Box vbox = new Box(Orientation.Vertical, 5);

            bienvenidaLabel = new Label();
            vbox.PackStart(bienvenidaLabel, false, false, 0);

            // Mostrar el mensaje de bienvenida
            var usuario = code.data.Variables.usuarioActual;
            bienvenidaLabel.Text = $"Bienvenido, {usuario.Nombres} {usuario.Apellidos}";

            verVehiculosButton = new Button("Ver Servicios");
            verVehiculosButton.Clicked += OnVerVehiculosButtonClicked;
            vbox.PackStart(verVehiculosButton, false, false, 0);

            verFacturasButton = new Button("Ver Facturas");
            verFacturasButton.Clicked += OnVerFacturasButtonClicked;
            vbox.PackStart(verFacturasButton, false, false, 0);

            ElimianrFacturaButton = new Button("Eliminar Factura");
            ElimianrFacturaButton.Clicked += OnEliminarFacturaButtonClicked;
            vbox.PackStart(ElimianrFacturaButton, false, false, 0);

            cerrarSesionButton = new Button("Cerrar Sesión");
            cerrarSesionButton.Clicked += OnCerrarSesionButtonClicked;
            vbox.PackStart(cerrarSesionButton, false, false, 0);

            Add(vbox);
            ShowAll();
        }

        private void OnVerVehiculosButtonClicked(object sender, EventArgs e)
        {
            int idUsuario = code.data.Variables.usuarioActual.Id;
            List<int> List_Ids_vehiculos = code.data.Variables.listaVehiculos.ListarVehiculos_Usuario(idUsuario);

            List<Nodo_Servicio> List_Servicios_Usuarios_InOrden = code.data.Variables.arbolServicios.TablaInOrden_Vehiculos(List_Ids_vehiculos);
            List<Nodo_Servicio> List_Servicios_Usuarios_PreOrden = code.data.Variables.arbolServicios.TablaPreOrden_Vehiculos(List_Ids_vehiculos);
            List<Nodo_Servicio> List_Servicios_Usuarios_PostOrden = code.data.Variables.arbolServicios.TablaPostOrden_Vehiculos(List_Ids_vehiculos);

            new UserWindowTableServices(List_Servicios_Usuarios_InOrden, List_Servicios_Usuarios_PreOrden, List_Servicios_Usuarios_PostOrden);
        }

        private void OnVerFacturasButtonClicked(object sender, EventArgs e)
        {
            int idUsuario = code.data.Variables.usuarioActual.Id;
            List<int> List_Ids_vehiculos = code.data.Variables.listaVehiculos.ListarVehiculos_Usuario(idUsuario);
            List<int> Lista_Ids_Servicios = code.data.Variables.arbolServicios.Servicios_Vehiculos(List_Ids_vehiculos);
            List<Factura> Lista_Facturas_Usuario = code.data.Variables.arbolFacturas.ObtenerFacturasPorServicios(Lista_Ids_Servicios);

            new UserWindowTableInvoices(Lista_Facturas_Usuario);
        }

        private void OnEliminarFacturaButtonClicked(object sender, EventArgs e)
        {
            new UserWindowDeleteInvoice();
        }

        private void OnCerrarSesionButtonClicked(object sender, EventArgs e)
        {
            //Hora de cierre de sesións
            code.data.Variables.salida = code.utils.time.TimeUtils.ObtenerHoraActual();

            //Obtener valores
            string json_usuario = code.data.Variables.usuarioActual.Correo;
            string json_entrada = code.data.Variables.entrada;
            string json_salida = code.data.Variables.salida;

            //Guardar en archivo JSON
            code.utils.json.Jsons.InsertarRegistro("registros", json_usuario, json_entrada, json_salida);

            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Destroy();
        }
    }
}
