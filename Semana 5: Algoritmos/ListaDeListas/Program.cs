using System;
using System.Text;

// Clase para representar un nodo de la lista enlazada
class Nodo
{
    public int Valor { get; set; }
    public Nodo Siguiente { get; set; }

    public Nodo(int valor)
    {
        Valor = valor;
        Siguiente = null;
    }
}

// Clase para representar una lista enlazada simple
class ListaEnlazada
{
    public Nodo Cabeza { get; private set; }

    public ListaEnlazada()
    {
        Cabeza = null;
    }

    // Método para insertar un valor al final de la lista
    public void Insertar(int valor)
    {
        Nodo nuevoNodo = new Nodo(valor);
        if (Cabeza == null)
        {
            Cabeza = nuevoNodo;
        }
        else
        {
            Nodo actual = Cabeza;
            while (actual.Siguiente != null)
            {
                actual = actual.Siguiente;
            }
            actual.Siguiente = nuevoNodo;
        }
    }

    // Método para mostrar la lista en la consola
    public void Mostrar()
    {
        Nodo actual = Cabeza;
        while (actual != null)
        {
            Console.Write(actual.Valor + " -> ");
            actual = actual.Siguiente;
        }
        Console.WriteLine("null");
    }

    // Método para buscar un valor en la lista
    public bool Buscar(int valor)
    {
        Nodo actual = Cabeza;
        while (actual != null)
        {
            if (actual.Valor == valor)
            {
                return true;
            }
            actual = actual.Siguiente;
        }
        return false;
    }

    // Método para generar un string en formato DOT para graficar con Graphviz
    public string Graficar(string nombreLista)
    {
        StringBuilder dot = new StringBuilder();
        dot.AppendLine($"subgraph cluster_{nombreLista} {{");
        dot.AppendLine($"  label = \"{nombreLista}\";");
        dot.AppendLine("  node [shape=box];");

        Nodo actual = Cabeza;
        int i = 0;

        while (actual != null)
        {
            dot.AppendLine($"  node_{nombreLista}_{i} [label=\"{actual.Valor}\"];");
            if (i > 0)
            {
                dot.AppendLine($"  node_{nombreLista}_{i - 1} -> node_{nombreLista}_{i};");
            }
            actual = actual.Siguiente;
            i++;
        }

        dot.AppendLine("}");
        return dot.ToString();
    }
}

// Clase para representar una lista de listas enlazadas
class ListaDeListas
{
    private ListaEnlazada[] listas;

    public ListaDeListas(int cantidadListas)
    {
        listas = new ListaEnlazada[cantidadListas];
        for (int i = 0; i < cantidadListas; i++)
        {
            listas[i] = new ListaEnlazada();
        }
    }

    // Método para insertar un valor en una lista específica
    public void Insertar(int indiceLista, int valor)
    {
        if (indiceLista >= 0 && indiceLista < listas.Length)
        {
            listas[indiceLista].Insertar(valor);
        }
        else
        {
            Console.WriteLine("Índice de lista no válido.");
        }
    }

    // Método para mostrar todas las listas en la consola
    public void Mostrar()
    {
        for (int i = 0; i < listas.Length; i++)
        {
            Console.Write($"Lista {i + 1}: ");
            listas[i].Mostrar();
        }
    }

    // Método para buscar un valor en todas las listas
    public void Buscar(int valor)
    {
        bool encontrado = false;
        for (int i = 0; i < listas.Length; i++)
        {
            if (listas[i].Buscar(valor))
            {
                Console.WriteLine($"El valor {valor} se encontró en la lista {i + 1}.");
                encontrado = true;
            }
        }
        if (!encontrado)
        {
            Console.WriteLine($"El valor {valor} no se encontró en ninguna lista.");
        }
    }

    // Método para generar un string en formato DOT para graficar con Graphviz
    public string Graficar()
    {
        StringBuilder dot = new StringBuilder();
        dot.AppendLine("digraph ListaDeListas {");
        dot.AppendLine("  node [shape=box];");

        for (int i = 0; i < listas.Length; i++)
        {
            dot.Append(listas[i].Graficar($"Lista_{i + 1}"));
        }

        dot.AppendLine("}");
        return dot.ToString();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Crear una lista de listas con 3 listas enlazadas
        ListaDeListas listaDeListas = new ListaDeListas(3);

        // Insertar valores en las listas
        listaDeListas.Insertar(0, 10);
        listaDeListas.Insertar(0, 20);
        listaDeListas.Insertar(1, 30);
        listaDeListas.Insertar(2, 40);
        listaDeListas.Insertar(2, 50);
        listaDeListas.Insertar(2, 30);

        // Mostrar todas las listas
        Console.WriteLine("Mostrando la lista de listas:");
        listaDeListas.Mostrar();

        // Buscar un valor en las listas
        Console.WriteLine("\nBuscando el valor 30:");
        listaDeListas.Buscar(30);

        // Graficar la lista de listas y obtener el string en formato DOT
        string dot = listaDeListas.Graficar();
        Console.WriteLine("\nString en formato DOT para Graphviz:");
        Console.WriteLine(dot);
    }
}