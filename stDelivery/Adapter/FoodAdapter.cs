using System;

using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using stDelivery.Kitchen;
using Android.Util;

namespace stDelivery.Adapter
{
    class FoodAdapter : RecyclerView.Adapter
    {
        /*
         * Event handler for different action.
         * Foods member who will populate the screen.
         */
        public event EventHandler<FoodAdapterClickEventArgs> ItemClick;
        public event EventHandler<FoodAdapterClickEventArgs> ItemLongClick;
        public event EventHandler<FoodAdapterClickEventArgs> ItemBuyClick;

        public IFood foods;

        public FoodAdapter(IFood data)
        {
            foods = data;
        }


        /// <summary>
        /// Create new views (invoked by the layout manager)
        /// </summary>
        /// See <see cref="View"/> to get more information about android views.
        /// <param name="parent">Get main context.</param>
        /// <param name="viewType">Unused.</param>
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup layout here
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.UsesForFood, parent, false);

            var vh = new FoodAdapterViewHolder(itemView, OnClick, OnLongClick, OnBuyFoodClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = foods.menuitem[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as FoodAdapterViewHolder;
            holder.foodNameText.Text = item.name;
            holder.foodDescpriptionText.Text = item.description;
            holder.foodPriceText.Text = item.price.ToString() + " LEI";
        }

        //Specify number of element on the screen
        public override int ItemCount => foods.menuitem.Count;

        void OnClick(FoodAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(FoodAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);
        void OnBuyFoodClick(FoodAdapterClickEventArgs args) => ItemBuyClick?.Invoke(this, args);

    }

    /// <summary>
    /// In principle is for populate entire screen after user choices with a type of food.
    /// </summary>
    public class FoodAdapterViewHolder : RecyclerView.ViewHolder
    {
        public TextView foodNameText { get; set; }
        public TextView foodDescpriptionText { get; set; }
        public TextView foodPriceText { get; set; }
        public ImageView addFoodButton { get; set; }

        public FoodAdapterViewHolder(View itemView, Action<FoodAdapterClickEventArgs> clickListener,
                            Action<FoodAdapterClickEventArgs> longClickListener, Action<FoodAdapterClickEventArgs> buyClickListener) : base(itemView)
        {
            foodNameText = (TextView)itemView.FindViewById(Resource.Id.textFoodName);
            foodDescpriptionText = (TextView)itemView.FindViewById(Resource.Id.textFoodDescription);
            foodPriceText = (TextView)itemView.FindViewById(Resource.Id.textFoodPrice);
            addFoodButton = (ImageView)itemView.FindViewById(Resource.Id.btnAddFood);

            itemView.Click += (sender, e) => clickListener(new FoodAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new FoodAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            addFoodButton.Click += (sender, e) => buyClickListener(new FoodAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class FoodAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}