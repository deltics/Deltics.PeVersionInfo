using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.IO;
using Deltics.PeImageInfo.Reader;
using Deltics.VersionInfo;
using FileInfo = Deltics.VersionInfo.FileInfo;

namespace Deltics.VersionInfo.ReaderExtensions
{
    internal static class ReadVsFixedFileInfoExtension
    {
        internal static VsFixedFileInfo ReadVsFixedFileInfo(this PeReader reader)
        {
            return new()
            {
                Signature        = reader.ReadUInt32(),
                StrucVersion     = reader.ReadUInt32(),
                FileVersionMs    = reader.ReadUInt32(),
                FileVersionLs    = reader.ReadUInt32(),
                ProductVersionMs = reader.ReadUInt32(),
                ProductVersionLs = reader.ReadUInt32(),
                FileFlagsMask    = reader.ReadUInt32(),
                FileFlags        = reader.ReadUInt32(),
                FileOs           = reader.ReadUInt32(),
                FileType         = reader.ReadUInt32(),
                FileSubtype      = reader.ReadUInt32(),
                FileDateMs       = reader.ReadUInt32(),
                FileDateLs       = reader.ReadUInt32()
            };
        }
    }
}