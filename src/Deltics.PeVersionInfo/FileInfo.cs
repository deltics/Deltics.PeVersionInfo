namespace Deltics.VersionInfo
{
    public class FileInfo
    {
        public ushort   Length      { get; internal set; }
        public ushort   ValueLength { get; internal set; }
        public ushort   Type        { get; internal set; }
        public string   Key         { get; internal set; }
        public ushort[] Padding1    { get; internal set; }

        internal ulong Position { get; set; }
    }
}