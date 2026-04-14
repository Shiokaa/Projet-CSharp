using System;
using System.Windows.Forms;
using BibliothequeApp.Models;
using BibliothequeApp.Services;

namespace BibliothequeApp.Forms
{
    public partial class AdherentForm : Form
    {
        public AdherentForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNom.Text) ||
                string.IsNullOrWhiteSpace(txtPrenom.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Le Nom, Prénom, et l'E-mail sont obligatoires.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var adherent = new Adherent
            {
                Nom = txtNom.Text.Trim(),
                Prenom = txtPrenom.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Telephone = txtTel.Text.Trim()
            };

            try
            {
                DatabaseService.Instance.AddAdherent(adherent);
                MessageBox.Show("Adhérent ajouté avec succès !");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'ajout (l'email est peut-être déjà utilisé) : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
