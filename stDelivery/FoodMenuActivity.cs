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
using stDelivery.FileWork;
using stDelivery.Kitchen;

namespace stDelivery
{
    [Activity(Label = "FoodMenu", Theme = "@style/AppTheme.NoActionBar")]
    public class FoodMenuActivity : Activity
    {
        private FoodFactory _foodFactory;
        private IFood _foods;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FoodMenuActivity);
            int typeFood = Intent.GetIntExtra("Type", -1);
            if(typeFood != -1)
            {
                TypeOfFood myFood = (TypeOfFood)typeFood;
                this._foodFactory = new FoodFactory(myFood);
                try
                {
                    this._foods = this._foodFactory.prepareFoods();
                } catch (Exception e)
                {
                    Log.Info("myapp", e.Message);
                }
            }

        }
    }
}