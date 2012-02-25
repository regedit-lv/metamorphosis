 
#include <vector>
#include <string>

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
    enum TE::Type te ;
    std::vector<int> sub_ai ;
    unsigned int ui ;
    int i ;
    
    SubStruct() ;
    size_t size(void) ;
    size_t write(void **data) ;
    void read(const void *data) ; 
};


struct BaseStruct 
{
    std::string bs ;
    int bi ;
    
    BaseStruct() ;
    size_t size(void) ;
    size_t write(void **data) ;
    void read(const void *data) ; 
};


struct TestStruct : public BaseStruct
{
    struct SubStruct ss ;
    unsigned int ui ;
    std::vector<std::vector<int>> aai ;
    std::vector<std::string> as ;
    std::string s ;
    
    TestStruct() ;
    size_t size(void) ;
    size_t write(void **data) ;
    void read(const void *data) ; 
};


}
