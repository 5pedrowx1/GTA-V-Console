namespace Injector_UI
{
    public partial class InputForm : Form
    {
        public string InputValue => txtInput.Text;

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
