using System;
using Gtk;
using source.interfaces;
using source.structures;

namespace source.interfaces
{
    class BackupApp
    {
        private Window mainWindow;
        private VBox vbox;
        private Button btnInsert;
        private Button btnView;
        private Button btnBackup;
        private Button btnLoadBackup;

        public BackupApp()
        {
            mainWindow = new Window("Menú Simple");
            mainWindow.SetDefaultSize(400, 300);
            mainWindow.DeleteEvent += delegate { Application.Quit(); };

            vbox = new VBox();
            vbox.Spacing = 10;
            vbox.BorderWidth = 10;

            btnInsert = new Button("Insertar Datos");
            btnView = new Button("Visualizar Datos");
            btnBackup = new Button("Generar Backup");
            btnLoadBackup = new Button("Cargar Backup");

            btnInsert.Clicked += OnInsertClicked;
            btnView.Clicked += OnViewClicked;
            btnBackup.Clicked += OnBackupClicked;
            btnLoadBackup.Clicked += OnLoadBackupClicked;

            vbox.PackStart(btnInsert, false, false, 0);
            vbox.PackStart(btnView, false, false, 0);
            vbox.PackStart(btnBackup, false, false, 0);
            vbox.PackStart(btnLoadBackup, false, false, 0);

            mainWindow.Add(vbox);
            mainWindow.ShowAll();
        }

        private void OnInsertClicked(object? sender, EventArgs e)
        {
            Console.WriteLine("===================================================");
            Console.WriteLine("Ingresando Vehiculos...");
            source.variables.Variables.VehicleList.InsertVehicle(1, "123456", "Toyota", "Corolla", "ABC123");
            source.variables.Variables.VehicleList.InsertVehicle(2, "654321", "Honda", "Civic", "XYZ789");
            source.variables.Variables.VehicleList.InsertVehicle(3, "789012", "Ford", "Focus", "LMN456");
            source.variables.Variables.VehicleList.InsertVehicle(4, "345678", "Chevrolet", "Malibu", "DEF321");
            source.variables.Variables.VehicleList.InsertVehicle(5, "901234", "Nissan", "Altima", "GHI654");
            Console.WriteLine("===================================================");
        }

        private void OnViewClicked(object? sender, EventArgs e)
        {
            Console.WriteLine("==================================================");
            Console.WriteLine("Visualizando Vehiculos..."); 
            source.variables.Variables.VehicleList.ViewVehicles();
            Console.WriteLine("==================================================");
        }

        private void OnBackupClicked(object? sender, EventArgs e)
        {
            Console.WriteLine("===================================================");
            Console.WriteLine("Generando texto plano de vehiculos...........");
            source.variables.Variables.VehicleList.GenerateFile();
            Console.WriteLine("===================================================");

            Console.WriteLine("===================================================");
            Console.WriteLine("Generando backup de vehiculos...........");
            HuffmanCompressor compressor = new HuffmanCompressor();
            compressor.Compress("vehiculos");
            Console.WriteLine("===================================================");

        }

        private void OnLoadBackupClicked(object? sender, EventArgs e)
        {
            Console.WriteLine("===================================================");
            Console.WriteLine("Cargando backup de vehiculos...........");
            HuffmanCompressor compressor = new HuffmanCompressor();
            string decompressVehicule = compressor.Decompress("vehiculos");
            
            string[] lineas = decompressVehicule.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string linea in lineas)
                {
                    string[] atributos = linea.Split(',');
                    if (atributos.Length == 5)
                    {
                        if (int.TryParse(atributos[0], out int id))
                        {
                            string idUsuario = atributos[1];
                            string marca = atributos[2];
                            string modelo = atributos[3];
                            string placa = atributos[4];
                            source.variables.Variables.VehicleList.InsertVehicle(id, idUsuario, marca, modelo, placa);
                        }
                        else
                        {
                            Console.WriteLine($"Error: No se pudo parsear el ID en la línea: {linea}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Error: Línea con formato incorrecto: {linea}");
                    }
                }
            Console.WriteLine("===================================================");
            
        }
    }
}