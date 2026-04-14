using System;
using System.Linq;
using System.Windows.Forms;
using BibliothequeApp.Models;
using BibliothequeApp.Services;

namespace BibliothequeApp.Forms
{
    public partial class BorrowForm : Form
    {
        public BorrowForm()
        {
            InitializeComponent();
        }

        private void BorrowForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var emprunts = DatabaseService.Instance.GetEmpruntsActifs();
                dgvEmprunts.DataSource = emprunts;
                
                if (dgvEmprunts.Columns["Id"] != null) dgvEmprunts.Columns["Id"].Visible = false;
                if (dgvEmprunts.Columns["IdLivre"] != null) dgvEmprunts.Columns["IdLivre"].Visible = false;
                if (dgvEmprunts.Columns["IdAdherent"] != null) dgvEmprunts.Columns["IdAdherent"].Visible = false;
                if (dgvEmprunts.Columns["DateRetourReelle"] != null) dgvEmprunts.Columns["DateRetourReelle"].Visible = false;

                if (dgvEmprunts.Columns["LivreTitre"] != null) dgvEmprunts.Columns["LivreTitre"].HeaderText = "Livre";
                if (dgvEmprunts.Columns["AdherentNom"] != null) dgvEmprunts.Columns["AdherentNom"].HeaderText = "Adhérent";
                if (dgvEmprunts.Columns["DateEmprunt"] != null) dgvEmprunts.Columns["DateEmprunt"].HeaderText = "Date Emprunt";
                if (dgvEmprunts.Columns["DateRetourPrevue"] != null) dgvEmprunts.Columns["DateRetourPrevue"].HeaderText = "Retour Prévu";

                var livres = DatabaseService.Instance.GetAllLivres().Where(l => l.Disponible).ToList();
                cbxLivre.DisplayMember = "Titre";
                cbxLivre.ValueMember = "Id";
                cbxLivre.DataSource = livres;

                LoadAdherents();

                dtpRetourPrevu.Value = DateTime.Now.AddDays(14);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur au chargement des emprunts : " + ex.Message);
            }
        }

        private void LoadAdherents()
        {
            var adherents = DatabaseService.Instance.GetAdherents();
            cbxAdherent.DisplayMember = "ToString";
            cbxAdherent.ValueMember = "Id";
            cbxAdherent.DataSource = adherents;
        }

        private void btnAddAdherent_Click(object sender, EventArgs e)
        {
            using (var form = new AdherentForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadAdherents(); 
                }
            }
        }

        private void btnEmprunter_Click(object sender, EventArgs e)
        {
            if (cbxLivre.SelectedItem == null || cbxAdherent.SelectedItem == null)
            {
                MessageBox.Show("Sélectionnez un livre et un adhérent.");
                return;
            }

            if (dtpRetourPrevu.Value.Date <= DateTime.Now.Date)
            {
                MessageBox.Show("La date de retour prévue doit être dans le futur.");
                return;
            }

            var livre = (Livre)cbxLivre.SelectedItem;
            var adherent = (Adherent)cbxAdherent.SelectedItem;

            try
            {
                DatabaseService.Instance.EmprunterLivre(livre.Id, adherent.Id, dtpRetourPrevu.Value);
                MessageBox.Show("Emprunt enregistré avec succès !");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'emprunt : " + ex.Message);
            }
        }

        private void btnRetourner_Click(object sender, EventArgs e)
        {
            if (dgvEmprunts.SelectedRows.Count > 0)
            {
                var emprunt = (Emprunt)dgvEmprunts.SelectedRows[0].DataBoundItem;
                try
                {
                    DatabaseService.Instance.RetournerLivre(emprunt.Id);
                    MessageBox.Show("Livre retourné avec succès !");
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur lors du retour : " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un emprunt.");
            }
        }
    }
}
