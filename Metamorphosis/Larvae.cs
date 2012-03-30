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

        static void GenerateStruct(Larva larva)
        {
            // generate struct
            string structDefinition = larva.BaseName == null ? GetElement(ElementType.StructDefinition) : GetElement(ElementType.StructDefinitionWithBase);

            TextHelper.ReplaceExpressions(ref structDefinition, larva);

            string body = larva.GetStructBody();
            TextHelper.ReplaceField(ref structDefinition, "%body%", body);

            larva.Declaration.Add(structDefinition);
            larva.Definitions.Add(larva.GetConstructorDefinition());

            // generate method definition
            foreach (string name in larva.Methods)
            {
                Method m = MethodFactory.GetMethod(name);
                larva.Definitions.Add(m.GetDefinition(larva));
            }
        }

        static void GenerateStatic(Larva larva)
        {
            // generate struct
            string staticDefinition = GetElement(ElementType.StaticDefinition);

            TextHelper.ReplaceExpressions(ref staticDefinition, larva);

            string body = larva.GetStaticBody();

            TextHelper.ReplaceField(ref staticDefinition, "%body%", body);

            larva.Declaration.Add(staticDefinition);

            // generate field definition
            foreach (Part p in larva.Parts)
            {
                larva.Definitions.Add(p.GetStaticFieldDefinition());
            }

            // generate method definition
            foreach (string name in larva.Methods)
            {
                Method m = MethodFactory.GetMethod(name);
                larva.Definitions.Add(m.GetDefinition(larva));
            }
        }

        static void GenerateEnum(Larva larva)
        {
            string enumDefinition = GetElement(ElementType.EnumDefinition);           
            TextHelper.ReplaceExpressions(ref enumDefinition, larva);

            string enumBody = larva.GetEnumBody();
            TextHelper.ReplaceField(ref enumDefinition, "%body%", enumBody);

            larva.Declaration.Add(enumDefinition);
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

                    case LarvaType.Static:
                        GenerateStatic(l);
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
