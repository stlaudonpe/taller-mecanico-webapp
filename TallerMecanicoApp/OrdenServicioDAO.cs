using System;
using MySql.Data.MySqlClient;

namespace TallerMecanicoApp
{
    public class OrdenServicioDAO
    {
        private ConexionBD conexionBD;

        public OrdenServicioDAO()
        {
            conexionBD = new ConexionBD();
        }

        // Método para Crear una Orden de Servicio (INSERT)
        public bool CrearOrden(string falla, decimal costo, int idVehiculo, int idMecanico)
        {
            string query = @"INSERT INTO ordenes_servicio (falla_reportada, estado, costo_estimado, id_vehiculo, id_mecanico) 
                             VALUES (@falla, 'En Progreso', @costo, @idVehiculo, @idMecanico)";
            
            try
            {
                MySqlConnection con = conexionBD.AbrirConexion();
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@falla", falla);
                    cmd.Parameters.AddWithValue("@costo", costo);
                    cmd.Parameters.AddWithValue("@idVehiculo", idVehiculo);
                    cmd.Parameters.AddWithValue("@idMecanico", idMecanico);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("[ERROR DAO] No se pudo crear la orden: " + ex.Message);
                return false;
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }

        // Método para ver la bitácora completa del taller (DOUBLE INNER JOIN)
        public void ListarBitacoraTaller()
        {
            string query = @"SELECT o.id_orden, o.falla_reportada, o.estado, o.costo_estimado, 
                                    v.placa, v.marca, m.nombre AS mecanico
                             FROM ordenes_servicio o
                             INNER JOIN vehiculos v ON o.id_vehiculo = v.id_vehiculo
                             INNER JOIN mecanicos m ON o.id_mecanico = m.id_mecanico";

            try
            {
                MySqlConnection con = conexionBD.AbrirConexion();
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("\n=== BITÁCORA GENERAL DE ÓRDENES - GARAGE GT ===");
                        Console.WriteLine("--------------------------------------------------------------------------------------");
                        while (reader.Read())
                        {
                            Console.WriteLine($"Orden #{reader["id_orden"]} | Carro: {reader["marca"]} ({reader["placa"]}) | Mecánico: {reader["mecanico"]} | Estado: {reader["estado"]} | Costo: Q{reader["costo_estimado"]}");
                            Console.WriteLine($"Falla: {reader["falla_reportada"]}");
                            Console.WriteLine("--------------------------------------------------------------------------------------");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("[ERROR DAO] No se pudo leer la bitácora: " + ex.Message);
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }
    }
}