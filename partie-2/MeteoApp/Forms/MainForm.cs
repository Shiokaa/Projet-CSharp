using System.Text.Json;
using MeteoApp.Models;
using MeteoApp.Services;

namespace MeteoApp.Forms
{
    public class MainForm : Form
    {
        private WeatherService _weatherService = null!;
        private FavoritesService _favoritesService = null!;

        private bool _isDarkMode = false;

        private string _currentCity = string.Empty;
        private string _currentCountry = string.Empty;

        private Panel _topBar = null!;
        private TextBox _searchBox = null!;
        private Button _searchButton = null!;
        private Button _favoriteButton = null!;
        private Button _darkModeButton = null!;

        private Panel _mainPanel = null!;

        private Panel _currentWeatherPanel = null!;
        private Label _cityLabel = null!;
        private Label _dateLabel = null!;
        private PictureBox _currentIcon = null!;
        private Label _tempLabel = null!;
        private Label _descriptionLabel = null!;
        private Label _feelsLikeLabel = null!;
        private Label _humidityLabel = null!;
        private Label _windLabel = null!;
        private Label _cloudLabel = null!;
        private Label _uvLabel = null!;

        private Label _forecastTitle = null!;
        private FlowLayoutPanel _forecastPanel = null!;

        private Panel _sidePanel = null!;
        private Label _favoritesTitle = null!;
        private ListBox _favoritesListBox = null!;
        private Button _removeFavoriteButton = null!;

        private Label _statusLabel = null!;
        private Panel _welcomePanel = null!;

        private Color _bgColor = Color.FromArgb(245, 247, 250);
        private Color _panelColor = Color.White;
        private Color _textColor = Color.FromArgb(30, 30, 30);
        private Color _secondaryText = Color.FromArgb(100, 100, 110);
        private Color _accentColor = Color.FromArgb(59, 130, 246);
        private Color _accentHover = Color.FromArgb(37, 99, 235);
        private Color _sidebarColor = Color.FromArgb(248, 250, 252);
        private Color _cardBorderColor = Color.FromArgb(226, 232, 240);
        private Color _dangerColor = Color.FromArgb(239, 68, 68);

        public MainForm()
        {
            InitializeServices();
            InitializeUI();
            ApplyTheme();
        }

        private void InitializeServices()
        {
            string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
            string apiKey = "VOTRE_CLE_ICI";

            if (File.Exists(configPath))
            {
                try
                {
                    string json = File.ReadAllText(configPath);
                    using JsonDocument doc = JsonDocument.Parse(json);
                    apiKey = doc.RootElement.GetProperty("WeatherApiKey").GetString() ?? apiKey;
                }
                catch { }
            }

            if (apiKey == "VOTRE_CLE_ICI")
            {
                MessageBox.Show(
                    "Veuillez configurer votre clé API WeatherAPI.com dans le fichier appsettings.json.\n\n" +
                    "1. Inscrivez-vous sur https://www.weatherapi.com/signup.aspx\n" +
                    "2. Copiez votre clé API\n" +
                    "3. Collez-la dans appsettings.json",
                    "Configuration requise", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            _weatherService = new WeatherService(apiKey);
            _favoritesService = new FavoritesService();
        }

        private void InitializeUI()
        {
            Text = "MeteoApp – Application Météo";
            Size = new Size(1100, 750);
            MinimumSize = new Size(900, 600);
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("Segoe UI", 10F);
            DoubleBuffered = true;

            _topBar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 65,
                Padding = new Padding(20, 12, 20, 12)
            };

            _searchBox = new TextBox
            {
                PlaceholderText = "🔍 Rechercher une ville...",
                Font = new Font("Segoe UI", 12F),
                Width = 350,
                Height = 38,
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(20, 14)
            };
            _searchBox.KeyDown += SearchBox_KeyDown;

            _searchButton = new Button
            {
                Text = "Rechercher",
                Font = new Font("Segoe UI Semibold", 10F),
                Size = new Size(120, 38),
                Location = new Point(380, 14),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            _searchButton.FlatAppearance.BorderSize = 0;
            _searchButton.Click += SearchButton_Click;

            _favoriteButton = new Button
            {
                Text = "☆ Favori",
                Font = new Font("Segoe UI", 10F),
                Size = new Size(100, 38),
                Location = new Point(510, 14),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Visible = false
            };
            _favoriteButton.FlatAppearance.BorderSize = 1;
            _favoriteButton.Click += FavoriteButton_Click;

            _darkModeButton = new Button
            {
                Text = "🌙",
                Font = new Font("Segoe UI", 14F),
                Size = new Size(45, 38),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            _darkModeButton.FlatAppearance.BorderSize = 0;
            _darkModeButton.Click += DarkModeButton_Click;

            _topBar.Controls.AddRange(new Control[] { _searchBox, _searchButton, _favoriteButton, _darkModeButton });
            _topBar.Resize += (s, e) =>
            {
                _darkModeButton.Location = new Point(_topBar.Width - 65, 14);
            };

            _sidePanel = new Panel
            {
                Dock = DockStyle.Right,
                Width = 240,
                Padding = new Padding(12)
            };

            _favoritesTitle = new Label
            {
                Text = "⭐ Favoris",
                Font = new Font("Segoe UI Semibold", 13F),
                Dock = DockStyle.Top,
                Height = 40,
                TextAlign = ContentAlignment.MiddleLeft
            };

            _favoritesListBox = new ListBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 11F),
                BorderStyle = BorderStyle.None,
                ItemHeight = 32
            };
            _favoritesListBox.DoubleClick += FavoritesListBox_DoubleClick;

            _removeFavoriteButton = new Button
            {
                Text = "🗑 Supprimer",
                Dock = DockStyle.Bottom,
                Height = 36,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F),
                Cursor = Cursors.Hand
            };
            _removeFavoriteButton.FlatAppearance.BorderSize = 0;
            _removeFavoriteButton.Click += RemoveFavoriteButton_Click;

            _sidePanel.Controls.Add(_favoritesListBox);
            _sidePanel.Controls.Add(_removeFavoriteButton);
            _sidePanel.Controls.Add(_favoritesTitle);

            _mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(25, 15, 25, 15),
                AutoScroll = true
            };

            _welcomePanel = new Panel
            {
                Dock = DockStyle.Fill
            };
            var welcomeLabel = new Label
            {
                Text = "☁️ Bienvenue sur MeteoApp\n\nRecherchez une ville pour afficher les prévisions météo.",
                Font = new Font("Segoe UI", 16F),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                ForeColor = Color.FromArgb(150, 150, 160)
            };
            _welcomePanel.Controls.Add(welcomeLabel);

            _currentWeatherPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 220,
                Padding = new Padding(25, 20, 25, 20),
                Visible = false
            };

            _cityLabel = new Label
            {
                Text = "",
                Font = new Font("Segoe UI Semibold", 22F),
                Location = new Point(25, 15),
                AutoSize = true
            };

            _dateLabel = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 10F),
                Location = new Point(25, 55),
                AutoSize = true
            };

            _currentIcon = new PictureBox
            {
                Size = new Size(80, 80),
                Location = new Point(25, 80),
                SizeMode = PictureBoxSizeMode.Zoom
            };

            _tempLabel = new Label
            {
                Text = "",
                Font = new Font("Segoe UI Light", 42F),
                Location = new Point(115, 72),
                AutoSize = true
            };

            _descriptionLabel = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 12F),
                Location = new Point(115, 135),
                AutoSize = true
            };

            _feelsLikeLabel = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 10F),
                Location = new Point(115, 160),
                AutoSize = true
            };

            _humidityLabel = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 11F),
                Size = new Size(200, 30),
                Location = new Point(420, 80),
                TextAlign = ContentAlignment.MiddleLeft
            };

            _windLabel = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 11F),
                Size = new Size(200, 30),
                Location = new Point(420, 115),
                TextAlign = ContentAlignment.MiddleLeft
            };

            _cloudLabel = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 11F),
                Size = new Size(200, 30),
                Location = new Point(420, 150),
                TextAlign = ContentAlignment.MiddleLeft
            };

            _uvLabel = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 11F),
                Size = new Size(200, 30),
                Location = new Point(620, 80),
                TextAlign = ContentAlignment.MiddleLeft
            };

            _currentWeatherPanel.Controls.AddRange(new Control[]
            {
                _cityLabel, _dateLabel, _currentIcon, _tempLabel,
                _descriptionLabel, _feelsLikeLabel,
                _humidityLabel, _windLabel, _cloudLabel, _uvLabel
            });

            _forecastTitle = new Label
            {
                Text = "📅 Prévisions",
                Font = new Font("Segoe UI Semibold", 14F),
                Dock = DockStyle.Top,
                Height = 45,
                TextAlign = ContentAlignment.BottomLeft,
                Padding = new Padding(0, 10, 0, 5),
                Visible = false
            };

            _forecastPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 240,
                AutoScroll = true,
                WrapContents = true,
                Padding = new Padding(0, 5, 0, 5),
                Visible = false
            };

            _statusLabel = new Label
            {
                Dock = DockStyle.Bottom,
                Height = 28,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 9F),
                Padding = new Padding(20, 0, 0, 0),
                Text = "Prêt"
            };

            _mainPanel.Controls.Add(_forecastPanel);
            _mainPanel.Controls.Add(_forecastTitle);
            _mainPanel.Controls.Add(_currentWeatherPanel);
            _mainPanel.Controls.Add(_welcomePanel);

            var separator = new Panel
            {
                Dock = DockStyle.Right,
                Width = 1,
                BackColor = _cardBorderColor
            };

            Controls.Add(_mainPanel);
            Controls.Add(separator);
            Controls.Add(_sidePanel);
            Controls.Add(_statusLabel);
            Controls.Add(_topBar);

            RefreshFavoritesList();
        }

        private void SearchBox_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                _ = SearchWeatherAsync(_searchBox.Text.Trim());
            }
        }

        private void SearchButton_Click(object? sender, EventArgs e)
        {
            _ = SearchWeatherAsync(_searchBox.Text.Trim());
        }

        private async Task SearchWeatherAsync(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                MessageBox.Show("Veuillez entrer un nom de ville.", "Attention",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _statusLabel.Text = $"Recherche en cours pour \"{city}\"...";
            _searchButton.Enabled = false;
            _searchBox.Enabled = false;

            try
            {
                var weather = await _weatherService.GetForecastAsync(city);

                if (weather == null)
                {
                    MessageBox.Show($"Impossible de trouver la météo pour \"{city}\".\n" +
                        "Vérifiez le nom de la ville ou votre connexion internet.",
                        "Ville introuvable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _statusLabel.Text = "Ville introuvable.";
                    return;
                }

                _currentCity = weather.Location.Name;
                _currentCountry = weather.Location.Country;
                DisplayCurrentWeather(weather);
                await DisplayForecastAsync(weather);
                UpdateFavoriteButtonState();

                _statusLabel.Text = $"Météo chargée pour {weather.Location.Name}, {weather.Location.Country}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _statusLabel.Text = "Erreur lors de la recherche.";
            }
            finally
            {
                _searchButton.Enabled = true;
                _searchBox.Enabled = true;
                _searchBox.Focus();
            }
        }

        private void DisplayCurrentWeather(WeatherResponse weather)
        {
            _welcomePanel.Visible = false;
            _currentWeatherPanel.Visible = true;
            _favoriteButton.Visible = true;

            _cityLabel.Text = $"{weather.Location.Name}, {weather.Location.Country}";
            _dateLabel.Text = weather.Location.LocalTime;
            _dateLabel.ForeColor = _secondaryText;

            _tempLabel.Text = $"{weather.Current.TempC:0.0}°C";
            _descriptionLabel.Text = weather.Current.Condition.Text;
            _descriptionLabel.ForeColor = _secondaryText;
            _feelsLikeLabel.Text = $"Ressenti : {weather.Current.FeelsLikeC:0.0}°C";
            _feelsLikeLabel.ForeColor = _secondaryText;

            _humidityLabel.Text = $"💧 Humidité : {weather.Current.Humidity}%";
            _windLabel.Text = $"💨 Vent : {weather.Current.WindKph} km/h";
            _cloudLabel.Text = $"☁️ Nuages : {weather.Current.Cloud}%";
            _uvLabel.Text = $"☀️ UV : {weather.Current.UV}";

            _ = LoadIconAsync(_currentIcon, weather.Current.Condition.Icon);
        }

        private async Task DisplayForecastAsync(WeatherResponse weather)
        {
            _forecastTitle.Visible = true;
            _forecastPanel.Visible = true;
            _forecastPanel.Controls.Clear();

            foreach (var day in weather.Forecast.ForecastDays)
            {
                var card = CreateForecastCard(day);
                _forecastPanel.Controls.Add(card);
            }

            int index = 0;
            foreach (var day in weather.Forecast.ForecastDays)
            {
                if (index < _forecastPanel.Controls.Count)
                {
                    var card = _forecastPanel.Controls[index] as Panel;
                    if (card != null)
                    {
                        var iconBox = card.Controls.OfType<PictureBox>().FirstOrDefault();
                        if (iconBox != null)
                        {
                            await LoadIconAsync(iconBox, day.Day.Condition.Icon);
                        }
                    }
                }
                index++;
            }
        }

        private Panel CreateForecastCard(ForecastDay day)
        {
            var card = new Panel
            {
                Size = new Size(230, 210),
                Margin = new Padding(8),
                Padding = new Padding(15),
                BackColor = _panelColor
            };
            card.Paint += (s, e) =>
            {
                using var pen = new Pen(_cardBorderColor, 1);
                var rect = new Rectangle(0, 0, card.Width - 1, card.Height - 1);
                int radius = 12;
                using var path = RoundedRect(rect, radius);
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using var brush = new SolidBrush(card.BackColor);
                e.Graphics.FillPath(brush, path);
                e.Graphics.DrawPath(pen, path);
            };

            string dateStr = day.Date;
            string formattedDate = dateStr;
            try
            {
                var dt = DateTime.Parse(dateStr);
                formattedDate = dt.ToString("dddd dd MMM", new System.Globalization.CultureInfo("fr-FR"));
                formattedDate = char.ToUpper(formattedDate[0]) + formattedDate[1..];
            }
            catch { }

            var dateLabel = new Label
            {
                Text = formattedDate,
                Font = new Font("Segoe UI Semibold", 11F),
                Location = new Point(15, 12),
                AutoSize = true,
                ForeColor = _textColor,
                BackColor = Color.Transparent
            };

            var iconBox = new PictureBox
            {
                Size = new Size(50, 50),
                Location = new Point(15, 40),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent
            };

            var tempLabel = new Label
            {
                Text = $"🌡️ {day.Day.MinTempC:0}° / {day.Day.MaxTempC:0}°C",
                Font = new Font("Segoe UI", 11F),
                Location = new Point(75, 48),
                AutoSize = true,
                ForeColor = _textColor,
                BackColor = Color.Transparent
            };

            var condLabel = new Label
            {
                Text = day.Day.Condition.Text,
                Font = new Font("Segoe UI", 9F),
                Location = new Point(15, 98),
                Size = new Size(200, 20),
                ForeColor = _secondaryText,
                BackColor = Color.Transparent
            };

            var humidityLabel = new Label
            {
                Text = $"💧 Humidité : {day.Day.AvgHumidity:0}%",
                Font = new Font("Segoe UI", 9.5F),
                Location = new Point(15, 122),
                AutoSize = true,
                ForeColor = _secondaryText,
                BackColor = Color.Transparent
            };

            var windLabel = new Label
            {
                Text = $"💨 Vent : {day.Day.MaxWindKph:0} km/h",
                Font = new Font("Segoe UI", 9.5F),
                Location = new Point(15, 145),
                AutoSize = true,
                ForeColor = _secondaryText,
                BackColor = Color.Transparent
            };

            var rainLabel = new Label
            {
                Text = $"🌧️ Pluie : {day.Day.ChanceOfRain}%",
                Font = new Font("Segoe UI", 9.5F),
                Location = new Point(15, 168),
                AutoSize = true,
                ForeColor = _secondaryText,
                BackColor = Color.Transparent
            };

            card.Controls.AddRange(new Control[]
            {
                dateLabel, iconBox, tempLabel, condLabel,
                humidityLabel, windLabel, rainLabel
            });

            return card;
        }

        private async Task LoadIconAsync(PictureBox pictureBox, string iconUrl)
        {
            var image = await _weatherService.GetWeatherIconAsync(iconUrl);
            if (image != null)
            {
                pictureBox.Image?.Dispose();
                pictureBox.Image = image;
            }
        }

        private void FavoriteButton_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentCity)) return;

            if (_favoritesService.IsFavorite(_currentCity))
            {
                _favoritesService.RemoveFavorite(_currentCity);
                _statusLabel.Text = $"{_currentCity} retiré des favoris.";
            }
            else
            {
                _favoritesService.AddFavorite(_currentCity, _currentCountry);
                _statusLabel.Text = $"{_currentCity} ajouté aux favoris !";
            }

            UpdateFavoriteButtonState();
            RefreshFavoritesList();
        }

        private void UpdateFavoriteButtonState()
        {
            if (string.IsNullOrEmpty(_currentCity))
            {
                _favoriteButton.Visible = false;
                return;
            }

            _favoriteButton.Visible = true;

            if (_favoritesService.IsFavorite(_currentCity))
            {
                _favoriteButton.Text = "★ Favori";
                _favoriteButton.BackColor = Color.FromArgb(254, 243, 199);
                _favoriteButton.ForeColor = Color.FromArgb(180, 130, 0);
                _favoriteButton.FlatAppearance.BorderColor = Color.FromArgb(253, 224, 71);
            }
            else
            {
                _favoriteButton.Text = "☆ Favori";
                _favoriteButton.BackColor = _panelColor;
                _favoriteButton.ForeColor = _textColor;
                _favoriteButton.FlatAppearance.BorderColor = _cardBorderColor;
            }
        }

        private void RefreshFavoritesList()
        {
            _favoritesListBox.Items.Clear();
            foreach (var fav in _favoritesService.GetFavorites())
            {
                _favoritesListBox.Items.Add($"{fav.Name} ({fav.Country})");
            }
        }

        private void FavoritesListBox_DoubleClick(object? sender, EventArgs e)
        {
            if (_favoritesListBox.SelectedItem is string item)
            {
                string city = item.Split('(')[0].Trim();
                _searchBox.Text = city;
                _ = SearchWeatherAsync(city);
            }
        }

        private void RemoveFavoriteButton_Click(object? sender, EventArgs e)
        {
            if (_favoritesListBox.SelectedItem is string item)
            {
                string city = item.Split('(')[0].Trim();
                _favoritesService.RemoveFavorite(city);
                RefreshFavoritesList();
                UpdateFavoriteButtonState();
                _statusLabel.Text = $"{city} retiré des favoris.";
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un favori à supprimer.",
                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DarkModeButton_Click(object? sender, EventArgs e)
        {
            _isDarkMode = !_isDarkMode;
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            if (_isDarkMode)
            {
                _bgColor = Color.FromArgb(17, 24, 39);
                _panelColor = Color.FromArgb(31, 41, 55);
                _textColor = Color.FromArgb(243, 244, 246);
                _secondaryText = Color.FromArgb(156, 163, 175);
                _accentColor = Color.FromArgb(96, 165, 250);
                _accentHover = Color.FromArgb(59, 130, 246);
                _sidebarColor = Color.FromArgb(24, 32, 48);
                _cardBorderColor = Color.FromArgb(55, 65, 81);
                _darkModeButton.Text = "☀️";
            }
            else
            {
                _bgColor = Color.FromArgb(245, 247, 250);
                _panelColor = Color.White;
                _textColor = Color.FromArgb(30, 30, 30);
                _secondaryText = Color.FromArgb(100, 100, 110);
                _accentColor = Color.FromArgb(59, 130, 246);
                _accentHover = Color.FromArgb(37, 99, 235);
                _sidebarColor = Color.FromArgb(248, 250, 252);
                _cardBorderColor = Color.FromArgb(226, 232, 240);
                _darkModeButton.Text = "🌙";
            }

            BackColor = _bgColor;

            _topBar.BackColor = _panelColor;

            _searchBox.BackColor = _isDarkMode ? Color.FromArgb(55, 65, 81) : Color.White;
            _searchBox.ForeColor = _textColor;

            _searchButton.BackColor = _accentColor;
            _searchButton.ForeColor = Color.White;

            _darkModeButton.BackColor = _panelColor;
            _darkModeButton.ForeColor = _textColor;

            _sidePanel.BackColor = _sidebarColor;
            _favoritesTitle.ForeColor = _textColor;
            _favoritesListBox.BackColor = _sidebarColor;
            _favoritesListBox.ForeColor = _textColor;

            _removeFavoriteButton.BackColor = _dangerColor;
            _removeFavoriteButton.ForeColor = Color.White;

            _mainPanel.BackColor = _bgColor;
            _currentWeatherPanel.BackColor = _panelColor;

            _cityLabel.ForeColor = _textColor;
            _dateLabel.ForeColor = _secondaryText;
            _tempLabel.ForeColor = _textColor;
            _descriptionLabel.ForeColor = _secondaryText;
            _feelsLikeLabel.ForeColor = _secondaryText;
            _humidityLabel.ForeColor = _secondaryText;
            _windLabel.ForeColor = _secondaryText;
            _cloudLabel.ForeColor = _secondaryText;
            _uvLabel.ForeColor = _secondaryText;

            _forecastTitle.ForeColor = _textColor;
            _forecastTitle.BackColor = _bgColor;

            _statusLabel.BackColor = _panelColor;
            _statusLabel.ForeColor = _secondaryText;

            _welcomePanel.BackColor = _bgColor;
            foreach (Control c in _welcomePanel.Controls)
            {
                c.BackColor = _bgColor;
                c.ForeColor = _secondaryText;
            }

            UpdateFavoriteButtonState();

            foreach (Control card in _forecastPanel.Controls)
            {
                if (card is Panel p)
                {
                    p.BackColor = _panelColor;
                    foreach (Control child in p.Controls)
                    {
                        if (child is Label lbl)
                        {
                            if (lbl.Font.Bold || lbl.Font.Size >= 11)
                                lbl.ForeColor = _textColor;
                            else
                                lbl.ForeColor = _secondaryText;
                            lbl.BackColor = Color.Transparent;
                        }
                        else if (child is PictureBox pic)
                        {
                            pic.BackColor = Color.Transparent;
                        }
                    }
                    p.Invalidate();
                }
            }

            _forecastPanel.BackColor = _bgColor;
            Invalidate(true);
        }

        private static System.Drawing.Drawing2D.GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _weatherService.Dispose();
            base.OnFormClosed(e);
        }
    }
}
