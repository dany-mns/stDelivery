using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Security.Cryptography;


namespace stDelivery
{
    class Crypt
    {
        public static string SHA256hash(string pass)
        {
            SHA256 hash = SHA256Managed.Create();
            Byte[] result = hash.ComputeHash(Encoding.UTF8.GetBytes(pass));
            StringBuilder sb = new StringBuilder();
            foreach (Byte b in result)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

        
    }
}