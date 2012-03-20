
#include "stdafx.h"

#include "ByteReader.h"
#include "ByteWriter.h"
#include <sstream>
#include "xmq.h"

namespace xmq
{
 
SM::SM()
{
    
}

 
void SM::read(const void *data)
{
    
    ByteReader reader(data);
    
     
    // read array mss
    size_t s_mss = reader.read<size_t>();
    
    for (size_t i = 0; i < s_mss; i++)
    {
        std::string e_key_1;
        std::string e_value_2;
            
        e_key_1 = reader.readString();
        e_value_2 = reader.readString();
        
        mss[e_key_1] = e_value_2;
    }
    
    
    
}

 
size_t SM::write(void **data)
{
    
    ByteWriter writer(*data, size());
    
     
    // write map mss
    writer.write<size_t>(mss.size());
    {
        for (std::map<std::string, std::string>::iterator it = mss.begin(); it != mss.end(); ++it)        
        {
            const std::string &e_key_1 = it->first;
            const std::string &e_value_2 = it->second;
            
            writer.writeString(e_key_1);
            writer.writeString(e_value_2);
        }
    }
    
     
    
    if (*data == NULL)
    {
        *data = writer.getData();
        writer.giveBufferOwnership();
    }
    
    return writer.getDataSize();
    
}

 
size_t SM::size(void)
{
    
    
    size_t s_mss = sizeof(size_t);
    {
        for (std::map<std::string, std::string>::iterator it = mss.begin(); it != mss.end(); ++it)        
        {
            const std::string &e_key_1 = it->first;
            const std::string &e_value_2 = it->second;
            
            
            
            
            s_mss += 0  + (2 + e_key_1.size())
                            + (2 + e_value_2.size());
        }
    }
    
    
    
    return 0  + s_mss
    ;
    
}

 
SubStruct::SubStruct()
{
    
}

 
void SubStruct::read(const void *data)
{
    
    ByteReader reader(data);
    
    subI = reader.read<int>();
    subString = reader.readString();
    
    
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

 
size_t SubStruct::size(void)
{
    
    
    
    return 0  + sizeof(int)
     + (2 + subString.size())
    ;
    
}

 
BaseStruct::BaseStruct()
{
    
}

 
TestStruct::TestStruct() : BaseStruct()
{
    bs = "bs_value" ;
    bi = 3 ;
    
}

 
void TestStruct::read(const void *data)
{
    
    ByteReader reader(data);
    
    bs = reader.readString();
    bi = reader.read<int>();
     
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
        std::vector<std::string> &e_name_3 = ass[i];
            
         
        // read array e_name_3
        size_t s_e_name_3 = reader.read<size_t>();
        e_name_3.resize(s_e_name_3);
        
        for (size_t i = 0; i < s_e_name_3; i++)
        {
            std::string &e_name_7 = e_name_3[i];
                
            e_name_7 = reader.readString();
        }
        
    }
    
    
    
}

 
size_t TestStruct::write(void **data)
{
    
    ByteWriter writer(*data, size());
    
    writer.writeString(bs);
    writer.write<int>(bi);
     
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
        std::vector<std::string> &e_name_3 = ass[i];
            
         
        // write array e_name_3
        writer.write<size_t>(e_name_3.size());
        
        for (size_t i = 0; i < e_name_3.size(); i++)
        {
            std::string &e_name_8 = e_name_3[i];
                
            writer.writeString(e_name_8);
        }
        
    }
    
     
    
    if (*data == NULL)
    {
        *data = writer.getData();
        writer.giveBufferOwnership();
    }
    
    return writer.getDataSize();
    
}

 
size_t TestStruct::size(void)
{
    
     
    
    size_t s_ass = sizeof(size_t);
    {
        for (size_t i = 0; i < ass.size(); i++)
        {
            std::vector<std::string> &e_name_3 = ass[i];
            
            
            size_t s_e_name_3 = sizeof(size_t);
            {
                for (size_t i = 0; i < e_name_3.size(); i++)
                {
                    std::string &e_name_9 = e_name_3[i];
                    
                    
                    
                    s_e_name_3 += 0  + (2 + e_name_9.size());
                }
            }
            
            
            s_ass += 0  + s_e_name_3;
        }
    }
    
    
    
    return 0  + (2 + bs.size())
     + sizeof(int)
     + sub.size()
     + sizeof(unsigned int)
     + (sizeof(size_t) + sizeof(int) * ai.size())
     + (2 + s.size())
     + s_ass
    ;
    
}


}
