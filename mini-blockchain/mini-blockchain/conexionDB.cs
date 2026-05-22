using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
//ALUMNA:Sarai Fregozo Aréchiga Código: 219523217
namespace mini_blockchain
{
    internal class conexionDB
    {
        private string cadenaConexion = "server=localhost;user=root;password=sebastian;database=miniblock;";
        public MySqlConnection ObtenerConexion()
        {
            // Siempre devuelve una nueva conexión
            
            return new MySqlConnection(cadenaConexion);
        }
    }
}


