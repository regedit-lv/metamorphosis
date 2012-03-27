// Metamorphosis.CPPTest.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include <fstream>
#include <sstream>
#include "xmq.h"

#include "tinyXML\tinyxml.h"

int _tmain(int argc, _TCHAR* argv[])
{
    /*
    xmq::bus::configuration::Configuration conf;
    xmq::bus::configuration::IpRange ir;
    xmq::bus::configuration::Module m;
    
    m.name = "Module1";
    m.id = "test1";
    conf.modules.push_back(m);
    m.name = "Module2";
    m.id = "test2";
    conf.modules.push_back(m);

    ir.from = "192.168.0.1";
    ir.to = "192.168.0.255";
    conf.whiteIp.push_back(ir);
    ir.from = "192.168.1.1";
    ir.to = "192.168.1.255";
    conf.whiteIp.push_back(ir);
    ir.from = "192.168.0.111";
    ir.to = "192.168.0.120";
    conf.blackIp.push_back(ir);

    xmq::bus::configuration::Connection c;
    c.host = "localhost";
    c.port = 40001;
    conf.busses.push_back(c);
    c.port = 40002;
    conf.busses.push_back(c);

    conf.path.modules = "C:\\DataPath";
    conf.path.modulesData = "C:\\Path";
    conf.instance.host = "localhost";
    conf.instance.name = "bus1";
    conf.instance.port = 2000;
    conf.instance.id = 12;
    std::string xml = conf.toXml(nullptr);

    std::cout << xml << std::endl;


    TiXmlDocument doc;

    doc.Parse(xml.c_str());

    long a = sizeof(long);;

    TiXmlElement *root = doc.RootElement();

    std::cout << root->Value() << std::endl;

    for (TiXmlNode *child = root->FirstChild(); child != 0; child = child->NextSibling()) 
    {
        const char * s = child->Value();
        if (strcmp(child->Value(), "instance") == 0)
        {
        }
    }
    
    int aaa = _stricmp("AA", "aa");
    xmq::bus::configuration::Configuration conf2;

    std::ifstream infile("config.xml");
    std::string allText = "";
    std::string line;

    while(!infile.eof()) // To get you all the lines.
    {
	    getline(infile, line); // Saves the line in STRING.
	    allText += line;
    }

    conf2.fromXml(allText, nullptr);

    return 0;
    /*
    xmq::SM sm, sm2;

    sm.mss["asd"] = "123";
    sm.mss["zxcv"] = "876";

    void *dd = nullptr;
    sm.write(&dd);
    sm2.read(dd);
    delete dd;

    xmq::TestStruct ts;
    ts.s = "ts str";
    ts.ui = 3;
    ts.ai.push_back(1);
    ts.ai.push_back(2);

    std::vector<std::string> vs;
    vs.push_back("s11");
    vs.push_back("s12");
    vs.push_back("s13");
    ts.ass.push_back(vs);
    vs.clear();
    vs.push_back("s21");
    vs.push_back("s22");
    ts.ass.push_back(vs);

    ts.sub.subI = 3;
    ts.sub.subString = "sub string value";
    */
    
    //std::string xml = ts.toXml(nullptr);

    //std::cout << xml << std::endl;
    /*
    TiXmlDocument doc;
    TiXmlDeclaration * decl = new TiXmlDeclaration("1.0", "", "");
    TiXmlElement * element = new TiXmlElement("Hello");
    TiXmlText * text = new TiXmlText("<World isMine=\"true\"/>");
    text->SetCDATA(true);
    element->LinkEndChild(text);
    doc.LinkEndChild(decl);
    doc.LinkEndChild(element);
    //element->
    //
    //std::string xml;
    TiXmlPrinter printer;
    printer.SetIndent("    ");

    doc.Accept(&printer);
    std::string xmltext = printer.CStr();

    std::cout << xmltext;
    */
 //   xmq::TestStruct ts, ts2;
 //   ts.ss.i = 0;
 //   ts.ss.sub_ai.push_back(123);
 //   ts.ss.ui = 9;

 //   std::vector<int> vi;
 //   vi.clear();
 //   vi.push_back(111);
 //   vi.push_back(112);
 //   vi.push_back(113);
 //   ts.aai.push_back(vi);
 //   vi.clear();
 //   vi.push_back(121);
 //   vi.push_back(122);
 //   vi.push_back(123);
 //   ts.aai.push_back(vi);
 //
 //   ts.as.push_back("21abcd");
 //   ts.as.push_back("22abcd");

 //   ts.bi = 3;
 //   ts.bs = "base 3";
 //   ts.s = "string s";
 //   ts.ui = 2;

 //   void *d = nullptr;
 //   ts.write(&d);

 //   ts2.read(d);

 //   delete d;

    return 0;
}

