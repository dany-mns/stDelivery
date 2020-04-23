using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using System.Security.Cryptography;

namespace stDelivery
{
    [Activity(Label = "Register")]
    public class RegisterActivity : Activity
    {
        Database db;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.RegisterLayout);

            db = new Database();

            Button regButton = FindViewById<Button>(Resource.Id.FinRegisterButton);
            regButton.Click += RegisterCl;

            Button backButton = FindViewById<Button>(Resource.Id.BackButton);
            backButton.Click += BackCl;
            
        }

        public void RegisterCl(Object sender, EventArgs eventArgs)
        {
            EditText LastNameText = FindViewById<EditText>(Resource.Id.LastNameText);
            EditText FirstNameText = FindViewById<EditText>(Resource.Id.FirstNameText);
            EditText EmailText = FindViewById<EditText>(Resource.Id.EmailText);
            EditText AdressText = FindViewById<EditText>(Resource.Id.AdressText);
            EditText AgeText = FindViewById<EditText>(Resource.Id.AgeText);
            EditText pass1Text = FindViewById<EditText>(Resource.Id.PassText1);
            EditText pass2Text = FindViewById<EditText>(Resource.Id.PassText2);

            if (pass1Text.Text.ToString() == pass2Text.Text.ToString())
            {


                if (db.emailExist(EmailText.Text.ToString()) == 0)
                {
                    User user = new User()
                    {
                        Nume = LastNameText.Text.ToString(),
                        Prenume = FirstNameText.Text.ToString(),
                        Email = EmailText.Text.ToString(),
                        Varsta = Int32.Parse(AgeText.Text.ToString()),
                        Adresa=AdressText.Text.ToString(),
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
        public void BackCl(Object sender, EventArgs eventArgs)
        {
            Intent loginActivity = new Intent(this, typeof(LoginActivity));
            StartActivity(loginActivity);
        }
    }
}