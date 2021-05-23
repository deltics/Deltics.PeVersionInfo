using System.IO;
using FluentAssertions;
using Xunit;
using Deltics.VersionInfo;


namespace VersionInfoTests
{
    public class Tests
    {
        private VersionInfo LoadArtefact(string filename)
        {
            return new (new FileStream($"artefacts/{filename}", FileMode.Open));
        }


        [Theory]
        [InlineData("x86.exe", 1, 2, 3, 4)]
        [InlineData("x86.dll", 1, 2, 3, 4)]
        [InlineData("x64.exe", 1, 2, 3, 4)]
        [InlineData("x64.dll", 1, 2, 3, 4)]
        public void FileVersionNumberHasExpectedValues(string filename, ushort major, ushort minor, ushort build, ushort patch)
        {
            var sut = LoadArtefact(filename);

            sut.FileVersionNumber.Should().NotBeNull();
            
            sut.FileVersionNumber.Major.Should().Be(major);
            sut.FileVersionNumber.Minor.Should().Be(minor);
            sut.FileVersionNumber.Build.Should().Be(build);
            sut.FileVersionNumber.Private.Should().Be(patch);
        }
        

        [Theory]
        [InlineData("x86.exe", 5, 6, 7, 8)]
        [InlineData("x86.dll", 5, 6, 7, 8)]
        [InlineData("x64.exe", 5, 6, 7, 8)]
        [InlineData("x64.dll", 5, 6, 7, 8)]
        public void ProductVersionNumberHasExpectedValues(string filename, ushort major, ushort minor, ushort build, ushort patch)
        {
            var sut = LoadArtefact(filename);

            sut.ProductVersionNumber.Should().NotBeNull();
            
            sut.ProductVersionNumber.Major.Should().Be(major);
            sut.ProductVersionNumber.Minor.Should().Be(minor);
            sut.ProductVersionNumber.Build.Should().Be(build);
            sut.ProductVersionNumber.Private.Should().Be(patch);
        }
        

        [Theory]
        [InlineData("x86.exe")]
        [InlineData("x86.dll")]
        [InlineData("x64.exe")]
        [InlineData("x64.dll")]
        public void VersionInfoStringsHaveExpectedValues(string filename)
        {
            var sut = LoadArtefact(filename);

            sut.Strings.Should().NotBeNull();
            
            sut.Strings.Should().ContainKey("FileVersion");
            sut.Strings.Should().ContainKey("ProductVersion");

            sut.FileVersion.Should().Be("1.2.3-meta+info");
            sut.ProductVersion.Should().Be("5.6.7.8");

            sut.Comments.Should().Be("The Comments");
            sut.CompanyName.Should().Be("The Company Name");
            sut.FileDescription.Should().Be("The File Description");
            sut.InternalName.Should().Be("The Internal Name");
            sut.LegalCopyright.Should().Be("The Legal Copyright");
            sut.LegalTrademarks.Should().Be("The Legal Trademarks");
            sut.OriginalFilename.Should().Be("The Original Filename");
            sut.ProductName.Should().Be("The Product Name");
            sut.PrivateBuild.Should().Be("The Private Build");
            sut.SpecialBuild.Should().Be("The Special Build");

            sut.Strings.Should().ContainKey("SomeCustomString");
            sut.Strings["SomeCustomString"].Should().Be("Some Custom String Value");
        }
    }
}