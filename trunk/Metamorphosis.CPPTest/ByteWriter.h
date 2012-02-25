#pragma once
#include <string>

//! Class for writing data to byte array
class ByteWriter
{
private:
    //! Buffer to store data
    char *buffer;
    //! Current position in buffer
    char *position;
    //! Buffer size
    size_t size;
    //! Need to delete buffer when this object will be deleted. Object is responsible for deleting buffer
    bool deleteBuffer;

public:    
    //! Main constructor to create ByteWritter object
    /*! It's possible to use internal or external buffer for data output
        \param data Pointer to memory where to write output. If NULL memory (size bytes) will be allocated by object. Object is responsible for deleting this buffer
        \param size Data size or if data is NULL means how big buffer must be allocated
    */
    ByteWriter(void *data, size_t size);

    //! Main destructor
    ~ByteWriter(void);

    //! Get position pointer
    void *getPosition();

    //! Skip bytes
    void skipBytes(int count);

    //! Write size byte from source to output buffer
    void writeBytes(const void *source, size_t size);

    //! Write any type
    template<typename T>
    void write(T value)
    {
        writeBytes((void*)&value, sizeof(T));
    }

    //! Write any type to memory
    /*! 
        \param destination Data destination
    */
    template<typename T>
    static void write(T value, void * destination)
    {
        memcpy(destination, &value, sizeof(T));
    }

    //! Get pointer to written data
    void *getData();

    //! Get written data size
    int getDataSize();

    //! Write C string. 2 bytes length and then symbols.
    /*! String length must be less that 2^16-1. String length will be written in network byte order
        \param source Pointer to C string.
    */
    void writeString(std::string source);

    //! Write data. 2 bytes length and then data.
    /*! Data length must be less that 2^16-1. Data length will be written in network byte order
        \param source Pointer to data.
        \param size Data size
    */
    void writeData(const void *source, unsigned short size);

    //! Give buffer ownership to another object. When this object will be delete buffer will not be deleted.
    void giveBufferOwnership();

    //! Write unsigned short to network byte order
    static void writeNetworkUShort(unsigned short value, void *destination);

    //! Write int from network byte order
    static void writeNetworkInt(int value, void *destination);

    //! Write int from network byte order
    void writeNetworkInt(int value);

    //! Write unsigned short from network byte order
    void writeNetworkUShort(unsigned short value);
};

