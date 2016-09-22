using System;
using System.IO;
using System.Text;

namespace jnm2.SoundbankFormats.Riff
{
    public sealed class RiffReader : IDisposable
    {
        private readonly BinaryReader reader;

        public RiffReader(Stream stream, bool leaveOpen = true)
        {
            reader = new BinaryReader(stream, Encoding.ASCII, leaveOpen);
        }

        public void Dispose() => reader.Dispose();

        public RiffChunk ReadRiff() => RiffChunk.ReadRiff(reader);
        public RiffChunk ReadRiff(string expectedName)
        {
            var r = ReadRiff();
            if (r.Name != expectedName) throw new InvalidDataException($"Expected \"{expectedName}\" but found \"{r.Name}\".");
            return r;
        }
    }
}