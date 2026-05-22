using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
//ALUMNA:Sarai Fregozo Aréchiga Código: 219523217
namespace mini_blockchain
{
    // Clase que representa un bloque individual
    public class Block
    {
        public int ID { get; set; }
        public DateTime Timestamp { get; set; }
        public string Data { get; set; }
        public string PrevHash { get; set; }
        public string Hash { get; set; }

        // Constructor para crear un nuevo bloque
        public Block(int id, string data, string prevHash)
        {
            ID = id;
            Data = data;
            PrevHash = prevHash;
            Timestamp = DateTime.Now;

            Hash = CalculateHash();
        }

        // Constructor para reconstruir desde la BD sin recalcular hash
        public Block(int id, DateTime timestamp, string data, string prevHash, string hash)
        {
            ID = id;
            Timestamp = timestamp;
            Data = data;
            PrevHash = prevHash;
            Hash = hash; // se usa el hash exacto guardado en la BD
        }

        // Calcula el hash del bloque
        public string CalculateHash()
        {
            // Evita discrepancias
            string rawData = ID
                + Timestamp.ToString("yyyy-MM-dd HH:mm:ss.fff")
                + Data
                + PrevHash;

            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }
    }
}