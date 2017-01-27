using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xNet;
using CORE;

namespace Connecter2.Sending
{
    class ProxyConnecter:IConnecter
    {
        private HttpRequest HTTP;
        private string url;
        private static string _n = "proxySend";
        public ProxyConnecter()
        {
            HTTP = new HttpRequest();
            if (sys.setting.Get(_n, "isProxy").Equals("1"))
            {
                string type = sys.setting.Get(_n, "ProxyType");
                ProxyClient client;
                if (type.Equals("Socks5"))
                {
                    client = new Socks5ProxyClient();
                }
                else
                {
                    if (type.Equals("Socks4"))
                        client = new Socks4ProxyClient();
                    else
                        if (type.Equals("Socks4a"))
                            client = new Socks4aProxyClient();
                        else
                            if (type.Equals("Http"))
                                client = new HttpProxyClient();
                            else
                                throw new Exception("Unexcepted Proxy");
                }
                client.Host = sys.setting.Get(_n, "ProxyHost");
                client.Port = int.Parse(sys.setting.Get(_n, "ProxyPort"));
                if (sys.setting.Get(_n, "ProxyAuth").Equals("1"))
                {
                    client.Username = sys.setting.Get(_n, "ProxyUser");
                    client.Password = sys.setting.Get(_n, "ProxyPass");
                }
                this.HTTP.Proxy = client;
            }
            url = sys.setting.Get(_n, "url");
        }

        public void send(string filename)
        {
            using (var request = HTTP)
            {
                var multipartContent = new MultipartContent()
				{
					{new StringContent("Bill Gates"), "id"},
					{new FileContent(filename), "data", filename}
				};

                string syt = request.Post(url, multipartContent).ToString();
            }
        }
        public static void SetSetting(){
            sys.write(sys.lang.Get(_n, "isProxy"));
            string tmp = sys.read();
            if (tmp.Equals("1"))
            {
                sys.setting.Add(_n, "isProxy", "1");

                sys.write(sys.lang.Get(_n, "ProxyType"));
                sys.setting.Add(_n, "ProxyType", sys.read());

                sys.write(sys.lang.Get(_n, "ProxyHost"));
                sys.setting.Add(_n, "ProxyHost", sys.read());

                sys.write(sys.lang.Get(_n, "ProxyPort"));
                sys.setting.Add(_n, "ProxyPort", sys.read());

                sys.write(sys.lang.Get(_n, "ProxyAuth"));

                if (sys.read().Equals("1"))
                {
                    sys.setting.Add(_n, "ProxyAuth", "1");

                    sys.write(sys.lang.Get(_n, "ProxyUser"));
                    sys.setting.Add(_n, "ProxyUser", sys.read());

                    sys.write(sys.lang.Get(_n, "ProxyPass"));
                    sys.setting.Add(_n, "ProxyPass", sys.read());
                }
                else
                    sys.setting.Add(_n, "ProxyAuth", "0");

            }
            else
                sys.setting.Add(_n, "isProxy", "0");
            sys.write(sys.lang.Get(_n, "url"));
            sys.setting.Add(_n, "url", sys.read());
        }
    }
}
