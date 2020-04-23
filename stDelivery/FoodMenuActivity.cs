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
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using stDelivery.Adapter;
using stDelivery.Kitchen;

/**************************************************************************
 *                                                                        *
 *  File:        FoodMenuActivity.cs                                      *
 *  Copyright:   (c) 2020, Manastireanu Dany                              *
 *  E-mail:      andrei-dany.manastireanu@student.tuiasi.ro               *
 *  Website:     http://127.0.0.1                                         *
 *  Description: Activity for select from the multiple type of foods what *
 *                you like.                                               *
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

			// Init RecyclerView view and populate withe foods selected
			SetupRecyclerView();

			_btnFinish.Click += delegate
			{
				Log.Info("myap", "Send new object to Stef activity");
			};
		}

		private void SetupRecyclerView()
		{
			_recyclerView.SetLayoutManager(new Android.Support.V7.Widget.LinearLayoutManager(_recyclerView.Context));
			FoodAdapter adapter = new FoodAdapter(this._foods);
			adapter.ItemBuyClick += Adapter_BuyItemClick;
			_recyclerView.SetAdapter(adapter);
		}

		private void Adapter_BuyItemClick(object sender, FoodAdapterClickEventArgs e)
		{
			string nameFood = _foods.menuitem[e.Position].name;
			string priceFood = _foods.menuitem[e.Position].price.ToString();
			Android.Support.V7.App.AlertDialog.Builder message = new Android.Support.V7.App.AlertDialog.Builder(this);
			message.SetTitle("Ati adaugat in cos");
			message.SetMessage(nameFood + "\nPret: " + priceFood + " LEI.");
			message.SetPositiveButton("Confirm", (confirmAlert, args) =>
			{
				//Add new item food in shopping cart
			});

			message.SetNegativeButton("Revoke", (revokeAlert, args) =>
			{
				// Do nothing
			});

			message.Show();
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