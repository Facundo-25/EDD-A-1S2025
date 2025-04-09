using source.blockchain;
using System;

class Program
    {
        static void Main(string[] args)
        {
            // Crear una nueva instancia del Blockchain
            Blockchain blockchain = new Blockchain();


            // Agregar un usuario de ejemplo
            blockchain.AddBlock(1, "Carlos Alberto", "Gomez Martinez", "carlos.alberto@usac.com", 20, "CarlosMartinez");
            blockchain.AddBlock(2, "Maria"         , "Lopez"         , "maria.lopez@usac.com"   , 25, "MariaLopez");

            // Visualizar bloques
            Console.WriteLine("\nBloque 0:");
            blockchain.ViewBlock(0);
            Console.WriteLine("\nBloque 1:");
            blockchain.ViewBlock(1);


            // Generar y mostrar el código .dot
            Console.WriteLine("\nCódigo .dot para Graphviz:");
            Console.WriteLine("=============================");
            Console.WriteLine(blockchain.GenerateDot());
            Console.WriteLine("=============================");

            // Analizar la integridad de la cadena
            Console.WriteLine("\nAnálisis de la cadena:");
            blockchain.AnalyzeBlockchain();
        }
    }