namespace Updater
{
    partial class Updater
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Updater));
            rtbLogs = new RichTextBox();
            pnlTop = new Guna.UI2.WinForms.Guna2Panel();
            lblName = new Guna.UI2.WinForms.Guna2HtmlLabel();
            BtnClose = new Guna.UI2.WinForms.Guna2GradientButton();
            ProgressBar = new Guna.UI2.WinForms.Guna2ProgressBar();
            DragPnlTop = new Guna.UI2.WinForms.Guna2DragControl(components);
            guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(components);
            pnlTop.SuspendLayout();
            SuspendLayout();
            // 
            // rtbLogs
            // 
            rtbLogs.BackColor = Color.FromArgb(18, 18, 18);
            rtbLogs.BorderStyle = BorderStyle.None;
            rtbLogs.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rtbLogs.ForeColor = Color.Purple;
            rtbLogs.Location = new Point(12, 41);
            rtbLogs.Name = "rtbLogs";
            rtbLogs.ReadOnly = true;
            rtbLogs.Size = new Size(544, 263);
            rtbLogs.TabIndex = 0;
            rtbLogs.Text = "";
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.FromArgb(18, 18, 18);
            pnlTop.Controls.Add(lblName);
            pnlTop.Controls.Add(BtnClose);
            pnlTop.CustomizableEdges = customizableEdges3;
            pnlTop.Location = new Point(-1, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.ShadowDecoration.CustomizableEdges = customizableEdges4;
            pnlTop.Size = new Size(572, 35);
            pnlTop.TabIndex = 1;
            // 
            // lblName
            // 
            lblName.BackColor = Color.Transparent;
            lblName.ForeColor = Color.Purple;
            lblName.Location = new Point(13, 12);
            lblName.Name = "lblName";
            lblName.Size = new Size(121, 17);
            lblName.TabIndex = 5;
            lblName.Text = "Updater by 5pedrowx1";
            // 
            // BtnClose
            // 
            BtnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnClose.BorderRadius = 8;
            BtnClose.CustomizableEdges = customizableEdges1;
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
            BtnClose.Location = new Point(532, 4);
            BtnClose.Name = "BtnClose";
            BtnClose.ShadowDecoration.CustomizableEdges = customizableEdges2;
            BtnClose.Size = new Size(32, 27);
            BtnClose.TabIndex = 4;
            BtnClose.Text = "✕";
            BtnClose.Click += BtnClose_Click;
            // 
            // ProgressBar
            // 
            ProgressBar.CustomizableEdges = customizableEdges5;
            ProgressBar.FillColor = Color.FromArgb(18, 18, 18);
            ProgressBar.Location = new Point(-1, 310);
            ProgressBar.Name = "ProgressBar";
            ProgressBar.ProgressColor = Color.Purple;
            ProgressBar.ProgressColor2 = Color.DarkMagenta;
            ProgressBar.ShadowDecoration.CustomizableEdges = customizableEdges6;
            ProgressBar.Size = new Size(572, 15);
            ProgressBar.TabIndex = 2;
            ProgressBar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // DragPnlTop
            // 
            DragPnlTop.DockIndicatorTransparencyValue = 0.6D;
            DragPnlTop.TargetControl = pnlTop;
            DragPnlTop.UseTransparentDrag = true;
            // 
            // guna2Elipse1
            // 
            guna2Elipse1.BorderRadius = 17;
            guna2Elipse1.TargetControl = this;
            // 
            // Updater
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 18, 18);
            ClientSize = new Size(568, 327);
            Controls.Add(ProgressBar);
            Controls.Add(pnlTop);
            Controls.Add(rtbLogs);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Updater";
            Text = "Form1";
            Shown += UpdaterForm_Shown;
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox rtbLogs;
        private Guna.UI2.WinForms.Guna2Panel pnlTop;
        private Guna.UI2.WinForms.Guna2ProgressBar ProgressBar;
        private Guna.UI2.WinForms.Guna2GradientButton BtnClose;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblName;
        private Guna.UI2.WinForms.Guna2DragControl DragPnlTop;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
    }
}
