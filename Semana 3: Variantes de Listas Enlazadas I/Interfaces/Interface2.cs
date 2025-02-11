using Gtk;

public class Interface2 : Window
{
    public Interface2() : base("Interface 2")
    {
        SetDefaultSize(300, 200);
        SetPosition(WindowPosition.Center);

        // Crear un contenedor para los elementos
        VBox vbox = new VBox(false, 5);

        // ListBox
        ListBox listBox = new ListBox();
        listBox.SelectionMode = SelectionMode.Single;

        // Agregar opciones al ListBox
        listBox.Insert(new ListBoxRow { Child = new Label("Opcion1") }, -1);
        listBox.Insert(new ListBoxRow { Child = new Label("Opcion2") }, -1);
        listBox.Insert(new ListBoxRow { Child = new Label("Opcion3") }, -1);

        listBox.RowSelected += OnListBoxRowSelected;
        vbox.PackStart(listBox, true, true, 0);

        // Botón para regresar a Interface1
        Button goBackButton = new Button("Regresar a Interface 1");
        goBackButton.Clicked += OnGoBackButtonClicked;
        vbox.PackStart(goBackButton, false, false, 0);

        Add(vbox);
    }

    private void OnListBoxRowSelected(object sender, RowSelectedArgs e)
    {
        if (e.Row != null)
        {
            Label selectedLabel = (Label)e.Row.Child;
            Console.WriteLine("Opción seleccionada: " + selectedLabel.Text);
        }
    }

    private void OnGoBackButtonClicked(object sender, EventArgs e)
    {
        // Mostrar Interface1 y ocultar Interface2
        Interface1 interface1 = new Interface1();
        interface1.ShowAll();
        this.Hide();
    }
}