using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Metamorphosis
{
    static class Larvae
    {
        public static Dictionary<ElementType, string> Elements = new Dictionary<ElementType,string>();
        public static Dictionary<string, Larva> Items = new Dictionary<string,Larva>();
        public static List<string> Imports = new List<string>();

        static uint id = 1;

        public static uint GetId()
        {
            return id++;
        }

        public static void AddImport(string import)
        {
            Imports.Add(Program.RemoveExtension(import));
        }

        public static string GetElement(ElementType elementType)
        {
            return Elements.ContainsKey(elementType) ? Larvae.Elements[elementType] : "";
        }

        public static void SetElement(ElementType elementType, string value)
        {
            Larvae.Elements[elementType] = value;
        }

        static public Larva GetLarva(string fullName, bool existingOnly = false)
        {
            if (fullName != null && Items.ContainsKey(fullName))
            {
                return Items[fullName];
            }
            else
            {
                // try to add current namespace if name is without . (maybe from local namespace)
                if (fullName != null && !fullName.Contains('.'))
                {                    
                    string realyFullName = GetFullName(fullName);
                    if (Items.ContainsKey(realyFullName))
                    {
                        return Items[realyFullName];
                    }
                }

                if (existingOnly)
                {
                    return null;
                }

                Larva l = new Larva();
                l.Name = l.FullName = fullName;
                l.Type = LarvaType.Custom;
                Items[fullName] = l;
                return l;
            }
        }

        static void Parse(string text)
        {
            Parser parser = new Parser(text);
            parser.Parse();
        }

        public static void ParseLanguageDefinition()
        {
            string fn = GetElement(ElementType.DefinitionFile);
            if (fn == "")
            {
                string ol = GetElement(ElementType.OutputLanguage);
                switch (ol.ToUpper())
                {
                    case "C++":
                        fn = "cpp.def";
                        break;

                    case "C#":
                        fn = "cs.def";
                        break;
                }
            }
            if (fn != null)
            {
                string lines = Program.ReadAllText(fn);
                Parse(lines);
            }
        }

        public static void ParseMetaDefinition(string lines)
        {
            Parse(lines);
        }

        public static string AddIndent(string text, int n)
        {
            string si = GetIndent(n);
            return AddIndent(text, si);
        }

        public static string AddIndent(string text, string indent)
        {
            text = indent + text.Replace(Environment.NewLine, Environment.NewLine + indent);
            return text;
        }

        public static string ReplaceExpression(ref string text, object obj)
        {
            while (true)
            {
                // check for expression
                Match match = Regex.Match(text, @"%![^%]+%", RegexOptions.IgnoreCase);

                // Here we check the Match instance.
                if (match.Success)
                {
                    string v = match.Captures[0].Value;
                    string n = v.Substring(2, v.Length - 3);
                    n = ObjectHelper.GetValue(obj, n);
                    text = text.Replace(v, n);
                }
                else
                {
                    break;
                }
            }
            return text;
        }

        public static string ReplaceSystemFields(string text, Part part)
        {
            while (true)
            {
                // check for auto_id names
                Match match = Regex.Match(text, @"%_[_0-9a-zA-Z]+%", RegexOptions.IgnoreCase);

                // Here we check the Match instance.
                if (match.Success)
                {
                    string v = match.Captures[0].Value;
                    string n = part.GetVariable(v);
                    text = text.Replace(v, n);
                }
                else
                {
                    break;
                }
            }

            ReplaceExpression(ref text, part);

            while (true)
            {
                // check method variable
                Match match = Regex.Match(text, @"%#[^%]+%", RegexOptions.IgnoreCase);

                // Here we check the Match instance.
                if (match.Success)
                {
                    string v = match.Captures[0].Value;
                    string n = v.Substring(2, v.Length - 3);
                    n = CommandProcessor.Process(n);
                    Larvae.ReplaceField(ref text, v, n);
                }
                else
                {
                    break;
                }
            }

            return text;
        }

        public static string ReplaceField(ref string text, string field, string value)
        {
            // replace indention symbols inside value
            value = value.Replace("%n%", Environment.NewLine).Replace("%t%", GetIndent(1));
            bool emptyIndention;
            string indent = GetIndentForField(text, field, out emptyIndention);
            if (emptyIndention)
            {
                text = RemoveIndentForField(text, field);
                value = AddIndent(value, indent);
            }
            text = text.Replace(field, value);
            return text;
        }

        public static string GetIndentForField(string text, string field, out bool emptyIndention)
        {
            emptyIndention = true;
            int i = 0;

            i = text.IndexOf(field, i);
            if (i == -1)
            {
                return null;
            }

            int e = i > 0 ? i - 1 : i;

            // search for new line
            while (e > 0 && text.IndexOf(Environment.NewLine, e) != e)
            {
                if (text[e] != ' ' && !Environment.NewLine.Contains(text[e]))
                {
                    emptyIndention = false;
                }
                e--;
            }

            // check for first symbol
            if (e == 0 && text[e] != ' ' && !Environment.NewLine.Contains(text[e]))
            {
                emptyIndention = false;
            }

            if (text.IndexOf(Environment.NewLine, e) == e)
            {
                e += Environment.NewLine.Length;
            }

            text = new String(' ', i - e);

            return text;
        }

        public static string RemoveIndentForField(string text, string field)
        {
            int i = 0;
            while (true)
            {
                i = text.IndexOf(field, i);
                if (i == -1)
                {
                    break;
                }
                
                int e = i;

                // search for new line
                while (e > 0 && text.IndexOf(Environment.NewLine, e) != e)
                {                    
                    e--;
                }

                if (text.IndexOf(Environment.NewLine, e) == e)
                {
                    e += Environment.NewLine.Length;
                }

                text = text.Remove(e, i - e);
                i = e + 1;
            }

            return text;
        }

        public static string GetIndent(int index)
        {
            string r = "";

            while (index > 0)
            {
                r += "    ";
                index--;
            }

            return r;
        }

        static void GenerateStruct(Larva larva)
        {
            // generate struct
            string templateLarvaDeclaration = larva.BaseName == null ? Elements[ElementType.StructDefinition] : Elements[ElementType.StructDefinitionWithBase];

            string n = larva.Name;
            if (larva.BaseName != null)
            {
                n += " : " + larva.BaseName;
            }

            string larvaDeclaration = templateLarvaDeclaration.Replace("%name%", larva.Name).Replace("%base%", larva.BaseName);

            string body = larva.GetStructBody();
            Larvae.ReplaceField(ref larvaDeclaration, "%body%", body);

            larva.Declaration.Add(larvaDeclaration);
            larva.Definitions.Add(larva.GetConstructorDefinition());

            // generate method definition
            foreach (string name in larva.Methods)
            {
                Method m = MethodFactory.GetMethod(name);
                larva.Definitions.Add(m.GetDefinition(larva));
            }
        }

        static void GenerateEnum(Larva larva)
        {
            string enumBody = larva.GetEnumBody();
            string enumDeclaration = GetElement(ElementType.EnumDeclaration);
            
            enumDeclaration = enumDeclaration.Replace("%name%", larva.Name);
            Larvae.ReplaceField(ref enumDeclaration, "%body%", enumBody);

            larva.Declaration.Add(enumDeclaration);
        }

        public static void Generate(string outputFile)
        {
            List<string> output = new List<string>();

            foreach (KeyValuePair<string, Larva> pair in Items)
            {
                Larva l = pair.Value;

                if (l.Mode == LarvaMode.Skip)
                {
                    continue;
                }

                switch (l.Type)
                {
                    case LarvaType.Struct:
                        GenerateStruct(l);
                        break;

                    case LarvaType.Enum:
                        GenerateEnum(l);
                        break;
                }
            }

            Generator.Current.Generate();
        }

        public static string GetFullName(string larvaName)
        {
            string ns = GetElement(ElementType.Namespace);

            if (ns != "")
            {
                larvaName = ns + "." + larvaName;
            }

            return larvaName;
        }
    }
}
