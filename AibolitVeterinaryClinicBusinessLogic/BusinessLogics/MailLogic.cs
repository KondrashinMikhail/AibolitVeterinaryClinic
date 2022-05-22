using AibolitVeterinaryClinicContracts.BindingModels;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AibolitVeterinaryClinicBusinessLogic.BusinessLogics
{
    public class MailLogic
    {
        private string _smtpClientHost;

        private int _smtpClientPort;

        private string _mailLogin;

        private string _mailPassword;

        private string _mailName;
        public void MailConfig(MailConfigBindingModel config)
        {
            _smtpClientHost = config.SmtpClientHost;
            _smtpClientPort = config.SmtpClientPort;
            _mailLogin = config.MailLogin;
            _mailPassword = config.MailPassword;
            _mailName = config.MailName;
        }
        public async void MailSendAsync(MailSendInfoBindingModel info)
        {
            if (string.IsNullOrEmpty(_smtpClientHost) || _smtpClientPort == 0) return;
            if (string.IsNullOrEmpty(_mailLogin) || string.IsNullOrEmpty(_mailPassword) || string.IsNullOrEmpty(_mailName)) return;
            if (string.IsNullOrEmpty(info.MailAddress) || string.IsNullOrEmpty(info.Subject) || string.IsNullOrEmpty(info.Text) || string.IsNullOrEmpty(info.FileName)) return;
            await SendMailAsync(info);
        }

        private async Task SendMailAsync(MailSendInfoBindingModel info)
        {
            using var objMailMessage = new MailMessage();
            using var objSmtpClient = new SmtpClient(_smtpClientHost, _smtpClientPort);
            try
            {
                objMailMessage.From = new MailAddress(_mailLogin, _mailName);
                objMailMessage.To.Add(new MailAddress(info.MailAddress));
                objMailMessage.Subject = info.Subject;
                objMailMessage.Body = info.Text;
                objMailMessage.Attachments.Add(new Attachment(info.FileName));
                objMailMessage.SubjectEncoding = Encoding.UTF8;
                objMailMessage.BodyEncoding = Encoding.UTF8;
                objSmtpClient.UseDefaultCredentials = false;
                objSmtpClient.EnableSsl = true;
                objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                objSmtpClient.Credentials = new NetworkCredential(_mailLogin, _mailPassword);
                await Task.Run(() => objSmtpClient.Send(objMailMessage));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
