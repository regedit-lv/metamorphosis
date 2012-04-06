
#include "stdafx.h"

#include <sstream>
#include "xmq.h"

namespace xmq
{
namespace settings
{
 
Property::Property()
{
    
}

 
void Property::read(const void *data)
{
    
    ByteReader reader(data);
    
    key = reader.readString();
    value = reader.readString();
    
    
}

 
size_t Property::write(void **data)
{
    
    ByteWriter writer(*data, size());
    
    writer.writeString(key);
    writer.writeString(value);
     
    
    if (*data == NULL)
    {
        *data = writer.getData();
        writer.giveBufferOwnership();
    }
    
    return writer.getDataSize();
    
}

 
size_t Property::size(void)
{
    
    
    
    return 0  + (2 + key.size())
     + (2 + value.size())
    ;
    
}

 
BaseMessage::BaseMessage()
{
    
}

 
void BaseMessage::read(const void *data)
{
    
    ByteReader reader(data);
    
    messageType = reader.read<xmq::settings::MessageType>();
    
    
}

 
size_t BaseMessage::write(void **data)
{
    
    ByteWriter writer(*data, size());
    
    writer.write<xmq::settings::MessageType>(messageType);
     
    
    if (*data == NULL)
    {
        *data = writer.getData();
        writer.giveBufferOwnership();
    }
    
    return writer.getDataSize();
    
}

 
size_t BaseMessage::size(void)
{
    
    
    
    return 0  + sizeof(xmq::settings::MessageType)
    ;
    
}

 
GetAllMessage::GetAllMessage() : BaseMessage()
{
     messageType =  MessageType::GetAll ; 
    
}

 
void GetAllMessage::read(const void *data)
{
    
    ByteReader reader(data);
    
    messageType = reader.read<xmq::settings::MessageType>();
    
    
}

 
size_t GetAllMessage::write(void **data)
{
    
    ByteWriter writer(*data, size());
    
    writer.write<xmq::settings::MessageType>(messageType);
     
    
    if (*data == NULL)
    {
        *data = writer.getData();
        writer.giveBufferOwnership();
    }
    
    return writer.getDataSize();
    
}

 
size_t GetAllMessage::size(void)
{
    
    
    
    return 0  + sizeof(xmq::settings::MessageType)
    ;
    
}

 
GetAllReplyMessage::GetAllReplyMessage() : BaseMessage()
{
     messageType =  MessageType::GetAllReply ; 
    
}

 
void GetAllReplyMessage::read(const void *data)
{
    
    ByteReader reader(data);
    
    messageType = reader.read<xmq::settings::MessageType>();
     
    // read array settings
    size_t s_settings = reader.read<size_t>();
    settings.resize(s_settings);
    
    for (size_t i = 0; i < s_settings; i++)
    {
        struct xmq::settings::Property &e_name_1 = settings[i];
            
         
        e_name_1.read(reader.getPosition());
        reader.skipBytes(e_name_1.size()); 
        
    }
    
    
    
}

 
size_t GetAllReplyMessage::write(void **data)
{
    
    ByteWriter writer(*data, size());
    
    writer.write<xmq::settings::MessageType>(messageType);
     
    // write array settings
    writer.write<size_t>(settings.size());
    
    for (size_t i = 0; i < settings.size(); i++)
    {
        struct xmq::settings::Property &e_name_1 = settings[i];
            
         
        void *p_e_name_1 = writer.getPosition();
        e_name_1.write(&p_e_name_1);
        writer.skipBytes(e_name_1.size()); 
        
    }
    
     
    
    if (*data == NULL)
    {
        *data = writer.getData();
        writer.giveBufferOwnership();
    }
    
    return writer.getDataSize();
    
}

 
size_t GetAllReplyMessage::size(void)
{
    
    
    size_t s_settings = sizeof(size_t);
    {
        for (size_t i = 0; i < settings.size(); i++)
        {
            struct xmq::settings::Property &e_name_1 = settings[i];
            
            
            
            s_settings += 0  + e_name_1.size();
        }
    }
    
    
    
    return 0  + sizeof(xmq::settings::MessageType)
     + s_settings
    ;
    
}

 
SetMessage::SetMessage() : BaseMessage()
{
     messageType =  MessageType::Set ; 
    
}

 
void SetMessage::read(const void *data)
{
    
    ByteReader reader(data);
    
    messageType = reader.read<xmq::settings::MessageType>();
    key = reader.readString();
    value = reader.readString();
    
    
}

 
size_t SetMessage::write(void **data)
{
    
    ByteWriter writer(*data, size());
    
    writer.write<xmq::settings::MessageType>(messageType);
    writer.writeString(key);
    writer.writeString(value);
     
    
    if (*data == NULL)
    {
        *data = writer.getData();
        writer.giveBufferOwnership();
    }
    
    return writer.getDataSize();
    
}

 
size_t SetMessage::size(void)
{
    
    
    
    return 0  + sizeof(xmq::settings::MessageType)
     + (2 + key.size())
     + (2 + value.size())
    ;
    
}

 
SetReplyMessage::SetReplyMessage() : BaseMessage()
{
     messageType =  MessageType::SetReply ; 
    
}

 
void SetReplyMessage::read(const void *data)
{
    
    ByteReader reader(data);
    
    messageType = reader.read<xmq::settings::MessageType>();
    error = reader.read<xmq::settings::Error>();
    
    
}

 
size_t SetReplyMessage::write(void **data)
{
    
    ByteWriter writer(*data, size());
    
    writer.write<xmq::settings::MessageType>(messageType);
    writer.write<xmq::settings::Error>(error);
     
    
    if (*data == NULL)
    {
        *data = writer.getData();
        writer.giveBufferOwnership();
    }
    
    return writer.getDataSize();
    
}

 
size_t SetReplyMessage::size(void)
{
    
    
    
    return 0  + sizeof(xmq::settings::MessageType)
     + sizeof(xmq::settings::Error)
    ;
    
}


}

}
