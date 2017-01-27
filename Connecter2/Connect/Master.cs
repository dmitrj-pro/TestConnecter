using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CORE;
using System.Threading.Tasks;

namespace Connecter2.Sending
{
    class Master
    {
        private List<IConnecter> cons;
        private string startFol="";
        private string[] type;
        private int CountType;
        public Master(string[] args)
        {
            cons = new List<IConnecter>();
            type = new string[10];
            bool typ = false;
            CountType = 0;
            for (int i = 0; i < args.Length; i++)
            {
               
                if (args[i].Equals("--search"))
                {
                    SearchConnecter.SetSetting();
                    sys.Save();
                    continue;
                }
                if (args[i].Equals("-search"))
                {
                    cons.Add(new SearchConnecter());
                    continue;
                }
                if (args[i].Equals("-mail"))
                {
                    cons.Add(new EmailConnecter());
                    continue;
                }
                if (args[i].Equals("--mail"))
                {
                    EmailConnecter.SetSetting();
                    sys.Save();
                    continue;
                }
                if (args[i].Equals("--http"))
                {
                    ProxyConnecter.SetSetting();
                    sys.Save();
                    continue;
                }
                if (args[i].Equals("-http"))
                {
                    cons.Add(new ProxyConnecter());
                    continue;
                }

                if (args[i].Equals("-type"))
                {
                    typ = true;
                    continue;
                }
                if (args[i].Equals("-start"))
                {
                    startFol = args[++i];
                    continue;
                }
                if (typ)
                {
                    type[CountType++] = args[i];
                    continue;
                }
            }
            if (startFol.Equals(""))
            {
                startFol = System.IO.Directory.GetCurrentDirectory();
            }
        }
        public void Send()
        {
            for (int i = 0; i < CountType; i++)
                foreach (string fil in System.IO.Directory.GetFiles(startFol, type[i]))
                    foreach (var conn in cons)
                        conn.send(fil);
        }
    }
}
