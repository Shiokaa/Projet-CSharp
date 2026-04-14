using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace BibliothequeApp.Forms
{
    partial class BorrowForm
    {
        private IContainer components = null;
        private Label lblHeader;
        private DataGridView dgvEmprunts;
        private Panel panelActions;
        private Label lblLivre;
        private ComboBox cbxLivre;
        private Label lblAdherent;
        private ComboBox cbxAdherent;
        private Button btnAddAdherent;
        private Label lblDatePrevue;
        private DateTimePicker dtpRetourPrevu;
        private Button btnEmprunter;
        private Button btnRetourner;

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
            this.lblHeader = new Label();
            this.dgvEmprunts = new DataGridView();
            this.panelActions = new Panel();
            this.lblLivre = new Label();
            this.cbxLivre = new ComboBox();
            this.lblAdherent = new Label();
            this.cbxAdherent = new ComboBox();
            this.btnAddAdherent = new Button();
            this.lblDatePrevue = new Label();
            this.dtpRetourPrevu = new DateTimePicker();
            this.btnEmprunter = new Button();
            this.btnRetourner = new Button();

            ((ISupportInitialize)(this.dgvEmprunts)).BeginInit();
            this.panelActions.SuspendLayout();
            this.SuspendLayout();

            this.lblHeader.Text = "Gestion des Emprunts Actifs";
            this.lblHeader.Font = new Font("Segoe UI", 24F, FontStyle.Regular);
            this.lblHeader.ForeColor = Color.FromArgb(139, 92, 246);
            this.lblHeader.Location = new Point(20, 15);
            this.lblHeader.AutoSize = true;

            this.dgvEmprunts.Location = new Point(20, 60);
            this.dgvEmprunts.Size = new Size(740, 250);
            this.dgvEmprunts.AllowUserToAddRows = false;
            this.dgvEmprunts.AllowUserToDeleteRows = false;
            this.dgvEmprunts.ReadOnly = true;
            this.dgvEmprunts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvEmprunts.MultiSelect = false;
            this.dgvEmprunts.BackgroundColor = Color.White;
            this.dgvEmprunts.BorderStyle = BorderStyle.None;
            this.dgvEmprunts.EnableHeadersVisualStyles = false;
            this.dgvEmprunts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(249, 250, 251);
            this.dgvEmprunts.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(107, 114, 128);
            this.dgvEmprunts.DefaultCellStyle.BackColor = Color.White;
            this.dgvEmprunts.DefaultCellStyle.ForeColor = Color.FromArgb(17, 24, 39);
            this.dgvEmprunts.DefaultCellStyle.SelectionBackColor = Color.FromArgb(139, 92, 246);
            this.dgvEmprunts.DefaultCellStyle.SelectionForeColor = Color.White;
            this.dgvEmprunts.GridColor = Color.FromArgb(243, 244, 246);
            this.dgvEmprunts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEmprunts.RowHeadersVisible = false;

            this.btnRetourner.Text = "Marquer comme Retourné";
            this.btnRetourner.Location = new Point(20, 320);
            this.btnRetourner.Size = new Size(180, 35);
            this.btnRetourner.BackColor = Color.FromArgb(139, 92, 246);
            this.btnRetourner.ForeColor = Color.White;
            this.btnRetourner.FlatAppearance.BorderSize = 0;
            this.btnRetourner.FlatStyle = FlatStyle.Flat;
            this.btnRetourner.Click += new EventHandler(this.btnRetourner_Click);

            this.panelActions.Location = new Point(20, 380);
            this.panelActions.Size = new Size(740, 150);
            this.panelActions.BackColor = Color.White;
            this.panelActions.BorderStyle = BorderStyle.None;
            
            Label lblAddHeader = new Label();
            lblAddHeader.Text = "Nouvel Emprunt";
            lblAddHeader.Font = new Font("Segoe UI", 16F, FontStyle.Regular);
            lblAddHeader.ForeColor = Color.FromArgb(107, 114, 128);
            lblAddHeader.Location = new Point(10, 10);
            lblAddHeader.AutoSize = true;
            this.panelActions.Controls.Add(lblAddHeader);

            this.lblLivre.Text = "Livre :";
            this.lblLivre.Location = new Point(15, 50);
            
            this.cbxLivre.Location = new Point(70, 47);
            this.cbxLivre.Width = 250;
            this.cbxLivre.DropDownStyle = ComboBoxStyle.DropDownList;

            this.lblAdherent.Text = "Adhérent :";
            this.lblAdherent.Location = new Point(340, 50);

            this.cbxAdherent.Location = new Point(410, 47);
            this.cbxAdherent.Width = 200;
            this.cbxAdherent.DropDownStyle = ComboBoxStyle.DropDownList;

            this.btnAddAdherent.Text = "+";
            this.btnAddAdherent.Location = new Point(620, 46);
            this.btnAddAdherent.Size = new Size(30, 26);
            this.btnAddAdherent.BackColor = Color.FromArgb(139, 92, 246);
            this.btnAddAdherent.ForeColor = Color.White;
            this.btnAddAdherent.FlatAppearance.BorderSize = 0;
            this.btnAddAdherent.FlatStyle = FlatStyle.Flat;
            this.btnAddAdherent.Click += new EventHandler(this.btnAddAdherent_Click);

            this.lblDatePrevue.Text = "Date de retour :";
            this.lblDatePrevue.Location = new Point(15, 95);
            this.lblDatePrevue.AutoSize = true;

            this.dtpRetourPrevu.Location = new Point(130, 92);
            this.dtpRetourPrevu.Format = DateTimePickerFormat.Short;

            this.btnEmprunter.Text = "Enregistrer l'emprunt";
            this.btnEmprunter.Location = new Point(410, 90);
            this.btnEmprunter.Size = new Size(250, 30);
            this.btnEmprunter.BackColor = Color.FromArgb(139, 92, 246);
            this.btnEmprunter.ForeColor = Color.White;
            this.btnEmprunter.FlatAppearance.BorderSize = 0;
            this.btnEmprunter.FlatStyle = FlatStyle.Flat;
            this.btnEmprunter.Click += new EventHandler(this.btnEmprunter_Click);

            this.panelActions.Controls.Add(this.lblLivre);
            this.panelActions.Controls.Add(this.cbxLivre);
            this.panelActions.Controls.Add(this.lblAdherent);
            this.panelActions.Controls.Add(this.cbxAdherent);
            this.panelActions.Controls.Add(this.btnAddAdherent);
            this.panelActions.Controls.Add(this.lblDatePrevue);
            this.panelActions.Controls.Add(this.dtpRetourPrevu);
            this.panelActions.Controls.Add(this.btnEmprunter);

            this.ClientSize = new Size(780, 550);
            this.BackColor = Color.FromArgb(248, 250, 252);
            this.ForeColor = Color.FromArgb(17, 24, 39);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.dgvEmprunts);
            this.Controls.Add(this.btnRetourner);
            this.Controls.Add(this.panelActions);
            this.Name = "BorrowForm";
            this.Text = "Gestion des Emprunts";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Load += new EventHandler(this.BorrowForm_Load);

            ((ISupportInitialize)(this.dgvEmprunts)).EndInit();
            this.panelActions.ResumeLayout(false);
            this.panelActions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
