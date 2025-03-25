using System;
using System.IO;
using System.Text;

namespace code.structures.tree_avl{

    public class Nodo_Repuesto
    {
        public int Id { get; set; }
        public string Repuesto { get; set; }
        public string Detalles { get; set; }
        public double Costo { get; set; }
        public Nodo_Repuesto Izquierda { get; set; }     // Nodo_Repuesto hijo izquierdo
        public Nodo_Repuesto Derecha { get; set; }       // Nodo_Repuesto hijo derecho
        public int Altura { get; set; }                  // Altura del nodo

        public Nodo_Repuesto(int id, string repuesto, string detalles, double costo)
        {
            Id = id;
            Repuesto = repuesto;
            Detalles = detalles;
            Costo = costo;
            Izquierda = null;
            Derecha = null;
            Altura = 1;
        }
    }



    public class ArbolAVL
    {

        private Nodo_Repuesto raiz;



        // Obtener altura de un nodo
        private int ObtenerAltura(Nodo_Repuesto nodo)
        {
            return nodo == null ? 0 : nodo.Altura;
        }



        // Obtener factor de balanceo
        private int ObtenerBalance(Nodo_Repuesto nodo)
        {
            return nodo == null ? 0 : ObtenerAltura(nodo.Izquierda) - ObtenerAltura(nodo.Derecha);
        }



        // Rotación derecha
        private Nodo_Repuesto RotacionDerecha(Nodo_Repuesto y)
        {
            // Paso 1: Guardamos el subárbol izquierdo de 'y' en 'x'
            Nodo_Repuesto x = y.Izquierda;

            // Paso 2: Guardamos el subárbol derecho de 'x' en 'T2', ya que lo vamos a reubicar
            Nodo_Repuesto T2 = x.Derecha;

            // Paso 3: Realizamos la rotación. 'x' se convierte en el nuevo nodo raíz del subárbol
            // 'y' pasa a ser el hijo derecho de 'x', y 'T2' pasa a ser el hijo izquierdo de 'y'
            x.Derecha = y;
            y.Izquierda = T2;

            // Paso 4: Actualizamos las alturas de los nodos después de la rotación
            // La altura de 'y' es 1 más el valor máximo entre las alturas de sus nuevos hijos
            y.Altura = Math.Max(ObtenerAltura(y.Izquierda), ObtenerAltura(y.Derecha)) + 1;
            
            // La altura de 'x' es 1 más el valor máximo entre las alturas de sus nuevos hijos
            x.Altura = Math.Max(ObtenerAltura(x.Izquierda), ObtenerAltura(x.Derecha)) + 1;

            // Paso 5: Retornamos 'x', que ahora es la nueva raíz del subárbol balanceado
            return x;
        }




        // Rotación izquierda
        private Nodo_Repuesto RotacionIzquierda(Nodo_Repuesto x)
        {
            // Paso 1: Guardamos el subárbol derecho de 'x' en 'y'
            Nodo_Repuesto y = x.Derecha;

            // Paso 2: Guardamos el subárbol izquierdo de 'y' en 'T2', ya que lo vamos a reubicar
            Nodo_Repuesto T2 = y.Izquierda;

            // Paso 3: Realizamos la rotación. 'y' se convierte en el nuevo nodo raíz del subárbol
            // 'x' pasa a ser el hijo izquierdo de 'y', y 'T2' pasa a ser el hijo derecho de 'x'
            y.Izquierda = x;
            x.Derecha = T2;

            // Paso 4: Actualizamos las alturas de los nodos después de la rotación
            // La altura de 'x' es 1 más el valor máximo entre las alturas de sus nuevos hijos
            x.Altura = Math.Max(ObtenerAltura(x.Izquierda), ObtenerAltura(x.Derecha)) + 1;

            // La altura de 'y' es 1 más el valor máximo entre las alturas de sus nuevos hijos
            y.Altura = Math.Max(ObtenerAltura(y.Izquierda), ObtenerAltura(y.Derecha)) + 1;

            // Paso 5: Retornamos 'y', que ahora es la nueva raíz del subárbol balanceado
            return y;
        }




        // Insertar 
        public void Insertar(int id, string repuesto, string detalles, double costo)
        {
            // Llamamos a la función recursiva para insertar el nodo y mantener el balance del árbol
            raiz = InsertarRecursivo(raiz, id, repuesto, detalles, costo);
        }

        private Nodo_Repuesto InsertarRecursivo(Nodo_Repuesto nodo, int id, string repuesto, string detalles, double costo)
        {
            // Si el nodo actual es nulo
            if (nodo == null)
                return new Nodo_Repuesto(id, repuesto, detalles, costo); // Creamos un nuevo nodo

            // Si el id es menor que el del nodo actual, insertamos en el subárbol izquierdo
            if (id < nodo.Id)
                nodo.Izquierda = InsertarRecursivo(nodo.Izquierda, id, repuesto, detalles, costo);
            // Si el id es mayor que el del nodo actual, insertamos en el subárbol derecho
            else if (id > nodo.Id)
                nodo.Derecha = InsertarRecursivo(nodo.Derecha, id, repuesto, detalles, costo);
            else
                return nodo; // Si el id ya existe en el árbol, no permitimos duplicados

            // Actualizamos la altura del nodo actual
            nodo.Altura = 1 + Math.Max(ObtenerAltura(nodo.Izquierda), ObtenerAltura(nodo.Derecha));

            // Obtenemos el factor de balanceo del nodo actual
            int balance = ObtenerBalance(nodo);

            // Casos de balanceo para mantener el árbol AVL balanceado

            // Caso 1: Rotación Izquierda-Izquierda (LL)
            // Si el factor de balanceo es mayor que 1 (indicando que el subárbol izquierdo está desequilibrado)
            // y el id a insertar es menor que el id del hijo izquierdo, hacemos una rotación derecha
            if (balance > 1 && id < nodo.Izquierda.Id)
                return RotacionDerecha(nodo);

            // Caso 2: Rotación Derecha-Derecha (RR)
            // Si el factor de balanceo es menor que -1 (indicando que el subárbol derecho está desequilibrado)
            // y el id a insertar es mayor que el id del hijo derecho, hacemos una rotación izquierda
            if (balance < -1 && id > nodo.Derecha.Id)
                return RotacionIzquierda(nodo);

            // Caso 3: Rotación Izquierda-Derecha (LR)
            // Si el factor de balanceo es mayor que 1 (subárbol izquierdo desequilibrado)
            // y el id a insertar es mayor que el id del hijo izquierdo, primero hacemos una rotación izquierda en el hijo izquierdo
            // y luego una rotación derecha en el nodo actual
            if (balance > 1 && id > nodo.Izquierda.Id)
            {
                nodo.Izquierda = RotacionIzquierda(nodo.Izquierda); // Rotación izquierda en el subárbol izquierdo
                return RotacionDerecha(nodo); // Rotación derecha en el nodo actual
            }

            // Caso 4: Rotación Derecha-Izquierda (RL)
            // Si el factor de balanceo es menor que -1 (subárbol derecho desequilibrado)
            // y el id a insertar es menor que el id del hijo derecho, primero hacemos una rotación derecha en el hijo derecho
            // y luego una rotación izquierda en el nodo actual
            if (balance < -1 && id < nodo.Derecha.Id)
            {
                nodo.Derecha = RotacionDerecha(nodo.Derecha); // Rotación derecha en el subárbol derecho
                return RotacionIzquierda(nodo); // Rotación izquierda en el nodo actual
            }

            // Si no se aplica ninguna de las rotaciones anteriores, retornamos el nodo sin cambios
            return nodo;
        }




        // Generar Graphviz
        public string  GraficarGraphviz()
        {
            StringBuilder dot = new StringBuilder();
            dot.AppendLine("digraph ArbolBinario {");
            dot.AppendLine("    node [shape=rectangle];");
            GenerarNodo_RepuestosGraphviz(raiz, dot);
            GenerarConexionesGraphviz(raiz, dot);
            dot.AppendLine("}");
            return dot.ToString();
        }

        private void GenerarNodo_RepuestosGraphviz(Nodo_Repuesto nodo, StringBuilder dot)
        {
            if (nodo == null) return;

            dot.AppendLine($"    \"{nodo.Id}\" [label=\"ID: {nodo.Id}\\nRepuesto: {nodo.Repuesto}\\nDetalles: {nodo.Detalles}\\nCosto: {nodo.Costo}\"];");
            GenerarNodo_RepuestosGraphviz(nodo.Izquierda, dot);
            GenerarNodo_RepuestosGraphviz(nodo.Derecha, dot);
        }

        private void GenerarConexionesGraphviz(Nodo_Repuesto nodo, StringBuilder dot)
        {
            if (nodo == null) return;

            if (nodo.Izquierda != null)
                dot.AppendLine($"    \"{nodo.Id}\" -> \"{nodo.Izquierda.Id}\";");
            if (nodo.Derecha != null)
                dot.AppendLine($"    \"{nodo.Id}\" -> \"{nodo.Derecha.Id}\";");

            GenerarConexionesGraphviz(nodo.Izquierda, dot);
            GenerarConexionesGraphviz(nodo.Derecha, dot);
        }


        


        // Recorridos

        // Recorrido InOrden (Izquierda-Raíz-Derecha)
        public Nodo_Repuesto[] TablaInOrden()
        {
            List<Nodo_Repuesto> resultado = new List<Nodo_Repuesto>();
            InOrdenRecursivo(raiz, resultado);
            return resultado.ToArray();
        }

        private void InOrdenRecursivo(Nodo_Repuesto nodo, List<Nodo_Repuesto> resultado)
        {
            if (nodo == null) return;
            
            InOrdenRecursivo(nodo.Izquierda, resultado);
            resultado.Add(nodo);
            InOrdenRecursivo(nodo.Derecha, resultado);
        }

        // Recorrido PreOrden (Raíz-Izquierda-Derecha)
        public Nodo_Repuesto[] TablaPreOrden()
        {
            List<Nodo_Repuesto> resultado = new List<Nodo_Repuesto>();
            PreOrdenRecursivo(raiz, resultado);
            return resultado.ToArray();
        }

        private void PreOrdenRecursivo(Nodo_Repuesto nodo, List<Nodo_Repuesto> resultado)
        {
            if (nodo == null) return;
            
            resultado.Add(nodo);
            PreOrdenRecursivo(nodo.Izquierda, resultado);
            PreOrdenRecursivo(nodo.Derecha, resultado);
        }

        // Recorrido PostOrden (Izquierda-Derecha-Raíz)
        public Nodo_Repuesto[] TablaPostOrden()
        {
            List<Nodo_Repuesto> resultado = new List<Nodo_Repuesto>();
            PostOrdenRecursivo(raiz, resultado);
            return resultado.ToArray();
        }

        private void PostOrdenRecursivo(Nodo_Repuesto nodo, List<Nodo_Repuesto> resultado)
        {
            if (nodo == null) return;
            
            PostOrdenRecursivo(nodo.Izquierda, resultado);
            PostOrdenRecursivo(nodo.Derecha, resultado);
            resultado.Add(nodo);
        }



    }

}