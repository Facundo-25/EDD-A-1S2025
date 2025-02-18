using System;

namespace Matriz
{
    // Definición de una estructura genérica NodoInterno
    // Esta estructura usa el tipo de dato T, que debe ser un tipo no administrado (unmanaged)
    public unsafe struct NodoInterno<T> where T : unmanaged
    {
        public T id;               // Identificador de tipo T. Al ser genérico, puede ser cualquier tipo no administrado.
        public string nombre;       // Nombre del nodo, un valor de tipo string.
        public int coordenadaX;     
        public int coordenadaY;     
        
        // Punteros a otros nodos que forman la estructura de la matriz
        public NodoInterno<T>* arriba;      // Puntero al nodo superior 
        public NodoInterno<T>* abajo;       // Puntero al nodo inferior 
        public NodoInterno<T>* derecha;     // Puntero al nodo siguiente
        public NodoInterno<T>* izquierda;   // Puntero al nodo anterior 
    }
}
