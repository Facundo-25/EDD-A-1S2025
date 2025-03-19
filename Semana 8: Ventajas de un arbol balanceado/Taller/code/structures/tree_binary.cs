using System;
using System.IO;
using System.Text;



namespace code.structures.tree_binary
{

    public class Nodo_Servicio
    {
        public int Id { get; set; }
        public int Id_Repuesto { get; set; }
        public int Id_Vehiculo { get; set; }
        public string Detalles { get; set; }
        public double Costo { get; set; }
        public Nodo_Servicio Izquierda { get; set; }
        public Nodo_Servicio Derecha { get; set; }

        public Nodo_Servicio(int id, int idRepuesto, int idVehiculo, string detalles, double costo)
        {
            Id = id;
            Id_Repuesto = idRepuesto;
            Id_Vehiculo = idVehiculo;
            Detalles = detalles;
            Costo = costo;
            Izquierda = null;
            Derecha = null;
        }
    }



    public class ArbolBinario
    {


        private Nodo_Servicio raiz;



        public ArbolBinario()
        {
            raiz = null;
        }





        public void Insertar(int id, int idRepuesto, int idVehiculo, string detalles, double costo)
        {
            // Creamos un nuevo nodo con los parámetros recibidos
            Nodo_Servicio nuevo = new Nodo_Servicio(id, idRepuesto, idVehiculo, detalles, costo);

            // Si el árbol está vacío (raíz es null), el nuevo nodo se convierte en la raíz
            if (raiz == null)
            {
                raiz = nuevo;
            }
            else
            {
                // Si el árbol no está vacío, llamamos al método recursivo para insertar el nodo
                InsertarRecursivo(raiz, nuevo);
            }
        }

        // Método privado y recursivo que inserta el nodo en el lugar correcto del árbol
        private void InsertarRecursivo(Nodo_Servicio actual, Nodo_Servicio nuevo)
        {
            // Si el id del nuevo nodo es menor que el id del nodo actual, debe ir en el subárbol izquierdo
            if (nuevo.Id < actual.Id)
            {
                // Si no hay nodo en la izquierda, insertamos el nuevo nodo allí
                if (actual.Izquierda == null)
                {
                    actual.Izquierda = nuevo;
                }
                else
                {
                    // Si ya hay un nodo en la izquierda, llamamos recursivamente para insertar en el subárbol izquierdo
                    InsertarRecursivo(actual.Izquierda, nuevo);
                }
            }
            else
            {
                // Si el id del nuevo nodo es mayor o igual al del nodo actual, debe ir en el subárbol derecho
                if (actual.Derecha == null)
                {
                    actual.Derecha = nuevo;
                }
                else
                {
                    // Si ya hay un nodo en la derecha, llamamos recursivamente para insertar en el subárbol derecho
                    InsertarRecursivo(actual.Derecha, nuevo);
                }
            }
        }







        // Método para generar el archivo .dot para Graphviz
        public string GraficarGraphviz()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("digraph BST {");
            sb.AppendLine("    node [shape=rectangle];");
            if (raiz != null)
            {
                GenerarDotRecursivo(raiz, sb);
            }
            sb.AppendLine("}");
            return sb.ToString();
        }

        private void GenerarDotRecursivo(Nodo_Servicio nodo, StringBuilder sw)
        {
            if (nodo != null)
            {
                // Formato del nodo con toda la información
                string nodoLabel = $"\"{nodo.Id}\" [label=\"Id: {nodo.Id}\\nRepuesto: {nodo.Id_Repuesto}\\nVehiculo: {nodo.Id_Vehiculo}\\nDetalles: {nodo.Detalles}\\nCosto: {nodo.Costo}\"]";
                sw.AppendLine($"    {nodoLabel};");

                // Conexiones con hijos
                if (nodo.Izquierda != null)
                {
                    sw.AppendLine($"    \"{nodo.Id}\" -> \"{nodo.Izquierda.Id}\";");
                    GenerarDotRecursivo(nodo.Izquierda, sw);
                }
                if (nodo.Derecha != null)
                {
                    sw.AppendLine($"    \"{nodo.Id}\" -> \"{nodo.Derecha.Id}\";");
                    GenerarDotRecursivo(nodo.Derecha, sw);
                }
            }
        }




        // Recorridos 

        // Recorrido InOrden (izquierda, raíz, derecha)
        public List<Nodo_Servicio> TablaInOrden()
        {
            List<Nodo_Servicio> resultado = new List<Nodo_Servicio>();
            InOrdenRecursivo(raiz, resultado);
            return resultado;
        }

        private void InOrdenRecursivo(Nodo_Servicio nodo, List<Nodo_Servicio> resultado)
        {
            if (nodo != null)
            {
                InOrdenRecursivo(nodo.Izquierda, resultado);
                resultado.Add(nodo);
                InOrdenRecursivo(nodo.Derecha, resultado);
            }
        }

        // Recorrido PreOrden (raíz, izquierda, derecha)
        public List<Nodo_Servicio> TablaPreOrden()
        {
            List<Nodo_Servicio> resultado = new List<Nodo_Servicio>();
            PreOrdenRecursivo(raiz, resultado);
            return resultado;
        }

        private void PreOrdenRecursivo(Nodo_Servicio nodo, List<Nodo_Servicio> resultado)
        {
            if (nodo != null)
            {
                resultado.Add(nodo);
                PreOrdenRecursivo(nodo.Izquierda, resultado);
                PreOrdenRecursivo(nodo.Derecha, resultado);
            }
        }

        // Recorrido PostOrden (izquierda, derecha, raíz)
        public List<Nodo_Servicio> TablaPostOrden()
        {
            List<Nodo_Servicio> resultado = new List<Nodo_Servicio>();
            PostOrdenRecursivo(raiz, resultado);
            return resultado;
        }

        private void PostOrdenRecursivo(Nodo_Servicio nodo, List<Nodo_Servicio> resultado)
        {
            if (nodo != null)
            {
                PostOrdenRecursivo(nodo.Izquierda, resultado);
                PostOrdenRecursivo(nodo.Derecha, resultado);
                resultado.Add(nodo);
            }
        }


    }




}