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

namespace stDelivery
{
    class FinishOrderActivity : Activity
    {
        public FinishOrderActivity(AppCompatActivity _mainActivity) : base(_mainActivity)
        {
            Spinner contactDataSelector = _mainActivity.FindViewById<Spinner>(Resource.Id.contactSelector);
            string[] contactDataOptions = new string[]{ "Introducere manuala", "Contul de logare" };
            ArrayAdapter<string> contactDataAdapter = new ArrayAdapter<string>(_mainActivity, Android.Resource.Layout.SimpleSpinnerItem, contactDataOptions);
            contactDataAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            contactDataSelector.Adapter = contactDataAdapter;

            Spinner paymentMethod = _mainActivity.FindViewById<Spinner>(Resource.Id.modalitatePlataClient);
            contactDataOptions = new string[] { "Cash", "Card", "Paypal", "Rinichi" };
            contactDataAdapter = new ArrayAdapter<string>(_mainActivity, Android.Resource.Layout.SimpleSpinnerItem, contactDataOptions);
            contactDataAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            paymentMethod.Adapter = contactDataAdapter;

            Dictionary<String, Object> credentials = new Dictionary<string, Object>();
            credentials.Add("numePersoana", _mainActivity.FindViewById<EditText>(Resource.Id.numeClient));
            credentials.Add("telefonPersoana", _mainActivity.FindViewById<EditText>(Resource.Id.telefonClient));
            credentials.Add("emailPersoana", _mainActivity.FindViewById<EditText>(Resource.Id.emailClient));
            credentials.Add("stradaPersoana", _mainActivity.FindViewById<EditText>(Resource.Id.telefonClient));
            credentials.Add("nrCasaPersoana", _mainActivity.FindViewById<EditText>(Resource.Id.nrCasaClient));
            credentials.Add("nrBlocPersoana", _mainActivity.FindViewById<EditText>(Resource.Id.blocClient));
            credentials.Add("apartamentPersoana", _mainActivity.FindViewById<EditText>(Resource.Id.apartamentClient));
            credentials.Add("orasPersoana", _mainActivity.FindViewById<EditText>(Resource.Id.orasClient));
            credentials.Add("observatiiPersoana", _mainActivity.FindViewById<EditText>(Resource.Id.observatiiClient));
            credentials.Add("robotCheck", _mainActivity.FindViewById<CheckBox>(Resource.Id.robotCheck));
            credentials.Add("termsCheck", _mainActivity.FindViewById<CheckBox>(Resource.Id.termsCheck));

          //  CheckBox robotCheck = _mainActivity.FindViewById<CheckBox>(Resource.Id.robotCheck);
           // CheckBox termsCheck = _mainActivity.FindViewById<CheckBox>(Resource.Id.termsCheck);
            TextView termsClick = _mainActivity.FindViewById<TextView>(Resource.Id.termsClick);
            Button sendCommand = _mainActivity.FindViewById<Button>(Resource.Id.sendCommand);
            

            termsClick.Click += delegate{
                // trimitere catre pagina de termeni si conditii
            };

            sendCommand.Click += (object sender, System.EventArgs e) => {
                bool statusCheck = this.StatusCredentials(credentials);
                if(statusCheck == true)
                {
                    this.SendCommand(credentials);
                }
            };

            contactDataSelector.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs args) =>
            {
                if(args.Position == 1) // auto complete cu datele userului curent
                {
                    this.AutoComplete(credentials);
                }
            };

        }
        private void AutoComplete(Dictionary<String, Object> credentials)
        {
            // extragere date user curent si completare acolo unde se cunosc datele
            ((EditText)credentials["numePersoana"]).Text = "Manastireanu Danny";
            ((EditText)credentials["telefonPersoana"]).Text = "0745454545";
            ((EditText)credentials["emailPersoana"]).Text = "danny.manastireanu@gmail.com";
            ((EditText)credentials["orasPersoana"]).Text = "Targu Frumos";
        }

        private bool StatusCredentials(Dictionary<String, Object> credentials)
        {
            // verificare checkbox-uri
            if (((CheckBox)credentials["robotCheck"]).Checked && ((CheckBox)credentials["termsCheck"]).Checked) {
                //verificare entry-uri
                return true;
            }
            return false;
        }

        private void SendCommand(Dictionary<String, Object> credentials)
        {
            // trimitere email si daca nu se genereaza vreo exceptie sau vreo eroare, se trece la pagina de final
            // (incapsulare trimitere email in dll)
            /*SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
            smtpServer.Port = 587;
            smtpServer.Credentials = new System.Net.NetworkCredential("stefanc.stratulat", "<top_secret>");

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("stefanc.stratulat@gmail.com");
            mail.To.Add("iftimie.roxana@yahoo.com");
            mail.Subject = "Test Mail";
            mail.Body = "This is for testing SMTP from gmail.";

            smtpServer.EnableSsl = true;
            //try
            //{
            smtpServer.Send(mail);
            //*/
        }
    }
}