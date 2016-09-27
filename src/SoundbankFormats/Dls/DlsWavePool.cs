using System.Diagnostics;

namespace jnm2.SoundbankFormats.Dls
{
    [DebuggerDisplay("{ToString(),nq}")]
    public struct DlsWaveFile
    {
        public uint AvgBytesPerSec { get; }
        public ushort? BitsPerSample { get; }
        public ushort BlockAlign { get; }
        public DlsWaveFormat Format { get; }
        public ushort NumChannels { get; }
        public uint SamplesPerSecond { get; }
        public byte[] Data { get; }
        public DlsWaveSample? WaveSample { get; }
        public DlsInfo Info { get; set; }

        public DlsWaveFile(DlsWaveFormat format, ushort numChannels, uint samplesPerSecond, uint avgBytesPerSec, ushort blockAlign, ushort? bitsPerSample, byte[] data, DlsWaveSample? waveSample, DlsInfo info)
        {
            Format = format;
            NumChannels = numChannels;
            SamplesPerSecond = samplesPerSecond;
            AvgBytesPerSec = avgBytesPerSec;
            BlockAlign = blockAlign;
            BitsPerSample = bitsPerSample;
            Data = data;
            WaveSample = waveSample;
            Info = info;
        }

        public override string ToString() => Info.Name;
    }
}
