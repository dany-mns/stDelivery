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
    public class Persoana
    {
        private User Client { get; set; }
        //private IFood Food{get;set;}

        public Persoana(User user)
        {
            this.Client = user;
            //food = new IFood();
        }

        
    }

    public static class GlobalVariables
    {
        public static Persoana currentUser;
    }
}