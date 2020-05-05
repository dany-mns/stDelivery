using System;
using System.Collections.Generic;
using System.Text;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace StDelivery
{
    /// <summary>
    /// The FinishOrderActivity class that handles the logic of the last activity of the application.
    /// It contains a form about placing the command and the posibility to successfully send the command by sending an email to the client
    /// </summary>
    class FinishOrderActivity : Activity
    {
        /// <summary>
        /// The shopping cart object from the previous activity.
        /// </summary>
        private ShoppingCart _shoppingCart;
        /// <summary>
        /// The final price of the command.
        /// </summary>
        private int _finalPrice;
        /// <summary>
        /// The selected payment method modified from the graphical interface.
        /// </summary>
        private int _selectedPaymentMethod = 0;
        
        /// <summary>
        /// The getter and setter that set or return the value of the shopping cart object.
        /// </summary>
        public ShoppingCart Cart
        {
            set
            { 
                this._shoppingCart = value; 
            }
            get 
            {
                return this._shoppingCart;
            }
        }
        /// <summary>
        /// The getter and setter that set or return the value of the final price object.
        /// </summary>
        public int Price
        {
            set
            { 
                this._finalPrice = value;
            }
            get
            { 
                return this._finalPrice;
            }
        }

        /// <summary>
        /// The class constructor that creates the object which hold the representation of each view, which set 
        /// the values for the Spinner (combobex representing the payment method and the auto complete of the form).
        /// It also handles the logic of the page by calling subsequenct functions and by adding callback functions to the objects.
        /// </summary>
        /// <param name="mainActivity">The main activity context.</param>
        public FinishOrderActivity(AppCompatActivity mainActivity) : base(mainActivity)
        {
            Spinner contactDataSelector = mainActivity.FindViewById<Spinner>(Resource.Id.contactSelector);
            string[] contactDataOptions = new string[]{ "Introducere manuala", "Contul de logare" };
            this.SetSpinner(mainActivity, contactDataSelector, contactDataOptions);

            Spinner paymentMethod = mainActivity.FindViewById<Spinner>(Resource.Id.modalitatePlataClient);
            contactDataOptions = new string[] { "Cash", "Card", "Paypal", "Rinichi" };
            this.SetSpinner(mainActivity, paymentMethod, contactDataOptions);

            Dictionary<String, View> credentials = new Dictionary<string, View>();
            credentials.Add("numePersoana", mainActivity.FindViewById<EditText>(Resource.Id.numeClient));
            credentials.Add("telefonPersoana", mainActivity.FindViewById<EditText>(Resource.Id.telefonClient));
            credentials.Add("emailPersoana", mainActivity.FindViewById<EditText>(Resource.Id.emailClient));
            credentials.Add("stradaPersoana", mainActivity.FindViewById<EditText>(Resource.Id.stradaClient));
            credentials.Add("nrCasaPersoana", mainActivity.FindViewById<EditText>(Resource.Id.nrCasaClient));
            credentials.Add("nrBlocPersoana", mainActivity.FindViewById<EditText>(Resource.Id.blocClient));
            credentials.Add("scaraPersoana", mainActivity.FindViewById<EditText>(Resource.Id.scaraClient));
            credentials.Add("apartamentPersoana", mainActivity.FindViewById<EditText>(Resource.Id.apartamentClient));
            credentials.Add("orasPersoana", mainActivity.FindViewById<EditText>(Resource.Id.orasClient));
            credentials.Add("observatiiPersoana", mainActivity.FindViewById<EditText>(Resource.Id.observatiiClient));
            credentials.Add("metodaPlata", mainActivity.FindViewById<Spinner>(Resource.Id.modalitatePlataClient));
            credentials.Add("robotCheck", mainActivity.FindViewById<CheckBox>(Resource.Id.robotCheck));
            credentials.Add("termsCheck", mainActivity.FindViewById<CheckBox>(Resource.Id.termsCheck));

            TextView termsClick = mainActivity.FindViewById<TextView>(Resource.Id.termsClick);
            Button sendCommand = mainActivity.FindViewById<Button>(Resource.Id.sendCommand);
            
            termsClick.Click += delegate
            {
                // trimitere catre pagina de termeni si conditii
                // vedem daca mai are sens asta
            };

            sendCommand.Click += (object sender, System.EventArgs e) => 
            {
                this.SendCommand(mainActivity, credentials);
            };

            contactDataSelector.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs args) =>
            {
                if(args.Position == 1)
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
            ((EditText)credentials["numePersoana"]).Text = "Manastireanu Danny";
            ((EditText)credentials["telefonPersoana"]).Text = "0745454545";
            ((EditText)credentials["emailPersoana"]).Text = "danny.manastireanu@gmail.com";
            ((EditText)credentials["orasPersoana"]).Text = "Targu Frumos";
        }

        /// <summary>
        /// This function is used to verify the validity of the form fields inputs. It is necessary to have all the 
        /// fields completed and the Checkbox elements checked.
        /// </summary>
        /// <param name="credentials">The dictionary that contains a key : value pair as a way to identify the form elements.</param>
        /// <returns>The validity of the verification</returns>
        private bool StatusCredentials(Dictionary<String, View> credentials)
        {
            if (((CheckBox)credentials["robotCheck"]).Checked && ((CheckBox)credentials["termsCheck"]).Checked) {
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
        /// This callback function is used at the selecting of the send command button. It display a Toast message of the status 
        /// of the command. If the verification of the form is valid, it used a Email DLL to send an email to the client. If the 
        /// verification is not valid, it display an error message.
        /// </summary>
        /// <param name="mainActivity">The main activity context</param>
        /// <param name="credentials">The dictionary that contains a key : value pair as a way to identify the form elements.</param>
        private void SendCommand(AppCompatActivity mainActivity, Dictionary<String, View> credentials)
        {
            bool isStatusCheck = this.StatusCredentials(credentials);
            Toast toast;
            StringBuilder toastMessage = new StringBuilder();
            if (!isStatusCheck)
            {
                toastMessage.Append("Va rugam sa introduceti corect toate datele!\n\n");
                toastMessage.Append("stDelivery 2020 ©");
                toast = Toast.MakeText(mainActivity, toastMessage.ToString(), ToastLength.Long);
                toast.SetGravity(GravityFlags.Center,0,0);
                toast.Show();
            }
            else
            {
                StringBuilder body = new StringBuilder();
                SMTP.Email email = new SMTP.Email();
                email.Credentials = new Tuple<string, string>("stdeliveryip", "proiectIP2020");
                email.Sender = "stdeliveryip@gmail.com";
                email.Receiver = ((EditText)credentials["emailPersoana"]).Text;
                email.Subject = "Comanda stDelivery";
                email.Body = this.BuildEmailBody(credentials);
                if (email.SendEmail())
                {
                    toastMessage.Append("Comanda a fost trimisa cu succes!\n");
                    toastMessage.Append("Va rugam sa verificati adresa de email!\n\n");
                    toastMessage.Append("Va multumim pentru comanda dumneavoastra!\n");
                    toastMessage.Append("stDelivery 2020 ©");
                    toast = Toast.MakeText(mainActivity, toastMessage.ToString(), ToastLength.Long);
                    toast.SetGravity(GravityFlags.Center, 0, 0);
                    toast.Show();
                }
                else 
                {
                    toastMessage.Append("Eroare de trimitere a emailului!\n");
                    toastMessage.Append("stDelivery 2020 ©");
                    toast = Toast.MakeText(mainActivity, toastMessage.ToString(), ToastLength.Long);
                    toast.SetGravity(GravityFlags.Center, 0, 0);
                    toast.Show();
                }
            }
        }

        /// <summary>
        /// This function set the Spinner elements possible values.
        /// </summary>
        /// <param name="mainActivity">The main activity context</param>
        /// <param name="spinner">The spinner element in the graphical interface</param>
        /// <param name="contactDataOptions">The values to be set</param>
        private void SetSpinner(AppCompatActivity mainActivity, Spinner spinner, String[] contactDataOptions)
        {
            ArrayAdapter<string> contactDataAdapter = new ArrayAdapter<string>(mainActivity, Android.Resource.Layout.SimpleSpinnerItem, contactDataOptions);
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
            body.Append("Buna-ziua! \n Tocmai am receptionat comanda dumneavoastra!\n");
            body.Append("\n-----------------\n");
            body.Append("Ati comandat urmatorele produse : \n");
            this.Cart.Cart.ForEach(produs =>
            {
                body.Append(" - " + produs.Name + " : " + produs.Price + " lei.\n");
            });
            body.Append("\n * Pret final : " + this.Price + " lei. \n");
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
            body.Append(" - Metoda de plata : " + ((Spinner)credentials["metodaPlata"]).GetItemAtPosition(this._selectedPaymentMethod) + "\n") ;
            body.Append("\n-----------------\n");

            body.Append("Va multumim si va mai asteptam!\n");
            body.Append("stDelivery 2020 ©\n");
            return body.ToString();
        }
    }
}