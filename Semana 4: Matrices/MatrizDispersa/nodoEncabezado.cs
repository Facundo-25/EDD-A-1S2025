using System;

namespace Matriz
{
    // Estructura genérica NodoEncabezado para manejar enlaces en una lista o matriz
    public unsafe struct NodoEncabezado<T> where T : unmanaged
    {
        public T id; // Identificador del nodo, de tipo genérico T.
        
        // Punteros para enlazar nodos en ambas direcciones (siguiente y anterior)
        public NodoEncabezado<T>* siguiente;
        public NodoEncabezado<T>* anterior; 
        
        // Puntero a un NodoInterno, usado para acceder a una ubicación específica de la matriz
        public NodoInterno<T>* acceso; // Puntero al nodo interno de la matriz.
    }
}
