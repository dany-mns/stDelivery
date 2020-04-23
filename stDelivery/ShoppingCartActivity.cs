using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace stDelivery
{
    class ShoppingCartActivity : Activity
    {
        private FinishOrderActivity finishOrderActivity;

        public ShoppingCartActivity(AppCompatActivity _mainActivity) : base(_mainActivity)
        {
            Button finalizareComanda = _mainActivity.FindViewById<Button>(Resource.Id.finalizareComanda);
            finalizareComanda.Click += (object sender, System.EventArgs e) =>
            {
                _mainActivity.SetContentView(Resource.Layout.FinishOrder);
                this.finishOrderActivity = new FinishOrderActivity(_mainActivity);
            };
        }
    }
}