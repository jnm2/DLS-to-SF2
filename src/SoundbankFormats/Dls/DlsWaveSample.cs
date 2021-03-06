﻿using System.Collections.Generic;

namespace jnm2.SoundbankFormats.Dls
{
    public struct DlsWaveSample
    {
        public ushort UnityNote { get; }
        public short FineTune { get; }
        public int Attenuation { get; }
        public DlsWaveSampleOptions Options { get; }
        public IReadOnlyList<DlsWaveSampleLoop> Loops { get; }

        public DlsWaveSample(ushort unityNote, short fineTune, int attenuation, DlsWaveSampleOptions options, IReadOnlyList<DlsWaveSampleLoop> loops)
        {
            UnityNote = unityNote;
            FineTune = fineTune;
            Attenuation = attenuation;
            Options = options;
            Loops = loops;
        }
    }
}