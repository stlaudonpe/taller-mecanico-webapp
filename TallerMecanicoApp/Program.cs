// Prueba de integración DevOps Git + Jira - Garage GT
// TM-5: Módulo de auditoría en desarrollo...

using System;

namespace TallerMecanicoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ClienteDAO clienteManejador = new ClienteDAO();
            VehiculoDAO vehiculoManejador = new VehiculoDAO();
            MecanicoDAO mecanicoManejador = new MecanicoDAO();
            OrdenServicioDAO ordenManejador = new OrdenServicioDAO();
            
            bool continuar = true;

            while (continuar)
            {
                Console.WriteLine("=== SISTEMA DE REPARACIONES GARAGE GT ===");
                Console.WriteLine("1. Registrar Nuevo Cliente");
                Console.WriteLine("2. Registrar Nuevo Vehículo (Vincular a Cliente)");
                Console.WriteLine("3. Ver Inventario de Vehículos");
                Console.WriteLine("4. Registrar Nuevo Mecánico");
                Console.WriteLine("5. Ver Personal Técnico (Mecánicos)");
                Console.WriteLine("6. GENERAR ÓRDEN DE SERVICIO (Nueva Reparación)");
                Console.WriteLine("7. Ver Bitácora de Órdenes del Taller");
                Console.WriteLine("8. Salir");
                Console.Write("Seleccione una opción: ");
                string opcion = Console.ReadLine() ?? "";

                switch (opcion)
                {
                    case "1":
                        Console.WriteLine("\n--- Registro de Cliente ---");
                        Console.Write("Nombre completo: ");
                        string nombreC = Console.ReadLine() ?? "";
                        Console.Write("Teléfono: ");
                        string telefono = Console.ReadLine() ?? "";
                        Console.Write("Correo: ");
                        string correo = Console.ReadLine() ?? "";
                        if (clienteManejador.RegistrarCliente(nombreC, telefono, correo))
                            Console.WriteLine("[SUCCESS] Cliente guardado.");
                        break;

                    case "2":
                        Console.WriteLine("\n--- Registro de Vehículo ---");
                        clienteManejador.ListarClientes();
                        Console.Write("Ingrese el ID del cliente dueño del carro: ");
                        if (int.TryParse(Console.ReadLine(), out int idCliente))
                        {
                            Console.Write("Número de Placa: ");
                            string placa = Console.ReadLine() ?? "";
                            Console.Write("Marca: ");
                            string marca = Console.ReadLine() ?? "";
                            Console.Write("Modelo: ");
                            string modelo = Console.ReadLine() ?? "";
                            if (vehiculoManejador.RegistrarVehiculo(placa, marca, modelo, idCliente))
                                Console.WriteLine("[SUCCESS] Vehículo vinculado.");
                        }
                        break;

                    case "3":
                        vehiculoManejador.ListarVehiculosConDueño();
                        break;

                    case "4":
                        Console.WriteLine("\n--- Registro de Mecánico ---");
                        Console.Write("Nombre completo: ");
                        string nombreM = Console.ReadLine() ?? "";
                        Console.Write("Especialidad: ");
                        string especialidad = Console.ReadLine() ?? "";
                        if (mecanicoManejador.RegistrarMecanico(nombreM, especialidad))
                            Console.WriteLine("[SUCCESS] Mecánico registrado.");
                        break;

                    case "5":
                        mecanicoManejador.ListarMecanicos();
                        break;

                    case "6":
                        Console.WriteLine("\n--- Apertura de Orden de Servicio ---");
                        // Mostramos los carros disponibles
                        vehiculoManejador.ListarVehiculosConDueño();
                        Console.Write("Ingrese el ID del vehículo a reparar: ");
                        int.TryParse(Console.ReadLine(), out int idV);

                        // Mostramos los mecánicos disponibles
                        mecanicoManejador.ListarMecanicos();
                        Console.Write("Ingrese el ID del Mecánico asignado: ");
                        int.TryParse(Console.ReadLine(), out int idM);

                        Console.Write("Reporte de la falla (¿Qué le pasa al carro?): ");
                        string falla = Console.ReadLine() ?? "";

                        Console.Write("Presupuesto estimado (Costo inicial en Quetzales): ");
                        decimal.TryParse(Console.ReadLine(), out decimal costo);

                        if (ordenManejador.CrearOrden(falla, costo, idV, idM))
                            Console.WriteLine("[SUCCESS] ¡Orden de servicio creada y asignada con éxito!");
                        break;

                    case "7":
                        ordenManejador.ListarBitacoraTaller();
                        break;

                    case "8":
                        continuar = false;
                        Console.WriteLine("Saliendo de Garage GT...");
                        break;

                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }

                if (continuar)
                {
                    Console.WriteLine("\nPresione cualquier tecla para volver al menú...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }
    }
}