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

        public void Read(out int value)
        {
            value = br.ReadInt32();
        }

        public void Read(out uint value)
        {
            value = br.ReadUInt32();
        }

        public void Read(out byte value)
        {
            value = br.ReadByte();
        }

        public void Read(out short value)
        {
            value = br.ReadInt16();
        }

        public void Read(out ushort value)
        {
            value = br.ReadUInt16();
        }

        public void Read(out long value)
        {
            value = br.ReadInt64();
        }

        public void Read(out ulong value)
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
