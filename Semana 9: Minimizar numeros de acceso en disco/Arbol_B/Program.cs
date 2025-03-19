using System;

class Program
{
    static void Main(string[] args)
    {
        BTree tree = new BTree();
        int i = 1;

        Console.WriteLine("Presiona Enter para insertar un nodo en el árbol. Escribe 'exit' para salir.");

        while (true)
        {
            string input = Console.ReadLine();

            if (input.ToLower() == "exit") 
                break;

            tree.Insert(new Factura(i, i, i * 100));
            i++;

            Console.WriteLine("Inserción de factura exitosa con ID: " + (i-1));
            tree.GenerateGraphviz(); 
        }

        Console.WriteLine("Finalizando programa...");
    }
}
