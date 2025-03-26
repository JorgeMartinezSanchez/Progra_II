using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization.Metadata;

GestorUsuarios gestor = GestorUsuarios.Instance;
Almacen almacen = Almacen.Instance;
UsuarioSistema elAdmin = new UsuarioSistema("Jorge", "8323115", "jorge.martinez.s", UsuarioSistema.Rol.Administrador);
Inicio.SingIn();

public class Carrito{
    public Cliente? Person;
    public Dictionary<Producto, int> Productos;

    public Carrito(Cliente _person){
        Person = _person;
        Productos = new Dictionary<Producto, int>();
    }

    public void Mostrar(){
        if(Productos.Count!=0){
            Console.WriteLine("\nProductos en el carrito:");
            foreach(var entry in Productos){
                Producto p = entry.Key;
                Console.WriteLine($"{p.Nombre}: {Productos[p]}");
            }
            Console.WriteLine();
        } else {
            Console.WriteLine("\nEl carrito esta vacio.\n");
        }
    }

    public bool estaVacio(){
        return Productos.Count == 0;
    }

    public int CantidadDeUnproducto(Producto p){
        return Productos[p];
    }

    public void Agregar(Producto _prod, int _amount){
        foreach (var entry in Productos){ // Itera sobre KeyValuePair<Producto, int>
            Producto p = entry.Key;
            if (_prod.Nombre == p.Nombre){
                Productos[p] += _amount;
                Console.WriteLine($"\nSe ha aumentado {_amount} en {p.Nombre}.\n");
                return;
            }
        }
        Productos[_prod] = _amount;
        Console.WriteLine($"\nSe agregó {_amount} copias de {_prod.Nombre} al carrito.\n");
    }

    public void vender(Producto _prod, int _amount){
        Productos[_prod] -= _amount;
    }

    public void elmininar(Producto _prod){
        foreach (var entry in Productos){ // Itera sobre KeyValuePair<Producto, int>
            Producto p = entry.Key;
            if(_prod.Nombre == p.Nombre){
                if(Productos[p]>1){
                    Console.WriteLine("\nDesea:");
                    while(true){
                        Console.WriteLine("a) Eliminar Todo | b) Eliminar Cierta Cantidad");
                        ConsoleKeyInfo select = Console.ReadKey();
                        switch(select.Key){
                            case ConsoleKey.A:
                                Productos.Remove(p);
                                Console.WriteLine($"{p.Nombre} elimindado del carrito.");
                                break;
                            case ConsoleKey.B:
                                int cantidad;
                                while(true){
                                    Console.Write("Que tanto desea eliminar?: ");
                                    if (int.TryParse(Console.ReadLine(), out cantidad)){
                                        break;
                                    }else{
                                        Console.WriteLine("Entrada inválida. debe ser un valor numerico.");
                                    }
                                }
                                Productos[p] -= cantidad;
                                break;
                            default:
                                Console.WriteLine("\nOpcion incorrecta\n");
                                break;
                        }
                        if(select.Key == ConsoleKey.A || select.Key == ConsoleKey.B) break; 
                    }
                } else {
                    Productos.Remove(p);
                }
            }
        }
        Console.WriteLine($"\nNo existe \"{_prod.Nombre}\" en el carrito.\n");
    }

    public Producto? obtenerDelCarrito(string _ProdName){
        foreach(var entry in Productos){
            Producto p = entry.Key;
            if(p.Nombre == _ProdName) return p;
        }
        return null;
    }
}
public class Almacen{ // Clase singleton
    private static Almacen? _instance;
    public List<Categoria> Contenido;
    public Dictionary<Producto, int> Inventario;

    private Almacen(){
        Contenido = new List<Categoria>();
        Inventario = new Dictionary<Producto, int>();
    }

    public bool estaVacio(){
        return Contenido.Count == 0;
    }
    public static Almacen Instance{
        get{
            if (_instance == null){
                _instance = new Almacen();
            }
            return _instance;
        }
    }

    public Producto? obtenerProducto(string pnombre){
        foreach (var entry in Inventario){ // Itera sobre KeyValuePair<Producto, int>
            Producto p = entry.Key;
            if (pnombre == p.Nombre){
                return p;
            }
        }
        return null;
    }

    public bool mostrarProductosDeCategoria(string CateName){
        Categoria? categoria = obtenerCategoria(CateName);
        if (categoria != null){
            Console.WriteLine($"{categoria.Nombre}");
            foreach (Producto p in categoria.Productos){
                int cantidad = Inventario.ContainsKey(p) ? Inventario[p] : 0;
                Console.WriteLine($"|_ {p.Nombre}.| Precio: {p.Precio}$ | Quedan: " + (cantidad>0 ? cantidad + "Stock" : "AGOTADO"));
            }
            return true;
        }else{
            Console.WriteLine("La categoría no existe.");
            return false;
        }
    }

    public bool encontrarProductoDeCategoria(string CateName, string ProdNombre){
        Categoria? categoria = obtenerCategoria(CateName);
        if (categoria != null){
            foreach (Producto p in categoria.Productos){
                if(p.Nombre == ProdNombre) return true;
            }
            return false;
        }else{
            return false;
        }
    }

    public Categoria? obtenerCategoria(string CateName){
        foreach(Categoria c in Contenido){
            if (CateName == c.Nombre) return c;
        }
        return null;
    }

    public void mostrarCategorias(){
        Console.WriteLine("CATEGORIAS");
        foreach(Categoria c in Contenido){
            Console.WriteLine(c.Nombre);
        }
    }

    public void mostrarFullStock(){
        Console.WriteLine("STOCK");
        foreach(Categoria c in Contenido){
            Console.WriteLine(c.Nombre);
            foreach(Producto p in c.Productos){
                int cantidad = Inventario.ContainsKey(p) ? Inventario[p] : 0;
                Console.WriteLine("|_" + p.Nombre + ": " + (cantidad>0 ? cantidad + "Stock" : "AGOTADO"));
            }
            Console.WriteLine();
        }
    }

    public void RegistrarCategoria(string Nombre){
        Categoria nuevaCategoria = new Categoria(Nombre);
    }

    public void RegistrarProducto(string Nombre, Categoria categoria, float precio, int cantidad){
        Producto nuevoProducto = new Producto(Nombre, categoria, precio, cantidad);
    }

    public void AgregarProductos(Producto producto, int cantidad){
        if (Inventario.ContainsKey(producto)){
            Inventario[producto] += cantidad;
        }else{
            if(Contenido.Contains(producto.Categoria)){
                Inventario[producto] = cantidad;
            } else {
                Console.WriteLine("No existe Categoria para tal producto.");
            }
        }
    }

    public void ActualizarCantidad(Producto producto, int cantidad){
        if (Inventario.ContainsKey(producto)){
            Inventario[producto] = cantidad;
        }else{
            Console.WriteLine("El producto no existe en el inventario.");
        }
    }

    public float Vender(Producto producto, int cantidad){
        float precioTotal = producto.Precio * cantidad;
        if (Inventario.ContainsKey(producto)){
            if(Inventario[producto]>0){
                Inventario[producto] -= cantidad;
                return precioTotal;
            } else {
                Console.WriteLine($"{producto.Nombre} ya no se encuentra disponible.");
                return 0;
            }
        }else{
            Console.WriteLine("El producto no existe en el inventario.");
            return 0;
        }
    }
}

public class Categoria{
    public string Nombre;
    public List<Producto> Productos;

    public Categoria(string nombre){
        Nombre = nombre;
        Productos = new List<Producto>();
        Almacen.Instance.Contenido.Add(this);
    }
}

public class Producto{
    public string Nombre;
    public Categoria Categoria;
    public float Precio;
    public Producto(string nombre, Categoria categoria, float precio, int cantidad)
    {
        Nombre = nombre;
        Categoria = categoria;
        Precio = precio;
        Categoria.Productos.Add(this);
    }
}

//----------------------------------------------------------------------------
///////////////////////////////// USUARIOS ///////////////////////////////////
//----------------------------------------------------------------------------

public class Usuario{
    public string Nombre;
    public string CarnetDeIdentidad;
    public string CorreoElectronico;

    public Usuario(string name, string ID, string Email){
        Nombre = name;
        CarnetDeIdentidad = ID;
        CorreoElectronico = Email;
    }
}

public class GestorUsuarios { // Clase Singleton
    public List<UsuarioSistema> usuarios = new List<UsuarioSistema>();
    public List<Cliente> clientes = new List<Cliente>();
    private static GestorUsuarios? _instance;
    public static GestorUsuarios Instance{
        get{
            if (_instance == null){
                _instance = new GestorUsuarios();
            }
            return _instance;
        }
    }

    public UsuarioSistema? getSysUser(string name, string id, string email, UsuarioSistema.Rol role){
        email += "@cshop.co.bo";
        foreach(UsuarioSistema US in usuarios){
            if(name == US.Nombre && id == US.CarnetDeIdentidad && email == US.CorreoElectronico && role == US.Rol_){
                return US;
            }
        }
        return null;
    }


    public Cliente? getClinet(string name, string id, string email, string country, string direction){
        email += "@gmail.com";
        foreach(Cliente US in clientes){
            if(name == US.Nombre &&
               id == US.CarnetDeIdentidad &&
               email == US.CorreoElectronico &&
               country == US.Pais &&
               direction == US.Direccion) 
                return US;
        }
        return null;
    }
}
public class UsuarioSistema : Usuario{
    public enum Rol{
        Administrador,  // No debe ser seleccionable
        EncargadoDeInventario,
        AtencionAlCliente,
        GestionadorDeUsuarios
    }

    public Rol Rol_;

    public UsuarioSistema(string name, string ID, string Email, Rol rol) : base(name, ID, Email){
        CorreoElectronico = Email + "@cshop.co.bo";
        Rol_ = rol;
        GestorUsuarios.Instance.usuarios.Add(this);
        Console.WriteLine($"Usuario {Nombre} registrado en el sistema.");
    }


    public void Init(){
        Console.WriteLine("¡Bienvenido/a " + Nombre + "!");
        Console.WriteLine("¿Qué desea hacer?");
        switch (Rol_){
            case Rol.Administrador:
                while(true){
                    Console.WriteLine("\na) Agregar empleados");
                    Console.WriteLine("b) Salir");
                    ConsoleKeyInfo select = Console.ReadKey();
                    switch(select.Key){
                        case ConsoleKey.A:
                            AgregarEmpleado();
                        break;
                        case ConsoleKey.B:
                            break;
                        default:
                            Console.WriteLine("Opcion incorrecta");
                            break;
                    }
                    if(select.Key == ConsoleKey.B) break;
                }
            break;
            case Rol.EncargadoDeInventario:
                while(true){
                    Console.WriteLine("\na)Agregar productos");
                    Console.WriteLine("b)Salir");
                    ConsoleKeyInfo select = Console.ReadKey();
                    switch(select.Key){
                        case ConsoleKey.A:
                            
                            break;
                        case ConsoleKey.B:
                            break;
                        default:
                            Console.WriteLine("\nOpcion incorrecta.\n");
                            break;
                    }
                    if(select.Key == ConsoleKey.B) break;
                }
            break;
        }
    }

    private void AgregarProducto(){
        string CatName, ProName;
        int amount;
        if(!Almacen.Instance.estaVacio()){
            Almacen.Instance.mostrarFullStock();
        } else {
            Console.WriteLine("\nEl stock esta vacio\n");
            Console.WriteLine("Agrega una categoria:");
            
        }
        
    }

    private void AgregarEmpleado(){
        string nombre = "", correo = "", ci = "";
        UsuarioSistema.Rol rolEnum = UsuarioSistema.Rol.AtencionAlCliente; // Valor por defecto

        while (true){
            Console.WriteLine("\n------------------------------");
            Console.WriteLine("Nombre:");
            nombre = Console.ReadLine() ?? "";

            Console.WriteLine("\nCorreo:");
            Console.WriteLine("____________@Cshop.co.bo");
            correo = Console.ReadLine() ?? "";

            Console.WriteLine("\nCédula de Identidad:");
            ci = Console.ReadLine() ?? "";

            while (true){
                Console.WriteLine("\nRol:");
                Console.WriteLine("1 - Encargado De Inventario");
                Console.WriteLine("2 - Atención Al Cliente");
                Console.WriteLine("3 - Gestionador De Usuarios");

                if (int.TryParse(Console.ReadLine(), out int rol_num) && rol_num >= 1 && rol_num <= 3){
                    rolEnum = (UsuarioSistema.Rol)(rol_num);
                    break;
                } else{
                    Console.WriteLine("Entrada inválida. Debe ser un número entre 1 y 3.");
                }
            }
            if (!string.IsNullOrWhiteSpace(nombre) && !string.IsNullOrWhiteSpace(correo) && !string.IsNullOrWhiteSpace(ci)){
                Console.WriteLine("\n¡Cuenta registrada!\n");
                break;
            }else{
                Console.WriteLine("\nPor favor llene todas las casillas.\n");
            }
        }
        UsuarioSistema nuevoEmpleado = new UsuarioSistema(nombre, ci, correo, rolEnum); // REGISTRADO
    }
}

public class Cliente : Usuario{
    public string Pais;
    public string Direccion;
    public Carrito carrito;
    public Cliente(string name, string ID, string Email, string country, string direction) : base(name, ID, Email)
    {
        CorreoElectronico = Email + "@gmail.com";
        Pais = country;
        Direccion = direction;
        carrito = new Carrito(this);
        GestorUsuarios.Instance.clientes.Add(this);
    }

    ///////////////////////////////////
    /// MENU PRINCIPAL DEL CLIENTE ///
    /////////////////////////////////
    public void Init(){
        string? ProductName, CategoryName;
        Console.WriteLine("¡Bienvenido/a " + Nombre + "!");
        while(true){
            Console.WriteLine("¿Que desea hacer?");
            Console.WriteLine("a) Comprar | b) Ver Carrito | c) Salir");
            ConsoleKeyInfo selectMenu = Console.ReadKey();
            switch(selectMenu.Key){
                case ConsoleKey.A:
                    while(true){
                        Console.WriteLine("¿Que Busca?");
                        Console.WriteLine();
                        Almacen.Instance.mostrarCategorias(); // Muestra las categorias existentes
                        Console.WriteLine();
                        CategoryName = Console.ReadLine();

                        if(string.IsNullOrEmpty(CategoryName)){
                            Console.WriteLine("\nPor favor, escribe la categoria.\n");
                        } else {
                            Console.WriteLine();
                            if(Almacen.Instance.mostrarProductosDeCategoria(CategoryName)){; // muestra productosd de categoria elegida
                                Console.WriteLine();
                                while(true){
                                    Console.Write("\nElige producto: ");
                                    ProductName = Console.ReadLine();
                                    if(ProductName == null){
                                        Console.WriteLine("Por favor, introduzca el nombre del producto que desea.");
                                    } else {
                                        Producto? prodElegido = Almacen.Instance.obtenerProducto(ProductName);
                                        if(prodElegido != null){
                                            Comprar(prodElegido);
                                            break;
                                        } else {
                                            Console.WriteLine("No existe tal producto en " + CategoryName);
                                        }
                                    }
                                }
                                break;
                            }
                            Console.WriteLine();
                        }
                    }
                    break;
                case ConsoleKey.B:
                    carrito.Mostrar();
                    while(true){
                        Console.WriteLine("Que desea hacer:");
                        Console.WriteLine("a) Agregar | b) Eliminar | c) Comprar | d) Atras");
                        ConsoleKeyInfo selectCarritoMenu = Console.ReadKey();
                        switch(selectCarritoMenu.Key){
                            case ConsoleKey.A:
                                Console.WriteLine("¿Que Busca?");
                                Console.WriteLine();
                                Almacen.Instance.mostrarCategorias(); // Muestra las categorias existentes
                                Console.WriteLine();
                                CategoryName = Console.ReadLine();

                                if(string.IsNullOrEmpty(CategoryName)){
                                    Console.WriteLine("\nPor favor, escribe la categoria.\n");
                                } else {
                                    Console.WriteLine();
                                    if(Almacen.Instance.mostrarProductosDeCategoria(CategoryName)){; // muestra productosd de categoria elegida
                                        Console.WriteLine();
                                        while(true){
                                            Console.Write("\nElige producto: ");
                                            ProductName = Console.ReadLine();
                                            if(ProductName == null){
                                                Console.WriteLine("Por favor, introduzca el nombre del producto que desea.");
                                            } else {
                                                Producto? prodElegido = Almacen.Instance.obtenerProducto(ProductName);
                                                int cantidad;
                                                if(prodElegido != null){
                                                    while(true){
                                                        Console.Write($"Ingrese Cantidad de {ProductName}$): ");
                                                        if (int.TryParse(Console.ReadLine(), out cantidad)){
                                                            if(cantidad<1){
                                                                Console.WriteLine("No se puede poner un numero menor a 1.");
                                                            } else {
                                                                break;
                                                            }
                                                        }else{
                                                            Console.WriteLine("Entrada inválida. debe ser un valor numerico.");
                                                        }
                                                    }
                                                    carrito.Agregar(prodElegido, cantidad);
                                                    break;
                                                } else {
                                                    Console.WriteLine($"No existe {ProductName} en {CategoryName}");
                                                }
				                            }
			                            }
			                        }   
		                        }
                                break;
                            case ConsoleKey.B:
                                if(!carrito.estaVacio()){
                                    carrito.Mostrar();
                                    while(true){
                                        Console.Write("\nElige producto: ");
                                        ProductName = Console.ReadLine();
                                        if(ProductName == null){
                                            Console.WriteLine("Por favor, introduzca el nombre del producto que desea.");
                                        } else {
                                            Producto? prodElegido = Almacen.Instance.obtenerProducto(ProductName);
                                            if(prodElegido != null){
                                                carrito.elmininar(prodElegido);
                                                break;
                                            } else{
                                                Console.WriteLine("El producto no existe.");
                                            }
				                        }
			                        }
                                } else {
                                    carrito.Mostrar();
                                }
                                break;
                            case ConsoleKey.C:
                                if(!carrito.estaVacio()){
                                    carrito.Mostrar();
                                    while(true){
                                        Console.Write("\nElige producto: ");
                                        ProductName = Console.ReadLine();
                                        if(ProductName == null){
                                            Console.WriteLine("Por favor, introduzca el nombre del producto que desea.");
                                        } else {
                                            Producto? prodElegido = carrito.obtenerDelCarrito(ProductName);
                                            if(prodElegido != null){
                                                ComprarDelCarrito(prodElegido);
                                                break;
                                            } else{
                                                Console.WriteLine("El producto no existe.");
                                            }
				                        }
			                        }
                                } else {
                                    carrito.Mostrar();
                                }
                                break;
                            case ConsoleKey.D:
                                break;
                            default:
                                Console.WriteLine("\nOpcion incorrecta.\n");
                                break;
                        }
                        if(selectCarritoMenu.Key == ConsoleKey.D) break;
                    }
                    break;
                case ConsoleKey.C:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("\nOpcion incorrecta.\n");
                    break;
            }
        }
    }
    private void Comprar(Producto name){
        int cantidad;
        while(true){
            Console.Write($"Ingrese Cantidad (Recuerde que el producto cuesta {name.Precio}$): ");
            if (int.TryParse(Console.ReadLine(), out cantidad)){
                if(cantidad<1){
                    Console.WriteLine("No se puede poner un numero menor a 1.");
                } else {
                    break;
                }
            }else{
                Console.WriteLine("Entrada inválida. debe ser un valor numerico.");
            }
        }
        float precioTotal = Almacen.Instance.Vender(name, cantidad);
        Console.WriteLine($"Usted compro:{name.Nombre} × {cantidad}.");
        Console.WriteLine($"Total: {precioTotal}");
    }

    private void ComprarDelCarrito(Producto name){
        int cantidad;
        while(true){
            Console.Write($"Ingrese Cantidad (Recuerde que el producto cuesta {name.Precio}$): ");
            if (int.TryParse(Console.ReadLine(), out cantidad)){
                if(cantidad<1){
                    Console.WriteLine("No se puede poner un numero menor a 1.");
                } else if(cantidad>carrito.CantidadDeUnproducto(name)){
                    Console.WriteLine($"No tiene suficiente \"{name.Nombre}\" en el carrito");
                } else {
                    break;
                }
            }else{
                Console.WriteLine("Entrada inválida. debe ser un valor numerico.");
            }
        }
        carrito.vender(name, cantidad);
        float precioTotal = Almacen.Instance.Vender(name, cantidad);
        Console.WriteLine($"Usted compro:{name.Nombre} × {cantidad}.");
        Console.WriteLine($"Total: {precioTotal}");
    }
}
//------------------------------------------------------------------
///////////////////// INTERFAZ E INICIO DE SESION///////////////////
//------------------------------------------------------------------

public static class Inicio{
    private static Cliente? cliente;
    private static UsuarioSistema? sysuser;
    private static string? nombre, correo, ci, pais, direccion;
    private static int rol_num;
    
    private static void LogIn(){
        Console.WriteLine("\n\n¡Bienvenido a Cshop!");
        while(true){
            Console.WriteLine("------------------------------");
            Console.WriteLine("\nIniciar sesion como:");
            Console.WriteLine("a) Cliente");
            Console.WriteLine("b) Usuario del Sistema");
            Console.WriteLine("c) Salir");
            ConsoleKeyInfo select = Console.ReadKey();
            switch(select.Key){
                case ConsoleKey.A:
                    while(true){
                        Console.WriteLine("\n------------------------------");
                        Console.WriteLine("Nombre:");
                        nombre = Console.ReadLine();

                        Console.WriteLine("\nCorreo:");
                        Console.WriteLine("____________@gmail.com");
                        correo = Console.ReadLine();

                        Console.WriteLine("\nCedula de Identidad:");
                        ci = Console.ReadLine();

                        Console.WriteLine("\nPais:");
                        pais = Console.ReadLine();

                        Console.WriteLine("\nDireccion:");
                        direccion = Console.ReadLine();

                        if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(ci) || string.IsNullOrEmpty(pais) || string.IsNullOrEmpty(direccion)){
                            Console.WriteLine("Por favor llene todos los campos.\n");
                        } else {
                            cliente = GestorUsuarios.Instance.getClinet(nombre, ci, correo, pais, direccion);
                            if(cliente != null){
                                cliente.Init();
                                break;
                            } else {
                                Console.WriteLine("\nNo existe un usuario en el sistema. Por favor registrese o introduzca bien los datos\n");
                                break;
                            }
                        }
                    }
                    break;
                case ConsoleKey.B:
                    while(true){
                        Console.WriteLine("\n------------------------------");
                        Console.WriteLine("Nombre:");
                        nombre = Console.ReadLine();

                        Console.WriteLine("\nCorreo:");
                        Console.WriteLine("____________@Cshop.co.bo");
                        correo = Console.ReadLine();

                        Console.WriteLine("\nCedula de Identidad:");
                        ci = Console.ReadLine();

                        UsuarioSistema.Rol rolEnum;
                        while(true){
                            Console.WriteLine("\nRol:");
                            Console.WriteLine("0 - Administrador");
                            Console.WriteLine("1 - Encargado De Inventario");
                            Console.WriteLine("2 - Atencion Al Cliente");
                            Console.WriteLine("3 - Gestionador De Usuarios");
                            
                            if (int.TryParse(Console.ReadLine(), out rol_num) && Enum.IsDefined(typeof(UsuarioSistema.Rol), rol_num)){
                                rolEnum = (UsuarioSistema.Rol)rol_num;
                                break;
                            }else{
                                Console.WriteLine("Entrada inválida. Debe ser un número entre 0 y 3.");
                            }
                        }
                        if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(ci)){
                            Console.WriteLine("\nPor favor llene todos los campos.\n");
                        } else {
                            sysuser = GestorUsuarios.Instance.getSysUser(nombre, ci, correo, rolEnum);
                            if(sysuser != null){
                                sysuser.Init();
                                break;
                            } else {
                                Console.WriteLine("\nUsuario no registrado en el sistema.\n");
                            }
                        }
                    }
                    break;
                case ConsoleKey.C:
                    return;
                default:
                    Console.WriteLine("\nOpcion Incorrecta\n");
                    break;
            }   
        } 
    }
    public static void SingIn(){
        while(true){
            Console.WriteLine("¡Bienvenido a Cshop!");
            Console.WriteLine("Por favor registrese:");
            Console.WriteLine("------------------------------------");
            while(true){
                Console.WriteLine("Nombre:");
                nombre = Console.ReadLine();

                Console.WriteLine("\nCorreo:");
                Console.WriteLine("____________@gmail.com");
                correo = Console.ReadLine();

                Console.WriteLine("\nCedula de Identidad:");
                ci = Console.ReadLine();

                Console.WriteLine("\nPais:");
                pais = Console.ReadLine();

                Console.WriteLine("\nDireccion:");
                direccion = Console.ReadLine();

                if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(ci) || string.IsNullOrEmpty(pais) || string.IsNullOrEmpty(direccion)){
                    Console.WriteLine("\nPor favor llene todos los campos.\n");
                } else {
                    Console.WriteLine("\nRegistrado con exito.\n");
                    break;
                }
            }
            Cliente nuevoCliente = new Cliente(nombre, ci, correo, pais, direccion); // REGISTRADO
            while(true){
                Console.WriteLine("Desea Iniciar Sesion o Registrar otra cuenta?");
                Console.WriteLine("a) Registrar otra cuenta.");
                Console.WriteLine("b) Iniciar sesion.");
                ConsoleKeyInfo select = Console.ReadKey();
                switch(select.Key){
                    case ConsoleKey.A:
                        break;
                    case ConsoleKey.B:
                        LogIn();
                        break;
                    default:
                        Console.WriteLine("\nOpcion incorrecta\n");
                        break;
                }
                if(select.Key == ConsoleKey.A) break;
            }
        }
    }
}