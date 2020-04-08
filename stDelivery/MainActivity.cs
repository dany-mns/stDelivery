using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace stDelivery
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            var btn = FindViewById(Resource.Id.btnSwitchToMenu);
            btn.Click += delegate
            {
                Intent menuActivity = new Intent(this, typeof(FoodMenuActivity));
                this.StartActivity(menuActivity);
            };
        }

        //void OnButtonClicked(object sender, EventArgs args)
        //{
        //    Intent menuActivity = new Intent(this, typeof(FoodMenuActivity));
        //    this.StartActivity(menuActivity);
        //}


    }
}

