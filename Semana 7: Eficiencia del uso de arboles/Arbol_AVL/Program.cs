using System;

// Clase que representa un nodo del árbol AVL
public class NodoAVL
{
    public int Valor;         // Valor almacenado en el nodo
    public NodoAVL Izquierda; // Referencia al hijo izquierdo
    public NodoAVL Derecha;   // Referencia al hijo derecho
    public int Altura;        // Altura del nodo (para balanceo)

    // Constructor del nodo
    public NodoAVL(int valor)
    {
        Valor = valor;
        Izquierda = null;
        Derecha = null;
        Altura = 1; // Nuevo nodo tiene altura 1
    }
}

// Clase que implementa el Árbol AVL
public class ArbolAVL
{
    private NodoAVL raiz; // Raíz del árbol

    // Constructor del árbol
    public ArbolAVL()
    {
        raiz = null;
    }

    // Obtiene la altura de un nodo (0 si es null)
    private int ObtenerAltura(NodoAVL nodo)
    {
        return nodo == null ? 0 : nodo.Altura;
    }

    // Calcula el factor de balance de un nodo
    private int ObtenerFactorBalance(NodoAVL nodo)
    {
        return nodo == null ? 0 : ObtenerAltura(nodo.Izquierda) - ObtenerAltura(nodo.Derecha);
    }

    // Actualiza la altura de un nodo basado en sus hijos
    private void ActualizarAltura(NodoAVL nodo)
    {
        nodo.Altura = Math.Max(ObtenerAltura(nodo.Izquierda), ObtenerAltura(nodo.Derecha)) + 1;
    }

    // Rotación simple a la derecha
    private NodoAVL RotarDerecha(NodoAVL y)
    {
        NodoAVL x = y.Izquierda;
        NodoAVL T2 = x.Derecha;

        x.Derecha = y;
        y.Izquierda = T2;

        ActualizarAltura(y);
        ActualizarAltura(x);

        return x;
    }

    // Rotación simple a la izquierda
    private NodoAVL RotarIzquierda(NodoAVL x)
    {
        NodoAVL y = x.Derecha;
        NodoAVL T2 = y.Izquierda;

        y.Izquierda = x;
        x.Derecha = T2;

        ActualizarAltura(x);
        ActualizarAltura(y);

        return y;
    }

    // Método público para insertar un valor
    public void Insertar(int valor)
    {
        raiz = InsertarRecursivo(raiz, valor);
    }

    // Método privado recursivo para insertar y balancear
    private NodoAVL InsertarRecursivo(NodoAVL nodo, int valor)
    {
        // Inserción como en BST
        if (nodo == null)
        {
            return new NodoAVL(valor);
        }

        if (valor < nodo.Valor)
        {
            nodo.Izquierda = InsertarRecursivo(nodo.Izquierda, valor);
        }
        else if (valor > nodo.Valor)
        {
            nodo.Derecha = InsertarRecursivo(nodo.Derecha, valor);
        }
        else
        {
            return nodo; // No se permiten duplicados
        }

        // Actualizar altura del nodo actual
        ActualizarAltura(nodo);

        // Obtener factor de balance
        int balance = ObtenerFactorBalance(nodo);

        // Caso Izquierda-Izquierda
        if (balance > 1 && valor < nodo.Izquierda.Valor)
        {
            return RotarDerecha(nodo);
        }

        // Caso Derecha-Derecha
        if (balance < -1 && valor > nodo.Derecha.Valor)
        {
            return RotarIzquierda(nodo);
        }

        // Caso Izquierda-Derecha
        if (balance > 1 && valor > nodo.Izquierda.Valor)
        {
            nodo.Izquierda = RotarIzquierda(nodo.Izquierda);
            return RotarDerecha(nodo);
        }

        // Caso Derecha-Izquierda
        if (balance < -1 && valor < nodo.Derecha.Valor)
        {
            nodo.Derecha = RotarDerecha(nodo.Derecha);
            return RotarIzquierda(nodo);
        }

        return nodo;
    }

    // Recorrido en orden para verificar el árbol
    public void RecorridoEnOrden()
    {
        RecorridoEnOrdenRecursivo(raiz);
        Console.WriteLine();
    }

    private void RecorridoEnOrdenRecursivo(NodoAVL nodo)
    {
        if (nodo != null)
        {
            RecorridoEnOrdenRecursivo(nodo.Izquierda);
            Console.Write(nodo.Valor + " ");
            RecorridoEnOrdenRecursivo(nodo.Derecha);
        }
    }
}

// Clase principal para probar el Árbol AVL
class Program
{
    static void Main(string[] args)
    {
        ArbolAVL arbol = new ArbolAVL();

        // Insertamos valores que forzarán rotaciones
        arbol.Insertar(10);
        arbol.Insertar(20);
        arbol.Insertar(30); // Provoca un caso Derecha-Derecha
        arbol.Insertar(40);
        arbol.Insertar(50);
        arbol.Insertar(25);

        // Mostramos el recorrido en orden
        Console.WriteLine("Recorrido en orden del Árbol AVL:");
        arbol.RecorridoEnOrden(); // Salida esperada: 10 20 25 30 40 50
    }
}