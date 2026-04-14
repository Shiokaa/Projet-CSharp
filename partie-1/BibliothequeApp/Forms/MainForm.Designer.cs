using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BibliothequeApp.Forms
{
    partial class MainForm
    {
        private IContainer components = null;
        private Panel panelSidebar;
        private Panel panelMain;
        private Panel panelHeader;
        private Label lblTitle;
        private Label lblSubtitle;
        private TextBox txtSearch;
        private Button btnSearch;
        private DataGridView dgvLivres;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnBorrows;
        private Button btnToggleDispo;
        private Button btnAddUser;
        private PictureBox picCover;
        private Label lblPreview;

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
            this.panelSidebar = new Panel();
            this.panelMain = new Panel();
            this.panelHeader = new Panel();
            this.lblTitle = new Label();
            this.lblSubtitle = new Label();
            this.txtSearch = new TextBox();
            this.btnSearch = new Button();
            this.dgvLivres = new DataGridView();
            this.btnAdd = new Button();
            this.btnEdit = new Button();
            this.btnDelete = new Button();
            this.btnBorrows = new Button();
            this.btnToggleDispo = new Button();
            this.btnAddUser = new Button();
            this.picCover = new PictureBox();
            this.lblPreview = new Label();

            this.panelSidebar.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelHeader.SuspendLayout();
            ((ISupportInitialize)(this.dgvLivres)).BeginInit();
            ((ISupportInitialize)(this.picCover)).BeginInit();
            this.SuspendLayout();

            
            Color colBgMain = Color.FromArgb(248, 250, 252);
            Color colSidebar = Color.White;
            Color colPrimary = Color.FromArgb(139, 92, 246); 
            Color colTextDark = Color.FromArgb(17, 24, 39);
            Color colTextMuted = Color.FromArgb(107, 114, 128);

            this.BackColor = colBgMain;
            this.ClientSize = new Size(1150, 750);

            
            this.panelSidebar.Dock = DockStyle.Right;
            this.panelSidebar.Width = 280;
            this.panelSidebar.BackColor = colSidebar;
            this.panelSidebar.Padding = new Padding(20);
            
            int currentY = 30;
            
            var lblActions = new Label();
            lblActions.Text = "ACTIONS & FILTRES";
            lblActions.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblActions.ForeColor = colTextMuted;
            lblActions.Location = new Point(20, currentY);
            lblActions.AutoSize = true;
            this.panelSidebar.Controls.Add(lblActions);
            currentY += 40;

            var SetupSidebarButton = new Action<Button, string, EventHandler>((btn, text, handler) => {
                btn.Text = "  " + text;
                btn.Location = new Point(20, currentY);
                btn.Size = new Size(240, 45);
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.BackColor = Color.Transparent;
                btn.ForeColor = colTextDark;
                btn.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
                btn.TextAlign = ContentAlignment.MiddleLeft;
                btn.Cursor = Cursors.Hand;
                btn.Click += handler;
                this.panelSidebar.Controls.Add(btn);
                currentY += 50;
            });

            this.btnAdd.Text = "  Ajouter un livre";
            this.btnAdd.Location = new Point(20, currentY);
            this.btnAdd.Size = new Size(240, 50);
            this.btnAdd.FlatStyle = FlatStyle.Flat;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.BackColor = colPrimary;
            this.btnAdd.ForeColor = Color.White;
            this.btnAdd.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnAdd.Cursor = Cursors.Hand;
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            this.panelSidebar.Controls.Add(this.btnAdd);
            currentY += 70;

            SetupSidebarButton(this.btnEdit, "Modifier sélection", this.btnEdit_Click);
            SetupSidebarButton(this.btnToggleDispo, "Basculer disponibilité", this.btnToggleDispo_Click);
            SetupSidebarButton(this.btnDelete, "Supprimer", this.btnDelete_Click);
            currentY += 20;

            var lblMenu = new Label();
            lblMenu.Text = "MENU PRINCIPAL";
            lblMenu.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblMenu.ForeColor = colTextMuted;
            lblMenu.Location = new Point(20, currentY);
            lblMenu.AutoSize = true;
            this.panelSidebar.Controls.Add(lblMenu);
            currentY += 40;

            SetupSidebarButton(this.btnBorrows, "Gestion Emprunts", this.btnBorrows_Click);
            this.btnBorrows.BackColor = Color.FromArgb(243, 244, 246); 
            this.btnBorrows.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            SetupSidebarButton(this.btnAddUser, "Ajouter un adhérent", this.btnAddUser_Click);

            currentY += 40;

            this.lblPreview.Text = "APERÇU COUVERTURE";
            this.lblPreview.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblPreview.ForeColor = colTextMuted;
            this.lblPreview.Location = new Point(20, currentY);
            this.lblPreview.AutoSize = true;
            this.panelSidebar.Controls.Add(this.lblPreview);
            currentY += 30;

            this.picCover.Location = new Point(20, currentY);
            this.picCover.Size = new Size(240, 300);
            this.picCover.SizeMode = PictureBoxSizeMode.Zoom;
            this.picCover.BorderStyle = BorderStyle.None;
            this.picCover.BackColor = Color.White;
            this.panelSidebar.Controls.Add(this.picCover);

            
            this.panelMain.Dock = DockStyle.Fill;
            this.panelMain.Padding = new Padding(30);

            
            Panel marginPanel = new Panel();
            marginPanel.Dock = DockStyle.Top;
            marginPanel.Height = 160;

            this.lblTitle.Text = "Bibliothèque Municipale";
            this.lblTitle.Font = new Font("Segoe UI", 26F, FontStyle.Regular);
            this.lblTitle.ForeColor = colPrimary;
            this.lblTitle.Location = new Point(0, 0);
            this.lblTitle.AutoSize = true;

            this.lblSubtitle.Text = "Gérez votre inventaire et vos adhérents.";
            this.lblSubtitle.Font = new Font("Segoe UI", 11F);
            this.lblSubtitle.ForeColor = colTextMuted;
            this.lblSubtitle.Location = new Point(8, 45);
            this.lblSubtitle.AutoSize = true;

            
            Panel searchContainer = new Panel();
            searchContainer.BackColor = Color.White;
            searchContainer.Location = new Point(10, 95);
            searchContainer.Size = new Size(600, 50);
            searchContainer.Paint += (s, e) => {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                int radius = 25; 
                Rectangle bounds = new Rectangle(0, 0, searchContainer.Width - 1, searchContainer.Height - 1);
                GraphicsPath path = new GraphicsPath();
                path.AddArc(bounds.X, bounds.Y, radius, radius, 180, 90);
                path.AddArc(bounds.Right - radius, bounds.Y, radius, radius, 270, 90);
                path.AddArc(bounds.Right - radius, bounds.Bottom - radius, radius, radius, 0, 90);
                path.AddArc(bounds.X, bounds.Bottom - radius, radius, radius, 90, 90);
                path.CloseFigure();
                using (Pen p = new Pen(Color.FromArgb(229, 231, 235), 2)) {
                    e.Graphics.DrawPath(p, path);
                }
            };

            this.txtSearch.Location = new Point(20, 14);
            this.txtSearch.Width = 450;
            this.txtSearch.Font = new Font("Segoe UI", 12F);
            this.txtSearch.BorderStyle = BorderStyle.None;
            this.txtSearch.PlaceholderText = "Rechercher par titre, auteur, ISBN...";

            this.btnSearch.Text = "Search";
            this.btnSearch.Location = new Point(490, 8);
            this.btnSearch.Size = new Size(100, 34);
            this.btnSearch.FlatStyle = FlatStyle.Flat;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.BackColor = Color.FromArgb(237, 242, 247);
            this.btnSearch.ForeColor = colTextMuted;
            this.btnSearch.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnSearch.Cursor = Cursors.Hand;
            this.btnSearch.Click += new EventHandler(this.btnSearch_Click);

            searchContainer.Controls.Add(this.txtSearch);
            searchContainer.Controls.Add(this.btnSearch);

            marginPanel.Controls.Add(this.lblTitle);
            marginPanel.Controls.Add(this.lblSubtitle);
            marginPanel.Controls.Add(searchContainer);

            
            this.panelMain.Controls.Add(this.dgvLivres);
            this.panelMain.Controls.Add(marginPanel);

            
            this.dgvLivres.Dock = DockStyle.Fill;
            this.dgvLivres.AllowUserToAddRows = false;
            this.dgvLivres.AllowUserToDeleteRows = false;
            this.dgvLivres.ReadOnly = true;
            this.dgvLivres.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvLivres.MultiSelect = false;
            this.dgvLivres.BackgroundColor = Color.White; 
            this.dgvLivres.BorderStyle = BorderStyle.None;
            this.dgvLivres.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLivres.RowHeadersVisible = false;
            this.dgvLivres.EnableHeadersVisualStyles = false;
            this.dgvLivres.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(249, 250, 251);
            this.dgvLivres.ColumnHeadersDefaultCellStyle.ForeColor = colTextMuted;
            this.dgvLivres.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.dgvLivres.ColumnHeadersHeight = 45;
            
            this.dgvLivres.DefaultCellStyle.BackColor = Color.White;
            this.dgvLivres.DefaultCellStyle.ForeColor = colTextDark;
            this.dgvLivres.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            this.dgvLivres.DefaultCellStyle.SelectionBackColor = colPrimary;
            this.dgvLivres.DefaultCellStyle.SelectionForeColor = Color.White;
            this.dgvLivres.RowTemplate.Height = 45;
            this.dgvLivres.GridColor = Color.FromArgb(243, 244, 246);
            this.dgvLivres.CellClick += new DataGridViewCellEventHandler(this.dgvLivres_CellClick);

            
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelSidebar); 
            
            this.Name = "MainForm";
            this.Text = "ATS Library Analyzer";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Load += new EventHandler(this.MainForm_Load);

            this.panelSidebar.ResumeLayout(false);
            this.panelSidebar.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((ISupportInitialize)(this.dgvLivres)).EndInit();
            ((ISupportInitialize)(this.picCover)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
