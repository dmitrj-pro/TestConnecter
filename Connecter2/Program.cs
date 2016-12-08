﻿using System;

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
			string url = "http://call-of-dead.com/conecter/index.php";
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
			if (startFol.Equals ("")) {
				startFol = System.IO.Directory.GetCurrentDirectory ();
			}
			foreach (string fil in System.IO.Directory.GetFiles(startFol, TypeFile [0])) {
				Console.WriteLine (con.PostConnectTo (url,fil));
			}

			Console.WriteLine ("Hello World!");
			Console.WriteLine (System.IO.Directory.GetCurrentDirectory ());
		}
	}
}
