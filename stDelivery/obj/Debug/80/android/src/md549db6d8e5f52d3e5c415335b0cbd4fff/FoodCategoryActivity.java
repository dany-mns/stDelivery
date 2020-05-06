package md549db6d8e5f52d3e5c415335b0cbd4fff;


public class FoodCategoryActivity
	extends android.support.v7.app.AppCompatActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("stDelivery.FoodCategoryActivity, stDelivery", FoodCategoryActivity.class, __md_methods);
	}


	public FoodCategoryActivity ()
	{
		super ();
		if (getClass () == FoodCategoryActivity.class)
			mono.android.TypeManager.Activate ("stDelivery.FoodCategoryActivity, stDelivery", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
