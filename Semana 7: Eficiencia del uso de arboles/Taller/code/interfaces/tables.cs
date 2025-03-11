using Gtk;
using code.structures.tree_avl;
using System;

namespace code.interfaces
{
    public class RootWindowTables : Window
    {
        private Button inOrdenButton;
        private Button preOrdenButton;
        private Button postOrdenButton;
        private ListBox listBoxRecorridos;

        public RootWindowTables() : base("Recorridos Árbol AVL")
        {
            SetDefaultSize(400, 300);
            SetPosition(WindowPosition.Center);

            // Crear una caja vertical para organizar los widgets
            Box vbox = new Box(Orientation.Vertical, 5);

            // Crear los botones para seleccionar el tipo de recorrido
            inOrdenButton = new Button("InOrden");
            inOrdenButton.Clicked += OnInOrdenButtonClicked;
            vbox.PackStart(inOrdenButton, false, false, 0);

            preOrdenButton = new Button("PreOrden");
            preOrdenButton.Clicked += OnPreOrdenButtonClicked;
            vbox.PackStart(preOrdenButton, false, false, 0);

            postOrdenButton = new Button("PostOrden");
            postOrdenButton.Clicked += OnPostOrdenButtonClicked;
            vbox.PackStart(postOrdenButton, false, false, 0);

            // Crear el ListBox para mostrar los resultados
            listBoxRecorridos = new ListBox();
            vbox.PackStart(listBoxRecorridos, true, true, 0);

            // Agregar la caja al contenedor principal
            Add(vbox);
            ShowAll();
        }

        // Acción para el botón InOrden
        private void OnInOrdenButtonClicked(object sender, EventArgs e)
        {
            MostrarRecorrido("InOrden");
        }

        // Acción para el botón PreOrden
        private void OnPreOrdenButtonClicked(object sender, EventArgs e)
        {
            MostrarRecorrido("PreOrden");
        }

        // Acción para el botón PostOrden
        private void OnPostOrdenButtonClicked(object sender, EventArgs e)
        {
            MostrarRecorrido("PostOrden");
        }

        // Método para mostrar el recorrido seleccionado
        private void MostrarRecorrido(string tipoRecorrido)
        {
            // Eliminar todas las filas del ListBox antes de agregar nuevas
            foreach (var row in listBoxRecorridos.Children)
            {
                listBoxRecorridos.Remove(row);
            }

            // Obtener el árbol de repuestos de las variables
            var arbol = code.data.Variables.arbolRepuestos;

            Nodo_Repuesto[] recorrido = null;

            // Seleccionar el tipo de recorrido
            switch (tipoRecorrido)
            {
                case "InOrden":
                    recorrido = arbol.TablaInOrden();
                    break;

                case "PreOrden":
                    recorrido = arbol.TablaPreOrden();
                    break;

                case "PostOrden":
                    recorrido = arbol.TablaPostOrden();
                    break;
            }

            // Mostrar los resultados en el ListBox
            foreach (var nodo in recorrido)
            {
                var row = new ListBoxRow();
                var label = new Label($"ID: {nodo.Id}, Repuesto: {nodo.Repuesto}, Costo: {nodo.Costo}");
                row.Add(label);
                listBoxRecorridos.Add(row);
            }

            // Refrescar el ListBox para que se actualice con los nuevos elementos
            listBoxRecorridos.ShowAll();
        }

    }
}
