using System.Collections.Generic;

namespace jnm2.SoundbankFormats.Dls
{
    public struct DlsCollection
    {
        public DlsCollection(IReadOnlyList<DlsInstrument> instruments)
        {
            Instruments = instruments;
        }

        public IReadOnlyList<DlsInstrument> Instruments { get; }
    }
}
