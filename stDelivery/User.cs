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

namespace stDelivery
{

    public class User
    {
        [SQLite.PrimaryKey,SQLite.AutoIncrement]
        public int Id { get; set; }
        [SQLite.NotNull]
        public string Nume { get; set; }
        [SQLite.NotNull]
        public string Prenume { get; set; }
        [SQLite.NotNull]
        public string Email { get; set; }
        [SQLite.NotNull]
        public int Varsta { get; set; }
        [SQLite.NotNull]
        public string Adresa { get; set; }
        [SQLite.NotNull]
        public string Parola { get; set; }

        
    
    }
}