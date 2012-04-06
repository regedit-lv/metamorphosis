using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Metamorphosis
{
    class Program
    {
        public static string ReadAllText(string fileName)
        {
            try
            {
                return File.ReadAllText(fileName);
            }
            catch(Exception)
            {
                Console.WriteLine("Can't read from file: " + fileName);
                return "";
            }
        }

        static void Main(string[] args)
        {
            string a = AppDomain.CurrentDomain.FriendlyName;
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: " + a + " metafile [-op output_path_prefix] -ol language" + Environment.NewLine + "example: " + a + @" data.met -op Generated -ol C#");
                return;
            }

            string fileName = args[0];
            string path = @"Generated";

            for (int i = 1; i < args.Length; i++)
            {
                string next = (i + 1) < args.Length ?  args[i + 1] : "";
                switch (args[i])
                {
                    case "-op":
                        path = next;
                        i++;
                        break;

                    case "-ol":
                        Larvae.SetElement(ElementType.OutputLanguage, next);
                        i++;
                        break;

                    case "-df":
                        Larvae.SetElement(ElementType.DefinitionFile, next);
                        i++;
                        break;

                    case "-id":
                        Directory.SetCurrentDirectory(next);
                        i++;
                        break;

                    case "-on":
                        Larvae.SetElement(ElementType.OutputName, next);
                        i++;
                        break;
                }
            }

            if (Larvae.GetElement(ElementType.OutputLanguage) == "")
            {
                Console.WriteLine("Error: output language is not set. Use -ol to set it. Example: -ol C++");
                return;
            }

            if (Larvae.GetElement(ElementType.OutputName) == "")
            {
                Console.WriteLine("Error: output name is not set. Use -ol to set it. Example: -on generated_from_meta");
                return;
            }

            string name = Larvae.GetElement(ElementType.OutputName);

            Larvae.ParseLanguageDefinition();

            Generator.SetGenerator(Larvae.GetElement(ElementType.OutputLanguage));

            Directory.CreateDirectory(path);
           
            path += @"\";

            Generator.Current.SetOutputPath(path);

            path += name;

            Generator.Current.SetOutputFile(path);
            Larvae.SetElement(ElementType.OutputName, name);

            string larvaeDefinition = ReadAllText(fileName);
            Larvae.ParseMetaDefinition(larvaeDefinition);
            Larvae.Generate(path);
        }

        public static string RemoveExtension(string importName)
        {
            int l = importName.IndexOf('.');
            if (l > 0)
            {
                return importName.Substring(0, l);
            }

            return importName;
        }
    }
}
