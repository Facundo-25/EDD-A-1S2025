using code.structures.simple_list;
using code.structures.tree_avl;
using code.structures.tree_binary;
using code.structures.double_list;


namespace code.data
{
    public static class Variables
    {

        // Usuarios
        public static ListaEnlazada listaUsuarios = new ListaEnlazada(); 
        public static Nodo_Usuario usuarioActual = new Nodo_Usuario(0, "", "", "", 0, "");

        // Vehiculos
        public static ListaDoble listaVehiculos = new ListaDoble();


        //Repuestos
        public static ArbolAVL arbolRepuestos = new ArbolAVL();
        

        //Servicios
        public static ArbolBinario arbolServicios = new ArbolBinario();
        
    }
}
     