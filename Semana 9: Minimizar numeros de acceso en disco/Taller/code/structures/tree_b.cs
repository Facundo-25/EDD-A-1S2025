using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace code.structures.tree_b{

    public class Factura
    {
        public int Id { get; set; }
        public int Id_Servicio { get; set; }
        public double Total { get; set; }

        public Factura(int id, int id_servicio, double total)
        {
            Id = id;
            Id_Servicio = id_servicio;
            Total = total;
        }
    }

    // Clase que representa un nodo en el árbol B
    public class NodoArbolB
    {
        private const int ORDEN = 5;
        private const int MAX_CLAVES = ORDEN - 1;
        private const int MIN_CLAVES = (ORDEN / 2) - 1;
        
        public List<Factura> Claves { get; set; }
        public List<NodoArbolB> Hijos { get; set; }
        public bool EsHoja { get; set; }

        public NodoArbolB()
        {
            Claves = new List<Factura>(MAX_CLAVES);
            Hijos = new List<NodoArbolB>(ORDEN);
            EsHoja = true;
        }

        // Verifica si el nodo está lleno
        public bool EstaLleno()
        {
            return Claves.Count >= MAX_CLAVES;
        }

        // Verifica si el nodo tiene el mínimo de claves requerido
        public bool TieneMinimoClaves()
        {
            return Claves.Count >= MIN_CLAVES;
        }
    }

    public class ArbolB
    {
        private NodoArbolB raiz;
        private const int ORDEN = 5;
        private const int MAX_CLAVES = ORDEN - 1;
        private const int MIN_CLAVES = (ORDEN / 2) - 1;

        public ArbolB()
        {
            raiz = new NodoArbolB();
        }

        // Método para insertar un nuevo Factura
        public void Insertar(int id, int id_servicio, double total)
        {
            Factura nuevoFactura = new Factura(id,id_servicio,total);
            
            // Si la raíz está llena, se crea una nueva raíz
            if (raiz.EstaLleno())
            {
                NodoArbolB nuevaRaiz = new NodoArbolB();
                nuevaRaiz.EsHoja = false;
                nuevaRaiz.Hijos.Add(raiz);
                DividirHijo(nuevaRaiz, 0);
                raiz = nuevaRaiz;
            }
            
            InsertarNoLleno(raiz, nuevoFactura);
        }

        // Divide un hijo cuando está lleno durante la inserción
        private void DividirHijo(NodoArbolB padre, int indiceHijo)
        {
            NodoArbolB hijoCompleto = padre.Hijos[indiceHijo];
            NodoArbolB nuevoHijo = new NodoArbolB();
            nuevoHijo.EsHoja = hijoCompleto.EsHoja;

            // Factura del medio que se promoverá al padre
            Factura FacturaMedio = hijoCompleto.Claves[MIN_CLAVES];

            // Mover la mitad de las claves al nuevo hijo
            for (int i = MIN_CLAVES + 1; i < MAX_CLAVES; i++)
            {
                nuevoHijo.Claves.Add(hijoCompleto.Claves[i]);
            }

            // Si no es hoja, mover también los hijos correspondientes
            if (!hijoCompleto.EsHoja)
            {
                for (int i = (ORDEN / 2); i < ORDEN; i++)
                {
                    nuevoHijo.Hijos.Add(hijoCompleto.Hijos[i]);
                }
                hijoCompleto.Hijos.RemoveRange((ORDEN / 2), hijoCompleto.Hijos.Count - (ORDEN / 2));
            }

            // Eliminar las claves movidas del hijo original
            hijoCompleto.Claves.RemoveRange(MIN_CLAVES, hijoCompleto.Claves.Count - MIN_CLAVES);

            // Insertar el nuevo hijo en el padre
            padre.Hijos.Insert(indiceHijo + 1, nuevoHijo);

            // Insertar la clave media en el padre
            int j = 0;
            while (j < padre.Claves.Count && padre.Claves[j].Id < FacturaMedio.Id)
            {
                j++;
            }
            padre.Claves.Insert(j, FacturaMedio);
        }

        // Inserta un Factura en un nodo que no está lleno
        private void InsertarNoLleno(NodoArbolB nodo, Factura Factura)
        {
            int i = nodo.Claves.Count - 1;

            // Si es hoja, simplemente inserta el Factura en orden
            if (nodo.EsHoja)
            {
                // Buscar la posición correcta para insertar
                while (i >= 0 && Factura.Id < nodo.Claves[i].Id)
                {
                    i--;
                }
                nodo.Claves.Insert(i + 1, Factura);
            }
            else
            {
                // Encuentra el hijo donde debe estar el Factura
                while (i >= 0 && Factura.Id < nodo.Claves[i].Id)
                {
                    i--;
                }
                i++;

                // Si el hijo está lleno, divídelo primero
                if (nodo.Hijos[i].EstaLleno())
                {
                    DividirHijo(nodo, i);
                    if (Factura.Id > nodo.Claves[i].Id)
                    {
                        i++;
                    }
                }
                InsertarNoLleno(nodo.Hijos[i], Factura);
            }
        }

        // Busca un Factura por su Id
        public Factura Buscar(int id)
        {
            return BuscarRecursivo(raiz, id);
        }

        private Factura BuscarRecursivo(NodoArbolB nodo, int id)
        {
            int i = 0;
            // Buscar la primera clave mayor o igual que id
            while (i < nodo.Claves.Count && id > nodo.Claves[i].Id)
            {
                i++;
            }

            // Si encontramos el id, devolvemos el Factura
            if (i < nodo.Claves.Count && id == nodo.Claves[i].Id)
            {
                return nodo.Claves[i];
            }

            // Si es una hoja y no encontramos el id, no existe
            if (nodo.EsHoja)
            {
                return null;
            }

            // Si no es hoja, buscamos en el hijo correspondiente
            return BuscarRecursivo(nodo.Hijos[i], id);
        }

        // Método para eliminar un Factura por su Id
        public void Eliminar(int id)
        {
            EliminarRecursivo(raiz, id);
            
            // Si la raíz quedó vacía pero tiene hijos, el primer hijo se convierte en la nueva raíz
            if (raiz.Claves.Count == 0 && !raiz.EsHoja)
            {
                NodoArbolB antiguaRaiz = raiz;
                raiz = raiz.Hijos[0];
            }
        }

        private void EliminarRecursivo(NodoArbolB nodo, int id)
        {
            int indice = EncontrarIndice(nodo, id);

            // Caso 1: La clave está en este nodo
            if (indice < nodo.Claves.Count && nodo.Claves[indice].Id == id)
            {
                // Si es hoja, simplemente eliminamos
                if (nodo.EsHoja)
                {
                    nodo.Claves.RemoveAt(indice);
                }
                else
                {
                    // Si no es hoja, usamos estrategias más complejas
                    EliminarDeNodoInterno(nodo, indice);
                }
            }
            else
            {
                // Caso 2: La clave no está en este nodo
                if (nodo.EsHoja)
                {
                    Console.WriteLine($"El Factura con Id {id} no existe en el árbol");
                    return;
                }

                // Determinar si el último hijo fue visitado
                bool ultimoHijo = (indice == nodo.Claves.Count);

                // Si el hijo tiene el mínimo de claves, rellenarlo
                if (!nodo.Hijos[indice].TieneMinimoClaves())
                {
                    RellenarHijo(nodo, indice);
                }

                // Si el último hijo se fusionó, recurrimos al hijo anterior
                if (ultimoHijo && indice > nodo.Hijos.Count - 1)
                {
                    EliminarRecursivo(nodo.Hijos[indice - 1], id);
                }
                else
                {
                    EliminarRecursivo(nodo.Hijos[indice], id);
                }
            }
        }

        // Encuentra el índice de la primera clave mayor o igual a id
        private int EncontrarIndice(NodoArbolB nodo, int id)
        {
            int indice = 0;
            while (indice < nodo.Claves.Count && nodo.Claves[indice].Id < id)
            {
                indice++;
            }
            return indice;
        }

        // Elimina un Factura de un nodo interno
        private void EliminarDeNodoInterno(NodoArbolB nodo, int indice)
        {
            Factura clave = nodo.Claves[indice];

            // Caso 2a: Si el hijo anterior tiene más del mínimo de claves
            if (nodo.Hijos[indice].Claves.Count > MIN_CLAVES)
            {
                // Reemplazar clave con el predecesor
                Factura predecesor = ObtenerPredecesor(nodo, indice);
                nodo.Claves[indice] = predecesor;
                EliminarRecursivo(nodo.Hijos[indice], predecesor.Id);
            }
            // Caso 2b: Si el hijo siguiente tiene más del mínimo de claves
            else if (nodo.Hijos[indice + 1].Claves.Count > MIN_CLAVES)
            {
                // Reemplazar clave con el sucesor
                Factura sucesor = ObtenerSucesor(nodo, indice);
                nodo.Claves[indice] = sucesor;
                EliminarRecursivo(nodo.Hijos[indice + 1], sucesor.Id);
            }
            // Caso 2c: Si ambos hijos tienen el mínimo de claves
            else
            {
                // Fusionar el hijo actual con el siguiente
                FusionarNodos(nodo, indice);
                EliminarRecursivo(nodo.Hijos[indice], clave.Id);
            }
        }

        // Obtiene el predecesor de una clave (la clave más grande en el subárbol izquierdo)
        private Factura ObtenerPredecesor(NodoArbolB nodo, int indice)
        {
            NodoArbolB actual = nodo.Hijos[indice];
            while (!actual.EsHoja)
            {
                actual = actual.Hijos[actual.Claves.Count];
            }
            return actual.Claves[actual.Claves.Count - 1];
        }

        // Obtiene el sucesor de una clave (la clave más pequeña en el subárbol derecho)
        private Factura ObtenerSucesor(NodoArbolB nodo, int indice)
        {
            NodoArbolB actual = nodo.Hijos[indice + 1];
            while (!actual.EsHoja)
            {
                actual = actual.Hijos[0];
            }
            return actual.Claves[0];
        }

        // Rellena un hijo que tiene menos del mínimo de claves
        private void RellenarHijo(NodoArbolB nodo, int indice)
        {
            // Si el hermano izquierdo existe y tiene más del mínimo de claves
            if (indice > 0 && nodo.Hijos[indice - 1].Claves.Count > MIN_CLAVES)
            {
                TomaPrestadoDelAnterior(nodo, indice);
            }
            // Si el hermano derecho existe y tiene más del mínimo de claves
            else if (indice < nodo.Claves.Count && nodo.Hijos[indice + 1].Claves.Count > MIN_CLAVES)
            {
                TomaPrestadoDelSiguiente(nodo, indice);
            }
            // Si no se puede tomar prestado, fusionar con un hermano
            else
            {
                if (indice < nodo.Claves.Count)
                {
                    FusionarNodos(nodo, indice);
                }
                else
                {
                    FusionarNodos(nodo, indice - 1);
                }
            }
        }

        // Toma prestado una clave del hermano anterior
        private void TomaPrestadoDelAnterior(NodoArbolB nodo, int indice)
        {
            NodoArbolB hijo = nodo.Hijos[indice];
            NodoArbolB hermano = nodo.Hijos[indice - 1];

            // Desplazar todas las claves e hijos para hacer espacio para la nueva clave
            hijo.Claves.Insert(0, nodo.Claves[indice - 1]);

            // Si no es hoja, mover también el hijo correspondiente
            if (!hijo.EsHoja)
            {
                hijo.Hijos.Insert(0, hermano.Hijos[hermano.Claves.Count]);
                hermano.Hijos.RemoveAt(hermano.Claves.Count);
            }

            // Actualizar la clave del padre
            nodo.Claves[indice - 1] = hermano.Claves[hermano.Claves.Count - 1];
            hermano.Claves.RemoveAt(hermano.Claves.Count - 1);
        }

        // Toma prestado una clave del hermano siguiente
        private void TomaPrestadoDelSiguiente(NodoArbolB nodo, int indice)
        {
            NodoArbolB hijo = nodo.Hijos[indice];
            NodoArbolB hermano = nodo.Hijos[indice + 1];

            // Añadir la clave del padre al hijo
            hijo.Claves.Add(nodo.Claves[indice]);

            // Si no es hoja, mover también el hijo correspondiente
            if (!hijo.EsHoja)
            {
                hijo.Hijos.Add(hermano.Hijos[0]);
                hermano.Hijos.RemoveAt(0);
            }

            // Actualizar la clave del padre
            nodo.Claves[indice] = hermano.Claves[0];
            hermano.Claves.RemoveAt(0);
        }

        // Fusiona dos nodos hijo
        private void FusionarNodos(NodoArbolB nodo, int indice)
        {
            NodoArbolB hijo = nodo.Hijos[indice];
            NodoArbolB hermano = nodo.Hijos[indice + 1];

            // Añadir la clave del padre al hijo
            hijo.Claves.Add(nodo.Claves[indice]);

            // Añadir todas las claves del hermano al hijo
            for (int i = 0; i < hermano.Claves.Count; i++)
            {
                hijo.Claves.Add(hermano.Claves[i]);
            }

            // Si no es hoja, mover también los hijos
            if (!hijo.EsHoja)
            {
                for (int i = 0; i < hermano.Hijos.Count; i++)
                {
                    hijo.Hijos.Add(hermano.Hijos[i]);
                }
            }

            // Remover la clave y el hijo del nodo padre
            nodo.Claves.RemoveAt(indice);
            nodo.Hijos.RemoveAt(indice + 1);
        }

        // Recorrido InOrden del árbol
        public List<Factura> RecorridoInOrden()
        {
            List<Factura> resultado = new List<Factura>();
            RecorridoInOrdenRecursivo(raiz, resultado);
            return resultado;
        }

        private void RecorridoInOrdenRecursivo(NodoArbolB nodo, List<Factura> resultado)
        {
            if (nodo == null)
                return;

            int i;
            for (i = 0; i < nodo.Claves.Count; i++)
            {
                // Recorrer el hijo izquierdo
                if (!nodo.EsHoja)
                    RecorridoInOrdenRecursivo(nodo.Hijos[i], resultado);

                // Agregar la clave actual
                resultado.Add(nodo.Claves[i]);
            }

            // Recorrer el último hijo
            if (!nodo.EsHoja)
                RecorridoInOrdenRecursivo(nodo.Hijos[i], resultado);
        }

        // Método para generar el archivo .dot para Graphviz
        public string GraficarGraphviz()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("digraph BTree {");
            sb.AppendLine("    node [shape=record];");
            
            int contadorNodos = 0;
            GraficarGraphvizRecursivo(raiz, sb, ref contadorNodos);
            
            sb.AppendLine("}");
            return sb.ToString();
        }

        private void GraficarGraphvizRecursivo(NodoArbolB nodo, StringBuilder sb, ref int contadorNodos)
        {
            if (nodo == null)
                return;

            int nodoActual = contadorNodos++;
            
            // Construir la etiqueta del nodo
            StringBuilder etiquetaNodo = new StringBuilder();
            etiquetaNodo.Append($"node{nodoActual} [label=\"");
            
            for (int i = 0; i < nodo.Claves.Count; i++)
            {
                if (i > 0)
                    etiquetaNodo.Append("|");
                etiquetaNodo.Append($"<f{i}> |Id: {nodo.Claves[i].Id}, Total: {nodo.Claves[i].Total}|");
            }
            
            // Añadir un puerto más para el último hijo
            if (nodo.Claves.Count > 0)
                etiquetaNodo.Append($"<f{nodo.Claves.Count}>");
            
            etiquetaNodo.Append("\"];");
            sb.AppendLine(etiquetaNodo.ToString());
            
            // Graficar los hijos y sus conexiones
            if (!nodo.EsHoja)
            {
                for (int i = 0; i <= nodo.Claves.Count; i++)
                {
                    int hijoPosicion = contadorNodos;
                    GraficarGraphvizRecursivo(nodo.Hijos[i], sb, ref contadorNodos);
                    sb.AppendLine($"    node{nodoActual}:f{i} -> node{hijoPosicion};");
                }
            }
        }


        public List<Factura> ObtenerFacturasPorServicios(List<int> idsServicios)
        {
            List<Factura> resultado = new List<Factura>();
            ObtenerFacturasPorServiciosRecursivo(raiz, idsServicios, resultado);
            return resultado;
        }

        private void ObtenerFacturasPorServiciosRecursivo(NodoArbolB nodo, List<int> idsServicios, List<Factura> resultado)
        {
            if (nodo == null) return;

            // Revisar todas las claves del nodo
            foreach (var factura in nodo.Claves)
            {
                if (idsServicios.Contains(factura.Id_Servicio))
                {
                    resultado.Add(factura);
                }
            }

            // Recorrer hijos si no es hoja
            if (!nodo.EsHoja)
            {
                foreach (var hijo in nodo.Hijos)
                {
                    ObtenerFacturasPorServiciosRecursivo(hijo, idsServicios, resultado);
                }
            }
        }


        
    }

}