/**************************************************************************
 *                                                                        *
 *  File:        MainActivity.cs                                          *
 *  Copyright:   (c) 2020, Stratulat Stefan                               *
 *  E-mail:      stefanc.stratulat@gmail.com                              *
 *  Website:     -                                                        *
 *  Description: The main activity that handles all the activities and    *
 *              contains the Xamarin context.                             *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

using Android.App;
using Android.OS;
using Android.Support.V7.App;

namespace StDelivery
{
    /// <summary>
    /// The main activity that handles all the activities. This class offers the context to handle all the elements (views).
    /// </summary>
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        /// <summary>
        /// Generic method for setting the current activity view. All the activities wil be built in a chain. 
        /// In this method, we instantiate the ShoppingCartActivity.
        /// </summary>
        /// <param name="savedInstanceState"></param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ShoppingCart);

            ShoppingCartActivity shoppingCartActivity = new ShoppingCartActivity(this);
        }
    }
}

