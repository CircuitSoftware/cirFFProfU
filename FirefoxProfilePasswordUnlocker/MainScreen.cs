using System;
using System.IO;
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
        string database = "signons.sqlite";
        string dbstring;

        private void SetDirectoryPath(string path)
        {
            tbDirectoryPath.Text = path;

            DirectoryPathChanged();
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
            string rootdir = Environment.SpecialFolder.MyComputer.ToString();

            if (tbDirectoryPath.Text.Contains(@":\"))
            {
                rootdir = tbDirectoryPath.Text;
            }

            FolderBrowserDialog fbd = new FolderBrowserDialog()
            {
                Description = "Select folder with Firefox profile in it.",
                SelectedPath = rootdir
            };

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                profilePath = fbd.SelectedPath;
                SetDirectoryPath(profilePath);
            }
        }

        private void ResetLbProfiles()
        {
            lbProfiles.Items.Clear();
            lbProfiles.ClearSelected();
            dgUserCredentials.Enabled = false;
            dgUserCredentials.Hide();
        }

        private void DirectoryPathChanged()
        {
            ResetLbProfiles();

            if (tbDirectoryPath.Text.Contains(@":\"))
            {
                DirectoryInfo di = new DirectoryInfo(tbDirectoryPath.Text);

                foreach (var fi in di.GetDirectories("*.default"))
                {
                    lbProfiles.Items.Add(fi.Name);
                }

                lbProfiles.SelectionMode = SelectionMode.One;
            }
            else
            {
                lbProfiles.Items.Add("No firefox profile found.");
                lbProfiles.SelectionMode = SelectionMode.None;
            }
        }

        private bool LbProfileSelected()
        {
            return lbProfiles.SelectedIndex >= 0;
        }

        private void SetDgUC(bool on)
        {
            if (on)
            {
                dgUserCredentials.Enabled = true;
                dgUserCredentials.Show();
            }
            else
            {
                dgUserCredentials.Enabled = false;
                dgUserCredentials.Hide();
            }
        }

        private void LbProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDgUC(LbProfileSelected());

            dbstring = tbDirectoryPath.Text + @"\" + lbProfiles.SelectedItem.ToString() + @"\" + database;

            if (File.Exists(dbstring))
            {
                Decoder decoder = new Decoder();

                dgUserCredentials.DataSource = decoder.Decode(dbstring).Tables[0];
            }
        }
    }
}