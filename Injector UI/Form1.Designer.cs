namespace Injector_UI
{
    partial class Form1
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            txtOut = new Guna.UI2.WinForms.Guna2TextBox();
            lblName = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(components);
            BtnClose = new Guna.UI2.WinForms.Guna2GradientButton();
            SuspendLayout();
            // 
            // txtOut
            // 
            txtOut.BorderRadius = 5;
            txtOut.BorderThickness = 0;
            txtOut.CustomizableEdges = customizableEdges1;
            txtOut.DefaultText = "";
            txtOut.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtOut.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtOut.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtOut.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtOut.FillColor = Color.FromArgb(10, 10, 10);
            txtOut.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtOut.Font = new Font("Segoe UI", 9F);
            txtOut.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtOut.Location = new Point(12, 48);
            txtOut.Multiline = true;
            txtOut.Name = "txtOut";
            txtOut.PlaceholderText = "";
            txtOut.ReadOnly = true;
            txtOut.SelectedText = "";
            txtOut.ShadowDecoration.CustomizableEdges = customizableEdges2;
            txtOut.Size = new Size(495, 260);
            txtOut.TabIndex = 1;
            // 
            // lblName
            // 
            lblName.BackColor = Color.Transparent;
            lblName.ForeColor = Color.Purple;
            lblName.Location = new Point(12, 16);
            lblName.Name = "lblName";
            lblName.Size = new Size(177, 17);
            lblName.TabIndex = 2;
            lblName.Text = "ScriptHook Loader by 5pedrowx1";
            // 
            // guna2DragControl1
            // 
            guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            guna2DragControl1.TargetControl = this;
            guna2DragControl1.UseTransparentDrag = true;
            // 
            // BtnClose
            // 
            BtnClose.BorderRadius = 10;
            BtnClose.CustomizableEdges = customizableEdges3;
            BtnClose.DisabledState.BorderColor = Color.DarkGray;
            BtnClose.DisabledState.CustomBorderColor = Color.DarkGray;
            BtnClose.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            BtnClose.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            BtnClose.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            BtnClose.FillColor = Color.Purple;
            BtnClose.Font = new Font("Comic Sans MS", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnClose.ForeColor = Color.White;
            BtnClose.Location = new Point(472, 12);
            BtnClose.Name = "BtnClose";
            BtnClose.ShadowDecoration.CustomizableEdges = customizableEdges4;
            BtnClose.Size = new Size(35, 30);
            BtnClose.TabIndex = 3;
            BtnClose.Text = "X";
            BtnClose.Click += BtnClose_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 18, 18);
            ClientSize = new Size(519, 320);
            Controls.Add(BtnClose);
            Controls.Add(lblName);
            Controls.Add(txtOut);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Guna.UI2.WinForms.Guna2TextBox txtOut;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblName;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
        private Guna.UI2.WinForms.Guna2GradientButton BtnClose;
    }
}
