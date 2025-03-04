using System.Runtime.InteropServices;

namespace core.Estructuras.ListaSimple
{
    public unsafe class ListaSimple
    {
        private Nodo* cabeza;


        //* Método constructor
        public ListaSimple()
        {
            cabeza = null;
        }


        //* Método para insertar un nuevo nodo en la lista
        public void Insertar(int id, string producto, float precio)
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

            // Insertar el nodo al final de la lista
            if (cabeza == null)
            {
                cabeza = nuevoNodo;
            }
            else
            {
                Nodo* actual = cabeza;
                while (actual->Siguiente != null)
                {
                    actual = actual->Siguiente;
                }
                actual->Siguiente = nuevoNodo;
            }
        }



        //* Método para mostrar la lista
        public void Mostrar()
        {
            Nodo* actual = cabeza;
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
            Nodo* actual = cabeza;
            while (actual != null)
            {
                Nodo* temp = actual;
                actual = actual->Siguiente;
                Marshal.FreeHGlobal((IntPtr)temp);
            }
            cabeza = null;
        }


        //* Método para generar el código Graphviz
        public string GenerarGraphviz()
        {
            // Si la lista está vacía, generamos un solo nodo con "NULL"
            if (cabeza == null)
            {
                return "digraph G {\n    node [shape=record];\n    NULL [label = \"{NULL}\"];\n}\n";
            }

            // Iniciamos el código Graphviz
            var graphviz = "digraph G {\n";
            graphviz += "    node [shape=record];\n";
            graphviz += "    rankdir=LR;\n";
            graphviz += "    subgraph cluster_0 {\n";
            graphviz += "        label = \"Lista Simple\";\n";

            // Iterar sobre los nodos de la lista y construir la representación Graphviz
            Nodo* actual = cabeza;
            int index = 0;

            while (actual != null)
            {
                string nombreProducto = ObtenerProducto(actual);
                graphviz += $"        n{index} [label = \"{{<data> ID: {actual->Id} \\n Producto: {nombreProducto} \\n Precio: {actual->Precio:C} \\n Siguiente: }}\"];\n";
                actual = actual->Siguiente;
                index++;
            }

            // Conectar los nodos
            actual = cabeza;
            for (int i = 0; actual != null && actual->Siguiente != null; i++)
            {
                graphviz += $"        n{i} -> n{i + 1};\n";
                actual = actual->Siguiente;
            }

            graphviz += "    }\n";
            graphviz += "}\n";
            return graphviz;
        }


        public List<(int Id, string Producto, float Precio)> RetornarDatosLista()
        {
            List<(int, string, float)> datosTabla = new List<(int, string, float)>();

            Nodo* actual = cabeza;
            while (actual != null)
            {
                string producto = ObtenerProducto(actual);
                datosTabla.Add((actual->Id, producto, actual->Precio));
                actual = actual->Siguiente;
            }

            return datosTabla;
        }


    }
}

