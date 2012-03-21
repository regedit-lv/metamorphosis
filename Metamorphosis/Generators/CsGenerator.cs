using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Metamorphosis
{
    class CsGenerator : Generator
    {
        string OutputFile;
        List<string> declaraion = new List<string>();
        string SourceFile;

        public CsGenerator()
        {
        }

        public override void SetOutputFile(string outputFile)
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

        public override void Generate()
        {
            string ns = Larvae.GetElement(ElementType.Namespace);

            AddDeclarationText(Larvae.GetElement(ElementType.IncludeDeclarationTop));

            foreach (string importName in Larvae.Imports)
            {
                string n = Program.RemoveExtension(importName);
                AddDeclarationText(Larvae.GetElement(ElementType.ImportInclude).Replace("%name%", n));
            }

            AddDeclarationText(Larvae.GetElement(ElementType.UserIncludeDeclarationTop));

            if (ns != null)
            {
                AddDeclarationText("namespace " + ns + Environment.NewLine + "{");
            }

            WriteLarvae(Larvae.Items);

            if (ns != null)
            {
                AddDeclarationText(Environment.NewLine + "}");
            }

            SaveToFile();
        }

        public override string GetVirtualModificator(bool fromBase, bool toChild)
        {
            if (fromBase)
            {
                return "override ";
            }
            else if (toChild)
            {
                return "virtual ";
            }


            return "";
        }

        public override string GetNamespace(string metaNamespace)
        {
            return metaNamespace;
        }

    }
}
