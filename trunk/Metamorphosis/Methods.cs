using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metamorphosis
{
    static class Methods
    {
        public static List<Method> Items = new List<Method>();

        static public void AddMethod(Method method)
        {
            Items.Add(method);
        }

        static public Method GetMethod(string name)
        {
            Method m = Items.Find(x => x.Name == name);

            if (m == null)
            {
                m = new Method();
                m.Name = name;
                Items.Add(m);
            }

            return m;
        }
    }
}
