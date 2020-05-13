using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using DatabaseStDeliveryLibrary;
using System;

/**************************************************************************
 *                                                                        *
 *  File:        RegisterActivity.cs                                      *
 *  Copyright:   (c) 2020, Zalinca Claudiu                                *
 *  E-mail:      claudiu-serban.zalinca@student.tuiasi.ro                 *
 *  Website:     http://127.0.0.1                                         *
 *  Description: The activity for register a new account                  *
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
    /// RegisterActivity class provides the logic for register activity, creating a valid account and store it in the database
    /// </summary>
    [Activity(Label = "Register")]
    public class RegisterActivity : Activity
    {
        Database db;
        /// <summary>
        /// The function which is called when the RegisterActivity starts
        /// </summary>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.RegisterLayout);

            db = Database.Instance();

            Button regButton = FindViewById<Button>(Resource.Id.FinRegisterButton);
            regButton.Click += RegisterCl;

            Button backButton = FindViewById<Button>(Resource.Id.BackButton);
            backButton.Click += BackCl;

        }

        /// <summary>
        /// Event of regiser click
        /// If the datas are valid, it creates an account and store it in the database
        /// If the account is created successfully then the user is moved to the login activity to log in in the application
        /// </summary>
        public void RegisterCl(Object sender, EventArgs eventArgs)
        {
            EditText NameText = FindViewById<EditText>(Resource.Id.NameText);

            EditText EmailText = FindViewById<EditText>(Resource.Id.EmailText);

            EditText PhoneText = FindViewById<EditText>(Resource.Id.PhoneText);
            EditText pass1Text = FindViewById<EditText>(Resource.Id.PassText1);
            EditText pass2Text = FindViewById<EditText>(Resource.Id.PassText2);

            EditText StreetText = FindViewById<EditText>(Resource.Id.StreetText);
            EditText StreetNumberText = FindViewById<EditText>(Resource.Id.StreetNumberText);
            EditText BlText = FindViewById<EditText>(Resource.Id.BlText);
            EditText ScText = FindViewById<EditText>(Resource.Id.ScText);
            EditText ApText = FindViewById<EditText>(Resource.Id.ApText);
            EditText CityText = FindViewById<EditText>(Resource.Id.CityText);
            String AdressText = StreetText.Text.ToString() + "|" + StreetNumberText.Text.ToString() + "|" + BlText.Text.ToString() + "|"
                + ScText.Text.ToString() + "|" + ApText.Text.ToString() + "|" + CityText.Text.ToString();
            if (pass1Text.Text.ToString() == pass2Text.Text.ToString())
            {


                if (db.emailExist(EmailText.Text.ToString()) == 0)
                {
                    User user = new User()
                    {
                        Nume = NameText.Text.ToString(),
                        Email = EmailText.Text.ToString(),
                        Telefon = PhoneText.Text.ToString(),
                        Adresa = AdressText,
                        Parola = Crypt.SHA256hash(pass1Text.Text.ToString())
                    };

                    db.InsertUserIntoTable(user);

                    Intent loginActivity = new Intent(this, typeof(LoginActivity));
                    StartActivity(loginActivity);


                }
                else
                {
                    View view = (View)sender;
                    Snackbar.Make(view, "Un cont cu emailul introdus exista!" + db.userNumber().ToString(), Snackbar.LengthLong)
                        .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
                }
            }
            else
            {
                View view = (View)sender;
                Snackbar.Make(view, "Parola introdusa nu corespunde!" + db.userNumber().ToString(), Snackbar.LengthLong)
                    .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();

            }


        }

        /// <summary>
        /// Back button event click it moves the user back to the login without creating an account
        /// </summary>
        public void BackCl(Object sender, EventArgs eventArgs)
        {
            Intent loginActivity = new Intent(this, typeof(LoginActivity));
            StartActivity(loginActivity);
        }
    }
}