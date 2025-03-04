using System;
using System.Text;

class RowMajorExample
{
    private int[] vector; // Un solo vector para almacenar la matriz
    private int rows;
    private int cols;

    public RowMajorExample(int rows, int cols)
    {
        this.rows = rows;
        this.cols = cols;
        vector = new int[rows * cols]; // Tamaño del vector: filas * columnas
    }

    // Insertar un valor en la matriz
    public void Insert(int row, int col, int value)
    {
        if (row >= 0 && row < rows && col >= 0 && col < cols)
        {
            int index = row * cols + col; // Fórmula row-major
            vector[index] = value;
        }
        else
        {
            throw new IndexOutOfRangeException("Índices fuera de rango.");
        }
    }

    // Mostrar la matriz en formato de cuadrícula
    public void Mostrar()
    {
        Console.WriteLine("Matriz:");
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                int index = i * cols + j; // Fórmula row-major
                Console.Write($"[{i}.{j}] = {vector[index]} \t");
            }
            Console.WriteLine();
        }
    }

    // Graficar la matriz en formato .dot
    public string Graficar()
    {
        StringBuilder dot = new StringBuilder();
        dot.AppendLine("digraph G {");
        dot.AppendLine("node [shape=plaintext];");

        dot.AppendLine("struct [label=<<TABLE BORDER=\"1\" CELLBORDER=\"1\" CELLSPACING=\"0\">");

        for (int i = 0; i < rows; i++)
        {
            dot.AppendLine("<TR>");
            for (int j = 0; j < cols; j++)
            {
                int index = i * cols + j; // Fórmula row-major
                dot.AppendLine($"<TD>[{i}.{j}] = {vector[index]}</TD>");
            }
            dot.AppendLine("</TR>");
        }

        dot.AppendLine("</TABLE>>];");
        dot.AppendLine("}");

        return dot.ToString();
    }

    // Buscar un valor en la matriz y devolver su posición
    public (int row, int col) Buscar(int value)
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                int index = i * cols + j; // Fórmula row-major
                if (vector[index] == value)
                {
                    return (i, j);
                }
            }
        }
        return (-1, -1); // Valor no encontrado
    }

    // Mostrar el vector en memoria (row-major order)
    public void MostrarVectorMemoria()
    {
        Console.WriteLine("Vector en memoria (row-major order):");
        for (int i = 0; i < vector.Length; i++)
        {
            Console.Write($"{vector[i]} ");
        }
        Console.WriteLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        RowMajorExample example = new RowMajorExample(4, 4);

        // Insertar valores en la matriz
        example.Insert(0, 0, 10);
        example.Insert(0, 1, 20);
        example.Insert(0, 2, 30);
        example.Insert(0, 3, 40);
        example.Insert(1, 0, 50);
        example.Insert(1, 1, 60);
        example.Insert(1, 2, 70);
        example.Insert(1, 3, 80);
        example.Insert(2, 0, 90);
        example.Insert(2, 1, 100);
        example.Insert(2, 2, 110);
        example.Insert(2, 3, 120);
        example.Insert(3, 0, 130);
        example.Insert(3, 1, 140);
        example.Insert(3, 2, 150);
        example.Insert(3, 3, 160);

        // Mostrar la matriz en formato de cuadrícula
        example.Mostrar();

        // Mostrar el vector en memoria (row-major order)
        example.MostrarVectorMemoria();

        // Graficar la matriz en formato .dot
        string dot = example.Graficar();
        Console.WriteLine("Representación en formato .dot:");
        Console.WriteLine(dot);

        // Buscar un valor en la matriz
        int valueToFind = 70;
        var position = example.Buscar(valueToFind);
        if (position.row != -1 && position.col != -1)
        {
            Console.WriteLine($"El valor {valueToFind} se encuentra en la fila {position.row} y columna {position.col}.");
        }
        else
        {
            Console.WriteLine($"El valor {valueToFind} no se encuentra en la matriz.");
        }
    }
}