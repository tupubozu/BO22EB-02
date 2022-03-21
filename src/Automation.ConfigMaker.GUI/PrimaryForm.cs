using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Automation.Configuration;
using Automation.Core;

namespace Automation.ConfigMaker.GUI
{
    public partial class PrimaryForm : Form
    {
        public static readonly string DefaultFileName = "run.prgconf";
        public static readonly string DefaultFileExtension = ".prgconf";
        public static readonly string DefaultFileFilter = $"Configuration |{DefaultFileExtension}";
        public static readonly ProgramConfiguration.Target.Job.JobCategory[] targetJobCategories = new ProgramConfiguration.Target.Job.JobCategory[]
        {
            ProgramConfiguration.Target.Job.JobCategory.McAfee,
            ProgramConfiguration.Target.Job.JobCategory.Acronis,
            ProgramConfiguration.Target.Job.JobCategory.vCenter,
            ProgramConfiguration.Target.Job.JobCategory.Custom
        };

        private ProgramConfiguration configuration;
        private Dictionary<ushort, byte[]> ScriptDictionary;
        private Dictionary<ushort, byte[]> KeyDictionary;

        public PrimaryForm()
        {
            InitializeComponent();

            configuration = new ProgramConfiguration()
            {
                Metadata = new ProgramConfiguration.ConfigurationMetadata { Author = new ProgramConfiguration.ConfigurationMetadata.MetadataAuthor() },
                Options = new ProgramConfiguration.Output[]
                {
                    new ProgramConfiguration.Output
                    {
                        Category = ProgramConfiguration.Output.OutputCategory.File,
                        Address = "report.xlsx",
                        Password = false
                    }
                }
            };

            ScriptDictionary = new Dictionary<ushort, byte[]>();
            KeyDictionary = new Dictionary<ushort, byte[]>();

            string initDir = Environment.GetEnvironmentVariables()["USERPROFILE"].ToString();

            saveFileDialog.InitialDirectory = initDir;
            saveFileDialog.FileName = DefaultFileName;
            saveFileDialog.DefaultExt = DefaultFileExtension;
            saveFileDialog.Filter = DefaultFileFilter;

            openFileDialog.InitialDirectory = initDir;
            openFileDialog.FileName = DefaultFileName;
            openFileDialog.DefaultExt = DefaultFileExtension;
            openFileDialog.Filter = DefaultFileFilter;

            jobCategoryComboBox.Items.AddRange(targetJobCategories.Cast<object>().ToArray());
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ScriptDictionary.Clear();
                    KeyDictionary.Clear();

                    var authForm = new AuthenticationForm();
                    if (authForm.ShowDialog() != DialogResult.OK) return;

                    await ConfigCore.OpenConfigAsync(openFileDialog.OpenFile(), authForm.Password, configuration, ScriptDictionary, KeyDictionary);

                    ProgramConfiguration.Script.NextID = (ushort)((configuration?.Scripts?.Select((item) => item.ID)?.Max() + 1) ?? 0);
                    ProgramConfiguration.Key.NextID = (ushort)((configuration?.Keys?.Select((item) => item.ID)?.Max() + 1) ?? 0);

                    titleTextBox.Text = configuration.Metadata.Title;
                    revisionTextBox.Text = configuration.Metadata.Revision.ToString("O");
                    descriptionTextBox.Text = configuration.Metadata.Description;
                    authorNameTextBox.Text = configuration.Metadata.Author.Name;
                    authorEmailTextBox.Text = configuration.Metadata.Author.Email;

                    hostListBox.Items.Clear();
                    if (!(configuration.Work is null))
                        hostListBox.Items.AddRange(configuration.Work);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), ex.GetType().FullName);
                }
            }
        }

        private async void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var authForm = new AuthenticationForm();
                    if (authForm.ShowDialog() != DialogResult.OK) return;

                    toolStripProgressBar.Value = 0;
                    toolStripStatusLabel.Text = "Writing configuration to file";

                    await ConfigCore.SaveConfigAsync(saveFileDialog.OpenFile(), authForm.Password, configuration, ScriptDictionary, KeyDictionary);

                    toolStripProgressBar.Value = 100;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), ex.GetType().FullName);
                    toolStripProgressBar.Value = 0;
                    toolStripStatusLabel.Text = $"Error: {ex.Message}";
                }
            }
        }

        private void titleTextBox_Leave(object sender, EventArgs e)
        {
            configuration.Metadata.Title = titleTextBox.Text;
        }

        private void descriptionTextBox_Leave(object sender, EventArgs e)
        {
            configuration.Metadata.Description = descriptionTextBox.Text;
        }

        private void authorNameTextBox_Leave(object sender, EventArgs e)
        {
            configuration.Metadata.Author.Name = authorNameTextBox.Text;
        }

        private void authorEmailTextBox_Leave(object sender, EventArgs e)
        {
            configuration.Metadata.Author.Email = authorEmailTextBox.Text;
        }

        private void portOverrideCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            portNumeric.Enabled = portOverrideCheckBox.Checked;
        }

        private void hostAddButton_Click(object sender, EventArgs e)
        {
            hostListBox.Items.Add(new ProgramConfiguration.Target { Host = hostTextBox.Text });
            configuration.Work = hostListBox.Items.Cast<ProgramConfiguration.Target>().ToArray();
        }

        private void hostRemoveButton_Click(object sender, EventArgs e)
        {
            if (hostListBox.SelectedIndex >= 0 && hostListBox.SelectedIndex < hostListBox.Items.Count)
                hostListBox.Items.RemoveAt(hostListBox.SelectedIndex);

            configuration.Work = hostListBox.Items.Cast<ProgramConfiguration.Target>().ToArray();
        }

        private void hostModifyButton_Click(object sender, EventArgs e)
        {
            if (hostListBox.SelectedIndex >= 0 && hostListBox.SelectedIndex < hostListBox.Items.Count)
                (hostListBox.Items[hostListBox.SelectedIndex] as ProgramConfiguration.Target).Host = hostTextBox.Text;
        }

        private void hostListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (hostListBox.SelectedIndex >= 0 && hostListBox.SelectedIndex < hostListBox.Items.Count)
            {
                jobListBox.Items.Clear();
                if (((hostListBox.Items[hostListBox.SelectedIndex] as ProgramConfiguration.Target)?.Jobs?.Length ?? -1) >= 0)
                    jobListBox.Items.AddRange((hostListBox.Items[hostListBox.SelectedIndex] as ProgramConfiguration.Target).Jobs);
            }
        }

        private void jobAddButton_Click(object sender, EventArgs e)
        {
            jobListBox.Items.Add(new ProgramConfiguration.Target.Job
            {
                Port = (ushort)portNumeric.Value,
                Category = ProgramConfiguration.Target.Job.JobCategory.Custom
            });
            UpdateJobs();
        }

        private void jobRemoveButton_Click(object sender, EventArgs e)
        {
            if (jobListBox.SelectedIndex >= 0 && jobListBox.SelectedIndex < jobListBox.Items.Count)
                jobListBox.Items.RemoveAt(jobListBox.SelectedIndex);

            UpdateJobs();
        }

        private void UpdateJobs()
        {
            if (hostListBox.SelectedIndex >= 0 && hostListBox.SelectedIndex < hostListBox.Items.Count)
                (hostListBox.Items[hostListBox.SelectedIndex] as ProgramConfiguration.Target).Jobs = jobListBox.Items.Cast<ProgramConfiguration.Target.Job>().ToArray();
        }

        private void jobCategoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (jobListBox.SelectedIndex >= 0 && jobListBox.SelectedIndex < jobListBox.Items.Count)
                if (!(jobCategoryComboBox.SelectedItem is null))
                {
                    (jobListBox.Items[jobListBox.SelectedIndex] as ProgramConfiguration.Target.Job).Category = (ProgramConfiguration.Target.Job.JobCategory)jobCategoryComboBox.SelectedItem;
                    if ((ProgramConfiguration.Target.Job.JobCategory)jobCategoryComboBox.SelectedItem == ProgramConfiguration.Target.Job.JobCategory.Custom)
                        portNumeric.Value = 22;
                    else
                        portNumeric.Value = 443;
                }
        }

        private void portNumeric_ValueChanged(object sender, EventArgs e)
        {
            if (jobListBox.SelectedIndex >= 0 && jobListBox.SelectedIndex < jobListBox.Items.Count)
                (jobListBox.Items[jobListBox.SelectedIndex] as ProgramConfiguration.Target.Job).Port = (ushort)portNumeric.Value;
        }

        private void jobNameTextBox_Leave(object sender, EventArgs e)
        {
            if (jobListBox.SelectedIndex >= 0 && jobListBox.SelectedIndex < jobListBox.Items.Count)
                (jobListBox.Items[jobListBox.SelectedIndex] as ProgramConfiguration.Target.Job).Name = jobNameTextBox.Text;
        }

        private void jobListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            scriptListBox.Items.Clear();
            keyListBox.Items.Clear();

            if (jobListBox.SelectedIndex >= 0 && jobListBox.SelectedIndex < jobListBox.Items.Count)
            {
                var job = jobListBox.Items[jobListBox.SelectedIndex] as ProgramConfiguration.Target.Job;
                portNumeric.Value = job.Port;
                jobNameTextBox.Text = job.Name;
                jobCategoryComboBox.SelectedIndex = jobCategoryComboBox.Items.IndexOf(job.Category);

                if (!(job.Scripts is null))
                    foreach (ushort ID in job.Scripts)
                        scriptListBox.Items.Add(configuration.Scripts.Where(item => item.ID == ID).First());

                if (!(job.Keys is null))
                    foreach (ushort ID in job.Keys)
                        keyListBox.Items.Add(configuration.Keys.Where(item => item.ID == ID).First());
            }
        }

        private void keyRemoveButton_Click(object sender, EventArgs e)
        {
            if (keyListBox.SelectedIndex >= 0 && keyListBox.SelectedIndex < keyListBox.Items.Count)
                keyListBox.Items.RemoveAt(keyListBox.SelectedIndex);

            UpdateKeys();
        }

        private void keyModifyButton_Click(object sender, EventArgs e)
        {
            if (keyListBox.SelectedIndex >= 0 && keyListBox.SelectedIndex < keyListBox.Items.Count)
                keyModify((ProgramConfiguration.Key)keyListBox.Items[keyListBox.SelectedIndex]);
        }

        private bool keyModify(ProgramConfiguration.Key key)
        {
            var keyPropertiesCtrl = new KeyPropertiesForm(key);
            if (keyPropertiesCtrl.ShowDialog() == DialogResult.OK)
            {
                if (KeyDictionary.ContainsKey(key.ID))
                    KeyDictionary[key.ID] = keyPropertiesCtrl.FileContents;
                else
                    KeyDictionary.Add(key.ID, keyPropertiesCtrl.FileContents);
                return true;
            }
            else
            {
                MessageBox.Show("Operation has been aborted");
                return false;
            }
        }

        private void keyAddButton_Click(object sender, EventArgs e)
        {
            var key = new ProgramConfiguration.Key();
            if (keyModify(key))
            {
                keyListBox.Items.Add(key);
                UpdateKeys();
            }
        }

        private void scriptRemoveButton_Click(object sender, EventArgs e)
        {
            if (scriptListBox.SelectedIndex >= 0 && scriptListBox.SelectedIndex < scriptListBox.Items.Count)
                scriptListBox.Items.RemoveAt(scriptListBox.SelectedIndex);

            UpdateScripts();
        }

        private void scriptModifyButton_Click(object sender, EventArgs e)
        {
            if (scriptListBox.SelectedIndex >= 0 && scriptListBox.SelectedIndex < scriptListBox.Items.Count)
                scriptModify((ProgramConfiguration.Script)scriptListBox.Items[scriptListBox.SelectedIndex]);
        }

        private bool scriptModify(ProgramConfiguration.Script script)
        {
            var scriptPropertiesCtrl = new ScriptPropertiesForm(script);
            if (scriptPropertiesCtrl.ShowDialog() == DialogResult.OK)
            {
                if (ScriptDictionary.ContainsKey(script.ID))
                    ScriptDictionary[script.ID] = scriptPropertiesCtrl.FileContents;
                else
                    ScriptDictionary.Add(script.ID, scriptPropertiesCtrl.FileContents);
                return true;
            }
            else
            {
                MessageBox.Show("Operation has been aborted");
                return false;
            }
        }

        private void scriptAddButton_Click(object sender, EventArgs e)
        {
            var script = new ProgramConfiguration.Script();
            if (scriptModify(script))
            {
                scriptListBox.Items.Add(script);
                UpdateScripts();
            }
        }

        private void UpdateScripts()
        {
            if (jobListBox.SelectedIndex >= 0 && jobListBox.SelectedIndex < jobListBox.Items.Count)
            {
                (jobListBox.Items[jobListBox.SelectedIndex] as ProgramConfiguration.Target.Job).Scripts = scriptListBox.Items.Cast<ProgramConfiguration.Script>().Select((item) => item.ID).ToArray();

                configuration.Scripts = scriptListBox.Items.Cast<ProgramConfiguration.Script>().ToArray();
            }
        }

        private void UpdateKeys()
        {
            if (jobListBox.SelectedIndex >= 0 && jobListBox.SelectedIndex < jobListBox.Items.Count)
            {
                (jobListBox.Items[jobListBox.SelectedIndex] as ProgramConfiguration.Target.Job).Keys = keyListBox.Items.Cast<ProgramConfiguration.Key>().Select((item) => item.ID).ToArray();

                configuration.Keys = keyListBox.Items.Cast<ProgramConfiguration.Key>().ToArray();
            }
        }
    }
}
