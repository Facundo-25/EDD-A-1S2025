using Gtk;

namespace code.interfaces
{
    public class RootWindow : Window
    {
        private Button cargaMasivaButton;
        private Button reportesButton;
        private Button MostrarTablas;
        private Button cerrarSesionButton;
        

        public RootWindow() : base("Root Window")
        {
            //Hora de inicio de sesión
            code.data.Variables.entrada = code.utils.time.TimeUtils.ObtenerHoraActual();

            SetDefaultSize(300, 250);
            SetPosition(WindowPosition.Center);

            Box vbox = new Box(Orientation.Vertical, 5);

            cargaMasivaButton = new Button("Carga Masiva");
            cargaMasivaButton.Clicked += OnCargaMasivaButtonClicked;
            vbox.PackStart(cargaMasivaButton, false, false, 0);


            MostrarTablas = new Button("Mostrar Tablas");
            MostrarTablas.Clicked += OnMostrarTablasButtonClicked;
            vbox.PackStart(MostrarTablas, false, false, 0);

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
            code.data.Variables.listaUsuarios.Agregar(1,"Steven","Mejia","sm@usac.com",23,"sm123");
            Console.WriteLine("Usuario Insertado");

            code.data.Variables.listaVehiculos.Insertar(1,1,"Honda Civic",2015,"P-SM");
            code.data.Variables.listaVehiculos.Insertar(2,1,"Toyota Corolla",2016,"P-AB");
            code.data.Variables.listaVehiculos.Insertar(3,2,"Nissan Sentra",2017,"P-CD");
            code.data.Variables.listaVehiculos.Insertar(4,2,"Mazda 3",2018,"P-EF");
            Console.WriteLine("Vehiculos Insertados");

            code.data.Variables.arbolRepuestos.Insertar(1,  "Repuesto 1", "Marca 1", 100);
            code.data.Variables.arbolRepuestos.Insertar(2,  "Repuesto 2", "Marca 2", 200);
            code.data.Variables.arbolRepuestos.Insertar(3,  "Repuesto 3", "Marca 3", 300);
            code.data.Variables.arbolRepuestos.Insertar(4,  "Repuesto 4", "Marca 4", 400);
            code.data.Variables.arbolRepuestos.Insertar(5,  "Repuesto 5", "Marca 5", 500);
            Console.WriteLine("Repuestos Insertados");

            code.data.Variables.arbolServicios.Insertar(5, 1, 1, "Filtro de aire", 20.00);    
            code.data.Variables.arbolServicios.Insertar(3, 2, 3, "Pastillas de freno", 45.99);
            code.data.Variables.arbolServicios.Insertar(2, 3, 5, "Batería", 75.00);           
            code.data.Variables.arbolServicios.Insertar(1, 4, 2, "Filtro de aceite", 25.50);  
            code.data.Variables.arbolServicios.Insertar(4, 5, 4, "Aceite motor", 35.00);      
            Console.WriteLine("Servicios Insertados");

            code.data.Variables.arbolFacturas.Insertar(1,1,100.00);
            code.data.Variables.arbolFacturas.Insertar(2,2,200.00);
            code.data.Variables.arbolFacturas.Insertar(3,3,300.00);
            code.data.Variables.arbolFacturas.Insertar(4,4,400.00);
            code.data.Variables.arbolFacturas.Insertar(5,5,500.00);
            Console.WriteLine("Facturas Insertadas");
        }


        private void OnMostrarTablasButtonClicked(object sender, EventArgs e)
        {
            RootWindowTables rootWindowTables = new RootWindowTables();
            rootWindowTables.Show();
        }

        private void OnReportesButtonClicked(object sender, EventArgs e)
        {

            //Usuarios
            string CodigoDot_Usuarios = code.data.Variables.listaUsuarios.GraficarGraphviz();
            code.utils.Utilidades.GenerarArchivoDot("Usuarios", CodigoDot_Usuarios);
            code.utils.Utilidades.ConvertirDotAImagen("Usuarios.dot");


            //Vehiculos
            string CodigoDot_Vehiculos = code.data.Variables.listaVehiculos.GraficarGraphviz();
            code.utils.Utilidades.GenerarArchivoDot("Vehiculos", CodigoDot_Vehiculos);
            code.utils.Utilidades.ConvertirDotAImagen("Vehiculos.dot");

            //Repuestos
            string CodigoDot_Repuestos = code.data.Variables.arbolRepuestos.GraficarGraphviz();
            code.utils.Utilidades.GenerarArchivoDot("Repuestos", CodigoDot_Repuestos);
            code.utils.Utilidades.ConvertirDotAImagen("Repuestos.dot");


            //Servicios
            string CodigoDot_Servicios = code.data.Variables.arbolServicios.GraficarGraphviz();
            code.utils.Utilidades.GenerarArchivoDot("Servicios", CodigoDot_Servicios);
            code.utils.Utilidades.ConvertirDotAImagen("Servicios.dot");


            //Facturas 
            string CodigoDot_Facturas = code.data.Variables.arbolFacturas.GraficarGraphviz();
            code.utils.Utilidades.GenerarArchivoDot("Facturas", CodigoDot_Facturas);
            code.utils.Utilidades.ConvertirDotAImagen("Facturas.dot");
        }


        private void OnCerrarSesionButtonClicked(object sender, EventArgs e)
        {
            //Hora de cierre de sesión
            code.data.Variables.salida = code.utils.time.TimeUtils.ObtenerHoraActual();

            //Obtener valores
            string json_usuario = "root";
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




            



                   
