using System;
using System.Collections.Generic;
using System.IO;

class Factura
{
    public int Id { get; set; }
    public int Id_Servicio { get; set; }
    public double Total { get; set; }

    public Factura(int id, int idServicio, double total)
    {
        Id = id;
        Id_Servicio = idServicio;
        Total = total;
    }

    public override string ToString()
    {
        return $"ID: {Id} \\n Servicio: {Id_Servicio} \\n Total: {Total}";
    }
}

class BTreeNode
{
    public List<Factura> Keys { get; set; } = new List<Factura>();
    public List<BTreeNode> Children { get; set; } = new List<BTreeNode>();
    public bool Leaf { get; set; } = true;

    public override string ToString()
    {
        return string.Join(" | ", Keys);
    }
}

class BTree
{
    private BTreeNode root;
    private int T = 3; 

    public BTree()
    {
        root = new BTreeNode { Leaf = true };
    }

    public void Insert(Factura factura)
    {
        if (Search(factura.Id) != null)
        {
            Console.WriteLine($"Error: La factura con ID {factura.Id} ya existe.");
            return;
        }

        if (root.Keys.Count == (2 * T) - 1)
        {
            BTreeNode newRoot = new BTreeNode { Leaf = false };
            newRoot.Children.Add(root);
            SplitChild(newRoot, 0, root);
            root = newRoot;
        }
        InsertNonFull(root, factura);
    }

    private void InsertNonFull(BTreeNode node, Factura factura)
    {
        int i = node.Keys.Count - 1;

        if (node.Leaf)
        {
            while (i >= 0 && factura.Id < node.Keys[i].Id)
            {
                i--;
            }
            node.Keys.Insert(i + 1, factura);
        }
        else
        {
            while (i >= 0 && factura.Id < node.Keys[i].Id)
                i--;
            i++;

            if (node.Children[i].Keys.Count == (2 * T) - 1)
            {
                SplitChild(node, i, node.Children[i]);
                if (factura.Id > node.Keys[i].Id)
                    i++;
            }
            InsertNonFull(node.Children[i], factura);
        }
    }

    private void SplitChild(BTreeNode parent, int index, BTreeNode child)
    {
        BTreeNode newNode = new BTreeNode { Leaf = child.Leaf };
        int mid = T - 1;

        parent.Keys.Insert(index, child.Keys[mid]);
        parent.Children.Insert(index + 1, newNode);

        newNode.Keys.AddRange(child.Keys.GetRange(mid + 1, T - 1));
        child.Keys.RemoveRange(mid, T);

        if (!child.Leaf)
        {
            newNode.Children.AddRange(child.Children.GetRange(mid + 1, T));
            child.Children.RemoveRange(mid + 1, T);
        }
    }

    public Factura Search(int id)
    {
        return Search(root, id);
    }

    private Factura Search(BTreeNode node, int id)
    {
        int i = 0;
        while (i < node.Keys.Count && id > node.Keys[i].Id)
            i++;

        if (i < node.Keys.Count && node.Keys[i].Id == id)
            return node.Keys[i];

        if (node.Leaf)
            return null;

        return Search(node.Children[i], id);
    }

    public void PrintTree()
    {
        PrintTree(root, 0);
    }

    private void PrintTree(BTreeNode node, int level)
    {
        if (node == null)
            return;

        Console.WriteLine(new string(' ', level * 4) + node);

        foreach (var child in node.Children)
        {
            PrintTree(child, level + 1);
        }
    }


     public void GenerateGraphviz()
    {
        using (StreamWriter writer = new StreamWriter("btree.dot"))
        {
            writer.WriteLine("digraph BTree {");
            writer.WriteLine("node [shape=record];");
            GenerateGraphviz(root, writer);
            writer.WriteLine("}");
        }
    }

    private void GenerateGraphviz(BTreeNode node, StreamWriter writer)
    {
        if (node == null)
            return;

        string nodeLabel = "\"" + string.Join(" | ", node.Keys) + "\"";
        writer.WriteLine($"{node.GetHashCode()} [label={nodeLabel}];");

        for (int i = 0; i < node.Children.Count; i++)
        {
            writer.WriteLine($"{node.GetHashCode()} -> {node.Children[i].GetHashCode()};");
            GenerateGraphviz(node.Children[i], writer);
        }
    }

}
