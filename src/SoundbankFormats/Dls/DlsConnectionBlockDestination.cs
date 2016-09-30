namespace jnm2.SoundbankFormats.Dls
{
    public enum DlsConnectionBlockDestination : ushort
    {
        None = 0x0,
        Attenuation = 0x1,
        Reserved = 0x2,
        Pitch = 0x3,
        Pan = 0x4,
        LfoFrequency = 0x104,
        LfoStartDelay = 0x105,
        Eg1AttackTime = 0x206,
        Eg1DecayTime = 0x207,
        Eg1Reserved = 0x208,
        Eg1ReleaseTime = 0x209,
        Eg1SustainLevel = 0x20A,
        Eg2AttackTime = 0x30A,
        Eg2DecayTime = 0x30B,
        Eg2Reserved = 0x30C,
        Eg2ReleaseTime = 0x30D,
        Eg2SustainLevel = 0x30E
    }
}