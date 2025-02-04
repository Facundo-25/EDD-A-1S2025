using System;
using System.Runtime.InteropServices;

namespace List
{
    public unsafe struct Nodo<T> where T : unmanaged
    {
        public T Data;
        public Nodo<T>* Sig;
    }
}
