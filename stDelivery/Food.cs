using System;

namespace StDelivery
{
    public class Food
    {
        private String _name;
        private int _price;
        
        public Food(String name, int price)
        {
            this._name = name;
            this._price = price;
        }

        public String Name {
            get 
            {
                return this._name; 
            }
            set
            {
                this._name = value; 
            }
        }

        public int Price
        {
            get 
            { 
                return this._price;
            }
            set
            { 
                this._price = value;
            }
        }
    }
}