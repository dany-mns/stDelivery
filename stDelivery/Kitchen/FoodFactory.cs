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

namespace stDelivery.Kitchen
{
    class FoodFactory
    {
        public IFood prepareFood(TypeOfFood tf, string name, string description, int price)
        {
            switch (tf)
            {
                case TypeOfFood.Pizza:
                    return new Pizza(name, description, price);
                    
                case TypeOfFood.Hamburger:
                    return new Hamburger(name, description, price);
                    
                case TypeOfFood.Desert:
                    return new Desert(name, description, price);
                    
                default:
                    throw new Exception("This type of food doesn't exist!");
            }
        }
    }
}