using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Helpers
{
    static class XmlReaderWrapper
    {
        public static bool ReadAttribute(XmlReader reader, string name, out Int32 value)
        {
            value = 0;
            try
            {
                string s = reader.GetAttribute(name);
                if (s != null)
                {
                    value = Convert.ToInt32(s);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static bool ReadAttribute(XmlReader reader, string name, out UInt16 value)
        {
            value = 0;
            try
            {
                string s = reader.GetAttribute(name);
                if (s != null)
                {
                    value = Convert.ToUInt16(s);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static bool ReadAttribute(XmlReader reader, string name, out Int16 value)
        {
            value = 0;
            try
            {
                string s = reader.GetAttribute(name);
                if (s != null)
                {
                    value = Convert.ToInt16(s);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static bool ReadAttribute(XmlReader reader, string name, out UInt64 value)
        {
            value = 0;
            try
            {
                string s = reader.GetAttribute(name);
                if (s != null)
                {
                    value = Convert.ToUInt32(s);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static bool ReadAttribute(XmlReader reader, string name, out bool value)
        {
            value = false;
            try
            {
                string s = reader.GetAttribute(name);
                if (s != null)
                {
                    value = s.ToLower() == "true";
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static bool ReadAttribute(XmlReader reader, string name, out string value)
        {
            value = "";
            try
            {
                string s = reader.GetAttribute(name);
                if (s != null)
                {
                    value = s;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

    }
}
