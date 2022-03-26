using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ReGen.Configuration;

namespace ReGen.ConfigMaker.GUI
{
    public partial class KeyPropertiesForm : Form
    {
        private ProgramConfiguration.Key _key;
        public byte[] FileContents { get; set; }

        public KeyPropertiesForm(ProgramConfiguration.Key key)
        {
            InitializeComponent();

            _key = key;
            updateControls(_key);
        }

        private void updateControls(ProgramConfiguration.Key key)
        {
            keyNameTextBox.Text = key?.Name;
            keySourceTextBox.Text = key?.Source;
            keyUserTextBox.Text = key?.User;

            switch (key.Category)
            {
                case ProgramConfiguration.Key.KeyCategory.API:
                    keyCategoryApiRadioButton.Checked = true;
                    break;
                case ProgramConfiguration.Key.KeyCategory.SSH:
                    keyCategorySshRadioButton.Checked = true;
                    break;
                default:
                    break;
            }

            fileBrowseButton.Enabled = key?.Source?.Contains("#") ?? true;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private async void fileBrowseButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                keySourceTextBox.Text = openFileDialog.FileName;
                using (var fileStream = openFileDialog.OpenFile())
                using (var reader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    FileContents = Encoding.UTF8.GetBytes(await reader.ReadToEndAsync());
                }
            }
        }

        private void keyCategorySshRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (keyCategorySshRadioButton.Checked)
            {
                _key.Category = ProgramConfiguration.Key.KeyCategory.SSH;
                keyValueTextBox.Enabled = false;
                fileBrowseButton.Enabled = true;
            }
        }

        private void keyCategoryApiRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (keyCategoryApiRadioButton.Checked)
            {
                _key.Category = ProgramConfiguration.Key.KeyCategory.API;
                keyValueTextBox.Enabled = true;
                fileBrowseButton.Enabled = false;
            }
        }

        private void keyValueTextBox_Leave(object sender, EventArgs e)
        {
            _key.Value = keyValueTextBox.Text;
        }

        private void keyUserTextBox_Leave(object sender, EventArgs e)
        {
            _key.User = keyUserTextBox.Text;
        }

        private void keyNameTextBox_Leave(object sender, EventArgs e)
        {
            _key.Name = keyNameTextBox.Text;
        }
    }
}
