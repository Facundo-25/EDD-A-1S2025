using System;

class Nodo
{
    public int Id;
    public string Nombres;
    public string Apellidos;
    public string Correo;
    public int Edad;
    public string Contrasenia;
    public Nodo? Siguiente;  // Ahora puede ser null

    public Nodo(int id, string nombres, string apellidos, string correo, int edad, string contrasenia)
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




class ListaEnlazada
{
    private Nodo cabeza;

    public void Agregar(int id, string nombres, string apellidos, string correo, int edad, string contrasenia)
    {
        Nodo nuevo = new Nodo(id, nombres, apellidos, correo, edad, contrasenia);
        if (cabeza == null)
        {
            cabeza = nuevo;
        }
        else
        {
            Nodo actual = cabeza;
            while (actual.Siguiente != null)
            {
                actual = actual.Siguiente;
            }
            actual.Siguiente = nuevo;
        }
    }



    public void BubbleSort()
    {
        // Si la lista está vacía o solo tiene un elemento, no hay nada que ordenar.
        if (cabeza == null || cabeza.Siguiente == null)
            return;

        bool intercambiado;
        
        do
        {
            intercambiado = false;
            Nodo actual = cabeza;
            Nodo anterior = null;

            // Recorremos la lista mientras el siguiente nodo no sea null
            while (actual != null && actual.Siguiente != null) 
            {
                // Comparamos la edad de nodos adyacentes
                if (actual.Edad > actual.Siguiente.Edad)  
                {
                    // Guardamos el nodo siguiente temporalmente
                    Nodo temp = actual.Siguiente;

                    // Intercambiamos los nodos ajustando punteros
                    actual.Siguiente = temp.Siguiente;
                    temp.Siguiente = actual;

                    // Si estamos al inicio de la lista, actualizamos la cabeza
                    if (anterior == null)
                        cabeza = temp;
                    else
                        anterior.Siguiente = temp;

                    // Marcamos que hubo un intercambio
                    intercambiado = true;
                }

                // Avanzamos en la lista
                anterior = actual;
                actual = actual.Siguiente;
            }
        } while (intercambiado); // Se repite el proceso hasta que no haya más intercambios
    }





    public void Imprimir()
    {
        Nodo actual = cabeza;
        while (actual != null)
        {
            Console.WriteLine($"ID: {actual.Id}, Nombre: {actual.Nombres} {actual.Apellidos}, Edad: {actual.Edad}");
            actual = actual.Siguiente;
        }
    }
}



class Program
{
    static void Main()
    {
        ListaEnlazada lista = new ListaEnlazada();

        lista.Agregar(1, "Carlos", "Gómez", "carlos@mail.com", 25, "pass123");
        lista.Agregar(2, "Ana", "Pérez", "ana@mail.com", 20, "pass456");
        lista.Agregar(3, "Luis", "Martínez", "luis@mail.com", 99, "pass789");
        lista.Agregar(4, "María", "García", "maria@gmail.com", 22, "passabc");
        lista.Agregar(5, "Pedrito", "Pernandez", "pedrito@gmail.com", 10, "passdef");

        Console.WriteLine("Lista antes de ordenar:");
        lista.Imprimir();

        lista.BubbleSort();

        Console.WriteLine("\nLista después de ordenar:");
        lista.Imprimir();
    }
}
