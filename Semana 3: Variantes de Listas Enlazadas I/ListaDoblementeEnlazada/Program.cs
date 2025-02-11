using System;

namespace ListaDobleUnsafe
{
    class Program
    {
        static unsafe void Main()
        {
            ListaDoblementeEnlazada lista = new ListaDoblementeEnlazada();

            lista.Insertar(1, "Tienda A", "Juan Pérez", "Calle 123");
            lista.Insertar(2, "Panadería B", "María López", "Avenida 456");
            lista.Insertar(3, "Farmacia C", "Carlos Gómez", "Boulevard 789");

            Console.WriteLine("Lista de locales:");
            lista.Mostrar();

            Console.WriteLine("\nEliminando el nodo con ID 2...");
            lista.Eliminar(2);

            Console.WriteLine("Lista después de eliminar:");
            lista.Mostrar();


            Console.WriteLine("Lista Mostrada de reversa:");
            lista.MostrarReversa();
        }
    }
}
