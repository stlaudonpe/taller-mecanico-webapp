using System;
using MySql.Data.MySqlClient;

namespace TallerMecanicoApp
{
    public class VehiculoDAO
    {
        private ConexionBD conexionBD;

        public VehiculoDAO()
        {
            conexionBD = new ConexionBD();
        }

        // Método para Registrar un Vehículo vinculado a un Cliente (INSERT)
        public bool RegistrarVehiculo(string placa, string marca, string modelo, int idCliente)
        {
            string query = "INSERT INTO vehiculos (placa, marca, modelo, id_cliente) VALUES (@placa, @marca, @modelo, @idCliente)";
            
            try
            {
                MySqlConnection con = conexionBD.AbrirConexion();
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@placa", placa);
                    cmd.Parameters.AddWithValue("@marca", marca);
                    cmd.Parameters.AddWithValue("@modelo", modelo);
                    cmd.Parameters.AddWithValue("@idCliente", idCliente);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("[ERROR DAO] No se pudo registrar el vehículo: " + ex.Message);
                return false;
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }

        // Método para listar vehículos con el nombre de su dueño (INNER JOIN)
        public void ListarVehiculosConDueño()
        {
            // Usamos un JOIN para traer el nombre del cliente de la otra tabla
            string query = @"SELECT v.id_vehiculo, v.placa, v.marca, v.modelo, c.nombre AS duenio 
                             FROM vehiculos v 
                             INNER JOIN clientes c ON v.id_cliente = c.id_cliente";

            try
            {
                MySqlConnection con = conexionBD.AbrirConexion();
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("\n=== INVENTARIO DE VEHÍCULOS EN TALLER ===");
                        Console.WriteLine("----------------------------------------------------------------------");
                        while (reader.Read())
                        {
                            Console.WriteLine($"ID: {reader["id_vehiculo"]} | Placa: {reader["placa"]} | {reader["marca"]} {reader["modelo"]} | Dueño: {reader["duenio"]}");
                        }
                        Console.WriteLine("----------------------------------------------------------------------\n");
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("[ERROR DAO] No se pudieron leer los vehículos: " + ex.Message);
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }
    }
}