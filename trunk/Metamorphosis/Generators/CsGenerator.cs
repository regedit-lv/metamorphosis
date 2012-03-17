using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Metamorphosis
{
    class CsGenerator
    {
        string OutputFile;
        List<string> declaraion = new List<string>();
        string SourceFile;

        public CsGenerator(string outputFile)
        {
            OutputFile = outputFile;
            SourceFile = OutputFile + ".cs";
        }

        public void SaveToFile()
        {
            StreamWriter declarationFile = new StreamWriter(SourceFile);

            foreach (string line in declaraion)
            {
                declarationFile.WriteLine(line);
            }

            declarationFile.Close();
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
            }
        }

        public void AddDeclarationText(string text)
        {
            if (text != null)
            {
                declaraion.Add(text.Replace("%n%", Environment.NewLine).Replace("%t%", Larvae.GetIndent(1)));
            }
        }
    }
}
