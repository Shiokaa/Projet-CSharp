using System.Text.Json;
using MeteoApp.Models;

namespace MeteoApp.Services
{
    public class FavoritesService
    {
        private readonly string _filePath;
        private List<FavoriteCity> _favorites;

        public FavoritesService()
        {
            string appDir = AppDomain.CurrentDomain.BaseDirectory;
            _filePath = Path.Combine(appDir, "favorites.json");
            _favorites = LoadFavorites();
        }

        public List<FavoriteCity> GetFavorites() => _favorites;

        public bool AddFavorite(string cityName, string country)
        {
            if (_favorites.Any(f => f.Name.Equals(cityName, StringComparison.OrdinalIgnoreCase)))
                return false;

            _favorites.Add(new FavoriteCity
            {
                Name = cityName,
                Country = country,
                DateAdded = DateTime.Now
            });

            SaveFavorites();
            return true;
        }

        public bool RemoveFavorite(string cityName)
        {
            var fav = _favorites.FirstOrDefault(f => f.Name.Equals(cityName, StringComparison.OrdinalIgnoreCase));
            if (fav == null) return false;

            _favorites.Remove(fav);
            SaveFavorites();
            return true;
        }

        public bool IsFavorite(string cityName)
        {
            return _favorites.Any(f => f.Name.Equals(cityName, StringComparison.OrdinalIgnoreCase));
        }

        private List<FavoriteCity> LoadFavorites()
        {
            try
            {
                if (!File.Exists(_filePath))
                    return new List<FavoriteCity>();

                string json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<FavoriteCity>>(json) ?? new List<FavoriteCity>();
            }
            catch
            {
                return new List<FavoriteCity>();
            }
        }

        private void SaveFavorites()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(_favorites, options);
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la sauvegarde des favoris : {ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
