# Deltics.PeVersionInfo

This library builds on [`Deltics.PeImageInfo`](https://https://github.com/deltics/Deltics.PeImageInfo) and [`Deltics.PeResourceInfo'](https://github.com/deltics/Deltics.PeResourceInfo), to provide information extracted from the `VERSIONINFO` resource in a PE image.


# Usage

The `VersionInfo` constructor accepts a stream which is passed to an internal `ResourceInfo` object.  If version information is located in the PE image stream, then this is extracted and presented in various public properties of the `VersionInfo` object.  If there is no such information then these properties are null.

```
   var vi = new VersionInfo(new FileStream("somefile.exe", FileMode.Open))
```

## Available Version Info Properties

The following public properties provide access to the Version information in a PE image, where present:

* FileVersionNumber
* ProduceVersionNumber
* Strings
* Translations
* ActiveTranslation


### FileVersionNumber / ProductVersionNumber

`FileVersionNumber` and `ProductVersionNumber` present the fixed version info numbers for file and product, each of the form `&lt;major&gt;.&lt;minor&gt;.&lt;build&gt;.&lt;private&gt;'.

`Major`, `Minor`, `Build`, and `Private` fields in these version numbers are all 16-bit unsigned ints.


### Strings

The `Strings` property contains all string values contained in the version information for the `ActiveTranslation`.

In addition to the `Strings` collection, properties are also provided as short-cuts to the standard string values normally held in a VERSIONINFO resource:

* Comments
* CompanyName
* FileDescription
* FileVersion
* InternalName
* LegalCopyright
* LegalTrademarks
* OriginalFilename
* PrivateBuild
* ProductName
* ProductVersion
* SpecialBuild

Note that `FileVersion` and `ProductVersion` values here are strings, held separately from the `FileVersionNumber` and `ProductVersionNumber` values and may contain additional information (such as a version number compliant with the Semantic Version scheme).


### ActiveTranslation / Translations

The `Translations` property contains all translations for the version information in the PE image.  A `Translation` identifies a Language Code for each translation, from which a Language Id, SubLangauge and Codepage may be determined.

