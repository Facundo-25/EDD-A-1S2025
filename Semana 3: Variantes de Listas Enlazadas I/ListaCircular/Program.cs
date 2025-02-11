using System;

namespace ListaCircular
{
    class Program
    {
        static unsafe void Main()
        {
            ListaCircular lista = new ListaCircular();

            lista.Insertar(1, "Supermercado X", "Ana Torres", "Calle A #123");
            lista.Insertar(2, "Farmacia Y", "Luis Méndez", "Avenida B #456");
            lista.Insertar(3, "Restaurante Z", "Carmen López", "Carrera C #789");

            Console.WriteLine("Lista de locales:");
            lista.Mostrar();

            Console.WriteLine("\nEliminando el nodo con ID 2...");
            lista.Eliminar(2);

            Console.WriteLine("Lista después de eliminar:");
            lista.Mostrar();
        }
    }
}
