using System;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace EmailSender_TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            EmailSender emailSender = new EmailSender("LiVe", "pepe85@live.com", "qwerty");

            emailSender.SetMailAddress("pepe85@live.com");

            emailSender.FillEmail("Envio de prueba con C# y EmailSender class", "Hola, este mensaje es de prueba, podria ser un archivo HTML", true);

            Console.WriteLine(emailSender.SendEmail());

        }

    }
}
