using Guna.UI2.WinForms;

namespace Injector_UI
{
    partial class AddCustomDllForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Size = new Size(650, 420);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(18, 18, 18);

            // Top Panel
            panelTop = new Guna2Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.FromArgb(20, 20, 20),
                BorderRadius = 12
            };

            lblTitle = new Guna2HtmlLabel
            {
                Text = "➕ Adicionar DLL",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.Purple,
                Location = new Point(20, 18),
                AutoSize = true
            };
            panelTop.Controls.Add(lblTitle);

            this.Controls.Add(panelTop);

            int y = 80;

            // Name
            var lblName = new Label
            {
                Text = "Nome:",
                Location = new Point(30, y),
                ForeColor = Color.White,
                AutoSize = true,
                Font = new Font("Segoe UI", 10)
            };
            this.Controls.Add(lblName);

            txtName = new Guna2TextBox
            {
                Location = new Point(180, y - 5),
                Size = new Size(430, 35),
                BorderRadius = 5,
                FillColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White,
                PlaceholderText = "Nome da DLL"
            };
            this.Controls.Add(txtName);
            y += 55;

            // Path
            var lblPath = new Label
            {
                Text = "Caminho:",
                Location = new Point(30, y),
                ForeColor = Color.White,
                AutoSize = true,
                Font = new Font("Segoe UI", 10)
            };
            this.Controls.Add(lblPath);

            txtPath = new Guna2TextBox
            {
                Location = new Point(180, y - 5),
                Size = new Size(340, 35),
                BorderRadius = 5,
                FillColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White,
                PlaceholderText = "Caminho completo do arquivo"
            };
            this.Controls.Add(txtPath);

            btnBrowse = new Guna2GradientButton
            {
                Text = "📁 Procurar",
                Location = new Point(530, y - 5),
                Size = new Size(80, 35),
                FillColor = Color.Purple,
                FillColor2 = Color.DarkMagenta,
                BorderRadius = 5,
                Font = new Font("Segoe UI", 8, FontStyle.Bold)
            };
            btnBrowse.Click += BtnBrowse_Click;
            this.Controls.Add(btnBrowse);
            y += 55;

            // Description
            var lblDescription = new Label
            {
                Text = "Descrição:",
                Location = new Point(30, y),
                ForeColor = Color.White,
                AutoSize = true,
                Font = new Font("Segoe UI", 10)
            };
            this.Controls.Add(lblDescription);

            txtDescription = new Guna2TextBox
            {
                Location = new Point(180, y - 5),
                Size = new Size(430, 90),
                BorderRadius = 5,
                FillColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White,
                Multiline = true,
                PlaceholderText = "Descrição opcional da DLL"
            };
            this.Controls.Add(txtDescription);
            y += 110;

            // Enabled
            chkEnabled = new Guna2CheckBox
            {
                Text = "Habilitado",
                Location = new Point(180, y),
                ForeColor = Color.White,
                Checked = true,
                CheckedState = { FillColor = Color.Purple },
                Font = new Font("Segoe UI", 10)
            };
            this.Controls.Add(chkEnabled);
            y += 50;

            // Separator
            var separator = new Guna2Separator
            {
                Location = new Point(20, y),
                Size = new Size(610, 10),
                FillColor = Color.FromArgb(40, 40, 40)
            };
            this.Controls.Add(separator);
            y += 20;

            // Buttons
            btnCancel = new Guna2GradientButton
            {
                Text = "✕ Cancelar",
                Location = new Point(400, y),
                Size = new Size(100, 40),
                FillColor = Color.FromArgb(64, 64, 64),
                FillColor2 = Color.FromArgb(45, 45, 45),
                BorderRadius = 8,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnCancel.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
            this.Controls.Add(btnCancel);

            btnSave = new Guna2GradientButton
            {
                Text = "💾 Salvar",
                Location = new Point(510, y),
                Size = new Size(100, 40),
                FillColor = Color.Green,
                FillColor2 = Color.DarkGreen,
                BorderRadius = 8,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnSave.Click += BtnSave_Click;
            this.Controls.Add(btnSave);
        }

        private Guna2TextBox txtName;
        private Guna2TextBox txtPath;
        private Guna2TextBox txtDescription;
        private Guna2CheckBox chkEnabled;
        private Guna2GradientButton btnBrowse;
        private Guna2GradientButton btnSave;
        private Guna2GradientButton btnCancel;
        private Guna2Panel panelTop;
        private Guna2HtmlLabel lblTitle;

        #endregion
    }
}