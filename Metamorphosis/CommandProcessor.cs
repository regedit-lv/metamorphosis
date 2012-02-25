using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Metamorphosis
{
    static class CommandProcessor
    {
        public static string Process(string command)
        {
            string[] p = command.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
            string[] v = p[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (v[0] == "method")
            {
                if (v.Length < 3)
                {
                    return "";
                }
                VariableList vl = Methods.GetMethod(v[1]).Variables;
                //Variable variable = vl.GetVariable(v[2]);

                for (int i = 2; i < v.Length; i++ )
                {
                    Variable nv = vl.GetVariable(v[i], true);
                    if (nv == null)
                    {
                        return "";
                    }
                    vl = nv.Variables;
                }

                Larva l = Larvae.GetLarva(p[2], true);
                if (l == null)
                {
                    return "<error:larva not found with name " + p[2] + ">";
                }
                
                Part part = new Part();
                part.Name = p[1];
                part.Larva = l;
                string value = vl.ReplacePart(part);    
                //value = Larvae.ReplaceSystemFields(value, part).Replace("%type%", part.Larva.GetTypeDefinition()).Replace("%field%", part.Name);
                return value;
            }

            return "";
        }
    }
}
