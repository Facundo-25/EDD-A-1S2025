using System;

// Clase que representa un nodo del árbol
public class Nodo
{
    public int Valor;         // Valor almacenado en el nodo
    public Nodo Izquierda;    // Referencia al hijo izquierdo
    public Nodo Derecha;      // Referencia al hijo derecho

    // Constructor del nodo
    public Nodo(int valor)
    {
        Valor = valor;
        Izquierda = null;
        Derecha = null;
    }
}

// Clase que implementa el Árbol Binario de Búsqueda
public class ArbolBinarioBusqueda
{
    private Nodo raiz;    // Raíz del árbol

    // Constructor del árbol
    public ArbolBinarioBusqueda()
    {
        raiz = null;
    }

    // Método público para insertar un valor
    public void Insertar(int valor)
    {
        raiz = InsertarRecursivo(raiz, valor);
    }

    // Método privado recursivo para insertar un valor
    private Nodo InsertarRecursivo(Nodo actual, int valor)
    {
        // Si el nodo actual es null, creamos un nuevo nodo
        if (actual == null)
        {
            return new Nodo(valor);
        }

        // Si el valor es menor, vamos al subárbol izquierdo
        if (valor < actual.Valor)
        {
            actual.Izquierda = InsertarRecursivo(actual.Izquierda, valor);
        }
        // Si el valor es mayor, vamos al subárbol derecho
        else if (valor > actual.Valor)
        {
            actual.Derecha = InsertarRecursivo(actual.Derecha, valor);
        }
        // Si el valor ya existe, no hacemos nada (BST no permite duplicados)

        return actual;
    }

    // Método público para buscar un valor
    public bool Buscar(int valor)
    {
        return BuscarRecursivo(raiz, valor);
    }

    // Método privado recursivo para buscar un valor
    private bool BuscarRecursivo(Nodo actual, int valor)
    {
        // Si el nodo es null o encontramos el valor, terminamos
        if (actual == null)
        {
            return false;
        }
        if (valor == actual.Valor)
        {
            return true;
        }

        // Buscamos en el subárbol izquierdo o derecho según el valor
        return valor < actual.Valor
            ? BuscarRecursivo(actual.Izquierda, valor)
            : BuscarRecursivo(actual.Derecha, valor);
    }

    // Método para recorrido en orden (Inorder)
    public void RecorridoEnOrden()
    {
        RecorridoEnOrdenRecursivo(raiz);
        Console.WriteLine(); // Nueva línea al final
    }

    // Método privado recursivo para recorrido en orden
    private void RecorridoEnOrdenRecursivo(Nodo actual)
    {
        if (actual != null)
        {
            // Primero visitamos el subárbol izquierdo
            RecorridoEnOrdenRecursivo(actual.Izquierda);
            // Luego imprimimos el valor del nodo actual
            Console.Write(actual.Valor + " ");
            // Finalmente visitamos el subárbol derecho
            RecorridoEnOrdenRecursivo(actual.Derecha);
        }
    }
}

// Clase principal para probar el BST
class Program
{
    static void Main(string[] args)
    {
        // Creamos una instancia del árbol
        ArbolBinarioBusqueda arbol = new ArbolBinarioBusqueda();

        // Insertamos algunos valores
        arbol.Insertar(50);
        arbol.Insertar(30);
        arbol.Insertar(70);
        arbol.Insertar(20);
        arbol.Insertar(40);
        arbol.Insertar(60);
        arbol.Insertar(80);

        // Mostramos el recorrido en orden (debe mostrar los valores ordenados)
        Console.WriteLine("Recorrido en orden del BST:");
        arbol.RecorridoEnOrden();  

        // Probamos la búsqueda
        Console.WriteLine("¿Está el 40 en el árbol? " + arbol.Buscar(40));  
        Console.WriteLine("¿Está el 90 en el árbol? " + arbol.Buscar(90));  
    }
}