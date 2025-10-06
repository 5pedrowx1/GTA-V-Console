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

        [Obsolete]
        public GTAVConsole()
        {
            Thread consoleThread = new Thread(() =>
            {
                consoleForm = new MainForm();
                Application.Run(consoleForm);
            });

            consoleThread.SetApartmentState(ApartmentState.STA);
            consoleThread.IsBackground = true;
            consoleThread.Start();

            KeyDown += OnKeyDown;
            Notification.Show("~y~GTAV Mod Menu by 5pedrowx1~w~ carregado! Pressiona ~b~Insert~w~ para abrir.");
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