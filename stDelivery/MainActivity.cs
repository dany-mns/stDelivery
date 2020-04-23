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
using stDelivery.Kitchen;

/**************************************************************************
 *                                                                        *
 *  File:        FoodCategoryActivity.cs                                  *
 *  Copyright:   (c) 2020, Manastireanu Dany                              *
 *  E-mail:      andrei-dany.manastireanu@student.tuiasi.ro               *
 *  Website:     http://127.0.0.1                                         *
 *  Description: Main activity for select food category                   *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/


namespace stDelivery
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private RestaurantFactory restaurantFactory;


        /// <summary>
        /// The main entry point for the application food select.
        /// </summary>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            // First and unique init of restaurant (singleton)
            string restaurantFoods = "food-menu.json";
            restaurantFactory = RestaurantFactory.GetInstance(this.Assets, restaurantFoods);
            Restaurant restaurant = restaurantFactory.Restaurant;


            // Start corect view based on user choice
            var btnPizza = FindViewById(Resource.Id.btnPizza);
            btnPizza.Click += delegate
            {
                Intent menuActivity = new Intent(this, typeof(FoodMenuActivity));
                menuActivity.PutExtra("Type", 0);
                this.StartActivity(menuActivity);
            };

            var btnHam = FindViewById(Resource.Id.btnHamburger);
            btnHam.Click += delegate
            {
                Intent menuActivity = new Intent(this, typeof(FoodMenuActivity));
                menuActivity.PutExtra("Type", 1);
                this.StartActivity(menuActivity);
            };

            var btnCiorba = FindViewById(Resource.Id.btnCiorba);
            btnCiorba.Click += delegate
            {
                Intent menuActivity = new Intent(this, typeof(FoodMenuActivity));
                menuActivity.PutExtra("Type", 2);
                this.StartActivity(menuActivity);
            };

            var btnPaste = FindViewById(Resource.Id.btnPaste);
            btnPaste.Click += delegate
            {
                Intent menuActivity = new Intent(this, typeof(FoodMenuActivity));
                menuActivity.PutExtra("Type", 3);
                this.StartActivity(menuActivity);
            };

            var btnSalata = FindViewById(Resource.Id.btnSalata);
            btnSalata.Click += delegate
            {
                Intent menuActivity = new Intent(this, typeof(FoodMenuActivity));
                menuActivity.PutExtra("Type", 4);
                this.StartActivity(menuActivity);
            };

            var btnDesert = FindViewById(Resource.Id.btnDesert);
            btnDesert.Click += delegate
            {
                Intent menuActivity = new Intent(this, typeof(FoodMenuActivity));
                menuActivity.PutExtra("Type", 5);
                this.StartActivity(menuActivity);
            };
        }
    }
}

