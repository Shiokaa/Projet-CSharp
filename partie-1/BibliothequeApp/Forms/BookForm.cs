using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using BibliothequeApp.Models;
using BibliothequeApp.Services;

namespace BibliothequeApp.Forms
{
    public partial class BookForm : Form
    {
        private Livre? _livreEnEdition;
        private string? _cheminCouverture;

        public BookForm(Livre? livre = null)
        {
            InitializeComponent();
            _livreEnEdition = livre;
            LoadComboBoxes();
            
            if (_livreEnEdition != null)
            {
                lblTitleHeader.Text = "Éditer le livre";
                txtTitre.Text = _livreEnEdition.Titre;
                txtAuteur.Text = _livreEnEdition.Auteur;
                txtIsbn.Text = _livreEnEdition.Isbn;
                numAnnee.Value = _livreEnEdition.AnneePublication;
                
                foreach(Genre item in cbxGenre.Items) {
                    if (item.Id == _livreEnEdition.IdGenre) { cbxGenre.SelectedItem = item; break; }
                }
                foreach(Rayon item in cbxRayon.Items) {
                    if (item.Id == _livreEnEdition.IdRayon) { cbxRayon.SelectedItem = item; break; }
                }
                foreach(Etagere item in cbxEtagere.Items) {
                    if (item.Id == _livreEnEdition.IdEtagere) { cbxEtagere.SelectedItem = item; break; }
                }

                _cheminCouverture = _livreEnEdition.Couverture;
                if (!string.IsNullOrEmpty(_cheminCouverture))
                {
                    string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _cheminCouverture);
                    if (File.Exists(fullPath))
                        picCover.Image = Image.FromFile(fullPath);
                }
            }
            else
            {
                lblTitleHeader.Text = "Ajouter un livre";
            }
        }

        private void LoadComboBoxes()
        {
            try
            {
                var genres = DatabaseService.Instance.GetGenres();
                cbxGenre.Items.Clear();
                foreach (var g in genres) cbxGenre.Items.Add(g);
                if (cbxGenre.Items.Count > 0) cbxGenre.SelectedIndex = 0;

                var rayons = DatabaseService.Instance.GetRayons();
                cbxRayon.Items.Clear();
                foreach (var r in rayons) cbxRayon.Items.Add(r);
                if (cbxRayon.Items.Count > 0) cbxRayon.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur chargement des listes de référence : " + ex.Message);
            }
        }

        private void cbxRayon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxRayon.SelectedItem is Rayon selectedRayon)
            {
                try
                {
                    var etageres = DatabaseService.Instance.GetEtageres(selectedRayon.Id);
                    cbxEtagere.Items.Clear();
                    foreach (var etag in etageres) cbxEtagere.Items.Add(etag);
                    if (cbxEtagere.Items.Count > 0) cbxEtagere.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur chargement des étagères : " + ex.Message);
                }
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    picCover.Image = Image.FromFile(openFileDialog.FileName);
                    _cheminCouverture = openFileDialog.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur au chargement de l'image : " + ex.Message);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitre.Text) ||
                string.IsNullOrWhiteSpace(txtAuteur.Text) ||
                string.IsNullOrWhiteSpace(txtIsbn.Text) ||
                cbxGenre.SelectedItem == null ||
                cbxRayon.SelectedItem == null ||
                cbxEtagere.SelectedItem == null)
            {
                MessageBox.Show("Veuillez remplir tous les champs obligatoires.", "Erreur de validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var livre = new Livre
            {
                Titre = txtTitre.Text.Trim(),
                Auteur = txtAuteur.Text.Trim(),
                Isbn = txtIsbn.Text.Trim(),
                AnneePublication = (int)numAnnee.Value,
                IdGenre = ((Genre)cbxGenre.SelectedItem).Id,
                IdRayon = ((Rayon)cbxRayon.SelectedItem).Id,
                IdEtagere = ((Etagere)cbxEtagere.SelectedItem).Id,
                Couverture = _cheminCouverture
            };

            try
            {
                if (_livreEnEdition == null)
                {
                    DatabaseService.Instance.AddLivre(livre);
                }
                else
                {
                    livre.Id = _livreEnEdition.Id;
                    DatabaseService.Instance.UpdateLivre(livre);
                }
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur d'enregistrement : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
