using System;
using MySql.Data.MySqlClient;

namespace TallerMecanicoApp
{
    public class ClienteDAO
    {
        private ConexionBD conexionBD;

        public ClienteDAO()
        {
            conexionBD = new ConexionBD();
        }

        // Método para Registrar un Cliente (INSERT)
        public bool RegistrarCliente(string nombre, string telefono, string correo)
        {
            string query = "INSERT INTO clientes (nombre, telefono, correo) VALUES (@nombre, @telefono, @correo)";
            
            try
            {
                MySqlConnection con = conexionBD.AbrirConexion();
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    // Añadimos los parámetros de forma segura
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@telefono", telefono);
                    cmd.Parameters.AddWithValue("@correo", correo);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("[ERROR DAO] No se pudo registrar el cliente: " + ex.Message);
                return false;
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }

        // Método para Mostrar todos los Clientes (SELECT)
        public void ListarClientes()
        {
            string query = "SELECT id_cliente, nombre, telefono, correo FROM clientes";

            try
            {
                MySqlConnection con = conexionBD.AbrirConexion();
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("\n=== LISTADO DE CLIENTES EN GARAGE GT ===");
                        Console.WriteLine("--------------------------------------------------");
                        while (reader.Read())
                        {
                            Console.WriteLine($"ID: {reader["id_cliente"]} | Nombre: {reader["nombre"]} | Tel: {reader["telefono"]} | Correo: {reader["correo"]}");
                        }
                        Console.WriteLine("--------------------------------------------------\n");
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("[ERROR DAO] No se pudieron leer los clientes: " + ex.Message);
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }
    }
}