/**************************************************************************
 *                                                                        *
 *  File:        Email.cs                                                 *
 *  Copyright:   (c) 2020, Stratulat Stefan                               *
 *  E-mail:      stefanc.stratulat@gmail.com                              *
 *  Website:     -                                                        *
 *  Description: The DLL containing the logic of building and sending an  *
 *               email by using SMTP and gmail email server.              *
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
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SMTP
{
    /// <summary>
    /// The class containing the logic of building and sending an email.
    /// </summary>
    public class Email
    {
        /// <summary>
        /// The server instance of SmtpClient type
        /// </summary>
        private static SmtpClient smptServer = null;
        /// <summary>
        /// The string that contains the sender email adress.
        /// </summary>
        private string sender;
        /// <summary>
        /// The string that contains the receiver email adress.
        /// </summary>
        private string receiver;
        /// <summary>
        /// The string that contains the subject of the email.
        /// </summary>
        private string subject;
        /// <summary>
        /// The string that contains the body of the email.
        /// </summary>
        private string message;
        /// <summary>
        /// A pair of strings that contains the credentials for connecting to the SMTP server.
        /// </summary>
        private Tuple<String, String> credentials;
        /// <summary>
        /// The sender getter and setter.
        /// </summary>
        public String Sender
        {
            set { this.sender = value; }
            get { return this.sender; }
        }
        /// <summary>
        /// The receiver getter and setter.
        /// </summary>
        public String Receiver
        {
            set { this.receiver = value; }
            get { return this.receiver; }
        }
        /// <summary>
        /// The subject getter and setter.
        /// </summary>
        public String Subject
        {
            set { this.subject = value; }
            get { return this.subject; }
        }
        /// <summary>
        /// The body getter and setter.
        /// </summary>
        public String Body
        {
            set { this.message = value; }
            get { return this.message; }
        }
        /// <summary>
        /// The credentials getter and setter.
        /// </summary>
        public Tuple<String, String> Credentials
        {
            set { this.credentials = value; }
            get { return this.credentials; }
        }
        /// <summary>
        /// The singleton client instance of SMTPClient.
        /// It also makes the association with the gmail server on a specified port and enables the SSL
        /// </summary>
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
        /// <summary>
        /// The function that builds the email object by setting the credentials, the sender, receiver, subject and the body.
        /// It also tries to send the email.
        /// </summary>
        /// <returns>The status of sending the email.</returns>
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
            catch(SmtpException e)
            {
                return false;
            }
            return true;
        }
    }
}
