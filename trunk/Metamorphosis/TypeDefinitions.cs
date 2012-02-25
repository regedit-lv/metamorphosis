using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metamorphosis
{
    class TypeDefinition
    {
        public int Length;
        public string Group;
        public string Name;
        public string Definition;
    }

    static class TypeDefinitions
    {
        static List<TypeDefinition> items = new List<TypeDefinition>();

        public static TypeDefinition Get(string name, bool exsistedOnly = false)
        {
            TypeDefinition td = items.Find(x => x.Name == name);
            
            if (td == null && !exsistedOnly)
            {
                td = new TypeDefinition() { Name = name, Definition = name, Group = "default", Length = 1 };
                items.Add(td);
            }

            return td;
        }

    }
}
