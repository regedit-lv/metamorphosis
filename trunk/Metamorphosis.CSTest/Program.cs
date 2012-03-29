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
            string xml2 = md.toXml();
            md.fromXml(xml2);
            string xml3 = md.toXml();

            if (xml2 != xml3)
            {
                Console.WriteLine("Error!!! Xml are not equal!!!!");
            }

            Console.WriteLine(xml);
            Console.WriteLine("----");
            Console.WriteLine(xml2);
        }
    }
}
