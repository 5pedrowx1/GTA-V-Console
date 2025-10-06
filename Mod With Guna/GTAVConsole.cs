using GTA;
using GTA.UI;
using System;
using System.Threading;
using System.Windows.Forms;

namespace Mod_With_Guna
{
    public class GTAVConsole : Script
    {
        private MainForm consoleForm;
        private PlayerLogic playerLogic;
        private VehicleLogic vehicleLogic;
        private WeaponLogic weaponLogic;
        private WorldLogic worldLogic;
        private ChaosLogic chaosLogic;
        private FunLogic funLogic;
        private MoneyLogic moneyLogic;
        private TeleportLogic teleportLogic;
        private SpawnerLogic spawnerLogic;

        [Obsolete]
        public GTAVConsole()
        {
            // Criar as instâncias primeiro
            playerLogic = new PlayerLogic();
            vehicleLogic = new VehicleLogic();
            weaponLogic = new WeaponLogic();
            worldLogic = new WorldLogic();
            chaosLogic = new ChaosLogic();
            funLogic = new FunLogic();
            moneyLogic = new MoneyLogic();
            teleportLogic = new TeleportLogic();
            spawnerLogic = new SpawnerLogic();

            // Criar o formulário passando as instâncias
            Thread consoleThread = new Thread(() =>
            {
                consoleForm = new MainForm(
                    playerLogic,
                    vehicleLogic,
                    weaponLogic,
                    worldLogic,
                    chaosLogic,
                    funLogic,
                    teleportLogic,
                    spawnerLogic,
                    moneyLogic
                );
                Application.Run(consoleForm);
            });
            consoleThread.SetApartmentState(ApartmentState.STA);
            consoleThread.IsBackground = true;
            consoleThread.Start();

            KeyDown += OnKeyDown;
            Tick += OnTick;
            Notification.Show("~y~GTAV Mod Menu by 5pedrowx1~w~ carregado! Pressiona ~b~Insert~w~ para abrir.");
        }

        private void OnTick(object sender, EventArgs e)
        {
            playerLogic?.Update();
            vehicleLogic?.Update();
            weaponLogic?.Update();
            worldLogic?.Update();
            chaosLogic?.Update();
            funLogic?.Update();
            teleportLogic?.Update();
            spawnerLogic?.Update();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert && consoleForm != null && consoleForm.IsHandleCreated)
            {
                consoleForm.Invoke(new Action(() =>
                {
                    consoleForm.Visible = !consoleForm.Visible;
                }));
            }
        }
    }
}