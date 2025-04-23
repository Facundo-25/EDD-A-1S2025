using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace source.merkle
{
    public class Factura
    {
        public int ID { get; set; }           
        public int ID_Servicio { get; set; }  
        public double Total { get; set; }     
        public string Fecha { get; set; }     
        public string MetodoPago { get; set; }

        // Constructor de la factura
        public Factura(int id, int idServicio, double total, string fecha, string metodoPago)
        {
            ID = id;
            ID_Servicio = idServicio;
            Total = total;
            Fecha = fecha;
            MetodoPago = metodoPago;
        }

        // Método para obtener el hash de la factura
        public string GetHash()
        {
            string data = JsonConvert.SerializeObject(this); // Serializar la factura a JSON
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2")); // Convertir a hexadecimal
                }
                return builder.ToString();
            }
        }
    }




    public class MerkleNode
    {
        public string Hash { get; set; }         // Hash del nodo 
        public MerkleNode Left { get; set; }     // Hijo izquierdo
        public MerkleNode Right { get; set; }    // Hijo derecho
        public Factura Factura { get; set; }     // Factura asociada 

        // Constructor para nodos hoja
        public MerkleNode(Factura factura)
        {
            Factura = factura;
            Hash = factura.GetHash();
            Left = null;
            Right = null;
        }

        // Constructor para nodos internos (combinación de hijos)
        public MerkleNode(MerkleNode left, MerkleNode right)
        {
            Factura = null;
            Left = left;
            Right = right;
            Hash = CalculateHash(left.Hash, right?.Hash); // Si right es null, usa solo left
        }

        // Método para calcular el hash combinado de dos nodos
        private string CalculateHash(string leftHash, string rightHash)
        {
            string combined = leftHash + (rightHash ?? leftHash); // Si no hay right, duplicar left
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combined));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }




    public class MerkleTree
    {
        private List<MerkleNode> Leaves; // Lista de nodos hoja 
        private MerkleNode Root;         // Raíz del árbol

        // Constructor del árbol
        public MerkleTree()
        {
            Leaves = new List<MerkleNode>();
            Root = null;
        }



        // Método para insertar una nueva factura
        public void Insert(int id, int idServicio, double total, string fecha, string metodoPago)
        {
            // Verificar unicidad del ID
            foreach (var leaf in Leaves)
            {
                if (leaf.Factura.ID == id)
                {
                    Console.WriteLine($"Error: Ya existe una factura con ID {id}.");
                    return;
                }
            }

            // Crear la factura y el nodo hoja
            Factura factura = new Factura(id, idServicio, total, fecha, metodoPago);
            MerkleNode newLeaf = new MerkleNode(factura);
            Leaves.Add(newLeaf);

            // Reconstruir el árbol con las hojas actuales
            BuildTree();
        }




        // Método privado para construir el árbol a partir de las hojas
        private void BuildTree()
        {
            if (Leaves.Count == 0)
            {
                Root = null;
                return;
            }

            List<MerkleNode> currentLevel = new List<MerkleNode>(Leaves);

            while (currentLevel.Count > 1)
            {
                List<MerkleNode> nextLevel = new List<MerkleNode>();

                for (int i = 0; i < currentLevel.Count; i += 2)
                {
                    MerkleNode left = currentLevel[i];
                    MerkleNode right = (i + 1 < currentLevel.Count) ? currentLevel[i + 1] : null;
                    MerkleNode parent = new MerkleNode(left, right);
                    nextLevel.Add(parent);
                }

                currentLevel = nextLevel;
            }

            Root = currentLevel[0]; // La raíz es el único nodo que queda
        }




        public string GenerateDot()
        {
            StringBuilder dot = new StringBuilder();
            dot.AppendLine("digraph MerkleTree {"); 
            dot.AppendLine("  node [shape=record];"); 
            dot.AppendLine("  graph [rankdir=TB];"); 
            dot.AppendLine("  subgraph cluster_0 {"); 
            dot.AppendLine("    label=\"Facturas\";");

            if (Root == null)
            {
                dot.AppendLine("    empty [label=\"Árbol vacío\"];");
            }
            else
            {
                Dictionary<string, int> nodeIds = new Dictionary<string, int>(); 
                int idCounter = 0;
                GenerateDotRecursive(Root, dot, nodeIds, ref idCounter);
            }

            dot.AppendLine("  }");
            dot.AppendLine("}"); 
            return dot.ToString();
        }

        private void GenerateDotRecursive(MerkleNode node, StringBuilder dot, Dictionary<string, int> nodeIds, ref int idCounter)
        {
            if (node == null) return;

            if (!nodeIds.ContainsKey(node.Hash))
            {
                nodeIds[node.Hash] = idCounter++;
            }
            int nodeId = nodeIds[node.Hash];

            string label;
            if (node.Factura != null) 
            {
                label = $"\"Factura {node.Factura.ID}\\nTotal: {node.Factura.Total}\\nHash: {node.Hash.Substring(0, 8)}...\"";
            }
            else 
            {
                label = $"\"Hash: {node.Hash.Substring(0, 8)}...\"";
            }
            dot.AppendLine($"  node{nodeId} [label={label}];");

            if (node.Left != null)
            {
                if (!nodeIds.ContainsKey(node.Left.Hash))
                {
                    nodeIds[node.Left.Hash] = idCounter++;
                }
                int leftId = nodeIds[node.Left.Hash];
                dot.AppendLine($"  node{nodeId} -> node{leftId};");
                GenerateDotRecursive(node.Left, dot, nodeIds, ref idCounter);
            }

            if (node.Right != null)
            {
                if (!nodeIds.ContainsKey(node.Right.Hash))
                {
                    nodeIds[node.Right.Hash] = idCounter++;
                }
                int rightId = nodeIds[node.Right.Hash];
                dot.AppendLine($"  node{nodeId} -> node{rightId};");
                GenerateDotRecursive(node.Right, dot, nodeIds, ref idCounter);
            }
        }
    }
}