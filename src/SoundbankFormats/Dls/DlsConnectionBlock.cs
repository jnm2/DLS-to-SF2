using System.Diagnostics;
using System.Text;

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

        public override string ToString()
        {
            var r = new StringBuilder();

            if (Source != DlsConnectionBlockSource.None)
            {
                r.Append(Source).Append(" × ");

                if (Control != DlsConnectionBlockSource.None)
                    r.Append(Control).Append(" × ");
            }

            r.Append(Scale).Append(" → ").Append(Destination);
            if (Transform != DlsConnectionBlockTransform.None) r.Append(" (").Append(Transform).Append(')');
            return r.ToString();
        }
    }
}
