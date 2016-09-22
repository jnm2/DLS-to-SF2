using System.Collections.Generic;

namespace jnm2.SoundbankFormats.Dls
{
    public struct DlsInstrument
    {
        public DlsInstrument(byte bankMsb, byte bankLsb, byte patch, bool isPercussion, IReadOnlyList<DlsRegion> ranges)
        {
            BankMsb = bankMsb;
            BankLsb = bankLsb;
            Patch = patch;
            IsPercussion = isPercussion;
            Ranges = ranges;
        }

        public byte BankMsb { get; }
        public byte BankLsb { get; }
        public byte Patch { get; }
        public bool IsPercussion { get; }

        public IReadOnlyList<DlsRegion> Ranges { get; }
    }
}