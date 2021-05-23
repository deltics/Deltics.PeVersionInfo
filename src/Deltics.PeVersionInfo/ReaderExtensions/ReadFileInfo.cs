using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.IO;
using Deltics.PeImageInfo.Reader;
using Deltics.VersionInfo;
using FileInfo = Deltics.VersionInfo.FileInfo;


namespace Deltics.VersionInfo.ReaderExtensions
{
    internal static class ReadFileInfoExtension
    {
        internal static void ReadFileInfo(this PeReader reader, FileInfo info)
        {
            info.Position = reader.GetPosition();
            
            info.Length      = reader.ReadUInt16();
            info.ValueLength = reader.ReadUInt16();
            info.Type        = reader.ReadUInt16();
            info.Key         = reader.ReadStringZ();
            info.Padding1    = reader.ReadPadding();
        }

        
        internal static void ReadFileInfo(this PeReader reader, FileInfo info, int keyLen)
        {
            info.Position = reader.GetPosition();
            
            info.Length      = reader.ReadUInt16();
            info.ValueLength = reader.ReadUInt16();
            info.Type        = reader.ReadUInt16();
            info.Key         = reader.ReadStringZ(keyLen);
            info.Padding1    = reader.ReadPadding();
        }

        
        internal static bool ReadFileInfo(this PeReader reader, FileInfo info, string expectedKey)
        {
            var orgPos = reader.GetPosition();
            
            reader.ReadFileInfo(info, expectedKey.Length);

            var result = info.Key.Equals(expectedKey, StringComparison.Ordinal);

            if (!result)
            {
                info.Position = 0;
                reader.SetPosition(orgPos);
            }

            return result;
        }
    }
}