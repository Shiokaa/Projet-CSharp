using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using BibliothequeApp.Models;
using BibliothequeApp.Services;

namespace BibliothequeApp.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadLivres();
        }

        private void LoadLivres(string search = "")
        {
            try
            {
                var livres = string.IsNullOrWhiteSpace(search) 
                    ? DatabaseService.Instance.GetAllLivres() 
                    : DatabaseService.Instance.SearchLivres(search);

                dgvLivres.DataSource = livres;
                
                if(dgvLivres.Columns["Id"] != null) dgvLivres.Columns["Id"].Visible = false;
                if(dgvLivres.Columns["IdGenre"] != null) dgvLivres.Columns["IdGenre"].Visible = false;
                if(dgvLivres.Columns["IdRayon"] != null) dgvLivres.Columns["IdRayon"].Visible = false;
                if(dgvLivres.Columns["IdEtagere"] != null) dgvLivres.Columns["IdEtagere"].Visible = false;
                if(dgvLivres.Columns["Couverture"] != null) dgvLivres.Columns["Couverture"].Visible = false;

                if(dgvLivres.Columns["Titre"] != null) dgvLivres.Columns["Titre"].HeaderText = "Titre";
                if(dgvLivres.Columns["Auteur"] != null) dgvLivres.Columns["Auteur"].HeaderText = "Auteur";
                if(dgvLivres.Columns["Isbn"] != null) dgvLivres.Columns["Isbn"].HeaderText = "ISBN";
                if(dgvLivres.Columns["AnneePublication"] != null) dgvLivres.Columns["AnneePublication"].HeaderText = "Année";
                if(dgvLivres.Columns["Disponible"] != null) dgvLivres.Columns["Disponible"].HeaderText = "Dispo";
                if(dgvLivres.Columns["GenreNom"] != null) dgvLivres.Columns["GenreNom"].HeaderText = "Genre";
                if(dgvLivres.Columns["RayonNom"] != null) dgvLivres.Columns["RayonNom"].HeaderText = "Rayon";
                if(dgvLivres.Columns["EtagereNom"] != null) dgvLivres.Columns["EtagereNom"].HeaderText = "Étagère";
                if(dgvLivres.Columns["DateAjout"] != null) dgvLivres.Columns["DateAjout"].HeaderText = "Ajouté le";
                
                picCover.Image = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de connexion à la base de données : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadLivres(txtSearch.Text);
        }

        private void dgvLivres_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvLivres.Rows[e.RowIndex];
                int idLivre = Convert.ToInt32(row.Cells["Id"].Value);

                var cheminCouverture = DatabaseService.Instance.GetLivreCouverture(idLivre);
                if (!string.IsNullOrEmpty(cheminCouverture))
                {
                    string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, cheminCouverture);
                    if (File.Exists(fullPath))
                        picCover.Image = Image.FromFile(fullPath);
                    else
                        picCover.Image = null;
                }
                else
                {
                    picCover.Image = null;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var form = new BookForm(null);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadLivres(txtSearch.Text);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvLivres.SelectedRows.Count > 0)
            {
                var livre = (Livre)dgvLivres.SelectedRows[0].DataBoundItem;
                
                livre.Couverture = DatabaseService.Instance.GetLivreCouverture(livre.Id);

                var form = new BookForm(livre);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadLivres(txtSearch.Text);
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un livre à modifier.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvLivres.SelectedRows.Count > 0)
            {
                var livre = (Livre)dgvLivres.SelectedRows[0].DataBoundItem;
                var res = MessageBox.Show($"Êtes-vous sûr de vouloir supprimer '{livre.Titre}' ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (res == DialogResult.Yes)
                {
                    try
                    {
                        DatabaseService.Instance.DeleteLivre(livre.Id);
                        LoadLivres(txtSearch.Text);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Impossible de supprimer le livre (il est peut-être associé à un emprunt) : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un livre à supprimer.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBorrows_Click(object sender, EventArgs e)
        {
            var form = new BorrowForm();
            form.ShowDialog();
            LoadLivres(txtSearch.Text);
        }

        private void btnToggleDispo_Click(object sender, EventArgs e)
        {
            if (dgvLivres.SelectedRows.Count > 0)
            {
                var livre = (Livre)dgvLivres.SelectedRows[0].DataBoundItem;
                try
                {
                    DatabaseService.Instance.ToggleDisponibilite(livre.Id, !livre.Disponible);
                    LoadLivres(txtSearch.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur lors du changement de disponibilité : " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un livre.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            var form = new AdherentForm();
            form.ShowDialog();
        }
    }
}
