using System.Collections.Immutable;

namespace Deltics.VersionInfo
{
    public class VarFileInfo : FileInfo
    {
        public Var Value { get; internal set; }
    }

}