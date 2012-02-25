﻿using System;
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

            if (expression.Length > 0 && expression[0] == '(')
            {
                // method call
                MethodInfo m = obj.GetType().GetMethod(f);
                string s = m.Invoke(obj, null) as string;
                return s;
            }

            FieldInfo field = obj.GetType().GetField(f);

            if (field == null)
            {
                return null;
            }

            object fo = field.GetValue(obj);            

            if (expression.Length == 0)
            {
                return fo.ToString();
            }
            else
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

                // remove '.'
                if (expression[0] == '.')
                {
                    expression = expression.Remove(0, 1);
                }
            }
            return GetValue(fo, expression);
        }

    }
}
