using System;
using MySql.Data.MySqlClient;

namespace TallerMecanicoApp
{
    public class ConexionBD
    {
        // Cadena de conexión para el XAMPP local
        private string cadenaConexion = "Server=localhost;Port=3306;Database=garage_gt_db;Uid=root;Pwd=;";
        private MySqlConnection conexion;

        public ConexionBD()
        {
            conexion = new MySqlConnection(cadenaConexion);
        }

        // Método para abrir la base de datos
        public MySqlConnection AbrirConexion()
        {
            try
            {
                if (conexion.State == System.Data.ConnectionState.Closed)
                {
                    conexion.Open();
                    Console.WriteLine("\n[SUCCESS] Conexión exitosa a garage_gt_db en XAMPP.");
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("\n[ERROR] No se pudo conectar a la base de datos: " + ex.Message);
            }
            return conexion;
        }

        // Método para cerrar la conexión
        public void CerrarConexion()
        {
            try
            {
                if (conexion.State == System.Data.ConnectionState.Open)
                {
                    conexion.Close();
                    Console.WriteLine("[INFO] Conexión cerrada correctamente.\n");
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("[ERROR] Error al cerrar la conexión: " + ex.Message);
            }
        }
    }
}