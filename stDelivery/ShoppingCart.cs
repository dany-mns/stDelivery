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
    class ShoppingCart
    {
        private List<Food> cosCumparaturi;
        public ShoppingCart()
        {
            this.cosCumparaturi = new List<Food>();
        }

        public Food AdaugainCos
        {
            set{ this.cosCumparaturi.Add(value); }
        }
        
        public List<Food> CosCumparaturi
        {
            get { return this.cosCumparaturi;  }
        }

        public Food EliminadinCos
        {
            set { this.cosCumparaturi.Remove(value);  }
        }
    }
}