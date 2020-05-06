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
using stDelivery.Kitchen;

namespace stDelivery
{
    public class Persoana
    {
        public User Client { get; set; }
        public IFood ShoppingCart { get => _shoppingCart; set => _shoppingCart = value; }

        private IFood _shoppingCart;

        public Persoana(User user)
        {
            this.Client = user;
            _shoppingCart = new IFood();
        }

        
    }

    public static class GlobalVariables
    {
        public static Persoana currentUser;
    }
}