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
using stDelivery;

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
		private Food _foods;

		//User controls
		private RecyclerView _recyclerView;
		private TextView _btnFinish;

        /// <summary>
        /// Get user selected type of foods
        /// Init RecyclerView view and populate withe foods selected
        /// </summary>
        protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.FoodMenuActivity);

			_btnFinish = (TextView)FindViewById(Resource.Id.btnFinishBuy);
			_recyclerView = (RecyclerView)FindViewById(Resource.Id.myRecyclerView);
            
			int typeFood = Intent.GetIntExtra("Type", -1);
			this._foods = GetSpecificFood(typeFood);
            
			SetupRecyclerView();

			_btnFinish.Click += delegate
			{
				Intent shoppingCartActivity = new Intent(this, typeof(ShoppingCartActivity));
				StartActivity(shoppingCartActivity);
                Log.Info("myap", "Send new object to Stef activity");
			};
		}


        /// <summary>
        /// Init recycler view for performace.
        /// </summary>
        private void SetupRecyclerView()
		{
			_recyclerView.SetLayoutManager(new Android.Support.V7.Widget.LinearLayoutManager(_recyclerView.Context));
			FoodAdapter adapter = new FoodAdapter(this._foods);
			adapter.ItemBuyClick += Adapter_BuyItemClick;
			_recyclerView.SetAdapter(adapter);
		}

        /// <summary>
        /// Adds food in cart shop or not.
        /// </summary>
        /// See <see cref="FoodAdapterClickEventArgs"/> to use adapter click event.
        /// <param name="sender">Unused in this function.</param>
        /// <param name="e">Get selected element.</param>
        private void Adapter_BuyItemClick(object sender, FoodAdapterClickEventArgs e)
		{
			string nameFood = _foods.menuitem[e.Position].Name;
			string priceFood = _foods.menuitem[e.Position].Price.ToString();
			Android.Support.V7.App.AlertDialog.Builder message = new Android.Support.V7.App.AlertDialog.Builder(this);
			message.SetTitle("Ati adaugat in cos");
			message.SetMessage(nameFood + "\nPret: " + priceFood + " LEI.");
			message.SetPositiveButton("Confirm", (confirmAlert, args) =>
			{
                //Add new item food in shopping cart
                GlobalVariables.currentUser.ShoppingCart.Add(_foods.menuitem[e.Position]);

            });

			message.SetNegativeButton("Revoke", (revokeAlert, args) =>
			{
				// Do nothing
			});

			message.Show();
		}

        /// <summary>
        /// Make the food factory
        /// </summary>
        /// <returns>
        /// The factory or null in case of bad user choice.
        /// </returns>
        /// <param name="typeFood">An integer which specify user choice.</param>
        private Food GetSpecificFood(int typeFood)
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