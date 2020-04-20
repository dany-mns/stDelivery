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
    public class IFood
    {
        public List<Menuitem> menuitem { get; set; }
    }
}