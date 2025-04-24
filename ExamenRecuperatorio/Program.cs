using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Reflection.Metadata;

ListaVehiculos List = new ListaVehiculos();

List.agregar_vehiculo(new Auto("Toyota", "Rojo", "M102", "Caja"));
List.agregar_vehiculo(new Scooter("Mattel", "Amarillo", "Duracell", 3));
List.agregar_vehiculo(new Bicicleta("Jitensha", "Azul"));
List.mostrar_caracteristicas();
Console.WriteLine();
List.arrancar_todos();

public class Vehiculo{
    private string marca;
    private string color;

    public Vehiculo(string brand, string colour){
        marca = brand;
        color = colour;
    }

    public string dar_marca(){
        return marca;
    }

    public string dar_color(){
        return color;
    }

    public virtual string combustible(){
        return "Combustible";
    }

    public virtual void mover(){
        Console.WriteLine("Moviendo con " + combustible());
    }

    public virtual int mostrar_llantas(){
        return 1;
    }
}

public class Auto : Vehiculo{
    private string motor;
    private string caja;
    public Auto(string brand, string colour, string motor, string type) : base(brand, colour)
    {
        this.motor = motor;
        caja = type;
    }

    public override string combustible(){
        return "Gasolina";
    }

    public override void mover()
    {
        Console.WriteLine("Conduciendo con " + combustible());
    }

    public override int mostrar_llantas()
    {
        return 4;
    }
}

public class Scooter : Vehiculo{
    private string motour;
    private int llantas;
    public Scooter(string brand, string colour, string motor, int wheels) : base(brand, colour)
    {
        motour = motor;
        llantas = wheels;
    }

    public override string combustible(){
        return "Bateria Electrica";
    }

    public override void mover()
    {
        Console.WriteLine("Avanzando con " + combustible());
    }

    public override int mostrar_llantas()
    {
        return llantas;
    }
}

public class Bicicleta : Vehiculo{
    public Bicicleta(string brand, string colour) : base(brand, colour)
    {
    }

    public override string combustible()
    {
        return "Esfuerzo";
    }

    public override void mover()
    {
        Console.WriteLine("Pedaleando con " + combustible());
    }

    public override int mostrar_llantas()
    {
        return 2;
    }
}

public class ListaVehiculos{
    public Vehiculo[] movilidades;
    public const int MAX = 10;
    public int count = 0;

    public ListaVehiculos(){
        movilidades = new Vehiculo[MAX];
    }

    public void agregar_vehiculo(Vehiculo v) {
        if (count < MAX) {
            movilidades[count] = v;
            count++;
        } else {
            Console.WriteLine("La lista de vehículos está llena.");
        }
    }

    public void arrancar_todos(){
        foreach(Vehiculo m in movilidades){
            if (m == null) break;
            m.mover();
        }
    }

    public void mostrar_caracteristicas(){
        foreach(Vehiculo m in movilidades){
            if(m == null) break;
            Console.WriteLine($"Marca: {m.dar_marca()} | Color: {m.dar_color()}, | Combustible: {m.combustible()} | Llantas: {m.mostrar_llantas()}");
        }
    }
}