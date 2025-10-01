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
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddCustomDllForm));
            panelTop = new Guna2Panel();
            lblTitle = new Guna2HtmlLabel();
            lblName = new Label();
            txtName = new Guna2TextBox();
            lblPath = new Label();
            txtPath = new Guna2TextBox();
            btnBrowse = new Guna2GradientButton();
            lblDescription = new Label();
            txtDescription = new Guna2TextBox();
            chkEnabled = new Guna2CheckBox();
            separator = new Guna2Separator();
            btnCancel = new Guna2GradientButton();
            btnSave = new Guna2GradientButton();
            guna2DragControl1 = new Guna2DragControl(components);
            guna2Elipse1 = new Guna2Elipse(components);
            panelTop.SuspendLayout();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.FromArgb(20, 20, 20);
            panelTop.Controls.Add(lblTitle);
            panelTop.CustomizableEdges = customizableEdges1;
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.ShadowDecoration.CustomizableEdges = customizableEdges2;
            panelTop.Size = new Size(650, 60);
            panelTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.Purple;
            lblTitle.Location = new Point(20, 18);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(155, 27);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "➕ Adicionar DLL";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 10F);
            lblName.ForeColor = Color.White;
            lblName.Location = new Point(30, 80);
            lblName.Name = "lblName";
            lblName.Size = new Size(49, 19);
            lblName.TabIndex = 1;
            lblName.Text = "Nome:";
            // 
            // txtName
            // 
            txtName.BorderRadius = 5;
            txtName.CustomizableEdges = customizableEdges3;
            txtName.DefaultText = "";
            txtName.FillColor = Color.FromArgb(30, 30, 30);
            txtName.Font = new Font("Segoe UI", 9F);
            txtName.ForeColor = Color.White;
            txtName.Location = new Point(180, 75);
            txtName.Name = "txtName";
            txtName.PlaceholderText = "Nome da DLL";
            txtName.SelectedText = "";
            txtName.ShadowDecoration.CustomizableEdges = customizableEdges4;
            txtName.Size = new Size(430, 35);
            txtName.TabIndex = 2;
            // 
            // lblPath
            // 
            lblPath.AutoSize = true;
            lblPath.Font = new Font("Segoe UI", 10F);
            lblPath.ForeColor = Color.White;
            lblPath.Location = new Point(30, 135);
            lblPath.Name = "lblPath";
            lblPath.Size = new Size(67, 19);
            lblPath.TabIndex = 3;
            lblPath.Text = "Caminho:";
            // 
            // txtPath
            // 
            txtPath.BorderRadius = 5;
            txtPath.CustomizableEdges = customizableEdges5;
            txtPath.DefaultText = "";
            txtPath.FillColor = Color.FromArgb(30, 30, 30);
            txtPath.Font = new Font("Segoe UI", 9F);
            txtPath.ForeColor = Color.White;
            txtPath.Location = new Point(180, 130);
            txtPath.Name = "txtPath";
            txtPath.PlaceholderText = "Caminho completo do arquivo";
            txtPath.SelectedText = "";
            txtPath.ShadowDecoration.CustomizableEdges = customizableEdges6;
            txtPath.Size = new Size(340, 35);
            txtPath.TabIndex = 4;
            // 
            // btnBrowse
            // 
            btnBrowse.BorderRadius = 5;
            btnBrowse.CustomizableEdges = customizableEdges7;
            btnBrowse.FillColor = Color.Purple;
            btnBrowse.FillColor2 = Color.DarkMagenta;
            btnBrowse.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            btnBrowse.ForeColor = Color.White;
            btnBrowse.Location = new Point(530, 130);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnBrowse.Size = new Size(80, 35);
            btnBrowse.TabIndex = 5;
            btnBrowse.Text = "📁 Procurar";
            btnBrowse.Click += BtnBrowse_Click;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Font = new Font("Segoe UI", 10F);
            lblDescription.ForeColor = Color.White;
            lblDescription.Location = new Point(30, 190);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(70, 19);
            lblDescription.TabIndex = 6;
            lblDescription.Text = "Descrição:";
            // 
            // txtDescription
            // 
            txtDescription.BorderRadius = 5;
            txtDescription.CustomizableEdges = customizableEdges9;
            txtDescription.DefaultText = "";
            txtDescription.FillColor = Color.FromArgb(30, 30, 30);
            txtDescription.Font = new Font("Segoe UI", 9F);
            txtDescription.ForeColor = Color.White;
            txtDescription.Location = new Point(180, 185);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.PlaceholderText = "Descrição opcional da DLL";
            txtDescription.SelectedText = "";
            txtDescription.ShadowDecoration.CustomizableEdges = customizableEdges10;
            txtDescription.Size = new Size(430, 90);
            txtDescription.TabIndex = 7;
            // 
            // chkEnabled
            // 
            chkEnabled.Checked = true;
            chkEnabled.CheckedState.BorderRadius = 0;
            chkEnabled.CheckedState.BorderThickness = 0;
            chkEnabled.CheckedState.FillColor = Color.Purple;
            chkEnabled.CheckState = CheckState.Checked;
            chkEnabled.Font = new Font("Segoe UI", 10F);
            chkEnabled.ForeColor = Color.White;
            chkEnabled.Location = new Point(180, 295);
            chkEnabled.Name = "chkEnabled";
            chkEnabled.Size = new Size(104, 24);
            chkEnabled.TabIndex = 8;
            chkEnabled.Text = "Habilitado";
            chkEnabled.UncheckedState.BorderRadius = 0;
            chkEnabled.UncheckedState.BorderThickness = 0;
            // 
            // separator
            // 
            separator.FillColor = Color.FromArgb(40, 40, 40);
            separator.Location = new Point(20, 345);
            separator.Name = "separator";
            separator.Size = new Size(610, 10);
            separator.TabIndex = 9;
            // 
            // btnCancel
            // 
            btnCancel.BorderRadius = 8;
            btnCancel.CustomizableEdges = customizableEdges11;
            btnCancel.FillColor = Color.FromArgb(64, 64, 64);
            btnCancel.FillColor2 = Color.FromArgb(45, 45, 45);
            btnCancel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(400, 365);
            btnCancel.Name = "btnCancel";
            btnCancel.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btnCancel.Size = new Size(100, 40);
            btnCancel.TabIndex = 10;
            btnCancel.Text = "✕ Cancelar";
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.BorderRadius = 8;
            btnSave.CustomizableEdges = customizableEdges13;
            btnSave.FillColor = Color.Green;
            btnSave.FillColor2 = Color.DarkGreen;
            btnSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(510, 365);
            btnSave.Name = "btnSave";
            btnSave.ShadowDecoration.CustomizableEdges = customizableEdges14;
            btnSave.Size = new Size(100, 40);
            btnSave.TabIndex = 11;
            btnSave.Text = "💾 Salvar";
            btnSave.Click += BtnSave_Click;
            // 
            // guna2DragControl1
            // 
            guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            guna2DragControl1.UseTransparentDrag = true;
            // 
            // guna2Elipse1
            // 
            guna2Elipse1.BorderRadius = 17;
            guna2Elipse1.TargetControl = this;
            // 
            // AddCustomDllForm
            // 
            BackColor = Color.FromArgb(18, 18, 18);
            ClientSize = new Size(650, 420);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            Controls.Add(separator);
            Controls.Add(chkEnabled);
            Controls.Add(txtDescription);
            Controls.Add(lblDescription);
            Controls.Add(btnBrowse);
            Controls.Add(txtPath);
            Controls.Add(lblPath);
            Controls.Add(txtName);
            Controls.Add(lblName);
            Controls.Add(panelTop);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "AddCustomDllForm";
            StartPosition = FormStartPosition.CenterParent;
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private Guna2Panel panelTop;
        private Guna2HtmlLabel lblTitle;
        private Label lblName;
        private Guna2TextBox txtName;
        private Label lblPath;
        private Guna2TextBox txtPath;
        private Guna2GradientButton btnBrowse;
        private Label lblDescription;
        private Guna2TextBox txtDescription;
        private Guna2CheckBox chkEnabled;
        private Guna2Separator separator;
        private Guna2GradientButton btnCancel;
        private Guna2GradientButton btnSave;

        #endregion

        private Guna2DragControl guna2DragControl1;
        private Guna2Elipse guna2Elipse1;
    }
}