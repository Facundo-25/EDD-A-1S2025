using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace source.structures
{
    // Clase que representa un nodo en el árbol de Huffman
    public class HuffmanNode : IComparable<HuffmanNode>
    {
        // Carácter almacenado en el nodo (solo para nodos hoja)
        public char Character { get; set; }
        // Frecuencia del carácter o suma de frecuencias de los hijos
        public int Frequency { get; set; }
        // Hijo izquierdo del nodo en el árbol
        public HuffmanNode Left { get; set; }
        // Hijo derecho del nodo en el árbol
        public HuffmanNode Right { get; set; }

        // Compara nodos según su frecuencia para la cola de prioridad
        public int CompareTo(HuffmanNode other)
        {
            return this.Frequency - other.Frequency;
        }

        // Verifica si el nodo es una hoja 
        public bool IsLeaf()
        {
            return Left == null && Right == null;
        }
    }


    // Clase que implementa el algoritmo de compresión y descompresión de Huffman
    public class HuffmanCompressor
    {

        // Carpetas donde se encuentras los archivos
        private const string DataFolder = "reports";
        private const string ReportsFolder = "reports";



        // Comprime un archivo de texto
        public void Compress(string entityName)
        {
            // Construye las rutas de los archivos de entrada y salida
            string inputFile = Path.Combine(DataFolder, entityName + ".txt");
            string outputFile = Path.Combine(ReportsFolder, entityName + ".edd");

            // Verifica si el archivo de entrada existe
            if (!File.Exists(inputFile))
            {
                Console.WriteLine($"El archivo {inputFile} no existe.");
                return;
            }

            // Lee el texto del archivo de entrada
            string text = File.ReadAllText(inputFile);
            // Calcula las frecuencias de cada carácter
            Dictionary<char, int> frequencies = CalculateFrequencies(text);
            // Construye el árbol de Huffman
            HuffmanNode root = BuildHuffmanTree(frequencies);
            // Genera los códigos de Huffman para cada carácter
            Dictionary<char, string> huffmanCodes = GenerateCodes(root);
            // Guarda el archivo comprimido
            SaveCompressedFile(text, huffmanCodes, outputFile);

            Console.WriteLine($"Compresión completada. Archivo generado: {outputFile}");
        }



        // Descomprime un archivo comprimido 
        public string Decompress(string entityName)
        {
            // Construye las rutas de los archivos comprimido y de salida
            string compressedFile = Path.Combine(ReportsFolder, entityName + ".edd");
            string outputFile = Path.Combine(DataFolder, entityName + "_decompressed.txt");
            string inputTextFile = Path.Combine(DataFolder, entityName + ".txt");

            // Verifica si el archivo comprimido existe
            if (!File.Exists(compressedFile))
            {
                Console.WriteLine($"El archivo {compressedFile} no existe.");
                return "";
            }

            // Verifica si el archivo original existe para reconstruir el árbol
            if (!File.Exists(inputTextFile))
            {
                Console.WriteLine($"El archivo original {inputTextFile} no existe para reconstruir el árbol.");
                return "";
            }

            // Lee el texto original para obtener las frecuencias
            string originalText = File.ReadAllText(inputTextFile);
            // Calcula las frecuencias de los caracteres
            Dictionary<char, int> frequencies = CalculateFrequencies(originalText);
            // Reconstruye el árbol de Huffman
            HuffmanNode root = BuildHuffmanTree(frequencies);

            // Lee los bytes comprimidos
            byte[] compressedBytes = File.ReadAllBytes(compressedFile);
            // Descomprime el texto usando el árbol
            string decompressedText = DecompressText(compressedBytes, root);

            // Escribe el texto descomprimido en el archivo de salida
            File.WriteAllText(outputFile, decompressedText);
            Console.WriteLine($"Descompresión completada.");
            return decompressedText.ToString();
        }

        // Calcula la frecuencia de cada carácter en el texto
        private Dictionary<char, int> CalculateFrequencies(string text)
        {
            // Crea un diccionario para almacenar las frecuencias
            Dictionary<char, int> frequencies = new Dictionary<char, int>();

            // Recorre cada carácter del texto
            foreach (char c in text)
            {
                // Inicializa la frecuencia si el carácter no está en el diccionario
                if (!frequencies.ContainsKey(c))
                {
                    frequencies[c] = 0;
                }
                // Incrementa la frecuencia del carácter
                frequencies[c]++;
            }

            return frequencies;
        }

        // Construye el árbol de Huffman a partir de las frecuencias
        private HuffmanNode BuildHuffmanTree(Dictionary<char, int> frequencies)
        {
            // Crea una cola de prioridad para los nodos
            PriorityQueue<HuffmanNode> priorityQueue = new PriorityQueue<HuffmanNode>();

            // Crea un nodo por cada carácter y su frecuencia
            foreach (var symbol in frequencies)
            {
                priorityQueue.Enqueue(new HuffmanNode()
                {
                    Character = symbol.Key,
                    Frequency = symbol.Value,
                    Left = null,
                    Right = null
                });
            }

            // Combina nodos hasta que quede uno solo (la raíz del árbol)
            while (priorityQueue.Count > 1)
            {
                // Extrae los dos nodos con menor frecuencia
                HuffmanNode left = priorityQueue.Dequeue();
                HuffmanNode right = priorityQueue.Dequeue();

                // Crea un nodo padre con la suma de las frecuencias
                HuffmanNode parent = new HuffmanNode()
                {
                    Frequency = left.Frequency + right.Frequency,
                    Left = left,
                    Right = right
                };

                // Agrega el nodo padre a la cola
                priorityQueue.Enqueue(parent);
            }

            // Retorna la raíz del árbol
            return priorityQueue.Dequeue();
        }

        // Genera los códigos de Huffman para cada carácter
        private Dictionary<char, string> GenerateCodes(HuffmanNode root)
        {
            // Crea un diccionario para almacenar los códigos
            Dictionary<char, string> huffmanCodes = new Dictionary<char, string>();
            // Genera los códigos recursivamente
            GenerateCodesRecursive(root, "", huffmanCodes);
            return huffmanCodes;
        }

        // Genera los códigos de Huffman recursivamente
        private void GenerateCodesRecursive(HuffmanNode node, string code, Dictionary<char, string> huffmanCodes)
        {
            // Si el nodo es una hoja, asigna el código al carácter
            if (node.IsLeaf())
            {
                huffmanCodes[node.Character] = code.Length > 0 ? code : "0";
                return;
            }

            // Recorre el hijo izquierdo (agrega 0 al código)
            if (node.Left != null)
                GenerateCodesRecursive(node.Left, code + "0", huffmanCodes);
            // Recorre el hijo derecho (agrega 1 al código)
            if (node.Right != null)
                GenerateCodesRecursive(node.Right, code + "1", huffmanCodes);
        }

        // Guarda el texto comprimido en un archivo binario
        private void SaveCompressedFile(string text, Dictionary<char, string> huffmanCodes, string outputFile)
        {
            // Construye el texto codificado (cadena de bits)
            StringBuilder encodedText = new StringBuilder();

            // Convierte cada carácter a su código de Huffman
            foreach (char c in text)
            {
                encodedText.Append(huffmanCodes[c]);
            }

            // Calcula el número de bytes necesarios
            int numOfBytes = (encodedText.Length + 7) / 8;
            byte[] bytes = new byte[numOfBytes];
            int byteIndex = 0, bitIndex = 0;

            // Convierte la cadena de bits a bytes
            for (int i = 0; i < encodedText.Length; i++)
            {
                if (encodedText[i] == '1')
                {
                    bytes[byteIndex] |= (byte)(1 << (7 - bitIndex));
                }

                bitIndex++;
                if (bitIndex == 8)
                {
                    byteIndex++;
                    bitIndex = 0;
                }
            }

            // Escribe los bytes en el archivo de salida
            File.WriteAllBytes(outputFile, bytes);
        }

        // Descomprime los bytes comprimidos usando el árbol de Huffman
        private string DecompressText(byte[] compressedBytes, HuffmanNode root)
        {
            // Convierte los bytes a una cadena de bits
            StringBuilder bitString = new StringBuilder();

            foreach (byte b in compressedBytes)
            {
                for (int i = 7; i >= 0; i--)
                {
                    bitString.Append((b & (1 << i)) != 0 ? "1" : "0");
                }
            }

            // Reconstruye el texto original
            StringBuilder decompressedText = new StringBuilder();
            HuffmanNode currentNode = root;

            foreach (char bit in bitString.ToString())
            {
                // Navega por el árbol según el bit
                if (bit == '0')
                {
                    currentNode = currentNode.Left;
                }
                else
                {
                    currentNode = currentNode.Right;
                }

                // Si se llega a una hoja, agrega el carácter y reinicia
                if (currentNode.IsLeaf())
                {
                    decompressedText.Append(currentNode.Character);
                    currentNode = root;
                }
            }

            return decompressedText.ToString();
        }
    }

    // Implementación de una cola de prioridad para ordenar nodos por frecuencia
    public class PriorityQueue<T> where T : IComparable<T>
    {
        // Lista interna para almacenar los elementos
        private List<T> data;

        // Constructor
        public PriorityQueue()
        {
            this.data = new List<T>();
        }

        // Agrega un elemento a la cola manteniendo el orden
        public void Enqueue(T item)
        {
            data.Add(item);
            int ci = data.Count - 1;
            // Reorganiza el montón hacia arriba
            while (ci > 0)
            {
                int pi = (ci - 1) / 2;
                if (data[ci].CompareTo(data[pi]) >= 0)
                    break;
                T tmp = data[ci]; data[ci] = data[pi]; data[pi] = tmp;
                ci = pi;
            }
        }

        // Extrae el elemento con menor valor
        public T Dequeue()
        {
            int li = data.Count - 1;
            T frontItem = data[0];
            data[0] = data[li];
            data.RemoveAt(li);

            --li;
            int pi = 0;
            // Reorganiza el montón hacia abajo
            while (true)
            {
                int ci = pi * 2 + 1;
                if (ci > li) break;
                int rc = ci + 1;
                if (rc <= li && data[rc].CompareTo(data[ci]) < 0)
                    ci = rc;
                if (data[pi].CompareTo(data[ci]) <= 0) break;
                T tmp = data[pi]; data[pi] = data[ci]; data[ci] = tmp;
                pi = ci;
            }
            return frontItem;
        }

        // Obtiene la cantidad de elementos en la cola
        public int Count
        {
            get { return data.Count; }
        } 
    }

}