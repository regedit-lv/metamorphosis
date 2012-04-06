using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Metamorphosis
{
    class JavaGenerator : Generator
    {
        string OutputFile;
        string OutputPath;
        List<string> declaraion = new List<string>();
        string SourceFile;

        public JavaGenerator()
        {
        }

        public override void SetOutputFile(string outputFile)
        {
            OutputFile = outputFile;
            SourceFile = OutputPath + OutputFile + ".java";
        }

        public override void SetOutputPath(string outputPath)
        {
            OutputPath = outputPath;
        }

        public void SaveToFile()
        {
            StreamWriter declarationFile = new StreamWriter(SourceFile);

            foreach (string line in declaraion)
            {
                declarationFile.WriteLine(line);
            }
            declarationFile.Close();
            declaraion.Clear();
        }

        public void WriteLarvae(Dictionary<string, Larva> items)
        {
            string ns = Larvae.GetElement(ElementType.Namespace);

            foreach (KeyValuePair<string, Larva> pair in items)
            {
                Larva l = pair.Value;

                if (l.Mode == LarvaMode.Skip || l.Declaration.Count == 0)
                {
                    continue;
                }

                SetOutputFile(l.Name);

                if (ns != null)
                {
                    AddDeclarationText("package " + ns + ";");
                }

                AddDeclarationText(Larvae.GetElement(ElementType.IncludeDeclarationTop));
                AddDeclarationText(Larvae.GetElement(ElementType.UserIncludeDeclarationTop));

                // output declaration
                foreach (string d in l.Declaration)
                {
                    AddDeclarationText(d);
                }

                SaveToFile();
            }
        }

        public void AddDeclarationText(string text)
        {
            if (text != null)
            {
                TextHelper.ReplaceExpressions(ref text, null);
                declaraion.Add(text);
            }
        }

        public override void Generate()
        {
            //string ns = Larvae.GetElement(ElementType.Namespace);

            //AddDeclarationText(Larvae.GetElement(ElementType.IncludeDeclarationTop));

            //foreach (string importName in Larvae.Imports)
            //{
            //    string n = Program.RemoveExtension(importName);
            //    AddDeclarationText(Larvae.GetElement(ElementType.ImportInclude));
            //}

            //AddDeclarationText(Larvae.GetElement(ElementType.UserIncludeDeclarationTop));

            //if (ns != null)
            //{
            //    AddDeclarationText("namespace " + ns + Environment.NewLine + "{");
            //}

            WriteLarvae(Larvae.Items);

            //if (ns != null)
            //{
            //    AddDeclarationText(Environment.NewLine + "}");
            //}

        }

        public override string GetVirtualModificator(bool fromBase, bool toChild)
        {
            return "";
        }

        public override string GetNamespace(string metaNamespace)
        {
            return metaNamespace;
        }
    }
}
