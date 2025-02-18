using Gtk;
using core.Interfaces.Opciones;
using System.Diagnostics;

namespace core.Interfaces
{
    public class Dashboard : Window
    {
        public Dashboard() : base("Menú")
        {
            // Crear un contenedor para los elementos
            Box vbox = new Box(Orientation.Vertical, 5);

            // Botón para mostrar en consola
            Button cargaMercaderiaButton = new Button("Carga masiva de Mercaderias");
            cargaMercaderiaButton.Clicked += OnCargaMercaderiaButtonClicked;
            vbox.PackStart(cargaMercaderiaButton, false, false, 0);

            Button verMercaderiasButton = new Button("Ver Mercaderías");
            verMercaderiasButton.Clicked += OnVerMercaderiasButtonClicked;
            vbox.PackStart(verMercaderiasButton, false, false, 0);

            Button realizarCompraButton = new Button("Realizar una compra");
            realizarCompraButton.Clicked += OnRealizarCompraButtonClicked;
            vbox.PackStart(realizarCompraButton, false, false, 0);

            Button pagarCompraButton = new Button("Pagar la compra");
            pagarCompraButton.Clicked += OnPagarCompraButtonClicked;
            vbox.PackStart(pagarCompraButton, false, false, 0);

            Button verComprasButton = new Button("Ver compras");
            verComprasButton.Clicked += OnVerComprasButtonClicked;
            vbox.PackStart(verComprasButton, false, false, 0);

            Add(vbox);
        }

        private void OnCargaMercaderiaButtonClicked(object? sender, EventArgs e)
        {
            CargaMercaderiasWindow cargaMercaderiasWindow = new CargaMercaderiasWindow();
            cargaMercaderiasWindow.Maximize();
            cargaMercaderiasWindow.ShowAll();
        }

        private void OnVerMercaderiasButtonClicked(object? sender, EventArgs e)
        {
            string CodigoDot = core.Gestores.Estructuras.ListaSimple.GenerarGraphviz();
            core.Utilidades.Utilidades.GenerarArchivoDot("Mercaderia", CodigoDot);
            core.Utilidades.Utilidades.ConvertirDotAImagen("Mercaderia.dot");

            new MostrarReporteWindow("Mercaderia.png");
            

        }

        private void OnRealizarCompraButtonClicked(object? sender, EventArgs e)
        {
            RealizarCompraWindow realizarCompraWindow = new RealizarCompraWindow();
            realizarCompraWindow.Maximize();
            realizarCompraWindow.ShowAll();
            
        }

        private void OnPagarCompraButtonClicked(object? sender, EventArgs e)
        {
            core.Gestores.Estructuras.Pila.Desapilar();            
        }

        private void OnVerComprasButtonClicked(object? sender, EventArgs e)
        {
            string CodigoDot = core.Gestores.Estructuras.Pila.GenerarGraphviz();
            core.Utilidades.Utilidades.GenerarArchivoDot("Compra", CodigoDot);
            core.Utilidades.Utilidades.ConvertirDotAImagen("Compra.dot");

            new MostrarReporteWindow("Compra.png");
            
        }
    }
}
