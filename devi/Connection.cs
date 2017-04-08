using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devi
{
    class Connection
    {
        public static string path = Path.Combine(Environment.CurrentDirectory, @"") + @"\datb.mdf";
        public static string connection_string = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + path + ";Integrated Security=True;Connect Timeout=30";
    }
}
