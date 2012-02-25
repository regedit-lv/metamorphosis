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
            if (args.Length == 0 || args.Length > 2)
            {
                Console.WriteLine("Usage: " + a + " metafile [output_path_prefix]" + Environment.NewLine + "example: " + a + @" data.met Generated");
                return;
            }

            string fileName = args[0];
            string path = args.Length == 2 ? args[1] : @"Generated";
            
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
