
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

 
TestStruct::TestStruct()
{
    
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
            std::string &e_name_4 = e_name_1[i];
                
            writer.writeString(e_name_4);
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


}
