using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            TypeInfo td = part.Larva.TypeInfo;
            Variable v = Items.Find(x => x.Name == td.Group);
            if (v != null)
            {
                // search for sub larva group
                if (l.SubLarvae != null)
                {
                    for (int i = 0; i < l.SubLarvae.Count; i++)
                    {
                        Larva sl = l.SubLarvae[i];
                        Variable sv = v.Variables.GetVariable(sl.TypeInfo.Group, true);
                        if (sv == null)
                        {
                            break;
                        }
                        v = sv;
                    }
                }

                string value = v.Value;

                TextHelper.ReplaceSystemFields(ref value, part);

                return value;
            }

            return "";
        }

        public string ReplaceAll(string text, Larva larva)
        {
            while (true)
            {
                // check for expression
                Match match = Regex.Match(text, @"%(~[^%]+)?%", RegexOptions.IgnoreCase);

                // Here we check the Match instance.
                if (match.Success)
                {
                    string v = match.Captures[0].Value;
                    string group = null;

                    if (v != "%%")
                    {
                        group = v.Substring(2, v.Length - 3);
                    }

                    string fields = "";

                    // replace with base larva fields
                    Larva bl = larva;
                    while (true)
                    {
                        if (bl.BaseName == null)
                        {
                            break;
                        }

                        bl = larva.BaseLarva;
                        if (bl == null)
                        {
                            break;
                        }

                        foreach (Part p in bl.Parts)
                        {
                            // skip part if part from different group or with skip mode
                            if ((group != null && group != p.Larva.TypeInfo.Group) || 
                                p.Mode == PartMode.Skip)
                            {
                                continue;
                            }

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
                        // skip part if part from different group or with skip mode
                        if ((group != null && group != p.Larva.TypeInfo.Group) ||
                            p.Mode == PartMode.Skip)
                        {
                            continue;
                        }

                        string sp = ReplacePart(p);
                        if (sp != "")
                        {
                            fields += sp + Environment.NewLine;
                        }
                    }

                    text = TextHelper.ReplaceField(ref text, v, fields);
                }
                else
                {
                    break;
                }
            }

            // replace with variables
            foreach (Variable v in Items)
            {
                string name = "%" + v.Name + "%";
                if (text.Contains(name))
                {
                    string value = v.Value;
                    value = v.Variables.ReplaceAll(value, larva);
                    TextHelper.ReplaceExpressions(ref value, larva);
                    TextHelper.ReplaceField(ref text, name, value);
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
            string m = larva.Type == LarvaType.Struct ? Larvae.GetElement(ElementType.MethodDeclaration) : Larvae.GetElement(ElementType.StaticMethodDeclaration);

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

            m = Generator.Current.GetVirtualModificator(fromBase, toChild) + m;
            m = Variables.ReplaceAll(m, larva).Replace("%larva%", larva.Name).Replace("%method%", Name);

            return m;
        }

        public string GetDefinition(Larva larva)
        {
            string m = larva.Type == LarvaType.Struct ? Larvae.GetElement(ElementType.MethodDefinition) : Larvae.GetElement(ElementType.StaticMethodDefinition);

            m = Variables.ReplaceAll(m, larva).Replace("%larva%", larva.Name).Replace("%method%", Name);

            return m;
        }
    }
}
