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
using stDelivery.FileWork;

namespace stDelivery.Kitchen
{
    class FoodFactory
    {
        private TypeOfFood _tf;
        private MyJsonFile _factory;

        public FoodFactory(TypeOfFood tf)
        {
            this._tf = tf;
            this._factory = MyJsonFile.GetInstance(null);
        }

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