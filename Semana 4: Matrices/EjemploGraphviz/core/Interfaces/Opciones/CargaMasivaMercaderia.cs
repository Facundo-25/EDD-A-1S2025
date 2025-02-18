using Gtk;
using Newtonsoft.Json;


namespace core.Interfaces.Opciones
{
    public class CargaMercaderiasWindow : Window
    {
        public CargaMercaderiasWindow() : base("Carga masiva de Mercaderías")
        {
            // Crear un contenedor para los elementos
            Box vbox = new Box(Orientation.Vertical, 5);

            // Etiqueta para mostrar instrucciones
            Label label = new Label("Selecciona un archivo JSON para cargar los productos.");
            vbox.PackStart(label, false, false, 0);

            // Crear un FileChooserDialog para seleccionar el archivo JSON
            Button selectFileButton = new Button("Seleccionar archivo JSON");
            selectFileButton.Clicked += OnSelectFileButtonClicked;
            vbox.PackStart(selectFileButton, false, false, 0);

            // Añadir contenedor a la ventana
            Add(vbox);
            SetDefaultSize(300, 200);
        }


        private void OnSelectFileButtonClicked(object? sender, EventArgs e)
        {
            // Crear un FileChooserDialog para seleccionar el archivo JSON
            FileChooserDialog fileChooser = new FileChooserDialog(
                "Selecciona un archivo JSON",
                this,
                FileChooserAction.Open,
                "Cancelar", ResponseType.Cancel,
                "Abrir", ResponseType.Accept);

            fileChooser.Filter = new FileFilter();
            fileChooser.Filter.AddPattern("*.json");

            if (fileChooser.Run() == (int)ResponseType.Accept)
            {
                string filePath = fileChooser.Filename;

                // Verificar que el archivo no sea nulo o vacío
                if (!string.IsNullOrEmpty(filePath))
                {
                    CargarProductosDesdeJson(filePath);
                    this.Destroy();
                }
            }

            fileChooser.Destroy();
        }


        private void CargarProductosDesdeJson(string filePath)
        {
            try
            {
                // Leer el archivo JSON
                string jsonContent = File.ReadAllText(filePath);

                // Verificar si el archivo está vacío
                if (string.IsNullOrEmpty(jsonContent))
                {
                    Console.WriteLine("El archivo JSON está vacío.");
                    return;
                }

                // Deserializar el contenido del archivo JSON
                var productos = JsonConvert.DeserializeObject<List<Mercancia>>(jsonContent);

                // Verificar si los productos se deserializaron correctamente
                if (productos != null && productos.Count > 0)
                {
                    foreach (var producto in productos)
                    {
                        // Verificar que el producto y su nombre no sean nulos
                        if (producto != null && !string.IsNullOrEmpty(producto.Producto))
                        {
                            core.Gestores.Estructuras.ListaSimple.Insertar(producto.Id, producto.Producto, producto.Precio);
                        }
                        else
                        {
                            Console.WriteLine($"Producto con ID {producto?.Id} tiene datos inválidos.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No se pudieron cargar los productos desde el archivo JSON.");
                }
            }
            catch (JsonException jsonEx)
            {
                // Capturar errores de deserialización
                Console.WriteLine($"Error al deserializar el archivo JSON: {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                // Capturar cualquier otro tipo de error
                Console.WriteLine($"Error al cargar el archivo JSON: {ex.Message}");
            }
        }


    }


    public class Mercancia
    {
        public int Id { get; set; }
        public string? Producto { get; set; }  // Debe ser 'Producto' para coincidir con el JSON
        public float Precio { get; set; }
    }



}
