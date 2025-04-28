using System;
using System.IO;

namespace source.structures
{
    public class Vehiculo
    {
        public int Id { get; set; }
        public string Id_Usuario { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }
        public Vehiculo? Siguiente { get; set; }
        public Vehiculo? Anterior { get; set; }

        public Vehiculo(int id, string idUsuario, string marca, string modelo, string placa)
        {
            Id = id;
            Id_Usuario = idUsuario;
            Marca = marca;
            Modelo = modelo;
            Placa = placa;
            Siguiente = null;
            Anterior = null;
        }
    }

    public class DoubleLinkedList
    {
        private Vehiculo? cabeza;
        private Vehiculo? cola;

        public DoubleLinkedList()
        {
            cabeza = null;
            cola = null;
        }

        public void InsertVehicle(int id, string idUsuario, string marca, string modelo, string placa)
        {
            Vehiculo nuevo = new Vehiculo(id, idUsuario, marca, modelo, placa);

            if (cabeza == null)
            {
                cabeza = nuevo;
                cola = nuevo;
            }
            else
            {
                nuevo.Anterior = cola;
                cola!.Siguiente = nuevo;
                cola = nuevo;
            }
        }

        public void ViewVehicles()
        {
            if (cabeza == null)
            {
                Console.WriteLine("La lista de vehículos está vacía.");
                return;
            }

            Vehiculo? actual = cabeza;
            Console.WriteLine("Lista de vehículos:");
            while (actual != null)
            {
                Console.WriteLine($"ID: {actual?.Id}, ID Usuario: {actual?.Id_Usuario}, Marca: {actual?.Marca}, Modelo: {actual?.Modelo}, Placa: {actual?.Placa}");
                actual = actual.Siguiente;
            }
        }

        public void GenerateFile()
        {
            string ruta = "./reports/vehiculos.txt"; 
            try
            {
                string? directorio = Path.GetDirectoryName(ruta);
                if (!string.IsNullOrEmpty(directorio) && !Directory.Exists(directorio))
                {
                    Directory.CreateDirectory(directorio);
                }

                using (StreamWriter writer = new StreamWriter(ruta, false))
                {
                    Vehiculo? actual = cabeza;
                    while (actual != null)
                    {
                        writer.WriteLine($"{actual.Id},{actual.Id_Usuario},{actual.Marca},{actual.Modelo},{actual.Placa}");
                        actual = actual.Siguiente;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al generar el archivo: {ex.Message}");
            }
        }
    }
}