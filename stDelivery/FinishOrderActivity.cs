using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Org.Apache.Http.Cookies;

namespace stDelivery
{
    class FinishOrderActivity : Activity
    {
        private ShoppingCart shoopingCart;
        private int finalPrice;
        private int selectedPaymentMethod = 0;
        
        public ShoppingCart Cart
        {
            set { this.shoopingCart = value; }
            get { return this.shoopingCart;  }
        }
        public int Price
        {
            set { this.finalPrice = value; }
            get { return this.finalPrice; }
        }
        public FinishOrderActivity(AppCompatActivity _mainActivity) : base(_mainActivity)
        {
            Spinner contactDataSelector = _mainActivity.FindViewById<Spinner>(Resource.Id.contactSelector);
            string[] contactDataOptions = new string[]{ "Introducere manuala", "Contul de logare" };
            this.SetSpinner(_mainActivity, contactDataSelector, contactDataOptions);

            Spinner paymentMethod = _mainActivity.FindViewById<Spinner>(Resource.Id.modalitatePlataClient);
            contactDataOptions = new string[] { "Cash", "Card", "Paypal", "Rinichi" };
            this.SetSpinner(_mainActivity, paymentMethod, contactDataOptions);

            Dictionary<String, View> credentials = new Dictionary<string, View>();
            credentials.Add("numePersoana", _mainActivity.FindViewById<EditText>(Resource.Id.numeClient));
            credentials.Add("telefonPersoana", _mainActivity.FindViewById<EditText>(Resource.Id.telefonClient));
            credentials.Add("emailPersoana", _mainActivity.FindViewById<EditText>(Resource.Id.emailClient));
            credentials.Add("stradaPersoana", _mainActivity.FindViewById<EditText>(Resource.Id.stradaClient));
            credentials.Add("nrCasaPersoana", _mainActivity.FindViewById<EditText>(Resource.Id.nrCasaClient));
            credentials.Add("nrBlocPersoana", _mainActivity.FindViewById<EditText>(Resource.Id.blocClient));
            credentials.Add("scaraPersoana", _mainActivity.FindViewById<EditText>(Resource.Id.scaraClient));
            credentials.Add("apartamentPersoana", _mainActivity.FindViewById<EditText>(Resource.Id.apartamentClient));
            credentials.Add("orasPersoana", _mainActivity.FindViewById<EditText>(Resource.Id.orasClient));
            credentials.Add("observatiiPersoana", _mainActivity.FindViewById<EditText>(Resource.Id.observatiiClient));
            credentials.Add("metodaPlata", _mainActivity.FindViewById<Spinner>(Resource.Id.modalitatePlataClient));
            credentials.Add("robotCheck", _mainActivity.FindViewById<CheckBox>(Resource.Id.robotCheck));
            credentials.Add("termsCheck", _mainActivity.FindViewById<CheckBox>(Resource.Id.termsCheck));


            TextView termsClick = _mainActivity.FindViewById<TextView>(Resource.Id.termsClick);
            Button sendCommand = _mainActivity.FindViewById<Button>(Resource.Id.sendCommand);
            

            termsClick.Click += delegate{
                // trimitere catre pagina de termeni si conditii
                // vedem daca mai are sens asta
            };

            sendCommand.Click += (object sender, System.EventArgs e) => {
                this.SendCommand(_mainActivity, credentials);
            };

            contactDataSelector.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs args) =>
            {
                if(args.Position == 1) // auto complete cu datele userului curent
                {
                    this.AutoComplete(credentials);
                }
            };

            paymentMethod.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs args) =>
            {
                this.selectedPaymentMethod = args.Position;
            };

        }
        private void AutoComplete(Dictionary<String, View> credentials)
        {
            // extragere date user curent si completare acolo unde se cunosc datele - preluare date de la Claudiu
            ((EditText)credentials["numePersoana"]).Text = "Manastireanu Danny";
            ((EditText)credentials["telefonPersoana"]).Text = "0745454545";
            ((EditText)credentials["emailPersoana"]).Text = "danny.manastireanu@gmail.com";
            ((EditText)credentials["orasPersoana"]).Text = "Targu Frumos";
        }

        private bool StatusCredentials(Dictionary<String, View> credentials)
        {
            if (((CheckBox)credentials["robotCheck"]).Checked && ((CheckBox)credentials["termsCheck"]).Checked) {
                if (((EditText)credentials["numePersoana"]).Text == "")
                    return false;
                if (((EditText)credentials["telefonPersoana"]).Text == "")
                    return false;
                if (((EditText)credentials["emailPersoana"]).Text == "")
                    return false;
                if (((EditText)credentials["stradaPersoana"]).Text == "")
                    return false;
                if (((EditText)credentials["nrCasaPersoana"]).Text == "")
                    return false;
                if (((EditText)credentials["nrBlocPersoana"]).Text == "")
                    return false;
                if (((EditText)credentials["scaraPersoana"]).Text == "")
                    return false;
                if (((EditText)credentials["apartamentPersoana"]).Text == "")
                    return false;
                if (((EditText)credentials["orasPersoana"]).Text == "")
                    return false;
                return true;
            }
            return false;
        }

        private void SendCommand(AppCompatActivity _mainActivity, Dictionary<String, View> credentials)
        {
            bool statusCheck = this.StatusCredentials(credentials);
            Toast toast;
            StringBuilder toastMessage = new StringBuilder();
            if (!statusCheck)
            {
                toastMessage.Append("Va rugam sa introduceti corect toate datele!\n\n");
                toastMessage.Append("stDelivery 2020 ©");
                toast = Toast.MakeText(_mainActivity, toastMessage.ToString(), ToastLength.Long);
                toast.SetGravity(GravityFlags.Center,0,0);
                toast.Show();
            }
            else
            {
                StringBuilder body = new StringBuilder();
                
                SMTP.Email EMAIL = new SMTP.Email();
                EMAIL.Credentials = new Tuple<string, string>("stdeliveryip", "proiectIP2020");
                EMAIL.Sender = "stdeliveryip@gmail.com";
                EMAIL.Receiver = ((EditText)credentials["emailPersoana"]).Text;
                EMAIL.Subject = "Comanda stDelivery";
                EMAIL.Body = this.buildEmailBody(credentials);
                if (EMAIL.SendEmail())
                {
                    toastMessage.Append("Comanda a fost trimisa cu succes!\n");
                    toastMessage.Append("Va rugam sa verificati adresa de email!\n\n");
                    toastMessage.Append("Va multumim pentru comanda dumneavoastra!\n");
                    toastMessage.Append("stDelivery 2020 ©");
                    toast = Toast.MakeText(_mainActivity, toastMessage.ToString(), ToastLength.Long);
                    toast.SetGravity(GravityFlags.Center, 0, 0);
                    toast.Show();
                }
                else {
                    toastMessage.Append("Eroare de trimitere a emailului!\n");
                    toastMessage.Append("stDelivery 2020 ©");
                    toast = Toast.MakeText(_mainActivity, toastMessage.ToString(), ToastLength.Long);
                    toast.SetGravity(GravityFlags.Center, 0, 0);
                    toast.Show();
                }
            }
        }

        private void SetSpinner(AppCompatActivity _mainActivity, Spinner spinner, String[] contactDataOptions)
        {
            ArrayAdapter<string> contactDataAdapter = new ArrayAdapter<string>(_mainActivity, Android.Resource.Layout.SimpleSpinnerItem, contactDataOptions);
            contactDataAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = contactDataAdapter;
        }

        private String buildEmailBody(Dictionary<String, View> credentials)
        { 
            StringBuilder body = new StringBuilder();
            body.Append("Buna-ziua! \n Tocmai am receptionat comanda dumneavoastra!\n");
            body.Append("\n-----------------\n");
            body.Append("Ati comandat urmatorele produse : \n");
            this.Cart.CosCumparaturi.ForEach(produs =>
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
            body.Append(" - Metoda de plata : " + ((Spinner)credentials["metodaPlata"]).GetItemAtPosition(this.selectedPaymentMethod) + "\n") ;
            body.Append("\n-----------------\n");

            body.Append("Va multumim si va mai asteptam!\n");
            body.Append("stDelivery 2020 ©\n");
            return body.ToString();
        }
    }
}