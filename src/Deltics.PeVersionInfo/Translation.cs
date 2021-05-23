namespace Deltics.VersionInfo
{
    public class Translation
    {
        public ulong Code { get; }

        public ushort LanguageId  => (ushort) ((Code & 0xffff0000) >> 16);
        public ushort Language    => (ushort) ((Code & 0x003f0000) >> 16);
        public ushort SubLanguage => (ushort) ((Code & 0xfc000000) >> 26);
        public ushort CodePage    => (ushort) (Code & 0x0000ffff);
        
        public Translation(ulong code)
        {
            this.Code = code;
        }
    }
}