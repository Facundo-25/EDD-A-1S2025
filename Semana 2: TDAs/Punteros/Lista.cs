using System;
using System.Runtime.InteropServices;

namespace List
{
    public unsafe class ListaSimple<T> where T : unmanaged
    {
        private Nodo<T>* cabeza;

        public ListaSimple()
        {
            cabeza = null;
        }

        public void Insertar(T data)
        {
            Nodo<T>* nuevo = (Nodo<T>*)Marshal.AllocHGlobal(sizeof(Nodo<T>));
            nuevo->Data = data;
            nuevo->Sig = null;

            if (cabeza == null)
            {
                cabeza = nuevo;
            }
            else
            {
                Nodo<T>* temp = cabeza;
                while (temp->Sig != null)
                {
                    temp = temp->Sig;
                }
                temp->Sig = nuevo;
            }
        }

        public void Eliminar(T data)
        {
            if (cabeza == null) return;

            if (cabeza->Data.Equals(data))
            {
                Nodo<T>* temp = cabeza;
                cabeza = cabeza->Sig;
                Marshal.FreeHGlobal((IntPtr)temp);
                return;
            }

            Nodo<T>* actual = cabeza;
            while (actual->Sig != null && !actual->Sig->Data.Equals(data))
            {
                actual = actual->Sig;
            }

            if (actual->Sig != null)
            {
                Nodo<T>* temp = actual->Sig;
                actual->Sig = actual->Sig->Sig;
                Marshal.FreeHGlobal((IntPtr)temp);
            }
        }

        public void Imprimir()
        {
            Nodo<T>* temp = cabeza;
            while (temp != null)
            {
                Console.WriteLine(temp->Data);
                temp = temp->Sig;
            }
        }

        ~ListaSimple()
        {
            while (cabeza != null)
            {
                Nodo<T>* temp = cabeza;
                cabeza = cabeza->Sig;
                Marshal.FreeHGlobal((IntPtr)temp);
            }
        }
    }
}
