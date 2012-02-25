// Metamorphosis.CPPTest.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include "xmq.h"

int _tmain(int argc, _TCHAR* argv[])
{
    xmq::TestStruct ts, ts2;
    ts.ss.i = 0;
    ts.ss.sub_ai.push_back(123);
    ts.ss.ui = 9;

    std::vector<int> vi;
    vi.clear();
    vi.push_back(111);
    vi.push_back(112);
    vi.push_back(113);
    ts.aai.push_back(vi);
    vi.clear();
    vi.push_back(121);
    vi.push_back(122);
    vi.push_back(123);
    ts.aai.push_back(vi);
 
    ts.as.push_back("21abcd");
    ts.as.push_back("22abcd");

    ts.bi = 3;
    ts.bs = "base 3";
    ts.s = "string s";
    ts.ui = 2;

    void *d = nullptr;
    ts.write(&d);

    ts2.read(d);

    delete d;

    return 0;
}

