using System.IO;
using jnm2.SoundbankFormats.Dls;

namespace jnm2.DLS_to_SF2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var dlsFile = File.OpenRead(args[0]))
            {
                var collection = DlsReader.Read(dlsFile);
            }
        }
    }
}
