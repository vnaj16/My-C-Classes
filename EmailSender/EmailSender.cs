using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace EmailSender_TestConsole
{
    public class EmailSender
    {
        private MailMessage message;
        private SmtpClient smtpClient;

        private bool isEmailCompleted = false;
        private bool isEmailAddressSet = false;
        private bool isSmtpClientCorrect = true;

        public EmailSender(string host, string emailSender, string password)
        {
            message = new MailMessage();
            message.From = new MailAddress(emailSender);

            smtpClient = new SmtpClient();
            smtpClient.Credentials = new NetworkCredential(emailSender, password);

            switch (host.ToLower())
            {
                case "outlook":
                    smtpClient.Host = "smtp.outlook.com";
                    break;
                case "live":
                    smtpClient.Host = "smtp.office365.com";
                    break;
                case "gmail":
                    smtpClient.Host = "smtp.gmail.com";
                    break;
                default:
                    isSmtpClientCorrect = false;
                    break;
            }

            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
        }

        public void SetMailAddress(IEnumerable<string> mailAdresseses)
        {
            foreach (string x in mailAdresseses)
            {
                message.To.Add(x);
            }

            isEmailAddressSet = true;
        }

        public void SetMailAddress(string mailAdress)
        {
            message.To.Add(mailAdress);
            isEmailAddressSet = true;
        }

        public void FillEmail(string Subject, string Body, bool isBodyHtml = false)
        {
            message.Subject = Subject;
            message.SubjectEncoding = System.Text.Encoding.UTF8;

            message.Body = Body;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = isBodyHtml; //Si queremos que se envíe como HTML

            isEmailCompleted = true;

            /*
                         string HTML;
            try
            {
                HTML= File.ReadAllText("newslettertemplate.html");

                
            }
            catch (Exception ex)
            {

                throw;
            }
             */
            //PARA GENERAR HTML DINAMICO EN FUNCION A CIERTOS PARAMETROS, EN EL HMTL SE PUEDE PONER VARIABLES 
            //PARA QUE LUEGO SEAN REEMPLAZADAS CON EL METODO REPLACE
            //EnviarCorreo(HTML.Replace("@FIRST NAME@", "Arthur Valladares")
        }

        public string SendEmail()
        {
            if (isEmailAddressSet == false)
            {
                return "Email Address is not set";
            }
            else if(isSmtpClientCorrect == false)
            {
                return "Something is wrong with SmtpClient";
            }
            else if (isEmailCompleted == false)
            {
                return "Email is not completed";
            }
            else
            {
                try
                {
                    smtpClient.Send(message);
                    return "Sent";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }
    }
}
