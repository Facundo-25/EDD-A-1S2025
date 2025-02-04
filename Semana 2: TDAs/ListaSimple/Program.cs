using System;
using List;
using Estudiante;

class Program
{
    static void Main()
    {
        // Lista creada de numeros
        ListaSimple<int> listaNumeros = new ListaSimple<int>();
        listaNumeros.Insertar(10);
        listaNumeros.Insertar(20);
        listaNumeros.Insertar(30);
        listaNumeros.Insertar(40);

        Console.WriteLine("Lista de números:");
        listaNumeros.Imprimir();
        Console.WriteLine("-----------------\n");

        listaNumeros.Eliminar(20);
        Console.WriteLine("\nLista de números después de eliminar 20:");
        listaNumeros.Imprimir();
        Console.WriteLine("-----------------\n");

        // Lista de alumnos

        ListaSimple<Alumno> listaAlumnos = new ListaSimple<Alumno>();

        Alumno nuevoAlumno1 = new Alumno(20210001, "Juan Garcia");
        Alumno nuevoAlumno2 = new Alumno(20210002, "Marcos López");
        Alumno nuevoAlumno3 = new Alumno(20210003, "Julian Perez");
        Console.WriteLine("-----------------\n");

        listaAlumnos.Insertar(nuevoAlumno1);
        listaAlumnos.Insertar(nuevoAlumno2);
        listaAlumnos.Insertar(nuevoAlumno3);
        listaAlumnos.Insertar(new Alumno(20210004, "Fabian");)


        Console.WriteLine("Lista de Alumnos:");
        listaAlumnos.Imprimir();
        Console.WriteLine("-----------------\n");

        // Modificar el alumno con carnet 20210002
        listaAlumnos.ModificarPorCarnet(20210002, "Marcos López Actualizado");
        Console.WriteLine("\nLista de alumnos después de modificar al carnet  2021002:");
        listaAlumnos.Imprimir();
        Console.WriteLine("-----------------\n");
    }
}
