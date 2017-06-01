using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirefoxProfilePasswordUnlocker
{
    public class Decoder
    {
        string globalSalt = "676c6f62616c2d73616c74";
        string passwordCheck = "70617373776f72642d636865636b";
        string GS, ES, HP, CHP, PES, tk, k1, k2, k;


        public String GetKey(string keyPath)
        {
            string key = "";
            FileStream fs = new FileStream(keyPath, FileMode.Open);
            int hexIn;
            String hex;

            for (int i = 0; (hexIn = fs.ReadByte()) != -1; i++)
            {
                hex = string.Format("{0:X2}", hexIn);
                key += hex;
            }

            return key;
        }

        public String Decode(string hash)
        {
            string value;

            value = hash;
            
            return value;
        }

    }
}