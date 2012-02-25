#include <vector> 
#include <string>
namespace xmsg
{
struct AStruct {
    int a;
    int b;

    size_t getSize();
    size_t save(void **data);
    void load(const void *data); 
};
struct BaseRecord {
    int cmd;

    size_t getSize();
    size_t save(void **data);
    void load(const void *data); 
};
struct TradeRecord : BaseRecord {
    AStruct a;
    unsigned int closeTime;
    int order;
    int openTime;
    std::string symbolName;
    double volume;
    double sl;
    double tp;
    double deviation;
    double openPrice;
    double commission;
    int activation;
    int execCommand;

    size_t getSize();
    size_t save(void **data);
    void load(const void *data); 
};
struct XTest {
    int a;
    std::string s;
    std::vector<std::string> as;
    std::vector<int> ai;
    std::vector<std::vector<std::vector<std::string>>> aas;

    size_t getSize();
    size_t save(void **data);
    void load(const void *data); 
};

}
