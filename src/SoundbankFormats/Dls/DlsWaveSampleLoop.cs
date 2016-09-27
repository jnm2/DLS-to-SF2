namespace jnm2.SoundbankFormats.Dls
{
    public struct DlsWaveSampleLoop
    {
        public uint Start { get; }
        public uint Length { get; }
        public DlsWaveSampleLoopType Type { get; }

        public DlsWaveSampleLoop(DlsWaveSampleLoopType type, uint start, uint length)
        {
            Type = type;
            Start = start;
            Length = length;
        }
    }
}
