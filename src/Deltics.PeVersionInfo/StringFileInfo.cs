using System.Collections.Immutable;


namespace Deltics.VersionInfo
{
    public class StringFileInfo : FileInfo
    {
        public ImmutableList<StringTable> Children { get; internal set; }
    }
}