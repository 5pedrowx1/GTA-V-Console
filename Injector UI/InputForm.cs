using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Injector_UI
{
    public partial class InputForm : Form
    {
        public InputForm(string title, string prompt)
        {
            InitializeComponent(title, prompt);
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtInput.Text))
            {
                MessageBox.Show("O campo não pode estar vazio!", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtInput.Focus();
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
