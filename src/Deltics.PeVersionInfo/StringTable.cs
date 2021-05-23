using System.Collections.Immutable;

namespace Deltics.VersionInfo
{
    public class StringTable : FileInfo
    {
        public class String : FileInfo
        {
            public string Value { get; internal set; } 
        }
        
        public ulong  LanguageCode { get; internal set; }
        public ushort LanguageId   => (ushort) ((LanguageCode & 0xffff0000) >> 16);
        public ushort Language     => (ushort) ((LanguageCode & 0x003f0000) >> 16); 
        public ushort SubLanguage  => (ushort) ((LanguageCode & 0xfc000000) >> 26);
        public ushort CodePage     => (ushort) (LanguageCode & 0x0000ffff);
        
        public ImmutableDictionary<string, string> Values       { get; internal set; }
    }
}