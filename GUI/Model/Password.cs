using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Model
{
    internal class Password
    {
        public static bool verity(string password, string Hasdpassword, string algorithm = "md5")
        {
            if (algorithm == "md5") return Password.Create_MD5(password) == Hasdpassword;

            return false;
        }

        public static string Create_MD5(string text)
        {
            return Password.ComputeHasd(text, new MD5CryptoServiceProvider());
        }

        public static string ComputeHasd(string input, HashAlgorithm algorithm)
        {
            Byte[] inputByte = Encoding.UTF8.GetBytes(input);
            Byte[] hasdByte = algorithm.ComputeHash(inputByte);

            return BitConverter.ToString(hasdByte).Replace("-", "").ToLower();
        }
    }
}
