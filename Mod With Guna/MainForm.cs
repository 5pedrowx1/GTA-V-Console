using System;
using System.Windows.Forms;

namespace Mod_With_Guna
{
    public partial class MainForm : Form
    {
        // Instâncias das classes de lógica (recebidas do GTAVConsole)
        private readonly PlayerLogic playerLogic;
        private readonly VehicleLogic vehicleLogic;
        private readonly WeaponLogic weaponLogic;
        private readonly WorldLogic worldLogic;
        private readonly ChaosLogic chaosLogic;
        private readonly FunLogic funLogic;
        private readonly TeleportLogic teleportLogic;
        private readonly SpawnerLogic spawnerLogic;
        private readonly MoneyLogic moneyLogic;

        // Construtor modificado para receber as instâncias
        public MainForm(
            PlayerLogic playerLogic,
            VehicleLogic vehicleLogic,
            WeaponLogic weaponLogic,
            WorldLogic worldLogic,
            ChaosLogic chaosLogic,
            FunLogic funLogic,
            TeleportLogic teleportLogic,
            SpawnerLogic spawnerLogic,
            MoneyLogic moneyLogic)
        {
            InitializeComponent();

            // Atribuir as instâncias recebidas
            this.playerLogic = playerLogic;
            this.vehicleLogic = vehicleLogic;
            this.weaponLogic = weaponLogic;
            this.worldLogic = worldLogic;
            this.chaosLogic = chaosLogic;
            this.funLogic = funLogic;
            this.teleportLogic = teleportLogic;
            this.spawnerLogic = spawnerLogic;
            this.moneyLogic = moneyLogic;

            // Registrar eventos
            RegisterEvents();
        }

        private void RegisterEvents()
        {
            // Eventos do PanelTop
            btnClose.Click += BtnClose_Click;
            btnMinimize.Click += BtnMinimize_Click;

            // Eventos da aba CHAOS
            togglePedsCrazy.CheckedChanged += (s, e) => chaosLogic.TogglePedsCrazy(togglePedsCrazy.Checked);
            btnExplosaoVeiculos.Click += (s, e) => chaosLogic.ExplodeNearbyVehicles();
            btnChaosTotal.Click += (s, e) => chaosLogic.ToggleTotalChaos();

            // Eventos da aba PLAYER
            toggleGodMode.CheckedChanged += (s, e) => playerLogic.ToggleGodMode(toggleGodMode.Checked);
            toggleInfiniteStamina.CheckedChanged += (s, e) => playerLogic.ToggleInfiniteStamina(toggleInfiniteStamina.Checked);
            trackBarVida.Scroll += (s, e) => {
                playerLogic.SetHealth(trackBarVida.Value);
                lblVida.Text = $"Vida: {trackBarVida.Value}/200";
            };
            btnCurarPlayer.Click += (s, e) => {
                playerLogic.HealPlayer();
                trackBarVida.Value = 200;
                lblVida.Text = "Vida: 200/200";
            };

            // Eventos da aba VEÍCULOS
            toggleVeiculoIndestrutivel.CheckedChanged += (s, e) => vehicleLogic.ToggleInvincibleVehicle(toggleVeiculoIndestrutivel.Checked);
            btnRepararVeiculo.Click += (s, e) => vehicleLogic.RepairVehicle();
            trackBarVelocidade.Scroll += (s, e) => {
                vehicleLogic.SetMaxSpeed(trackBarVelocidade.Value);
                lblVelocidade.Text = $"Velocidade Máxima: {trackBarVelocidade.Value}%";
            };

            // Eventos da aba ARMAS
            toggleMunicaoInfinita.CheckedChanged += (s, e) => weaponLogic.ToggleInfiniteAmmo(toggleMunicaoInfinita.Checked);
            toggleSemRecarga.CheckedChanged += (s, e) => weaponLogic.ToggleNoReload(toggleSemRecarga.Checked);
            btnTodasArmas.Click += (s, e) => weaponLogic.GiveAllWeapons();

            // Eventos da aba MUNDO
            toggleCongelarTempo.CheckedChanged += (s, e) => worldLogic.ToggleFreezeTime(toggleCongelarTempo.Checked);
            trackBarTempo.Scroll += (s, e) => {
                worldLogic.SetTime(trackBarTempo.Value);
                lblTempo.Text = $"Hora: {trackBarTempo.Value:D2}:00";
            };
            comboBoxClima.SelectedIndexChanged += (s, e) => worldLogic.SetWeather(comboBoxClima.SelectedIndex);

            // Eventos da aba DIVERSÃO
            btnGravidadeLua.Click += (s, e) => funLogic.ToggleMoonGravity();
            btnCameraLenta.Click += (s, e) => funLogic.ToggleSlowMotion();
            btnModoRagdoll.Click += (s, e) => funLogic.ActivateRagdoll();

            // Eventos da aba TELEPORTE
            listBoxLocais.DoubleClick += (s, e) => {
                if (listBoxLocais.SelectedIndex >= 0)
                {
                    teleportLogic.TeleportToLocation(listBoxLocais.SelectedIndex);
                }
            };
            btnTpWaypoint.Click += (s, e) => teleportLogic.TeleportToWaypoint();
            btnTpVeiculo.Click += (s, e) => teleportLogic.TeleportVehicleToPlayer();

            // Eventos da aba SPAWNER
            btnSpawnVeiculo.Click += (s, e) => spawnerLogic.SpawnVehicle(txtSpawnModel.Text);
            btnSpawnPed.Click += (s, e) => spawnerLogic.SpawnPed(txtSpawnModel.Text);

            // Eventos da aba DINHEIRO
            btnAddDinheiro.Click += (s, e) => {
                if (int.TryParse(txtValorDinheiro.Text, out int valor))
                {
                    moneyLogic.AddMoney(valor);
                    AtualizarDinheiro();
                }
            };

            btnSetDinheiro.Click += (s, e) => {
                if (int.TryParse(txtValorDinheiro.Text, out int valor))
                {
                    moneyLogic.SetMoney(valor);
                    AtualizarDinheiro();
                }
            };

            btnAdd10k.Click += (s, e) => {
                moneyLogic.AddMoney(10000);
                AtualizarDinheiro();
            };

            btnAdd100k.Click += (s, e) => {
                moneyLogic.AddMoney(100000);
                AtualizarDinheiro();
            };

            btnAdd1M.Click += (s, e) => {
                moneyLogic.AddMoney(1000000);
                AtualizarDinheiro();
            };
        }

        private void AtualizarDinheiro()
        {
            int dinheiro = moneyLogic.GetCurrentMoney();
            lblValorDinheiro.Text = $"${dinheiro:N0}";
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Visible = false; // Só esconde, não fecha o jogo
        }

        private void BtnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}