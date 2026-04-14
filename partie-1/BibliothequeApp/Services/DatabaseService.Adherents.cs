using System;
using System.Collections.Generic;
using MySqlConnector;
using BibliothequeApp.Models;

namespace BibliothequeApp.Services
{
    public partial class DatabaseService
    {
        public List<Adherent> GetAdherents()
        {
            var adherents = new List<Adherent>();
            string query = "SELECT id_adherent, nom, prenom, email, telephone FROM adherents ORDER BY nom ASC";
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            adherents.Add(new Adherent {
                                Id = reader.GetInt32("id_adherent"),
                                Nom = reader.GetString("nom"),
                                Prenom = reader.GetString("prenom"),
                                Email = reader.GetString("email"),
                                Telephone = reader.IsDBNull(reader.GetOrdinal("telephone")) ? "" : reader.GetString("telephone")
                            });
                        }
                    }
                }
            }
            return adherents;
        }

        public void AddAdherent(Adherent adherent)
        {
            string query = "INSERT INTO adherents (nom, prenom, email, telephone) VALUES (@nom, @prenom, @email, @tel)";
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nom", adherent.Nom);
                    cmd.Parameters.AddWithValue("@prenom", adherent.Prenom);
                    cmd.Parameters.AddWithValue("@email", adherent.Email);
                    if (string.IsNullOrWhiteSpace(adherent.Telephone))
                        cmd.Parameters.AddWithValue("@tel", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@tel", adherent.Telephone);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
