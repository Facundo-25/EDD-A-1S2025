using Gtk;
using code.structures.tree_binary;
using System;

namespace code.interfaces
{
    public class UserWindowTableServices : Window
    {
        private Button inOrdenButton;
        private Button preOrdenButton;
        private Button postOrdenButton;
        private ListBox listBoxRecorridos;
        private List<Nodo_Servicio> inOrdenList;
        private List<Nodo_Servicio> preOrdenList;
        private List<Nodo_Servicio> postOrdenList;

        public UserWindowTableServices(List<Nodo_Servicio> inOrden, List<Nodo_Servicio> preOrden, List<Nodo_Servicio> postOrden) 
            : base("Servicios")
        {
            inOrdenList = inOrden;
            preOrdenList = preOrden;
            postOrdenList = postOrden;

            SetDefaultSize(400, 300);
            SetPosition(WindowPosition.Center);

            Box vbox = new Box(Orientation.Vertical, 5);

            inOrdenButton = new Button("InOrden");
            inOrdenButton.Clicked += OnInOrdenButtonClicked;
            vbox.PackStart(inOrdenButton, false, false, 0);

            preOrdenButton = new Button("PreOrden");
            preOrdenButton.Clicked += OnPreOrdenButtonClicked;
            vbox.PackStart(preOrdenButton, false, false, 0);

            postOrdenButton = new Button("PostOrden");
            postOrdenButton.Clicked += OnPostOrdenButtonClicked;
            vbox.PackStart(postOrdenButton, false, false, 0);

            listBoxRecorridos = new ListBox();
            vbox.PackStart(listBoxRecorridos, true, true, 0);

            Add(vbox);
            ShowAll();
        }

        private void OnInOrdenButtonClicked(object sender, EventArgs e)
        {
            MostrarRecorrido(inOrdenList);
        }

        private void OnPreOrdenButtonClicked(object sender, EventArgs e)
        {
            MostrarRecorrido(preOrdenList);
        }

        private void OnPostOrdenButtonClicked(object sender, EventArgs e)
        {
            MostrarRecorrido(postOrdenList);
        }

        private void MostrarRecorrido(List<Nodo_Servicio> recorrido)
        {
            foreach (var row in listBoxRecorridos.Children)
            {
                listBoxRecorridos.Remove(row);
            }

            foreach (var servicio in recorrido)
            {
                var row = new ListBoxRow();
                var label = new Label($"ID: {servicio.Id}, Repuesto: {servicio.Id_Repuesto}, Vehículo: {servicio.Id_Vehiculo}, Detalles: {servicio.Detalles}, Costo: {servicio.Costo}");
                row.Add(label);
                listBoxRecorridos.Add(row);
            }

            listBoxRecorridos.ShowAll();
        }
    }
}