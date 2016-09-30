using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace jnm2.SoundbankFormats.Dls
{
    [DebuggerDisplay("{ToString(),nq}")]
    public struct DlsInstrument
    {
        public DlsInstrument(Guid? id, DlsInfo info, byte bankMsb, byte bankLsb, byte patch, bool isPercussion, IReadOnlyList<DlsRegion> regions, IReadOnlyList<DlsArticulator> articulatorList)
        {
            Id = id;
            Info = info;
            BankMsb = bankMsb;
            BankLsb = bankLsb;
            Patch = patch;
            IsPercussion = isPercussion;
            Regions = regions;
            ArticulatorList = articulatorList;
        }

        public Guid? Id { get; }
        public DlsInfo Info { get; set; }
        public byte BankMsb { get; }
        public byte BankLsb { get; }
        public byte Patch { get; }
        public bool IsPercussion { get; }

        public IReadOnlyList<DlsRegion> Regions { get; }
        public IReadOnlyList<DlsArticulator> ArticulatorList { get; set; }

        public override string ToString() => Info.Name;
    }
}