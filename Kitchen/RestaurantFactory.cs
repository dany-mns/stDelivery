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
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using stDelivery.Kitchen;


/**************************************************************************
 *                                                                        *
 *  File:        FoodCategoryActivity.cs                                  *
 *  Copyright:   (c) 2020, Manastireanu Dany                              *
 *  E-mail:      andrei-dany.manastireanu@student.tuiasi.ro               *
 *  Website:     http://127.0.0.1                                         *
 *  Description:Create single instance of a restaurant based on file input*
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/


namespace stDelivery.Kitchen
{

    /// <summary>
    /// The main RestaurantFactory class.
    /// Contains method for performing restaurant creation based on input file and retain this
    /// restaurant until user shutdown the app.
    /// </summary>
    public sealed class RestaurantFactory
    {
        private static RestaurantFactory _singleton = null;
        private Restaurant _restaurant;
        private string _content;

        public Restaurant Restaurant { get => _restaurant; set => _restaurant = value; }

        /// <summary>
        /// Init singleton
        /// </summary>
        /// <param name="fileName">To specify name of restaurant(json file) with specific foods.</param>
        /// <param name="asset">llows you to open and read raw files that have been bundled with the application as a simple stream of bytes.</param>
        private RestaurantFactory(string fileName, AssetManager asset)
        {
            try
            {
                using (StreamReader sr = new StreamReader(asset.Open(fileName)))
                {
                    _content = sr.ReadToEnd();
                    Restaurant = JsonConvert.DeserializeObject<Restaurant>(_content);
                }
            }
            catch (IOException ioe)
            {
                Log.Info("myapp", "Fail on read: " + ioe.Message);
            }
        }
        
        /// <summary>
        /// Get instance the singletone restaurant where you need
        /// </summary>
        /// <param name="fileName">To specify name of restaurant(json file) with specific foods.</param>
        /// <param name="asset">llows you to open and read raw files that have been bundled with the application as a simple stream of bytes.</param>
        public static RestaurantFactory GetInstance(AssetManager asset, string fileName)
        {
            if (_singleton == null)
            {
                if(asset == null && fileName == "")
                {
                    throw new Exception("Wrong file read.");
                }

                try
                {
                    _singleton = new RestaurantFactory(fileName, asset);
                } catch (IOException exp)
                {
                    Log.Info("myapp", "Fail on read: " + exp.Message);
                }
            }
            return _singleton;
        }
    }
}