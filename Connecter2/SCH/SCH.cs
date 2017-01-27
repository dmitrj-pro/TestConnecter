using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_SCH_LIB
{
    class Crypt
    {
        private Key _key;
        private String __key;
        private int _map_C;
        private System.Collections.Generic.List<int> ENC_Step1(string str)
        {
            System.Collections.Generic.List<int> res = new List<int>();
            System.Collections.Generic.Dictionary<char, int> map = new Dictionary<char, int>();
            for (int i = 0; i < str.Length; i++)
            {
                if (!map.ContainsKey(str[i]))
                {
                    map.Add(str[i], 0);
                }
            }
            _map_C = map.Count;
            UniKey k = new UniKey(1,map.Count, _key);
            for (int i = 0; i < map.Count; i++)
            {
                KeyValuePair<char, int> t = map.ElementAt(i);
                int tmo = k.Gen();
                map[t.Key]= tmo;
                res.Add(CONVERT.ord(t.Key));
            }
            res.Add(0);
            for (int i = 0; i < str.Length; i++)
            {
                res.Add(map[str[i]]);
            }
            return res;
        }
        private string DEC_Step1(System.Collections.Generic.List<int> tmp)
        {
            System.Collections.Generic.Dictionary<int, char> map = new Dictionary<int, char>();
            int key = 0;
            int i = 0;
            for (i = 0; i < tmp.Count; i++)
            {
                if (tmp[i] == 0)
                    break;
            }
            UniKey k = new UniKey(1, i, new Key(__key));
            for (int j = 0; j < i; j++)
            {
                map.Add(k.Gen(), CONVERT.sym(tmp[j]));
            }
            string res="";
            for (int j = i + 1; j < tmp.Count; j++)
            {
                if (tmp[j] == 0)
                {
                    break;
                }
                res += map[tmp[j]];
            }
            return res;
        }
        public Crypt(string key)
        {
            __key = key;
            _key = new Key(key);
        } 
        private int Step3_Get_Key(){
            int res=1;
            for (int i=0; i<__key.Length;i++){
                res*=CONVERT.ord(__key[i]);
            }
            return res;
        }
        private List<int> ENC_Step2(List<int> l)
        {
            List<int> res = new List<int>();
            int MAX = sizeof(int);
            byte[] dat =new byte[MAX];
            int tmp = 0;
            bool t=false;
            for (int i = 0; i < l.Count; i++)
            {
                if (t)
                {
                    if (tmp >= MAX)
                    {
                        res.Add(BitConverter.ToInt32(dat, 0));
                        tmp = 0;
                    }
                    dat[tmp++] = Convert.ToByte(l[i]);
                    continue;
                }
                if (l[i] == 0)
                {
                    dat[tmp++] = 0;
                    t = true;
                    continue;
                }
                res.Add(l[i]);
            }
            for (int i = tmp; i < dat.Length; i++)
            {
                dat[i] = 0;
            }
            res.Add(BitConverter.ToInt32(dat, 0));
            return res;
        }
        private List<int> DEC_Step2(List<int> l)
        {
            List<int> res = new List<int>();
            int i = 0;
            for (; i < l.Count; i++)
            {
                byte[] r = BitConverter.GetBytes(l[i]);
                if (r[0] == 0)
                {
                    break;
                }
                res.Add(l[i]);
            }
            for (int j = i; j < l.Count; j++)
            {
                byte[] r = BitConverter.GetBytes(l[j]);
                for (int k = 0; k < r.Length; k++)
                {
                    res.Add(r[k]);
                }
            }
            return res;
        }
        private List<int> ENC_Step3(List<int> l)
        {
            List<int> res = new List<int>();
            int MAX = this.Step3_Get_Key();
            for (int i = 0; i < l.Count; i++)
            {
                res.Add(l[i] ^ _key.Gen(MAX));
            }
            return res;
        }
        private List<int> DEC_Step3(List<int> l)
        {
            Key k = new Key(__key);
            for (int i = 0; i < _map_C; i++)
            {
                k.Gen();
            }
            List<int> res = new List<int>();
            int MAX = this.Step3_Get_Key();
            for (int i = 0; i < l.Count; i++)
            {
                int tmp = l[i] ^ k.Gen(MAX);
                if (tmp < 0)
                {
                    //tmp += int.MaxValue;
                }
                res.Add(tmp);
            }
            return res;
        }
        private List<int> ENC_Step4(List<int> l)
        {
            for (int i = 0; i < l.Count; i++)
            {
                int tmp = l[i];
                int num = _key.Gen(l.Count);
                l[i] = l[num];
                l[num] = tmp;
            }
            return l;
        }
        private List<int> DEC_Step4(List<int> l)
        {
            Key k = new Key(__key);
            for (int i = 0; i < _map_C+l.Count; i++)
            {
                k.Gen();
            }
            int[] st = new int[l.Count];
            for (int i = 0; i < l.Count; i++)
            {
                st[i] = k.Gen(l.Count);
            }
            for (int i = l.Count - 1; i >= 0; i--)
            {
                int tmp = l[st[i]];
                l[st[i]] = l[i];
                l[i] = tmp;
            }
            return l;

        }
        private string ENC_Step5(List<int> l)
        {
            string res = CONVERT.ToHex(Convert.ToByte(_map_C));
            for (int i = 0; i < l.Count; i++)
            {
                byte[] t = System.BitConverter.GetBytes(l[i]);
                for (int j = 0; j < t.Length; j++)
                {
                    res += CONVERT.ToHex(t[j]);
                }
            }
            return res;
        }
        private List<int> DEC_Step5(string str)
        {
            List<int> res = new List<int>();
            string tmp = str[0]+"" + str[1];
            _map_C = CONVERT.HexToByte(tmp);
            int k = 0;
            int ras = (str.Length - 2) / 2;
            if (ras % 4 != 0)
            {
                throw new Exception();
            }
            byte[] bs = new byte[ras];
            for (int i = 2; i < str.Length; i+=2)
            {
                tmp = str[i] + "" + str[i + 1];
                bs[k++]=CONVERT.HexToByte(tmp);
            }
            for (int i = 0; i < ras; i+=4)
            {
                res.Add(BitConverter.ToInt32(bs, i));
            }

            return res;
        }
        public string Enc(string text)
        {
            return this.ENC_Step5(this.ENC_Step4(this.ENC_Step3(this.ENC_Step2(this.ENC_Step1(text)))));
        }
        public string Dec(string text)
        {
            try
            {
                return this.DEC_Step1(this.DEC_Step2(this.DEC_Step3(this.DEC_Step4(this.DEC_Step5(text)))));
            }
            catch (Exception e)
            {
                throw new Exception("Fatal KEY");
            }
        }
    }
}
