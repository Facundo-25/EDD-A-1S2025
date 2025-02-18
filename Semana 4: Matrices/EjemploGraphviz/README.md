Pasos 

1. Crear el Proyecto

dotnet new console -n EjemploGraphviz

2. Habilitar el unsafe code  editando el archivo EjemploGraphviz.csproj (nombre del proyecto creado + .csprj) agregando entre las etiquetas   <PropertyGroup>  </PropertyGroup> la siguiente linea de codigo:

<AllowUnsafeBlocks>true</AllowUnsafeBlocks>


3. Instalamos la siguiente libreria para usar GTK

sudo apt-get install gtk-sharp3


4. Y luego lo agregamos en nuestro proyecto

dotnet add package GtkSharp


5. Instalamos la siguiente libreria para poder utilizar y cargar un archivo json:

dotnet add package Newtonsoft.Json


6. Instalamos graphviz

sudo apt-get install graphviz








