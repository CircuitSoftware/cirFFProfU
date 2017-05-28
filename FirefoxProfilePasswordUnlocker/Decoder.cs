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
using System.Data.SQLite;

namespace FirefoxProfilePasswordUnlocker
{
    public class Decoder
    {
        SQLiteConnection sqlite;
        SQLiteCommand sqlcmd;
        string sql;

        public DataSet Decode(string database)
        {
            //DataGridView dgv = new DataGridView();

            DataSet dataset = new DataSet();

            sqlite = new SQLiteConnection("Data Source="+database+";Version=3");
            sqlite.Open();

            sql = "select hostname as Website, encryptedUsername as Username, encryptedPassword as Password from moz_logins order by hostname asc";
            //sql += 

            sqlcmd = new SQLiteCommand(sql, sqlite);

            SQLiteDataAdapter sqladapt = new SQLiteDataAdapter(sql, sqlite);
            sqladapt.Fill(dataset);

            //dgv.DataSource = dataset.Tables[0];

            return dataset;
        }

        public void DecodeBase64()
        {

        }
    }
}