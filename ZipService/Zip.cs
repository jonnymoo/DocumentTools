using System.Collections.ObjectModel;
using System.IO.Compression;

namespace DocumentTools.ZipService;

public class Zip : IDisposable
{
    public Zip(string base64Zip)
    {
        ZipStream = new(Convert.FromBase64String(base64Zip));
        ZipArchive = new ZipArchive(ZipStream);
        Entries = ZipArchive.Entries.Where(e => e.Name.Length > 0);
        Files = Entries.Select(entry => new ZipFile(entry.Name, entry.FullName)).ToArray();
    }

    public byte[] this[int index]
    {
        get
        {
            if (Entries.Count() < index)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index out of range.");
            }
            else
            {
                var entry = Entries.ElementAt(index);

                using var stream = entry.Open();
                using var byteStream = new MemoryStream();
                stream.CopyTo(byteStream);
                return byteStream.ToArray();
            }
        }
    }

    public void Dispose()
    {
        ZipStream?.Dispose();
        ZipArchive?.Dispose();
    }

    private MemoryStream ZipStream;
    private ZipArchive ZipArchive;

    private IEnumerable<ZipArchiveEntry> Entries;

    public record ZipFile(string Name, string FullName);
    public ZipFile[] Files {get; private set;}
   
}
