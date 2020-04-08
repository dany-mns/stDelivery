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

namespace stDelivery.Kitchen
{
    class Hamburger : IFood
    {
        public Hamburger(string name, string desc, int price)
        {
            this.Name = name;
            this.Description = desc;
            this.Price = price;
        }
    }
}