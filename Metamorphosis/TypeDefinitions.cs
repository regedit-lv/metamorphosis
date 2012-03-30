using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metamorphosis
{
    class TypeInfo
    {
        public int Length;
        public string Group;
        public string Name;
        public string Definition;
    }

    static class TypeFactory
    {
        static List<TypeInfo> items = new List<TypeInfo>();

        public static TypeInfo Get(string name, bool exsistedOnly = false)
        {
            TypeInfo td = items.Find(x => x.Name == name);
            
            if (td == null && !exsistedOnly)
            {
                td = new TypeInfo() { Name = name, Definition = name, Group = "default", Length = 1 };
                items.Add(td);
            }

            return td;
        }

    }
}
