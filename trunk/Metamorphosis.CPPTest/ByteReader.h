#pragma once
#include <string>

//! Class for reading data from byte array
/*! Provide functionality to read data from data source (void*) or use static method with data source as parameter*/
class ByteReader
{
private:
    //! Data source given in constructor
    char *buffer;
    //! Current reading position
    char *position;

public:
    //! Main constructor
    /*!
        \param data Data source from where data will be readed
    */
    ByteReader(const void *source);

    //! Main descrtuctor
    ~ByteReader(void);

    //! Get position pointer
    void *getPosition();

    //! Skip bytes
    void skipBytes(int count);

    //! Read any type
    /*! Return variable that will be filled with data from data source */
    template<typename T>
    T read()
    {
        T value;
        readBytes(&value, sizeof(T));
        return value;
    }

    //! Read any type
    /*! Return variable that will be filled with data from data source.
        \param source Data source
    */
    template<typename T>
    static T read(void * source) 
    {
        T value;
        memcpy(&value, source, sizeof(T));
        return value;
    }

    //! Read bytes from data source
    void readBytes(void *value, size_t size);

    //! Read short from network byte order
    static short readNetworkShort(void *source);

    //! Read unsigned short from network byte order
    static unsigned short readNetworkUShort(void *source);

    //! Read int from network byte order
    int readNetworkInt();

    //! Read unsigned short from network byte order
    unsigned short readNetworkUShort();

    //! Read data
    /*! Read data in format: 2 bytes data size (network byte order) and data. 
        \param destinaion Pointer to buffer pointer. If buffer pointer is NULL then memory will be allocated for this operation. 
        In this case caller is responsible to delete this buffer.
        \return Return data size readed. Return -1 if error occurs.
    */
    size_t readData(void **destination);

    //! Read C string
    /*! Read C string in format: 2 bytes data size (network byte order) and C string data. 
        \param destinaion std::string where to save readed string.
        \return Return C string size readed. Return -1 if error occurs.
    */
    std::string readString();

    //! \return Returns bytes readed count
    int bytesReaded();
};

