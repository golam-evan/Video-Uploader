public class VideoFile
{
    public string FileName { get; set; }
    public long FileSize { get; set; } // in bytes
    public string RelativePath => $"/media/{FileName}";
}
