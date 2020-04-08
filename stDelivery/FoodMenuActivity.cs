using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using stDelivery.Kitchen;

namespace stDelivery
{
    [Activity(Label = "FoodMenu", Theme = "@style/AppTheme.NoActionBar")]
    public class FoodMenuActivity : Activity
    {
        private FoodFactory _foodFactory;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FoodMenuActivity);
            int typeFood = Intent.GetIntExtra("Type", -1);
            if(typeFood != -1)
            {
                TypeOfFood myFood = (TypeOfFood)typeFood;
                this._foodFactory = new FoodFactory(myFood);
            }

            string content;
            AssetManager assets = this.Assets;
            using (StreamReader sr = new StreamReader(assets.Open("food-menu.json")))
            {
                content = sr.ReadToEnd();
                TextView text = FindViewById<TextView>(Resource.Id.textFoodDescription);
                text.Text = content;
            }
        }
    }
}