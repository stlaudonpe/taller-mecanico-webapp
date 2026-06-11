using System;
using MySql.Data.MySqlClient;

namespace TallerMecanicoApp
{
    public class MecanicoDAO
    {
        private ConexionBD conexionBD;

        public MecanicoDAO()
        {
            conexionBD = new ConexionBD();
        }

        // Método para Registrar un Mecánico (INSERT)
        public bool RegistrarMecanico(string nombre, string especialidad)
        {
            string query = "INSERT INTO mecanicos (nombre, especialidad) VALUES (@nombre, @especialidad)";
            
            try
            {
                MySqlConnection con = conexionBD.AbrirConexion();
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@especialidad", especialidad);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("[ERROR DAO] No se pudo registrar al mecánico: " + ex.Message);
                return false;
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }

        // Método para Listar Mecánicos (SELECT)
        public void ListarMecanicos()
        {
            string query = "SELECT id_mecanico, nombre, especialidad FROM mecanicos";

            try
            {
                MySqlConnection con = conexionBD.AbrirConexion();
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("\n=== PERSONAL TÉCNICO (MECÁNICOS) ===");
                        Console.WriteLine("--------------------------------------------------");
                        while (reader.Read())
                        {
                            Console.WriteLine($"ID: {reader["id_mecanico"]} | Nombre: {reader["nombre"]} | Especialidad: {reader["especialidad"]}");
                        }
                        Console.WriteLine("--------------------------------------------------\n");
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("[ERROR DAO] No se pudieron leer los mecánicos: " + ex.Message);
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }
    }
}