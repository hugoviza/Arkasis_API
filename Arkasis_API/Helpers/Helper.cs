using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Arkasis_API.Helpers
{
    public class Helper
    {
        public static String GetMD5Hash(String input)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            String hash = s.ToString();
            return hash;
        }

        public static String PadLeftZero(String text, int length = 4)
        {
            String newText = text;

            while(newText.Length < length)
            {
                newText = "0" + newText;
            }

            return newText;
        }
    }
}
