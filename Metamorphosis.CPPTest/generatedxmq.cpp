#include "stdafx.h" 
#include "ByteReader.h" 
#include "ByteWriter.h" 
#include "xmq.h" 

namespace xmsg
{

size_t AStruct::getSize() 
{ 

    return sizeof(int) + 
        sizeof(int);
 
}
    
size_t AStruct::save(void **data) 
{ 
    ByteWriter writer(*data, getSize()); 
    
    writer.write<int>(a);
    writer.write<int>(b); 
    
    if (*data == NULL) 
    { 
        *data = writer.getData(); 
        writer.giveBufferOwnership(); 
    } 
    return writer.getDataSize(); 
}
    
void AStruct::load(const void *data) 
{ 
    ByteReader reader(data); 
    
    a = reader.read<int>();
    b = reader.read<int>(); 
     
}

size_t BaseRecord::getSize() 
{ 

    return sizeof(int);
 
}
    
size_t BaseRecord::save(void **data) 
{ 
    ByteWriter writer(*data, getSize()); 
    
    writer.write<int>(cmd); 
    
    if (*data == NULL) 
    { 
        *data = writer.getData(); 
        writer.giveBufferOwnership(); 
    } 
    return writer.getDataSize(); 
}
    
void BaseRecord::load(const void *data) 
{ 
    ByteReader reader(data); 
    
    cmd = reader.read<int>(); 
     
}

size_t TradeRecord::getSize() 
{ 

    return sizeof(int) + 
        a.getSize() + 
        sizeof(unsigned int) + 
        sizeof(int) + 
        sizeof(int) + 
        (2 + symbolName.size()) + 
        sizeof(double) + 
        sizeof(double) + 
        sizeof(double) + 
        sizeof(double) + 
        sizeof(double) + 
        sizeof(double) + 
        sizeof(int) + 
        sizeof(int);
 
}
    
size_t TradeRecord::save(void **data) 
{ 
    ByteWriter writer(*data, getSize()); 
    
    writer.write<int>(cmd);
    void *p_a = writer.getPosition(); 
    a.save(&p_a); 
    writer.skipBytes(a.getSize());
    writer.write<unsigned int>(closeTime);
    writer.write<int>(order);
    writer.write<int>(openTime);
    writer.writeString(symbolName);
    writer.write<double>(volume);
    writer.write<double>(sl);
    writer.write<double>(tp);
    writer.write<double>(deviation);
    writer.write<double>(openPrice);
    writer.write<double>(commission);
    writer.write<int>(activation);
    writer.write<int>(execCommand); 
    
    if (*data == NULL) 
    { 
        *data = writer.getData(); 
        writer.giveBufferOwnership(); 
    } 
    return writer.getDataSize(); 
}
    
void TradeRecord::load(const void *data) 
{ 
    ByteReader reader(data); 
    
    cmd = reader.read<int>();
    a.load(reader.getPosition()); 
    reader.skipBytes(a.getSize());
    closeTime = reader.read<unsigned int>();
    order = reader.read<int>();
    openTime = reader.read<int>();
    symbolName = reader.readString();
    volume = reader.read<double>();
    sl = reader.read<double>();
    tp = reader.read<double>();
    deviation = reader.read<double>();
    openPrice = reader.read<double>();
    commission = reader.read<double>();
    activation = reader.read<int>();
    execCommand = reader.read<int>(); 
     
}

size_t XTest::getSize() 
{ 

    size_t totalSize_45523402 = 2; 
    { 
        for (size_t i = 0; i < as.size(); i++) 
        { 
            std::string &e_45523402 = as[i]; 
             
            totalSize_45523402 += (2 + e_45523402.size()); 
        } 
    } 
    
    size_t totalSize_35287174 = 2; 
    { 
        for (size_t i = 0; i < aas.size(); i++) 
        { 
            std::vector<std::vector<std::string>> &e_35287174 = aas[i]; 
            size_t totalSize_44419000 = 2; 
            { 
                for (size_t i = 0; i < e_35287174.size(); i++) 
                { 
                    std::vector<std::string> &e_44419000 = e_35287174[i]; 
                    size_t totalSize_45523402 = 2; 
                    { 
                        for (size_t i = 0; i < e_44419000.size(); i++) 
                        { 
                            std::string &e_45523402 = e_44419000[i]; 
                             
                            totalSize_45523402 += (2 + e_45523402.size()); 
                        } 
                    } 
                     
                    totalSize_44419000 += totalSize_45523402; 
                } 
            } 
             
            totalSize_35287174 += totalSize_44419000; 
        } 
    } 
    
    return sizeof(int) + 
        (2 + s.size()) + 
        totalSize_45523402 + 
        (2 + sizeof(int) * ai.size()) + 
        totalSize_35287174;
 
}
    
size_t XTest::save(void **data) 
{ 
    ByteWriter writer(*data, getSize()); 
    
    writer.write<int>(a);
    writer.writeString(s);
    // write array as 
    writer.write<size_t>(as.size()); 
    for (size_t i = 0; i < as.size(); i++) 
    { 
        std::string &e_45523402 = as[i]; 
        writer.writeString(e_45523402); 
    }
    // write array ai 
    writer.write<size_t>(ai.size()); 
    for (size_t i = 0; i < ai.size(); i++) 
    { 
        int &e_52697953 = ai[i]; 
        writer.write<int>(e_52697953); 
    }
    // write array aas 
    writer.write<size_t>(aas.size()); 
    for (size_t i = 0; i < aas.size(); i++) 
    { 
        std::vector<std::vector<std::string>> &e_35287174 = aas[i]; 
        // write array e_35287174 
        writer.write<size_t>(e_35287174.size()); 
        for (size_t i = 0; i < e_35287174.size(); i++) 
        { 
            std::vector<std::string> &e_44419000 = e_35287174[i]; 
            // write array e_44419000 
            writer.write<size_t>(e_44419000.size()); 
            for (size_t i = 0; i < e_44419000.size(); i++) 
            { 
                std::string &e_45523402 = e_44419000[i]; 
                writer.writeString(e_45523402); 
            } 
        } 
    } 
    
    if (*data == NULL) 
    { 
        *data = writer.getData(); 
        writer.giveBufferOwnership(); 
    } 
    return writer.getDataSize(); 
}
    
void XTest::load(const void *data) 
{ 
    ByteReader reader(data); 
    
    a = reader.read<int>();
    s = reader.readString();
    // read array as 
    size_t as_size = reader.read<size_t>(); 
    as.resize(as_size); 
    for (size_t i = 0; i < as_size; i++) 
    { 
        std::string &e_45523402 = as[i]; 
        e_45523402 = reader.readString(); 
    }
    // read array ai 
    size_t ai_size = reader.read<size_t>(); 
    ai.resize(ai_size); 
    for (size_t i = 0; i < ai_size; i++) 
    { 
        int &e_52697953 = ai[i]; 
        e_52697953 = reader.read<int>(); 
    }
    // read array aas 
    size_t aas_size = reader.read<size_t>(); 
    aas.resize(aas_size); 
    for (size_t i = 0; i < aas_size; i++) 
    { 
        std::vector<std::vector<std::string>> &e_35287174 = aas[i]; 
        // read array e_35287174 
        size_t e_35287174_size = reader.read<size_t>(); 
        e_35287174.resize(e_35287174_size); 
        for (size_t i = 0; i < e_35287174_size; i++) 
        { 
            std::vector<std::string> &e_44419000 = e_35287174[i]; 
            // read array e_44419000 
            size_t e_44419000_size = reader.read<size_t>(); 
            e_44419000.resize(e_44419000_size); 
            for (size_t i = 0; i < e_44419000_size; i++) 
            { 
                std::string &e_45523402 = e_44419000[i]; 
                e_45523402 = reader.readString(); 
            } 
        } 
    } 
     
}

}
