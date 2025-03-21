using System;
using System.Text.RegularExpressions;

public class Agenda
{

    public static void Main(string[] args)
    {
        int opc = 1;

        do
        {
            Console.Write(Menu());
            opc = Convert.ToInt32(Console.ReadLine());
            
            switch (opc)
            {
                case 1:
                    AgregarRegistro();
                    break;
                case 2:
                    EditarRegistro();
                    break;
                case 3:
                    EliminarRegistro();
                    break;
                case 4:
                    MostrarRegistros();
                    break;
                case 5:
                    Console.WriteLine("Saliendo del programa");
                    break;
                default:
                    Console.WriteLine("OPCIÓN INVÁLIDA\n");
                    break;
            }
        } while (opc != 5); 
    }

    public static string Menu()
    {
        return string.Format(
            "MENÚ PRINCIPAL\n" +
            "****************\n" +
            "\n" +
            "1) Agregar registro\n" +
            "2) Editar registro\n" +
            "3) Eliminar registro\n" +
            "4) Mostrar registros\n" +
            "5) Terminar\n" +
            "ELIJA UNA OPCIÓN:\n"
        );
    }

    public static void AgregarRegistro()
    {
        Console.WriteLine("Ingrese el nombre:");
        string nombre = Console.ReadLine();
        Console.WriteLine("Ingrese el teléfono:");
        string telefono = Console.ReadLine();
        Console.WriteLine("Ingrese el correo electrónico:");
        string email = Console.ReadLine();

        Persona nuevaPersona = new Persona(nombre, telefono, email);
        agenda.Add(nuevaPersona);
        Console.WriteLine("Registro agregado con éxito.\n");
    }

    public static void EditarRegistro()
    {

    }

    public static void EliminarRegistro()
    {
       
    }

    public static void MostrarRegistros()
    {
       
    }
}

public class Persona
{
    public string Nombre { get; set; }

    private string telefono;
    public string Telefono
    {
        set
        {
            Regex regexp = new Regex(@"^\+?[1-9]\d{1,14}$");
            if (regexp.IsMatch(value))
            {
                telefono = value;
            }
            else
            {
                telefono = "########";
            }
        }
        get { return telefono; }
    }

    private string email;
    public string Email
    {
        set
        {
            Regex regexp = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            if (regexp.IsMatch(value))
            {
                email = value;
            }
            else
            {
                email = "########"; 
            }
        }
    }

    public Persona() { }

    public Persona(string nombre, string telefono, string email)
    {
        this.Nombre = nombre;
        this.Telefono = telefono;
        this.Email = email;
    }

    public override string ToString()
    {
        return $"Nombre: {Nombre}, Teléfono: {Telefono}, Email: {Email}";
    }
}
