using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("upload")]
public class UploadController : ControllerBase
{
    [HttpPost("multiple")]
    public async Task<IActionResult> UploadFiles(List<IFormFile> files)
    {
        if (files == null || files.Count == 0)
            return BadRequest("No files uploaded.");

        Directory.CreateDirectory("wwwroot/uploads");

        foreach (var file in files)
        {
            var path = Path.Combine("wwwroot/uploads", file.FileName);
            using var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);
        }

        return Ok(new { Message = $"{files.Count} file(s) uploaded successfully." });
    }
}
