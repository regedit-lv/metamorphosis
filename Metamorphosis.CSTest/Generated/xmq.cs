 
using System;
using System.Collections.Generic;
using Helpers;
using System.Xml;


namespace xmq.configuration
{

public enum MetaType : int
{
    Int ,
    String ,
    Array ,
    Object ,
    
}


public class MetaData 
{
    public MetaType type ;
    public string name ;
    public List<xmq.configuration.MetaData> elements ;
    
     
    public MetaData()
    {
        
    }
    
     
    public string toXml()
    {
        
            return "";
        
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
                int i_type;
                XmlReaderWrapper.ReadAttribute(reader, "type", out i_type);
                type = (MetaType)i_type;
            }
            
              
            
            // string name
            XmlReaderWrapper.ReadAttribute(reader, "name", out name);
            
             
            
        
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
                                            xmq.configuration.MetaData sub = new xmq.configuration.MetaData();
                        
                                            
                                            // xmq.configuration.MetaData sub
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


public class Module 
{
    public string name ;
    public string id ;
    
     
    public Module()
    {
        
    }
    
     
    public string toXml()
    {
        
            return "";
        
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
            XmlReaderWrapper.ReadAttribute(reader, "name", out name);
            
            
            // string id
            XmlReaderWrapper.ReadAttribute(reader, "id", out id);
            
             
            
        
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
    public string from ;
    public string to ;
    
     
    public IpRange()
    {
        
    }
    
     
    public string toXml()
    {
        
            return "";
        
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
            XmlReaderWrapper.ReadAttribute(reader, "from", out from);
            
            
            // string to
            XmlReaderWrapper.ReadAttribute(reader, "to", out to);
            
             
            
        
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
    public string name ;
    public int id ;
    public string host ;
    public int port ;
    
     
    public Instance()
    {
        
    }
    
     
    public string toXml()
    {
        
            return "";
        
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
            XmlReaderWrapper.ReadAttribute(reader, "id", out id);
            
            
            // int port
            XmlReaderWrapper.ReadAttribute(reader, "port", out port);
            
            
              
            
            // string name
            XmlReaderWrapper.ReadAttribute(reader, "name", out name);
            
            
            // string host
            XmlReaderWrapper.ReadAttribute(reader, "host", out host);
            
             
            
        
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
    public string modules ;
    public string modulesData ;
    
     
    public Path()
    {
        
    }
    
     
    public string toXml()
    {
        
            return "";
        
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
            XmlReaderWrapper.ReadAttribute(reader, "modules", out modules);
            
            
            // string modulesData
            XmlReaderWrapper.ReadAttribute(reader, "modulesData", out modulesData);
            
             
            
        
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
    public string host ;
    public int port ;
    
     
    public Connection()
    {
        
    }
    
     
    public string toXml()
    {
        
            return "";
        
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
            XmlReaderWrapper.ReadAttribute(reader, "port", out port);
            
            
              
            
            // string host
            XmlReaderWrapper.ReadAttribute(reader, "host", out host);
            
             
            
        
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
    public xmq.configuration.MetaData metaData ;
    public xmq.configuration.Instance instance ;
    public xmq.configuration.Path path ;
    public List<xmq.configuration.Connection> busses ;
    public List<xmq.configuration.IpRange> whiteIp ;
    public List<xmq.configuration.IpRange> blackIp ;
    public List<xmq.configuration.Module> modules ;
    
     
    public Configuration()
    {
        
    }
    
     
    public string toXml()
    {
        
            return "";
        
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
                            metaData.fromXml("", reader);
                        }
                        
                         
                        // struct instance
                        if (reader.Name.ToLower() == "instance".ToLower())
                        {
                            instance.fromXml("", reader);
                        }
                        
                         
                        // struct path
                        if (reader.Name.ToLower() == "path".ToLower())
                        {
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
                                            xmq.configuration.Connection sub = new xmq.configuration.Connection();
                        
                                            
                                            // xmq.configuration.Connection sub
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
                                            xmq.configuration.IpRange sub = new xmq.configuration.IpRange();
                        
                                            
                                            // xmq.configuration.IpRange sub
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
                                            xmq.configuration.IpRange sub = new xmq.configuration.IpRange();
                        
                                            
                                            // xmq.configuration.IpRange sub
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
                                            xmq.configuration.Module sub = new xmq.configuration.Module();
                        
                                            
                                            // xmq.configuration.Module sub
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
