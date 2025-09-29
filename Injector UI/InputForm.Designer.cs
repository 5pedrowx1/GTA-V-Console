using Guna.UI2.WinForms;

namespace Injector_UI
{
    partial class InputForm
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
        private void InitializeComponent(string title, string prompt)
        {
            this.Size = new Size(500, 240);
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
                Text = title,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.Purple,
                Location = new Point(20, 18),
                AutoSize = true
            };
            panelTop.Controls.Add(lblTitle);

            this.Controls.Add(panelTop);

            // Prompt
            lblPrompt = new Label
            {
                Text = prompt,
                Location = new Point(30, 80),
                ForeColor = Color.White,
                AutoSize = true,
                Font = new Font("Segoe UI", 10)
            };
            this.Controls.Add(lblPrompt);

            // Input
            txtInput = new Guna2TextBox
            {
                Location = new Point(30, 110),
                Size = new Size(430, 35),
                BorderRadius = 5,
                FillColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10)
            };
            txtInput.KeyPress += (s, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    e.Handled = true;
                    btnOk.PerformClick();
                }
                else if (e.KeyChar == (char)Keys.Escape)
                {
                    e.Handled = true;
                    btnCancel.PerformClick();
                }
            };
            this.Controls.Add(txtInput);

            // Separator
            var separator = new Guna2Separator
            {
                Location = new Point(20, 160),
                Size = new Size(460, 10),
                FillColor = Color.FromArgb(40, 40, 40)
            };
            this.Controls.Add(separator);

            // Buttons
            btnCancel = new Guna2GradientButton
            {
                Text = "✕ Cancelar",
                Location = new Point(250, 180),
                Size = new Size(100, 40),
                FillColor = Color.FromArgb(64, 64, 64),
                FillColor2 = Color.FromArgb(45, 45, 45),
                BorderRadius = 8,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnCancel.Click += (s, e) =>
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };
            this.Controls.Add(btnCancel);

            btnOk = new Guna2GradientButton
            {
                Text = "✓ OK",
                Location = new Point(360, 180),
                Size = new Size(100, 40),
                FillColor = Color.Purple,
                FillColor2 = Color.DarkMagenta,
                BorderRadius = 8,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnOk.Click += BtnOk_Click;
            this.Controls.Add(btnOk);

            // Set focus to input
            this.Shown += (s, e) => txtInput.Focus();
        }

        #endregion

        private Guna2TextBox txtInput;
        private Guna2GradientButton btnOk;
        private Guna2GradientButton btnCancel;
        private Guna2Panel panelTop;
        private Guna2HtmlLabel lblTitle;
        private Label lblPrompt;
    }
}