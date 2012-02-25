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
        static List<string> Imports = new List<string>();

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
            return Elements.ContainsKey(elementType) ? Larvae.Elements[elementType] : null;
        }

        public static void SetElement(ElementType elementType, string value)
        {
            Larvae.Elements[elementType] = value;
        }

        static public Larva GetLarva(string name, bool existingOnly = false)
        {
            if (Items.ContainsKey(name))
            {
                return Items[name];
            }
            else
            {
                if (existingOnly)
                {
                    return null;
                }

                Larva l = new Larva();
                l.Name = name;
                l.Type = LarvaType.Custom;
                Items[name] = l;
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
            string ol = Elements[ElementType.OutputLanguage];
            string fn = null;
            switch (ol.ToUpper())
            {
                case "C++":
                    fn = "cpp.def";
                    break;
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

        public static string ReplaceSystemFields(string text, Part part)
        {
            while (true)
            {
                // check for auto_id names
                Match match = Regex.Match(text, @"%_[^%]+%", RegexOptions.IgnoreCase);

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

            while (true)
            {
                // check for expression
                Match match = Regex.Match(text, @"%![^%]+%", RegexOptions.IgnoreCase);

                // Here we check the Match instance.
                if (match.Success)
                {
                    string v = match.Captures[0].Value;
                    string n = v.Substring(2, v.Length - 3);
                    n = ObjectHelper.GetValue(part, n);
                    text = text.Replace(v, n);
                }
                else
                {
                    break;
                }
            }

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
                    text = Larvae.ReplaceField(text, v, n);
                }
                else
                {
                    break;
                }
            }

            return text;
        }

        public static string ReplaceField(string text, string field, string value)
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
            return text.Replace(field, value);
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
            larvaDeclaration = Larvae.ReplaceField(larvaDeclaration, "%body%", body);

            larva.Declaration.Add(larvaDeclaration);
            larva.Definitions.Add(larva.GetConstructorDefinition());
            
            foreach (Method m in Methods.Items)
            {
                larva.Definitions.Add(m.GetDefinition(larva));
            }
        }

        static void GenerateEnum(Larva larva)
        {
            string enumBody = larva.GetEnumBody();
            enumBody = AddIndent(enumBody, 2);

            string enumDeclaration = RemoveIndentForField(GetElement(ElementType.EnumDeclaration), "%body%");
            
            enumDeclaration = enumDeclaration.Replace("%body%", enumBody).Replace("%name%", larva.Name);
            
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

            switch (Elements[ElementType.OutputLanguage].ToUpper())
            {
                case "C++":
                    string ns = GetElement(ElementType.Namespace);
                    
                    CppGenerator g = new CppGenerator(outputFile);

                    g.AddDeclarationText(GetElement(ElementType.IncludeDeclarationTop));

                    foreach (string importName in Imports)
                    {
                        string n = Program.RemoveExtension(importName);
                        g.AddDeclarationText(GetElement(ElementType.ImportInclude).Replace("%name%", n));
                    }

                    g.AddDeclarationText(GetElement(ElementType.UserIncludeDeclarationTop));

                    g.AddDefinitionText(GetElement(ElementType.IncludeDefinitionTop).Replace("%output_name%", GetElement(ElementType.OutputName)));

                    if (ns != null)
                    {
                        g.AddDeclarationText("namespace " + ns + Environment.NewLine + "{");
                        g.AddDefinitionText("namespace " + ns + Environment.NewLine + "{");
                    }                    

                    g.WriteLarvae(Items);

                    if (ns != null)
                    {
                        g.AddDeclarationText(Environment.NewLine + "}");
                        g.AddDefinitionText(Environment.NewLine + "}");
                    }

                    g.SaveToFile();
                    break;
            }
        }
    }
}
