using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirefoxProfilePasswordUnlocker
{
    public partial class MainScreen : Form
    {
        public MainScreen()
        {
            InitializeComponent();
        }

        string profilePath = "Choose directory from your pc...";

        private void SetDirectoryPath(string path)
        {
            tbDirectoryPath.Text = path;
        }

        public static string GetDefaultFirefoxProfileDirectory
        {
            get
            {
                string mozilla = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Mozilla");

                if (Directory.Exists(mozilla))
                {
                    string firefox = Path.Combine(mozilla, "Firefox");

                    if (Directory.Exists(firefox))
                    {
                        string profiles = Path.Combine(firefox, "Profiles");

                        if (Directory.Exists(profiles))
                        {
                            return profiles;
                        }
                    }
                }
                return "No profile directory can be found.";
            }
        }

        private void RadioDirectory_CheckedChanged(object sender, EventArgs e)
        {
            lbProfiles.Items.Clear();

            if (rbDirectoryDefault.Checked)
            {
                //tbDirectoryPath.Text = ReadFirefoxProfile;
                bBrowseFolder.Enabled = false;
                tbDirectoryPath.Enabled = false;
                SetDirectoryPath(GetDefaultFirefoxProfileDirectory);
            }
            else if (rbDirectoryChoose.Checked)
            {
                bBrowseFolder.Enabled = true;
                tbDirectoryPath.Enabled = true;
                SetDirectoryPath(profilePath);
            }
            else
            {
                SetDirectoryPath("");
            }
        }

        private void BBrowseFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog()
            {
                Description = "Select folder with Firefox profile in it.",
                RootFolder = Environment.SpecialFolder.MyComputer
            };
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                profilePath = fbd.SelectedPath;
                SetDirectoryPath(profilePath);
            }
        }

        private void TbDirectoryPath_Changed(object sender, EventArgs e)
        {
            lbProfiles.ClearSelected();

            if (tbDirectoryPath.Text.Contains(@":\"))
            {
                DirectoryInfo di = new DirectoryInfo(tbDirectoryPath.Text);

                foreach (var fi in di.GetDirectories("*.default"))
                {
                    lbProfiles.Items.Add(fi.Name);
                }
            }
            else
            {
                //MessageBox.Show(tbDirectoryPath.Text);
            }
        }

        private void LbProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LbProfileSelected())
            {
                dgUserCredentials.Enabled = true;
                dgUserCredentials.Show();


            }
            else {
                dgUserCredentials.Enabled = false;
                dgUserCredentials.Hide();
            }
        }

        private bool LbProfileSelected()
        {
            return lbProfiles.SelectedIndex >= 0;
        }
    }
}