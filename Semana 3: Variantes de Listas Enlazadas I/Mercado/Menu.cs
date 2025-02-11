using Gtk;

public class Menu : Window
{
    public Menu() : base("Interface 1")
    {
        SetDefaultSize(300, 200);
        SetPosition(WindowPosition.Center);

        // Crear un contenedor para los elementos
        VBox vbox = new VBox(false, 5);

        // Label
        Label label = new Label("Menu");
        vbox.PackStart(label, false, false, 0);


        // Cargar Archivo
        Button Btn_CargaMasiva = new Button("Carga Masiva");
        Btn_CargaMasiva.Clicked += GoCargaMasiva;
        vbox.PackStart(Btn_CargaMasiva, false, false, 0);

        // Insertar Manual
        Button Btn_IngresoManual = new Button("Mostrar Lista");
        Btn_IngresoManual.Clicked += GoIngresoManual;
        vbox.PackStart(Btn_IngresoManual, false, false, 0);


        Add(vbox);
    }

    private void GoCargaMasiva(object sender, EventArgs e)
    {
        CargaMasiva cargaMasiva = new CargaMasiva();
        cargaMasiva.ShowAll();
    }

    private void GoIngresoManual(object sender, EventArgs e)
    {
        ListaGlobal.Lista_Local.Mostrar();
    }

}