#include "StdAfx.h"

#include "ByteReader.h"
#include <Winsock2.h>
#include <malloc.h>

ByteReader::ByteReader(const void *source)
{
    buffer = (char*)source;
    position = buffer;
}

ByteReader::~ByteReader(void)
{
}

void *ByteReader::getPosition()
{
    return position;
}

void ByteReader::skipBytes(int count)
{
    position += count;
}

void ByteReader::readBytes(void *value, size_t size)
{
    memcpy(value, position, size);
    position += size;
}

short ByteReader::readNetworkShort(void *source)
{
    short value = ByteReader::read<short>(source);
    return ntohs(value);
}

unsigned short ByteReader::readNetworkUShort(void *source)
{
    unsigned short value = ByteReader::read<unsigned short>(source);
    return ntohs(value);
}

int ByteReader::readNetworkInt()
{
    int value = ByteReader::read<int>();
    return ntohl(value);
}

unsigned short ByteReader::readNetworkUShort()
{
    unsigned short value = ByteReader::read<unsigned short>();
    return ntohs(value);
}

size_t ByteReader::readData(void **destination)
{
    u_short size = read<u_short>();

    if (!size)
    {
        *destination = NULL;
        return 0;
    }

    void *buf = *destination;

    if (buf == NULL)
    {
        buf = malloc(size);
        if (buf == NULL)
        {
            return -1;
        }
        *destination = buf;
    }

    readBytes(buf, size);

    return size;
}

std::string ByteReader::readString()
{
    u_short size = read<u_short>();
    std::string result;

    result.assign(position, size);

    position += size;

    return result;
}

int ByteReader::bytesReaded()
{
    return position - buffer;
}