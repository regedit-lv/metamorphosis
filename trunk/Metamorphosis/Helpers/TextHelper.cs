using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Metamorphosis
{
    static class TextHelper
    {
        public static string AddIndent(string text, int n)
        {
            string si = GetIndent(n);
            return AddIndent(text, si);
        }

        public static string AddIndent(string text, string indent)
        {
            text = indent + text.Replace(Environment.NewLine, Environment.NewLine + indent);
            return text;
        }

        public static string GetIndentForField(string text, string field, out bool emptyIndention)
        {
            emptyIndention = true;
            int i = 0;

            i = text.IndexOf(field, i);
            if (i == -1)
            {
                return null;
            }

            int e = i > 0 ? i - 1 : i;

            // search for new line
            while (e > 0 && text.IndexOf(Environment.NewLine, e) != e)
            {
                if (text[e] != ' ' && !Environment.NewLine.Contains(text[e]))
                {
                    emptyIndention = false;
                }
                e--;
            }

            // check for first symbol
            if (e == 0 && text[e] != ' ' && !Environment.NewLine.Contains(text[e]))
            {
                emptyIndention = false;
            }

            if (text.IndexOf(Environment.NewLine, e) == e)
            {
                e += Environment.NewLine.Length;
            }

            text = new String(' ', i - e);

            return text;
        }

        public static string RemoveIndentForField(string text, string field)
        {
            int i = 0;
            while (true)
            {
                i = text.IndexOf(field, i);
                if (i == -1)
                {
                    break;
                }

                int e = i;

                // search for new line
                while (e > 0 && text.IndexOf(Environment.NewLine, e) != e)
                {
                    e--;
                }

                if (text.IndexOf(Environment.NewLine, e) == e)
                {
                    e += Environment.NewLine.Length;
                }

                text = text.Remove(e, i - e);
                i = e + 1;
            }

            return text;
        }

        public static string GetIndent(int index)
        {
            string r = "";

            while (index > 0)
            {
                r += "    ";
                index--;
            }

            return r;
        }

        public static string ReplaceExpressions(ref string text, object obj)
        {
            if (obj != null)
            {
                while (true)
                {
                    // check for owner expression
                    Match match = Regex.Match(text, @"%![^%]+%", RegexOptions.IgnoreCase);

                    // Here we check the Match instance.
                    if (match.Success)
                    {
                        string v = match.Captures[0].Value;
                        string n = v.Substring(2, v.Length - 3);
                        string r = ObjectHelper.GetValue(obj, n);
                        if (r == null)
                        {
                            Log.Error(n, Error.WrongFormat, "Can't parse expression");
                        }
                        text = text.Replace(v, r);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            while (true)
            {
                // check for variable expression
                Match match = Regex.Match(text, @"%@[^%]+%", RegexOptions.IgnoreCase);

                // Here we check the Match instance.
                if (match.Success)
                {
                    string v = match.Captures[0].Value;
                    string n = v.Substring(2, v.Length - 3);
                    ElementType t = Parser.ConvertToElementType(n);
                    n = Larvae.GetElement(t);
                    text = text.Replace(v, n);
                }
                else
                {
                    break;
                }
            }

            return text;
        }

        public static string ReplaceSystemFields(ref string text, Part part)
        {
            while (true)
            {
                // check for auto_id names
                Match match = Regex.Match(text, @"%_[_0-9a-zA-Z]+%", RegexOptions.IgnoreCase);

                // Here we check the Match instance.
                if (match.Success)
                {
                    string v = match.Captures[0].Value;
                    string n = part.GetVariable(v);
                    text = text.Replace(v, n);
                }
                else
                {
                    break;
                }
            }

            TextHelper.ReplaceExpressions(ref text, part);

            while (true)
            {
                // check method variable
                Match match = Regex.Match(text, @"%#[^%]+%", RegexOptions.IgnoreCase);

                // Here we check the Match instance.
                if (match.Success)
                {
                    string v = match.Captures[0].Value;
                    string n = v.Substring(2, v.Length - 3);
                    n = CommandProcessor.Process(n);
                    TextHelper.ReplaceField(ref text, v, n);
                }
                else
                {
                    break;
                }
            }

            return text;
        }

        public static string ReplaceField(ref string text, string field, string value)
        {
            // replace indention symbols inside value
            value = value.Replace("%n%", Environment.NewLine).Replace("%t%", GetIndent(1));
            bool emptyIndention;
            string indent = GetIndentForField(text, field, out emptyIndention);
            if (emptyIndention)
            {
                text = RemoveIndentForField(text, field);
                value = AddIndent(value, indent);
            }
            text = text.Replace(field, value);
            return text;
        }

    }
}
