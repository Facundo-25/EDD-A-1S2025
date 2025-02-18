using Gtk;
using System;
using System.IO;

namespace core.Interfaces.Opciones
{
    public class MostrarReporteWindow : Window
    {
        public MostrarReporteWindow(string nombreReporte) : base("Mostrar Reporte")
        {
            // Crear un contenedor para los elementos
            Box vbox = new Box(Orientation.Vertical, 5);

            // Etiqueta para mostrar instrucciones
            Label label = new Label("Reporte generado:");
            vbox.PackStart(label, false, false, 0);

            // Construir la ruta completa de la imagen
            string rutaImagen = "/home/sfmx/Downloads/EjemploGraphviz/reports/" + nombreReporte;

            // Crear un widget Image para mostrar la imagen
            var imagen = new Image();

            // Intentar cargar la imagen desde la ruta proporcionada
            try
            {
                if (File.Exists(rutaImagen)) 
                {
                    imagen.Pixbuf = new Gdk.Pixbuf(rutaImagen); // Cargar la imagen
                }
                else
                {
                    Console.WriteLine($"La imagen no existe en la ruta: {rutaImagen}");
                    label.Text = "Error al cargar la imagen.";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar la imagen: {ex.Message}");
                label.Text = "Error al cargar la imagen.";
            }

            // Añadir la imagen al contenedor
            vbox.PackStart(imagen, true, true, 0);

            // Añadir el contenedor a la ventana
            Add(vbox);

            // Configuración de la ventana
            SetDefaultSize(800, 600);
            DeleteEvent += (o, e) => Application.Quit();

            // Mostrar la ventana
            ShowAll();
        }
    }
}
