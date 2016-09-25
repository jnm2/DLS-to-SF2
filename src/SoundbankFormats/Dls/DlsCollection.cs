using System.Collections.Generic;

namespace jnm2.SoundbankFormats.Dls
{
    public struct DlsCollection
    {
        public DlsCollection(DlsInfo info, IReadOnlyList<DlsInstrument> instruments, IReadOnlyList<DlsWaveFile> wavePool)
        {
            Info = info;
            Instruments = instruments;
            WavePool = wavePool;
        }

        public DlsInfo Info { get; }
        public IReadOnlyList<DlsInstrument> Instruments { get; }
        public IReadOnlyList<DlsWaveFile> WavePool { get; }
    }
}

