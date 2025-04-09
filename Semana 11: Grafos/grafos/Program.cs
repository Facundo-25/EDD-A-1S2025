    using source.graph;
    
    class Program
    {
        static void Main(string[] args)
        {
            GrafoNoDirigido graph = new GrafoNoDirigido();

            // Insertar relaciones vehículo-repuesto
            graph.Insertar("V1", "R1");
            graph.Insertar("V1", "R2");
            graph.Insertar("V2", "R2");
            graph.Insertar("V3", "R3");

            // Generar y mostrar el código DOT
            string dotCode = graph.GenerarDot();
            Console.WriteLine(dotCode);
        }
    }