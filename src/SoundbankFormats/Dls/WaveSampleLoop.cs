namespace jnm2.SoundbankFormats.Dls
{
    public struct WaveSampleLoop
    {
        public uint Start { get; }
        public uint Length { get; }
        public WaveSampleLoopType Type { get; }

        public WaveSampleLoop(WaveSampleLoopType type, uint start, uint length)
        {
            Type = type;
            Start = start;
            Length = length;
        }
    }
}
