using Android.App;
using Android.OS;
using Android.Support.V7.App;

namespace StDelivery
{
    /// <summary>
    /// The main activity that handles all the activities. This class offers the context to handle all the elements (views).
    /// </summary>
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        /// <summary>
        /// Generic method for setting the current activity view. All the activities wil be built in a chain. 
        /// In this method, we instantiate the ShoppingCartActivity.
        /// </summary>
        /// <param name="savedInstanceState"></param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ShoppingCart);

            ShoppingCartActivity shoppingCartActivity = new ShoppingCartActivity(this);
        }
    }
}

