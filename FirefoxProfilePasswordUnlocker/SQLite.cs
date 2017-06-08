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
    public class SQLite
    {
        SQLiteConnection sqlite;
        SQLiteCommand sqlcmd;
        string sql;

        public DataSet GetData(string database)
        {
            DataSet dataset = new DataSet();

            sqlite = new SQLiteConnection("Data Source="+database+";Version=3");
            try
            {
                sqlite.Open();
            }
            catch (SQLiteException e)
            {
                Console.WriteLine("SQLite Exception: " + e.Message);
            }

            sql = "select hostname as Website, encryptedUsername as Username, encryptedPassword as Password from moz_logins order by hostname asc";

            sqlcmd = new SQLiteCommand(sql, sqlite);

            SQLiteDataAdapter sqladapt = new SQLiteDataAdapter(sql, sqlite);
            sqladapt.Fill(dataset);

            return dataset;
        }
    }
}