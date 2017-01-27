using System;

namespace DP_SCH_LIB
{
    public class Key
    {
        private string _Key;
        private int _pos;
        private int _prev;
        private int _num;

        public Key(string Key)
        {
            _num = 1;
            int tmp = 0;
            for (int i = 0; i < Key.Length; i++)
            {
                tmp += DP_SCH_LIB.CONVERT.ord(Key[i]);
            }
            tmp *= Key.Length;
            _Key = Key;
            _prev = tmp;
        }
        public int Gen()
        {
            if (_pos >= _Key.Length)
            {
                _pos = 0;
            }
            int tmp = _prev * _num * CONVERT.ord(_Key[_pos])+_pos;
            if (tmp < 0)
                tmp *= -1;
            _prev = tmp;
            _num++;
            _pos++;
            return tmp;
        }
        public int Gen(int max)
        {
            if (max <= 0)
                return this.Gen();
            return this.Gen() % max;
        }
    }
}
