#include "StdAfx.h"
#include "ByteWriter.h"
#include <stdlib.h>
#include <WinSock2.h>

ByteWriter::ByteWriter(void *data, size_t size)
{
    if (data == NULL)
    {
        buffer = new char[size];
        deleteBuffer = true;
    }
    else
    {
        buffer = (char*)data;
        deleteBuffer = false;
    }
    this->size = size;
    position = buffer;
}

ByteWriter::~ByteWriter(void)
{
    if (deleteBuffer)
    {
        delete buffer;
    }
}

void *ByteWriter::getPosition()
{
    return position;
}

void ByteWriter::writeBytes(const void *source, size_t size)
{
    memcpy(position, source, size);
    position += size;
}

void *ByteWriter::getData()
{
    return buffer;
}

void ByteWriter::skipBytes(int count)
{
    position += count;
}

int ByteWriter::getDataSize()
{
    return position - buffer;
}

void ByteWriter::writeString(std::string source)
{
    u_short size = source.size();
    writeData(source.c_str(), size);
}

void ByteWriter::writeData(const void *source, unsigned short size)
{
    write<u_short>(size);
    writeBytes(source, size);
}

void ByteWriter::giveBufferOwnership()
{
    deleteBuffer = false;
}

void ByteWriter::writeNetworkUShort(unsigned short value, void *destination)
{
    write<unsigned short>(htons(value), destination);
}

void ByteWriter::writeNetworkInt(int value, void *destination)
{    
    write<int>(htonl(value), destination);
}

void ByteWriter::writeNetworkInt(int value)
{    
    write<int>(htonl(value));
}

void ByteWriter::writeNetworkUShort(unsigned short value)
{
    write<unsigned short>(htons(value));
}