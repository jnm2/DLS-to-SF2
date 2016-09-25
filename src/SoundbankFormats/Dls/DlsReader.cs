using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using jnm2.SoundbankFormats.Riff;

namespace jnm2.SoundbankFormats.Dls
{
    public static class DlsReader
    {
        public static DlsCollection Read(Stream stream)
        {
            using (var reader = new RiffReader(stream))
            {
                var collectionVersion = default(Version);
                var instruments = new List<DlsInstrument>();
                var wavePool = new List<DlsWaveFile>();
                var info = new DlsInfo();

                foreach (var dlsSubchunk in reader.ReadRiff("DLS ").ReadList())
                    switch (dlsSubchunk.Name)
                    {
                        case "vers":
                            var minor = dlsSubchunk.ReadUInt16();
                            var major = dlsSubchunk.ReadUInt16();
                            var revision = dlsSubchunk.ReadUInt16();
                            var build = dlsSubchunk.ReadUInt16();
                            collectionVersion = new Version(major, minor, build, revision);
                            break;
                        case "lins":
                            instruments.AddRange(
                                from lrgnSubchunk in dlsSubchunk.ReadList()
                                where lrgnSubchunk.Name == "ins "
                                let instrument = ReadDlsInstrument(lrgnSubchunk)
                                where instrument != null
                                select instrument.Value);
                            break;
                        case "wvpl":
                            wavePool.AddRange(
                                from wvplSubchunk in dlsSubchunk.ReadList()
                                where wvplSubchunk.Name == "wave"
                                let waveFile = ReadDlsWaveFile(wvplSubchunk)
                                where waveFile != null
                                select waveFile.Value);
                            break;
                        case "INFO":
                            info = ReadDlsInfo(dlsSubchunk);
                            break;
                    }

                return new DlsCollection(collectionVersion, info, instruments, wavePool);
            }
        }

        private static DlsInstrument? ReadDlsInstrument(RiffChunk insChunk)
        {
            var isHeaderSet = false;
            byte msb = 0, lsb = 0, patch = 0;
            var isPercussion = false;

            var regions = new List<DlsRegion>();

            foreach (var insSubchunk in insChunk.ReadList())
                switch (insSubchunk.Name)
                {
                    case "insh":
                        isHeaderSet = true;
                        insSubchunk.ReadUInt32(); // NumRegions
                        var bank = insSubchunk.ReadUInt32();
                        lsb = (byte)bank;
                        msb = (byte)(bank >> 8);
                        isPercussion = (bank & 0x8000000u) != 0;
                        patch = (byte)insSubchunk.ReadUInt32();
                        break;
                    case "lrgn":
                        regions.AddRange(
                            from lrgnSubchunk in insSubchunk.ReadList()
                            where lrgnSubchunk.Name == "rgn "
                            let region = ReadDlsRegion(lrgnSubchunk)
                            where region != null
                            select region.Value);
                        break;
                }

            if (!isHeaderSet) return null;
            return new DlsInstrument(msb, lsb, patch, isPercussion, regions);
        }

        private static DlsRegion? ReadDlsRegion(RiffChunk rgnChunk)
        {
            var isHeaderSet = false;
            ushort rangeKeyLow = 0;
            ushort rangeKeyHigh = 0;
            ushort rangeVelocityLow = 0;
            ushort rangeVelocityHigh = 0;
            var selfNonExclusive = false;
            ushort keyGroup = 0;

            foreach (var rgnSubchunk in rgnChunk.ReadList())
                switch (rgnSubchunk.Name)
                {
                    case "rgnh":
                        isHeaderSet = true;
                        rangeKeyLow = rgnSubchunk.ReadUInt16();
                        rangeKeyHigh = rgnSubchunk.ReadUInt16();
                        rangeVelocityLow = rgnSubchunk.ReadUInt16();
                        rangeVelocityHigh = rgnSubchunk.ReadUInt16();
                        selfNonExclusive = rgnSubchunk.ReadUInt16() == 1;
                        keyGroup = rgnSubchunk.ReadUInt16();
                        break;
                }

            if (!isHeaderSet) return null;
            return new DlsRegion(rangeKeyLow, rangeKeyHigh, rangeVelocityLow, rangeVelocityHigh, selfNonExclusive, keyGroup);
        }



        private static DlsWaveFile? ReadDlsWaveFile(RiffChunk waveChunk)
        {
            var format = default(DlsWaveFormat);
            var numChannels = default(ushort);
            var samplesPerSecond = default(uint);
            var avgBytesPerSec = default(uint);
            var blockAlign = default(ushort);
            var bitsPerSample = default(ushort?);
            var data = default(byte[]);
            var info = new DlsInfo();

            foreach (var waveSubchunk in waveChunk.ReadList())
                switch (waveSubchunk.Name)
                {
                    case "fmt ":
                        format = (DlsWaveFormat)waveSubchunk.ReadUInt16();
                        numChannels = waveSubchunk.ReadUInt16();
                        samplesPerSecond = waveSubchunk.ReadUInt32();
                        avgBytesPerSec = waveSubchunk.ReadUInt32();
                        blockAlign = waveSubchunk.ReadUInt16();
                        if (format == DlsWaveFormat.PCM) bitsPerSample = waveSubchunk.ReadUInt16();
                        break;
                    case "data":
                        data = waveSubchunk.ReadAllBytes();
                        break;
                    case "INFO":
                        info = ReadDlsInfo(waveSubchunk);
                        break;
                }

            return new DlsWaveFile(format, numChannels, samplesPerSecond, avgBytesPerSec, blockAlign, bitsPerSample, data, info);
        }



        private static DlsInfo ReadDlsInfo(RiffChunk infoChunk)
        {
            var archivalLocation = (string)null;
            var artist = (string)null;
            var commissioned = (string)null;
            var comments = (string)null;
            var copyright = (string)null;
            var creationDate = (DateTime?)null;
            var engineer = (string)null;
            var genre = (string)null;
            var keywords = (string)null;
            var medium = (string)null;
            var name = (string)null;
            var product = (string)null;
            var subject = (string)null;
            var software = (string)null;
            var source = (string)null;
            var sourceForm = (string)null;
            var technician = (string)null;

            foreach (var infoSubchunk in infoChunk.ReadList())
                switch (infoSubchunk.Name)
                {
                    case "IARL":
                        archivalLocation = infoSubchunk.ReadNullTerminatedString();
                        break;
                    case "IART":
                        artist = infoSubchunk.ReadNullTerminatedString();
                        break;
                    case "ICMS":
                        commissioned = infoSubchunk.ReadNullTerminatedString();
                        break;
                    case "ICMT":
                        comments = infoSubchunk.ReadNullTerminatedString();
                        break;
                    case "ICOP":
                        copyright = infoSubchunk.ReadNullTerminatedString();
                        break;
                    case "ICRD":
                        DateTime value;
                        if (DateTime.TryParse(infoSubchunk.ReadNullTerminatedString(), out value)) creationDate = value;
                        break;
                    case "IENG":
                        engineer = infoSubchunk.ReadNullTerminatedString();
                        break;
                    case "IGNR":
                        genre = infoSubchunk.ReadNullTerminatedString();
                        break;
                    case "IKEY":
                        keywords = infoSubchunk.ReadNullTerminatedString();
                        break;
                    case "IMED":
                        medium = infoSubchunk.ReadNullTerminatedString();
                        break;
                    case "INAM":
                        name = infoSubchunk.ReadNullTerminatedString();
                        break;
                    case "IPRD":
                        product = infoSubchunk.ReadNullTerminatedString();
                        break;
                    case "ISBJ":
                        subject = infoSubchunk.ReadNullTerminatedString();
                        break;
                    case "ISFT":
                        software = infoSubchunk.ReadNullTerminatedString();
                        break;
                    case "ISRC":
                        source = infoSubchunk.ReadNullTerminatedString();
                        break;
                    case "ISRF":
                        sourceForm = infoSubchunk.ReadNullTerminatedString();
                        break;
                    case "ITCH":
                        technician = infoSubchunk.ReadNullTerminatedString();
                        break;
                }

            return new DlsInfo(
                archivalLocation,
                artist,
                commissioned,
                comments,
                copyright,
                creationDate,
                engineer,
                genre,
                keywords,
                medium,
                name,
                product,
                subject,
                software,
                source,
                sourceForm,
                technician);
        }
    }
}
