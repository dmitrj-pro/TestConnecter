using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_SCH_LIB
{
    class UniKey
    {
        private Key _key;
        private List<int> _l;
        public UniKey(int start, int N, Key k)
        {
            if (N < 0)
                throw new System.Exception();
            _l = new List<int>();
            for (int i = 0; i < N; i++)
            {
                _l.Add(i+start);
            }
            _key = k;
        }
        public int Gen()
        {
            if (_l.Count < 0)
                return 0;
            int i = _key.Gen() % _l.Count;
            int res = _l.ElementAt(i);
            _l.RemoveAt(i);
            return res;
        }
    }
}
