using System;
using System.Runtime.InteropServices;

namespace ListaDobleUnsafe
{
    public unsafe class ListaDoblementeEnlazada
    {
        private Node* head;
        private Node* tail;

        public ListaDoblementeEnlazada()
        {
            head = null;
            tail = null;
        }

        public void Insertar(int id, string nombreLocal, string nombrePropietario, string direccion)
        {
            Node* nuevoNodo = (Node*)NativeMemory.Alloc((nuint)sizeof(Node));
            *nuevoNodo = new Node(id, nombreLocal, nombrePropietario, direccion);

            if (head == null)
            {
                head = tail = nuevoNodo;
            }
            else
            {
                tail->Next = nuevoNodo;
                nuevoNodo->Prev = tail;
                tail = nuevoNodo;
            }
        }

        public void Eliminar(int id)
        {
            Node* actual = head;
            while (actual != null)
            {
                if (actual->Id == id)
                {
                    if (actual->Prev != null)
                        actual->Prev->Next = actual->Next;
                    else
                        head = actual->Next;

                    if (actual->Next != null)
                        actual->Next->Prev = actual->Prev;
                    else
                        tail = actual->Prev;

                    NativeMemory.Free(actual);
                    return;
                }
                actual = actual->Next;
            }
        }

        public void Mostrar()
        {
            Node* actual = head;
            while (actual != null)
            {
                Console.WriteLine(actual->ToString());
                actual = actual->Next;
            }
        }

        public void MostrarReversa()
        {
            Node* actual = tail;
            while (actual != null)
            {
                Console.WriteLine(actual->ToString());
                actual = actual->Prev;
            }
        }

        ~ListaDoblementeEnlazada()
        {
            Node* actual = head;
            while (actual != null)
            {
                Node* temp = actual;
                actual = actual->Next;
                NativeMemory.Free(temp);
            }
        }
    }
}
