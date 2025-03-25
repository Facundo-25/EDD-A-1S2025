using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace code.utils.json
{
    public static class Jsons
    {
        public static void CrearJson(string nombre)
        {
            try
            {
                string carpeta = Path.Combine(Directory.GetCurrentDirectory(), "reports");

                if (!Directory.Exists(carpeta))
                {
                    Directory.CreateDirectory(carpeta);
                }

                if (string.IsNullOrEmpty(nombre))
                {
                    Console.WriteLine("El nombre del archivo no puede ser nulo o vacío.");
                    return;
                }

                if (!nombre.EndsWith(".json"))
                {
                    nombre += ".json";
                }

                string rutaArchivo = Path.Combine(carpeta, nombre);

                if (!File.Exists(rutaArchivo))
                {
                    File.WriteAllText(rutaArchivo, "[]"); 
                    Console.WriteLine($"Archivo JSON creado con éxito en: {rutaArchivo}");
                }
                else
                {
                    Console.WriteLine($"El archivo ya existe en: {rutaArchivo}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Error al generar el archivo: {ex.Message}");
            }
        }

        


        public static void InsertarRegistro(string nombre, string usuario, string entrada, string salida)
        {
            try
            {
                string carpeta = Path.Combine(Directory.GetCurrentDirectory(), "reports");
                string rutaArchivo = Path.Combine(carpeta, nombre.EndsWith(".json") ? nombre : nombre + ".json");

                if (!File.Exists(rutaArchivo))
                {
                    CrearJson(nombre);
                }

                string json = File.ReadAllText(rutaArchivo);
                List<Dictionary<string, string>> registros = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(json) ?? new List<Dictionary<string, string>>();

                var nuevoRegistro = new Dictionary<string, string>
                {
                    { "usuario", usuario },
                    { "entrada", entrada },
                    { "salida", salida }
                };

                registros.Add(nuevoRegistro);

                File.WriteAllText(rutaArchivo, JsonConvert.SerializeObject(registros, Formatting.Indented));

                Console.WriteLine($"Registro agregado con éxito en: {rutaArchivo}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al insertar el registro: {ex.Message}");
            }
        }


        
    }
}
