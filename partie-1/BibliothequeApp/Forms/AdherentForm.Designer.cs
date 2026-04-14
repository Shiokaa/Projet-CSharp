using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace BibliothequeApp.Forms
{
    partial class AdherentForm
    {
        private IContainer components = null;
        private Label lblHeader;
        private Label lblNom;
        private TextBox txtNom;
        private Label lblPrenom;
        private TextBox txtPrenom;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblTel;
        private TextBox txtTel;
        private Button btnSave;
        private Button btnCancel;

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
            this.lblNom = new Label();
            this.txtNom = new TextBox();
            this.lblPrenom = new Label();
            this.txtPrenom = new TextBox();
            this.lblEmail = new Label();
            this.txtEmail = new TextBox();
            this.lblTel = new Label();
            this.txtTel = new TextBox();
            this.btnSave = new Button();
            this.btnCancel = new Button();

            this.SuspendLayout();

            this.lblHeader.Text = "Ajouter un Adhérent";
            this.lblHeader.Font = new Font("Segoe UI", 24F, FontStyle.Regular);
            this.lblHeader.ForeColor = Color.FromArgb(139, 92, 246);
            this.lblHeader.Location = new Point(20, 20);
            this.lblHeader.AutoSize = true;

            int startY = 70;
            int spacing = 40;

            this.lblNom.Text = "Nom :";
            this.lblNom.Location = new Point(20, startY);
            this.txtNom.Location = new Point(100, startY - 3);
            this.txtNom.Width = 250;

            this.lblPrenom.Text = "Prénom :";
            this.lblPrenom.Location = new Point(20, startY + spacing);
            this.txtPrenom.Location = new Point(100, startY + spacing - 3);
            this.txtPrenom.Width = 250;

            this.lblEmail.Text = "E-mail :";
            this.lblEmail.Location = new Point(20, startY + 2 * spacing);
            this.txtEmail.Location = new Point(100, startY + 2 * spacing - 3);
            this.txtEmail.Width = 250;

            this.lblTel.Text = "Téléphone :";
            this.lblTel.Location = new Point(20, startY + 3 * spacing);
            this.txtTel.Location = new Point(100, startY + 3 * spacing - 3);
            this.txtTel.Width = 250;

            this.btnSave.Text = "Enregistrer";
            this.btnSave.Location = new Point(140, startY + 4 * spacing);
            this.btnSave.Size = new Size(100, 35);
            this.btnSave.BackColor = Color.FromArgb(139, 92, 246);
            this.btnSave.ForeColor = Color.White;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = FlatStyle.Flat;
            this.btnSave.Click += new EventHandler(this.btnSave_Click);

            this.btnCancel.Text = "Annuler";
            this.btnCancel.Location = new Point(250, startY + 4 * spacing);
            this.btnCancel.Size = new Size(100, 35);
            this.btnCancel.BackColor = Color.White;
            this.btnCancel.ForeColor = Color.FromArgb(107, 114, 128);
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

            this.ClientSize = new Size(380, 290);
            this.BackColor = Color.FromArgb(248, 250, 252);
            this.ForeColor = Color.FromArgb(17, 24, 39);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.lblNom);
            this.Controls.Add(this.txtNom);
            this.Controls.Add(this.lblPrenom);
            this.Controls.Add(this.txtPrenom);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblTel);
            this.Controls.Add(this.txtTel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            
            this.Name = "AdherentForm";
            this.Text = "Nouvel Adhérent";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
