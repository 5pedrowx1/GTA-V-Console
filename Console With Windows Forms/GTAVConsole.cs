using GTA;
using GTA.UI;
using System;
using System.Threading;
using System.Windows.Forms;

namespace Console_With_Windows_Forms
{
    public class GTAVConsole : Script
    {
        private ConsoleForm consoleForm;

        [Obsolete]
        public GTAVConsole()
        {
            Thread consoleThread = new Thread(() =>
            {
                consoleForm = new ConsoleForm();
                Application.Run(consoleForm);
            });

            consoleThread.SetApartmentState(ApartmentState.STA);
            consoleThread.IsBackground = true;
            consoleThread.Start();

            KeyDown += OnKeyDown;
            Notification.Show("~y~GTAV Console~w~ carregado! Pressiona ~b~F10~w~ para abrir.");
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10 && consoleForm != null && consoleForm.IsHandleCreated)
            {
                consoleForm.Invoke(new Action(() =>
                {
                    consoleForm.Visible = !consoleForm.Visible;
                }));
            }
        }
    }
}