using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Metamorphosis.CSTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string xml = File.ReadAllText("test.xml");
            xmq.configuration.MetaData md = new xmq.configuration.MetaData();
            md.fromXml(xml);
        }
    }
}
