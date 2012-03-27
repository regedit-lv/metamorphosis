 
#include <vector>
#include <string>
#include <map>
#include <cstdint>
#include "tinyXML\tinyxml.h"


namespace xmq
{
namespace configuration
{

enum class MetaType : int
{
    Int ,
    String ,
    Array ,
    Object ,
    
};


class MetaTypeHelper 
{
public:
    static bool initDone ;
    static std::map<xmq::configuration::MetaType, std::string> valueToName ;
    static std::map<std::string, xmq::configuration::MetaType> nameToValue ;
    
    static std::string getEnumName(xmq::configuration::MetaType value) ;
    static xmq::configuration::MetaType getEnumValue(std::string name) ;
    static void initEnum(void) ; 
};


struct MetaData 
{
    xmq::configuration::MetaType type ;
    std::string name ;
    std::vector<struct xmq::configuration::MetaData> elements ;
    
    MetaData() ;
    std::string toXml(TiXmlElement *parentNode) ;
    void fromXml(const std::string &xml, TiXmlElement *parentNode) ; 
};


struct Module 
{
    std::string name ;
    std::string id ;
    
    Module() ;
    std::string toXml(TiXmlElement *parentNode) ;
    void fromXml(const std::string &xml, TiXmlElement *parentNode) ; 
};


struct IpRange 
{
    std::string from ;
    std::string to ;
    
    IpRange() ;
    std::string toXml(TiXmlElement *parentNode) ;
    void fromXml(const std::string &xml, TiXmlElement *parentNode) ; 
};


struct Instance 
{
    std::string name ;
    int32_t id ;
    std::string host ;
    int32_t port ;
    
    Instance() ;
    std::string toXml(TiXmlElement *parentNode) ;
    void fromXml(const std::string &xml, TiXmlElement *parentNode) ; 
};


struct Path 
{
    std::string modules ;
    std::string modulesData ;
    
    Path() ;
    std::string toXml(TiXmlElement *parentNode) ;
    void fromXml(const std::string &xml, TiXmlElement *parentNode) ; 
};


struct Connection 
{
    std::string host ;
    int32_t port ;
    
    Connection() ;
    std::string toXml(TiXmlElement *parentNode) ;
    void fromXml(const std::string &xml, TiXmlElement *parentNode) ; 
};


struct Configuration 
{
    struct xmq::configuration::MetaData metaData ;
    struct xmq::configuration::Instance instance ;
    struct xmq::configuration::Path path ;
    std::vector<struct xmq::configuration::Connection> busses ;
    std::vector<struct xmq::configuration::IpRange> whiteIp ;
    std::vector<struct xmq::configuration::IpRange> blackIp ;
    std::vector<struct xmq::configuration::Module> modules ;
    
    Configuration() ;
    std::string toXml(TiXmlElement *parentNode) ;
    void fromXml(const std::string &xml, TiXmlElement *parentNode) ; 
};


}

}
