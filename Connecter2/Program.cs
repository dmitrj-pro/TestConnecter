using System;
using CORE;
using Connecter2.Sending;

namespace Connecter2
{
	class MainClass
	{
        public static void LoadSystem(string[] args)
        {
            string password = "std::password";
            sys.fileSetting = "conf.sys";
            string lang = "std";
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].Equals("--pass"))
                {
                    password = args[i + 1];
                    i += 1;
                    continue;
                }
                if (args[i].Equals("--dsch"))
                {
                    string DecFile = args[i + 2];
                    string SFile = args[i + 3];
                    string DecPass = args[i + 1];
                    i += 3;
                    DP_SCH_LIB.Crypt cr = new DP_SCH_LIB.Crypt(DecPass);
                    System.IO.File.WriteAllText(SFile, cr.Dec(System.IO.File.ReadAllText(DecFile)));
                    continue;
                }
                if (args[i].Equals("--dec"))
                {
                    string DecFile = args[i + 2];
                    string DecPass = args[i + 1];
                    i += 2;
                    DP_SCH_LIB.Crypt cr = new DP_SCH_LIB.Crypt(DecPass);
                    sys.writeln(cr.Dec(System.IO.File.ReadAllText(DecFile)));
                    continue;
                }
                if (args[i].Equals("--enc"))
                {
                    string DecFile = args[i + 2];
                    string DecPass = args[i + 1];
                    i += 2;
                    DP_SCH_LIB.Crypt cr = new DP_SCH_LIB.Crypt(DecPass);
                    sys.writeln(cr.Enc(System.IO.File.ReadAllText(DecFile)));
                    continue;
                }
                if (args[i].Equals("--esch"))
                {
                    string DecFile = args[i + 2];
                    string SFile = args[i + 3];
                    string DecPass = args[i + 1];
                    i += 3;
                    DP_SCH_LIB.Crypt cr = new DP_SCH_LIB.Crypt(DecPass);
                    System.IO.File.WriteAllText(SFile, cr.Enc(System.IO.File.ReadAllText(DecFile)));
                    continue;
                }
                if (args[i].Equals("-lang"))
                {
                    lang = args[i + 1];
                    i++;
                    continue;
                }
                if (args[i].Equals("--conf"))
                {
                    sys.fileSetting = args[i + 1];
                    i++;
                    continue;
                }
            }
            sys.cr = new DP_SCH_LIB.Crypt(password);
            password = "pass";
            if (!System.IO.File.Exists(sys.fileSetting))
            {
                sys.setting = new INI("lang=ru");
            }
            else
            {
                string text = System.IO.File.ReadAllText(sys.fileSetting);
                text = sys.cr.Dec(text);
                sys.setting = new INI(text);
            }
            if (lang.Equals("std"))
            {
                if (sys.setting.Exists("lang"))
                    lang = sys.setting.Get("lang");
                else
                    lang = "ru";
            }
            sys.lang = new INI(System.IO.File.ReadAllText(lang + ".txt"));
        }
		public static void Main (string[] args)
		{
            LoadSystem(args);
            Master boot = new Master(args);
            boot.Send();

            sys.read();
			//CONNECTER.Test2 (args);
			//CONNECTER.SendData();
		}
	}
}
