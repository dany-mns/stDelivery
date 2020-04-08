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

namespace stDelivery.Food
{

    public enum TypeOfFood { Pizza, Hamburger, Desert}
    abstract class Food
    {
        private string _name;
        private string _description;
        private int _price;
    }
}