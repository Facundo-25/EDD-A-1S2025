namespace Estructuras
{
    class Cola
    {
        private Nodo frente; 
        private Nodo final; 

        public Cola()
        {
            frente = null;
            final = null;
        }

        // Método encolar: agrega un nuevo nodo al final de la cola.
        public void encolar(int valor)
        {
            Nodo nuevoNodo = new Nodo(valor); // Crea un nuevo nodo con el valor.
            if (final == null) // Si la cola está vacía, el nuevo nodo es tanto el frente como el final.
            {
                frente = nuevoNodo;
                final = nuevoNodo;
            }
            else // Si la cola no está vacía, agrega el nodo al final y actualiza el final.
            {
                final.Sig = nuevoNodo;
                final = nuevoNodo;
            }
        }

        // Método desencolar: elimina y devuelve el valor del nodo al frente de la cola.
        // Si la cola está vacía, retorna -999.
        public int desencolar()
        {
            if (frente == null) return -999; // Si la cola está vacía, retorna un valor especial.
            int ret = frente.Data; // Guarda el valor del nodo frente.
            frente = frente.Sig; // Mueve el frente al siguiente nodo.
            if (frente == null) final = null; // Si la cola queda vacía, el final también se establece como null.
            return ret; // Retorna el valor eliminado.
        }

        // Método Print: imprime los valores de la cola.
        public void Print()
        {
            Nodo temp = frente; // Comienza desde el frente de la cola.
            while (temp != null) // Mientras haya nodos en la cola.
            {
                Console.Write(temp.Data + " <- "); // Imprime el valor del nodo.
                temp = temp.Sig; // Se mueve al siguiente nodo.
            }
            Console.WriteLine("NULL"); // Indica el final de la cola.
        }
    }
}
