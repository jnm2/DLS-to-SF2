namespace jnm2.SoundbankFormats.Dls
{
    public enum DlsConnectionBlockSource : ushort
    {
        None = 0x0,
        LFO = 0x1,
        KeyOnVelocity = 0x2,
        KeyNumber = 0x3,
        EG1 = 0x4,
        EG2 = 0x5,
        PitchWheel = 0x6,
        CC1 = 0x81,
        CC7 = 0x87,
        CC10 = 0x8A,
        CC11 = 0x8B,
        RPN0 = 0x100,
        RPN1 = 0x101,
        RPN2 = 0x102
    }
}
