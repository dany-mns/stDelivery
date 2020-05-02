using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SMTP
{
    public class Email
    {
        private static SmtpClient smptServer = null;
        private string sender;
        private string receiver;
        private string subject;
        private string message;
        private Tuple<String, String> credentials;
        public String Sender
        {
            set { this.sender = value; }
            get { return this.sender; }
        }
        public String Receiver
        {
            set { this.receiver = value; }
            get { return this.receiver; }
        }
        public String Subject
        {
            set { this.subject = value; }
            get { return this.subject; }
        }
        public String Body
        {
            set { this.message = value; }
            get { return this.message; }
        }
        public Tuple<String, String> Credentials
        {
            set { this.credentials = value; }
            get { return this.credentials; }
        }
        private static SmtpClient Server
        {
            get
            {
                if (Email.smptServer == null) { 
                    Email.smptServer = new SmtpClient("smtp.gmail.com");
                    Email.smptServer.Port = 587;
                    Email.smptServer.EnableSsl = true;
                }
                return Email.smptServer;
            }
        }
        public bool SendEmail()
        {
            Server.Credentials = new NetworkCredential(this.Credentials.Item1, this.Credentials.Item2);
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(this.Sender);
            mail.To.Add(this.Receiver);
            mail.Subject = this.Subject;
            mail.Body = this.Body;
            try
            {
                Server.Send(mail);
            }
            catch(Exception e)
            {
                return false;
            }
            return true;
        }
    }
}
