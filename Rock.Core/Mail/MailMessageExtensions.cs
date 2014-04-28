﻿using System.Net.Mail;
using System.Threading.Tasks;

namespace Rock.Mail
{
    public static class MailMessageExtensions
    {
        public static void Send(this MailMessage mailMessage, IDeliveryMethod deliveryMethod = null)
        {
            SendImpl(mailMessage, deliveryMethod ?? DeliveryMethod.Default);
        }

        public static Task SendAsync(this MailMessage mailMessage, IDeliveryMethod deliveryMethod = null)
        {
            return SendAsyncImpl(mailMessage, deliveryMethod ?? DeliveryMethod.Default);
        }

        private static void SendImpl(this MailMessage mailMessage, IDeliveryMethod deliveryMethod)
        {
            using (var smtpClient = new SmtpClient())
            {
                deliveryMethod.ConfigureSmtpClient(smtpClient);
                smtpClient.Send(mailMessage);
            }
        }

        private static async Task SendAsyncImpl(MailMessage mailMessage, IDeliveryMethod deliveryMethod)
        {
            using (var smtpClient = new SmtpClient())
            {
                deliveryMethod.ConfigureSmtpClient(smtpClient);
                await smtpClient.SendMailAsync(mailMessage);
            }
        }
    }
}