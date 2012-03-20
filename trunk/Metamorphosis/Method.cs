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

                string value = v.Value.Replace("%type%", l.GetTypeDefinition()).Replace("%field%", part.Name);
                value = Larvae.ReplaceSystemFields(value, part);

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
                        string sp = ReplacePart(p);
                        if (sp != "")
                        {
                            fields += sp + Environment.NewLine;
                        }
                    }
                }

                // replace with larva fields
                foreach (Part p in larva.Parts)
                {
                    string sp = ReplacePart(p);
                    if (sp != "")
                    {
                        fields += sp + Environment.NewLine;
                    }
                }

                return Larvae.ReplaceField(ref text, "%%", fields);
            }
            else
            {
                // replace with variables
                foreach (Variable v in Items)
                {
                    string name = "%" + v.Name + "%";
                    string value = v.Value;
                    value = v.Variables.ReplaceAll(value, larva);
                    Larvae.ReplaceExpression(value, larva);
                    Larvae.ReplaceField(ref text, name, value);
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

            // check does this method need to be virtual            
            // check does base class contain the same method
            bool fromBase = false;
            Larva b = Larvae.GetLarva(larva.BaseName, true);

            while (b != null)
            {
                if (b.Methods.Contains(Name))
                {
                    fromBase = true;
                }
                b = Larvae.GetLarva(b.BaseName, true);
            }

            bool toChild = false;

            foreach (KeyValuePair<string, Larva> p in Larvae.Items)
            {
                if (p.Value.BaseName == larva.Name && p.Value.Methods.Contains(Name))
                {
                    toChild = true;
                    break;
                }
            }

            switch (Larvae.GetElement(ElementType.OutputLanguage).ToUpper())
            {
                case "C++":
                    if (toChild)
                    {
                        m = "virtual " + m;
                    }
                    break;

                case "C#":
                    if (fromBase)
                    {
                        m = "override " + m;
                    }
                    else if (toChild)
                    {
                        m = "virtual " + m;
                    }
                    
                    break;
            }

            m = Variables.ReplaceAll(m, larva).Replace("%larva%", larva.Name).Replace("%method%", Name);

            return m;
        }

        public static bool tt1(KeyValuePair<string, Larva> x)
        {
            return true;
        }

        public static bool tt2(KeyValuePair<string, Larva> x)
        {
            return true;
        }

        

        public string GetDefinition(Larva larva)
        {
            string m = Larvae.GetElement(ElementType.MethodDefinition);

            m = Variables.ReplaceAll(m, larva).Replace("%larva%", larva.Name).Replace("%method%", Name);

            return m;
        }
    }
}
