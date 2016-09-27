namespace jnm2.SoundbankFormats.Dls
{
    public struct DlsRegion
    {
        public ushort KeyGroup { get; }
        public ushort RangeKeyHigh { get; }
        public ushort RangeKeyLow { get; }
        public ushort RangeVelocityHigh { get; }
        public ushort RangeVelocityLow { get; }
        public bool SelfNonExclusive { get; }
        public DlsWaveSample? WaveSample { get; }

        public DlsRegion(ushort rangeKeyLow, ushort rangeKeyHigh, ushort rangeVelocityLow, ushort rangeVelocityHigh, bool selfNonExclusive, ushort keyGroup, DlsWaveSample? waveSample)
        {
            RangeKeyLow = rangeKeyLow;
            RangeKeyHigh = rangeKeyHigh;
            RangeVelocityLow = rangeVelocityLow;
            RangeVelocityHigh = rangeVelocityHigh;
            SelfNonExclusive = selfNonExclusive;
            KeyGroup = keyGroup;
            WaveSample = waveSample;
        }
    }
}