using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connecter2
{
    class INI
    {
        private Dictionary<string, Dictionary<string,string>> _data;
        public void Add(string root, string key, string value)
        {
            if (root.Equals(""))
            {
                root = "root";
            }
            if (!_data.ContainsKey(root))
                _data[root] = new Dictionary<string, string>();
            if (_data[root].ContainsKey(key))
                _data[root][key] = value;
            else
                _data[root].Add(key, value);
        }
        public void Add(string key, string value)
        {
            this.Add("", key, value);
        }
        public string Get(string root, string key)
        {
            if (!_data.ContainsKey(root))
                return "false";
            if (!_data[root].ContainsKey(key))
                return "false";
            return _data[root][key];
        }
        public string Get(string key)
        {
            return this.Get("root", key);
        }
        public bool Exists(string root, string key)
        {
            if (!_data.ContainsKey(root))
            {
                return false;
            }
            if (!_data[root].ContainsKey(key))
            {
                return false;
            }
            return true;
        }
        public bool Exists(string key)
        {
            return this.Exists("root", key);
        }
        public override string ToString()
        {
            string res = "";
            foreach (var x in _data)
            {
                res += "[" + x.Key + "]\n";
                foreach (var y in x.Value)
                {
                    res += y.Key + "=" + y.Value+"\n";
                }
            }
            return res;
        }
        public INI(string text)
        {
            string[] mass = text.Split('\n');
            string key = "root";
            _data = new Dictionary<string, Dictionary<string, string>>();
            for (int i = 0; i < mass.Length; i++)
            {
                if (mass[i].Equals(""))
                    continue;
                if (mass[i][0] == '[')
                {
                    key = mass[i].Substring(1, mass[i].Length - 1 - (mass[i].Length - mass[i].IndexOf(']')));
                    continue;
                }
                int j = 0;
                for (; j < mass[i].Length; j++)
                {
                    if (mass[i][j] == '=')
                    {
                        break;
                    }
                }
                try
                {
                    string k = mass[i].Substring(0, j);
                    string v = mass[i].Substring(j + 1);
                    this.Add(key, k, v);
                }
                catch (Exception e)
                {

                }
            }
        }
    }
}
