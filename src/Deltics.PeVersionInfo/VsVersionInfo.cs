namespace Deltics.VersionInfo
{
    public class VsVersionInfo : FileInfo
    {
        public VsFixedFileInfo Value       { get; internal set; }
        public ushort[]        Padding2    { get; internal set; }

        public StringFileInfo StringInfo { get; internal set; }
        public VarFileInfo    VarInfo    { get; internal set; }
    }

}