using System;
using System.Collections.Generic;
using MySqlConnector;
using BibliothequeApp.Models;

namespace BibliothequeApp.Services
{
    public partial class DatabaseService
    {
        public List<Emprunt> GetEmpruntsActifs()
        {
            var emprunts = new List<Emprunt>();
            string query = @"
                SELECT e.id_emprunt, e.id_livre, l.titre as livre_titre, e.id_adherent, 
                       CONCAT(a.prenom, ' ', a.nom) as adherent_nom, e.date_emprunt, e.date_retour_prevue, e.date_retour_reelle
                FROM emprunts e
                JOIN livres l ON e.id_livre = l.id_livre
                JOIN adherents a ON e.id_adherent = a.id_adherent
                WHERE e.date_retour_reelle IS NULL
                ORDER BY e.date_retour_prevue ASC";

            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            emprunts.Add(new Emprunt {
                                Id = reader.GetInt32("id_emprunt"),
                                IdLivre = reader.GetInt32("id_livre"),
                                LivreTitre = reader.GetString("livre_titre"),
                                IdAdherent = reader.GetInt32("id_adherent"),
                                AdherentNom = reader.GetString("adherent_nom"),
                                DateEmprunt = reader.GetDateTime("date_emprunt"),
                                DateRetourPrevue = reader.GetDateTime("date_retour_prevue"),
                                DateRetourReelle = reader.IsDBNull(reader.GetOrdinal("date_retour_reelle")) ? null : reader.GetDateTime("date_retour_reelle")
                            });
                        }
                    }
                }
            }
            return emprunts;
        }

        public void EmprunterLivre(int idLivre, int idAdherent, DateTime dateRetourPrevue)
        {
            string query = @"
                INSERT INTO emprunts (id_livre, id_adherent, date_emprunt, date_retour_prevue) 
                VALUES (@livre, @adherent, @date_emprunt, @date_prevue)";
            
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@livre", idLivre);
                    cmd.Parameters.AddWithValue("@adherent", idAdherent);
                    cmd.Parameters.AddWithValue("@date_emprunt", DateTime.Now.Date);
                    cmd.Parameters.AddWithValue("@date_prevue", dateRetourPrevue.Date);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void RetournerLivre(int idEmprunt)
        {
            string query = "UPDATE emprunts SET date_retour_reelle = @date_reelle WHERE id_emprunt = @id";
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idEmprunt);
                    cmd.Parameters.AddWithValue("@date_reelle", DateTime.Now.Date);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
