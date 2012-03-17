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
                        Larvae.ParseLanguageDefinition();
                        i++;
                        break;
                }
            }

            if (Larvae.GetElement(ElementType.OutputLanguage) == null)
            {
                Console.WriteLine("Error: output language is not set. Use -ol to set it. Example: -ol C++");
                return;
            }

            Directory.CreateDirectory(path);
            
            path += @"\";

            string name = Program.RemoveExtension(fileName);

            path += name;

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
