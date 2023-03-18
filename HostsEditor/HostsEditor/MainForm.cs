using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Created by BBitar#3731
//Feel free to do wtv u want with the code/improve
//Enjoy :D


namespace HostsEditor
{
    public partial class MainForm : Form
    {
        
        public MainForm()
        {
            
            InitializeComponent();
            SetBackgroundImage();
            
        }


        private void SetBackgroundImage()
        {
            // Load the background image from Resources
            Image backgroundImage = Properties.Resources.gamingImage;

            this.BackgroundImage = backgroundImage;

            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            
            Image backgroundImage = this.BackgroundImage;

            if (backgroundImage != null)
            {
                Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);

                // Draw the background image behind all the controls
                e.Graphics.DrawImage(backgroundImage, rect);
            }
        }
        private void addIPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnJoin.PerformClick();
        }

        private void removeIPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnLeave.PerformClick();
        }

        private void btnJoin_Click(object sender, EventArgs e)
        {
            // Get the server IP from the tbServerIP 
            string serverIP = tbServerIP.Text;
            AddHostsEntries(serverIP);
            MessageBox.Show("Entries added to hosts file.", "BBitar GTPS Joiner", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        private void btnLeave_Click(object sender, EventArgs e)
        {
            // Get the server IP from the tbServerIP 
            string serverIP = tbServerIP.Text;
            RemoveHostsEntries(serverIP);
            MessageBox.Show("Entries removed from hosts file." ,"BBitar GTPS Joiner", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void AddHostsEntries(string serverIP)
        {
            
            string[] entriesToAdd = new string[]
            {
        $"\n{serverIP} growtopia1.com",
        $"{serverIP} growtopia2.com",
        $"{serverIP} www.growtopia1.com",
        $"{serverIP} www.growtopia2.com"
            };

            string hostsContent = File.ReadAllText(@"C:\Windows\System32\drivers\etc\hosts");
            // Check if the entries already exist in the hosts file
            bool entriesAlreadyExist = entriesToAdd.All(entry => hostsContent.Contains(entry));
            if (!entriesAlreadyExist)
            {
                hostsContent += string.Join("\n", entriesToAdd) + "\n";
                File.WriteAllText(@"C:\Windows\System32\drivers\etc\hosts", hostsContent);
            }
        }

        private void RemoveHostsEntries(string serverIP)
        {
            string[] entriesToRemove = new string[]
            {
        $"{serverIP} growtopia1.com",
        $"{serverIP} growtopia2.com",
        $"{serverIP} www.growtopia1.com",
        $"{serverIP} www.growtopia2.com"
            };

            // Get the contents of hosts file
            string hostsContent = File.ReadAllText(@"C:\Windows\System32\drivers\etc\hosts");

            // Remove entries from the hosts file
            foreach (string entry in entriesToRemove)
            {
                hostsContent = hostsContent.Replace(entry + "\n", "");
            }

            // Write the modified hosts file contents back to the filepath
            File.WriteAllText(@"C:\Windows\System32\drivers\etc\hosts", hostsContent);
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/BBitarX");
        }
    }
}

