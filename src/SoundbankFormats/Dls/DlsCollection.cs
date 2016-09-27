using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace jnm2.SoundbankFormats.Dls
{
    [DebuggerDisplay("{ToString(),nq}")]
    public struct DlsCollection
    {
        public DlsCollection(Guid? id, Version collectionVersion, DlsInfo info, IReadOnlyList<DlsInstrument> instruments, IReadOnlyList<DlsWaveFile> wavePool)
        {
            CollectionVersion = collectionVersion;
            Info = info;
            Instruments = instruments;
            WavePool = wavePool;
            Id = id;
        }

        public Guid? Id { get; }
        public Version CollectionVersion { get; set; }
        public DlsInfo Info { get; }
        public IReadOnlyList<DlsInstrument> Instruments { get; }
        public IReadOnlyList<DlsWaveFile> WavePool { get; }

        public override string ToString() => Info.Name;
    }
}

