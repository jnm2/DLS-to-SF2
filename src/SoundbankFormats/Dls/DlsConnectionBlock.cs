using System.Diagnostics;

namespace jnm2.SoundbankFormats.Dls
{
    [DebuggerDisplay("{ToString(),nq}")]
    public struct DlsConnectionBlock
    {
        public DlsConnectionBlockSource Control { get; }
        public DlsConnectionBlockDestination Destination { get; }
        public int Scale { get; }
        public DlsConnectionBlockSource Source { get; }
        public DlsConnectionBlockTransform Transform { get; }

        public DlsConnectionBlock(DlsConnectionBlockSource source, DlsConnectionBlockSource control, DlsConnectionBlockDestination destination, DlsConnectionBlockTransform transform, int scale)
        {
            Source = source;
            Control = control;
            Destination = destination;
            Transform = transform;
            Scale = scale;
        }

        public override string ToString() => $"{Source} ({Control}) -> {Destination}";
    }
}
