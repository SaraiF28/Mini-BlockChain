using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//ALUMNA:Sarai Fregozo Aréchiga Código: 219523217
namespace mini_blockchain
{
    // Clase que administra toda la cadena de bloques y la conexión con la base de datos.
    // Se encarga de cargar la cadena, agregar nuevos bloques, verificarlos y guardarlos.
    public class Blockchain
    {
        private List<Block> chain = new List<Block>();
        private conexionDB db = new conexionDB();

        // Constructor: carga la cadena desde la BD y crea el bloque inicial si la tabla está vacía.
        public Blockchain()
        {
            LoadFromDB();

            // Si no hay bloques en la BD, crear el bloque inicial
            if (chain.Count == 0)
            {
                Block inicial = new Block(0, "BLOQUE INICIAL", "0000");
                chain.Add(inicial);
                SaveToDB(inicial);
            }
        }

        // Agrega un nuevo bloque usando el hash del último bloque almacenado.
        public void AddBlock(string data)
        {
            Block last = chain[chain.Count - 1];
            int newId = last.ID + 1;

            Block newBlock = new Block(newId, data, last.Hash);
            chain.Add(newBlock);

            SaveToDB(newBlock);
        }

        // Guarda un bloque en la BD
        private void SaveToDB(Block b)
        {
            using (MySqlConnection con = db.ObtenerConexion())
            {
                con.Open();
                string query = @"INSERT INTO blocks(timestamp, data, prev_hash, current_hash)
                                 VALUES(@t, @d, @p, @h)";

                MySqlCommand cmd = new MySqlCommand(query, con);

                // Guardamos timestamp EXACTO con milisegundos
                cmd.Parameters.AddWithValue("@t", b.Timestamp.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                cmd.Parameters.AddWithValue("@d", b.Data);
                cmd.Parameters.AddWithValue("@p", b.PrevHash);
                cmd.Parameters.AddWithValue("@h", b.Hash);

                cmd.ExecuteNonQuery();
            }
        }

        // Carga la cadena completa desde la BD
        private void LoadFromDB()
        {
            chain.Clear();

            using (MySqlConnection con = db.ObtenerConexion())
            {
                con.Open();
                string query = "SELECT * FROM blocks ORDER BY id ASC";

                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    // timestamp se guarda como VARCHAR → se convierte manualmente
                    DateTime ts = DateTime.Parse(reader.GetString("timestamp"));

                    Block b = new Block(
                        reader.GetInt32("id"),
                        ts,
                        reader.GetString("data"),
                        reader.GetString("prev_hash"),
                        reader.GetString("current_hash")
                    );

                    chain.Add(b);
                }
            }
        }

        // Devuelve la lista de bloques
        public List<Block> GetChain()
        {
            return chain;
        }

        // Recorre toda la cadena verificando hashes y prev_hash de cada bloque.
        // Si algo no coincide, la cadena es considerada corrupta.
        public bool VerifyChain(out string msg)
        {
            for (int i = 1; i < chain.Count; i++)
            {
                Block actual = chain[i];
                Block anterior = chain[i - 1];

                // Verifica que el hash almacenado no haya sido alterado
                if (actual.Hash != actual.CalculateHash())
                {
                    msg = $"El bloque {actual.ID} fue alterado.";
                    return false;
                }

                // Verifica que el prev_hash coincida con el hash del bloque anterior
                if (actual.PrevHash != anterior.Hash)
                {
                    msg = $"Integridad rota en bloque {actual.ID}.";
                    return false;
                }
            }

            msg = "La cadena es válida.";
            return true;
        }
    }
}
