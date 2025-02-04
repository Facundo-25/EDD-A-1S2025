using System;
using Estructuras; // Importa el namespace

class Program
{
    static void Main()
    {
        Menu menu = new Menu();
        menu.MostrarMenu();
    }
}

class Menu
{
    public void MostrarMenu()
    {
        int opcion;
        do
        {
            Console.Clear();
            Console.WriteLine("##### MENÚ PRINCIPAL #####");
            Console.WriteLine("1. Cola");
            Console.WriteLine("2. Pila");
            Console.WriteLine("3. Salir");
            Console.Write("Elija una opción: ");

            if (!int.TryParse(Console.ReadLine(), out opcion))
            {
                Console.WriteLine("Ingrese un número válido.");
                Console.ReadKey();
                continue;
            }

            switch (opcion)
            {
                case 1:
                    OpcionCola.Ejecutar();
                    break;
                case 2:
                    OpcionPila.Ejecutar();
                    break;
                case 3:
                    Console.WriteLine("Saliendo del programa  C: gracias ...");
                    break;
                default:
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    break;
            }
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        } while (opcion != 3);
    }
}

class OpcionCola
{
    public static void Ejecutar()
    {
        Console.WriteLine("\n==================================");

        Cola cola = new Cola();
        cola.encolar(10);
        cola.encolar(20);
        cola.encolar(30);


        Console.WriteLine("Cola actual: ");
        cola.Print();
        Console.WriteLine("-----------------\n");


        cola.desencolar();

        Console.WriteLine("Cola después de eliminar un elemento: ");
        cola.Print();
        Console.WriteLine("-----------------\n");

        Console.WriteLine("==================================\n");

    }
}

class OpcionPila
{
    public static void Ejecutar()
    {
        Console.WriteLine("\n==================================");
        Pila pila = new Pila();

        pila.Push(10);
        pila.Push(20);
        pila.Push(30);

        Console.WriteLine("Pila actual: ");
        pila.Print();
        Console.WriteLine("-----------------\n");


        Console.WriteLine("Pila después de eliminar un elemento: ");
        pila.Print();
        Console.WriteLine("-----------------\n");

        Console.WriteLine("==================================\n");
    }
}
