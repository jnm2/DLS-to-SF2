using System;
using System.IO;

namespace jnm2.SoundbankFormats
{
    public static class BinaryReaderExtensions
    {
        public static void Skip(this BinaryReader reader, long length)
        {
            if (length < 0) throw new ArgumentOutOfRangeException(nameof(length), length, "Length must be greater than or equal to zero.");
            if (reader.BaseStream.CanSeek)
                reader.BaseStream.Seek(length, SeekOrigin.Current);
            else
                while (length != 0 && reader.BaseStream.ReadByte() != -1)
                    length--;
        }
    }
}