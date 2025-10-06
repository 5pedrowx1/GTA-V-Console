namespace Mod_With_Guna
{
    partial class MainForm
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.DragControl = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.Elipse = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.PanelTop = new Guna.UI2.WinForms.Guna2Panel();
            this.PaneMain = new Guna.UI2.WinForms.Guna2Panel();
            this.TabControl = new Guna.UI2.WinForms.Guna2TabControl();
            this.Chaos = new System.Windows.Forms.TabPage();
            this.Player = new System.Windows.Forms.TabPage();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.Veiculos = new System.Windows.Forms.TabPage();
            this.Armas = new System.Windows.Forms.TabPage();
            this.Mundo = new System.Windows.Forms.TabPage();
            this.Diversao = new System.Windows.Forms.TabPage();
            this.Teleporte = new System.Windows.Forms.TabPage();
            this.Spawner = new System.Windows.Forms.TabPage();
            this.ShadowForm = new Guna.UI2.WinForms.Guna2ShadowForm(this.components);
            this.PanelTop.SuspendLayout();
            this.TabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // DragControl
            // 
            this.DragControl.DockIndicatorTransparencyValue = 0.6D;
            this.DragControl.TargetControl = this.PanelTop;
            this.DragControl.UseTransparentDrag = true;
            // 
            // Elipse
            // 
            this.Elipse.BorderRadius = 15;
            this.Elipse.TargetControl = this;
            // 
            // PanelTop
            // 
            this.PanelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.PanelTop.Controls.Add(this.guna2HtmlLabel1);
            this.PanelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelTop.Location = new System.Drawing.Point(0, 0);
            this.PanelTop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PanelTop.Name = "PanelTop";
            this.PanelTop.Size = new System.Drawing.Size(933, 51);
            this.PanelTop.TabIndex = 0;
            // 
            // PaneMain
            // 
            this.PaneMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PaneMain.Location = new System.Drawing.Point(0, 57);
            this.PaneMain.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PaneMain.Name = "PaneMain";
            this.PaneMain.Size = new System.Drawing.Size(933, 393);
            this.PaneMain.TabIndex = 1;
            // 
            // TabControl
            // 
            this.TabControl.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.TabControl.Controls.Add(this.Chaos);
            this.TabControl.Controls.Add(this.Player);
            this.TabControl.Controls.Add(this.Veiculos);
            this.TabControl.Controls.Add(this.Armas);
            this.TabControl.Controls.Add(this.Mundo);
            this.TabControl.Controls.Add(this.Diversao);
            this.TabControl.Controls.Add(this.Teleporte);
            this.TabControl.Controls.Add(this.Spawner);
            this.TabControl.Font = new System.Drawing.Font("MS Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TabControl.ItemSize = new System.Drawing.Size(180, 40);
            this.TabControl.Location = new System.Drawing.Point(0, 57);
            this.TabControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(933, 393);
            this.TabControl.TabButtonHoverState.BorderColor = System.Drawing.Color.Empty;
            this.TabControl.TabButtonHoverState.FillColor = System.Drawing.Color.Purple;
            this.TabControl.TabButtonHoverState.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TabControl.TabButtonHoverState.ForeColor = System.Drawing.Color.White;
            this.TabControl.TabButtonHoverState.InnerColor = System.Drawing.Color.DarkMagenta;
            this.TabControl.TabButtonIdleState.BorderColor = System.Drawing.Color.Empty;
            this.TabControl.TabButtonIdleState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabControl.TabButtonIdleState.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TabControl.TabButtonIdleState.ForeColor = System.Drawing.Color.White;
            this.TabControl.TabButtonIdleState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.TabControl.TabButtonSelectedState.BorderColor = System.Drawing.Color.Empty;
            this.TabControl.TabButtonSelectedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabControl.TabButtonSelectedState.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TabControl.TabButtonSelectedState.ForeColor = System.Drawing.Color.Purple;
            this.TabControl.TabButtonSelectedState.InnerColor = System.Drawing.Color.DarkMagenta;
            this.TabControl.TabButtonSize = new System.Drawing.Size(180, 40);
            this.TabControl.TabIndex = 0;
            this.TabControl.TabMenuBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            // 
            // Chaos
            // 
            this.Chaos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Chaos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Chaos.Location = new System.Drawing.Point(184, 4);
            this.Chaos.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Chaos.Name = "Chaos";
            this.Chaos.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Chaos.Size = new System.Drawing.Size(745, 385);
            this.Chaos.TabIndex = 0;
            this.Chaos.Text = "CHAOS";
            // 
            // Player
            // 
            this.Player.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Player.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Player.Location = new System.Drawing.Point(184, 4);
            this.Player.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Player.Name = "Player";
            this.Player.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Player.Size = new System.Drawing.Size(745, 385);
            this.Player.TabIndex = 1;
            this.Player.Text = "PLAYER";
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("MS Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel1.ForeColor = System.Drawing.Color.Purple;
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(14, 16);
            this.guna2HtmlLabel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(255, 18);
            this.guna2HtmlLabel1.TabIndex = 0;
            this.guna2HtmlLabel1.Text = "GTA V Mod Menu by 5pedrowx1";
            // 
            // Veiculos
            // 
            this.Veiculos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Veiculos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Veiculos.Location = new System.Drawing.Point(184, 4);
            this.Veiculos.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Veiculos.Name = "Veiculos";
            this.Veiculos.Size = new System.Drawing.Size(745, 385);
            this.Veiculos.TabIndex = 2;
            this.Veiculos.Text = "VEÍCULOS";
            // 
            // Armas
            // 
            this.Armas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Armas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Armas.Location = new System.Drawing.Point(184, 4);
            this.Armas.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Armas.Name = "Armas";
            this.Armas.Size = new System.Drawing.Size(745, 385);
            this.Armas.TabIndex = 3;
            this.Armas.Text = "ARMAS";
            // 
            // Mundo
            // 
            this.Mundo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Mundo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Mundo.Location = new System.Drawing.Point(184, 4);
            this.Mundo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Mundo.Name = "Mundo";
            this.Mundo.Size = new System.Drawing.Size(745, 385);
            this.Mundo.TabIndex = 4;
            this.Mundo.Text = "MUNDO";
            // 
            // Diversao
            // 
            this.Diversao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Diversao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Diversao.Location = new System.Drawing.Point(184, 4);
            this.Diversao.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Diversao.Name = "Diversao";
            this.Diversao.Size = new System.Drawing.Size(745, 385);
            this.Diversao.TabIndex = 5;
            this.Diversao.Text = "DIVERSÃO";
            // 
            // Teleporte
            // 
            this.Teleporte.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Teleporte.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Teleporte.Location = new System.Drawing.Point(184, 4);
            this.Teleporte.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Teleporte.Name = "Teleporte";
            this.Teleporte.Size = new System.Drawing.Size(745, 385);
            this.Teleporte.TabIndex = 6;
            this.Teleporte.Text = "TELEPORTE";
            // 
            // Spawner
            // 
            this.Spawner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Spawner.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Spawner.Location = new System.Drawing.Point(184, 4);
            this.Spawner.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Spawner.Name = "Spawner";
            this.Spawner.Size = new System.Drawing.Size(745, 385);
            this.Spawner.TabIndex = 7;
            this.Spawner.Text = "SPAWNER";
            // 
            // ShadowForm
            // 
            this.ShadowForm.BorderRadius = 15;
            this.ShadowForm.ShadowColor = System.Drawing.Color.Purple;
            this.ShadowForm.TargetForm = this;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(933, 450);
            this.Controls.Add(this.TabControl);
            this.Controls.Add(this.PaneMain);
            this.Controls.Add(this.PanelTop);
            this.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GTA V Mod Menu by 5pedrowx1";
            this.PanelTop.ResumeLayout(false);
            this.PanelTop.PerformLayout();
            this.TabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2DragControl DragControl;
        private Guna.UI2.WinForms.Guna2Panel PanelTop;
        private Guna.UI2.WinForms.Guna2Elipse Elipse;
        private Guna.UI2.WinForms.Guna2Panel PaneMain;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2TabControl TabControl;
        private System.Windows.Forms.TabPage Chaos;
        private System.Windows.Forms.TabPage Player;
        private System.Windows.Forms.TabPage Veiculos;
        private System.Windows.Forms.TabPage Armas;
        private System.Windows.Forms.TabPage Mundo;
        private System.Windows.Forms.TabPage Diversao;
        private System.Windows.Forms.TabPage Teleporte;
        private System.Windows.Forms.TabPage Spawner;
        private Guna.UI2.WinForms.Guna2ShadowForm ShadowForm;
    }
}

