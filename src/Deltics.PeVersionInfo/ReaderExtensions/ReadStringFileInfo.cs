using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using Deltics.PeImageInfo.Reader;
using Deltics.VersionInfo.ReaderExtensions;


namespace Deltics.VersionInfo.ReaderExtensions
{
    internal static class ReadStringFileInfoExtension
    {
        internal static StringFileInfo ReadStringFileInfo(this PeReader reader)
        {
            var orgPos = reader.GetPosition();

            var info = new StringFileInfo();

            if (!reader.ReadFileInfo(info, "StringFileInfo"))
                return null;

            // Read all children (StringTables)
            var children = new List<StringTable>();
            while (reader.GetPosition() < orgPos + info.Length)
                children.Add(reader.ReadStringTable());

            info.Children = children.ToImmutableList();
            
            return info;
        }
    }
}