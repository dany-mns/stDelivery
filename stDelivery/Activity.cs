using Android.Support.V7.App;

namespace StDelivery
{
    /// <summary>
    /// An abstract class, base for the concrete activities : ShoppingCartActivity and FinishOrderActivity
    /// </summary>
    abstract class Activity
    {
        /// <summary>
        /// The constructor of the abstract class, base for building the concrete classes
        /// </summary>
        /// <param name="mainActivity">The main activity context</param>
        public Activity(AppCompatActivity mainActivity) { }
    }
}