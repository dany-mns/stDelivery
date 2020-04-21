﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using stDelivery.Adapter;
using stDelivery.FileWork;
using stDelivery.Kitchen;

namespace stDelivery
{
    [Activity(Label = "FoodMenu", Theme = "@style/AppTheme.NoActionBar")]
    public class FoodMenuActivity : Activity
    {
        private FoodFactory _foodFactory;
        private IFood _foods;

        //User controls
        private RecyclerView _recyclerView;
        private TextView _btnFinish;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FoodMenuActivity);

            _btnFinish = (TextView)FindViewById(Resource.Id.btnFinishBuy);
            _recyclerView = (RecyclerView)FindViewById(Resource.Id.myRecyclerView);

            //Get user selected type of foods
            int typeFood = Intent.GetIntExtra("Type", -1);
            this._foods = GetSpecificFood(typeFood);

            SetupRecyclerView();
        }

        private void SetupRecyclerView()
        {
            _recyclerView.SetLayoutManager(new Android.Support.V7.Widget.LinearLayoutManager(_recyclerView.Context));
            FoodAdapter adapter = new FoodAdapter(this._foods);
            _recyclerView.SetAdapter(adapter);
        }

        private IFood GetSpecificFood(int typeFood)
        {

            if (typeFood != -1)
            {
                TypeOfFood myFood = (TypeOfFood)typeFood;
                this._foodFactory = new FoodFactory(myFood);
                try
                {
                    return this._foodFactory.prepareFoods();
                }
                catch (Exception e)
                {
                    Log.Info("myapp", e.Message);
                }
            }
            return null;
        }
    }
}