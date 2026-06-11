using System;

namespace TallerMecanicoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== SISTEMA DE REPARACIONES GARAGE GT ===");
            
            // Instanciamos el manejador de clientes
            ClienteDAO clienteManejador = new ClienteDAO();

            // 1. CAPTURA DE DATOS DESDE LA TERMINAL
            Console.WriteLine("\n--- Formulario de Registro de Nuevo Cliente ---");
            Console.Write("Ingrese el nombre completo del cliente: ");
            string nombre = Console.ReadLine();

            Console.Write("Ingrese el teléfono: ");
            string telefono = Console.ReadLine();

            Console.Write("Ingrese el correo electrónico: ");
            string correo = Console.ReadLine();

            // 2. ENVIAR DATOS A MYSQL
            Console.WriteLine("\nGuardando datos en la base de datos...");
            bool exito = clienteManejador.RegistrarCliente(nombre, telefono, correo);

            if (exito)
            {
                Console.WriteLine("[SUCCESS] ¡Cliente guardado exitosamente en XAMPP!");
            }
            else
            {
                Console.WriteLine("[ALERT] No se pudo completar el registro del cliente.");
            }

            // 3. MOSTRAR LA TABLA ACTUALIZADA
            clienteManejador.ListarClientes();

            Console.WriteLine("Presiona cualquier tecla para finalizar el flujo...");
            Console.ReadKey();
        }
    }
}

string nombre = Console.ReadLine() ?? "";
string telefono = Console.ReadLine() ?? "";
string correo = Console.ReadLine() ?? "";