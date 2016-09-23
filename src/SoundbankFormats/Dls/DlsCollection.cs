using System.Collections.Generic;

namespace jnm2.SoundbankFormats.Dls
{
    public struct DlsCollection
    {
        public DlsCollection(DlsInfo info, IReadOnlyList<DlsInstrument> instruments)
        {
            Info = info;
            Instruments = instruments;
        }

        public DlsInfo Info { get; }
        public IReadOnlyList<DlsInstrument> Instruments { get; }
    }
}

