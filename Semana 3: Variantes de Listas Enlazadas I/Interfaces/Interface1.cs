using Gtk;

public class Interface1 : Window
{
    private Entry inputEntry;

    public Interface1() : base("Interface 1")
    {
        SetDefaultSize(300, 200);
        SetPosition(WindowPosition.Center);

        // Crear un contenedor para los elementos
        VBox vbox = new VBox(false, 5);

        // Label
        Label label = new Label("Ingrese algo:");
        vbox.PackStart(label, false, false, 0);

        // Input
        inputEntry = new Entry();
        vbox.PackStart(inputEntry, false, false, 0);

        // Botón para mostrar en consola
        Button showButton = new Button("Mostrar en consola");
        showButton.Clicked += OnShowButtonClicked;
        vbox.PackStart(showButton, false, false, 0);

        // Botón para ir a Interface2
        Button goToInterface2Button = new Button("Ir a Interface 2");
        goToInterface2Button.Clicked += OnGoToInterface2ButtonClicked;
        vbox.PackStart(goToInterface2Button, false, false, 0);

        Add(vbox);
    }

    private void OnShowButtonClicked(object sender, EventArgs e)
    {
        string inputText = inputEntry.Text;
        Console.WriteLine("Texto ingresado: " + inputText);
    }

    private void OnGoToInterface2ButtonClicked(object sender, EventArgs e)
    {
        Interface2 interface2 = new Interface2();
        interface2.ShowAll();
        this.Hide();
    }
}