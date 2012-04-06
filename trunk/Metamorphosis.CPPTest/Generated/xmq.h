 
#include <vector>
#include <string>
#include <map>
#include <cstdint>
 
#include "tinyXML\tinyxml.h"


namespace xmq
{
namespace settings
{

enum class Error : int
{
     Ok , 
     Failed , 
    
};


enum class MessageType : int
{
     GetAll , 
     GetAllReply , 
     Set , 
     SetReply , 
    
};


struct Property 
{
     std::string key; 
     std::string value; 
    
     Property(); 
     void read(const void *data); 
     size_t write(void **data); 
     size_t size(void);  
};


struct BaseMessage 
{
     xmq::settings::MessageType messageType; 
    
     BaseMessage(); 
    virtual  void read(const void *data); 
    virtual  size_t write(void **data); 
    virtual  size_t size(void);  
};


struct  GetAllMessage  : public  BaseMessage 
{
    
     GetAllMessage(); 
     void read(const void *data); 
     size_t write(void **data); 
     size_t size(void);  
};


struct  GetAllReplyMessage  : public  BaseMessage 
{
     std::vector<struct xmq::settings::Property> settings; 
    
     GetAllReplyMessage(); 
     void read(const void *data); 
     size_t write(void **data); 
     size_t size(void);  
};


struct  SetMessage  : public  BaseMessage 
{
     std::string key; 
     std::string value; 
    
     SetMessage(); 
     void read(const void *data); 
     size_t write(void **data); 
     size_t size(void);  
};


struct  SetReplyMessage  : public  BaseMessage 
{
     xmq::settings::Error error; 
    
     SetReplyMessage(); 
     void read(const void *data); 
     size_t write(void **data); 
     size_t size(void);  
};


}

}
