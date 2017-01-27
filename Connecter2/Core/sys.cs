using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Connecter2;
using System.Threading.Tasks;

namespace CORE
{
    class sys
    {
        public static INI lang;
        public static INI setting;
        public static string fileSetting;
        public static DP_SCH_LIB.Crypt cr;
        public static void Save()
        {
            System.IO.File.WriteAllText(sys.fileSetting, cr.Enc(setting.ToString()));
            //writeln(setting.ToString());
        }
        public static string read()
        {
            return System.Console.ReadLine();
        }
        public static void write(string str)
        {
            Console.WriteLine(str);
        }
        public static void writeln(string str)
        {
            sys.write(str + "\n");
        }
    }
}
