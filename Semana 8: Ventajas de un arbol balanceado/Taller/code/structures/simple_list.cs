using System;


namespace code.structures.simple_list{

    public class Nodo_Usuario
    {
        public int Id;
        public string Nombres;
        public string Apellidos;
        public string Correo;
        public int Edad;
        public string Contrasenia;
        public Nodo_Usuario? Siguiente;  

        public Nodo_Usuario(int id, string nombres, string apellidos, string correo, int edad, string contrasenia)
        {
            Id = id;
            Nombres = nombres;
            Apellidos = apellidos;
            Correo = correo;
            Edad = edad;
            Contrasenia = contrasenia;
            Siguiente = null;
        }
    }


    public class ListaEnlazada
    {



        private Nodo_Usuario? cabeza = null; 



        public void Agregar(int id, string nombres, string apellidos, string correo, int edad, string contrasenia)
        {
            Nodo_Usuario nuevo = new Nodo_Usuario(id, nombres, apellidos, correo, edad, contrasenia);
            if (cabeza == null)
            {
                cabeza = nuevo;
            }
            else
            {
                Nodo_Usuario actual = cabeza;
                while (actual.Siguiente != null)
                {
                    actual = actual.Siguiente;
                }
                actual.Siguiente = nuevo;
            }
        }



        public int GetSize()
        {
            int size = 0;
            Nodo_Usuario? actual = cabeza;

            while (actual != null)
            {
                size++;
                actual = actual.Siguiente;
            }

            return size;
        }



        public Nodo_Usuario? BuscarPorCorreo(string correo)
        {
            Nodo_Usuario? actual = cabeza; 

            while (actual != null)
            {
    
                if (actual.Correo.Equals(correo, StringComparison.OrdinalIgnoreCase))
                {
                    return actual;  
                }
                
                actual = actual.Siguiente; 
            }

            return null; 
        }

        

        public bool ValidarContrasenia(string correo, string contrasenia)
        {
            Nodo_Usuario? usuario = BuscarPorCorreo(correo);
            if (usuario == null)
            {
                return false;
            }

            return usuario.Contrasenia.Equals(contrasenia);
        }



        public void Imprimir()
        {
            if (cabeza == null)
            {
                Console.WriteLine("La lista está vacía");
                return;
            }
            
            Nodo_Usuario? actual = cabeza;
            while (actual != null)
            {
                Console.WriteLine($"ID: {actual.Id}, Nombre: {actual.Nombres} {actual.Apellidos}, Edad: {actual.Edad}");
                actual = actual.Siguiente;
            }
        }




        public string GraficarGraphviz()
        {
            if (cabeza == null)
            {
                return "digraph G {\n    node [shape=record];\n    NULL [label = \"{NULL}\"];\n}\n";
            }

            // Iniciamos el código Graphviz
            var graphviz = "digraph G {\n";
            graphviz += "    node [shape=record];\n";
            graphviz += "    rankdir=LR;\n";
            graphviz += "    subgraph cluster_0 {\n";
            graphviz += "        label = \"Lista Simple\";\n";

            // Iterar sobre los nodos de la lista y construir la representación Graphviz
            Nodo_Usuario? actual = cabeza;
            int index = 0;

            while (actual != null)
            {
                graphviz += $"        n{index} [label = \"{{<data> ID: {actual.Id} \\n Nombre: {actual.Nombres} {actual.Apellidos} \\n Correo: {actual.Correo} \\n Edad: {actual.Edad} \\n Siguiente: }}\"];\n";
                actual = actual.Siguiente;
                index++;
            }

            // Conectar los nodos
            actual = cabeza;
            for (int i = 0; actual != null && actual.Siguiente != null; i++)
            {
                graphviz += $"        n{i} -> n{i + 1};\n";
                actual = actual.Siguiente;
            }

            graphviz += "    }\n";
            graphviz += "}\n";
            return graphviz;
        }


        
    }

}