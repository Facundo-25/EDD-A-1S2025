namespace core.Estructuras.Pila
{
    public unsafe struct Nodo
    {
        public int Id;
        public fixed char Producto[50]; 
        public float Precio;
        public Nodo* Siguiente;
 

    }
}