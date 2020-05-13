﻿/**************************************************************************
 *                                                                        *
 *  File:        FinishOrderActivity.cs                                   *
 *  Copyright:   (c) 2020, Stratulat Stefan                               *
 *  E-mail:      stefanc.stratulat@gmail.com                              *
 *  Website:     -                                                        *
 *  Description: The FinishOrderActivity class that handles the logic of  *
 *				the last activity of the application, containing the      *
 *				form for finishing the shopping command,by sending an     *
 *				email to the client with the details.                     *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using stDelivery.Kitchen;

namespace stDelivery
{
    /// <summary>
    /// The FinishOrderActivity class that handles the logic of the last activity of the application.
    /// It contains a form about placing the command and the posibility to successfully send the command by sending an email to the client
    /// </summary>
    [Activity(Label = "FinishOrderActivity")]
    class FinishOrderActivity : Activity
    {
        /// <summary>
        /// The selected payment method modified from the graphical interface.
        /// </summary>
        private int _selectedPaymentMethod = 0;

        /// <summary>
        /// The Toast object display at the command send button press or in case of an error
        /// </summary>
        private Toast _toast;

        /// <summary>
        /// The Toast object text content
        /// </summary>
        StringBuilder _toastMessage = new StringBuilder();

        /// <summary>
        /// The class constructor that creates the object which hold the representation of each view, which set 
        /// the values for the Spinner (combobex representing the payment method and the auto complete of the form).
        /// It also handles the logic of the page by calling subsequenct functions and by adding callback functions to the objects.
        /// </summary>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FinishOrder);

            Spinner contactDataSelector = FindViewById<Spinner>(Resource.Id.contactSelector);
            string[] contactDataOptions = new string[] { "Introducere manuala", "Contul de logare" };
            this.SetSpinner(contactDataSelector, contactDataOptions);

            Spinner paymentMethod = FindViewById<Spinner>(Resource.Id.modalitatePlataClient);
            contactDataOptions = new string[] { "Cash", "Card", "Paypal", "Rinichi" };
            this.SetSpinner(paymentMethod, contactDataOptions);

            Dictionary<String, View> credentials = new Dictionary<string, View>();
            credentials.Add("numePersoana", FindViewById<EditText>(Resource.Id.numeClient));
            credentials.Add("telefonPersoana", FindViewById<EditText>(Resource.Id.telefonClient));
            credentials.Add("emailPersoana", FindViewById<EditText>(Resource.Id.emailClient));
            credentials.Add("stradaPersoana", FindViewById<EditText>(Resource.Id.stradaClient));
            credentials.Add("nrCasaPersoana", FindViewById<EditText>(Resource.Id.nrCasaClient));
            credentials.Add("nrBlocPersoana", FindViewById<EditText>(Resource.Id.blocClient));
            credentials.Add("scaraPersoana", FindViewById<EditText>(Resource.Id.scaraClient));
            credentials.Add("apartamentPersoana", FindViewById<EditText>(Resource.Id.apartamentClient));
            credentials.Add("orasPersoana", FindViewById<EditText>(Resource.Id.orasClient));
            credentials.Add("observatiiPersoana", FindViewById<EditText>(Resource.Id.observatiiClient));
            credentials.Add("metodaPlata", FindViewById<Spinner>(Resource.Id.modalitatePlataClient));
            credentials.Add("robotCheck", FindViewById<CheckBox>(Resource.Id.robotCheck));
            credentials.Add("termsCheck", FindViewById<CheckBox>(Resource.Id.termsCheck));

            TextView termsClick = FindViewById<TextView>(Resource.Id.termsClick);
            Button sendCommand = FindViewById<Button>(Resource.Id.sendCommand);

            termsClick.Click += delegate
            {
                _toastMessage.Clear();
                _toastMessage.Append("Aceasta aplicatie este doar un prototip.");
                _toastMessage.Append(" Va rugam sa nu va asteptati ca produsele selectate chiar sa soseasca!");
                _toastMessage.Append("Veti primi maxim un email cu comanda solicitata!");
                _toastMessage.Append("Toate datele introduse sunt confidentiale. Va multumim!\n\n");
                _toastMessage.Append("stDelivery 2020 ©");
                _toast = Toast.MakeText(this, _toastMessage.ToString(), ToastLength.Long);
                _toast.SetGravity(GravityFlags.Center, 0, 0);
                _toast.Show();
            };

            sendCommand.Click += (object sender, System.EventArgs e) =>
            {
                this.SendCommand(credentials);
            };

            contactDataSelector.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs args) =>
            {
                if (args.Position == 1)
                {
                    this.AutoComplete(credentials);
                }
            };

            paymentMethod.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs args) =>
            {
                this._selectedPaymentMethod = args.Position;
            };

        }

        /// <summary>
        /// This callback function is used at the selection of the autocomplete button in the Spinner of the graphical interface.
        /// It takes the "User" object fields and replaces the forms values.
        /// </summary>
        /// <param name="credentials">The dictionary that contains a key : value pair as a way to identify the form elements.</param>
        private void AutoComplete(Dictionary<String, View> credentials)
        {
            ((EditText)credentials["numePersoana"]).Text = GlobalVariables.currentUser.Client.Nume;
            ((EditText)credentials["telefonPersoana"]).Text = GlobalVariables.currentUser.Client.Telefon;
            ((EditText)credentials["emailPersoana"]).Text = GlobalVariables.currentUser.Client.Email;
            String[] address = GlobalVariables.currentUser.Client.Adresa.Split("|");
            ((EditText)credentials["stradaPersoana"]).Text = address[0];
            ((EditText)credentials["nrCasaPersoana"]).Text = address[1];
            ((EditText)credentials["nrBlocPersoana"]).Text = address[2];
            ((EditText)credentials["scaraPersoana"]).Text = address[3];
            ((EditText)credentials["apartamentPersoana"]).Text = address[4];
            ((EditText)credentials["orasPersoana"]).Text = address[5];
        }

        /// <summary>
        /// This function is used to verify the validity of the form fields inputs. It is necessary to have all the 
        /// fields completed and the Checkbox elements checked.
        /// </summary>
        /// <param name="credentials">The dictionary that contains a key : value pair as a way to identify the form elements.</param>
        /// <returns>The validity of the verification</returns>
        private bool StatusCredentials(Dictionary<String, View> credentials)
        {
            if (((CheckBox)credentials["robotCheck"]).Checked && ((CheckBox)credentials["termsCheck"]).Checked)
            {
                if (((EditText)credentials["numePersoana"]).Text == "")
                {
                    return false;
                }
                if (((EditText)credentials["telefonPersoana"]).Text == "")
                {
                    return false;
                }
                if (((EditText)credentials["emailPersoana"]).Text == "")
                {
                    return false;
                }
                if (((EditText)credentials["stradaPersoana"]).Text == "")
                {
                    return false;
                }
                if (((EditText)credentials["nrCasaPersoana"]).Text == "")
                {
                    return false;
                }
                if (((EditText)credentials["nrBlocPersoana"]).Text == "")
                {
                    return false;
                }
                if (((EditText)credentials["scaraPersoana"]).Text == "")
                {
                    return false;
                }
                if (((EditText)credentials["apartamentPersoana"]).Text == "")
                {
                    return false;
                }
                if (((EditText)credentials["orasPersoana"]).Text == "")
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// This callback function is used at the selecting of the send command button. It display a _toast message of the status 
        /// of the command. If the verification of the form is valid, it used a Email DLL to send an email to the client. If the 
        /// verification is not valid, it display an error message.
        /// </summary>
        /// <param name="mainActivity">The main activity context</param>
        /// <param name="credentials">The dictionary that contains a key : value pair as a way to identify the form elements.</param>
        private void SendCommand(Dictionary<String, View> credentials)
        {
            bool isStatusCheck = this.StatusCredentials(credentials);

            if (!isStatusCheck)
            {
                _toastMessage.Clear();
                _toastMessage.Append("Va rugam sa introduceti corect toate datele!\n\n");
                _toastMessage.Append("stDelivery 2020 ©");
                _toast = Toast.MakeText(this, _toastMessage.ToString(), ToastLength.Long);
                _toast.SetGravity(GravityFlags.Center, 0, 0);
                _toast.Show();
            }
            else
            {
                StringBuilder body = new StringBuilder();
                SMTP.Email email = new SMTP.Email();
                email.Credentials = new Tuple<string, string>("stdeliveryip", "proiectIP2020");
                email.Sender = "stdeliveryip@gmail.com";
                email.Receiver = ((EditText)credentials["emailPersoana"]).Text;
                email.Subject = "Comanda stDelivery";
                try
                {
                    email.Body = this.BuildEmailBody(credentials);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    _toastMessage.Clear();
                    _toastMessage.Append("Nu se poate construi corpul emailului!\n\n");
                    _toastMessage.Append("stDelivery 2020 ©");
                    _toast = Toast.MakeText(this, _toastMessage.ToString(), ToastLength.Long);
                    _toast.SetGravity(GravityFlags.Center, 0, 0);
                    _toast.Show();
                }

                if (email.SendEmail())
                {
                    _toastMessage.Clear();
                    _toastMessage.Append("Comanda a fost trimisa cu succes!\n");
                    _toastMessage.Append("Va rugam sa verificati adresa de email!\n\n");
                    _toastMessage.Append("Va multumim pentru comanda dumneavoastra!\n");
                    _toastMessage.Append("stDelivery 2020 ©");
                    _toast = Toast.MakeText(this, _toastMessage.ToString(), ToastLength.Long);
                    _toast.SetGravity(GravityFlags.Center, 0, 0);
                    _toast.Show();
                }
                else
                {
                    _toastMessage.Clear();
                    _toastMessage.Append("Eroare de trimitere a emailului!\n");
                    _toastMessage.Append("stDelivery 2020 ©");
                    _toast = Toast.MakeText(this, _toastMessage.ToString(), ToastLength.Long);
                    _toast.SetGravity(GravityFlags.Center, 0, 0);
                    _toast.Show();
                }
            }
        }

        /// <summary>
        /// This function set the Spinner elements possible values.
        /// </summary>
        /// <param name="spinner">The spinner element in the graphical interface</param>
        /// <param name="contactDataOptions">The values to be set</param>
        private void SetSpinner(Spinner spinner, String[] contactDataOptions)
        {
            ArrayAdapter<string> contactDataAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, contactDataOptions);
            contactDataAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = contactDataAdapter;
        }

        /// <summary>
        /// This function is used at the email sending and it build the email body by taking all the fields in the form and
        /// building a beautiful string.
        /// </summary>
        /// <param name="credentials">The dictionary that contains a key : value pair as a way to identify the form elements.</param>
        /// <returns>The string that will be the body of the email</returns>
        private String BuildEmailBody(Dictionary<String, View> credentials)
        {
            StringBuilder body = new StringBuilder();
            try
            {
                body.Append("Buna-ziua! \n Tocmai am receptionat comanda dumneavoastra!\n");
                body.Append("\n-----------------\n");
                body.Append("Ati comandat urmatorele produse : \n");
                GlobalVariables.currentUser.ShoppingCart.menuitem.ForEach(produs =>
                {
                    body.Append(" - " + produs.Name + " : " + produs.Price + " lei.\n");
                });
                body.Append("\n * Pret final : " + this.PriceCommand(GlobalVariables.currentUser.ShoppingCart) + " lei. \n");
                body.Append("\n-----------------\n");
                body.Append("Datele dumneavoastra de contact :\n");
                body.Append(" - Nume : " + ((EditText)credentials["numePersoana"]).Text + "\n");
                body.Append(" - Numar de telefon : " + ((EditText)credentials["telefonPersoana"]).Text + "\n");

                body.Append(" - Adresa : strada " + ((EditText)credentials["stradaPersoana"]).Text + ", ");
                body.Append("nr. " + ((EditText)credentials["nrCasaPersoana"]).Text + ", ");
                body.Append("bloc " + ((EditText)credentials["nrBlocPersoana"]).Text + ", ");
                body.Append("scara " + ((EditText)credentials["scaraPersoana"]).Text + ", ");
                body.Append("apartament " + ((EditText)credentials["apartamentPersoana"]).Text + ", ");
                body.Append("oras " + ((EditText)credentials["orasPersoana"]).Text + "\n");
                body.Append(" - Observatii : " + ((EditText)credentials["observatiiPersoana"]).Text + "\n");
                body.Append(" - Metoda de plata : " + ((Spinner)credentials["metodaPlata"]).GetItemAtPosition(this._selectedPaymentMethod) + "\n");
                body.Append("\n-----------------\n");

                body.Append("Va multumim si va mai asteptam!\n");
                body.Append("stDelivery 2020 ©\n");
                return body.ToString();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This function takes the shopping cart object and calculates the total price of the command
        /// </summary>
        /// <param name="cart">The shoppinh cart object containing the products that have a name and a price</param>
        /// <returns>The total price</returns>
        private int PriceCommand(IFood cart)
        {
            int price = 0;
            cart.menuitem.ForEach(product => price += product.Price);
            return price;
        }
    }
}