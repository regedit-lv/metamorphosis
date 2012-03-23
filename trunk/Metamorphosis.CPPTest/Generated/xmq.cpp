
#include "stdafx.h"

#include <sstream>
#include "xmq.h"

namespace xmq
{
namespace bus
{
namespace configuration
{
 
MetaData::MetaData()
{
    
}

 
std::string MetaData::toXml(TiXmlElement *parentNode)
{
    
    TiXmlElement *parent;
    if (parentNode == nullptr)
    {
        parent = new TiXmlElement("MetaData");
    }
    else
    {
        parent = parentNode;
    }
    
    
    { // type
        std::stringstream oss;
        oss << (int)type;
        parent->SetAttribute("type", oss.str().c_str());
    }
    
    
    { // name
        std::stringstream oss;
        oss << name;
        parent->SetAttribute("name", oss.str().c_str());
    }
    
     
    // write array elements
    TiXmlElement * elements_element = new TiXmlElement("elements");
    parent->LinkEndChild(elements_element);
    elements_element->SetAttribute("size", elements.size());
    
    TiXmlElement * parent_elements = parent;
    parent = elements_element;
    
    for (size_t i = 0; i < elements.size(); i++)
    {
        struct xmq::bus::configuration::MetaData &MetaData = elements[i];
    
         
        { // MetaData
            TiXmlElement * element = new TiXmlElement("MetaData");
            std::string xml = MetaData.toXml(element);
            parent->LinkEndChild(element);
        }
        
    }
    
    parent = parent_elements;
    
     
    
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

 
void MetaData::fromXml(const std::string &xml, TiXmlElement *parentNode)
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
    
    
    
    
    // type
    root->Attribute("type", (int*)&type);
    
      
    
    // name
    {
        const char *s = root->Attribute("name");
        if (s != nullptr)
        {
            name = s;
        }
    }
    
     
    
    
     
     
    // read array elements
    for (TiXmlNode *child = root->FirstChild(); child != 0; child = child->NextSibling()) 
    {
        if (_stricmp(child->Value(), "elements") == 0)
        {
            TiXmlElement *element = child->ToElement();
    
            TiXmlElement *originalRoot = root;
    
            for (TiXmlNode *arrayChild = element->FirstChild(); arrayChild != 0; arrayChild = arrayChild->NextSibling()) 
            {
                root = arrayChild->ToElement();
                
                struct xmq::bus::configuration::MetaData sub;
    
                if (_stricmp("MetaData", root->Value()) == 0)
                {
                    
                    // sub
                    sub.fromXml("", root);
                    
    
                    elements.push_back(sub);
                }
            }
    
            root = originalRoot;
    
            break;
        }
    }
    
     
    
    
}

 
Module::Module()
{
    
}

 
std::string Module::toXml(TiXmlElement *parentNode)
{
    
    TiXmlElement *parent;
    if (parentNode == nullptr)
    {
        parent = new TiXmlElement("Module");
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

 
void Module::fromXml(const std::string &xml, TiXmlElement *parentNode)
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
    
    
    
      
    
    // name
    {
        const char *s = root->Attribute("name");
        if (s != nullptr)
        {
            name = s;
        }
    }
    
    
    // id
    {
        const char *s = root->Attribute("id");
        if (s != nullptr)
        {
            id = s;
        }
    }
    
     
    
    
     
     
    
    
}

 
IpRange::IpRange()
{
    
}

 
std::string IpRange::toXml(TiXmlElement *parentNode)
{
    
    TiXmlElement *parent;
    if (parentNode == nullptr)
    {
        parent = new TiXmlElement("IpRange");
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

 
void IpRange::fromXml(const std::string &xml, TiXmlElement *parentNode)
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
    {
        const char *s = root->Attribute("from");
        if (s != nullptr)
        {
            from = s;
        }
    }
    
    
    // to
    {
        const char *s = root->Attribute("to");
        if (s != nullptr)
        {
            to = s;
        }
    }
    
     
    
    
     
     
    
    
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
    {
        const char *s = root->Attribute("name");
        if (s != nullptr)
        {
            name = s;
        }
    }
    
    
    // host
    {
        const char *s = root->Attribute("host");
        if (s != nullptr)
        {
            host = s;
        }
    }
    
     
    
    
     
     
    
    
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
    {
        const char *s = root->Attribute("modules");
        if (s != nullptr)
        {
            modules = s;
        }
    }
    
    
    // modulesData
    {
        const char *s = root->Attribute("modulesData");
        if (s != nullptr)
        {
            modulesData = s;
        }
    }
    
     
    
    
     
     
    
    
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
    {
        const char *s = root->Attribute("host");
        if (s != nullptr)
        {
            host = s;
        }
    }
    
     
    
    
     
     
    
    
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
    
     
    { // metaData
        TiXmlElement * element = new TiXmlElement("metaData");
        std::string xml = metaData.toXml(element);
        parent->LinkEndChild(element);
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
        struct xmq::bus::configuration::IpRange &IpRange = whiteIp[i];
    
         
        { // IpRange
            TiXmlElement * element = new TiXmlElement("IpRange");
            std::string xml = IpRange.toXml(element);
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
        struct xmq::bus::configuration::IpRange &IpRange = blackIp[i];
    
         
        { // IpRange
            TiXmlElement * element = new TiXmlElement("IpRange");
            std::string xml = IpRange.toXml(element);
            parent->LinkEndChild(element);
        }
        
    }
    
    parent = parent_blackIp;
    
     
    // write array modules
    TiXmlElement * modules_element = new TiXmlElement("modules");
    parent->LinkEndChild(modules_element);
    modules_element->SetAttribute("size", modules.size());
    
    TiXmlElement * parent_modules = parent;
    parent = modules_element;
    
    for (size_t i = 0; i < modules.size(); i++)
    {
        struct xmq::bus::configuration::Module &Module = modules[i];
    
         
        { // Module
            TiXmlElement * element = new TiXmlElement("Module");
            std::string xml = Module.toXml(element);
            parent->LinkEndChild(element);
        }
        
    }
    
    parent = parent_modules;
    
     
    
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
    
    
    
      
     
    
    
     
    // metaData
    for (TiXmlNode *child = root->FirstChild(); child != 0; child = child->NextSibling()) 
    {
        if (_stricmp(child->Value(), "metaData") == 0)
        {
            TiXmlElement *element = child->ToElement();
            metaData.fromXml("", element);
            break;
        }
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
    
                if (_stricmp("Connection", root->Value()) == 0)
                {
                    
                    // sub
                    sub.fromXml("", root);
                    
    
                    busses.push_back(sub);
                }
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
                
                struct xmq::bus::configuration::IpRange sub;
    
                if (_stricmp("IpRange", root->Value()) == 0)
                {
                    
                    // sub
                    sub.fromXml("", root);
                    
    
                    whiteIp.push_back(sub);
                }
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
                
                struct xmq::bus::configuration::IpRange sub;
    
                if (_stricmp("IpRange", root->Value()) == 0)
                {
                    
                    // sub
                    sub.fromXml("", root);
                    
    
                    blackIp.push_back(sub);
                }
            }
    
            root = originalRoot;
    
            break;
        }
    }
    
     
    // read array modules
    for (TiXmlNode *child = root->FirstChild(); child != 0; child = child->NextSibling()) 
    {
        if (_stricmp(child->Value(), "modules") == 0)
        {
            TiXmlElement *element = child->ToElement();
    
            TiXmlElement *originalRoot = root;
    
            for (TiXmlNode *arrayChild = element->FirstChild(); arrayChild != 0; arrayChild = arrayChild->NextSibling()) 
            {
                root = arrayChild->ToElement();
                
                struct xmq::bus::configuration::Module sub;
    
                if (_stricmp("Module", root->Value()) == 0)
                {
                    
                    // sub
                    sub.fromXml("", root);
                    
    
                    modules.push_back(sub);
                }
            }
    
            root = originalRoot;
    
            break;
        }
    }
    
     
    
    
}


}

}

}
