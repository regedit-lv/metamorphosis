 
#include <vector>
#include <string>
#include <map>
#include "tinyXML\tinyxml.h"


namespace xmq
{

namespace TE
{
    enum Type
    {
        AA ,
        BB = 3 ,
        CC ,
        
    };
};


struct SM 
{
    std::map<std::string, std::string> mss ;
    
    SM() ;
    void read(const void *data) ;
    size_t write(void **data) ;
    size_t size(void) ; 
};


struct SubStruct 
{
    int subI ;
    std::string subString ;
    
    SubStruct() ;
    void read(const void *data) ;
    size_t write(void **data) ;
    size_t size(void) ; 
};


struct BaseStruct 
{
    std::string bs ;
    int bi ;
    
    BaseStruct() ; 
};


struct TestStruct : public BaseStruct
{
    struct SubStruct sub ;
    unsigned int ui ;
    std::vector<int> ai ;
    std::string s ;
    std::vector<std::vector<std::string>> ass ;
    
    TestStruct() ;
    void read(const void *data) ;
    size_t write(void **data) ;
    size_t size(void) ; 
};


}
