﻿using System;
using xNet;

namespace Connecter2
{
	enum TypeProxy{Nil,SOCKS5,SOCKS4,HTTP};
	public class Connect
	{
		private HttpRequest HTTP;
		public Connect ()
		{
			HTTP = new HttpRequest ();
			HTTP.ConnectTimeout = 20;
		}
		public void SetProxy(string host, int port){
			if (port < 0) {
				throw new Exception ("Port is not correct");
			}
			//switch (typ) {
			//case TypeProxy.SOCKS5:
				Socks5ProxyClient s = new Socks5ProxyClient (host, port);
				HTTP.Proxy = s;
				//break;
			//default:
				//throw new Exception ("Program is not run with this type proxy");
			//}
		}
		public void SetProxy(string host, int port, string user,string pass){
			if (port < 0) {
				throw new Exception ("Port is not correct");
			}
			//switch (typ) {
			//case TypeProxy.SOCKS5:
				Socks5ProxyClient s = new Socks5ProxyClient (host, port,user,pass);
				HTTP.Proxy = s;
				//break;
			//default:
			//	throw new Exception ("Program is not run with this type proxy");
			//}
		}
		public string ConnectTo(string url){
			HttpResponse res = HTTP.Get(url);
			return res.ToString ();
		}
		public string PostConnectTo(string url,string data){
			using (var request = HTTP)
			{
				var multipartContent = new MultipartContent()
				{
					{new StringContent("Bill Gates"), "id"},
					{new FileContent(data), "data", "1.rar"}
				};

				string syt= request.Post (url, multipartContent).ToString ();
				return syt;
			}
		}

	}
}

