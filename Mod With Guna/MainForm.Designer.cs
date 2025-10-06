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
            this.btnMinimize = new Guna.UI2.WinForms.Guna2Button();
            this.btnClose = new Guna.UI2.WinForms.Guna2Button();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.TabControl = new Guna.UI2.WinForms.Guna2TabControl();
            this.Chaos = new System.Windows.Forms.TabPage();
            this.btnExplosaoVeiculos = new Guna.UI2.WinForms.Guna2Button();
            this.btnChaosTotal = new Guna.UI2.WinForms.Guna2Button();
            this.togglePedsCrazy = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            this.lblPedsCrazy = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.Player = new System.Windows.Forms.TabPage();
            this.trackBarVida = new Guna.UI2.WinForms.Guna2TrackBar();
            this.lblVida = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.toggleGodMode = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            this.lblGodMode = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.toggleInfiniteStamina = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            this.lblStamina = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnCurarPlayer = new Guna.UI2.WinForms.Guna2Button();
            this.Veiculos = new System.Windows.Forms.TabPage();
            this.btnRepararVeiculo = new Guna.UI2.WinForms.Guna2Button();
            this.toggleVeiculoIndestrutivel = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            this.lblVeiculoIndestrutivel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.trackBarVelocidade = new Guna.UI2.WinForms.Guna2TrackBar();
            this.lblVelocidade = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.Armas = new System.Windows.Forms.TabPage();
            this.btnTodasArmas = new Guna.UI2.WinForms.Guna2Button();
            this.toggleMunicaoInfinita = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            this.lblMunicaoInfinita = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.toggleSemRecarga = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            this.lblSemRecarga = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.Mundo = new System.Windows.Forms.TabPage();
            this.comboBoxClima = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblClima = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.trackBarTempo = new Guna.UI2.WinForms.Guna2TrackBar();
            this.lblTempo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.toggleCongelarTempo = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            this.lblCongelarTempo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.Diversao = new System.Windows.Forms.TabPage();
            this.btnGravidadeLua = new Guna.UI2.WinForms.Guna2Button();
            this.btnCameraLenta = new Guna.UI2.WinForms.Guna2Button();
            this.btnModoRagdoll = new Guna.UI2.WinForms.Guna2Button();
            this.Teleporte = new System.Windows.Forms.TabPage();
            this.btnTpWaypoint = new Guna.UI2.WinForms.Guna2Button();
            this.btnTpVeiculo = new Guna.UI2.WinForms.Guna2Button();
            this.listBoxLocais = new System.Windows.Forms.ListBox();
            this.lblLocais = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.Spawner = new System.Windows.Forms.TabPage();
            this.txtSpawnModel = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblSpawnModel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnSpawnVeiculo = new Guna.UI2.WinForms.Guna2Button();
            this.btnSpawnPed = new Guna.UI2.WinForms.Guna2Button();
            this.ShadowForm = new Guna.UI2.WinForms.Guna2ShadowForm(this.components);
            this.PanelTop.SuspendLayout();
            this.TabControl.SuspendLayout();
            this.Chaos.SuspendLayout();
            this.Player.SuspendLayout();
            this.Veiculos.SuspendLayout();
            this.Armas.SuspendLayout();
            this.Mundo.SuspendLayout();
            this.Diversao.SuspendLayout();
            this.Teleporte.SuspendLayout();
            this.Spawner.SuspendLayout();
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
            this.PanelTop.Controls.Add(this.btnMinimize);
            this.PanelTop.Controls.Add(this.btnClose);
            this.PanelTop.Controls.Add(this.guna2HtmlLabel1);
            this.PanelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelTop.Location = new System.Drawing.Point(0, 0);
            this.PanelTop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PanelTop.Name = "PanelTop";
            this.PanelTop.Size = new System.Drawing.Size(933, 51);
            this.PanelTop.TabIndex = 0;
            // 
            // btnMinimize
            // 
            this.btnMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimize.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnMinimize.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnMinimize.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnMinimize.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnMinimize.FillColor = System.Drawing.Color.Transparent;
            this.btnMinimize.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.btnMinimize.ForeColor = System.Drawing.Color.Purple;
            this.btnMinimize.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnMinimize.Location = new System.Drawing.Point(837, 3);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(45, 45);
            this.btnMinimize.TabIndex = 2;
            this.btnMinimize.Text = "─";
            this.btnMinimize.TextOffset = new System.Drawing.Point(0, -5);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnClose.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnClose.FillColor = System.Drawing.Color.Transparent;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.Purple;
            this.btnClose.HoverState.FillColor = System.Drawing.Color.Red;
            this.btnClose.Location = new System.Drawing.Point(885, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(45, 45);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "✕";
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
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl.Font = new System.Drawing.Font("MS Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TabControl.ItemSize = new System.Drawing.Size(180, 40);
            this.TabControl.Location = new System.Drawing.Point(0, 51);
            this.TabControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(933, 399);
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
            this.Chaos.Controls.Add(this.btnExplosaoVeiculos);
            this.Chaos.Controls.Add(this.btnChaosTotal);
            this.Chaos.Controls.Add(this.togglePedsCrazy);
            this.Chaos.Controls.Add(this.lblPedsCrazy);
            this.Chaos.ForeColor = System.Drawing.Color.White;
            this.Chaos.Location = new System.Drawing.Point(184, 4);
            this.Chaos.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Chaos.Name = "Chaos";
            this.Chaos.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Chaos.Size = new System.Drawing.Size(745, 391);
            this.Chaos.TabIndex = 0;
            this.Chaos.Text = "CHAOS";
            // 
            // btnExplosaoVeiculos
            // 
            this.btnExplosaoVeiculos.BorderRadius = 8;
            this.btnExplosaoVeiculos.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnExplosaoVeiculos.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnExplosaoVeiculos.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnExplosaoVeiculos.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnExplosaoVeiculos.FillColor = System.Drawing.Color.Purple;
            this.btnExplosaoVeiculos.Font = new System.Drawing.Font("MS Gothic", 10F);
            this.btnExplosaoVeiculos.ForeColor = System.Drawing.Color.White;
            this.btnExplosaoVeiculos.Location = new System.Drawing.Point(30, 120);
            this.btnExplosaoVeiculos.Name = "btnExplosaoVeiculos";
            this.btnExplosaoVeiculos.Size = new System.Drawing.Size(200, 45);
            this.btnExplosaoVeiculos.TabIndex = 3;
            this.btnExplosaoVeiculos.Text = "Explodir Veículos";
            // 
            // btnChaosTotal
            // 
            this.btnChaosTotal.BorderRadius = 8;
            this.btnChaosTotal.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnChaosTotal.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnChaosTotal.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnChaosTotal.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnChaosTotal.FillColor = System.Drawing.Color.DarkMagenta;
            this.btnChaosTotal.Font = new System.Drawing.Font("MS Gothic", 10F);
            this.btnChaosTotal.ForeColor = System.Drawing.Color.White;
            this.btnChaosTotal.Location = new System.Drawing.Point(30, 180);
            this.btnChaosTotal.Name = "btnChaosTotal";
            this.btnChaosTotal.Size = new System.Drawing.Size(200, 45);
            this.btnChaosTotal.TabIndex = 2;
            this.btnChaosTotal.Text = "Modo Chaos Total";
            // 
            // togglePedsCrazy
            // 
            this.togglePedsCrazy.Animated = true;
            this.togglePedsCrazy.CheckedState.BorderColor = System.Drawing.Color.Purple;
            this.togglePedsCrazy.CheckedState.FillColor = System.Drawing.Color.Purple;
            this.togglePedsCrazy.CheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.togglePedsCrazy.CheckedState.InnerColor = System.Drawing.Color.White;
            this.togglePedsCrazy.Location = new System.Drawing.Point(30, 60);
            this.togglePedsCrazy.Name = "togglePedsCrazy";
            this.togglePedsCrazy.Size = new System.Drawing.Size(50, 25);
            this.togglePedsCrazy.TabIndex = 1;
            this.togglePedsCrazy.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.togglePedsCrazy.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.togglePedsCrazy.UncheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.togglePedsCrazy.UncheckedState.InnerColor = System.Drawing.Color.White;
            // 
            // lblPedsCrazy
            // 
            this.lblPedsCrazy.BackColor = System.Drawing.Color.Transparent;
            this.lblPedsCrazy.Font = new System.Drawing.Font("MS Gothic", 10F);
            this.lblPedsCrazy.ForeColor = System.Drawing.Color.White;
            this.lblPedsCrazy.Location = new System.Drawing.Point(90, 63);
            this.lblPedsCrazy.Name = "lblPedsCrazy";
            this.lblPedsCrazy.Size = new System.Drawing.Size(104, 15);
            this.lblPedsCrazy.TabIndex = 0;
            this.lblPedsCrazy.Text = "Peds Agressivos";
            // 
            // Player
            // 
            this.Player.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Player.Controls.Add(this.btnCurarPlayer);
            this.Player.Controls.Add(this.trackBarVida);
            this.Player.Controls.Add(this.lblVida);
            this.Player.Controls.Add(this.toggleGodMode);
            this.Player.Controls.Add(this.lblGodMode);
            this.Player.Controls.Add(this.toggleInfiniteStamina);
            this.Player.Controls.Add(this.lblStamina);
            this.Player.ForeColor = System.Drawing.Color.White;
            this.Player.Location = new System.Drawing.Point(184, 4);
            this.Player.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Player.Name = "Player";
            this.Player.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Player.Size = new System.Drawing.Size(745, 391);
            this.Player.TabIndex = 1;
            this.Player.Text = "PLAYER";
            // 
            // trackBarVida
            // 
            this.trackBarVida.Location = new System.Drawing.Point(30, 195);
            this.trackBarVida.Maximum = 200;
            this.trackBarVida.Name = "trackBarVida";
            this.trackBarVida.Size = new System.Drawing.Size(300, 23);
            this.trackBarVida.TabIndex = 6;
            this.trackBarVida.ThumbColor = System.Drawing.Color.Purple;
            this.trackBarVida.Value = 100;
            // 
            // lblVida
            // 
            this.lblVida.BackColor = System.Drawing.Color.Transparent;
            this.lblVida.Font = new System.Drawing.Font("MS Gothic", 10F);
            this.lblVida.ForeColor = System.Drawing.Color.White;
            this.lblVida.Location = new System.Drawing.Point(30, 168);
            this.lblVida.Name = "lblVida";
            this.lblVida.Size = new System.Drawing.Size(88, 15);
            this.lblVida.TabIndex = 5;
            this.lblVida.Text = "Vida: 100/200";
            // 
            // toggleGodMode
            // 
            this.toggleGodMode.Animated = true;
            this.toggleGodMode.CheckedState.BorderColor = System.Drawing.Color.Purple;
            this.toggleGodMode.CheckedState.FillColor = System.Drawing.Color.Purple;
            this.toggleGodMode.CheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.toggleGodMode.CheckedState.InnerColor = System.Drawing.Color.White;
            this.toggleGodMode.Location = new System.Drawing.Point(30, 40);
            this.toggleGodMode.Name = "toggleGodMode";
            this.toggleGodMode.Size = new System.Drawing.Size(50, 25);
            this.toggleGodMode.TabIndex = 4;
            this.toggleGodMode.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.toggleGodMode.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.toggleGodMode.UncheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.toggleGodMode.UncheckedState.InnerColor = System.Drawing.Color.White;
            // 
            // lblGodMode
            // 
            this.lblGodMode.BackColor = System.Drawing.Color.Transparent;
            this.lblGodMode.Font = new System.Drawing.Font("MS Gothic", 10F);
            this.lblGodMode.ForeColor = System.Drawing.Color.White;
            this.lblGodMode.Location = new System.Drawing.Point(90, 43);
            this.lblGodMode.Name = "lblGodMode";
            this.lblGodMode.Size = new System.Drawing.Size(112, 15);
            this.lblGodMode.TabIndex = 3;
            this.lblGodMode.Text = "Modo Invencível";
            // 
            // toggleInfiniteStamina
            // 
            this.toggleInfiniteStamina.Animated = true;
            this.toggleInfiniteStamina.CheckedState.BorderColor = System.Drawing.Color.Purple;
            this.toggleInfiniteStamina.CheckedState.FillColor = System.Drawing.Color.Purple;
            this.toggleInfiniteStamina.CheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.toggleInfiniteStamina.CheckedState.InnerColor = System.Drawing.Color.White;
            this.toggleInfiniteStamina.Location = new System.Drawing.Point(30, 90);
            this.toggleInfiniteStamina.Name = "toggleInfiniteStamina";
            this.toggleInfiniteStamina.Size = new System.Drawing.Size(50, 25);
            this.toggleInfiniteStamina.TabIndex = 2;
            this.toggleInfiniteStamina.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.toggleInfiniteStamina.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.toggleInfiniteStamina.UncheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.toggleInfiniteStamina.UncheckedState.InnerColor = System.Drawing.Color.White;
            // 
            // lblStamina
            // 
            this.lblStamina.BackColor = System.Drawing.Color.Transparent;
            this.lblStamina.Font = new System.Drawing.Font("MS Gothic", 10F);
            this.lblStamina.ForeColor = System.Drawing.Color.White;
            this.lblStamina.Location = new System.Drawing.Point(90, 93);
            this.lblStamina.Name = "lblStamina";
            this.lblStamina.Size = new System.Drawing.Size(112, 15);
            this.lblStamina.TabIndex = 1;
            this.lblStamina.Text = "Stamina Infinita";
            // 
            // btnCurarPlayer
            // 
            this.btnCurarPlayer.BorderRadius = 8;
            this.btnCurarPlayer.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCurarPlayer.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCurarPlayer.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCurarPlayer.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCurarPlayer.FillColor = System.Drawing.Color.Purple;
            this.btnCurarPlayer.Font = new System.Drawing.Font("MS Gothic", 10F);
            this.btnCurarPlayer.ForeColor = System.Drawing.Color.White;
            this.btnCurarPlayer.Location = new System.Drawing.Point(30, 240);
            this.btnCurarPlayer.Name = "btnCurarPlayer";
            this.btnCurarPlayer.Size = new System.Drawing.Size(200, 45);
            this.btnCurarPlayer.TabIndex = 7;
            this.btnCurarPlayer.Text = "Curar Completamente";
            // 
            // Veiculos
            // 
            this.Veiculos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Veiculos.Controls.Add(this.trackBarVelocidade);
            this.Veiculos.Controls.Add(this.lblVelocidade);
            this.Veiculos.Controls.Add(this.btnRepararVeiculo);
            this.Veiculos.Controls.Add(this.toggleVeiculoIndestrutivel);
            this.Veiculos.Controls.Add(this.lblVeiculoIndestrutivel);
            this.Veiculos.ForeColor = System.Drawing.Color.White;
            this.Veiculos.Location = new System.Drawing.Point(184, 4);
            this.Veiculos.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Veiculos.Name = "Veiculos";
            this.Veiculos.Size = new System.Drawing.Size(745, 391);
            this.Veiculos.TabIndex = 2;
            this.Veiculos.Text = "VEÍCULOS";
            // 
            // btnRepararVeiculo
            // 
            this.btnRepararVeiculo.BorderRadius = 8;
            this.btnRepararVeiculo.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnRepararVeiculo.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnRepararVeiculo.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnRepararVeiculo.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnRepararVeiculo.FillColor = System.Drawing.Color.Purple;
            this.btnRepararVeiculo.Font = new System.Drawing.Font("MS Gothic", 10F);
            this.btnRepararVeiculo.ForeColor = System.Drawing.Color.White;
            this.btnRepararVeiculo.Location = new System.Drawing.Point(30, 90);
            this.btnRepararVeiculo.Name = "btnRepararVeiculo";
            this.btnRepararVeiculo.Size = new System.Drawing.Size(200, 45);
            this.btnRepararVeiculo.TabIndex = 4;
            this.btnRepararVeiculo.Text = "Reparar Veículo";
            // 
            // toggleVeiculoIndestrutivel
            // 
            this.toggleVeiculoIndestrutivel.Animated = true;
            this.toggleVeiculoIndestrutivel.CheckedState.BorderColor = System.Drawing.Color.Purple;
            this.toggleVeiculoIndestrutivel.CheckedState.FillColor = System.Drawing.Color.Purple;
            this.toggleVeiculoIndestrutivel.CheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.toggleVeiculoIndestrutivel.CheckedState.InnerColor = System.Drawing.Color.White;
            this.toggleVeiculoIndestrutivel.Location = new System.Drawing.Point(30, 40);
            this.toggleVeiculoIndestrutivel.Name = "toggleVeiculoIndestrutivel";
            this.toggleVeiculoIndestrutivel.Size = new System.Drawing.Size(50, 25);
            this.toggleVeiculoIndestrutivel.TabIndex = 3;
            this.toggleVeiculoIndestrutivel.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.toggleVeiculoIndestrutivel.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.toggleVeiculoIndestrutivel.UncheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.toggleVeiculoIndestrutivel.UncheckedState.InnerColor = System.Drawing.Color.White;
            // 
            // lblVeiculoIndestrutivel
            // 
            this.lblVeiculoIndestrutivel.BackColor = System.Drawing.Color.Transparent;
            this.lblVeiculoIndestrutivel.Font = new System.Drawing.Font("MS Gothic", 10F);
            this.lblVeiculoIndestrutivel.ForeColor = System.Drawing.Color.White;
            this.lblVeiculoIndestrutivel.Location = new System.Drawing.Point(90, 43);
            this.lblVeiculoIndestrutivel.Name = "lblVeiculoIndestrutivel";
            this.lblVeiculoIndestrutivel.Size = new System.Drawing.Size(144, 15);
            this.lblVeiculoIndestrutivel.TabIndex = 2;
            this.lblVeiculoIndestrutivel.Text = "Veículo Indestrutível";
            // 
            // trackBarVelocidade
            // 
            this.trackBarVelocidade.Location = new System.Drawing.Point(30, 180);
            this.trackBarVelocidade.Maximum = 300;
            this.trackBarVelocidade.Name = "trackBarVelocidade";
            this.trackBarVelocidade.Size = new System.Drawing.Size(300, 23);
            this.trackBarVelocidade.TabIndex = 6;
            this.trackBarVelocidade.ThumbColor = System.Drawing.Color.Purple;
            this.trackBarVelocidade.Value = 100;
            // 
            // lblVelocidade
            // 
            this.lblVelocidade.BackColor = System.Drawing.Color.Transparent;
            this.lblVelocidade.Font = new System.Drawing.Font("MS Gothic", 10F);
            this.lblVelocidade.ForeColor = System.Drawing.Color.White;
            this.lblVelocidade.Location = new System.Drawing.Point(30, 155);
            this.lblVelocidade.Name = "lblVelocidade";
            this.lblVelocidade.Size = new System.Drawing.Size(176, 15);
            this.lblVelocidade.TabIndex = 5;
            this.lblVelocidade.Text = "Velocidade Máxima: 100%";
            // 
            // Armas
            // 
            this.Armas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Armas.Controls.Add(this.btnTodasArmas);
            this.Armas.Controls.Add(this.toggleMunicaoInfinita);
            this.Armas.Controls.Add(this.lblMunicaoInfinita);
            this.Armas.Controls.Add(this.toggleSemRecarga);
            this.Armas.Controls.Add(this.lblSemRecarga);
            this.Armas.ForeColor = System.Drawing.Color.White;
            this.Armas.Location = new System.Drawing.Point(184, 4);
            this.Armas.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Armas.Name = "Armas";
            this.Armas.Size = new System.Drawing.Size(745, 391);
            this.Armas.TabIndex = 3;
            this.Armas.Text = "ARMAS";
            // 
            // btnTodasArmas
            // 
            this.btnTodasArmas.BorderRadius = 8;
            this.btnTodasArmas.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnTodasArmas.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnTodasArmas.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnTodasArmas.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnTodasArmas.FillColor = System.Drawing.Color.Purple;
            this.btnTodasArmas.Font = new System.Drawing.Font("MS Gothic", 10F);
            this.btnTodasArmas.ForeColor = System.Drawing.Color.White;
            this.btnTodasArmas.Location = new System.Drawing.Point(30, 140);
            this.btnTodasArmas.Name = "btnTodasArmas";
            this.btnTodasArmas.Size = new System.Drawing.Size(200, 45);
            this.btnTodasArmas.TabIndex = 4;
            this.btnTodasArmas.Text = "Obter Todas as Armas";
            // 
            // toggleMunicaoInfinita
            // 
            this.toggleMunicaoInfinita.Animated = true;
            this.toggleMunicaoInfinita.CheckedState.BorderColor = System.Drawing.Color.Purple;
            this.toggleMunicaoInfinita.CheckedState.FillColor = System.Drawing.Color.Purple;
            this.toggleMunicaoInfinita.CheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.toggleMunicaoInfinita.CheckedState.InnerColor = System.Drawing.Color.White;
            this.toggleMunicaoInfinita.Location = new System.Drawing.Point(30, 40);
            this.toggleMunicaoInfinita.Name = "toggleMunicaoInfinita";
            this.toggleMunicaoInfinita.Size = new System.Drawing.Size(50, 25);
            this.toggleMunicaoInfinita.TabIndex = 3;
            this.toggleMunicaoInfinita.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.toggleMunicaoInfinita.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.toggleMunicaoInfinita.UncheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.toggleMunicaoInfinita.UncheckedState.InnerColor = System.Drawing.Color.White;
            // 
            // lblMunicaoInfinita
            // 
            this.lblMunicaoInfinita.BackColor = System.Drawing.Color.Transparent;
            this.lblMunicaoInfinita.Font = new System.Drawing.Font("MS Gothic", 10F);
            this.lblMunicaoInfinita.ForeColor = System.Drawing.Color.White;
            this.lblMunicaoInfinita.Location = new System.Drawing.Point(90, 43);
            this.lblMunicaoInfinita.Name = "lblMunicaoInfinita";
            this.lblMunicaoInfinita.Size = new System.Drawing.Size(120, 15);
            this.lblMunicaoInfinita.TabIndex = 2;
            this.lblMunicaoInfinita.Text = "Munição Infinita";
            // 
            // toggleSemRecarga
            // 
            this.toggleSemRecarga.Animated = true;
            this.toggleSemRecarga.CheckedState.BorderColor = System.Drawing.Color.Purple;
            this.toggleSemRecarga.CheckedState.FillColor = System.Drawing.Color.Purple;
            this.toggleSemRecarga.CheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.toggleSemRecarga.CheckedState.InnerColor = System.Drawing.Color.White;
            this.toggleSemRecarga.Location = new System.Drawing.Point(30, 90);
            this.toggleSemRecarga.Name = "toggleSemRecarga";
            this.toggleSemRecarga.Size = new System.Drawing.Size(50, 25);
            this.toggleSemRecarga.TabIndex = 1;
            this.toggleSemRecarga.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.toggleSemRecarga.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.toggleSemRecarga.UncheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.toggleSemRecarga.UncheckedState.InnerColor = System.Drawing.Color.White;
            // 
            // lblSemRecarga
            // 
            this.lblSemRecarga.BackColor = System.Drawing.Color.Transparent;
            this.lblSemRecarga.Font = new System.Drawing.Font("MS Gothic", 10F);
            this.lblSemRecarga.ForeColor = System.Drawing.Color.White;
            this.lblSemRecarga.Location = new System.Drawing.Point(90, 93);
            this.lblSemRecarga.Name = "lblSemRecarga";
            this.lblSemRecarga.Size = new System.Drawing.Size(88, 15);
            this.lblSemRecarga.TabIndex = 0;
            this.lblSemRecarga.Text = "Sem Recarga";
            // 
            // Mundo
            // 
            this.Mundo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Mundo.Controls.Add(this.comboBoxClima);
            this.Mundo.Controls.Add(this.lblClima);
            this.Mundo.Controls.Add(this.trackBarTempo);
            this.Mundo.Controls.Add(this.lblTempo);
            this.Mundo.Controls.Add(this.toggleCongelarTempo);
            this.Mundo.Controls.Add(this.lblCongelarTempo);
            this.Mundo.ForeColor = System.Drawing.Color.White;
            this.Mundo.Location = new System.Drawing.Point(184, 4);
            this.Mundo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Mundo.Name = "Mundo";
            this.Mundo.Size = new System.Drawing.Size(745, 391);
            this.Mundo.TabIndex = 4;
            this.Mundo.Text = "MUNDO";
            // 
            // comboBoxClima
            // 
            this.comboBoxClima.BackColor = System.Drawing.Color.Transparent;
            this.comboBoxClima.BorderColor = System.Drawing.Color.Purple;
            this.comboBoxClima.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxClima.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxClima.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.comboBoxClima.FocusedColor = System.Drawing.Color.Purple;
            this.comboBoxClima.FocusedState.BorderColor = System.Drawing.Color.Purple;
            this.comboBoxClima.Font = new System.Drawing.Font("MS Gothic", 10F);
            this.comboBoxClima.ForeColor = System.Drawing.Color.White;
            this.comboBoxClima.ItemHeight = 30;
            this.comboBoxClima.Items.AddRange(new object[] {
            "Ensolarado",
            "Claro",
            "Nublado",
            "Chuvoso",
            "Trovões",
            "Neblina",
            "Neve"});
            this.comboBoxClima.Location = new System.Drawing.Point(30, 180);
            this.comboBoxClima.Name = "comboBoxClima";
            this.comboBoxClima.Size = new System.Drawing.Size(200, 36);
            this.comboBoxClima.TabIndex = 5;
            // 
            // lblClima
            // 
            this.lblClima.BackColor = System.Drawing.Color.Transparent;
            this.lblClima.Font = new System.Drawing.Font("MS Gothic", 10F);
            this.lblClima.ForeColor = System.Drawing.Color.White;
            this.lblClima.Location = new System.Drawing.Point(30, 155);
            this.lblClima.Name = "lblClima";
            this.lblClima.Size = new System.Drawing.Size(40, 15);
            this.lblClima.TabIndex = 4;
            this.lblClima.Text = "Clima";
            // 
            // trackBarTempo
            // 
            this.trackBarTempo.Location = new System.Drawing.Point(30, 115);
            this.trackBarTempo.Maximum = 23;
            this.trackBarTempo.Name = "trackBarTempo";
            this.trackBarTempo.Size = new System.Drawing.Size(300, 23);
            this.trackBarTempo.TabIndex = 3;
            this.trackBarTempo.ThumbColor = System.Drawing.Color.Purple;
            this.trackBarTempo.Value = 12;
            // 
            // lblTempo
            // 
            this.lblTempo.BackColor = System.Drawing.Color.Transparent;
            this.lblTempo.Font = new System.Drawing.Font("MS Gothic", 10F);
            this.lblTempo.ForeColor = System.Drawing.Color.White;
            this.lblTempo.Location = new System.Drawing.Point(30, 90);
            this.lblTempo.Name = "lblTempo";
            this.lblTempo.Size = new System.Drawing.Size(80, 15);
            this.lblTempo.TabIndex = 2;
            this.lblTempo.Text = "Hora: 12:00";
            // 
            // toggleCongelarTempo
            // 
            this.toggleCongelarTempo.Animated = true;
            this.toggleCongelarTempo.CheckedState.BorderColor = System.Drawing.Color.Purple;
            this.toggleCongelarTempo.CheckedState.FillColor = System.Drawing.Color.Purple;
            this.toggleCongelarTempo.CheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.toggleCongelarTempo.CheckedState.InnerColor = System.Drawing.Color.White;
            this.toggleCongelarTempo.Location = new System.Drawing.Point(30, 40);
            this.toggleCongelarTempo.Name = "toggleCongelarTempo";
            this.toggleCongelarTempo.Size = new System.Drawing.Size(50, 25);
            this.toggleCongelarTempo.TabIndex = 1;
            this.toggleCongelarTempo.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.toggleCongelarTempo.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.toggleCongelarTempo.UncheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.toggleCongelarTempo.UncheckedState.InnerColor = System.Drawing.Color.White;
            // 
            // lblCongelarTempo
            // 
            this.lblCongelarTempo.BackColor = System.Drawing.Color.Transparent;
            this.lblCongelarTempo.Font = new System.Drawing.Font("MS Gothic", 10F);
            this.lblCongelarTempo.ForeColor = System.Drawing.Color.White;
            this.lblCongelarTempo.Location = new System.Drawing.Point(90, 43);
            this.lblCongelarTempo.Name = "lblCongelarTempo";
            this.lblCongelarTempo.Size = new System.Drawing.Size(104, 15);
            this.lblCongelarTempo.TabIndex = 0;
            this.lblCongelarTempo.Text = "Congelar Tempo";
            // 
            // Diversao
            // 
            this.Diversao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Diversao.Controls.Add(this.btnGravidadeLua);
            this.Diversao.Controls.Add(this.btnCameraLenta);
            this.Diversao.Controls.Add(this.btnModoRagdoll);
            this.Diversao.ForeColor = System.Drawing.Color.White;
            this.Diversao.Location = new System.Drawing.Point(184, 4);
            this.Diversao.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Diversao.Name = "Diversao";
            this.Diversao.Size = new System.Drawing.Size(745, 391);
            this.Diversao.TabIndex = 5;
            this.Diversao.Text = "DIVERSÃO";
            // 
            // btnGravidadeLua
            // 
            this.btnGravidadeLua.BorderRadius = 8;
            this.btnGravidadeLua.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnGravidadeLua.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnGravidadeLua.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnGravidadeLua.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnGravidadeLua.FillColor = System.Drawing.Color.Purple;
            this.btnGravidadeLua.Font = new System.Drawing.Font("MS Gothic", 10F);
            this.btnGravidadeLua.ForeColor = System.Drawing.Color.White;
            this.btnGravidadeLua.Location = new System.Drawing.Point(30, 40);
            this.btnGravidadeLua.Name = "btnGravidadeLua";
            this.btnGravidadeLua.Size = new System.Drawing.Size(200, 45);
            this.btnGravidadeLua.TabIndex = 2;
            this.btnGravidadeLua.Text = "Gravidade da Lua";
            // 
            // btnCameraLenta
            // 
            this.btnCameraLenta.BorderRadius = 8;
            this.btnCameraLenta.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCameraLenta.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCameraLenta.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCameraLenta.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCameraLenta.FillColor = System.Drawing.Color.DarkMagenta;
            this.btnCameraLenta.Font = new System.Drawing.Font("MS Gothic", 10F);
            this.btnCameraLenta.ForeColor = System.Drawing.Color.White;
            this.btnCameraLenta.Location = new System.Drawing.Point(30, 100);
            this.btnCameraLenta.Name = "btnCameraLenta";
            this.btnCameraLenta.Size = new System.Drawing.Size(200, 45);
            this.btnCameraLenta.TabIndex = 1;
            this.btnCameraLenta.Text = "Câmera Lenta";
            // 
            // btnModoRagdoll
            // 
            this.btnModoRagdoll.BorderRadius = 8;
            this.btnModoRagdoll.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnModoRagdoll.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnModoRagdoll.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnModoRagdoll.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnModoRagdoll.FillColor = System.Drawing.Color.Purple;
            this.btnModoRagdoll.Font = new System.Drawing.Font("MS Gothic", 10F);
            this.btnModoRagdoll.ForeColor = System.Drawing.Color.White;
            this.btnModoRagdoll.Location = new System.Drawing.Point(30, 160);
            this.btnModoRagdoll.Name = "btnModoRagdoll";
            this.btnModoRagdoll.Size = new System.Drawing.Size(200, 45);
            this.btnModoRagdoll.TabIndex = 0;
            this.btnModoRagdoll.Text = "Modo Ragdoll";
            // 
            // Teleporte
            // 
            this.Teleporte.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Teleporte.Controls.Add(this.btnTpWaypoint);
            this.Teleporte.Controls.Add(this.btnTpVeiculo);
            this.Teleporte.Controls.Add(this.listBoxLocais);
            this.Teleporte.Controls.Add(this.lblLocais);
            this.Teleporte.ForeColor = System.Drawing.Color.White;
            this.Teleporte.Location = new System.Drawing.Point(184, 4);
            this.Teleporte.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Teleporte.Name = "Teleporte";
            this.Teleporte.Size = new System.Drawing.Size(745, 391);
            this.Teleporte.TabIndex = 6;
            this.Teleporte.Text = "TELEPORTE";
            // 
            // btnTpWaypoint
            // 
            this.btnTpWaypoint.BorderRadius = 8;
            this.btnTpWaypoint.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnTpWaypoint.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnTpWaypoint.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnTpWaypoint.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnTpWaypoint.FillColor = System.Drawing.Color.Purple;
            this.btnTpWaypoint.Font = new System.Drawing.Font("MS Gothic", 10F);
            this.btnTpWaypoint.ForeColor = System.Drawing.Color.White;
            this.btnTpWaypoint.Location = new System.Drawing.Point(400, 40);
            this.btnTpWaypoint.Name = "btnTpWaypoint";
            this.btnTpWaypoint.Size = new System.Drawing.Size(200, 45);
            this.btnTpWaypoint.TabIndex = 3;
            this.btnTpWaypoint.Text = "TP para Waypoint";
            // 
            // btnTpVeiculo
            // 
            this.btnTpVeiculo.BorderRadius = 8;
            this.btnTpVeiculo.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnTpVeiculo.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnTpVeiculo.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnTpVeiculo.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnTpVeiculo.FillColor = System.Drawing.Color.DarkMagenta;
            this.btnTpVeiculo.Font = new System.Drawing.Font("MS Gothic", 10F);
            this.btnTpVeiculo.ForeColor = System.Drawing.Color.White;
            this.btnTpVeiculo.Location = new System.Drawing.Point(400, 100);
            this.btnTpVeiculo.Name = "btnTpVeiculo";
            this.btnTpVeiculo.Size = new System.Drawing.Size(200, 45);
            this.btnTpVeiculo.TabIndex = 2;
            this.btnTpVeiculo.Text = "TP Veículo para Mim";
            // 
            // listBoxLocais
            // 
            this.listBoxLocais.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.listBoxLocais.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxLocais.Font = new System.Drawing.Font("MS Gothic", 9F);
            this.listBoxLocais.ForeColor = System.Drawing.Color.White;
            this.listBoxLocais.FormattingEnabled = true;
            this.listBoxLocais.ItemHeight = 12;
            this.listBoxLocais.Items.AddRange(new object[] {
            "Centro de Los Santos",
            "Aeroporto Internacional",
            "Pier de Vespucci",
            "Monte Chiliad",
            "Vinewood Hills",
            "Paleto Bay",
            "Fort Zancudo",
            "Sandy Shores",
            "Praia de Del Perro",
            "Maze Bank Tower"});
            this.listBoxLocais.Location = new System.Drawing.Point(30, 70);
            this.listBoxLocais.Name = "listBoxLocais";
            this.listBoxLocais.Size = new System.Drawing.Size(300, 242);
            this.listBoxLocais.TabIndex = 1;
            // 
            // lblLocais
            // 
            this.lblLocais.BackColor = System.Drawing.Color.Transparent;
            this.lblLocais.Font = new System.Drawing.Font("MS Gothic", 10F);
            this.lblLocais.ForeColor = System.Drawing.Color.White;
            this.lblLocais.Location = new System.Drawing.Point(30, 40);
            this.lblLocais.Name = "lblLocais";
            this.lblLocais.Size = new System.Drawing.Size(112, 15);
            this.lblLocais.TabIndex = 0;
            this.lblLocais.Text = "Locais Populares";
            // 
            // Spawner
            // 
            this.Spawner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Spawner.Controls.Add(this.txtSpawnModel);
            this.Spawner.Controls.Add(this.lblSpawnModel);
            this.Spawner.Controls.Add(this.btnSpawnVeiculo);
            this.Spawner.Controls.Add(this.btnSpawnPed);
            this.Spawner.ForeColor = System.Drawing.Color.White;
            this.Spawner.Location = new System.Drawing.Point(184, 4);
            this.Spawner.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Spawner.Name = "Spawner";
            this.Spawner.Size = new System.Drawing.Size(745, 391);
            this.Spawner.TabIndex = 7;
            this.Spawner.Text = "SPAWNER";
            // 
            // txtSpawnModel
            // 
            this.txtSpawnModel.BorderColor = System.Drawing.Color.Purple;
            this.txtSpawnModel.BorderRadius = 8;
            this.txtSpawnModel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSpawnModel.DefaultText = "";
            this.txtSpawnModel.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtSpawnModel.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtSpawnModel.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSpawnModel.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSpawnModel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txtSpawnModel.FocusedState.BorderColor = System.Drawing.Color.Purple;
            this.txtSpawnModel.Font = new System.Drawing.Font("MS Gothic", 10F);
            this.txtSpawnModel.ForeColor = System.Drawing.Color.White;
            this.txtSpawnModel.HoverState.BorderColor = System.Drawing.Color.Purple;
            this.txtSpawnModel.Location = new System.Drawing.Point(30, 70);
            this.txtSpawnModel.Name = "txtSpawnModel";
            this.txtSpawnModel.PasswordChar = '\0';
            this.txtSpawnModel.PlaceholderForeColor = System.Drawing.Color.Gray;
            this.txtSpawnModel.PlaceholderText = "Ex: adder, sultan, etc.";
            this.txtSpawnModel.SelectedText = "";
            this.txtSpawnModel.Size = new System.Drawing.Size(300, 40);
            this.txtSpawnModel.TabIndex = 3;
            // 
            // lblSpawnModel
            // 
            this.lblSpawnModel.BackColor = System.Drawing.Color.Transparent;
            this.lblSpawnModel.Font = new System.Drawing.Font("MS Gothic", 10F);
            this.lblSpawnModel.ForeColor = System.Drawing.Color.White;
            this.lblSpawnModel.Location = new System.Drawing.Point(30, 40);
            this.lblSpawnModel.Name = "lblSpawnModel";
            this.lblSpawnModel.Size = new System.Drawing.Size(136, 15);
            this.lblSpawnModel.TabIndex = 2;
            this.lblSpawnModel.Text = "Nome do Modelo/Ped";
            // 
            // btnSpawnVeiculo
            // 
            this.btnSpawnVeiculo.BorderRadius = 8;
            this.btnSpawnVeiculo.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSpawnVeiculo.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSpawnVeiculo.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSpawnVeiculo.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSpawnVeiculo.FillColor = System.Drawing.Color.Purple;
            this.btnSpawnVeiculo.Font = new System.Drawing.Font("MS Gothic", 10F);
            this.btnSpawnVeiculo.ForeColor = System.Drawing.Color.White;
            this.btnSpawnVeiculo.Location = new System.Drawing.Point(30, 130);
            this.btnSpawnVeiculo.Name = "btnSpawnVeiculo";
            this.btnSpawnVeiculo.Size = new System.Drawing.Size(140, 45);
            this.btnSpawnVeiculo.TabIndex = 1;
            this.btnSpawnVeiculo.Text = "Spawn Veículo";
            // 
            // btnSpawnPed
            // 
            this.btnSpawnPed.BorderRadius = 8;
            this.btnSpawnPed.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSpawnPed.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSpawnPed.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSpawnPed.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSpawnPed.FillColor = System.Drawing.Color.DarkMagenta;
            this.btnSpawnPed.Font = new System.Drawing.Font("MS Gothic", 10F);
            this.btnSpawnPed.ForeColor = System.Drawing.Color.White;
            this.btnSpawnPed.Location = new System.Drawing.Point(190, 130);
            this.btnSpawnPed.Name = "btnSpawnPed";
            this.btnSpawnPed.Size = new System.Drawing.Size(140, 45);
            this.btnSpawnPed.TabIndex = 0;
            this.btnSpawnPed.Text = "Spawn Ped";
            // 
            // ShadowForm
            // 
            this.ShadowForm.BorderRadius = 15;
            this.ShadowForm.ShadowColor = System.Drawing.Color.Purple;
            this.ShadowForm.TargetForm = this;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(933, 450);
            this.Controls.Add(this.TabControl);
            this.Controls.Add(this.PanelTop);
            this.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MainForm";
            this.TopMost = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GTA V Mod Menu by 5pedrowx1";
            this.PanelTop.ResumeLayout(false);
            this.PanelTop.PerformLayout();
            this.TabControl.ResumeLayout(false);
            this.Chaos.ResumeLayout(false);
            this.Chaos.PerformLayout();
            this.Player.ResumeLayout(false);
            this.Player.PerformLayout();
            this.Veiculos.ResumeLayout(false);
            this.Veiculos.PerformLayout();
            this.Armas.ResumeLayout(false);
            this.Armas.PerformLayout();
            this.Mundo.ResumeLayout(false);
            this.Mundo.PerformLayout();
            this.Diversao.ResumeLayout(false);
            this.Teleporte.ResumeLayout(false);
            this.Teleporte.PerformLayout();
            this.Spawner.ResumeLayout(false);
            this.Spawner.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2DragControl DragControl;
        private Guna.UI2.WinForms.Guna2Panel PanelTop;
        private Guna.UI2.WinForms.Guna2Button btnMinimize;
        private Guna.UI2.WinForms.Guna2Button btnClose;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2Elipse Elipse;
        private Guna.UI2.WinForms.Guna2TabControl TabControl;
        private System.Windows.Forms.TabPage Chaos;
        private Guna.UI2.WinForms.Guna2Button btnExplosaoVeiculos;
        private Guna.UI2.WinForms.Guna2Button btnChaosTotal;
        private Guna.UI2.WinForms.Guna2ToggleSwitch togglePedsCrazy;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblPedsCrazy;
        private System.Windows.Forms.TabPage Player;
        private Guna.UI2.WinForms.Guna2Button btnCurarPlayer;
        private Guna.UI2.WinForms.Guna2TrackBar trackBarVida;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblVida;
        private Guna.UI2.WinForms.Guna2ToggleSwitch toggleGodMode;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblGodMode;
        private Guna.UI2.WinForms.Guna2ToggleSwitch toggleInfiniteStamina;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblStamina;
        private System.Windows.Forms.TabPage Veiculos;
        private Guna.UI2.WinForms.Guna2TrackBar trackBarVelocidade;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblVelocidade;
        private Guna.UI2.WinForms.Guna2Button btnRepararVeiculo;
        private Guna.UI2.WinForms.Guna2ToggleSwitch toggleVeiculoIndestrutivel;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblVeiculoIndestrutivel;
        private System.Windows.Forms.TabPage Armas;
        private Guna.UI2.WinForms.Guna2Button btnTodasArmas;
        private Guna.UI2.WinForms.Guna2ToggleSwitch toggleMunicaoInfinita;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblMunicaoInfinita;
        private Guna.UI2.WinForms.Guna2ToggleSwitch toggleSemRecarga;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSemRecarga;
        private System.Windows.Forms.TabPage Mundo;
        private Guna.UI2.WinForms.Guna2ComboBox comboBoxClima;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblClima;
        private Guna.UI2.WinForms.Guna2TrackBar trackBarTempo;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTempo;
        private Guna.UI2.WinForms.Guna2ToggleSwitch toggleCongelarTempo;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblCongelarTempo;
        private System.Windows.Forms.TabPage Diversao;
        private Guna.UI2.WinForms.Guna2Button btnGravidadeLua;
        private Guna.UI2.WinForms.Guna2Button btnCameraLenta;
        private Guna.UI2.WinForms.Guna2Button btnModoRagdoll;
        private System.Windows.Forms.TabPage Teleporte;
        private Guna.UI2.WinForms.Guna2Button btnTpWaypoint;
        private Guna.UI2.WinForms.Guna2Button btnTpVeiculo;
        private System.Windows.Forms.ListBox listBoxLocais;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblLocais;
        private System.Windows.Forms.TabPage Spawner;
        private Guna.UI2.WinForms.Guna2TextBox txtSpawnModel;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSpawnModel;
        private Guna.UI2.WinForms.Guna2Button btnSpawnVeiculo;
        private Guna.UI2.WinForms.Guna2Button btnSpawnPed;
        private Guna.UI2.WinForms.Guna2ShadowForm ShadowForm;
    }
}