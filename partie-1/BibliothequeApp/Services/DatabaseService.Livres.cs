using System;
using System.Collections.Generic;
using MySqlConnector;
using BibliothequeApp.Models;

namespace BibliothequeApp.Services
{
    public partial class DatabaseService
    {
        public List<Livre> GetAllLivres()
        {
            return SearchLivres("");
        }

        public List<Livre> SearchLivres(string search)
        {
            var livres = new List<Livre>();

            string query = @"
                SELECT l.id_livre, l.titre, l.auteur, l.isbn, l.annee_publication, l.disponible, l.date_ajout,
                       l.id_genre, g.nom as genre_nom,
                       l.id_rayon, r.nom as rayon_nom,
                       l.id_etagere, e.nom as etagere_nom
                FROM livres l
                JOIN genres g ON l.id_genre = g.id_genre
                JOIN rayons r ON l.id_rayon = r.id_rayon
                JOIN etageres e ON l.id_etagere = e.id_etagere
                WHERE l.titre LIKE @search 
                   OR l.auteur LIKE @search 
                   OR l.isbn LIKE @search 
                   OR g.nom LIKE @search
                ORDER BY l.titre ASC";

            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@search", $"%{search}%");

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            livres.Add(new Livre
                            {
                                Id = reader.GetInt32("id_livre"),
                                Titre = reader.GetString("titre"),
                                Auteur = reader.GetString("auteur"),
                                Isbn = reader.GetString("isbn"),
                                AnneePublication = reader.GetInt32("annee_publication"),
                                Disponible = reader.GetBoolean("disponible"),
                                DateAjout = reader.GetDateTime("date_ajout"),
                                IdGenre = reader.GetInt32("id_genre"),
                                GenreNom = reader.GetString("genre_nom"),
                                IdRayon = reader.GetInt32("id_rayon"),
                                RayonNom = reader.GetString("rayon_nom"),
                                IdEtagere = reader.GetInt32("id_etagere"),
                                EtagereNom = reader.GetString("etagere_nom")
                            });
                        }
                    }
                }
            }

            return livres;
        }

        public string? GetLivreCouverture(int idLivre)
        {
            string query = "SELECT couverture FROM livres WHERE id_livre = @id";
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idLivre);
                    var result = cmd.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                        return result.ToString();
                }
            }
            return null;
        }

        public void AddLivre(Livre livre)
        {
            string query = @"
                INSERT INTO livres (titre, auteur, isbn, annee_publication, id_genre, id_rayon, id_etagere, couverture)
                VALUES (@titre, @auteur, @isbn, @annee, @id_genre, @id_rayon, @id_etagere, @couverture)";

            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@titre", livre.Titre);
                    cmd.Parameters.AddWithValue("@auteur", livre.Auteur);
                    cmd.Parameters.AddWithValue("@isbn", livre.Isbn);
                    cmd.Parameters.AddWithValue("@annee", livre.AnneePublication);
                    cmd.Parameters.AddWithValue("@id_genre", livre.IdGenre);
                    cmd.Parameters.AddWithValue("@id_rayon", livre.IdRayon);
                    cmd.Parameters.AddWithValue("@id_etagere", livre.IdEtagere);
                    
                    if (!string.IsNullOrEmpty(livre.Couverture))
                        cmd.Parameters.AddWithValue("@couverture", livre.Couverture);
                    else
                        cmd.Parameters.AddWithValue("@couverture", DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateLivre(Livre livre)
        {
            string query = @"
                UPDATE livres 
                SET titre = @titre, auteur = @auteur, isbn = @isbn, annee_publication = @annee,
                    id_genre = @id_genre, id_rayon = @id_rayon, id_etagere = @id_etagere, couverture = @couverture
                WHERE id_livre = @id";

            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", livre.Id);
                    cmd.Parameters.AddWithValue("@titre", livre.Titre);
                    cmd.Parameters.AddWithValue("@auteur", livre.Auteur);
                    cmd.Parameters.AddWithValue("@isbn", livre.Isbn);
                    cmd.Parameters.AddWithValue("@annee", livre.AnneePublication);
                    cmd.Parameters.AddWithValue("@id_genre", livre.IdGenre);
                    cmd.Parameters.AddWithValue("@id_rayon", livre.IdRayon);
                    cmd.Parameters.AddWithValue("@id_etagere", livre.IdEtagere);
                    
                    if (!string.IsNullOrEmpty(livre.Couverture))
                        cmd.Parameters.AddWithValue("@couverture", livre.Couverture);
                    else
                        cmd.Parameters.AddWithValue("@couverture", DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteLivre(int idLivre)
        {
            string query = "DELETE FROM livres WHERE id_livre = @id";
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idLivre);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ToggleDisponibilite(int idLivre, bool estDisponible)
        {
            string query = "UPDATE livres SET disponible = @dispo WHERE id_livre = @id";
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idLivre);
                    cmd.Parameters.AddWithValue("@dispo", estDisponible ? 1 : 0);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
