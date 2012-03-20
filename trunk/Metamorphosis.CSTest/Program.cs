using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Metamorphosis.CSTest
{
    class Program
    {
        static void Main(string[] args)
        {
            xmq.SM sm = new xmq.SM();
            sm.mss = new Dictionary<string, string>();
            sm.mss["asdf"] = "123";
            sm.mss["zxcv"] = "6789";

            byte[] b = sm.write();

            xmq.SM sm2 = new xmq.SM();
            sm2.read(b);
        }
    }
}
