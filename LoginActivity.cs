using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;

/**************************************************************************
 *                                                                        *
 *  File:        LoginActivity.cs                                         *
 *  Copyright:   (c) 2020, Zalinca Claudiu                                *
 *  E-mail:      claudiu-serban.zalinca@student.tuiasi.ro                 *
 *  Website:     http://127.0.0.1                                         *
 *  Description: The activity for login                                   *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

namespace stDelivery
{
    /// <summary>
    /// LoginActivity class handles the login activity 
    /// Provides logic to login in application using email and password and also in case the user doesn't have an account he can creates one 
    /// </summary>

    [Activity(Label = "stDelivery", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class LoginActivity : AppCompatActivity
    {
        
        Database db;
        /// <summary>
        /// The function which is called when the LoginActivity starts
        /// </summary>
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

            

        }

        /// <summary>
        /// Event of click on register button
        /// Move to Register Activity
        /// </summary>
        private void RegisterClick(Object sender, EventArgs eventArgs)
        {
            Intent registerActivity = new Intent(this, typeof(RegisterActivity));
            StartActivity(registerActivity);
            
        }

        /// <summary>
        /// Event of click on login button
        /// Check the informations provided by the user in the database and log the user in if he has an account
        /// Also, if the user has a valid account is moved to the next activity to use the service
        /// </summary>
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

