using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using CORE;

namespace Connecter2.Sending
{
    class EmailConnecter : IConnecter
    {
        private System.Net.Mail.SmtpClient smtp;
        private MailMessage mes;
        private static string _n = "mailSend";
        public EmailConnecter()
        {
            smtp = new SmtpClient();
            smtp.Host = sys.setting.Get(_n, "ihost");
            smtp.Port = int.Parse(sys.setting.Get(_n, "iport"));
            if (sys.setting.Get(_n, "issl").Equals("1"))
            {
                smtp.EnableSsl = true;
            }
            if (!sys.setting.Get(_n, "iuser").Equals("none"))
            {
                NetworkCredential user = new NetworkCredential();
                user.UserName = sys.setting.Get(_n, "iuser");
                user.Password = sys.setting.Get(_n, "ipass");
                smtp.Credentials = user;
            }
            MailAddress to;
            MailAddress from;
            to = new MailAddress(sys.setting.Get(_n, "ito"));
            from = new MailAddress(sys.setting.Get(_n, "ifrom"), sys.setting.Get(_n, "ifrom"));
            mes = new MailMessage(from, to);
            mes.Body = sys.setting.Get(_n, "ibody");
            mes.Subject = sys.setting.Get(_n, "itheme");
            mes.From = from;
        }
        public void send(string filename)
        {
            mes.Attachments.Clear() ;
            mes.Attachments.Add(new Attachment(filename));
            smtp.Send(mes);
        }
        public static void SetSetting()
        {
            sys.write(sys.lang.Get(_n, "ihost"));
            sys.setting.Add(_n, "ihost", sys.read());
            sys.write(sys.lang.Get(_n, "iport"));
            sys.setting.Add(_n, "iport", sys.read());
            sys.write(sys.lang.Get(_n, "issl"));
            sys.setting.Add(_n, "issl", sys.read());
            sys.write(sys.lang.Get(_n, "iuser"));
            sys.setting.Add(_n, "iuser", sys.read());
            sys.write(sys.lang.Get(_n, "ipass"));
            sys.setting.Add(_n, "ipass", sys.read());
            sys.write(sys.lang.Get(_n, "ito"));
            sys.setting.Add(_n, "ito", sys.read());
            sys.write(sys.lang.Get(_n, "ifrom"));
            sys.setting.Add(_n, "ifrom", sys.read());
            sys.write(sys.lang.Get(_n, "ifromname"));
            sys.setting.Add(_n, "ifromname", sys.read());
            sys.write(sys.lang.Get(_n, "itheme"));
            sys.setting.Add(_n, "itheme", sys.read());
            sys.write(sys.lang.Get(_n, "ibody"));
            sys.setting.Add(_n, "ibody", sys.read());
        }
    }
}
