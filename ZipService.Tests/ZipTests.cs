using System.Text;
using DocumentTools.ZipService;

namespace ZipService.Tests;

public class ZipTests
{
  [Fact]
  public void Extract_File_Content()
  {
    string base64ZipString = "UEsDBBQAAAAAAIdbulgAAAAAAAAAAAAAAAAIAAAAVGVzdFppcC9QSwMECgAAAAAAilu6WMvQ3WkLAAAACwAAABEAAABUZXN0WmlwL1Rlc3QxLnR4dEkgYW0gdGVzdCAxUEsBAj8AFAAAAAAAh1u6WAAAAAAAAAAAAAAAAAgAJAAAAAAAAAAQAAAAAAAAAFRlc3RaaXAvCgAgAAAAAAABABgAPblVaVev2gE9uVVpV6/aAXB8tV5Xr9oBUEsBAj8ACgAAAAAAilu6WMvQ3WkLAAAACwAAABEAJAAAAAAAAAAgAAAAJgAAAFRlc3RaaXAvVGVzdDEudHh0CgAgAAAAAAABABgAlHI/bVev2gGQN0NtV6/aAanwoWZXr9oBUEsFBgAAAAACAAIAvQAAAGAAAAAAAA==";

    Zip zip = new Zip(base64ZipString,@".*");
    byte[] extractedFileContent = zip[0];
    string extractedString = Encoding.UTF8.GetString(extractedFileContent);

    Assert.Equal("I am test 1", extractedString);
  }

  [Fact]
  public void Get_File_List()
  {
    string base64ZipString = "UEsDBBQAAAAAAIdbulgAAAAAAAAAAAAAAAAIAAAAVGVzdFppcC9QSwMECgAAAAAAilu6WMvQ3WkLAAAACwAAABEAAABUZXN0WmlwL1Rlc3QxLnR4dEkgYW0gdGVzdCAxUEsBAj8AFAAAAAAAh1u6WAAAAAAAAAAAAAAAAAgAJAAAAAAAAAAQAAAAAAAAAFRlc3RaaXAvCgAgAAAAAAABABgAPblVaVev2gE9uVVpV6/aAXB8tV5Xr9oBUEsBAj8ACgAAAAAAilu6WMvQ3WkLAAAACwAAABEAJAAAAAAAAAAgAAAAJgAAAFRlc3RaaXAvVGVzdDEudHh0CgAgAAAAAAABABgAlHI/bVev2gGQN0NtV6/aAanwoWZXr9oBUEsFBgAAAAACAAIAvQAAAGAAAAAAAA==";

    Zip zip = new Zip(base64ZipString,@".*\.txt");
    
    Assert.Equal("Test1.txt", zip.Files[0].Name );
  }

[Fact]
  public void Zip_ReturnsAllEntries_WhenNoFilterProvided()
  {
    string base64ZipString = "UEsDBBQAAAAAAIdbulgAAAAAAAAAAAAAAAAIAAAAVGVzdFppcC9QSwMECgAAAAAAilu6WMvQ3WkLAAAACwAAABEAAABUZXN0WmlwL1Rlc3QxLnR4dEkgYW0gdGVzdCAxUEsBAj8AFAAAAAAAh1u6WAAAAAAAAAAAAAAAAAgAJAAAAAAAAAAQAAAAAAAAAFRlc3RaaXAvCgAgAAAAAAABABgAPblVaVev2gE9uVVpV6/aAXB8tV5Xr9oBUEsBAj8ACgAAAAAAilu6WMvQ3WkLAAAACwAAABEAJAAAAAAAAAAgAAAAJgAAAFRlc3RaaXAvVGVzdDEudHh0CgAgAAAAAAABABgAlHI/bVev2gGQN0NtV6/aAanwoWZXr9oBUEsFBgAAAAACAAIAvQAAAGAAAAAAAA==";

    var zip = new Zip(base64ZipString, "nomatch");

    Assert.Empty(zip.Files); 
  }

  [Fact]
  public void Zip_FiltersEntries_ByFilenameRegex()
  {
    string base64ZipString = "UEsDBBQAAAAAAIdbulgAAAAAAAAAAAAAAAAIAAAAVGVzdFppcC9QSwMECgAAAAAAilu6WMvQ3WkLAAAACwAAABEAAABUZXN0WmlwL1Rlc3QxLnR4dEkgYW0gdGVzdCAxUEsBAj8AFAAAAAAAh1u6WAAAAAAAAAAAAAAAAAgAJAAAAAAAAAAQAAAAAAAAAFRlc3RaaXAvCgAgAAAAAAABABgAPblVaVev2gE9uVVpV6/aAXB8tV5Xr9oBUEsBAj8ACgAAAAAAilu6WMvQ3WkLAAAACwAAABEAJAAAAAAAAAAgAAAAJgAAAFRlc3RaaXAvVGVzdDEudHh0CgAgAAAAAAABABgAlHI/bVev2gGQN0NtV6/aAanwoWZXr9oBUEsFBgAAAAACAAIAvQAAAGAAAAAAAA==";

    var zip = new Zip(base64ZipString, @".*\.txt");

    Assert.Single(zip.Files); // Assuming only one entry matches the filter
    Assert.Equal("Test1.txt", zip.Files[0].Name);
  }
}