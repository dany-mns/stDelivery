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
    public class Restaurant
    {
        public Pizza Pizza { get; set; }
        public Hamburger Hamburger { get; set; }
        public Ciorba Ciorba { get; set; }
        public Paste Paste { get; set; }
        public Salata Salata { get; set; }
        public Desert Desert { get; set; }
    }
}