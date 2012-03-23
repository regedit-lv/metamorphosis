 
#include <vector>
#include <string>
#include <map>
#include <cstdint>
#include "tinyXML\tinyxml.h"


namespace xmq
{
namespace bus
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


struct MetaData 
{
    xmq::bus::configuration::MetaType type ;
    std::string name ;
    std::vector<struct xmq::bus::configuration::MetaData> elements ;
    
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
    struct xmq::bus::configuration::MetaData metaData ;
    struct xmq::bus::configuration::Instance instance ;
    struct xmq::bus::configuration::Path path ;
    std::vector<struct xmq::bus::configuration::Connection> busses ;
    std::vector<struct xmq::bus::configuration::IpRange> whiteIp ;
    std::vector<struct xmq::bus::configuration::IpRange> blackIp ;
    std::vector<struct xmq::bus::configuration::Module> modules ;
    
    Configuration() ;
    std::string toXml(TiXmlElement *parentNode) ;
    void fromXml(const std::string &xml, TiXmlElement *parentNode) ; 
};


}

}

}
