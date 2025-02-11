using System;

namespace ListaDobleUnsafe
{
    public unsafe struct Node
    {
        public int Id;
        public fixed char NombreLocal[50];
        public fixed char NombrePropietario[50];
        public fixed char Direccion[100];
        public Node* Next;
        public Node* Prev;

        public Node(int id, string nombreLocal, string nombrePropietario, string direccion)
        {
            Id = id;
            Next = null;
            Prev = null;

            fixed (char* nl = NombreLocal)
            {
                for (int i = 0; i < nombreLocal.Length && i < 50; i++)
                    nl[i] = nombreLocal[i];
            }

            fixed (char* np = NombrePropietario)
            {
                for (int i = 0; i < nombrePropietario.Length && i < 50; i++)
                    np[i] = nombrePropietario[i];
            }

            fixed (char* d = Direccion)
            {
                for (int i = 0; i < direccion.Length && i < 100; i++)
                    d[i] = direccion[i];
            }
        }

        public override string ToString()
        {
            fixed (char* nl = NombreLocal, np = NombrePropietario, d = Direccion)
            {
                return $"ID: {Id}, NombreLocal: {new string(nl)}, Propietario: {new string(np)}, Dirección: {new string(d)}";
            }
        }
    }
}
