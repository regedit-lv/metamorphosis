using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metamorphosis
{
    enum LarvaType
    {
        Struct,
        Enum,
        Custom,
    };


    class PartVariable
    {
        public string Name;
        public string Value;
    }

    class Part
    {
        public string Name;
        public Larva Larva;
        public string InitialValue;

        List<PartVariable> variables = new List<PartVariable>();

        public string GetVariable(string name)
        {
            PartVariable pv = variables.Find(x => x.Name == name);

            if (pv == null)
            {
                pv = new PartVariable() { Name = name };
                pv.Value = name.Substring(2, name.Length - 3) + "_" + Larvae.GetId();
                variables.Add(pv);
            }

            return pv.Value;
        }
    }


    class Larva
    {
        public List<string> Definitions = new List<string>();
        public List<string> Declaration = new List<string>();
        public LarvaType Type;
        public List<Larva> SubLarvae;
        public string Name;
        public string BaseName;
        public List<Part> Parts = new List<Part>();
        public List<Part> BaseParts = new List<Part>();
        public LarvaMode Mode;
        public TypeDefinition TypeDefinition;
        public bool IsPrimitive
        {
            get
            {
                switch (Type)
                {
                    case LarvaType.Enum:
                    case LarvaType.Struct:
                        return false;

                    default:
                        return true;
                }
            }
        }

        public string GetTypeDefinition()
        {
            string typeName = TypeDefinition.Definition.Replace("%name%", Name);

            if (SubLarvae != null)
            {
                for (int i = 0; i < SubLarvae.Count; i++)
                {
                    Larva sl = SubLarvae[i];
                    string t = "%type" + i.ToString() + "%";
                    typeName = TypeDefinition.Definition.Replace(t, sl.GetTypeDefinition());
                }
            }

            return typeName;
        }

        public string GetStructFieldDefinition(string fieldName)
        {
            string tpd = Larvae.Elements[ElementType.StructFieldDeclaration];
            string d = tpd.Replace("%type%", GetTypeDefinition()).Replace("%field%", fieldName);
            return d;
        }

        public string GetConstructorBody()
        {
            string body = "";
            foreach (Part p in BaseParts)
            {
                if (p.InitialValue != null)
                {
                    string value = p.InitialValue;

                    int i = value.IndexOf(".");
                    if (i != -1)
                    {
                        string b = value.Substring(0, i);
                        Larva l = Larvae.GetLarva(b, true);
                        if (l != null && l.Type == LarvaType.Enum)
                        {
                            value = l.GetEnumValue(value.Remove(0, i + 1));
                        }
                    }

                    body += Larvae.GetElement(ElementType.FieldInitialisation).Replace("%field%", p.Name).Replace("%value%", value) + Environment.NewLine;
                }
            }

            foreach (Part p in Parts)
            {
                if (p.InitialValue != null)
                {
                    string value = p.InitialValue;

                    int i = value.IndexOf(".");
                    if (i != -1)
                    {
                        string b = value.Substring(0, i);
                        Larva l = Larvae.GetLarva(b, true);
                        if (l != null && l.Type == LarvaType.Enum)
                        {
                            value = l.GetEnumValue(value.Remove(0, i + 1));
                        }
                    }

                    body += Larvae.GetElement(ElementType.FieldInitialisation).Replace("%field%", p.Name).Replace("%value%", value) + Environment.NewLine;
                }
            }

            return body;
        }

        public string GetConstructorDefinition()
        {
            string cd = BaseName == null ?
                Larvae.GetElement(ElementType.ConstructorDefinition) :
                Larvae.GetElement(ElementType.ConstructorWithBaseDefinition).Replace("%base%", BaseName);
            cd = cd.Replace("%name%", Name);

            string body = GetConstructorBody();

            cd = Larvae.ReplaceField(cd, "%body%", body);

            return cd;
        }

        public string GetStructBody()
        {
            string body = "";
            
            // add fields
            foreach (Part p in Parts)
            {
                string pd = p.Larva.GetStructFieldDefinition(p.Name);
                body += pd + Environment.NewLine;
            }

            // add constrctor
            string ct = Larvae.GetElement(ElementType.ConstructorDeclaration).Replace("%name%", Name);
            body += Environment.NewLine + ct;
            // add methods
            foreach (Method m in Methods.Items)
            {
                body += Environment.NewLine + m.GetDeclaration(this);
            }

            return body;
        }

        public string GetEnumFieldDefinition(string fieldName, string value)
        {
            string tfd = value == null ?
                Larvae.GetElement(ElementType.EnumFieldDeclaration) :
                Larvae.GetElement(ElementType.EnumFieldWithValueDeclaration);
            string d = tfd.Replace("%value%", value).Replace("%field%", fieldName);
            return d;
        }

        public string GetEnumBody()
        {
            string body = "";

            foreach (Part p in Parts)
            {
                string pd = GetEnumFieldDefinition(p.Name, p.InitialValue);
                body += pd + Environment.NewLine;
            }

            return body;
        }

        public string GetEnumValue(string value)
        {
            return Larvae.GetElement(ElementType.EnumValueDeclaration).Replace("%name%", Name).Replace("%value%", value);
        }
    }
}
