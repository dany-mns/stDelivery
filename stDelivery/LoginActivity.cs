using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using System.Security.Cryptography;

namespace stDelivery
{
    
    [Activity(Label = "stDelivery", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class LoginActivity : AppCompatActivity
    {
        
        Database db;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LoginLayout);

            Button loginButton = FindViewById<Button>(Resource.Id.LoginButton);
            loginButton.Click += LoginClick;

            Button registerButton = FindViewById<Button>(Resource.Id.ToRegisterButton);
            registerButton.Click += RegisterClick;

            
            db = Database.Instance();

            

            
            db.createDatabase();

            

           // db.InsertUserIntoTable(user);
            
            /*Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;*/
        }

        /*public override bool OnCreateOptionsMenu(IMenu menu)
       

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }*/

        private void RegisterClick(Object sender, EventArgs eventArgs)
        {
            Intent registerActivity = new Intent(this, typeof(RegisterActivity));
            StartActivity(registerActivity);
            
        }

        private void LoginClick(Object sender, EventArgs eventArgs)
        {
            try
            {
                EditText emailText = FindViewById<EditText>(Resource.Id.LogEmailText);
                EditText passText = FindViewById<EditText>(Resource.Id.LogPassText);
                User user = db.getCurrentUser(emailText.Text.ToString(), Crypt.SHA256hash(passText.Text.ToString()));
                if (user!=null){
                    GlobalVariables.currentUser = new Persoana(user);
                    View view = (View)sender;
                    Snackbar.Make(view, "Te-ai logat!", Snackbar.LengthLong)
                        .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
                    Intent restaurantActivity = new Intent(this, typeof(RestaurantActivity));
                    this.StartActivity(restaurantActivity);
                }
                else
                {
                    View view = (View)sender;
                    Snackbar.Make(view, "Email sau parola gresita!"+db.userNumber().ToString(), Snackbar.LengthLong)
                        .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
                }
                
            }
            catch (Exception e)
            {
                View view = (View)sender;
                Snackbar.Make(view, "Eroare", Snackbar.LengthLong)
                    .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
                Log.Info("Login Exception", e.Message);
            }
        }
	}

    
}

