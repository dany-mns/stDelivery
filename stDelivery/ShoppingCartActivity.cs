using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace stDelivery
{
    class ShoppingCartActivity : Activity
    {
        private FinishOrderActivity finishOrderActivity;

        public ShoppingCartActivity(AppCompatActivity _mainActivity) : base(_mainActivity)
        {
            Button finalizareComanda = _mainActivity.FindViewById<Button>(Resource.Id.finalizareComanda);
            LinearLayout shoppingCart = _mainActivity.FindViewById<LinearLayout>(Resource.Id.shoppingCart);
            ShoppingCart cart = this.randomCartGenerator();
            this.drawCart(_mainActivity, shoppingCart, cart);

            finalizareComanda.Click += (object sender, System.EventArgs e) =>
            {
                _mainActivity.SetContentView(Resource.Layout.FinishOrder);
                this.finishOrderActivity = new FinishOrderActivity(_mainActivity);
                this.finishOrderActivity.Cart = cart;
                this.finishOrderActivity.Price = this.pretComanda(cart);
            };
        }


        private ShoppingCart randomCartGenerator()
        {
            ShoppingCart cart = new ShoppingCart();
            cart.AdaugainCos = new Food("Pizza Margherita", 25);
            cart.AdaugainCos = new Food("Salata Chaesar", 21);
            cart.AdaugainCos = new Food("Sos Ketchup", 5);
            cart.AdaugainCos = new Food("Sos Tzatziki", 5);
            cart.AdaugainCos = new Food("Doza Cola 330ml", 4);
            cart.AdaugainCos = new Food("Ciorba de burta", 25);
            cart.AdaugainCos = new Food("Paste Carbonara", 30);
           
            return cart;
            
        }

        private LinearLayout setShoppingCartHeader(AppCompatActivity _mainActivity)
        {
            var layout = new LinearLayout(_mainActivity);
            layout.Orientation = Orientation.Horizontal;

            TextView nameView = new TextView(_mainActivity);
            nameView.Text = "Produs";
            nameView.SetTypeface(Typeface.Default, TypefaceStyle.BoldItalic);
            nameView.TextSize = 22;
            nameView.SetPadding(50, 0, 0, 0);

            TextView priceView = new TextView(_mainActivity);
            priceView.Text = "Pret";
            priceView.SetTypeface(Typeface.Default, TypefaceStyle.BoldItalic);
            priceView.TextSize = 22;
            priceView.SetPadding(250, 0, 0, 0);

            layout.AddView(nameView);
            layout.AddView(priceView);
            return layout;
        }

        private void drawCart(AppCompatActivity _mainActivity, LinearLayout shoppingCart, ShoppingCart cart)
        {
            shoppingCart.RemoveAllViews();
            shoppingCart.AddView(this.setShoppingCartHeader(_mainActivity));
            cart.CosCumparaturi.ForEach(produs => {
                var layout = new LinearLayout(_mainActivity);
                layout.Orientation = Orientation.Horizontal;

                TextView nameView = new TextView(_mainActivity);
                nameView.Text = produs.Name;
                nameView.SetTypeface(Typeface.Default, TypefaceStyle.BoldItalic);
                nameView.TextSize = 22;
                int padding = 175 - produs.Name.Length;
                nameView.SetPadding(0, 0, padding, 0);


                TextView priceView = new TextView(_mainActivity);
                priceView.Text = produs.Price.ToString() + " lei";
                priceView.SetTypeface(Typeface.Default, TypefaceStyle.BoldItalic);
                priceView.TextSize = 22;
                padding = 50 - produs.Price.ToString().Length;
                priceView.SetPadding(0, 0, padding, 0);


                Button button = new Button(_mainActivity);
                button.Text = "Eliminare";
                button.Click += delegate
                {
                    cart.EliminadinCos = produs;
                    this.drawCart(_mainActivity, shoppingCart, cart);
                };
                layout.AddView(nameView);
                layout.AddView(priceView);
                layout.AddView(button);
                shoppingCart.AddView(layout);
            });
            int pret = this.pretComanda(cart);
            TextView finalPrice = _mainActivity.FindViewById<TextView>(Resource.Id.finalPrice);
            this.replacePretFinal(pret, finalPrice);
            
            
        }

        private int pretComanda(ShoppingCart cart)
        {
            int pret = 0;
            cart.CosCumparaturi.ForEach(produs => pret += produs.Price);
            return pret;
        }

        private void replacePretFinal(int pret, TextView label)
        {
            String pattern = @"[0-9]+";
            label.Text = Regex.Replace(label.Text, pattern, pret.ToString());
        }

    }
}