using System;
using List;

class Program
{
    static unsafe void Main()
    {
        ListaSimple<int> lista = new ListaSimple<int>();
        lista.Insertar(1);
        lista.Insertar(2);
        lista.Insertar(3);

        Console.WriteLine("Lista antes de eliminar:");
        lista.Imprimir();

        lista.Eliminar(2);
        Console.WriteLine("Lista después de eliminar:");
        lista.Imprimir();
    }
}
