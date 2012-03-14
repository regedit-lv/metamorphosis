 
#include <vector>
#include <string>
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


struct SubStruct 
{
    int subI ;
    std::string subString ;
    
    SubStruct() ;
    size_t write(void **data) ;
    size_t size(void) ; 
};


struct TestStruct 
{
    struct SubStruct sub ;
    unsigned int ui ;
    std::vector<int> ai ;
    std::string s ;
    std::vector<std::vector<std::string>> ass ;
    
    TestStruct() ;
    size_t write(void **data) ;
    size_t size(void) ; 
};


}
