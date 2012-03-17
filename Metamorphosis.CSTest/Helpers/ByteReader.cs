using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public class ByteReader
    {
        MemoryStream stream;
        BinaryReader br;

        public ByteReader(byte []data)
        {
            stream = new MemoryStream(data);
            br = new BinaryReader(stream);
        }

        public void Read(ref int value)
        {
            value = br.ReadInt32();
        }

        public void Read(ref uint value)
        {
            value = br.ReadUInt32();
        }

        public void Read(ref byte value)
        {
            value = br.ReadByte();
        }

        public void Read(ref short value)
        {
            value = br.ReadInt16();
        }

        public void Read(ref ushort value)
        {
            value = br.ReadUInt16();
        }

        public void Read(ref long value)
        {
            value = br.ReadInt64();
        }

        public void Read(ref ulong value)
        {
            value = br.ReadUInt64();
        }

        public string ReadString()
        {
            byte[] bytes = ReadBytes();
            return Encoding.ASCII.GetString(bytes);
        }

        public byte[] ReadBytes()
        {
            ushort s = br.ReadUInt16();
            return br.ReadBytes(s);
        }
    }
}
