using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.IO;
using Deltics.PeImageInfo.Reader;
using Deltics.VersionInfo;
using FileInfo = Deltics.VersionInfo.FileInfo;

namespace Deltics.VersionInfo.ReaderExtensions
{
    internal static class ReadVarFileInfoExtension
    {
        internal static VarFileInfo ReadVarFileInfo(this PeReader reader)
        {
            var orgPos = reader.GetPosition();

            var info = new VarFileInfo();
            if (!reader.ReadFileInfo(info, "VarFileInfo"))
                return null;

            var var = new Var();
            if (!reader.ReadFileInfo(var, "Translation"))
                return null;

            info.Value = var;
            
            var codes = new List<ulong>();
            while (reader.GetPosition() < var.Position + var.Length)
            {
                ulong code = reader.ReadUInt16();
                code = reader.ReadUInt16() | (code << 16);
                codes.Add(code);
            }

            info.Value.LanguageCodes = codes.ToImmutableArray();
            
            return info;
        }
    }
}