namespace Deltics.VersionInfo
{
    public class VsFixedFileInfo
    {
        public ulong Signature        { get; internal set; }
        public ulong StrucVersion     { get; internal set; }
        public ulong FileVersionMs    { get; internal set; }
        public ulong FileVersionLs    { get; internal set; }
        public ulong ProductVersionMs { get; internal set; }
        public ulong ProductVersionLs { get; internal set; }
        public ulong FileFlagsMask    { get; internal set; }
        public ulong FileFlags        { get; internal set; }
        public ulong FileOs           { get; internal set; }
        public ulong FileType         { get; internal set; }
        public ulong FileSubtype      { get; internal set; }
        public ulong FileDateMs       { get; internal set; }
        public ulong FileDateLs       { get; internal set; }
    }
}