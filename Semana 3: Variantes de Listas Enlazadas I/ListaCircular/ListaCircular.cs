using System;
using System.Runtime.InteropServices;

namespace ListaCircular
{
    public unsafe class ListaCircular
    {
        private Node* head;

        public ListaCircular()
        {
            head = null;
        }

        public void Insertar(int id, string nombreLocal, string nombrePropietario, string direccion)
        {
            Node* nuevoNodo = (Node*)Marshal.AllocHGlobal(sizeof(Node));
            *nuevoNodo = new Node(id, nombreLocal, nombrePropietario, direccion);

            if (head == null)
            {
                head = nuevoNodo;
                head->Next = head; // Apunta a sí mismo
            }
            else
            {
                Node* temp = head;
                while (temp->Next != head)
                {
                    temp = temp->Next;
                }
                temp->Next = nuevoNodo;
                nuevoNodo->Next = head;
            }
        }

        public void Eliminar(int id)
        {
            if (head == null) return;

            if (head->Id == id && head->Next == head)
            {
                Marshal.FreeHGlobal((IntPtr)head);
                head = null;
                return;
            }

            Node* temp = head;
            Node* prev = null;
            do
            {
                if (temp->Id == id)
                {
                    if (prev != null)
                    {
                        prev->Next = temp->Next;
                    }
                    else
                    {
                        Node* last = head;
                        while (last->Next != head)
                        {
                            last = last->Next;
                        }
                        head = head->Next;
                        last->Next = head;
                    }
                    Marshal.FreeHGlobal((IntPtr)temp);
                    return;
                }
                prev = temp;
                temp = temp->Next;
            } while (temp != head);
        }

        public void Mostrar()
        {
            if (head == null)
            {
                Console.WriteLine("Lista vacía.");
                return;
            }

            Node* temp = head;
            do
            {
                Console.WriteLine(temp->ToString());
                temp = temp->Next;
            } while (temp != head);
        }

        ~ListaCircular()
        {
            if (head == null) return;

            Node* temp = head;
            do
            {
                Node* next = temp->Next;
                Marshal.FreeHGlobal((IntPtr)temp);
                temp = next;
            } while (temp != head);
        }
    }
}
