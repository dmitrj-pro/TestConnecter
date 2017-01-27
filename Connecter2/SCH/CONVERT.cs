using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_SCH_LIB
{
    class CONVERT
    {
        public static int ord(char c)
        {
            return System.Convert.ToInt32(c);
        }
        private static char ToHexC(int b)
        {
            if ((b >= 0) && (b < 10))
            {
                return (char)('0' + b);
            }
            switch (b)
            {
                case 10:
                    return 'A';
                    break;
                case 11:
                    return 'B';
                    break;
                case 12:
                    return 'C';
                    break;
                case 13:
                    return 'D';
                    break;
                case 14:
                    return 'E';
                    break;
                case 15:
                    return 'F';
                    break;
            }
            return 'Z';
        }
        public static string ToHex(byte b)
        {
            string res = CONVERT.ToHexC(b/16).ToString()+CONVERT.ToHexC(b%16);
            return res;
        }
        private static byte HexToByte(char c)
        {
            if ((c <= '9') && (c >= '0'))
            {
                return Convert.ToByte(c - '0');
            }
            switch (c)
            {
                case 'A':
                    return 10;
                    break;
                case 'B':
                    return 11;
                    break;
                case 'C':
                    return 12;
                    break;
                case 'D':
                    return 13;
                    break;
                case 'E':
                    return 14;
                    break;
                case 'F':
                    return 15;
                    break;
                default:
                    return 18;
                    break;
            }
        }
        public static byte HexToByte(string str)
        {
            return Convert.ToByte(CONVERT.HexToByte(str[0]) * 16 + CONVERT.HexToByte(str[1]));

        }
        public static char sym(int c)
        {
            if (c < 0)
            {
                return '\n';
            }
            return System.Convert.ToChar(c);
        }
    }
}
