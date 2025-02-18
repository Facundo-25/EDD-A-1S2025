using System;
using System.Runtime.InteropServices;

namespace Matriz
{
    public unsafe class MatrizDispersa<T> where T : unmanaged
    {
        public int capa; // Representa una capa adicional que puede ser utilizada en la visualización de la matriz.
        public ListaEncabezado<int> filas; // Lista de encabezados para las filas
        public ListaEncabezado<int> columnas; // Lista de encabezados para las columnas

        // Constructor de la clase MatrizDispersa
        public MatrizDispersa(int capa)
        {
            this.capa = capa; // Inicializa la capa
            filas = new ListaEncabezado<int>("Fila"); // Inicializa la lista de encabezados de filas
            columnas = new ListaEncabezado<int>("Columna"); // Inicializa la lista de encabezados de columnas
        }

        // Método para insertar un nuevo nodo en la matriz dispersa
        public void insert(int pos_x, int pos_y, string nombre)
        {
            // Creación de un nuevo nodo interno que será insertado en la matriz
            NodoInterno<int>* nuevo = (NodoInterno<int>*)Marshal.AllocHGlobal(sizeof(NodoInterno<int>));
            nuevo->id = 1; // Asigna un ID al nodo
            nuevo->nombre = nombre; // Asigna el nombre proporcionado al nodo
            nuevo->coordenadaX = pos_x; // Asigna la coordenada X (fila)
            nuevo->coordenadaY = pos_y; // Asigna la coordenada Y (columna)
            nuevo->arriba = null;
            nuevo->abajo = null;
            nuevo->derecha = null;
            nuevo->izquierda = null;

            // Verificar si ya existen los encabezados para la fila y columna en la matriz
            NodoEncabezado<int>* nodo_X = filas.getEncabezado(pos_x); // Obtener el encabezado de la fila
            NodoEncabezado<int>* nodo_Y = columnas.getEncabezado(pos_y); // Obtener el encabezado de la columna

            // Si el encabezado de la fila no existe, crearlo
            if (nodo_X == null)
            {
                filas.insertar_nodoEncabezado(pos_x); // Crear encabezado para la fila
                nodo_X = filas.getEncabezado(pos_x); // Obtener el encabezado de la fila recién creado
            }

            // Si el encabezado de la columna no existe, crearlo
            if (nodo_Y == null)
            {
                columnas.insertar_nodoEncabezado(pos_y); // Crear encabezado para la columna
                nodo_Y = columnas.getEncabezado(pos_y); // Obtener el encabezado de la columna recién creado
            }

            // Verificar que ambos encabezados hayan sido creados correctamente
            if (nodo_X == null || nodo_Y == null)
            {
                throw new InvalidOperationException("Error al crear los encabezados.");
            }

            // Insertar el nuevo nodo en la fila correspondiente
            if (nodo_X->acceso == null)
            {
                nodo_X->acceso = nuevo; // Si la fila está vacía, asignamos el nuevo nodo como el primer acceso
            }
            else
            {
                // Si ya hay nodos en la fila, buscamos el lugar adecuado para insertar el nuevo nodo
                NodoInterno<int>* tmp = nodo_X->acceso;
                while (tmp != null)
                {
                    // Si la columna del nuevo nodo es menor que la columna del nodo actual, insertamos el nuevo nodo antes
                    if (nuevo->coordenadaY < tmp->coordenadaY)
                    {
                        nuevo->derecha = tmp;
                        nuevo->izquierda = tmp->izquierda;
                        tmp->izquierda->derecha = nuevo;
                        tmp->izquierda = nuevo;
                        break;
                    }
                    else if (nuevo->coordenadaX == tmp->coordenadaX && nuevo->coordenadaY == tmp->coordenadaY)
                    {
                        // Verificar que no haya nodos duplicados (con las mismas coordenadas)
                        break;
                    }
                    else
                    {
                        // Si no hemos encontrado el lugar, seguimos buscando
                        if (tmp->derecha == null)
                        {
                            tmp->derecha = nuevo; // Insertamos el nodo al final de la fila
                            nuevo->izquierda = tmp;
                            break;
                        }
                        else
                        {
                            tmp = tmp->derecha; // Avanzamos al siguiente nodo en la fila
                        }
                    }
                }
            }

            // Insertar el nuevo nodo en la columna correspondiente
            if (nodo_Y->acceso == null)
            {
                nodo_Y->acceso = nuevo; // Si la columna está vacía, asignamos el nuevo nodo como el primer acceso
            }
            else
            {
                // Si ya hay nodos en la columna, buscamos el lugar adecuado para insertar el nuevo nodo
                NodoInterno<int>* tmp2 = nodo_Y->acceso;
                while (tmp2 != null)
                {
                    // Si la fila del nuevo nodo es menor que la fila del nodo actual, insertamos el nuevo nodo antes
                    if (nuevo->coordenadaX < tmp2->coordenadaX)
                    {
                        nuevo->abajo = tmp2;
                        nuevo->arriba = tmp2->arriba;
                        tmp2->arriba->abajo = nuevo;
                        tmp2->arriba = nuevo;
                        break;
                    }
                    else if (nuevo->coordenadaX == tmp2->coordenadaX && nuevo->coordenadaY == tmp2->coordenadaY)
                    {
                        // Verificar que no haya nodos duplicados (con las mismas coordenadas)
                        break;
                    }
                    else
                    {
                        // Si no hemos encontrado el lugar, seguimos buscando
                        if (tmp2->abajo == null)
                        {
                            tmp2->abajo = nuevo; // Insertamos el nodo al final de la columna
                            nuevo->arriba = tmp2;
                            break;
                        }
                        else
                        {
                            tmp2 = tmp2->abajo; // Avanzamos al siguiente nodo en la columna
                        }
                    }
                }
            }
        }

        // Método para mostrar la matriz dispersa
        public void mostrar()
        {
            // Imprimir los encabezados de columnas
            NodoEncabezado<int>* y_columna = columnas.primero;
            Console.Write("\t"); // Espacio inicial para alinear las columnas

            // Imprimir los IDs de las columnas
            while (y_columna != null)
            {
                Console.Write(y_columna->id + "\t"); // Imprimir el encabezado de cada columna
                y_columna = y_columna->siguiente;
            }
            Console.WriteLine(); // Salto de línea después de las cabeceras de las columnas

            // Imprimir los nodos de cada fila
            NodoEncabezado<int>* x_fila = filas.primero;
            while (x_fila != null)
            {
                // Imprimir el encabezado de la fila
                Console.Write(x_fila->id + "\t");

                // Imprimir los valores de la fila
                NodoInterno<int>* interno = x_fila->acceso;
                NodoEncabezado<int>* y_columna_iter = columnas.primero;

                // Imprimir los valores de las columnas de la fila
                while (y_columna_iter != null)
                {
                    if (interno != null && interno->coordenadaY == y_columna_iter->id)
                    {
                        Console.Write(interno->nombre + "\t"); // Si el nodo interno existe, mostrar su nombre
                        interno = interno->derecha; // Mover al siguiente nodo en la fila
                    }
                    else
                    {
                        Console.Write("0\t"); // Si no hay nodo, mostrar 0 (representa la ausencia de un valor en esa posición)
                    }
                    y_columna_iter = y_columna_iter->siguiente; // Avanzar a la siguiente columna
                }
                Console.WriteLine(); // Salto de línea después de imprimir una fila
                x_fila = x_fila->siguiente; // Avanzar a la siguiente fila
            }
        }

        // Destructor para liberar la memoria de los nodos internos y encabezados
        ~MatrizDispersa()
        {
            // Liberar memoria de los nodos internos y encabezados de filas
            NodoEncabezado<int>* x_fila = filas.primero;
            while (x_fila != null)
            {
                // Liberar los nodos internos de la fila
                NodoInterno<int>* interno = x_fila->acceso;
                while (interno != null)
                {
                    NodoInterno<int>* tmp = interno;
                    interno = interno->derecha;
                    if (tmp != null)
                    {
                        Marshal.FreeHGlobal((IntPtr)tmp);
                    }
                }

                // Liberar el encabezado de fila
                NodoEncabezado<int>* tmp_fila = x_fila;
                x_fila = x_fila->siguiente;
                if (tmp_fila != null)
                {
                    Marshal.FreeHGlobal((IntPtr)tmp_fila);
                }
            }

            // Liberar memoria de los nodos internos y encabezados de columnas
            NodoEncabezado<int>* x_columna = columnas.primero;
            while (x_columna != null)
            {
                // Liberar los nodos internos de la columna
                NodoInterno<int>* interno = x_columna->acceso;
                while (interno != null)
                {
                    NodoInterno<int>* tmp = interno;
                    interno = interno->abajo;
                    if (tmp != null)
                    {
                        Marshal.FreeHGlobal((IntPtr)tmp);
                    }
                }

                // Liberar el encabezado de columna
                NodoEncabezado<int>* tmp_columna = x_columna;
                x_columna = x_columna->siguiente;
                if (tmp_columna != null)
                {
                    Marshal.FreeHGlobal((IntPtr)tmp_columna);
                }
            }
        }
    }
}
