using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metamorphosis
{
    class Variable
    {
        public VariableList Parent;
        public string Name;
        public string Value;
        public VariableList Variables = new VariableList();
    }

    class VariableList
    {
        public List<Variable> Items = new List<Variable>();

        public Variable GetVariable(string name, bool existedOnly = false)
        {
            Variable v = Items.Find(x => x.Name == name);

            if (v == null && !existedOnly)
            {
                v = new Variable() { Name = name, Parent = this };
                Items.Add(v);
            }

            return v;
        }

        public string ReplacePart(Part part)
        {
            Larva l = part.Larva;
            TypeDefinition td = part.Larva.TypeDefinition;
            Variable v = Items.Find(x => x.Name == td.Group);
            if (v != null)
            {
                // search for sub larva group
                if (l.SubLarvae != null)
                {
                    for (int i = 0; i < l.SubLarvae.Count; i++)
                    {
                        Larva sl = l.SubLarvae[i];
                        Variable sv = v.Variables.GetVariable(sl.TypeDefinition.Group, true);
                        if (sv == null)
                        {
                            break;
                        }
                        v = sv;
                    }
                }

                string value = Larvae.ReplaceSystemFields(v.Value, part).Replace("%type%", l.GetTypeDefinition()).Replace("%field%", part.Name);

                // replace sub types
                if (l.SubLarvae != null)
                {
                    for (int i = 0; i < l.SubLarvae.Count; i++)
                    {
                        Larva sl = l.SubLarvae[i];
                        string t = "%type" + i.ToString() + "%";
                        value = value.Replace(t, sl.GetTypeDefinition());
                    }
                }

                return value;
            }

            return "";
        }

        public string ReplaceAll(string text, Larva larva)
        {
            if (text.IndexOf("%%") != -1)
            {
                string fields = "";
                
                // replace with base larva fields
                Larva bl = larva;
                while (true)
                {
                    if (bl.BaseName == null)
                    {
                        break;
                    }

                    bl = Larvae.GetLarva(bl.BaseName, true);
                    if (bl == null)
                    {
                        break;
                    }

                    foreach (Part p in bl.Parts)
                    {
                        fields += ReplacePart(p) + Environment.NewLine;
                    }
                }

                // replace with larva fields
                foreach (Part p in larva.Parts)
                {
                    fields += ReplacePart(p) + Environment.NewLine;
                }

                return Larvae.ReplaceField(text, "%%", fields);
            }
            else
            {
                // replace with variables
                foreach (Variable v in Items)
                {
                    string name = "%" + v.Name + "%";
                    string value = v.Value;
                    value = v.Variables.ReplaceAll(value, larva);
                    text =  Larvae.ReplaceField(text, name, value);
                }
            }
            return text;
        }
    }

    class Method
    {
        public string Name;
        public VariableList Variables = new VariableList();

        public string GetDeclaration(Larva larva)
        {
            string m = Larvae.GetElement(ElementType.MethodDeclaration);

            m = Variables.ReplaceAll(m, larva).Replace("%larva%", larva.Name).Replace("%method%", Name);

            return m;
        }

        public string GetDefinition(Larva larva)
        {
            string m = Larvae.GetElement(ElementType.MethodDefinition);

            m = Variables.ReplaceAll(m, larva).Replace("%larva%", larva.Name).Replace("%method%", Name);

            return m;
        }
    }
}
