using System;


namespace code.structures.double_list{

    public class Nodo_Vehiculos
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string Marca { get; set; }
        public int Modelo { get; set; }
        public string Placa { get; set; }
        public Nodo_Vehiculos Siguiente { get; set; }
        public Nodo_Vehiculos Anterior { get; set; }

        public Nodo_Vehiculos(int id, int idUsuario, string marca, int modelo, string placa)
        {
            Id = id;
            IdUsuario = idUsuario;
            Marca = marca;
            Modelo = modelo;
            Placa = placa;
            Siguiente = null;
            Anterior = null;
        }
    }

    public class ListaDoble
    {

        
        private Nodo_Vehiculos cabeza;
        private Nodo_Vehiculos cola;



        public ListaDoble()
        {
            cabeza = null;
            cola = null;
        }




        // Método para insertar un nuevo vehículo
        public void Insertar(int id, int idUsuario, string marca, int modelo, string placa)
        {
            Nodo_Vehiculos nuevoNodo_Vehiculos = new Nodo_Vehiculos(id, idUsuario, marca, modelo, placa);

            // Si la lista está vacía
            if (cabeza == null)
            {
                cabeza = nuevoNodo_Vehiculos;
                cola = nuevoNodo_Vehiculos;
            }
            else
            {
                // Insertar al final
                nuevoNodo_Vehiculos.Anterior = cola;
                cola.Siguiente = nuevoNodo_Vehiculos;
                cola = nuevoNodo_Vehiculos;
            }
        }




        // Método para verificar si existe un vehículo por ID
        public bool ExisteVehiculo(int id)
        {
            Nodo_Vehiculos actual = cabeza;
            while (actual != null)
            {
                if (actual.Id == id)
                {
                    return true;
                }
                actual = actual.Siguiente;
            }
            return false;
        }




        // Método para generar código Graphviz
        public string GraficarGraphviz()
        {
            if (cabeza == null)
            {
                return "digraph G {label=\"Lista vacía\";}";
            }

            string dot = "digraph G {\n" +
                        "rankdir=LR;\n" +
                        "node [shape=rectangle];\n";

            // Crear nodos
            Nodo_Vehiculos actual = cabeza;
            while (actual != null)
            {
                dot += $"n{actual.Id} [label=\"ID: {actual.Id}\\nUsuario: {actual.IdUsuario}\\nMarca: {actual.Marca}\\nModelo: {actual.Modelo}\\nPlaca: {actual.Placa}\"];\n";
                actual = actual.Siguiente;
            }

            // Crear conexiones hacia adelante
            actual = cabeza;
            while (actual != null && actual.Siguiente != null)
            {
                dot += $"n{actual.Id} -> n{actual.Siguiente.Id};\n";
                actual = actual.Siguiente;
            }

            // Crear conexiones hacia atrás
            actual = cabeza;
            while (actual != null && actual.Siguiente != null)
            {
                dot += $"n{actual.Siguiente.Id} -> n{actual.Id};\n";
                actual = actual.Siguiente;
            }

            dot += "}";
            return dot;
        }
    }
}