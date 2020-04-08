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

    public enum TypeOfFood { Pizza=0, Hamburger, Desert}
    public abstract class IFood
    {
        private string _name;
        private string _description;
        private int _price;

        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }
        public int Price { get => _price; set => _price = value; }
    }
}