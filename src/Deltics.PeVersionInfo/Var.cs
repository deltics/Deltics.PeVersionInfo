using System.Collections.Immutable;

namespace Deltics.VersionInfo
{
    public class Var : FileInfo
    {
        public ImmutableArray<ulong> LanguageCodes { get; internal set; }
    }
}