using System.Collections.Generic;

namespace StDelivery
{
    class ShoppingCart
    {
        private List<Food> _shoppingCart;
        public ShoppingCart()
        {
            this._shoppingCart = new List<Food>();
        }

        public Food AddToCart
        {
            set 
            {
                this._shoppingCart.Add(value);
            }
        }
        
        public List<Food> Cart
        {
            get 
            {
                return this._shoppingCart;
            }
        }

        public Food RemoveFromCart
        {
            set 
            { 
                this._shoppingCart.Remove(value);
            }
        }
    }
}