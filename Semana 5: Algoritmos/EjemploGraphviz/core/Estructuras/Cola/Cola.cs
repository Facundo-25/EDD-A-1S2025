using System.Runtime.InteropServices;

namespace core.Estructuras.Cola
{
    public unsafe class Cola
    {
        private Nodo* frente;
        private Nodo* fin;


        //* Método constructor
        public Cola()
        {
            frente = null;
            fin = null;
        }


        //* Método para encolar 
        public void Encolar(int id, string producto, float precio)
        {
            Nodo* nuevoNodo = (Nodo*)Marshal.AllocHGlobal(sizeof(Nodo));
            nuevoNodo->Id = id;
            nuevoNodo->Precio = precio;
            nuevoNodo->Siguiente = null;

            // Limpiar el buffer
            for (int i = 0; i < 50; i++)
            {
                nuevoNodo->Producto[i] = '\0'; 
            }

            // Copiar el producto al buffer fijo
            for (int i = 0; i < producto.Length && i < 50; i++)
            {
                nuevoNodo->Producto[i] = producto[i];
            }

            // Si la cola está vacía, el nuevo nodo es tanto el frente como el fin
            if (fin == null)
            {
                frente = nuevoNodo;
                fin = nuevoNodo;
            }
            else
            {
                // Si no está vacía, agregamos el nodo al final
                fin->Siguiente = nuevoNodo;
                fin = nuevoNodo;  // Actualizamos el fin de la cola
            }
        }


        //* Método para desencolar 
        public Nodo* Desencolar()
        {
            if (frente == null)
            {
                Console.WriteLine("La cola está vacía.");
                return null;
            }

            Nodo* nodoDesencolado = frente;
            frente = frente->Siguiente;  // El nuevo frente es el siguiente nodo

            // Si la cola se queda vacía, también actualizamos el fin
            if (frente == null)
            {
                fin = null;
            }

            // Liberar memoria del nodo desencolado
            Marshal.FreeHGlobal((IntPtr)nodoDesencolado);

            return nodoDesencolado;
        }


        //* Método para mostrar la cola
        public void Mostrar()
        {
            Nodo* actual = frente;
            while (actual != null)
            {
                string nombreProducto = ObtenerProducto(actual);
                Console.WriteLine($"ID: {actual->Id}, Producto: {nombreProducto}, Precio: {actual->Precio:C}");
                actual = actual->Siguiente;
            }
        }


        //* Método para obtener el producto del buffer fijo
        private string ObtenerProducto(Nodo* nodo)
        {
            char[] nombreChars = new char[50];
            for (int i = 0; i < 50 && nodo->Producto[i] != '\0'; i++)
            {
                nombreChars[i] = nodo->Producto[i];
            }
            return new string(nombreChars).TrimEnd('\0');
        }


        //* Método para liberar memoria manualmente
        public void LiberarMemoria()
        {
            Nodo* actual = frente;
            while (actual != null)
            {
                Nodo* temp = actual;
                actual = actual->Siguiente;
                Marshal.FreeHGlobal((IntPtr)temp);
            }
            frente = null;
            fin = null;
        }
        
    }
}
