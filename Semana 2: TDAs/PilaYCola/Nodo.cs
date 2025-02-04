namespace Estructuras
{
    // Clase Nodo que representa un elemento de una lista enlazada.
    class Nodo
    {
        // Propiedad Data que almacena el valor del nodo.
        public int Data { get; set; }

        // Propiedad Sig que apunta al siguiente nodo.
        public Nodo Sig { get; set; }

        public Nodo(int data)
        {
            Data = data;
            Sig = null;
        }
    }
}
