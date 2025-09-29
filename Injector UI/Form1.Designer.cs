namespace Injector_UI
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            txtOut = new Guna.UI2.WinForms.Guna2TextBox();
            lblName = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(components);
            panelTop = new Guna.UI2.WinForms.Guna2Panel();
            lblVersion = new Guna.UI2.WinForms.Guna2HtmlLabel();
            BtnClose = new Guna.UI2.WinForms.Guna2GradientButton();
            BtnMinimize = new Guna.UI2.WinForms.Guna2GradientButton();
            panelStatus = new Guna.UI2.WinForms.Guna2Panel();
            lblStatus = new Guna.UI2.WinForms.Guna2HtmlLabel();
            indicatorStatus = new Guna.UI2.WinForms.Guna2CircleProgressBar();
            panelControls = new Guna.UI2.WinForms.Guna2Panel();
            btnSettings = new Guna.UI2.WinForms.Guna2GradientButton();
            btnClearLog = new Guna.UI2.WinForms.Guna2GradientButton();
            chkAutoInject = new Guna.UI2.WinForms.Guna2CheckBox();
            guna2Separator1 = new Guna.UI2.WinForms.Guna2Separator();
            panelTop.SuspendLayout();
            panelStatus.SuspendLayout();
            panelControls.SuspendLayout();
            SuspendLayout();
            // 
            // txtOut
            // 
            txtOut.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtOut.BorderRadius = 8;
            txtOut.BorderThickness = 0;
            txtOut.CustomizableEdges = customizableEdges1;
            txtOut.DefaultText = "";
            txtOut.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtOut.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtOut.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtOut.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtOut.FillColor = Color.FromArgb(15, 15, 15);
            txtOut.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtOut.Font = new Font("Consolas", 9F);
            txtOut.ForeColor = Color.FromArgb(224, 224, 224);
            txtOut.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtOut.Location = new Point(15, 150);
            txtOut.Multiline = true;
            txtOut.Name = "txtOut";
            txtOut.PlaceholderForeColor = Color.Gray;
            txtOut.PlaceholderText = "Aguardando inicialização...";
            txtOut.ReadOnly = true;
            txtOut.ScrollBars = ScrollBars.Vertical;
            txtOut.SelectedText = "";
            txtOut.ShadowDecoration.CustomizableEdges = customizableEdges2;
            txtOut.Size = new Size(770, 390);
            txtOut.TabIndex = 1;
            // 
            // lblName
            // 
            lblName.BackColor = Color.Transparent;
            lblName.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblName.ForeColor = Color.Purple;
            lblName.Location = new Point(15, 12);
            lblName.Name = "lblName";
            lblName.Size = new Size(306, 27);
            lblName.TabIndex = 2;
            lblName.Text = "ScriptHook Loader by 5pedrowx1";
            // 
            // guna2DragControl1
            // 
            guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            guna2DragControl1.TargetControl = panelTop;
            guna2DragControl1.UseTransparentDrag = true;
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.FromArgb(20, 20, 20);
            panelTop.BorderRadius = 12;
            panelTop.Controls.Add(lblVersion);
            panelTop.Controls.Add(lblName);
            panelTop.Controls.Add(BtnClose);
            panelTop.Controls.Add(BtnMinimize);
            panelTop.CustomizableEdges = customizableEdges7;
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.ShadowDecoration.CustomizableEdges = customizableEdges8;
            panelTop.Size = new Size(800, 55);
            panelTop.TabIndex = 5;
            // 
            // lblVersion
            // 
            lblVersion.BackColor = Color.Transparent;
            lblVersion.Font = new Font("Segoe UI", 8F);
            lblVersion.ForeColor = Color.Gray;
            lblVersion.Location = new Point(15, 35);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(32, 15);
            lblVersion.TabIndex = 5;
            lblVersion.Text = "v2.0.0";
            // 
            // BtnClose
            // 
            BtnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnClose.BorderRadius = 8;
            BtnClose.CustomizableEdges = customizableEdges3;
            BtnClose.DisabledState.BorderColor = Color.DarkGray;
            BtnClose.DisabledState.CustomBorderColor = Color.DarkGray;
            BtnClose.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            BtnClose.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            BtnClose.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            BtnClose.FillColor = Color.FromArgb(192, 0, 0);
            BtnClose.FillColor2 = Color.FromArgb(128, 0, 0);
            BtnClose.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            BtnClose.ForeColor = Color.White;
            BtnClose.HoverState.FillColor = Color.Red;
            BtnClose.HoverState.FillColor2 = Color.FromArgb(192, 0, 0);
            BtnClose.Location = new Point(745, 8);
            BtnClose.Name = "BtnClose";
            BtnClose.ShadowDecoration.CustomizableEdges = customizableEdges4;
            BtnClose.Size = new Size(40, 35);
            BtnClose.TabIndex = 3;
            BtnClose.Text = "✕";
            BtnClose.Click += BtnClose_Click;
            // 
            // BtnMinimize
            // 
            BtnMinimize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnMinimize.BorderRadius = 8;
            BtnMinimize.CustomizableEdges = customizableEdges5;
            BtnMinimize.DisabledState.BorderColor = Color.DarkGray;
            BtnMinimize.DisabledState.CustomBorderColor = Color.DarkGray;
            BtnMinimize.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            BtnMinimize.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            BtnMinimize.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            BtnMinimize.FillColor = Color.FromArgb(64, 64, 64);
            BtnMinimize.FillColor2 = Color.FromArgb(45, 45, 45);
            BtnMinimize.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            BtnMinimize.ForeColor = Color.White;
            BtnMinimize.HoverState.FillColor = Color.FromArgb(80, 80, 80);
            BtnMinimize.HoverState.FillColor2 = Color.FromArgb(60, 60, 60);
            BtnMinimize.Location = new Point(699, 8);
            BtnMinimize.Name = "BtnMinimize";
            BtnMinimize.ShadowDecoration.CustomizableEdges = customizableEdges6;
            BtnMinimize.Size = new Size(40, 35);
            BtnMinimize.TabIndex = 4;
            BtnMinimize.Text = "─";
            BtnMinimize.Click += BtnMinimize_Click;
            // 
            // panelStatus
            // 
            panelStatus.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelStatus.BackColor = Color.FromArgb(20, 20, 20);
            panelStatus.BorderRadius = 10;
            panelStatus.Controls.Add(lblStatus);
            panelStatus.Controls.Add(indicatorStatus);
            panelStatus.CustomizableEdges = customizableEdges10;
            panelStatus.Location = new Point(15, 65);
            panelStatus.Name = "panelStatus";
            panelStatus.ShadowDecoration.CustomizableEdges = customizableEdges11;
            panelStatus.Size = new Size(500, 70);
            panelStatus.TabIndex = 6;
            // 
            // lblStatus
            // 
            lblStatus.BackColor = Color.Transparent;
            lblStatus.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblStatus.ForeColor = Color.Cyan;
            lblStatus.Location = new Point(65, 23);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(151, 22);
            lblStatus.TabIndex = 1;
            lblStatus.Text = "Aguardando GTA V...";
            // 
            // indicatorStatus
            // 
            indicatorStatus.BackColor = Color.Transparent;
            indicatorStatus.FillColor = Color.FromArgb(30, 30, 30);
            indicatorStatus.FillThickness = 8;
            indicatorStatus.Font = new Font("Segoe UI", 12F);
            indicatorStatus.ForeColor = Color.White;
            indicatorStatus.Location = new Point(15, 15);
            indicatorStatus.Minimum = 0;
            indicatorStatus.Name = "indicatorStatus";
            indicatorStatus.ProgressColor = Color.Purple;
            indicatorStatus.ProgressColor2 = Color.Cyan;
            indicatorStatus.ProgressThickness = 8;
            indicatorStatus.ShadowDecoration.CustomizableEdges = customizableEdges9;
            indicatorStatus.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            indicatorStatus.Size = new Size(40, 40);
            indicatorStatus.TabIndex = 0;
            indicatorStatus.Text = "guna2CircleProgressBar1";
            // 
            // panelControls
            // 
            panelControls.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panelControls.BackColor = Color.FromArgb(20, 20, 20);
            panelControls.BorderRadius = 10;
            panelControls.Controls.Add(btnSettings);
            panelControls.Controls.Add(btnClearLog);
            panelControls.Controls.Add(chkAutoInject);
            panelControls.CustomizableEdges = customizableEdges16;
            panelControls.Location = new Point(525, 65);
            panelControls.Name = "panelControls";
            panelControls.ShadowDecoration.CustomizableEdges = customizableEdges17;
            panelControls.Size = new Size(260, 70);
            panelControls.TabIndex = 7;
            // 
            // btnSettings
            // 
            btnSettings.BorderRadius = 8;
            btnSettings.CustomizableEdges = customizableEdges12;
            btnSettings.DisabledState.BorderColor = Color.DarkGray;
            btnSettings.DisabledState.CustomBorderColor = Color.DarkGray;
            btnSettings.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnSettings.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            btnSettings.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnSettings.FillColor = Color.Purple;
            btnSettings.FillColor2 = Color.DarkMagenta;
            btnSettings.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSettings.ForeColor = Color.White;
            btnSettings.Location = new Point(135, 15);
            btnSettings.Name = "btnSettings";
            btnSettings.ShadowDecoration.CustomizableEdges = customizableEdges13;
            btnSettings.Size = new Size(110, 40);
            btnSettings.TabIndex = 2;
            btnSettings.Text = "⚙ Config";
            // 
            // btnClearLog
            // 
            btnClearLog.BorderRadius = 8;
            btnClearLog.CustomizableEdges = customizableEdges14;
            btnClearLog.DisabledState.BorderColor = Color.DarkGray;
            btnClearLog.DisabledState.CustomBorderColor = Color.DarkGray;
            btnClearLog.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnClearLog.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            btnClearLog.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnClearLog.FillColor = Color.FromArgb(64, 64, 64);
            btnClearLog.FillColor2 = Color.FromArgb(45, 45, 45);
            btnClearLog.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnClearLog.ForeColor = Color.White;
            btnClearLog.Location = new Point(15, 15);
            btnClearLog.Name = "btnClearLog";
            btnClearLog.ShadowDecoration.CustomizableEdges = customizableEdges15;
            btnClearLog.Size = new Size(110, 40);
            btnClearLog.TabIndex = 1;
            btnClearLog.Text = "🗑 Limpar";
            // 
            // chkAutoInject
            // 
            chkAutoInject.AutoSize = true;
            chkAutoInject.Checked = true;
            chkAutoInject.CheckedState.BorderColor = Color.Purple;
            chkAutoInject.CheckedState.BorderRadius = 0;
            chkAutoInject.CheckedState.BorderThickness = 0;
            chkAutoInject.CheckedState.FillColor = Color.Purple;
            chkAutoInject.CheckState = CheckState.Checked;
            chkAutoInject.Font = new Font("Segoe UI", 9F);
            chkAutoInject.ForeColor = Color.White;
            chkAutoInject.Location = new Point(220, 65);
            chkAutoInject.Name = "chkAutoInject";
            chkAutoInject.Size = new Size(15, 14);
            chkAutoInject.TabIndex = 0;
            chkAutoInject.UncheckedState.BorderColor = Color.FromArgb(125, 137, 149);
            chkAutoInject.UncheckedState.BorderRadius = 0;
            chkAutoInject.UncheckedState.BorderThickness = 0;
            chkAutoInject.UncheckedState.FillColor = Color.FromArgb(125, 137, 149);
            chkAutoInject.Visible = false;
            // 
            // guna2Separator1
            // 
            guna2Separator1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            guna2Separator1.FillColor = Color.FromArgb(40, 40, 40);
            guna2Separator1.FillThickness = 2;
            guna2Separator1.Location = new Point(15, 141);
            guna2Separator1.Name = "guna2Separator1";
            guna2Separator1.Size = new Size(770, 10);
            guna2Separator1.TabIndex = 8;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 18, 18);
            ClientSize = new Size(800, 555);
            Controls.Add(guna2Separator1);
            Controls.Add(panelControls);
            Controls.Add(panelStatus);
            Controls.Add(panelTop);
            Controls.Add(txtOut);
            FormBorderStyle = FormBorderStyle.None;
            MinimumSize = new Size(800, 555);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ScriptHook Loader";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelStatus.ResumeLayout(false);
            panelStatus.PerformLayout();
            panelControls.ResumeLayout(false);
            panelControls.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Guna.UI2.WinForms.Guna2TextBox txtOut;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblName;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
        private Guna.UI2.WinForms.Guna2GradientButton BtnClose;
        private Guna.UI2.WinForms.Guna2GradientButton BtnMinimize;
        private Guna.UI2.WinForms.Guna2Panel panelTop;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblVersion;
        private Guna.UI2.WinForms.Guna2Panel panelStatus;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblStatus;
        private Guna.UI2.WinForms.Guna2CircleProgressBar indicatorStatus;
        private Guna.UI2.WinForms.Guna2Panel panelControls;
        private Guna.UI2.WinForms.Guna2GradientButton btnSettings;
        private Guna.UI2.WinForms.Guna2GradientButton btnClearLog;
        private Guna.UI2.WinForms.Guna2CheckBox chkAutoInject;
        private Guna.UI2.WinForms.Guna2Separator guna2Separator1;
    }
}