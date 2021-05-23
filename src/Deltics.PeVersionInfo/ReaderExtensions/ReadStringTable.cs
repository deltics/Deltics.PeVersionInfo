using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.IO;
using System.Linq;
using Deltics.PeImageInfo.Reader;
using Deltics.VersionInfo;
using FileInfo = Deltics.VersionInfo.FileInfo;


namespace Deltics.VersionInfo.ReaderExtensions
{
    internal static class ReadStringTableExtensions
    {
        internal static StringTable ReadStringTable(this PeReader reader)
        {
            var orgPos = reader.GetPosition();
            
            var table  = new StringTable();
            
            reader.ReadFileInfo(table, 8);

            table.LanguageCode = ulong.Parse(table.Key, NumberStyles.HexNumber);

            // Read children (Strings, as sz key/value pairs)
            var children = new Dictionary<string, string>();
            while (reader.GetPosition() < orgPos + table.Length)
            {
                var child = reader.ReadStringTableString();
                children.Add(child.key, child.value);    
            }
            table.Values = children.ToImmutableDictionary();

            // Undocumented
            reader.ReadPadding();
            
            return table;
        }
        
        
        internal static (string key, string value) ReadStringTableString(this PeReader reader)
        {
            var entry = new StringTable.String();

            // Undocumented - StringTable entries are 32-bit aligned
            reader.ReadPadding();
            reader.ReadFileInfo(entry);

            return (entry.Key, reader.ReadStringZ());
        }
    }
}