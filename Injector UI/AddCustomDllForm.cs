using Injector_UI.Injector_UI;
using System.Xml.Linq;

namespace Injector_UI
{
    public partial class AddCustomDllForm : Form
    {
        public CustomDllConfig DllConfig { get; private set; }
        public AddCustomDllForm(CustomDllConfig? existing = null)
        {
            InitializeComponent();

            if (existing != null)
            {
                this.Text = "Editar DLL Customizada";
                lblTitle.Text = "✏ Editar DLL";
                txtName.Text = existing.Name;
                txtPath.Text = existing.Path;
                txtDescription.Text = existing.Description ?? "";
                chkEnabled.Checked = existing.Enabled;
                DllConfig = existing;
            }
            else
            {
                this.Text = "Adicionar DLL Customizada";
                lblTitle.Text = "➕ Adicionar DLL";
                DllConfig = new CustomDllConfig();
            }
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "DLL Files (*.dll;*.asi)|*.dll;*.asi|All Files (*.*)|*.*";
                openFileDialog.Title = "Selecione uma DLL";
                openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtPath.Text = openFileDialog.FileName;

                    if (string.IsNullOrWhiteSpace(txtName.Text))
                    {
                        txtName.Text = Path.GetFileNameWithoutExtension(openFileDialog.FileName);
                    }
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("O nome é obrigatório!", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPath.Text))
            {
                MessageBox.Show("O caminho é obrigatório!", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPath.Focus();
                return;
            }

            if (!File.Exists(txtPath.Text))
            {
                var result = MessageBox.Show(
                    "O arquivo não existe. Deseja continuar mesmo assim?",
                    "Aviso",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                    return;
            }

            DllConfig.Name = txtName.Text.Trim();
            DllConfig.Path = txtPath.Text.Trim();
            DllConfig.Description = txtDescription.Text.Trim();
            DllConfig.Enabled = chkEnabled.Checked;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; 
            this.Close(); 
        }
    }
}
