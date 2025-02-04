namespace Estudiante
{
    // Definimos la clase Alumno dentro del namespace Estudiante
    public class Alumno
    {
        // Propiedad Carnet: almacena el número de carnet del alumno
        public int Carnet { get; set; }

        // Propiedad Nombre: almacena el nombre del alumno
        public string Nombre { get; set; }

        // Constructor que permite crear un objeto Alumno con un carnet y un nombre
        public Alumno(int carnet, string nombre)
        {
            Carnet = carnet; 
            Nombre = nombre; 
        }

        // Método ToString que devuelve una representación en cadena del objeto Alumno
        public override string ToString()
        {
            return $"Carnet: {Carnet}, Nombre: {Nombre}";
        }
    }
}
