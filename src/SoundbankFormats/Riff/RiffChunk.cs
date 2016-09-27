using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace jnm2.SoundbankFormats.Riff
{
    [DebuggerDisplay("\\{{Name,nq}\\}")]
    public sealed class RiffChunk
    {
        public bool IsList { get; }
        public string Name { get; }
        public uint Length { get; }

        private uint lengthLeft;
        private readonly BinaryReader reader;
        private RiffChunk currentSubchunk;

        private RiffChunk(string name, bool isList, uint length, BinaryReader reader)
        {
            Name = name;
            Length = length;
            IsList = isList;
            lengthLeft = length;
            this.reader = reader;
        }

        public static RiffChunk ReadRiff(BinaryReader reader)
        {
            var type = new string(reader.ReadChars(4));
            if (type != "RIFF") throw new InvalidDataException($"Expected \"RIFF\" but found \"{type}\".");
            var length = reader.ReadUInt32();
            var formName = new string(reader.ReadChars(4));
            return new RiffChunk(formName, true, length - 4, reader);
        }


        private void TakeLength(uint length)
        {
            if (lengthLeft < length) throw new InvalidDataException("Unexpected end of chunk.");
            lengthLeft -= length;
        }

        private void CheckInvalidOperationIfList([CallerMemberName] string callerName = null)
        {
            if (IsList) throw new InvalidOperationException($"Cannot {callerName} on list chunks. Check {nameof(IsList)} before calling.");
        }

        public void Skip(uint numBytes)
        {
            CheckInvalidOperationIfList();
            TakeLength(numBytes);
            reader.Skip(numBytes);
        }

        public short ReadInt16()
        {
            CheckInvalidOperationIfList();
            TakeLength(2);
            return reader.ReadInt16();
        }

        public ushort ReadUInt16()
        {
            CheckInvalidOperationIfList();
            TakeLength(2);
            return reader.ReadUInt16();
        }

        public int ReadInt32()
        {
            CheckInvalidOperationIfList();
            TakeLength(4);
            return reader.ReadInt32();
        }

        public uint ReadUInt32()
        {
            CheckInvalidOperationIfList();
            TakeLength(4);
            return reader.ReadUInt32();
        }

        public string ReadNullTerminatedString()
        {
            CheckInvalidOperationIfList();

            var sb = new StringBuilder();

            while (true)
            {
                if (lengthLeft == 0) throw new InvalidDataException("Unterminated string.");
                lengthLeft--;

                var c = reader.Read();
                switch (c)
                {
                    case -1:
                        throw new InvalidDataException("Unterminated string and unexpected end of chunk.");
                    case 0:
                        return sb.ToString();
                }

                sb.Append((char)c);
            }
        }

        public byte[] ReadBytes(int numBytes)
        {
            if (numBytes < 0) throw new ArgumentOutOfRangeException(nameof(numBytes), numBytes, "Number of bytes must not be negative.");
            CheckInvalidOperationIfList();
            TakeLength((uint)numBytes);
            return reader.ReadBytes(numBytes);
        }

        public byte[] ReadAllBytes()
        {
            CheckInvalidOperationIfList();
            var r = reader.ReadBytes(checked((int)lengthLeft));
            lengthLeft = 0;
            return r;
        }



        public IEnumerable<RiffChunk> ReadList()
        {
            if (!IsList) throw new InvalidOperationException($"This is not a list chunk. Check {nameof(IsList)} before calling.");
            return new SubchunkEnumerable(this);
        }

        private void Skip()
        {
            currentSubchunk?.Skip();

            var lengthToSkip = lengthLeft + (Length & 1); // Round total length up to nearest 16-bit word; i.e. if it's odd, skip one extra.
            if (lengthToSkip == 0) return;
            reader.Skip(lengthToSkip);
            lengthLeft -= lengthToSkip;
        }

        private sealed class SubchunkEnumerable : IEnumerable<RiffChunk>
        {
            private readonly RiffChunk riffChunk;

            public SubchunkEnumerable(RiffChunk riffChunk)
            {
                this.riffChunk = riffChunk;
            }

            public IEnumerator<RiffChunk> GetEnumerator() => new SubchunkEnumerator(riffChunk);

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        private sealed class SubchunkEnumerator : IEnumerator<RiffChunk>
        {
            private readonly RiffChunk chunk;

            public SubchunkEnumerator(RiffChunk chunk)
            {
                this.chunk = chunk;
            }

            public void Dispose() => chunk.Skip();

            public bool MoveNext()
            {
                chunk.currentSubchunk?.Skip();

                if (chunk.lengthLeft == 0)
                {
                    chunk.currentSubchunk = null;
                    return false;
                }

                if (chunk.lengthLeft < 8) throw new InvalidDataException("Not enough room left for another chunk.");
                var subchunkName = new string(chunk.reader.ReadChars(4));
                var subchunkLength = chunk.reader.ReadUInt32();
                chunk.lengthLeft -= 8;

                var physicalSubchunkLength = ((subchunkLength + 1) >> 1) << 1; // Round up to nearest 16 bits
                if (chunk.lengthLeft < physicalSubchunkLength) throw new InvalidDataException("Not enough room left for the subchunk.");
                chunk.lengthLeft -= physicalSubchunkLength;

                var isList = subchunkName == "LIST";
                if (isList)
                {
                    if (subchunkLength < 4) throw new InvalidDataException("Not enough room in the subchunk for the list name.");
                    subchunkName = new string(chunk.reader.ReadChars(4));
                    subchunkLength -= 4;
                }

                chunk.currentSubchunk = new RiffChunk(subchunkName, isList, subchunkLength, chunk.reader);
                return true;
            }

            public void Reset()
            {
                throw new NotSupportedException();
            }

            public RiffChunk Current => chunk.currentSubchunk;

            object IEnumerator.Current => Current;
        }
    }
}