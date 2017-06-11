using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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
        //// GS Value
        ////6069e2a66b738bd4492179a70975d2aa952db20f
        //// ES
        ////82f0b7d9c8de052ef0a639ed3a1f8e36580fac8e
        //// PC Value
        ////42b111eba74474d7f0ed09f3b643141d

        //string globalSalt = "676c6f62616c2d73616c74";
        //string passwordCheck = "70617373776f72642d636865636b";
        //string GS, MP, ES, HP, CHP, PES, tk, k1, k2, k;

        byte[] bKey, bIV;

        ///////////////////////////
        ///// TESTING PURPOSE /////
        string GS = "6069e2a66b738bd4492179a70975d2aa952db20f";
        string MP = "";
        string ES = "82f0b7d9c8de052ef0a639ed3a1f8e36580fac8e";
        string PES = "82f0b7d9c8de052ef0a639ed3a1f8e36580fac8e";
        string HP, CHP, tk, k1, k2, k, key, iv;
        ///////////////////////////

        ///////////////////////////
        ///// TESTING PURPOSE /////
        //string GS = "5aac8e0439e8d69ea0fe1bc013cd5af8";
        //string MP = "";
        //string ES =  "1596bb8112652a43e7bdfb2fdc8799e5";
        //string PES = "1596bb8112652a43e7bdfb2fdc8799e500000000";
        //string HP, CHP, tk, k1, k2, k, key, iv;
        /////////////////////////////

        //
        public static string HexStringFromBytes(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }

        // 
        public static byte[] HexStringToBytes(string hexString)
        {
            if (hexString.Length % 2 != 0)
            {
                throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, "The binary key cannot have an odd number of digits: {0}", hexString));
            }

            byte[] hexAsBytes = new byte[hexString.Length / 2];
            for (int index = 0; index < hexAsBytes.Length; index++)
            {
                string byteValue = hexString.Substring(index * 2, 2);
                hexAsBytes[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }

            return hexAsBytes;
        }
        
        // SHA1 HELPER CLASS TO CALCULATE DECRYPTION KEY
        public static string GetSha1Hash(string text)
        {
            string result = null;

            //var arrayData = HexStringToBytes(text);
            byte[] arrayData = Encoding.Unicode.GetBytes(text);

            using (var sha1 = new SHA1CryptoServiceProvider()){
                var arrayResult = sha1.ComputeHash(arrayData);

                result = HexStringFromBytes(arrayResult);
            }
            
            return result;
        }

        // SHA1 HMAC ALGORITHM
        public static string GetHmacHash(string input, byte[] key)
        {
            string result = null;

            byte[] arrayData = HexStringToBytes(input);

            using (var hmac = new HMACSHA1(key))
            {
                var arrayResult = hmac.ComputeHash(arrayData);
                //result = arrayResult.Aggregate("", (s, e) => s + String.Format("{0:x2}", e), s => s);
                result = HexStringFromBytes(arrayResult);
            }

            return result;
        }

        // CALCULATING DECRYPTION KEY FROM KEY3.DB
        public String GetKey(string keyPath)
        {
            // TODO: getting GS, ES & PES from the key3.db
            //FileStream fs = new FileStream(keyPath, FileMode.Open);
            //int hexIn;
            //String hex;

            //for (int i = 0; (hexIn = fs.ReadByte()) != -1; i++)
            //{
            //    hex = string.Format("{0:X2}", hexIn);
            //    k += hex;
            //}

            // COMPUTE HASHED PASSWORD
            HP = GetSha1Hash(GS + MP);
            MainScreen.OutputCmdLine("HP", HP);

            // COMPUTE COMBINES HASHED PASSWORD
            CHP = GetSha1Hash(HP + ES);
            MainScreen.OutputCmdLine("CHP", CHP);

            // CREATING BYTE ARRAY FROM CHP
            byte[] CHPKey = HexStringToBytes(CHP);

            // CREATING FIRST PART OF THE KEY
            k1 = GetHmacHash(PES + ES, CHPKey);
            MainScreen.OutputCmdLine("k1", k1);

            // GENERATING TEMPORARY KEY
            tk = GetHmacHash(PES, CHPKey);
            MainScreen.OutputCmdLine("tk", tk);

            // CREATING SECOND PART OF THE KEY
            k2 = GetHmacHash(tk + ES, CHPKey);
            MainScreen.OutputCmdLine("k2", k2);

            // CREATE KEY
            k = k1 + k2;
            MainScreen.OutputCmdLine("k", k);

            // KEY
            key = k.Substring(0, 48);
            bKey = HexStringToBytes(key);
            MainScreen.OutputCmdLine("Key", key);

            // INITIALIZATION VECTOR
            iv = k.Substring(64, 16);
            bIV = HexStringToBytes(iv);
            MainScreen.OutputCmdLine("IV", iv);

            // RETURN FINAL KEY
            return k;
        }

        // DECODING METHOD
        public String Decode(string hash)
        {
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

            //byte[] toDecryptArray = Convert.FromBase64String(hash);
            byte[] toDecryptArray = HexStringToBytes(hash);
            //byte[] toDecryptArray = HexStringToByteArray(passwordCheck);
            byte[] resultArray = { 0 };

            ICryptoTransform cTransform = tdes.CreateDecryptor(bKey, bIV);

            try
            {
                resultArray = cTransform.TransformFinalBlock(toDecryptArray, 0, toDecryptArray.Length);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("CryptographicException: " + e.Message);
            }

            return Encoding.UTF8.GetString(resultArray);
        }
    }
}