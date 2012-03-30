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
        Static,
        Enum,
        Custom,
    };

    enum PartMode
    {
        Generate,
        Skip,
    }

    class PartVariable
    {
        public string Name;
        public string Value;
    }

    class Part
    {
        public PartMode Mode = PartMode.Generate;
        public Larva Parent;
        public string Name;
        public Larva Larva;
        public string InitialValue;
        public string Description;

        List<PartVariable> variables = new List<PartVariable>();

        public string Alias
        {
            get
            {
                return Description == null ? Name : Description;
            }
        }

        public string EnumValue
        {
            get
            {
                string d = Larvae.GetElement(ElementType.EnumValueDeclaration);
                return TextHelper.ReplaceExpressions(ref d, this);
            }
        }

        public string TypeDefinition
        {
            get
            {
                return Larva.TypeDefinition;
            }
        }

        public string InitialValueDefinition
        {
            get
            {
                string value = InitialValue;

                int i = value.IndexOf(".");
                if (i != -1)
                {
                    string b = value.Substring(0, i);
                    Larva l = Larvae.GetLarva(b, true);
                    if (l != null && l.Type == LarvaType.Enum)
                    {
                        value = value.Remove(0, i + 1);
                        Part p = Larva.Parts.Find(x => x.Name == value);
                        if (p != null)
                        {
                            InitialValue = p.EnumValue;
                        }
                        else
                        {
                            Log.Error(value, Error.NotFound, "Enum not found");
                        }
                    }
                    else
                    {
                        Log.Error(b, Error.NotFound, "Enum not found");
                    }
                }

                return value;
            }
        }

        public Part(Larva parent)
        {
            Parent = parent;
        }

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

        public string GetStructFieldDeclaration()
        {
            string tpd = Larvae.GetElement(ElementType.StructFieldDeclaration);
            return TextHelper.ReplaceExpressions(ref tpd, this);
        }

        public string GetStaticFieldDeclaration()
        {
            string d = null;

            if (InitialValue == null)
            {
                d = Larvae.GetElement(ElementType.StaticFieldDeclaration);
            }
            else
            {
                d = Larvae.GetElement(ElementType.StaticFieldDeclarationWithValue);
                if (d == "")
                {
                    d = Larvae.GetElement(ElementType.StaticFieldDeclaration);
                }
            }

            return TextHelper.ReplaceExpressions(ref d, this);            
        }

        public string GetStaticFieldDefinition()
        {
            string d = null;

            if (InitialValue == null)
            {
                d = Larvae.GetElement(ElementType.StaticFieldDefinition);
            }
            else
            {
                d = Larvae.GetElement(ElementType.StaticFieldDefinitionWithValue);
                if (d == "")
                {
                    d = Larvae.GetElement(ElementType.StaticFieldDefinition);
                }
            }
            return TextHelper.ReplaceExpressions(ref d, this);
        }

        public string GetEnumFieldDefinition()
        {
            string d = InitialValue == null ?
                Larvae.GetElement(ElementType.EnumFieldDeclaration) :
                Larvae.GetElement(ElementType.EnumFieldWithValueDeclaration);

            return TextHelper.ReplaceExpressions(ref d, this);
        }

        public string GetFieldInitialisation()
        {
            string d = Larvae.GetElement(ElementType.FieldInitialisation);
            return TextHelper.ReplaceExpressions(ref d, this);
        }
    }


    class Larva
    {
        public List<string> Definitions = new List<string>();
        public List<string> Declaration = new List<string>();
        public List<string> Methods = new List<string>();
        public LarvaType Type;
        public List<Larva> SubLarvae;
        public string Name;
        public string FullName;
        public string BaseName;
        public string Namespace;
        public List<Part> Parts = new List<Part>();
        public List<Part> BaseParts = new List<Part>();
        public LarvaMode Mode;
        public TypeInfo TypeInfo;
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

        public string TypeDefinition
        {
            get
            {
                if (TypeInfo == null)
                {
                    return "";
                }

                string d = TypeInfo.Definition;
                TextHelper.ReplaceExpressions(ref d, this);

                return d;
            }
        }

        public string NamespaceDefinition
        {
            get
            {
                return Generator.Current.GetNamespace(Namespace);
            }
        }

        public Larva GetBaseLarva()
        {
            return Larvae.GetLarva(BaseName, true);
        }

        public string GetConstructorBody()
        {
            string body = "";
            foreach (Part p in BaseParts)
            {
                if (p.InitialValue != null)
                {
                    body += p.GetFieldInitialisation() + Environment.NewLine;
                }
            }

            foreach (Part p in Parts)
            {
                if (p.InitialValue != null)
                {
                    body += p.GetFieldInitialisation() + Environment.NewLine;
                }
            }

            return body;
        }

        public string GetConstructorDefinition()
        {
            string d = BaseName == null ?
                Larvae.GetElement(ElementType.ConstructorDefinition) :
                Larvae.GetElement(ElementType.ConstructorWithBaseDefinition);
            TextHelper.ReplaceExpressions(ref d, this);

            string body = GetConstructorBody();

            TextHelper.ReplaceField(ref d, "%body%", body);

            return d;
        }

        public string GetConstructorDeclaration()
        {
            string d = BaseName == null ?
                Larvae.GetElement(ElementType.ConstructorDeclaration) :
                Larvae.GetElement(ElementType.ConstructorWithBaseDeclaration);
            TextHelper.ReplaceExpressions(ref d, this);

            string body = GetConstructorBody();

            TextHelper.ReplaceField(ref d, "%body%", body);

            return d;
        }

        public string GetStructBody()
        {
            string body = "";

            // add fields
            foreach (Part p in Parts)
            {
                string pd = p.GetStructFieldDeclaration();
                body += pd + Environment.NewLine;
            }

            // add constrctor
            string ct = GetConstructorDeclaration();
            body += Environment.NewLine + ct;

            // add methods
            foreach (string name in Methods)
            {
                Method m = MethodFactory.GetMethod(name);
                body += Environment.NewLine + m.GetDeclaration(this);
            }

            return body;
        }

        public string GetStaticBody()
        {
            string body = "";

            // add fields
            foreach (Part p in Parts)
            {
                string pd = p.GetStaticFieldDeclaration();
                body += pd + Environment.NewLine;
            }

            // add methods
            foreach (string name in Methods)
            {
                Method m = MethodFactory.GetMethod(name);
                body += Environment.NewLine + m.GetDeclaration(this);
            }

            return body;
        }

        public string GetEnumBody()
        {
            string body = "";

            foreach (Part p in Parts)
            {
                string pd = p.GetEnumFieldDefinition();
                body += pd + Environment.NewLine;
            }

            return body;
        }
    }
}
