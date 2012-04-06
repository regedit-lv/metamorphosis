using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Metamorphosis
{
    class Generator
    {
        public static Generator Current;

        public static void SetGenerator(string language)
        {
            switch (language.ToUpper())
            {
                case "C++":
                    {
                        Current = new CppGenerator();
                        break;
                    }

                case "C#":
                    {
                        Current = new CsGenerator();
                        break;
                    }

                case "JAVA":
                    {
                        Current = new JavaGenerator();
                        break;
                    }
            }
        }

        public virtual void Generate()
        {
        }

        public virtual void SetOutputFile(string outputFile)
        {
        }

        public virtual void SetOutputPath(string outputPath)
        {
        }

        public virtual string GetVirtualModificator(bool fromBase, bool toChild)
        {
            return "";
        }

        public virtual string GetNamespace(string metaNamespace)
        {
            return "";
        }

    }
}
