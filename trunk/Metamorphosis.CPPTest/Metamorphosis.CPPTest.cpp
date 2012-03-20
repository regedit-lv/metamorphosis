// Metamorphosis.CPPTest.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include <sstream>
#include "xmq.h"

#include "tinyXML\tinyxml.h"

int _tmain(int argc, _TCHAR* argv[])
{
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

    //std::string xml = ts.toXml(nullptr);

    //std::cout << xml << std::endl;

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

