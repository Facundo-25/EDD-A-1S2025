using System;
using Estudiante;
using System;

namespace List
{
    // Definimos la clase ListaSimple, que es genérica, es decir, puede almacenar cualquier tipo de dato
    public class ListaSimple<T>
    {
        // Propiedad privada que mantiene el primer nodo de la lista
        private Nodo<T> cabeza;


        // Constructor de la lista simple
        public ListaSimple()
        {
            // Inicializamos la lista vacía (cabeza es null al principio)
            cabeza = null;
        }


        // Método para insertar un nuevo nodo al final de la lista
        public void Insertar(T data)
        {
            // Creamos un nuevo nodo con el dato proporcionado
            Nodo<T> nuevo = new Nodo<T>(data);

            // Si la lista está vacía (cabeza es null), el nuevo nodo será el primero
            if (cabeza == null)
            {
                cabeza = nuevo;
            }
            else
            {
                // Si la lista no está vacía, recorremos la lista hasta llegar al último nodo
                Nodo<T> temp = cabeza;
                while (temp.Sig != null)
                {
                    temp = temp.Sig;  // Nos movemos al siguiente nodo
                }

                // Una vez en el último nodo, enlazamos el nuevo nodo
                temp.Sig = nuevo;
            }
        }


        // Método para eliminar un nodo con un dato específico de la lista
        public void Eliminar(T data)
        {
            // Si la lista está vacía, no hay nada que eliminar
            if (cabeza == null) return;

            // Si el nodo a eliminar es la cabeza de la lista, movemos la cabeza al siguiente nodo
            if (cabeza.Data.Equals(data))
            {
                cabeza = cabeza.Sig;
                return;
            }

            // Si el nodo a eliminar no es la cabeza, buscamos el nodo con el dato
            Nodo<T> actual = cabeza;
            while (actual.Sig != null && !actual.Sig.Data.Equals(data))
            {
                actual = actual.Sig;  // Avanzamos al siguiente nodo
            }

            // Si encontramos el nodo a eliminar, lo saltamos
            if (actual.Sig != null)
            {
                actual.Sig = actual.Sig.Sig;  // Saltamos el nodo a eliminar
            }
        }


        // Método para modificar un alumno por su número de carnet (asumiendo que T es Alumno)
        public void ModificarPorCarnet(int carnet, string nuevoNombre)
        {
            // Empezamos desde la cabeza de la lista
            Nodo<T> temp = cabeza;
            while (temp != null)
            {
                // Comprobamos si el dato del nodo actual es un Alumno y si su carnet coincide
                if (temp.Data is Alumno alumno && alumno.Carnet == carnet)
                {
                    // Si encontramos el alumno, actualizamos su nombre
                    alumno.Nombre = nuevoNombre;
                    return;  // Salimos del método ya que el alumno fue actualizado
                }
                temp = temp.Sig;  // Avanzamos al siguiente nodo
            }

            // Si llegamos aquí, significa que no se encontró el alumno
            Console.WriteLine("Alumno no encontrado.");
        }


        // Método para imprimir todos los elementos de la lista
        public void Imprimir()
        {
            // Comenzamos desde la cabeza de la lista
            Nodo<T> temp = cabeza;
            while (temp != null)
            {
                // Imprimimos el dato del nodo actual
                Console.WriteLine(temp.Data);
                temp = temp.Sig;  // Avanzamos al siguiente nodo
            }
        }
        
    }
}
