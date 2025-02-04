namespace Estructuras
{
    class Pila
    {
        private Nodo tope; // Apunta al nodo superior de la pila.

        public Pila()
        {
            tope = null;
        }

        // Método Push: agrega un nuevo nodo al tope de la pila.
        public void Push(int valor)
        {
            Nodo nuevoNodo = new Nodo(valor); // Crea un nuevo nodo con el valor.
            nuevoNodo.Sig = tope; // Apunta al nodo anterior.
            tope = nuevoNodo; // El nuevo nodo es ahora el tope de la pila.
        }

        // Método Pop: elimina y devuelve el valor del nodo superior.
        // Si la pila está vacía, devuelve -999.
        public int Pop()
        {
            if (tope == null) return -999; // Si la pila está vacía, retorna un valor especial.
            int ret = tope.Data; // Guarda el valor del nodo superior.
            tope = tope.Sig; // El tope pasa al siguiente nodo.
            return ret; // Retorna el valor eliminado.
        }

        // Método Print: imprime los valores de la pila.
        public void Print()
        {
            Nodo temp = tope; // Comienza en el tope de la pila.
            while (temp != null) // Mientras haya nodos en la pila.
            {
                Console.Write(temp.Data + " -> "); // Imprime el valor del nodo.
                temp = temp.Sig; // Se mueve al siguiente nodo.
            }
            Console.WriteLine("NULL"); // Indica el final de la pila.
        }
    }
}
