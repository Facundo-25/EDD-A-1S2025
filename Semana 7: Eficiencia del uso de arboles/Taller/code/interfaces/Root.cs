using Gtk;

namespace code.interfaces
{
    public class RootWindow : Window
    {
        private Button cargaMasivaButton;
        private Button InsertarUsuario;
        private Button InsertarVehiculo;
        private Button InsertarRepuesto;
        private Button InsertarServicio;
        private Button reportesButton;
        private Button MostrarTablas;
        private Button cerrarSesionButton;

        public RootWindow() : base("Root Window")
        {
            SetDefaultSize(300, 250);
            SetPosition(WindowPosition.Center);

            Box vbox = new Box(Orientation.Vertical, 5);

            cargaMasivaButton = new Button("Carga Masiva");
            cargaMasivaButton.Clicked += OnCargaMasivaButtonClicked;
            vbox.PackStart(cargaMasivaButton, false, false, 0);

            InsertarUsuario = new Button("Insertar Usuario");
            InsertarUsuario.Clicked += OnInsertarUsuarioButtonClicked;
            vbox.PackStart(InsertarUsuario, false, false, 0);

            InsertarVehiculo = new Button("Insertar Vehiculo");
            InsertarVehiculo.Clicked += OnInsertarVehiculoButtonClicked;
            vbox.PackStart(InsertarVehiculo, false, false, 0);
            
            InsertarRepuesto = new Button("Insertar Repuesto");
            InsertarRepuesto.Clicked += OnInsertarRepuestoButtonClicked;
            vbox.PackStart(InsertarRepuesto, false, false, 0);

            InsertarServicio = new Button("Insertar Servicio");
            InsertarServicio.Clicked += OnInsertarServicioButtonClicked;
            vbox.PackStart(InsertarServicio, false, false, 0);

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
            Console.WriteLine("Carga Masiva");
        }

        private void OnInsertarUsuarioButtonClicked(object sender, EventArgs e)
        {
            code.data.Variables.listaUsuarios.Agregar(1,"Steven","Mejia","sm@usac.com",23,"sm123");

        }

        private void OnInsertarVehiculoButtonClicked(object sender, EventArgs e)
        {
            code.data.Variables.listaVehiculos.Insertar(1,1,"Honda Civic",2015,"P-SM");
            code.data.Variables.listaVehiculos.Insertar(2,1,"Toyota Corolla",2016,"P-AB");
            code.data.Variables.listaVehiculos.Insertar(3,1,"Nissan Sentra",2017,"P-CD");
            code.data.Variables.listaVehiculos.Insertar(4,1,"Mazda 3",2018,"P-EF");
            code.data.Variables.listaVehiculos.Insertar(5,1,"Hyundai Elantra",2019,"P-GH");
        }

        private void OnInsertarRepuestoButtonClicked(object sender, EventArgs e)
        {
            code.data.Variables.arbolRepuestos.Insertar(1,  "Repuesto 1", "Marca 1", 100);
            code.data.Variables.arbolRepuestos.Insertar(2,  "Repuesto 2", "Marca 2", 200);
            code.data.Variables.arbolRepuestos.Insertar(3,  "Repuesto 3", "Marca 3", 300);
            code.data.Variables.arbolRepuestos.Insertar(4,  "Repuesto 4", "Marca 4", 400);
            code.data.Variables.arbolRepuestos.Insertar(5,  "Repuesto 5", "Marca 5", 500);
            code.data.Variables.arbolRepuestos.Insertar(6,  "Repuesto 6", "Marca 6", 600);
            code.data.Variables.arbolRepuestos.Insertar(7,  "Repuesto 7", "Marca 7", 700);
            code.data.Variables.arbolRepuestos.Insertar(8,  "Repuesto 8", "Marca 8", 800);
            code.data.Variables.arbolRepuestos.Insertar(9,  "Repuesto 9", "Marca 9", 900);
            code.data.Variables.arbolRepuestos.Insertar(10, "Repuesto 10", "Marca 10", 1000);
        }

        private void OnInsertarServicioButtonClicked(object sender, EventArgs e)
        {
            code.data.Variables.arbolServicios.Insertar(5, 1005, 2005, "Filtro de aire", 20.00);    
            code.data.Variables.arbolServicios.Insertar(3, 1003, 2003, "Pastillas de freno", 45.99);
            code.data.Variables.arbolServicios.Insertar(7, 1007, 2007, "Batería", 75.00);           
            code.data.Variables.arbolServicios.Insertar(1, 1001, 2001, "Filtro de aceite", 25.50);  
            code.data.Variables.arbolServicios.Insertar(4, 1004, 2004, "Aceite motor", 35.00);      
            code.data.Variables.arbolServicios.Insertar(6, 1006, 2006, "Líquido de frenos", 10.50); 
            code.data.Variables.arbolServicios.Insertar(9, 1009, 2009, "Neumático", 100.00);        
            code.data.Variables.arbolServicios.Insertar(2, 1002, 2002, "Bujía", 15.75);             
            code.data.Variables.arbolServicios.Insertar(8, 1008, 2008, "Lámpara", 5.25);            
            code.data.Variables.arbolServicios.Insertar(10, 1010, 2010, "Escobillas", 12.00);
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
        }


        private void OnCerrarSesionButtonClicked(object sender, EventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Destroy();
        }
    }
}




            



                   
