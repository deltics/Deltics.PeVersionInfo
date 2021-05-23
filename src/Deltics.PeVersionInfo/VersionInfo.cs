using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using Deltics.PeImageInfo.Reader;
using Deltics.PeResourceInfo;
using Deltics.VersionInfo.ReaderExtensions;


namespace Deltics.VersionInfo
{
    public class VersionInfo
    {
        private StringFileInfo _stringInfo;
        private Translation    _translation;

        public Translation ActiveTranslation
        {
            get => _translation;
            private set => SetTranslation(value.Code);
        }

        public VersionNumber                       FileVersionNumber    { get; }
        public VersionNumber                       ProductVersionNumber { get; }
        public ImmutableDictionary<string, string> Strings              { get; private set; }
        public ImmutableList<Translation>          Translations         { get; }
        
        public string Comments         => Strings.ContainsKey("Comments") ? Strings["Comments"] : null;
        public string CompanyName      => Strings.ContainsKey("CompanyName") ? Strings["CompanyName"] : null;
        public string FileDescription  => Strings.ContainsKey("FileDescription") ? Strings["FileDescription"] : null;
        public string FileVersion      => Strings.ContainsKey("FileVersion") ? Strings["FileVersion"] : null;
        public string InternalName     => Strings.ContainsKey("InternalName") ? Strings["InternalName"] : null;
        public string LegalCopyright   => Strings.ContainsKey("LegalCopyright") ? Strings["LegalCopyright"] : null;
        public string LegalTrademarks  => Strings.ContainsKey("LegalTrademarks") ? Strings["LegalTrademarks"] : null;
        public string OriginalFilename => Strings.ContainsKey("OriginalFilename") ? Strings["OriginalFilename"] : null;
        public string PrivateBuild     => Strings.ContainsKey("PrivateBuild") ? Strings["PrivateBuild"] : null;
        public string ProductName      => Strings.ContainsKey("ProductName") ? Strings["ProductName"] : null;
        public string ProductVersion   => Strings.ContainsKey("ProductVersion") ? Strings["ProductVersion"] : null;
        public string SpecialBuild     => Strings.ContainsKey("SpecialBuild") ? Strings["SpecialBuild"] : null;


        public VersionInfo(Stream stream)
        {
            var resources = new ResourceInfo(stream);
            if (!resources.IsValid)
                return;

            var resource = resources.GetResource(ResourceType.VERSION, 1);
            if (resource == null)
                return;

            var reader = resources.Reader;

            reader.SetPosition(resource.OffsetToData);
           
            var info = ReadVsVersionInfo(reader);

            FileVersionNumber    = new VersionNumber();
            ProductVersionNumber = new VersionNumber();

            FileVersionNumber.Major   = (ushort) ((info.Value.FileVersionMs & 0xffff0000) >> 16);
            FileVersionNumber.Minor   = (ushort) (info.Value.FileVersionMs & 0x0000ffff);
            FileVersionNumber.Build   = (ushort) ((info.Value.FileVersionLs & 0xffff0000) >> 16);
            FileVersionNumber.Private = (ushort) (info.Value.FileVersionLs & 0x0000ffff);

            ProductVersionNumber.Major   = (ushort) ((info.Value.ProductVersionMs & 0xffff0000) >> 16);
            ProductVersionNumber.Minor   = (ushort) (info.Value.ProductVersionMs & 0x0000ffff);
            ProductVersionNumber.Build   = (ushort) ((info.Value.ProductVersionLs & 0xffff0000) >> 16);
            ProductVersionNumber.Private = (ushort) (info.Value.ProductVersionLs & 0x0000ffff);

            _stringInfo = info.StringInfo;

            if (info.VarInfo == null)
                return;

            var translations = new List<Translation>();
            foreach (var code in info.VarInfo.Value.LanguageCodes)
                translations.Add(new Translation(code));

            Translations = translations.ToImmutableList();

            if ((Translations?.Count ?? 0) == 1)
                SetTranslation(Translations[0]);
        }


        private static VsVersionInfo ReadVsVersionInfo(PeReader reader)
        {
            var result = new VsVersionInfo();
            
            reader.ReadFileInfo(result, 15);

            result.Value      = reader.ReadVsFixedFileInfo();
            result.Padding2   = reader.ReadPadding();
            result.StringInfo = reader.ReadStringFileInfo();
            result.VarInfo    = reader.ReadVarFileInfo();

            return result;
        }        
        
        
        private bool SetTranslation(ulong languageCode)
        {
            var table  = _stringInfo.Children.FirstOrDefault(t => t.LanguageCode == languageCode);
            var result = table != null;
            
            Strings = table?.Values;

            if (result)
                _translation = Translations.FirstOrDefault(t => t.Code == languageCode);
            
            return result;
        }
        
        
        public bool SetTranslation(ushort languageId, ushort codepage)
        {
            return SetTranslation(((ulong) languageId << 16) & codepage);
        }
        
        
        public bool SetTranslation(Translation translation)
        {
            return SetTranslation(translation.Code);
        }
        
        
        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendLine($"File Version: {FileVersionNumber}");
            builder.AppendLine($"Product Version: {ProductVersionNumber}");

            builder.AppendLine("StringInfo: " + (_stringInfo != null ? "YES" : "NO"));
            builder.AppendLine("Translations: " + (Translations?.Count ?? 0));

            foreach (var xlat in Translations)
                builder.AppendLine($"   {xlat.LanguageId}, {xlat.CodePage}");

            return builder.ToString();
        }
    }
}