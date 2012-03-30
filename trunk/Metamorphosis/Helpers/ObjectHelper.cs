using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Metamorphosis
{
    class ObjectHelper
    {
        static string GetFirstFieldName(string expression)
        {
            Match match = Regex.Match(expression, @"^[a-zA-Z0-9_]+", RegexOptions.IgnoreCase);

            if (match.Success)
            {
                return match.Captures[0].Value;
            }
            else
            {
                return null;
            }            
        }

        public static string GetValue(object obj, string expression)
        {
            if (obj == null)
            {
                return null;
            }

            string f = GetFirstFieldName(expression);

            if (f == null)
            {
                return null;
            }
            expression = expression.Remove(0, f.Length);

            object fo = null;

            // check is it function call
            if (expression.Length > 0 && expression[0] == '(')
            {
                expression = expression.Remove(0, 2); // remove ()
                // method call
                MethodInfo m = obj.GetType().GetMethod(f);
                fo = m.Invoke(obj, null);
            }
            else
            {
                // check for field
                FieldInfo field = obj.GetType().GetField(f);

                if (field != null)
                {                    
                    fo = field.GetValue(obj);
                }

                // check for property
                PropertyInfo property = obj.GetType().GetProperty(f);
                if (property != null)
                {
                    if (property.CanRead)
                    {
                        fo = property.GetValue(obj, null);
                    }
                }

                if (fo == null)
                {
                    return null;
                }

                if (expression.Length != 0)
                {
                    // get array element
                    if (expression[0] == '[')
                    {
                        expression = expression.Remove(0, 1);
                        int i = expression.IndexOf(']');
                        if (i == -1)
                        {
                            return null;
                        }
                        string si = expression.Substring(0, i);
                        expression = expression.Remove(0, i + 1);
                        try
                        {
                            i = Convert.ToInt32(si);
                            IList a = fo as IList;
                            if (a == null)
                            {
                                return null;
                            }
                            fo = a[i];
                        }
                        catch (Exception)
                        {
                            return null;
                        }
                    }
                }
            }

            if (expression.Length == 0)
            {
                return fo.ToString();
            }

            // remove '.'
            if (expression[0] == '.')
            {
                expression = expression.Remove(0, 1);
            }
            else
            {
                // unexpected
                return "<expected symbol '.' but received '" + expression + "'>";
            }

            return GetValue(fo, expression);
        }

    }
}
