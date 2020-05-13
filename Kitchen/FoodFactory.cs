using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using stDelivery.Kitchen;

/**************************************************************************
 *                                                                        *
 *  File:        FoodCategoryActivity.cs                                  *
 *  Copyright:   (c) 2020, Manastireanu Dany                              *
 *  E-mail:      andrei-dany.manastireanu@student.tuiasi.ro               *
 *  Website:     http://127.0.0.1                                         *
 *  Description: Class for return reference to food type based on user    *
 *               selected option.                                         *
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
    /// The main FoodFactory class.
    /// Contains method for performing return food reference based on user choice.
    /// </summary>
    class FoodFactory
    {
        private TypeOfFood _tf;
        private RestaurantFactory _factory;
        
        public FoodFactory(TypeOfFood tf)
        {
            this._tf = tf;
            this._factory = RestaurantFactory.GetInstance(null, "");
        }

        // Get a reference on food user choice.
        public IFood prepareFoods()
        {
            switch (this._tf)
            {
                case TypeOfFood.Pizza:
                    return this._factory.Restaurant.Pizza;

                case TypeOfFood.Hamburger:
                    return this._factory.Restaurant.Hamburger;

                case TypeOfFood.Ciorba:
                    return this._factory.Restaurant.Ciorba;

                case TypeOfFood.Paste:
                    return this._factory.Restaurant.Paste;

                case TypeOfFood.Salata:
                    return this._factory.Restaurant.Salata;

                case TypeOfFood.Desert:
                    return this._factory.Restaurant.Desert;

                default:
                    throw new Exception("This type of food doesn't exist!");
            }
        }
    }
}