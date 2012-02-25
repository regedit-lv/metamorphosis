using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metamorphosis
{
    class CppGenerator
    {
        string OutputFile;
        List<string> declaraion = new List<string>();
        List<string> definition = new List<string>();
        string HeaderFile;
        string CppFile;

        public CppGenerator(string outputFile)
        {
            OutputFile = outputFile;
            HeaderFile = OutputFile + ".h";
            CppFile = OutputFile + ".cpp"; 
        }

        public void SaveToFile()
        {
            StreamWriter declarationFile = new StreamWriter(HeaderFile);
            StreamWriter definitionFile = new StreamWriter(CppFile);            

            foreach (string line in declaraion)
            {
                declarationFile.WriteLine(line);
            }

            foreach (string line in definition)
            {
                definitionFile.WriteLine(line);
            }

            declarationFile.Close();
            definitionFile.Close();
        }

        public void WriteLarvae(Dictionary<string, Larva> items)
        {
            foreach (KeyValuePair<string, Larva> pair in items)
            {
                 Larva l = pair.Value;
                 // output declaration
                 foreach (string d in l.Declaration)
                 {
                     AddDeclarationText(d);
                 }

                 // output definition
                 foreach (string d in l.Definitions)
                 {
                     AddDefinitionText(d);
                 }
            }

        }

        public void AddDeclarationText(string text)
        {
            if (text != null)
            {
                declaraion.Add(text.Replace("%n%", Environment.NewLine).Replace("%t%", Larvae.GetIndent(1)));
            }
        }

        public void AddDefinitionText(string text)
        {
            if (text != null)
            {
                definition.Add(text.Replace("%n%", Environment.NewLine).Replace("%t%", Larvae.GetIndent(1)));
            }
        }

    }
}
