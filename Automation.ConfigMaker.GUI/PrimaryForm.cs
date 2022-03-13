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
        public static readonly string DefaultFileFilter = $"Configuration | {DefaultFileExtension}";

        public PrimaryForm()
        {
            InitializeComponent();

            saveFileDialog1.InitialDirectory = Environment.GetEnvironmentVariables()["USERPROFILE"].ToString();
            saveFileDialog1.FileName = DefaultFileName;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.DereferenceLinks = true;
            saveFileDialog1.OverwritePrompt = true;
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.ValidateNames = true;
            saveFileDialog1.DefaultExt = DefaultFileExtension;
            saveFileDialog1.Filter = DefaultFileFilter;

            openFileDialog1.InitialDirectory = Environment.GetEnvironmentVariables()["USERPROFILE"].ToString();
            openFileDialog1.FileName = DefaultFileName;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.DereferenceLinks = true;
            openFileDialog1.Multiselect = false;
            openFileDialog1.AddExtension = true;
            openFileDialog1.ValidateNames = true;
            openFileDialog1.DefaultExt = DefaultFileExtension;
            openFileDialog1.Filter = DefaultFileFilter;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (var configStream = openFileDialog1.OpenFile())
                using (var zipConfig = new ZipArchive(configStream, ZipArchiveMode.Read, true, Encoding.UTF8))
                using (var hash = SHA512.Create())
                using (var aesCrypto = Crypto.GetAes512(hash.ComputeHash(new MemoryStream(Encoding.UTF8.GetBytes("This is a completely valid vay of initializing the IV. Trust me, I am an engineer!")))))
                {
                    aesCrypto.Key = hash.ComputeHash(new MemoryStream(Encoding.UTF8.GetBytes(Console.ReadLine())));

                    var encryptedConfig = zipConfig.GetEntry("config.xml.aes");

                    using (CryptoStream cryptoConfigStream = new CryptoStream(encryptedConfig.Open(), aesCrypto.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        var rootConfig = ProgramConfiguration.Parse(cryptoConfigStream);
                    }

                }
            }
        }

        private async void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = saveFileDialog1.ShowDialog();
        }
    }
}
