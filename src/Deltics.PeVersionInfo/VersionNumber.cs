namespace Deltics.VersionInfo
{
    public class VersionNumber
    {
        public ushort Major {get; internal set;}
        public ushort Minor {get; internal set;}
        public ushort Build {get; internal set;}
        public ushort Private {get; internal set;}
        
        
        public override string ToString()
        {
            return $"{Major}.{Minor}.{Build}.{Private}";
        }
    }
}