using code.structures.simple_list;
using code.structures.tree_avl;
using code.structures.tree_binary;
using code.structures.double_list;
using code.structures.tree_b;


namespace code.data
{
    public static class Variables
    {
        //Usuario Logueado
        public static Nodo_Usuario usuarioActual = new Nodo_Usuario(0, "", "", "", 0, "");
        public static string entrada = "";
        public static string salida = "";

        // Usuarios
        public static ListaEnlazada listaUsuarios = new ListaEnlazada(); 

        // Vehiculos
        public static ListaDoble listaVehiculos = new ListaDoble();


        //Repuestos
        public static ArbolAVL arbolRepuestos = new ArbolAVL();
        

        //Servicios
        public static ArbolBinario arbolServicios = new ArbolBinario();


        //Facturas
        public static ArbolB arbolFacturas = new ArbolB();
        
    }
}
     