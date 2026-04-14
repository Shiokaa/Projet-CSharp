using System;
using System.Collections.Generic;
using MySqlConnector;
using BibliothequeApp.Models;

namespace BibliothequeApp.Services
{
    public partial class DatabaseService
    {
        public List<Genre> GetGenres()
        {
            var genres = new List<Genre>();
            string query = "SELECT id_genre, nom FROM genres ORDER BY nom ASC";
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            genres.Add(new Genre {
                                Id = reader.GetInt32("id_genre"),
                                Nom = reader.GetString("nom")
                            });
                        }
                    }
                }
            }
            return genres;
        }

        public List<Rayon> GetRayons()
        {
            var rayons = new List<Rayon>();
            string query = "SELECT id_rayon, nom FROM rayons ORDER BY nom ASC";
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            rayons.Add(new Rayon {
                                Id = reader.GetInt32("id_rayon"),
                                Nom = reader.GetString("nom")
                            });
                        }
                    }
                }
            }
            return rayons;
        }

        public List<Etagere> GetEtageres(int? rayonId = null)
        {
            var etageres = new List<Etagere>();
            string query = "SELECT id_etagere, nom, id_rayon FROM etageres " + 
                           (rayonId.HasValue ? "WHERE id_rayon = @rayonId " : "") + 
                           "ORDER BY nom ASC";
            
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                {
                    if (rayonId.HasValue)
                        cmd.Parameters.AddWithValue("@rayonId", rayonId.Value);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            etageres.Add(new Etagere {
                                Id = reader.GetInt32("id_etagere"),
                                Nom = reader.GetString("nom"),
                                IdRayon = reader.GetInt32("id_rayon")
                            });
                        }
                    }
                }
            }
            return etageres;
        }
    }
}
