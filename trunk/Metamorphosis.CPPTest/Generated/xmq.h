 
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

struct IpRagne 
{
    std::string from ;
    std::string to ;
    
    IpRagne() ;
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
    struct xmq::bus::configuration::Instance instance ;
    struct xmq::bus::configuration::Path path ;
    std::vector<struct xmq::bus::configuration::Connection> busses ;
    std::vector<struct xmq::bus::configuration::IpRagne> whiteIp ;
    std::vector<struct xmq::bus::configuration::IpRagne> blackIp ;
    
    Configuration() ;
    std::string toXml(TiXmlElement *parentNode) ;
    void fromXml(const std::string &xml, TiXmlElement *parentNode) ; 
};


}

}

}
