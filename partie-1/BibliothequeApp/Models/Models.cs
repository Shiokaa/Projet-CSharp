using System;

namespace BibliothequeApp.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;

        public override string ToString() => Nom;
    }

    public class Rayon
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;

        public override string ToString() => Nom;
    }

    public class Etagere
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public int IdRayon { get; set; }

        public override string ToString() => Nom;
    }

    public class Livre
    {
        public int Id { get; set; }
        public string Titre { get; set; } = string.Empty;
        public string Auteur { get; set; } = string.Empty;
        public string Isbn { get; set; } = string.Empty;
        public int AnneePublication { get; set; }
        public int IdGenre { get; set; }
        public string GenreNom { get; set; } = string.Empty;
        public int IdRayon { get; set; }
        public string RayonNom { get; set; } = string.Empty;
        public int IdEtagere { get; set; }
        public string EtagereNom { get; set; } = string.Empty;
        public bool Disponible { get; set; } = true;
        public string? Couverture { get; set; }
        public DateTime DateAjout { get; set; }
    }

    public class Adherent
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;

        public override string ToString() => $"{Nom} {Prenom}";
    }

    public class Emprunt
    {
        public int Id { get; set; }
        public int IdLivre { get; set; }
        public string LivreTitre { get; set; } = string.Empty;
        public int IdAdherent { get; set; }
        public string AdherentNom { get; set; } = string.Empty;
        public DateTime DateEmprunt { get; set; }
        public DateTime DateRetourPrevue { get; set; }
        public DateTime? DateRetourReelle { get; set; }
    }
}
