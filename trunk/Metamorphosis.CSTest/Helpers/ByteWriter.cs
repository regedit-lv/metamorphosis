using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public class ByteWriter
    {
        MemoryStream stream;
        BinaryWriter bw;

        public ByteWriter()
        {
            stream = new MemoryStream();
            bw = new BinaryWriter(stream);
        }

        public byte[] GetBuffer()
        {
            bw.Flush();
            return stream.ToArray();
        }

        public void Write(int value)
        {
            bw.Write(value);
        }

        public void Write(uint value)
        {
            bw.Write(value);
        }

        public void Write(byte value)
        {
            bw.Write(value);
        }

        public void Write(short value)
        {
            bw.Write(value);
        }

        public void Write(ushort value)
        {
            bw.Write(value);
        }

        public void Write(long value)
        {
            bw.Write(value);
        }

        public void Write(ulong value)
        {
            bw.Write(value);
        }

        public void Write(bool value)
        {
            bw.Write(value);
        }

        public void Write<T>(T value)
        {
            bw.Write((int)(object)value);
        }

        public void WriteString(string s)
        {
            WriteBytes(Encoding.ASCII.GetBytes(s));
        }

        public void WriteBytes(byte[] bytes)
        {
            bw.Write((ushort)bytes.Length);
            bw.Write(bytes);
        }

    }
}
