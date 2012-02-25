
#include "stdafx.h"

#include "ByteReader.h"
#include "ByteWriter.h"
#include "xmq.h"

namespace xmq
{
 
SubStruct::SubStruct()
{
    te = TE::AA ;
    
}

 
size_t SubStruct::size(void)
{
    
    
     
    
    
    
    
    return 0  + sizeof(enum TE::Type)
     + (sizeof(size_t) + sizeof(int) * sub_ai.size())
     + sizeof(unsigned int)
     + sizeof(int)
    ;
    
}

 
size_t SubStruct::write(void **data)
{
    
    ByteWriter writer(*data, size());
    
    writer.write<enum TE::Type>(te);
    
    // write array sub_ai
    writer.write<size_t>(sub_ai.size());
    writer.writeBytes(sub_ai.data(), sizeof(int) * sub_ai.size() );
    
    writer.write<unsigned int>(ui);
    writer.write<int>(i);
     
    
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
    
    te = reader.read<enum TE::Type>();
     
    // read array sub_ai
    size_t s_sub_ai = reader.read<size_t>();
    sub_ai.resize(s_sub_ai);
    reader.readBytes((char*)sub_ai.data(), sizeof(int) * s_sub_ai);
    
    ui = reader.read<unsigned int>();
    i = reader.read<int>();
    
    
}

 
BaseStruct::BaseStruct()
{
    
}

 
size_t BaseStruct::size(void)
{
    
    
    
    
    
    return 0  + (2 + bs.size())
     + sizeof(int)
    ;
    
}

 
size_t BaseStruct::write(void **data)
{
    
    ByteWriter writer(*data, size());
    
    writer.writeString(bs);
    writer.write<int>(bi);
     
    
    if (*data == NULL)
    {
        *data = writer.getData();
        writer.giveBufferOwnership();
    }
    
    return writer.getDataSize();
    
}

 
void BaseStruct::read(const void *data)
{
    
    ByteReader reader(data);
    
    bs = reader.readString();
    bi = reader.read<int>();
    
    
}

 
TestStruct::TestStruct() : BaseStruct()
{
    bs = "bs_value" ;
    bi = 3 ;
    
}

 
size_t TestStruct::size(void)
{
    
    
    
    
    
    
    size_t s_aai = sizeof(size_t);
    {
        for (size_t i = 0; i < aai.size(); i++)
        {
            std::vector<int> &e_name_1 = aai[i];
            
             
            
            s_aai += 0  + (sizeof(size_t) + sizeof(int) * e_name_1.size());
        }
    }
    
    
    size_t s_as = sizeof(size_t);
    {
        for (size_t i = 0; i < as.size(); i++)
        {
            std::string &e_name_2 = as[i];
            
            
            
            s_as += 0  + (2 + e_name_2.size());
        }
    }
    
    
    
    
    return 0  + (2 + bs.size())
     + sizeof(int)
     + ss.size()
     + sizeof(unsigned int)
     + s_aai
     + s_as
     + (2 + s.size())
    ;
    
}

 
size_t TestStruct::write(void **data)
{
    
    ByteWriter writer(*data, size());
    
    writer.writeString(bs);
    writer.write<int>(bi);
     
    void *p_ss = writer.getPosition();
    ss.write(&p_ss);
    writer.skipBytes(ss.size()); 
    
    writer.write<unsigned int>(ui);
     
    // write array aai
    writer.write<size_t>(aai.size());
    
    for (size_t i = 0; i < aai.size(); i++)
    {
        std::vector<int> &e_name_1 = aai[i];
            
        
        // write array e_name_1
        writer.write<size_t>(e_name_1.size());
        writer.writeBytes(e_name_1.data(), sizeof(int) * e_name_1.size() );
        
    }
    
     
    // write array as
    writer.write<size_t>(as.size());
    
    for (size_t i = 0; i < as.size(); i++)
    {
        std::string &e_name_2 = as[i];
            
        writer.writeString(e_name_2);
    }
    
    writer.writeString(s);
     
    
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
    
    bs = reader.readString();
    bi = reader.read<int>();
     
    ss.read(reader.getPosition());
    reader.skipBytes(ss.size()); 
    
    ui = reader.read<unsigned int>();
     
    // read array aai
    size_t s_aai = reader.read<size_t>();
    aai.resize(s_aai);
    
    for (size_t i = 0; i < s_aai; i++)
    {
        std::vector<int> &e_name_1 = aai[i];
            
         
        // read array e_name_1
        size_t s_e_name_1 = reader.read<size_t>();
        e_name_1.resize(s_e_name_1);
        reader.readBytes((char*)e_name_1.data(), sizeof(int) * s_e_name_1);
        
    }
    
     
    // read array as
    size_t s_as = reader.read<size_t>();
    as.resize(s_as);
    
    for (size_t i = 0; i < s_as; i++)
    {
        std::string &e_name_2 = as[i];
            
        e_name_2 = reader.readString();
    }
    
    s = reader.readString();
    
    
}


}
