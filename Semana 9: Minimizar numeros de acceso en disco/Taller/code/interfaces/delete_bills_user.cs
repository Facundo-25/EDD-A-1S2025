using Gtk;
using code.structures.tree_b;
using System;

namespace code.interfaces
{
    public class UserWindowDeleteInvoice : Window
    {
        private Entry entryId;
        private Button searchButton;
        private Button deleteButton;
        private Label infoLabel;

        public UserWindowDeleteInvoice() : base("Eliminar Factura")
        {
            SetDefaultSize(300, 200);
            SetPosition(WindowPosition.Center);

            Box vbox = new Box(Orientation.Vertical, 5);

            entryId = new Entry();
            entryId.PlaceholderText = "Ingrese ID de la factura";
            vbox.PackStart(entryId, false, false, 5);

            searchButton = new Button("Buscar");
            searchButton.Clicked += OnSearchButtonClicked;
            vbox.PackStart(searchButton, false, false, 5);

            deleteButton = new Button("Eliminar");
            deleteButton.Clicked += OnDeleteButtonClicked;
            deleteButton.Sensitive = false; 
            vbox.PackStart(deleteButton, false, false, 5);

            infoLabel = new Label("Ingrese un ID para buscar la factura.");
            vbox.PackStart(infoLabel, true, true, 5);

            Add(vbox);
            ShowAll();
        }

        private void OnSearchButtonClicked(object sender, EventArgs e)
        {
            if (int.TryParse(entryId.Text, out int id))
            {
                Factura factura = code.data.Variables.arbolFacturas.Buscar(id);
                
                if (factura != null)
                {
                    int idUsuario = code.data.Variables.usuarioActual.Id;
                    List<int> List_Ids_vehiculos = code.data.Variables.listaVehiculos.ListarVehiculos_Usuario(idUsuario);
                    List<int> Lista_Ids_Servicios = code.data.Variables.arbolServicios.Servicios_Vehiculos(List_Ids_vehiculos);
                    List<Factura> Lista_Facturas_Usuario = code.data.Variables.arbolFacturas.ObtenerFacturasPorServicios(Lista_Ids_Servicios);
                    List<int> Ids_Facturas_Usuario = Lista_Facturas_Usuario.Select(f => f.Id).ToList();

                    if (Ids_Facturas_Usuario.Contains(id))
                    {
                        infoLabel.Text = $"ID: {factura.Id}, Servicio: {factura.Id_Servicio}, Total: {factura.Total}";
                        deleteButton.Sensitive = true; 
                    }
                    else
                    {
                        infoLabel.Text = "No tienes ninguna factura con este ID.";
                        deleteButton.Sensitive = false; 
                    }
                }
                else
                {
                    infoLabel.Text = "Factura no encontrada.";
                    deleteButton.Sensitive = false;
                }
            }
            else
            {
                infoLabel.Text = "Por favor, ingrese un ID válido.";
                deleteButton.Sensitive = false;
            }
        }


        private void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            if (int.TryParse(entryId.Text, out int id))
            {
                int idUsuario = code.data.Variables.usuarioActual.Id;
                List<int> List_Ids_vehiculos = code.data.Variables.listaVehiculos.ListarVehiculos_Usuario(idUsuario);
                List<int> Lista_Ids_Servicios = code.data.Variables.arbolServicios.Servicios_Vehiculos(List_Ids_vehiculos);
                List<Factura> Lista_Facturas_Usuario = code.data.Variables.arbolFacturas.ObtenerFacturasPorServicios(Lista_Ids_Servicios);
                
                List<int> Ids_Facturas_Usuario = Lista_Facturas_Usuario.Select(f => f.Id).ToList();

                if (Ids_Facturas_Usuario.Contains(id))
                {
                    code.data.Variables.arbolFacturas.Eliminar(id);
                    infoLabel.Text = $"Factura con ID {id} eliminada.";
                    deleteButton.Sensitive = false;
                    entryId.Text = "";
                }
                else
                {
                    infoLabel.Text = "Error: No puedes eliminar esta factura porque no te pertenece.";
                }
            }
            else
            {
                infoLabel.Text = "Por favor, ingrese un ID válido.";
            }
        }

    }
}