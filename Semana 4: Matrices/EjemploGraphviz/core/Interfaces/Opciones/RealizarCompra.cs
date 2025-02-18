using Gtk;
using System;
using System.Collections.Generic;

namespace core.Interfaces.Opciones
{
    public class RealizarCompraWindow : Window
    {
        private TreeView treeView;
        private ListStore listStore;

        public RealizarCompraWindow() : base("Tabla de Productos")
        {
            // Crear un contenedor para los elementos
            Box vbox = new Box(Orientation.Vertical, 5);

            // Crear un TreeView para mostrar la tabla
            treeView = new TreeView();
            listStore = new ListStore(typeof(int), typeof(string), typeof(float));
            treeView.Model = listStore;

            // Columnas de la tabla
            TreeViewColumn column1 = new TreeViewColumn { Title = "Id" };
            TreeViewColumn column2 = new TreeViewColumn { Title = "Producto" };
            TreeViewColumn column3 = new TreeViewColumn { Title = "Precio" };

            // Crear renderers para las columnas
            CellRendererText renderer1 = new CellRendererText();
            column1.PackStart(renderer1, true);
            column1.AddAttribute(renderer1, "text", 0);  // Columna Id

            CellRendererText renderer2 = new CellRendererText();
            column2.PackStart(renderer2, true);
            column2.AddAttribute(renderer2, "text", 1);  // Columna Producto

            CellRendererText renderer3 = new CellRendererText();
            column3.PackStart(renderer3, true);
            column3.AddAttribute(renderer3, "text", 2);  // Columna Precio

            // Añadir las columnas al TreeView
            treeView.AppendColumn(column1);
            treeView.AppendColumn(column2);
            treeView.AppendColumn(column3);

            // Crear un botón para mostrar el producto seleccionado
            Button showDetailsButton = new Button("Mostrar Detalles");
            showDetailsButton.Clicked += OnShowDetailsButtonClicked;

            // Añadir el TreeView y el botón al contenedor
            vbox.PackStart(treeView, true, true, 0);
            vbox.PackStart(showDetailsButton, false, false, 0);

            // Añadir contenedor a la ventana
            Add(vbox);
            SetDefaultSize(400, 300);

            // Cargar datos aleatorios en la tabla
            CargarDatosDeLista();
        }

        private void CargarDatosDeLista()
        {
            var datos = core.Gestores.Estructuras.ListaSimple.RetornarDatosLista();

            listStore.Clear();

            foreach (var dato in datos)
            {
                listStore.AppendValues(dato.Id, dato.Producto, dato.Precio);
            }
        }


        private void OnShowDetailsButtonClicked(object? sender, EventArgs e)
        {
            // Obtener la fila seleccionada en el TreeView
            TreeIter iter;
            if (treeView.Selection.GetSelected(out iter))
            {
                int id = (int)listStore.GetValue(iter, 0);  // Id
                string producto = (string)listStore.GetValue(iter, 1);  // Producto
                float precio = (float)listStore.GetValue(iter, 2);  // Precio

                core.Gestores.Estructuras.Pila.Apilar(id, producto, precio);
            }
            else
            {
                Console.WriteLine("No se seleccionó ningún producto.");
            }
        }
    }
}
