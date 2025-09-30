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
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputForm));
            panelTop = new Guna2Panel();
            lblTitle = new Guna2HtmlLabel();
            lblPrompt = new Label();
            txtInput = new Guna2TextBox();
            separator = new Guna2Separator();
            btnCancel = new Guna2GradientButton();
            btnOk = new Guna2GradientButton();
            guna2DragControl1 = new Guna2DragControl(components);
            panelTop.SuspendLayout();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.FromArgb(20, 20, 20);
            panelTop.BorderRadius = 12;
            panelTop.Controls.Add(lblTitle);
            panelTop.CustomizableEdges = customizableEdges1;
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.ShadowDecoration.CustomizableEdges = customizableEdges2;
            panelTop.Size = new Size(484, 58);
            panelTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.Purple;
            lblTitle.Location = new Point(20, 12);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(41, 27);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Title";
            // 
            // lblPrompt
            // 
            lblPrompt.AutoSize = true;
            lblPrompt.Font = new Font("Segoe UI", 10F);
            lblPrompt.ForeColor = Color.White;
            lblPrompt.Location = new Point(20, 76);
            lblPrompt.Name = "lblPrompt";
            lblPrompt.Size = new Size(55, 19);
            lblPrompt.TabIndex = 1;
            lblPrompt.Text = "Prompt";
            // 
            // txtInput
            // 
            txtInput.BorderRadius = 5;
            txtInput.CustomizableEdges = customizableEdges3;
            txtInput.DefaultText = "";
            txtInput.FillColor = Color.FromArgb(30, 30, 30);
            txtInput.Font = new Font("Segoe UI", 10F);
            txtInput.ForeColor = Color.White;
            txtInput.Location = new Point(20, 106);
            txtInput.Name = "txtInput";
            txtInput.PlaceholderText = "";
            txtInput.SelectedText = "";
            txtInput.ShadowDecoration.CustomizableEdges = customizableEdges4;
            txtInput.Size = new Size(430, 35);
            txtInput.TabIndex = 2;
            txtInput.KeyPress += TxtInput_KeyPress;
            // 
            // separator
            // 
            separator.FillColor = Color.FromArgb(40, 40, 40);
            separator.Location = new Point(20, 160);
            separator.Name = "separator";
            separator.Size = new Size(460, 10);
            separator.TabIndex = 3;
            // 
            // btnCancel
            // 
            btnCancel.BorderRadius = 8;
            btnCancel.CustomizableEdges = customizableEdges5;
            btnCancel.FillColor = Color.FromArgb(64, 64, 64);
            btnCancel.FillColor2 = Color.FromArgb(45, 45, 45);
            btnCancel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(250, 180);
            btnCancel.Name = "btnCancel";
            btnCancel.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnCancel.Size = new Size(100, 40);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "✕ Cancelar";
            btnCancel.Click += BtnCancel_Click;
            // 
            // btnOk
            // 
            btnOk.BorderRadius = 8;
            btnOk.CustomizableEdges = customizableEdges7;
            btnOk.FillColor = Color.Purple;
            btnOk.FillColor2 = Color.DarkMagenta;
            btnOk.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnOk.ForeColor = Color.White;
            btnOk.Location = new Point(360, 180);
            btnOk.Name = "btnOk";
            btnOk.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnOk.Size = new Size(100, 40);
            btnOk.TabIndex = 5;
            btnOk.Text = "✓ OK";
            btnOk.Click += BtnOk_Click;
            // 
            // guna2DragControl1
            // 
            guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            guna2DragControl1.UseTransparentDrag = true;
            // 
            // InputForm
            // 
            BackColor = Color.FromArgb(18, 18, 18);
            ClientSize = new Size(484, 229);
            Controls.Add(panelTop);
            Controls.Add(lblPrompt);
            Controls.Add(txtInput);
            Controls.Add(separator);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "InputForm";
            StartPosition = FormStartPosition.CenterParent;
            Shown += InputForm_Shown;
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna2TextBox txtInput;
        private Guna2GradientButton btnOk;
        private Guna2GradientButton btnCancel;
        private Guna2Panel panelTop;
        private Guna2HtmlLabel lblTitle;
        private Label lblPrompt;
        private Guna2Separator separator;
        private Guna2DragControl guna2DragControl1;
    }
}