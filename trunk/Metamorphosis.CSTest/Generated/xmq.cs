 
using System;
using System.Collections.Generic;
using Helpers;
using System.Xml;
using System.IO;
using System.Xml.Linq;


namespace xmq.configuration
{

public enum MetaType : int
{
     None , 
     Int , 
     String , 
     Array , 
     Object , 
    
}


static class MetaTypeHelper 
{
     public static bool initDone = false; 
     public static Dictionary<MetaType, string> valueToName; 
     public static Dictionary<string, MetaType> nameToValue; 
    
     
    public static string getEnumName(MetaType value)
    {
        
        if (!initDone)
        {
            initEnum();
        }
        return valueToName[value];
        
    }
    
     
    public static MetaType getEnumValue(string name)
    {
        
        if (!initDone)
        {
            initEnum();
        }
        return nameToValue[name];
        
    }
    
     
    public static void initEnum()
    {
        
            valueToName = new Dictionary<MetaType, string>();
            nameToValue = new Dictionary<string, MetaType>();
            
            valueToName[ MetaType.None ] = "none";
            nameToValue["none"] =  MetaType.None ;
            
            
            valueToName[ MetaType.Int ] = "int";
            nameToValue["int"] =  MetaType.Int ;
            
            
            valueToName[ MetaType.String ] = "string";
            nameToValue["string"] =  MetaType.String ;
            
            
            valueToName[ MetaType.Array ] = "array";
            nameToValue["array"] =  MetaType.Array ;
            
            
            valueToName[ MetaType.Object ] = "object";
            nameToValue["object"] =  MetaType.Object ;
            
            
            initDone = true;
        
    }
     
}


public class MetaData 
{
     public MetaType type; 
     public string name; 
     public List<xmq.configuration.MetaData> elements; 
    
     
    public MetaData()
    {
         type = MetaType.None; 
        
    }
    
     
    public string toXml(XmlWriter parentWriter = null)
    {
        
            StringWriter sw = null;
            XmlWriter writer = null;
        
            if (parentWriter == null)
            {
                sw = new StringWriter();
                writer = XmlWriter.Create(sw);
                writer.WriteStartDocument();
                writer.WriteStartElement("MetaData");
            }
            else
            {
                writer = parentWriter;
            }
        
            // write primitive types
            
            
            
            // MetaType type
            XmlWrapper.WriteAttribute(writer, "type", MetaTypeHelper.getEnumName(type));
            
              
            
            // string name
            XmlWrapper.WriteAttribute(writer, "name", name);
            
             
            
        
            // write complex types
             
            
            // elements
            if (elements != null)
            {
                writer.WriteStartElement("elements");
            
                foreach (var element_1 in elements)
                {
                    writer.WriteStartElement("MetaData");
            
                    
                    // element_1
                    element_1.toXml(writer);
                    
                
                    writer.WriteEndElement();
                }
                               
                writer.WriteEndElement();
            }
            
             
            
            if (parentWriter == null)
            {
                writer.WriteEndElement();
                writer.Flush();
                string xml = sw.ToString();
                return XElement.Parse(xml).ToString();
            }
            else
            {
                return null;
            }
        
    }
    
     
    public void fromXml(string xml, XmlReader parentReader = null)
    {
        
            XmlReader reader;
            
            if (parentReader != null)
            {
                reader = parentReader;
            }
            else
            {
                reader = XmlReader.Create(new System.IO.StringReader(xml));
            }
            
            if (!reader.IsStartElement())
            {
                // error must be start element
                return;
            }
        
            string topName = reader.Name;
        
            
            
            
            // MetaType type
            {
                string i_type;
                XmlWrapper.ReadAttribute(reader, "type", out i_type);
                type = MetaTypeHelper.getEnumValue(i_type);
            }
            
              
            
            // string name
            XmlWrapper.ReadAttribute(reader, "name", out name);
            
             
            
        
            // read internal objects
            if (!reader.IsEmptyElement)
            {
                while(reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                         
                         
                        // read array elements
                        if (reader.Name.ToLower() == "elements".ToLower())
                        {
                            elements = new List<xmq.configuration.MetaData>();
                            if (!reader.IsEmptyElement)
                            {
                                while (reader.Read())
                                {
                                    if (reader.IsStartElement())
                                    {
                                        if ("MetaData".ToLower() == reader.Name.ToLower())
                                        {
                                            xmq.configuration.MetaData sub;
                        
                                            
                                            // xmq.configuration.MetaData sub
                                            sub = new xmq.configuration.MetaData();
                                            sub.fromXml("", reader);
                                            
                        
                                            elements.Add(sub);
                                        }
                                    }       
                                    if (reader.Name.ToLower() == "elements".ToLower() && reader.NodeType == XmlNodeType.EndElement)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        
                         
                    }
                    else if (reader.Name == topName && reader.NodeType == XmlNodeType.EndElement)
                    {
                        break;
                    }
                }
            }
        
    }
     
}


public class MetaData2 : MetaData
{
    
     
    public MetaData2() :base()
    {
        
    }
     
};


public class Module 
{
     public string name; 
     public string id; 
    
     
    public Module()
    {
        
    }
    
     
    public string toXml(XmlWriter parentWriter = null)
    {
        
            StringWriter sw = null;
            XmlWriter writer = null;
        
            if (parentWriter == null)
            {
                sw = new StringWriter();
                writer = XmlWriter.Create(sw);
                writer.WriteStartDocument();
                writer.WriteStartElement("Module");
            }
            else
            {
                writer = parentWriter;
            }
        
            // write primitive types
            
            
              
            
            // string name
            XmlWrapper.WriteAttribute(writer, "name", name);
            
            
            // string id
            XmlWrapper.WriteAttribute(writer, "id", id);
            
             
            
        
            // write complex types
             
             
            
            if (parentWriter == null)
            {
                writer.WriteEndElement();
                writer.Flush();
                string xml = sw.ToString();
                return XElement.Parse(xml).ToString();
            }
            else
            {
                return null;
            }
        
    }
    
     
    public void fromXml(string xml, XmlReader parentReader = null)
    {
        
            XmlReader reader;
            
            if (parentReader != null)
            {
                reader = parentReader;
            }
            else
            {
                reader = XmlReader.Create(new System.IO.StringReader(xml));
            }
            
            if (!reader.IsStartElement())
            {
                // error must be start element
                return;
            }
        
            string topName = reader.Name;
        
            
            
              
            
            // string name
            XmlWrapper.ReadAttribute(reader, "name", out name);
            
            
            // string id
            XmlWrapper.ReadAttribute(reader, "id", out id);
            
             
            
        
            // read internal objects
            if (!reader.IsEmptyElement)
            {
                while(reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                         
                         
                    }
                    else if (reader.Name == topName && reader.NodeType == XmlNodeType.EndElement)
                    {
                        break;
                    }
                }
            }
        
    }
     
}


public class IpRange 
{
     public string from; 
     public string to; 
    
     
    public IpRange()
    {
        
    }
    
     
    public string toXml(XmlWriter parentWriter = null)
    {
        
            StringWriter sw = null;
            XmlWriter writer = null;
        
            if (parentWriter == null)
            {
                sw = new StringWriter();
                writer = XmlWriter.Create(sw);
                writer.WriteStartDocument();
                writer.WriteStartElement("IpRange");
            }
            else
            {
                writer = parentWriter;
            }
        
            // write primitive types
            
            
              
            
            // string from
            XmlWrapper.WriteAttribute(writer, "from", from);
            
            
            // string to
            XmlWrapper.WriteAttribute(writer, "to", to);
            
             
            
        
            // write complex types
             
             
            
            if (parentWriter == null)
            {
                writer.WriteEndElement();
                writer.Flush();
                string xml = sw.ToString();
                return XElement.Parse(xml).ToString();
            }
            else
            {
                return null;
            }
        
    }
    
     
    public void fromXml(string xml, XmlReader parentReader = null)
    {
        
            XmlReader reader;
            
            if (parentReader != null)
            {
                reader = parentReader;
            }
            else
            {
                reader = XmlReader.Create(new System.IO.StringReader(xml));
            }
            
            if (!reader.IsStartElement())
            {
                // error must be start element
                return;
            }
        
            string topName = reader.Name;
        
            
            
              
            
            // string from
            XmlWrapper.ReadAttribute(reader, "from", out from);
            
            
            // string to
            XmlWrapper.ReadAttribute(reader, "to", out to);
            
             
            
        
            // read internal objects
            if (!reader.IsEmptyElement)
            {
                while(reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                         
                         
                    }
                    else if (reader.Name == topName && reader.NodeType == XmlNodeType.EndElement)
                    {
                        break;
                    }
                }
            }
        
    }
     
}


public class Instance 
{
     public string name; 
     public int id; 
     public string host; 
     public int port; 
    
     
    public Instance()
    {
        
    }
    
     
    public string toXml(XmlWriter parentWriter = null)
    {
        
            StringWriter sw = null;
            XmlWriter writer = null;
        
            if (parentWriter == null)
            {
                sw = new StringWriter();
                writer = XmlWriter.Create(sw);
                writer.WriteStartDocument();
                writer.WriteStartElement("Instance");
            }
            else
            {
                writer = parentWriter;
            }
        
            // write primitive types
            
            
            // int id
            XmlWrapper.WriteAttribute(writer, "id", id);
            
            
            // int port
            XmlWrapper.WriteAttribute(writer, "port", port);
            
            
              
            
            // string name
            XmlWrapper.WriteAttribute(writer, "name", name);
            
            
            // string host
            XmlWrapper.WriteAttribute(writer, "host", host);
            
             
            
        
            // write complex types
             
             
            
            if (parentWriter == null)
            {
                writer.WriteEndElement();
                writer.Flush();
                string xml = sw.ToString();
                return XElement.Parse(xml).ToString();
            }
            else
            {
                return null;
            }
        
    }
    
     
    public void fromXml(string xml, XmlReader parentReader = null)
    {
        
            XmlReader reader;
            
            if (parentReader != null)
            {
                reader = parentReader;
            }
            else
            {
                reader = XmlReader.Create(new System.IO.StringReader(xml));
            }
            
            if (!reader.IsStartElement())
            {
                // error must be start element
                return;
            }
        
            string topName = reader.Name;
        
            
            
            // int id
            XmlWrapper.ReadAttribute(reader, "id", out id);
            
            
            // int port
            XmlWrapper.ReadAttribute(reader, "port", out port);
            
            
              
            
            // string name
            XmlWrapper.ReadAttribute(reader, "name", out name);
            
            
            // string host
            XmlWrapper.ReadAttribute(reader, "host", out host);
            
             
            
        
            // read internal objects
            if (!reader.IsEmptyElement)
            {
                while(reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                         
                         
                    }
                    else if (reader.Name == topName && reader.NodeType == XmlNodeType.EndElement)
                    {
                        break;
                    }
                }
            }
        
    }
     
}


public class Path 
{
     public string modules; 
     public string modulesData; 
    
     
    public Path()
    {
        
    }
    
     
    public string toXml(XmlWriter parentWriter = null)
    {
        
            StringWriter sw = null;
            XmlWriter writer = null;
        
            if (parentWriter == null)
            {
                sw = new StringWriter();
                writer = XmlWriter.Create(sw);
                writer.WriteStartDocument();
                writer.WriteStartElement("Path");
            }
            else
            {
                writer = parentWriter;
            }
        
            // write primitive types
            
            
              
            
            // string modules
            XmlWrapper.WriteAttribute(writer, "modules", modules);
            
            
            // string modulesData
            XmlWrapper.WriteAttribute(writer, "modulesData", modulesData);
            
             
            
        
            // write complex types
             
             
            
            if (parentWriter == null)
            {
                writer.WriteEndElement();
                writer.Flush();
                string xml = sw.ToString();
                return XElement.Parse(xml).ToString();
            }
            else
            {
                return null;
            }
        
    }
    
     
    public void fromXml(string xml, XmlReader parentReader = null)
    {
        
            XmlReader reader;
            
            if (parentReader != null)
            {
                reader = parentReader;
            }
            else
            {
                reader = XmlReader.Create(new System.IO.StringReader(xml));
            }
            
            if (!reader.IsStartElement())
            {
                // error must be start element
                return;
            }
        
            string topName = reader.Name;
        
            
            
              
            
            // string modules
            XmlWrapper.ReadAttribute(reader, "modules", out modules);
            
            
            // string modulesData
            XmlWrapper.ReadAttribute(reader, "modulesData", out modulesData);
            
             
            
        
            // read internal objects
            if (!reader.IsEmptyElement)
            {
                while(reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                         
                         
                    }
                    else if (reader.Name == topName && reader.NodeType == XmlNodeType.EndElement)
                    {
                        break;
                    }
                }
            }
        
    }
     
}


public class Connection 
{
     public string host; 
     public int port; 
    
     
    public Connection()
    {
        
    }
    
     
    public string toXml(XmlWriter parentWriter = null)
    {
        
            StringWriter sw = null;
            XmlWriter writer = null;
        
            if (parentWriter == null)
            {
                sw = new StringWriter();
                writer = XmlWriter.Create(sw);
                writer.WriteStartDocument();
                writer.WriteStartElement("Connection");
            }
            else
            {
                writer = parentWriter;
            }
        
            // write primitive types
            
            
            // int port
            XmlWrapper.WriteAttribute(writer, "port", port);
            
            
              
            
            // string host
            XmlWrapper.WriteAttribute(writer, "host", host);
            
             
            
        
            // write complex types
             
             
            
            if (parentWriter == null)
            {
                writer.WriteEndElement();
                writer.Flush();
                string xml = sw.ToString();
                return XElement.Parse(xml).ToString();
            }
            else
            {
                return null;
            }
        
    }
    
     
    public void fromXml(string xml, XmlReader parentReader = null)
    {
        
            XmlReader reader;
            
            if (parentReader != null)
            {
                reader = parentReader;
            }
            else
            {
                reader = XmlReader.Create(new System.IO.StringReader(xml));
            }
            
            if (!reader.IsStartElement())
            {
                // error must be start element
                return;
            }
        
            string topName = reader.Name;
        
            
            
            // int port
            XmlWrapper.ReadAttribute(reader, "port", out port);
            
            
              
            
            // string host
            XmlWrapper.ReadAttribute(reader, "host", out host);
            
             
            
        
            // read internal objects
            if (!reader.IsEmptyElement)
            {
                while(reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                         
                         
                    }
                    else if (reader.Name == topName && reader.NodeType == XmlNodeType.EndElement)
                    {
                        break;
                    }
                }
            }
        
    }
     
}


public class Configuration 
{
     public xmq.configuration.MetaData metaData; 
     public xmq.configuration.Instance instance; 
     public xmq.configuration.Path path; 
     public List<xmq.configuration.Connection> busses; 
     public List<xmq.configuration.IpRange> whiteIp; 
     public List<xmq.configuration.IpRange> blackIp; 
     public List<xmq.configuration.Module> modules; 
    
     
    public Configuration()
    {
        
    }
    
     
    public string toXml(XmlWriter parentWriter = null)
    {
        
            StringWriter sw = null;
            XmlWriter writer = null;
        
            if (parentWriter == null)
            {
                sw = new StringWriter();
                writer = XmlWriter.Create(sw);
                writer.WriteStartDocument();
                writer.WriteStartElement("Configuration");
            }
            else
            {
                writer = parentWriter;
            }
        
            // write primitive types
            
            
              
             
            
        
            // write complex types
            
            // metaData
            writer.WriteStartElement("metaData");
            metaData.toXml(writer);
            writer.WriteEndElement();
            
            
            // instance
            writer.WriteStartElement("instance");
            instance.toXml(writer);
            writer.WriteEndElement();
            
            
            // path
            writer.WriteStartElement("path");
            path.toXml(writer);
            writer.WriteEndElement();
            
             
            
            // busses
            if (busses != null)
            {
                writer.WriteStartElement("busses");
            
                foreach (var element_2 in busses)
                {
                    writer.WriteStartElement("Connection");
            
                    
                    // element_2
                    element_2.toXml(writer);
                    
                
                    writer.WriteEndElement();
                }
                               
                writer.WriteEndElement();
            }
            
            
            // whiteIp
            if (whiteIp != null)
            {
                writer.WriteStartElement("whiteIp");
            
                foreach (var element_3 in whiteIp)
                {
                    writer.WriteStartElement("IpRange");
            
                    
                    // element_3
                    element_3.toXml(writer);
                    
                
                    writer.WriteEndElement();
                }
                               
                writer.WriteEndElement();
            }
            
            
            // blackIp
            if (blackIp != null)
            {
                writer.WriteStartElement("blackIp");
            
                foreach (var element_4 in blackIp)
                {
                    writer.WriteStartElement("IpRange");
            
                    
                    // element_4
                    element_4.toXml(writer);
                    
                
                    writer.WriteEndElement();
                }
                               
                writer.WriteEndElement();
            }
            
            
            // modules
            if (modules != null)
            {
                writer.WriteStartElement("modules");
            
                foreach (var element_5 in modules)
                {
                    writer.WriteStartElement("Module");
            
                    
                    // element_5
                    element_5.toXml(writer);
                    
                
                    writer.WriteEndElement();
                }
                               
                writer.WriteEndElement();
            }
            
             
            
            if (parentWriter == null)
            {
                writer.WriteEndElement();
                writer.Flush();
                string xml = sw.ToString();
                return XElement.Parse(xml).ToString();
            }
            else
            {
                return null;
            }
        
    }
    
     
    public void fromXml(string xml, XmlReader parentReader = null)
    {
        
            XmlReader reader;
            
            if (parentReader != null)
            {
                reader = parentReader;
            }
            else
            {
                reader = XmlReader.Create(new System.IO.StringReader(xml));
            }
            
            if (!reader.IsStartElement())
            {
                // error must be start element
                return;
            }
        
            string topName = reader.Name;
        
            
            
              
             
            
        
            // read internal objects
            if (!reader.IsEmptyElement)
            {
                while(reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                         
                        // struct metaData
                        if (reader.Name.ToLower() == "metaData".ToLower())
                        {
                            metaData = new xmq.configuration.MetaData();
                            metaData.fromXml("", reader);
                        }
                        
                         
                        // struct instance
                        if (reader.Name.ToLower() == "instance".ToLower())
                        {
                            instance = new xmq.configuration.Instance();
                            instance.fromXml("", reader);
                        }
                        
                         
                        // struct path
                        if (reader.Name.ToLower() == "path".ToLower())
                        {
                            path = new xmq.configuration.Path();
                            path.fromXml("", reader);
                        }
                        
                         
                         
                        // read array busses
                        if (reader.Name.ToLower() == "busses".ToLower())
                        {
                            busses = new List<xmq.configuration.Connection>();
                            if (!reader.IsEmptyElement)
                            {
                                while (reader.Read())
                                {
                                    if (reader.IsStartElement())
                                    {
                                        if ("Connection".ToLower() == reader.Name.ToLower())
                                        {
                                            xmq.configuration.Connection sub;
                        
                                            
                                            // xmq.configuration.Connection sub
                                            sub = new xmq.configuration.Connection();
                                            sub.fromXml("", reader);
                                            
                        
                                            busses.Add(sub);
                                        }
                                    }       
                                    if (reader.Name.ToLower() == "busses".ToLower() && reader.NodeType == XmlNodeType.EndElement)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        
                         
                        // read array whiteIp
                        if (reader.Name.ToLower() == "whiteIp".ToLower())
                        {
                            whiteIp = new List<xmq.configuration.IpRange>();
                            if (!reader.IsEmptyElement)
                            {
                                while (reader.Read())
                                {
                                    if (reader.IsStartElement())
                                    {
                                        if ("IpRange".ToLower() == reader.Name.ToLower())
                                        {
                                            xmq.configuration.IpRange sub;
                        
                                            
                                            // xmq.configuration.IpRange sub
                                            sub = new xmq.configuration.IpRange();
                                            sub.fromXml("", reader);
                                            
                        
                                            whiteIp.Add(sub);
                                        }
                                    }       
                                    if (reader.Name.ToLower() == "whiteIp".ToLower() && reader.NodeType == XmlNodeType.EndElement)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        
                         
                        // read array blackIp
                        if (reader.Name.ToLower() == "blackIp".ToLower())
                        {
                            blackIp = new List<xmq.configuration.IpRange>();
                            if (!reader.IsEmptyElement)
                            {
                                while (reader.Read())
                                {
                                    if (reader.IsStartElement())
                                    {
                                        if ("IpRange".ToLower() == reader.Name.ToLower())
                                        {
                                            xmq.configuration.IpRange sub;
                        
                                            
                                            // xmq.configuration.IpRange sub
                                            sub = new xmq.configuration.IpRange();
                                            sub.fromXml("", reader);
                                            
                        
                                            blackIp.Add(sub);
                                        }
                                    }       
                                    if (reader.Name.ToLower() == "blackIp".ToLower() && reader.NodeType == XmlNodeType.EndElement)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        
                         
                        // read array modules
                        if (reader.Name.ToLower() == "modules".ToLower())
                        {
                            modules = new List<xmq.configuration.Module>();
                            if (!reader.IsEmptyElement)
                            {
                                while (reader.Read())
                                {
                                    if (reader.IsStartElement())
                                    {
                                        if ("Module".ToLower() == reader.Name.ToLower())
                                        {
                                            xmq.configuration.Module sub;
                        
                                            
                                            // xmq.configuration.Module sub
                                            sub = new xmq.configuration.Module();
                                            sub.fromXml("", reader);
                                            
                        
                                            modules.Add(sub);
                                        }
                                    }       
                                    if (reader.Name.ToLower() == "modules".ToLower() && reader.NodeType == XmlNodeType.EndElement)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        
                         
                    }
                    else if (reader.Name == topName && reader.NodeType == XmlNodeType.EndElement)
                    {
                        break;
                    }
                }
            }
        
    }
     
}


}
