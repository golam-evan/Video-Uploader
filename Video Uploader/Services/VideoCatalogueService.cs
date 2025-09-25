using System.IO;
using System.Collections.Generic;

public class VideoCatalogueService
{
    private readonly string _mediaFolder;

    public VideoCatalogueService(IWebHostEnvironment env)
    {
        _mediaFolder = Path.Combine(env.WebRootPath, "media");
        if (!Directory.Exists(_mediaFolder))
            Directory.CreateDirectory(_mediaFolder);
    }

    public IEnumerable<VideoFile> GetCatalogue()
    {
        var files = Directory.GetFiles(_mediaFolder, "*.mp4");
        foreach (var file in files)
        {
            var info = new FileInfo(file);
            yield return new VideoFile
            {
                FileName = info.Name,
                FileSize = info.Length
            };
        }
    }

    public string GetMediaFolderPath() => _mediaFolder;
}
