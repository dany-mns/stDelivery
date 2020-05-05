using System;
using System.Text.RegularExpressions;
using Android.Graphics;
using Android.Support.V7.App;
using Android.Widget;

namespace StDelivery
{
    /// <summary>
    /// This class implements the logic of building the shopping cart.
    /// It also provides methods for deleting products from the shopping cart.
    /// </summary>
    class ShoppingCartActivity : Activity
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
            ShoppingCart cart = this.RandomCartGenerator();
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
        /// This functions generates a random shopping cart object. It not be used in the final project because 
        /// the shooping cart list will be generated in a previous activity
        /// </summary>
        /// <returns>shopping cart object</returns>
        private ShoppingCart RandomCartGenerator()
        {
            ShoppingCart cart = new ShoppingCart();
            cart.AddToCart = new Food("Pizza Margherita", 25);
            cart.AddToCart = new Food("Salata Chaesar", 21);
            cart.AddToCart = new Food("Sos Ketchup", 5);
            cart.AddToCart = new Food("Sos Tzatziki", 5);
            cart.AddToCart = new Food("Doza Cola 330ml", 4);
            cart.AddToCart = new Food("Ciorba de burta", 25);
            cart.AddToCart = new Food("Paste Carbonara", 30);
     
            return cart;
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
        private void DrawCart(AppCompatActivity mainActivity, LinearLayout shoppingCart, ShoppingCart cart)
        {
            shoppingCart.RemoveAllViews();
            shoppingCart.AddView(this.SetShoppingCartHeader(mainActivity));
            cart.Cart.ForEach(product => {
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
                    cart.RemoveFromCart = product;
                    this.DrawCart(mainActivity, shoppingCart, cart);
                };
                layout.AddView(nameView);
                layout.AddView(priceView);
                layout.AddView(button);
                shoppingCart.AddView(layout);
            });
            int price = this.PriceCommand(cart);
            TextView finalPrice = mainActivity.FindViewById<TextView>(Resource.Id.finalPrice);
            this.ReplaceFinalPrice(price, finalPrice);
        }

        /// <summary>
        /// This function takes the shopping cart object and calculates the total price of the command
        /// </summary>
        /// <param name="cart">The shoppinh cart object containing the products that have a name and a price</param>
        /// <returns>The total price</returns>
        private int PriceCommand(ShoppingCart cart)
        {
            int price = 0;
            cart.Cart.ForEach(product => price += product.Price);
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
            label.Text = Regex.Replace(label.Text, pattern, price.ToString());
        }
    }
}