using Gtk;
using code.data;
using code.interfaces;  
using code.structures;

namespace code.interfaces
{
    public class LoginWindow : Window
    {
        private Entry usernameEntry;
        private Entry passwordEntry;
        private Button loginButton;
        private Button registerButton;  

        public LoginWindow() : base("Login Window")
        {
        
            SetDefaultSize(300, 200);
            SetPosition(WindowPosition.Center);

            
            VBox vbox = new VBox(false, 5);

            
            Label usernameLabel = new Label("Username:");
            vbox.PackStart(usernameLabel, false, false, 0);

            
            usernameEntry = new Entry();
            vbox.PackStart(usernameEntry, false, false, 0);

            
            Label passwordLabel = new Label("Password:");
            vbox.PackStart(passwordLabel, false, false, 0);

            
            passwordEntry = new Entry();
            passwordEntry.Visibility = false; // Establecer para que no se vea la contraseña
            vbox.PackStart(passwordEntry, false, false, 0);

            
            loginButton = new Button("Login");
            loginButton.Clicked += OnLoginButtonClicked;
            vbox.PackStart(loginButton, false, false, 0);

            
            registerButton = new Button("Registrar");  
            registerButton.Clicked += OnRegisterButtonClicked; 
            vbox.PackStart(registerButton, false, false, 0);

            
            Add(vbox);

            
            ShowAll();
        }

        
        private void OnLoginButtonClicked(object sender, EventArgs e)
        {
            
            string username = usernameEntry.Text;
            string password = passwordEntry.Text;

            if (username == "" || password == "")
            {
                MessageDialog messageDialog = new MessageDialog(
                    this,
                    DialogFlags.Modal,
                    MessageType.Error,
                    ButtonsType.Ok,
                    "Por favor, ingrese un nombre de usuario y una contraseña."
                );
                messageDialog.Run();
                messageDialog.Destroy();
                return;
            }

            if (username == "root@usac.com" && password == "root123")
            {
                this.Destroy();
                RootWindow rootWindow = new RootWindow();
                rootWindow.Show();
                return;
            }
            
            code.data.Variables.usuarioActual = code.data.Variables.listaUsuarios.BuscarPorCorreo(username);
    
            if (code.data.Variables.usuarioActual == null){
                MessageDialog messageDialog = new MessageDialog(
                    this,
                    DialogFlags.Modal,
                    MessageType.Error,
                    ButtonsType.Ok,
                    "Usuario no encontrado."
                );
                messageDialog.Run();
                messageDialog.Destroy();
                return;
            }

            if (code.data.Variables.listaUsuarios.ValidarContrasenia(username, password))
            {
                this.Destroy();

                code.data.Variables.usuarioActual = code.data.Variables.listaUsuarios.BuscarPorCorreo(username);
                
                UserWindow userWindow = new UserWindow();
                userWindow.Show();
            }
            else
            {
                MessageDialog messageDialog = new MessageDialog(
                    this,
                    DialogFlags.Modal,
                    MessageType.Error,
                    ButtonsType.Ok,
                    "Contraseña incorrecta."
                );
                messageDialog.Run();
                messageDialog.Destroy();
            }            
            
        }

        
        private void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
        }
    }
}
