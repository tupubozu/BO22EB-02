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
                    using (var zipConfig = new ZipArchive(openFileDialog.OpenFile(), ZipArchiveMode.Read, false, Encoding.UTF8))
                    {
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

                            using (CryptoStream cryptoStream = new CryptoStream(encryptedConfig.Open(), aesCrypto.CreateDecryptor(), CryptoStreamMode.Read))
                            using (StreamReader reader = new StreamReader(cryptoStream, Encoding.UTF8))
                            {
                                configuration = ProgramConfiguration.Parse(new MemoryStream(Encoding.UTF8.GetBytes(await reader.ReadToEndAsync())));
                            }
                        }

                        foreach (var key in configuration.Keys)
                        {
                            switch (key.Category)
                            {
                                case ProgramConfiguration.Key.KeyCategory.API:
                                    KeyDictionary.Add(key.ID, Encoding.UTF8.GetBytes(key.Value));
                                    break;
                                case ProgramConfiguration.Key.KeyCategory.SSH:
                                    using (var aesCrypto = Crypto.GetAes())
                                    {
                                        aesCrypto.IV = key.EncryptionIV;
                                        aesCrypto.Key = key.EncryptionKey;

                                        var encryptedConfig = zipConfig.GetEntry(key.Source);

                                        using (CryptoStream cryptoStream = new CryptoStream(encryptedConfig.Open(), aesCrypto.CreateDecryptor(), CryptoStreamMode.Read))
                                        using (StreamReader reader = new StreamReader(cryptoStream, Encoding.UTF8))
                                        {
                                            KeyDictionary.Add(key.ID, Encoding.UTF8.GetBytes(await reader.ReadToEndAsync()));
                                        }
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }

                        foreach (var script in configuration.Scripts)
                        {
                            using (var aesCrypto = Crypto.GetAes())
                            {
                                aesCrypto.IV = script.EncryptionIV;
                                aesCrypto.Key = script.EncryptionKey;

                                var encryptedConfig = zipConfig.GetEntry(script.Source);

                                using (CryptoStream cryptoStream = new CryptoStream(encryptedConfig.Open(), aesCrypto.CreateDecryptor(), CryptoStreamMode.Read))
                                using (StreamReader reader = new StreamReader(cryptoStream, Encoding.UTF8))
                                {
                                    KeyDictionary.Add(script.ID, Encoding.UTF8.GetBytes(await reader.ReadToEndAsync()));
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR");
                }
            }
        }

        private async void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var zipConfig = new ZipArchive(saveFileDialog.OpenFile(), ZipArchiveMode.Create, false, Encoding.UTF8))
                    {
                        foreach (var key in configuration.Keys)
                        {
                            if (key.Category == ProgramConfiguration.Key.KeyCategory.SSH)
                                using (var aesCrypto = Crypto.GetAes())
                                {
                                    key.EncryptionIV = aesCrypto.IV;
                                    key.EncryptionKey = aesCrypto.Key;
                                    key.Source = $"k#{key.ID}";

                                    var encryptedConfig = zipConfig.CreateEntry(key.Source);

                                    using (CryptoStream cryptoStream = new CryptoStream(encryptedConfig.Open(), aesCrypto.CreateEncryptor(), CryptoStreamMode.Read))
                                    using (var writer = new StreamWriter(cryptoStream, Encoding.UTF8))
                                    {
                                        await writer.WriteAsync(Encoding.UTF8.GetString(KeyDictionary[key.ID]));
                                    }
                                }
                        }

                        foreach (var script in configuration.Scripts)
                        {
                            using (var aesCrypto = Crypto.GetAes())
                            {
                                script.EncryptionIV = aesCrypto.IV;
                                script.EncryptionKey = aesCrypto.Key;
                                script.Source = $"s#{script.ID}";

                                var encryptedConfig = zipConfig.CreateEntry(script.Source);

                                using (CryptoStream cryptoStream = new CryptoStream(encryptedConfig.Open(), aesCrypto.CreateEncryptor(), CryptoStreamMode.Read))
                                using (var writer = new StreamWriter(cryptoStream, Encoding.UTF8))
                                {
                                    await writer.WriteAsync(Encoding.UTF8.GetString(ScriptDictionary[script.ID]));
                                }
                            }
                        }

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
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR");
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
            hostListBox.Items.Add(new ProgramConfiguration.Target { Host = hostTextBox.Text});
        }

        private void hostRemoveButton_Click(object sender, EventArgs e)
        {
            if (hostListBox.SelectedIndex >= 0 && hostListBox.SelectedIndex < hostListBox.Items.Count)
                hostListBox.Items.RemoveAt(hostListBox.SelectedIndex);
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
                if (((hostListBox.Items[hostListBox.SelectedIndex] as ProgramConfiguration.Target)?.Jobs?.Length ?? -1)  >= 0)
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
            if (hostListBox.SelectedIndex >= 0 && hostListBox.SelectedIndex < hostListBox.Items.Count)
                (hostListBox.Items[hostListBox.SelectedIndex] as ProgramConfiguration.Target).Jobs = jobListBox.Items.Cast<ProgramConfiguration.Target.Job>().ToArray(); 

        }

        private void jobRemoveButton_Click(object sender, EventArgs e)
        {
            if (jobListBox.SelectedIndex >= 0 && jobListBox.SelectedIndex < jobListBox.Items.Count)
                jobListBox.Items.RemoveAt(jobListBox.SelectedIndex);

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

            if (jobListBox.SelectedIndex >= 0 && jobListBox.SelectedIndex < jobListBox.Items.Count)
                (jobListBox.Items[jobListBox.SelectedIndex] as ProgramConfiguration.Target.Job).Keys = keyListBox.Items.Cast<ProgramConfiguration.Key>().Select((key) => key.ID).ToArray();
        }

        private void scriptRemoveButton_Click(object sender, EventArgs e)
        {
            if (scriptListBox.SelectedIndex >= 0 && scriptListBox.SelectedIndex < scriptListBox.Items.Count)
                scriptListBox.Items.RemoveAt(scriptListBox.SelectedIndex);

            if (jobListBox.SelectedIndex >= 0 && jobListBox.SelectedIndex < jobListBox.Items.Count)
                (jobListBox.Items[jobListBox.SelectedIndex] as ProgramConfiguration.Target.Job).Scripts = scriptListBox.Items.Cast<ProgramConfiguration.Script>().Select((script) => script.ID).ToArray();
        }
    }
}
