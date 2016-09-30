namespace jnm2.SoundbankFormats.Dls
{
    public struct DlsWaveLink
    {
        public uint Channels { get; }
        public DlsWaveLinkOptions Options { get; }
        public ushort PhaseGroup { get; }
        public uint TableIndex { get; }

        public DlsWaveLink(DlsWaveLinkOptions options, ushort phaseGroup, uint channels, uint tableIndex)
        {
            Options = options;
            PhaseGroup = phaseGroup;
            Channels = channels;
            TableIndex = tableIndex;
        }
    }
}
