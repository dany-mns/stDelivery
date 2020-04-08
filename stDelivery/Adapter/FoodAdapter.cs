using System;

using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;

namespace stDelivery.Adapter
{
    class FoodAdapter : RecyclerView.Adapter
    {
        public event EventHandler<FoodAdapterClickEventArgs> ItemClick;
        public event EventHandler<FoodAdapterClickEventArgs> ItemLongClick;
        string[] items;

        public FoodAdapter(string[] data)
        {
            items = data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup layout here
            View itemView = null;
            //var id = Resource.Layout.__YOUR_ITEM_HERE;
            //itemView = LayoutInflater.From(parent.Context).
            //       Inflate(id, parent, false);

            var vh = new FoodAdapterViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = items[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as FoodAdapterViewHolder;
            //holder.TextView.Text = items[position];
        }

        public override int ItemCount => items.Length;

        void OnClick(FoodAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(FoodAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class FoodAdapterViewHolder : RecyclerView.ViewHolder
    {
        //public TextView TextView { get; set; }


        public FoodAdapterViewHolder(View itemView, Action<FoodAdapterClickEventArgs> clickListener,
                            Action<FoodAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            //TextView = v;
            itemView.Click += (sender, e) => clickListener(new FoodAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new FoodAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class FoodAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}