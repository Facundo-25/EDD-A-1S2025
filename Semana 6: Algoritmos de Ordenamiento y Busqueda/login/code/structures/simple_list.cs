using System;


namespace code.structures{

    public class Nodo
    {
        public int Id;
        public string Nombres;
        public string Apellidos;
        public string Correo;
        public int Edad;
        public string Contrasenia;
        public Nodo? Siguiente;  

        public Nodo(int id, string nombres, string apellidos, string correo, int edad, string contrasenia)
        {
            Id = id;
            Nombres = nombres;
            Apellidos = apellidos;
            Correo = correo;
            Edad = edad;
            Contrasenia = contrasenia;
            Siguiente = null;
        }
    }

    public class ListaEnlazada
    {
        private Nodo? cabeza = null;  // Inicializado como null

        public void Agregar(int id, string nombres, string apellidos, string correo, int edad, string contrasenia)
        {
            Nodo nuevo = new Nodo(id, nombres, apellidos, correo, edad, contrasenia);
            if (cabeza == null)
            {
                cabeza = nuevo;
            }
            else
            {
                Nodo actual = cabeza;
                while (actual.Siguiente != null)
                {
                    actual = actual.Siguiente;
                }
                actual.Siguiente = nuevo;
            }
        }



    public int GetSize()
        {
            int size = 0;
            Nodo? actual = cabeza;

            while (actual != null)
            {
                size++;
                actual = actual.Siguiente;
            }

            return size;
        }



        public Nodo? BuscarPorCorreo(string correo)
        {
            Nodo? actual = cabeza;  // Empezamos desde el primer nodo

            // Recorrer la lista
            while (actual != null)
            {
                // Comparamos el correo del nodo actual con el que nos pasaron
                if (actual.Correo.Equals(correo, StringComparison.OrdinalIgnoreCase))
                {
                    return actual;  // Retorna el nodo si hay una coincidencia
                }
                
                actual = actual.Siguiente;  // Avanzamos al siguiente nodo
            }

            return null;  // Retorna null si no se encuentra el correo
        }

        //Validar COntraseña
        public bool ValidarContrasenia(string correo, string contrasenia)
        {
            Nodo? usuario = BuscarPorCorreo(correo);
            if (usuario == null)
            {
                return false;
            }

            return usuario.Contrasenia.Equals(contrasenia);
        }




        public void Imprimir()
        {
            if (cabeza == null)
            {
                Console.WriteLine("La lista está vacía");
                return;
            }
            
            Nodo? actual = cabeza;
            while (actual != null)
            {
                Console.WriteLine($"ID: {actual.Id}, Nombre: {actual.Nombres} {actual.Apellidos}, Edad: {actual.Edad}");
                actual = actual.Siguiente;
            }
        }
    }

}