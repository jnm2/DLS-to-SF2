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
                var instruments = new List<DlsInstrument>();

                foreach (var dlsSubchunk in reader.ReadRiff("DLS ").ReadList())
                    switch (dlsSubchunk.Name)
                    {
                        case "lins":
                            instruments.AddRange(
                                from lrgnSubchunk in dlsSubchunk.ReadList()
                                where lrgnSubchunk.Name == "ins "
                                let instrument = ReadDlsInstrument(lrgnSubchunk)
                                where instrument != null
                                select instrument.Value);
                            break;
                    }

                return new DlsCollection(instruments);
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
    }
}
