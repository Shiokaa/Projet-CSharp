using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace BibliothequeApp.Forms
{
    partial class BookForm
    {
        private IContainer components = null;
        private Label lblTitleHeader;
        private Label lblTitre;
        private TextBox txtTitre;
        private Label lblAuteur;
        private TextBox txtAuteur;
        private Label lblIsbn;
        private TextBox txtIsbn;
        private Label lblAnnee;
        private NumericUpDown numAnnee;
        private Label lblGenre;
        private ComboBox cbxGenre;
        private Label lblRayon;
        private ComboBox cbxRayon;
        private Label lblEtagere;
        private ComboBox cbxEtagere;
        private Label lblCouverture;
        private Button btnBrowse;
        private PictureBox picCover;
        private Button btnSave;
        private Button btnCancel;
        private OpenFileDialog openFileDialog;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitleHeader = new Label();
            this.lblTitre = new Label();
            this.txtTitre = new TextBox();
            this.lblAuteur = new Label();
            this.txtAuteur = new TextBox();
            this.lblIsbn = new Label();
            this.txtIsbn = new TextBox();
            this.lblAnnee = new Label();
            this.numAnnee = new NumericUpDown();
            this.lblGenre = new Label();
            this.cbxGenre = new ComboBox();
            this.lblRayon = new Label();
            this.cbxRayon = new ComboBox();
            this.lblEtagere = new Label();
            this.cbxEtagere = new ComboBox();
            this.lblCouverture = new Label();
            this.btnBrowse = new Button();
            this.picCover = new PictureBox();
            this.btnSave = new Button();
            this.btnCancel = new Button();
            this.openFileDialog = new OpenFileDialog();

            ((ISupportInitialize)(this.numAnnee)).BeginInit();
            ((ISupportInitialize)(this.picCover)).BeginInit();
            this.SuspendLayout();

            this.lblTitleHeader.Text = "Informations du Livre";
            this.lblTitleHeader.Font = new Font("Segoe UI", 24F, FontStyle.Regular);
            this.lblTitleHeader.ForeColor = Color.FromArgb(139, 92, 246);
            this.lblTitleHeader.Location = new Point(20, 20);
            this.lblTitleHeader.AutoSize = true;

            int startY = 70;
            int spacing = 40;

            this.lblTitre.Text = "Titre :";
            this.lblTitre.Location = new Point(20, startY);
            
            this.txtTitre.Location = new Point(130, startY - 3);
            this.txtTitre.Width = 250;

            this.lblAuteur.Text = "Auteur :";
            this.lblAuteur.Location = new Point(20, startY + spacing);

            this.txtAuteur.Location = new Point(130, startY + spacing - 3);
            this.txtAuteur.Width = 250;

            this.lblIsbn.Text = "ISBN :";
            this.lblIsbn.Location = new Point(20, startY + 2 * spacing);

            this.txtIsbn.Location = new Point(130, startY + 2 * spacing - 3);
            this.txtIsbn.Width = 250;

            this.lblAnnee.Text = "Année :";
            this.lblAnnee.Location = new Point(20, startY + 3 * spacing);

            this.numAnnee.Location = new Point(130, startY + 3 * spacing - 3);
            this.numAnnee.Minimum = 0;
            this.numAnnee.Maximum = 3000;
            this.numAnnee.Value = DateTime.Now.Year;
            this.numAnnee.Width = 100;

            this.lblGenre.Text = "Genre :";
            this.lblGenre.Location = new Point(20, startY + 4 * spacing);

            this.cbxGenre.Location = new Point(130, startY + 4 * spacing - 3);
            this.cbxGenre.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbxGenre.Width = 250;

            this.lblRayon.Text = "Rayon :";
            this.lblRayon.Location = new Point(20, startY + 5 * spacing);

            this.cbxRayon.Location = new Point(130, startY + 5 * spacing - 3);
            this.cbxRayon.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbxRayon.Width = 250;
            this.cbxRayon.SelectedIndexChanged += new EventHandler(this.cbxRayon_SelectedIndexChanged);

            this.lblEtagere.Text = "Étagère :";
            this.lblEtagere.Location = new Point(20, startY + 6 * spacing);

            this.cbxEtagere.Location = new Point(130, startY + 6 * spacing - 3);
            this.cbxEtagere.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbxEtagere.Width = 250;

            this.lblCouverture.Text = "Couverture :";
            this.lblCouverture.Location = new Point(420, startY);

            this.btnBrowse.Text = "Parcourir...";
            this.btnBrowse.Location = new Point(420, startY + 20);
            this.btnBrowse.Click += new EventHandler(this.btnBrowse_Click);

            this.picCover.Location = new Point(420, startY + 55);
            this.picCover.Size = new Size(180, 240);
            this.picCover.SizeMode = PictureBoxSizeMode.Zoom;
            this.picCover.BorderStyle = BorderStyle.FixedSingle;

            this.btnSave.Text = "Enregistrer";
            this.btnSave.Location = new Point(400, 420);
            this.btnSave.Size = new Size(100, 35);
            this.btnSave.BackColor = Color.FromArgb(139, 92, 246);
            this.btnSave.ForeColor = Color.White;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = FlatStyle.Flat;
            this.btnSave.Click += new EventHandler(this.btnSave_Click);

            this.btnCancel.Text = "Annuler";
            this.btnCancel.Location = new Point(510, 420);
            this.btnCancel.Size = new Size(100, 35);
            this.btnCancel.BackColor = Color.White;
            this.btnCancel.ForeColor = Color.FromArgb(107, 114, 128);
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

            this.ClientSize = new Size(640, 480);
            this.BackColor = Color.FromArgb(248, 250, 252);
            this.ForeColor = Color.FromArgb(17, 24, 39);
            this.Controls.Add(this.lblTitleHeader);
            this.Controls.Add(this.lblTitre);
            this.Controls.Add(this.txtTitre);
            this.Controls.Add(this.lblAuteur);
            this.Controls.Add(this.txtAuteur);
            this.Controls.Add(this.lblIsbn);
            this.Controls.Add(this.txtIsbn);
            this.Controls.Add(this.lblAnnee);
            this.Controls.Add(this.numAnnee);
            this.Controls.Add(this.lblGenre);
            this.Controls.Add(this.cbxGenre);
            this.Controls.Add(this.lblRayon);
            this.Controls.Add(this.cbxRayon);
            this.Controls.Add(this.lblEtagere);
            this.Controls.Add(this.cbxEtagere);
            this.Controls.Add(this.lblCouverture);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.picCover);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            
            this.Name = "BookForm";
            this.Text = "Formulaire Livre";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            ((ISupportInitialize)(this.numAnnee)).EndInit();
            ((ISupportInitialize)(this.picCover)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
