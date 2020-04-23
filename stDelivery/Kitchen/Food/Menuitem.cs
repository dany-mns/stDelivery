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
    public class Menuitem
    {
        public string name { get; set; }
        public string description { get; set; }
        public int price { get; set; }
    }
}