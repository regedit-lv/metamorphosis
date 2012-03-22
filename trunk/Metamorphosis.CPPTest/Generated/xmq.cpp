
#include "stdafx.h"

#include "ByteReader.h"
#include "ByteWriter.h"
#include <sstream>
#include "xmq.h"

namespace xmq
{
namespace bus
{
namespace configuration
{
 
IpRagne::IpRagne()
{
    
}

 
std::string IpRagne::toXml(TiXmlElement *parentNode)
{
    
    TiXmlElement *parent;
    if (parentNode == nullptr)
    {
        parent = new TiXmlElement("IpRagne");
    }
    else
    {
        parent = parentNode;
    }
    
    
    { // from
        std::stringstream oss;
        oss << from;
        parent->SetAttribute("from", oss.str().c_str());
    }
    
    
    { // to
        std::stringstream oss;
        oss << to;
        parent->SetAttribute("to", oss.str().c_str());
    }
    
     
    
    if (parentNode == nullptr)
    {
        TiXmlDocument doc;
        doc.LinkEndChild(parent);
    
        TiXmlPrinter printer;
        printer.SetIndent("    ");
        doc.Accept(&printer);
        return printer.CStr();
    }
    else
    {
        return "";
    }
    
}

 
void IpRagne::fromXml(const std::string &xml, TiXmlElement *parentNode)
{
    
    TiXmlElement *root;
    TiXmlDocument doc;
    
    if (parentNode == nullptr)
    {
        doc.Parse(xml.c_str());
        root = doc.RootElement();
    }
    else
    {
        root = parentNode;
    }
    
    
     
    
    // from
    from = root->Attribute("from");
    
    
    // to
    to = root->Attribute("to");
    
     
    
    
     
     
    
    
}

 
Instance::Instance()
{
    
}

 
std::string Instance::toXml(TiXmlElement *parentNode)
{
    
    TiXmlElement *parent;
    if (parentNode == nullptr)
    {
        parent = new TiXmlElement("Instance");
    }
    else
    {
        parent = parentNode;
    }
    
    
    { // name
        std::stringstream oss;
        oss << name;
        parent->SetAttribute("name", oss.str().c_str());
    }
    
    
    { // id
        std::stringstream oss;
        oss << id;
        parent->SetAttribute("id", oss.str().c_str());
    }
    
    
    { // host
        std::stringstream oss;
        oss << host;
        parent->SetAttribute("host", oss.str().c_str());
    }
    
    
    { // port
        std::stringstream oss;
        oss << port;
        parent->SetAttribute("port", oss.str().c_str());
    }
    
     
    
    if (parentNode == nullptr)
    {
        TiXmlDocument doc;
        doc.LinkEndChild(parent);
    
        TiXmlPrinter printer;
        printer.SetIndent("    ");
        doc.Accept(&printer);
        return printer.CStr();
    }
    else
    {
        return "";
    }
    
}

 
void Instance::fromXml(const std::string &xml, TiXmlElement *parentNode)
{
    
    TiXmlElement *root;
    TiXmlDocument doc;
    
    if (parentNode == nullptr)
    {
        doc.Parse(xml.c_str());
        root = doc.RootElement();
    }
    else
    {
        root = parentNode;
    }
    
    
    
    // id
    root->Attribute("id", &id);
    
    
    // port
    root->Attribute("port", &port);
    
     
    
    // name
    name = root->Attribute("name");
    
    
    // host
    host = root->Attribute("host");
    
     
    
    
     
     
    
    
}

 
Path::Path()
{
    
}

 
std::string Path::toXml(TiXmlElement *parentNode)
{
    
    TiXmlElement *parent;
    if (parentNode == nullptr)
    {
        parent = new TiXmlElement("Path");
    }
    else
    {
        parent = parentNode;
    }
    
    
    { // modules
        std::stringstream oss;
        oss << modules;
        parent->SetAttribute("modules", oss.str().c_str());
    }
    
    
    { // modulesData
        std::stringstream oss;
        oss << modulesData;
        parent->SetAttribute("modulesData", oss.str().c_str());
    }
    
     
    
    if (parentNode == nullptr)
    {
        TiXmlDocument doc;
        doc.LinkEndChild(parent);
    
        TiXmlPrinter printer;
        printer.SetIndent("    ");
        doc.Accept(&printer);
        return printer.CStr();
    }
    else
    {
        return "";
    }
    
}

 
void Path::fromXml(const std::string &xml, TiXmlElement *parentNode)
{
    
    TiXmlElement *root;
    TiXmlDocument doc;
    
    if (parentNode == nullptr)
    {
        doc.Parse(xml.c_str());
        root = doc.RootElement();
    }
    else
    {
        root = parentNode;
    }
    
    
     
    
    // modules
    modules = root->Attribute("modules");
    
    
    // modulesData
    modulesData = root->Attribute("modulesData");
    
     
    
    
     
     
    
    
}

 
Connection::Connection()
{
    
}

 
std::string Connection::toXml(TiXmlElement *parentNode)
{
    
    TiXmlElement *parent;
    if (parentNode == nullptr)
    {
        parent = new TiXmlElement("Connection");
    }
    else
    {
        parent = parentNode;
    }
    
    
    { // host
        std::stringstream oss;
        oss << host;
        parent->SetAttribute("host", oss.str().c_str());
    }
    
    
    { // port
        std::stringstream oss;
        oss << port;
        parent->SetAttribute("port", oss.str().c_str());
    }
    
     
    
    if (parentNode == nullptr)
    {
        TiXmlDocument doc;
        doc.LinkEndChild(parent);
    
        TiXmlPrinter printer;
        printer.SetIndent("    ");
        doc.Accept(&printer);
        return printer.CStr();
    }
    else
    {
        return "";
    }
    
}

 
void Connection::fromXml(const std::string &xml, TiXmlElement *parentNode)
{
    
    TiXmlElement *root;
    TiXmlDocument doc;
    
    if (parentNode == nullptr)
    {
        doc.Parse(xml.c_str());
        root = doc.RootElement();
    }
    else
    {
        root = parentNode;
    }
    
    
    
    // port
    root->Attribute("port", &port);
    
     
    
    // host
    host = root->Attribute("host");
    
     
    
    
     
     
    
    
}

 
Configuration::Configuration()
{
    
}

 
std::string Configuration::toXml(TiXmlElement *parentNode)
{
    
    TiXmlElement *parent;
    if (parentNode == nullptr)
    {
        parent = new TiXmlElement("Configuration");
    }
    else
    {
        parent = parentNode;
    }
    
     
    { // instance
        TiXmlElement * element = new TiXmlElement("instance");
        std::string xml = instance.toXml(element);
        parent->LinkEndChild(element);
    }
    
     
    { // path
        TiXmlElement * element = new TiXmlElement("path");
        std::string xml = path.toXml(element);
        parent->LinkEndChild(element);
    }
    
     
    // write array busses
    TiXmlElement * busses_element = new TiXmlElement("busses");
    parent->LinkEndChild(busses_element);
    busses_element->SetAttribute("size", busses.size());
    
    TiXmlElement * parent_busses = parent;
    parent = busses_element;
    
    for (size_t i = 0; i < busses.size(); i++)
    {
        struct xmq::bus::configuration::Connection &Connection = busses[i];
    
         
        { // Connection
            TiXmlElement * element = new TiXmlElement("Connection");
            std::string xml = Connection.toXml(element);
            parent->LinkEndChild(element);
        }
        
    }
    
    parent = parent_busses;
    
     
    // write array whiteIp
    TiXmlElement * whiteIp_element = new TiXmlElement("whiteIp");
    parent->LinkEndChild(whiteIp_element);
    whiteIp_element->SetAttribute("size", whiteIp.size());
    
    TiXmlElement * parent_whiteIp = parent;
    parent = whiteIp_element;
    
    for (size_t i = 0; i < whiteIp.size(); i++)
    {
        struct xmq::bus::configuration::IpRagne &IpRagne = whiteIp[i];
    
         
        { // IpRagne
            TiXmlElement * element = new TiXmlElement("IpRagne");
            std::string xml = IpRagne.toXml(element);
            parent->LinkEndChild(element);
        }
        
    }
    
    parent = parent_whiteIp;
    
     
    // write array blackIp
    TiXmlElement * blackIp_element = new TiXmlElement("blackIp");
    parent->LinkEndChild(blackIp_element);
    blackIp_element->SetAttribute("size", blackIp.size());
    
    TiXmlElement * parent_blackIp = parent;
    parent = blackIp_element;
    
    for (size_t i = 0; i < blackIp.size(); i++)
    {
        struct xmq::bus::configuration::IpRagne &IpRagne = blackIp[i];
    
         
        { // IpRagne
            TiXmlElement * element = new TiXmlElement("IpRagne");
            std::string xml = IpRagne.toXml(element);
            parent->LinkEndChild(element);
        }
        
    }
    
    parent = parent_blackIp;
    
     
    
    if (parentNode == nullptr)
    {
        TiXmlDocument doc;
        doc.LinkEndChild(parent);
    
        TiXmlPrinter printer;
        printer.SetIndent("    ");
        doc.Accept(&printer);
        return printer.CStr();
    }
    else
    {
        return "";
    }
    
}

 
void Configuration::fromXml(const std::string &xml, TiXmlElement *parentNode)
{
    
    TiXmlElement *root;
    TiXmlDocument doc;
    
    if (parentNode == nullptr)
    {
        doc.Parse(xml.c_str());
        root = doc.RootElement();
    }
    else
    {
        root = parentNode;
    }
    
    
     
     
    
    
     
    // instance
    for (TiXmlNode *child = root->FirstChild(); child != 0; child = child->NextSibling()) 
    {
        if (_stricmp(child->Value(), "instance") == 0)
        {
            TiXmlElement *element = child->ToElement();
            instance.fromXml("", element);
            break;
        }
    }
    
    
     
    // path
    for (TiXmlNode *child = root->FirstChild(); child != 0; child = child->NextSibling()) 
    {
        if (_stricmp(child->Value(), "path") == 0)
        {
            TiXmlElement *element = child->ToElement();
            path.fromXml("", element);
            break;
        }
    }
    
    
     
     
    // read array busses
    for (TiXmlNode *child = root->FirstChild(); child != 0; child = child->NextSibling()) 
    {
        if (_stricmp(child->Value(), "busses") == 0)
        {
            TiXmlElement *element = child->ToElement();
    
            TiXmlElement *originalRoot = root;
    
            for (TiXmlNode *arrayChild = element->FirstChild(); arrayChild != 0; arrayChild = arrayChild->NextSibling()) 
            {
                root = arrayChild->ToElement();
                
                struct xmq::bus::configuration::Connection sub;
    
                
                // sub
                sub.fromXml("", root);
                
    
                busses.push_back(sub);
            }
    
            root = originalRoot;
    
            break;
        }
    }
    
     
    // read array whiteIp
    for (TiXmlNode *child = root->FirstChild(); child != 0; child = child->NextSibling()) 
    {
        if (_stricmp(child->Value(), "whiteIp") == 0)
        {
            TiXmlElement *element = child->ToElement();
    
            TiXmlElement *originalRoot = root;
    
            for (TiXmlNode *arrayChild = element->FirstChild(); arrayChild != 0; arrayChild = arrayChild->NextSibling()) 
            {
                root = arrayChild->ToElement();
                
                struct xmq::bus::configuration::IpRagne sub;
    
                
                // sub
                sub.fromXml("", root);
                
    
                whiteIp.push_back(sub);
            }
    
            root = originalRoot;
    
            break;
        }
    }
    
     
    // read array blackIp
    for (TiXmlNode *child = root->FirstChild(); child != 0; child = child->NextSibling()) 
    {
        if (_stricmp(child->Value(), "blackIp") == 0)
        {
            TiXmlElement *element = child->ToElement();
    
            TiXmlElement *originalRoot = root;
    
            for (TiXmlNode *arrayChild = element->FirstChild(); arrayChild != 0; arrayChild = arrayChild->NextSibling()) 
            {
                root = arrayChild->ToElement();
                
                struct xmq::bus::configuration::IpRagne sub;
    
                
                // sub
                sub.fromXml("", root);
                
    
                blackIp.push_back(sub);
            }
    
            root = originalRoot;
    
            break;
        }
    }
    
     
    
    
}


}

}

}
