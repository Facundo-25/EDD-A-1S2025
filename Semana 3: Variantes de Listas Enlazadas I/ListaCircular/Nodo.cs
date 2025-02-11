using System;

namespace ListaCircular
{
    public unsafe struct Node
    {
        public int Id;
        public fixed char NombreLocal[50];
        public fixed char NombrePropietario[50];
        public fixed char Direccion[100];
        public Node* Next;

        public Node(int id, string nombreLocal, string nombrePropietario, string direccion)
        {
            Id = id;
            Next = null;

            fixed (char* ptr = NombreLocal)
                nombreLocal.AsSpan().CopyTo(new Span<char>(ptr, 50));

            fixed (char* ptr = NombrePropietario)
                nombrePropietario.AsSpan().CopyTo(new Span<char>(ptr, 50));

            fixed (char* ptr = Direccion)
                direccion.AsSpan().CopyTo(new Span<char>(ptr, 100));
        }

        public override string ToString()
        {
            fixed (char* ptrNombre = NombreLocal, ptrPropietario = NombrePropietario, ptrDireccion = Direccion)
            {
                return $"ID: {Id}, NombreLocal: {new string(ptrNombre)}, Propietario: {new string(ptrPropietario)}, Dirección: {new string(ptrDireccion)}";
            }
        }
    }
}
