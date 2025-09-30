namespace Injector_UI
{
    public partial class InputForm : Form
    {
        public string InputValue => txtInput.Text;

        public InputForm(string title, string prompt)
        {
            InitializeComponent();

            lblTitle.Text = title;
            lblPrompt.Text = prompt;
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

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void InputForm_Shown(object sender, EventArgs e)
        {
            txtInput.Focus();
        }

        private void TxtInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnOk.PerformClick();
            }
            else if (e.KeyChar == (char)Keys.Escape)
            {
                e.Handled = true;
                btnCancel.PerformClick();
            }
        }
    }
}