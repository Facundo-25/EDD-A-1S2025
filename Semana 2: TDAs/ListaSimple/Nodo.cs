namespace List
{
    public class Nodo<T>
    {
        // Propiedad Data: almacena el valor o dato del nodo, de tipo genérico T
        public T Data { get; set; }

        // Propiedad Sig: referencia al siguiente nodo en la lista 
        public Nodo<T> Sig { get; set; }

        public Nodo(T data)
        {
            Data = data;   
            Sig = null;    
        }
    }
}
