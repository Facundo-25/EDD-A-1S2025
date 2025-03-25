using Gtk;
using code.structures.tree_b; 
using System;
using System.Collections.Generic;

namespace code.interfaces
{
    public class UserWindowTableInvoices : Window
    {
        private ListBox listBoxFacturas;
        private List<Factura> facturas;

        public UserWindowTableInvoices(List<Factura> listaFacturas) : base("Facturas")
        {
            facturas = listaFacturas;

            SetDefaultSize(400, 300);
            SetPosition(WindowPosition.Center);

            Box vbox = new Box(Orientation.Vertical, 5);

            listBoxFacturas = new ListBox();
            vbox.PackStart(listBoxFacturas, true, true, 0);

            Add(vbox);
            MostrarFacturas();
            ShowAll();
        }

        private void MostrarFacturas()
        {
            foreach (var row in listBoxFacturas.Children)
            {
                listBoxFacturas.Remove(row);
            }

            foreach (var factura in facturas)
            {
                var row = new ListBoxRow();
                var label = new Label($"ID: {factura.Id}, Servicio: {factura.Id_Servicio}, Total: {factura.Total}");
                row.Add(label);
                listBoxFacturas.Add(row);
            }

            listBoxFacturas.ShowAll();
        }
    }
}