using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace stDelivery
{
    public class Food
    {
        private String name;
        private int price;
        
        public Food(String _name, int _price)
        {
            this.name = _name;
            this.price = _price;
        }

        public String Name {
            get { return this.name; }
            set { this.name = value;  }
        }

        public int Price
        {
            get { return this.price; }
            set { this.price = value; }
        }
    }
}