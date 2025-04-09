using System;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace source.blockchain
{
    public class User
    {
        public int ID { get; set; }              
        public string Nombres { get; set; }      
        public string Apellidos { get; set; }    
        public string Correo { get; set; }       
        public int Edad { get; set; }            
        public string Contrasenia { get; set; }  

        public User(int id, string nombres, string apellidos, string correo, int edad, string contrasenia)
        {
            ID = id;
            Nombres = nombres;
            Apellidos = apellidos;
            Correo = correo;
            Edad = edad;
            Contrasenia = contrasenia; 
        }
    }



    // Clase que representa un bloque (nodo) 
    public class Block
    {
        public int Index { get; set; }           // Número del bloque en la cadena (comienza en 0)
        public string Timestamp { get; set; }    // Fecha y hora de creación del bloque
        public string Data { get; set; }         // Información del usuario en formato JSON
        public int Nonce { get; set; }           // Número que se itera para la prueba de trabajo
        public string PreviousHash { get; set; } // Hash del bloque anterior
        public string Hash { get; set; }         // Hash único del bloque actual
        public Block Next { get; set; }          // Enlace al siguiente bloque en la lista

    
        public Block(int index, User user, string previousHash)
        {
            Index = index;
            Timestamp = DateTime.Now.ToString("dd-MM-yy::HH:mm:ss"); 
            Data = JsonConvert.SerializeObject(user);                   // Serializar el usuario a JSON
            Nonce = 0;                                                  // Inicia en 0 para la prueba de trabajo
            PreviousHash = previousHash;
            Hash = CalculateHash();                                     // Calcula el hash inicial
            Next = null;                                    
        }

        // Método para calcular el hash del bloque usando SHA-256
        public string CalculateHash()
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Concatenar todos los atributos sin espacios ni saltos de línea
                string rawData = Index + Timestamp + Data + Nonce + PreviousHash;
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));   // Convertir a hexadecimal
                }
                return builder.ToString();
            }
        }

        // Método para realizar la prueba de trabajo (encontrar un hash con prefijo "0000")
        public void MineBlock()
        {
            while (!Hash.StartsWith("0000")) // Condición de la prueba de trabajo
            {
                Nonce++;                    // Incrementar el nonce
                Hash = CalculateHash();     // Recalcular el hash
            }
        }
    }



    // Clase que representa la cadena de bloques como una lista simplemente enlazada
    public class Blockchain
    {
        private Block Head; 


        public Blockchain()
        {
            Head = null;
        }


        // Método para agregar un nuevo bloque a la cadena
        public void AddBlock(int id, string nombres, string apellidos, string correo, int edad, string contrasenia)
        {
            User user = new User(id, nombres, apellidos, correo, edad, contrasenia);

            if (Head == null)
            {
                Block firstBlock = new Block(0, user, "0000");  // PreviousHash inicial como "0000"
                firstBlock.MineBlock();                         // Minar el primer bloque
                Head = firstBlock;                              // Establecer como cabeza
                return;
            }


            Block current = Head;
            while (current.Next != null)
            {
                current = current.Next;
            }

            // Crear un nuevo bloque con el índice siguiente al último
            Block newBlock = new Block(current.Index + 1, user, current.Hash);
            newBlock.MineBlock();                               
            current.Next = newBlock; 
        }



        // Método para generar un string JSON con toda la cadena
        public string GenerateJson()
        {
            if (Head == null)
            {
                return "[]"; 
            }

            List<object> blocks = new List<object>(); 
            Block current = Head;

            // Recorrer la lista y agregar cada bloque
            while (current != null)
            {
                blocks.Add(new
                {
                    Index = current.Index,
                    Timestamp = current.Timestamp,
                    Data = JsonConvert.DeserializeObject(current.Data), 
                    Nonce = current.Nonce,
                    PreviousHash = current.PreviousHash,
                    Hash = current.Hash
                });
                current = current.Next;
            }

            return JsonConvert.SerializeObject(blocks, Formatting.Indented);
        }


        
        // Dentro de la clase Blockchain
        public string GenerateDot()
        {
            StringBuilder dot = new StringBuilder();
            dot.AppendLine("digraph Blockchain {"); 
            dot.AppendLine("    node [shape=record];");
            dot.AppendLine("    graph [rankdir=LR];"); 
            dot.AppendLine("    subgraph cluster_0 {"); 
            dot.AppendLine("        label=\"Usuarios\";"); 
            if (Head == null)
            {
                dot.AppendLine("  empty [label=\"Cadena vacía\"];"); 
            }
            else
            {
                Block current = Head;
                while (current != null)
                {
                    string hashDisplay = current.Hash.Length >= 8 ? current.Hash.Substring(0, 8) + "..." : current.Hash;
                    string prevHashDisplay = current.PreviousHash.Length >= 8 ? current.PreviousHash.Substring(0, 8) + "..." : current.PreviousHash;

                    string nodeLabel = $"\"Bloque {current.Index}\\nHash: {hashDisplay}\\nPrevious: {prevHashDisplay}\"";
                    dot.AppendLine($"  block{current.Index} [label={nodeLabel}];");

                    if (current.Next != null)
                    {
                        dot.AppendLine($"  block{current.Index} -> block{current.Next.Index};");
                    }
                    current = current.Next;
                }
            }

            dot.AppendLine("}}"); // Cerrar el grafo
            return dot.ToString();
        }



        // Método para analizar la integridad de la cadena
        public bool AnalyzeBlockchain()
        {
            if (Head == null)
            {
                Console.WriteLine("La cadena está vacía.");
                return true; // Una cadena vacía se considera válida
            }

            Block current = Head;
            Block previous = null;

            while (current != null)
            {
                // Verificar que el hash almacenado sea correcto
                if (current.Hash != current.CalculateHash())
                {
                    Console.WriteLine($"El hash del bloque {current.Index} es inválido.");
                    return false;
                }

                // Si no es el primer bloque, verificar el enlace con el bloque anterior
                if (previous != null && current.PreviousHash != previous.Hash)
                {
                    Console.WriteLine($"El PreviousHash del bloque {current.Index} no coincide.");
                    return false;
                }

                // Verificar que el hash cumple con la prueba de trabajo
                if (!current.Hash.StartsWith("0000"))
                {
                    Console.WriteLine($"El hash del bloque {current.Index} no cumple con la prueba de trabajo.");
                    return false;
                }

                previous = current;
                current = current.Next;
            }

            Console.WriteLine("La cadena de bloques es válida.");
            return true;
        }



        public void ViewBlock(int index)
        {
            Block current = Head;
            while (current != null)
            {
                if (current.Index == index)
                {
                    Console.WriteLine($"=====================================");
                    Console.WriteLine($"INDEX: {current.Index}");
                    Console.WriteLine($"TIMESTAMP: {current.Timestamp}");
                    Console.WriteLine($"DATA: {current.Data}");
                    Console.WriteLine($"NONCE: {current.Nonce}");
                    Console.WriteLine($"PREVIOUS HASH: {current.PreviousHash}");
                    Console.WriteLine($"HASH: {current.Hash}");
                    Console.WriteLine($"=====================================");
                    return;
                }
                current = current.Next;
            }
            Console.WriteLine("Índice de bloque inválido.");
        }
    }

}