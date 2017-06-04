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

        SQLite sql = new SQLite();
        Decoder decoder = new Decoder();
        string profilePath = "Choose directory from your pc...";
        string database = "signons.sqlite";
        string keyFile = "key3.db";
        string dbString;
        string keyPath;
        string keyString;
        DataTable dtCredentials;

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

            if (lbProfiles.SelectedItem != null)
            {
                dbString = tbDirectoryPath.Text + @"\" + lbProfiles.SelectedItem.ToString() + @"\" + database;

                keyPath = tbDirectoryPath.Text + @"\" + lbProfiles.SelectedItem.ToString() + @"\" + keyFile;


                if (File.Exists(dbString) && File.Exists(keyPath))
                {
                    Decoder decoder = new Decoder();
                    keyString = decoder.GetKey(keyPath);

                    Console.WriteLine("Key: " + keyString);

                    dtCredentials = sql.GetData(dbString).Tables[0];

                    string hash;
                    string value;
                    int colCount = dtCredentials.Columns.Count;
                    int rowCount = dtCredentials.Rows.Count;

                    //for (int i = 1; i < colCount; i++)
                    //{
                    //    for (int j = 0; j < rowCount; j++)
                    //    {
                    //        hash = dtCredentials.Rows[j][i].ToString();

                    //        switch (i)
                    //        {
                    //            case 1:
                    //                Console.WriteLine($"Username: {hash}");
                    //                break;
                    //            case 2:
                    //                Console.WriteLine($"Password: {hash}");
                    //                break;
                    //        }

                    //        value = decoder.Decode(hash);

                    //        switch (i)
                    //        {
                    //            case 1:
                    //                Console.WriteLine($"Username(decode): {value}");
                    //                break;
                    //            case 2:
                    //                Console.WriteLine($"Password(decode): {value}");
                    //                break;
                    //        }
                    //    }
                    //}

                    dgUserCredentials.DataSource = dtCredentials;

                    dgUserCredentials.AutoResizeColumn(0);

                }
            }
        }
    }
}