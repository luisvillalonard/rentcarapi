using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diversos.Core.Models
{
    public static class Util
    {
        private readonly static Random random = new();
        private readonly static string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!#$%&*-<>=";

        public static string RandomString(int length)
        {
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static byte[] NewSalt()
        {
            byte[] _salt = new byte[24];
            (new Random()).NextBytes(_salt);
            return _salt;
        }

        public static byte[] Base64ToBytes(string base64)
        {
            var valores = base64.Split(';').ToArray();
            if (valores.Length != 2)
                return null;

            var valor = valores.LastOrDefault().Replace("base64,", "");
            if (string.IsNullOrEmpty(valor))
                return null;

            return Convert.FromBase64String(valor);
        }

        public static string StringToBase64(byte[] bytes, string extension)
        {
            if (bytes == null || string.IsNullOrEmpty(extension))
                return null;

            try
            {
                var base64 = Convert.ToBase64String(bytes);
                var valor = string.Format("data:image/{0};base64,{1}", extension, base64);
                return valor;
            }
            catch (Exception) 
            {
                return null;
            }
            
        }

        public static string ExtensionFromBase64(string base64)
        {
            var valores = base64.Split(';').ToArray();
            if (valores.Length != 2)
                return null;

            return valores.First().Split('/').Last();
        }
    }
}
