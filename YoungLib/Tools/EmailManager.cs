using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace YoungLib.Tools
{
    public class EmailManager
    {
        private MailMessage _mailMessage;
        private SmtpClient _smtpClient;

        public string From
        {
            get { return (_mailMessage.From == null) ? String.Empty : _mailMessage.From.Address; }
            set { _mailMessage.From = new MailAddress(value); }
        }

        public MailAddressCollection To
        {
            get { return _mailMessage.To; }
        }

        public string Subject
        {
            get { return _mailMessage.Subject; }
            set { _mailMessage.Subject = value; }
        }

        public string Body
        {
            get { return _mailMessage.Body; }
            set { _mailMessage.Body = value; }
        }

        public EmailManager(string host, int port, string id, string password)
        {
            _smtpClient = new SmtpClient(host, port);
            _smtpClient.Credentials = new NetworkCredential(id, password);

            _mailMessage = new MailMessage();
            _mailMessage.IsBodyHtml = true;
            _mailMessage.Priority = MailPriority.Normal;
        }

        public void Send()
        {
            _smtpClient.Send(_mailMessage);
        }


        //App.config에서 읽어들임
        #region Static Methods
        public static void Send(string smtpSender, string to, string subject, string content, string cc, string bcc)
        {
            //null check
            if (String.IsNullOrEmpty(smtpSender))
                throw new ArgumentNullException("smtpSender is empty");
            if (String.IsNullOrEmpty(to))
                throw new ArgumentNullException("to is empty");
            if (String.IsNullOrEmpty(subject))
                throw new ArgumentNullException("subject is empty");
            if (String.IsNullOrEmpty(content))
                throw new ArgumentNullException("content is empty");

            //App.config에 해당 항목이 없으면 null 반환
            string smtpHost = ConfigurationManager.AppSettings["SMTPHost"];
            string smtpId = ConfigurationManager.AppSettings["SMTPId"];
            string smtpPw = ConfigurationManager.AppSettings["SMTPPw"];
            int.TryParse(ConfigurationManager.AppSettings["SMTPPort"], out int smtpPort);
            smtpPort = (smtpPort == 0) ? 25 : smtpPort;

            var mailMsg = new MailMessage();
            mailMsg.From = new MailAddress(smtpSender);
            mailMsg.To.Add(to);
            mailMsg.Subject = subject;
            mailMsg.IsBodyHtml = true;
            mailMsg.Body = content;
            if (!String.IsNullOrEmpty(cc)) mailMsg.CC.Add(cc);
            if (!String.IsNullOrEmpty(bcc)) mailMsg.Bcc.Add(bcc);
            mailMsg.Priority = MailPriority.Normal;

            var smtpClient = new SmtpClient();
            smtpClient.Credentials = new NetworkCredential(smtpId, smtpPw);
            smtpClient.Host = smtpHost;
            smtpClient.Port = smtpPort;
            smtpClient.Send(mailMsg);
        }

        public static void Send(string smtpSender, string to, string subject, string content)
        {
            Send(smtpSender, to, subject, content, null, null);
        }

        public static void Send(string to, string subject, string content)
        {
            string smtpSender = ConfigurationManager.AppSettings["SMTPSender"];
            Send(smtpSender, to, subject, content);
        }
        #endregion
    }
}
