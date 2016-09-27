using System;

namespace jnm2.SoundbankFormats.Dls
{
    [Flags]
    public enum DlsWaveSampleOptions : uint
    {
        None = 0,
        NoTruncation = 1,
        NoCompression = 2
    }
}
