package crc645c1daafe492a226f;


public class FoodAdapterViewHolder
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("stDelivery.Adapter.FoodAdapterViewHolder, stDelivery", FoodAdapterViewHolder.class, __md_methods);
	}


	public FoodAdapterViewHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == FoodAdapterViewHolder.class)
			mono.android.TypeManager.Activate ("stDelivery.Adapter.FoodAdapterViewHolder, stDelivery", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
