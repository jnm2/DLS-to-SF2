namespace jnm2.SoundbankFormats.Dls
{
    public struct DlsArticulator
    {
        public DlsConnectionBlock[] ConnectionBlocks { get; }

        public DlsArticulator(DlsConnectionBlock[] connectionBlocks)
        {
            ConnectionBlocks = connectionBlocks;
        }
    }
}
