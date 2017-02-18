using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xNet;
using CORE;

namespace Connecter2.Sending
{
    class FormConnecter:IConnecter
    {
        private static string _n = "formSend";
        public void send(string filename)
        {
            int kol = int.Parse(sys.setting.Get(_n, "symbols"));
            //string url = "https://docs.google.com/forms/d/e/1FAIpQLSeDPt79sPpuxYhecljV_iPrCRkemMkTAJ3xsaRmYmctgaDM1g/formResponse";

            string str = System.IO.File.ReadAllText(filename);
            DP_SCH_LIB.CRYPT cr = new DP_SCH_LIB.CRYPT(sys.setting.Get(_n,"pass"));
            str = cr.Enc(str);
            HttpRequest Http = new HttpRequest();
            int i = 0;
            while (i < str.Length)
            {
                string tmp = "part" + i.ToString() + "-";
                if ((i + kol) > str.Length)
                {
                    tmp += str.Substring(i);
                }
                else
                {
                    tmp += str.Substring(i, kol);
                }
                using (var request = new HttpRequest())
                {
                    request.AddParam(sys.setting.Get(_n,"Label"), tmp);
                    //sys.writeln(request.Post(sys.setting.Get(_n,"url")).ToString());
                    request.Post(sys.setting.Get(_n, "url"));
                }
                i += kol;
            }
        }

        public static void SetSetting()
        {
            sys.write(sys.lang.Get(_n, "formUrl"));
            sys.setting.Add(_n, "url", sys.read());

            sys.write(sys.lang.Get(_n, "formLabel"));
            sys.setting.Add(_n, "Label", sys.read());

            sys.write(sys.lang.Get(_n, "symbols"));
            sys.setting.Add(_n, "symbols", sys.read());

            sys.write(sys.lang.Get(_n, "password"));
            sys.setting.Add(_n, "pass", sys.read());
        }
    }
}
