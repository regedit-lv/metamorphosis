
#include "stdafx.h"

#include "ByteReader.h"
#include "ByteWriter.h"
#include <sstream>
#include "xmq.h"

namespace xmq
{
 
SubStruct::SubStruct()
{
    
}

 
size_t SubStruct::size(void)
{
    
    
    
    return 0  + sizeof(int)
     + (2 + subString.size())
    ;
    
}

 
size_t SubStruct::write(void **data)
{
    
    ByteWriter writer(*data, size());
    
    writer.write<int>(subI);
    writer.writeString(subString);
     
    
    if (*data == NULL)
    {
        *data = writer.getData();
        writer.giveBufferOwnership();
    }
    
    return writer.getDataSize();
    
}

 
void SubStruct::read(const void *data)
{
    
    ByteReader reader(data);
    
    subI = reader.read<int>();
    subString = reader.readString();
    
    
}

 
std::string SubStruct::toXml(TiXmlElement *parentNode)
{
    
    TiXmlElement *parent;
    if (parentNode == nullptr)
    {
        parent = new TiXmlElement("SubStruct");
    }
    else
    {
        parent = parentNode;
    }
    
    
    { // subI
        std::stringstream oss;
        oss << subI;
        TiXmlElement * element = new TiXmlElement("subI");
        TiXmlText * text = new TiXmlText(oss.str().c_str());
        element->LinkEndChild(text);
        parent->LinkEndChild(element);
    }
    
    
    { // subString
        std::stringstream oss;
        oss << subString;
        TiXmlElement * element = new TiXmlElement("subString");
        TiXmlText * text = new TiXmlText(oss.str().c_str());
        element->LinkEndChild(text);
        parent->LinkEndChild(element);
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

 
TestStruct::TestStruct()
{
    
}

 
size_t TestStruct::size(void)
{
    
     
    
    size_t s_ass = sizeof(size_t);
    {
        for (size_t i = 0; i < ass.size(); i++)
        {
            std::vector<std::string> &e_name_1 = ass[i];
            
            
            size_t s_e_name_1 = sizeof(size_t);
            {
                for (size_t i = 0; i < e_name_1.size(); i++)
                {
                    std::string &e_name_5 = e_name_1[i];
                    
                    
                    
                    s_e_name_1 += 0  + (2 + e_name_5.size());
                }
            }
            
            
            s_ass += 0  + s_e_name_1;
        }
    }
    
    
    
    return 0  + sub.size()
     + sizeof(unsigned int)
     + (sizeof(size_t) + sizeof(int) * ai.size())
     + (2 + s.size())
     + s_ass
    ;
    
}

 
size_t TestStruct::write(void **data)
{
    
    ByteWriter writer(*data, size());
    
     
    void *p_sub = writer.getPosition();
    sub.write(&p_sub);
    writer.skipBytes(sub.size()); 
    
    writer.write<unsigned int>(ui);
    
    // write array ai
    writer.write<size_t>(ai.size());
    writer.writeBytes(ai.data(), sizeof(int) * ai.size() );
    
    writer.writeString(s);
     
    // write array ass
    writer.write<size_t>(ass.size());
    
    for (size_t i = 0; i < ass.size(); i++)
    {
        std::vector<std::string> &e_name_1 = ass[i];
            
         
        // write array e_name_1
        writer.write<size_t>(e_name_1.size());
        
        for (size_t i = 0; i < e_name_1.size(); i++)
        {
            std::string &e_name_6 = e_name_1[i];
                
            writer.writeString(e_name_6);
        }
        
    }
    
     
    
    if (*data == NULL)
    {
        *data = writer.getData();
        writer.giveBufferOwnership();
    }
    
    return writer.getDataSize();
    
}

 
void TestStruct::read(const void *data)
{
    
    ByteReader reader(data);
    
     
    sub.read(reader.getPosition());
    reader.skipBytes(sub.size()); 
    
    ui = reader.read<unsigned int>();
     
    // read array ai
    size_t s_ai = reader.read<size_t>();
    ai.resize(s_ai);
    reader.readBytes((char*)ai.data(), sizeof(int) * s_ai);
    
    s = reader.readString();
     
    // read array ass
    size_t s_ass = reader.read<size_t>();
    ass.resize(s_ass);
    
    for (size_t i = 0; i < s_ass; i++)
    {
        std::vector<std::string> &e_name_1 = ass[i];
            
         
        // read array e_name_1
        size_t s_e_name_1 = reader.read<size_t>();
        e_name_1.resize(s_e_name_1);
        
        for (size_t i = 0; i < s_e_name_1; i++)
        {
            std::string &e_name_7 = e_name_1[i];
                
            e_name_7 = reader.readString();
        }
        
    }
    
    
    
}

 
std::string TestStruct::toXml(TiXmlElement *parentNode)
{
    
    TiXmlElement *parent;
    if (parentNode == nullptr)
    {
        parent = new TiXmlElement("TestStruct");
    }
    else
    {
        parent = parentNode;
    }
    
     
    { // sub
        TiXmlElement * element = new TiXmlElement("sub");
        std::string xml = sub.toXml(element);
        parent->LinkEndChild(element);
    }
    
    
    { // ui
        std::stringstream oss;
        oss << ui;
        TiXmlElement * element = new TiXmlElement("ui");
        TiXmlText * text = new TiXmlText(oss.str().c_str());
        element->LinkEndChild(text);
        parent->LinkEndChild(element);
    }
    
     
    // write array ai
    TiXmlElement * ai_element = new TiXmlElement("ai");
    parent->LinkEndChild(ai_element);
    ai_element->SetAttribute("size", ai.size());
    
    TiXmlElement * parent_ai = parent;
    parent = ai_element;
    
    for (size_t i = 0; i < ai.size(); i++)
    {
        int &ai_sub = ai[i];
    
        
        { // ai_sub
            std::stringstream oss;
            oss << ai_sub;
            TiXmlElement * element = new TiXmlElement("ai_sub");
            TiXmlText * text = new TiXmlText(oss.str().c_str());
            element->LinkEndChild(text);
            parent->LinkEndChild(element);
        }
        
    }
    
    parent = parent_ai;
    
    
    { // s
        std::stringstream oss;
        oss << s;
        TiXmlElement * element = new TiXmlElement("s");
        TiXmlText * text = new TiXmlText(oss.str().c_str());
        element->LinkEndChild(text);
        parent->LinkEndChild(element);
    }
    
     
    // write array ass
    TiXmlElement * ass_element = new TiXmlElement("ass");
    parent->LinkEndChild(ass_element);
    ass_element->SetAttribute("size", ass.size());
    
    TiXmlElement * parent_ass = parent;
    parent = ass_element;
    
    for (size_t i = 0; i < ass.size(); i++)
    {
        std::vector<std::string> &ass_sub = ass[i];
    
         
        // write array ass_sub
        TiXmlElement * ass_sub_element = new TiXmlElement("ass_sub");
        parent->LinkEndChild(ass_sub_element);
        ass_sub_element->SetAttribute("size", ass_sub.size());
        
        TiXmlElement * parent_ass_sub = parent;
        parent = ass_sub_element;
        
        for (size_t i = 0; i < ass_sub.size(); i++)
        {
            std::string &ass_sub_sub = ass_sub[i];
        
            
            { // ass_sub_sub
                std::stringstream oss;
                oss << ass_sub_sub;
                TiXmlElement * element = new TiXmlElement("ass_sub_sub");
                TiXmlText * text = new TiXmlText(oss.str().c_str());
                element->LinkEndChild(text);
                parent->LinkEndChild(element);
            }
            
        }
        
        parent = parent_ass_sub;
        
    }
    
    parent = parent_ass;
    
     
    
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


}
