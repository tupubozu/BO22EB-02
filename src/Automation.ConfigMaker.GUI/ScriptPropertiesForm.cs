using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Automation.Configuration;

namespace Automation.ConfigMaker.GUI
{
    public partial class ScriptPropertiesForm : Form
    {
        private ProgramConfiguration.Script _script;
        public byte[] FileContents { get; set; }

        public ScriptPropertiesForm(ProgramConfiguration.Script script)
        {
            InitializeComponent();

            _script = script;
            updateControls(_script);
        }

        private void updateControls(ProgramConfiguration.Script script)
        {
            scriptNameTextBox.Text = script?.Name;
            scriptSourceTextBox.Text = script?.Source;
            
            fileBrowseButton.Enabled = script?.Source?.Contains("#") ?? true;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult= DialogResult.Cancel;
            this.Close();
        }

        private async void fileBrowseButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                scriptSourceTextBox.Text = openFileDialog.FileName;
                using (var fileStream = openFileDialog.OpenFile())
                using (var reader = new StreamReader(fileStream,Encoding.UTF8, true))
                {
                    FileContents = Encoding.UTF8.GetBytes(await reader.ReadToEndAsync());
                }
            }
        }

        private void scriptNameTextBox_Leave(object sender, EventArgs e)
        {
            _script.Name = scriptNameTextBox.Text;
        }
    }
}
