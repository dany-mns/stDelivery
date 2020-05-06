/**************************************************************************
 *                                                                        *
 *  File:        ShoppingCartActivity.cs                                  *
 *  Copyright:   (c) 2020, Stratulat Stefan                               *
 *  E-mail:      stefanc.stratulat@gmail.com                              *
 *  Website:     -                                                        *
 *  Description: This class implements the logic of building the shopping *
 *              cart and provides methods for deleting product from       *
 *              the shopping cart.                                        *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/


using System;
using System.Text.RegularExpressions;
using Android.Graphics;
using Android.Support.V7.App;
using Android.Widget;
using stDelivery.Kitchen;

namespace stDelivery
{
    /// <summary>
    /// This class implements the logic of building the shopping cart.
    /// It also provides methods for deleting products from the shopping cart.
    /// </summary>
    class ShoppingCartActivity : StDelivery.Activity
    {
        /// <summary>
        /// A FinishOrderActivity object instantiated at the "Finalizeaza comanda" button.
        /// </summary>
        private FinishOrderActivity _finishOrderActivity;


        /// <summary>
        /// The class constructor that handles the logic of the shopping cart activity.
        /// It defines the using of the main components of the activity and also add a callback function to the button
        /// </summary>
        /// <param name="mainActivity"></param>
        public ShoppingCartActivity(AppCompatActivity mainActivity) : base(mainActivity)
        {
            Button finishCommand = mainActivity.FindViewById<Button>(Resource.Id.finalizareComanda);
            LinearLayout shoppingCart = mainActivity.FindViewById<LinearLayout>(Resource.Id.shoppingCart);
            IFood cart = GlobalVariables.currentUser.ShoppingCart;
            this.DrawCart(mainActivity, shoppingCart, cart);

            finishCommand.Click += (object sender, System.EventArgs e) =>
            {
                mainActivity.SetContentView(Resource.Layout.FinishOrder);
                this._finishOrderActivity = new FinishOrderActivity(mainActivity);
                this._finishOrderActivity.Cart = cart;
                this._finishOrderActivity.Price = this.PriceCommand(cart);
            };
        }

        /// <summary>
        /// This function sets the shopping cart list header.
        /// It creates two TextView elements and put into a LinearLayout.
        /// </summary>
        /// <param name="mainActivity">The main activity context</param>
        /// <returns>The layout containing the elements that will be added as the first element in the shopping cart</returns>
        private LinearLayout SetShoppingCartHeader(AppCompatActivity mainActivity)
        {
            var layout = new LinearLayout(mainActivity);
            layout.Orientation = Orientation.Horizontal;

            TextView nameView = new TextView(mainActivity);
            nameView.Text = "Produs";
            nameView.SetTypeface(Typeface.Default, TypefaceStyle.BoldItalic);
            nameView.TextSize = 22;
            nameView.SetPadding(50, 0, 0, 0);

            TextView priceView = new TextView(mainActivity);
            priceView.Text = "Pret";
            priceView.SetTypeface(Typeface.Default, TypefaceStyle.BoldItalic);
            priceView.TextSize = 22;
            priceView.SetPadding(250, 0, 0, 0);

            layout.AddView(nameView);
            layout.AddView(priceView);

            return layout;
        }

        /// <summary>
        /// This function creates the shopping cart list. As the first element, it has the header previously created.
        /// Then, each element from the shopping cart object in used to generate a label that will be added in the list.
        /// There is implemented the posibility of deleting a object from the shopping list.
        /// It also handles the view that contains the information about the final price.
        /// </summary>
        /// <param name="mainActivity">The main activity context</param>
        /// <param name="shoppingCart">The layout in the graphical interface that will contain the cart list</param>
        /// <param name="cart">The shopping cart object which contains the products to be added in the list</param>
        private void DrawCart(AppCompatActivity mainActivity, LinearLayout shoppingCart, IFood cart)
        {
            shoppingCart.RemoveAllViews();
            shoppingCart.AddView(this.SetShoppingCartHeader(mainActivity));
            try
            {
                cart.menuitem.ForEach(product =>
                {
                    var layout = new LinearLayout(mainActivity);
                    layout.Orientation = Orientation.Horizontal;

                    TextView nameView = new TextView(mainActivity);
                    nameView.Text = product.Name;
                    nameView.SetTypeface(Typeface.Default, TypefaceStyle.BoldItalic);
                    nameView.TextSize = 22;
                    int padding = 175 - product.Name.Length;
                    nameView.SetPadding(0, 0, padding, 0);

                    TextView priceView = new TextView(mainActivity);
                    priceView.Text = product.Price.ToString() + " lei";
                    priceView.SetTypeface(Typeface.Default, TypefaceStyle.BoldItalic);
                    priceView.TextSize = 22;
                    padding = 50 - product.Price.ToString().Length;
                    priceView.SetPadding(0, 0, padding, 0);

                    Button button = new Button(mainActivity);
                    button.Text = "Eliminare";
                    button.Click += delegate
                    {
                        cart.menuitem.Remove(product);
                        this.DrawCart(mainActivity, shoppingCart, cart);
                    };
                    layout.AddView(nameView);
                    layout.AddView(priceView);
                    layout.AddView(button);
                    shoppingCart.AddView(layout);
                });
            }
            catch(ArgumentNullException ex)
            {
                Toast.MakeText(mainActivity,
                    "ArgumentNullException! The shopping cart object is empty!\nstDelivery 2020 ©",
                    ToastLength.Long).Show();
            }
            catch(InvalidOperationException ex)
            {
                Toast.MakeText(mainActivity,
                    "InvalidOperationException! Could not operate with shopping cart object!\nstDelivery 2020 ©",
                    ToastLength.Long).Show();
            }
            int price = this.PriceCommand(cart);
            TextView finalPrice = mainActivity.FindViewById<TextView>(Resource.Id.finalPrice);
            try
            {
                this.ReplaceFinalPrice(price, finalPrice);
            }
            catch (ArgumentException ex)
            {
                Toast.MakeText(mainActivity,
                    "ArgumentException! Invalid object given to Regex!\nstDelivery 2020 ©",
                    ToastLength.Long).Show();
            }
            catch (RegexMatchTimeoutException ex)
            {
                Toast.MakeText(mainActivity,
                    "RegexMatchTimeoutException! Could not finish Regex Replace operation!\nstDelivery 2020 ©",
                    ToastLength.Long).Show();
            }

        }

        /// <summary>
        /// This function takes the shopping cart object and calculates the total price of the command
        /// </summary>
        /// <param name="cart">The shoppinh cart object containing the products that have a name and a price</param>
        /// <returns>The total price</returns>
        private int PriceCommand(IFood cart)
        {
            int price = 0;
            cart.menuitem.ForEach(product => price += product.Price);
            return price;
        }

        /// <summary>
        /// This function updates the graphical interface view that represents the final price.
        /// </summary>
        /// <param name="price">The total command price calculated</param>
        /// <param name="label">The view that contains the information about the price/</param>
        private void ReplaceFinalPrice(int price, TextView label)
        {
            String pattern = @"[0-9]+";
            try
            {
                label.Text = Regex.Replace(label.Text, pattern, price.ToString());
            }
            catch(ArgumentException ex)
            {
                throw ex;
            }
            catch (RegexMatchTimeoutException ex)
            {
                throw ex;
            }
        }
    }
}