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
        string GS, MP, ES, HP, CHP, PES, tk, k1, k2, k;

        // SHA1 hELPER CLASS TO CALCULATE DECRYPTION KEY
        public static string GetSha1Hash(string text)
        {
            var sha1 = new SHA1CryptoServiceProvider();

            string result = null;

            var arrayData = Encoding.ASCII.GetBytes(text);
            var arrayResult = sha1.ComputeHash(arrayData);
            foreach (var t in arrayResult)
            {
                var temp = Convert.ToString(t, 16);
                if (temp.Length == 1)
                    temp = string.Format("0{0}", temp);
                result += temp;
            }
            return result;
        }

        // SHA1 CHMAC ALGORITHM
        public static string GetChmacHash(string text)
        {
            var sha1 = new SHA1CryptoServiceProvider();

            string result = null;

            var arrayData = Encoding.ASCII.GetBytes(text);
            var arrayResult = sha1.ComputeHash(arrayData);
            foreach (var t in arrayResult)
            {
                var temp = Convert.ToString(t, 16);
                if (temp.Length == 1)
                    temp = string.Format("0{0}", temp);
                result += temp;
            }
            return result;
        }

        // CALCULATING DECRYPTION KEY FROM KEY3.DB
        public String GetKey(string keyPath)
        {
            FileStream fs = new FileStream(keyPath, FileMode.Open);
            int hexIn;
            String hex;

            for (int i = 0; (hexIn = fs.ReadByte()) != -1; i++)
            {
                hex = string.Format("{0:X2}", hexIn);
                k += hex;
            }

            // COMPUTE HASHED PASSWORD
            HP = GetSha1Hash(GS + MP);

            // 
            CHP = GetSha1Hash(HP + ES);

            // CREATING FIRST PART OF THE KEY
            k1 = GetChmacHash(PES + ES);

            // GENERATING TEMPORARY KEY
            tk = GetChmacHash(PES);

            // CREATING SECOND PART OF THE KEY
            k2 = GetChmacHash(tk + ES);

            // CREATE KEY
            k = k1 + k2;

            return k;
        }

        public String Decode(string hash)
        {
            string value;

            value = hash;

            return value;
        }

    }
}