using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UploadController : ControllerBase
{
    private readonly VideoCatalogueService _catalogueService;
    private const long MaxUploadSize = 200 * 1024 * 1024; // 200MB

    public UploadController(VideoCatalogueService catalogueService)
    {
        _catalogueService = catalogueService;
    }

    [HttpPost]
    [RequestSizeLimit(long.MaxValue)]
    public async Task<IActionResult> Upload(List<IFormFile> files)
    {
        if (files == null || files.Count == 0)
            return BadRequest("No files uploaded.");

        // Check total size
        long totalSize = files.Sum(f => f.Length);
        if (totalSize > MaxUploadSize)
        {
            return BadRequest("Total upload size exceeds 200 MB.");
        }

        var mediaFolder = _catalogueService.GetMediaFolderPath();

        foreach (var file in files)
        {
            if (!file.FileName.EndsWith(".mp4", StringComparison.OrdinalIgnoreCase))
                return BadRequest("Only .mp4 files are allowed.");


            var filePath = Path.Combine(mediaFolder, Path.GetFileName(file.FileName));

            // 🔴 Check if file already exists
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            using var stream = new FileStream(filePath, FileMode.CreateNew);
            await file.CopyToAsync(stream);
        }

        return Ok(new { Message = "Upload successful" });
    }
}