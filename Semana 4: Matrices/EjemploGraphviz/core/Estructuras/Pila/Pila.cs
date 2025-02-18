using System.Runtime.InteropServices;

namespace core.Estructuras.Pila
{
    public unsafe class Pila
    {
        private Nodo* cima;


        //* Método constructor
        public Pila()
        {
            cima = null;
        }


        //* Método para apilar
        public void Apilar(int id, string producto, float precio)
        {
            Nodo* nuevoNodo = (Nodo*)Marshal.AllocHGlobal(sizeof(Nodo));
            nuevoNodo->Id = id;
            nuevoNodo->Precio = precio;
            nuevoNodo->Siguiente = cima;

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

            // Actualizamos la cima de la pila para que apunte al nuevo nodo
            cima = nuevoNodo;
        }


        //* Método para desapilar
        public void Desapilar()
        {
            if (cima == null)
            {
                Console.WriteLine("La pila está vacía.");
            }

            Nodo* nodoDesapilado = cima;
            cima = cima->Siguiente;  // La nueva cima es el siguiente nodo en la lista

            // Liberar memoria del nodo desapilado
            Marshal.FreeHGlobal((IntPtr)nodoDesapilado);

        }


        //* Método para mostrar la pila
        public void Mostrar()
        {
            Nodo* actual = cima;
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


        //* Método para generar el código Graphviz de la pila
        public string GenerarGraphviz()
        {
            // Si la pila está vacía, mostramos solo un nodo con "NULL"
            if (cima == null)
            {
                return "digraph G {\n    node [shape=record];\n    NULL [label = \"{NULL}\"];\n}\n";
            }

            // Iniciamos el código Graphviz
            var graphviz = "digraph G {\n";
            graphviz += "    node [shape=record];\n";
            graphviz += "    rankdir=TB;\n";  // Definir el flujo de arriba hacia abajo para la pila
            graphviz += "    subgraph cluster_0 {\n";
            graphviz += "        label = \"Pila\";\n";

            // Iterar sobre los nodos de la pila y construir la representación Graphviz
            Nodo* actual = cima;
            int index = 0;

            while (actual != null)
            {
                string nombreProducto = ObtenerProducto(actual);
                graphviz += $"        n{index} [label = \"{{<data> ID: {actual->Id} \n Producto: {nombreProducto} \n Precio: {actual->Precio:C} \n Siguiente: }}\"];\n";
                actual = actual->Siguiente;
                index++;
            }

            // Conectar los nodos (de arriba hacia abajo, representando el orden de la pila)
            actual = cima;
            for (int i = 0; actual != null && actual->Siguiente != null; i++)
            {
                graphviz += $"        n{i} -> n{i + 1};\n";
                actual = actual->Siguiente;
            }

            graphviz += "    }\n";
            graphviz += "}\n";
            return graphviz;
        }


        //* Método para liberar memoria manualmente
        public void LiberarMemoria()
        {
            Nodo* actual = cima;
            while (actual != null)
            {
                Nodo* temp = actual;
                actual = actual->Siguiente;
                Marshal.FreeHGlobal((IntPtr)temp);
            }
            cima = null;
        }
    }
}
