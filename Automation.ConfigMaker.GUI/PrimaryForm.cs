using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Xml.Serialization;
using System.IO.Compression;
using System.IO;
using Automation.Configuration;
using Automation.Cryptography;

namespace Automation.ConfigMaker.GUI
{
    public partial class PrimaryForm : Form
    {
        public static readonly string DefaultFileName = "run.prgconf";
        public static readonly string DefaultFileExtension = ".prgconf";
        public static readonly string DefaultFileFilter = $"Configuration |{DefaultFileExtension}";
        public static readonly ProgramConfigurationTargetJobCategory[] targetJobCategories = new ProgramConfigurationTargetJobCategory[]
        {
            ProgramConfigurationTargetJobCategory.McAfee,
            ProgramConfigurationTargetJobCategory.Acronis,
            ProgramConfigurationTargetJobCategory.vCenter,
            ProgramConfigurationTargetJobCategory.Custom
        };
    

    private ProgramConfiguration configuration;

        public PrimaryForm()
        {
            InitializeComponent();

            configuration = new ProgramConfiguration()
            {
                Metadata = new ProgramConfigurationMetadata { Author = new ProgramConfigurationMetadataAuthor() },
                Options = new ProgramConfigurationOutput[]
                {
                    new ProgramConfigurationOutput
                    {
                        Category = ProgramConfigurationOutputCategory.File,
                        Address = "report.xlsx",
                        Password = false
                    }
                },
                Scripts = new ProgramConfigurationScript[0],
                Work = new ProgramConfigurationTarget[0],
            };

            saveFileDialog.InitialDirectory = Environment.GetEnvironmentVariables()["USERPROFILE"].ToString();
            saveFileDialog.FileName = DefaultFileName;
            saveFileDialog.DefaultExt = DefaultFileExtension;
            saveFileDialog.Filter = DefaultFileFilter;

            openFileDialog.InitialDirectory = Environment.GetEnvironmentVariables()["USERPROFILE"].ToString();
            openFileDialog.FileName = DefaultFileName;
            openFileDialog.DefaultExt = DefaultFileExtension;
            openFileDialog.Filter = DefaultFileFilter;

            foreach (ProgramConfigurationTargetJobCategory targetJobCategory in targetJobCategories)
                jobCategoryComboBox.Items.Add(targetJobCategory);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (var configStream = openFileDialog.OpenFile())
                using (var zipConfig = new ZipArchive(configStream, ZipArchiveMode.Read, true, Encoding.UTF8))
                using (var aesCrypto = Crypto.GetAes(Encoding.UTF8.GetBytes("I'm an engineer!")))
                using (var passwordHash = SHA256.Create())
                {
                    using (var passwordForm = new AuthenticationForm())
                    {
                        if (passwordForm.ShowDialog() == DialogResult.OK)
                        {
                            aesCrypto.Key = passwordHash.ComputeHash(new MemoryStream(Encoding.UTF8.GetBytes(passwordForm.Password)));
                        }
                        else
                        {
                            MessageBox.Show("Operation has been aborted");
                            return;
                        }
                    }

                    var encryptedConfig = zipConfig.GetEntry("config.xml.aes");

                    using (CryptoStream cryptoConfigStream = new CryptoStream(encryptedConfig.Open(), aesCrypto.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        configuration = ProgramConfiguration.Parse(cryptoConfigStream);
                    }
                }
            }
        }

        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (var configStream = saveFileDialog.OpenFile())
                using (var zipConfig = new ZipArchive(configStream, ZipArchiveMode.Create, true, Encoding.UTF8))
                using (var aesCrypto = Crypto.GetAes(Encoding.UTF8.GetBytes("I'm an engineer!")))
                using (var passwordHash = SHA256.Create())
                {
                    using (var passwordForm = new AuthenticationForm())
                    {
                        if (passwordForm.ShowDialog() == DialogResult.OK)
                        {
                            aesCrypto.Key = passwordHash.ComputeHash(new MemoryStream(Encoding.UTF8.GetBytes(passwordForm.Password)));
                        }
                        else
                        {
                            MessageBox.Show("Operation has been aborted");
                            return;
                        }
                    }

                    var encryptedConfig = zipConfig.CreateEntry("config.xml.aes", CompressionLevel.Optimal);

                    using (CryptoStream cryptoConfigStream = new CryptoStream(encryptedConfig.Open(), aesCrypto.CreateEncryptor(), CryptoStreamMode.Write))
                    using (var writer = new StreamWriter(cryptoConfigStream, Encoding.UTF8))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(ProgramConfiguration));
                        configuration.Metadata.Revision = DateTime.Now;
                        revisionTextBox.Text = configuration.Metadata.Revision.ToString("O");
                        serializer.Serialize(writer, configuration);
                    }

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
            hostListBox.Items.Add(new ProgramConfigurationTarget { Host = hostTextBox.Text});
        }

        private void hostRemoveButton_Click(object sender, EventArgs e)
        {
            if (hostListBox.SelectedIndex >= 0 && hostListBox.SelectedIndex < hostListBox.Items.Count)
                hostListBox.Items.RemoveAt(hostListBox.SelectedIndex);
        }

        private void hostModifyButton_Click(object sender, EventArgs e)
        {
            if (hostListBox.SelectedIndex >= 0 && hostListBox.SelectedIndex < hostListBox.Items.Count)
                (hostListBox.Items[hostListBox.SelectedIndex] as ProgramConfigurationTarget).Host = hostTextBox.Text;
        }

        private void hostListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (hostListBox.SelectedIndex >= 0 && hostListBox.SelectedIndex < hostListBox.Items.Count)
            {
                jobListBox.Items.Clear();
                if (((hostListBox.Items[hostListBox.SelectedIndex] as ProgramConfigurationTarget)?.Job?.Length ?? -1)  >= 0)
                    jobListBox.Items.AddRange((hostListBox.Items[hostListBox.SelectedIndex] as ProgramConfigurationTarget).Job);
            }
        }

        private void jobAddButton_Click(object sender, EventArgs e)
        {
            jobListBox.Items.Add(new ProgramConfigurationTargetJob
            {
                Port = (ushort)portNumeric.Value,
                Category = ProgramConfigurationTargetJobCategory.Custom
            });
            if (hostListBox.SelectedIndex >= 0 && hostListBox.SelectedIndex < hostListBox.Items.Count)
                (hostListBox.Items[hostListBox.SelectedIndex] as ProgramConfigurationTarget).Job = jobListBox.Items.Cast<ProgramConfigurationTargetJob>().ToArray(); 

        }

        private void jobRemoveButton_Click(object sender, EventArgs e)
        {
            if (jobListBox.SelectedIndex >= 0 && jobListBox.SelectedIndex < jobListBox.Items.Count)
                jobListBox.Items.RemoveAt(jobListBox.SelectedIndex);

            if (hostListBox.SelectedIndex >= 0 && hostListBox.SelectedIndex < hostListBox.Items.Count)
                (hostListBox.Items[hostListBox.SelectedIndex] as ProgramConfigurationTarget).Job = jobListBox.Items.Cast<ProgramConfigurationTargetJob>().ToArray();
        }

        private void jobCategoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (jobListBox.SelectedIndex >= 0 && jobListBox.SelectedIndex < jobListBox.Items.Count)
                if (!(jobCategoryComboBox.SelectedItem is null))
                {
                    (jobListBox.Items[jobListBox.SelectedIndex] as ProgramConfigurationTargetJob).Category = (ProgramConfigurationTargetJobCategory)jobCategoryComboBox.SelectedItem;
                    if ((ProgramConfigurationTargetJobCategory)jobCategoryComboBox.SelectedItem == ProgramConfigurationTargetJobCategory.Custom)
                        portNumeric.Value = 22;
                    else 
                        portNumeric.Value = 443;
                }
        }

        private void portNumeric_ValueChanged(object sender, EventArgs e)
        {
            if (jobListBox.SelectedIndex >= 0 && jobListBox.SelectedIndex < jobListBox.Items.Count)
                (jobListBox.Items[jobListBox.SelectedIndex] as ProgramConfigurationTargetJob).Port = (ushort)portNumeric.Value;
        }

        private void jobNameTextBox_Leave(object sender, EventArgs e)
        {
            if (jobListBox.SelectedIndex >= 0 && jobListBox.SelectedIndex < jobListBox.Items.Count)
                (jobListBox.Items[jobListBox.SelectedIndex] as ProgramConfigurationTargetJob).Name = jobNameTextBox.Text;
        }
    }
}
