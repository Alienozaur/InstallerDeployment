using System.Text;
using InstallerDeployment.Services.FileStorage;
using Microsoft.AspNetCore.Mvc;

namespace InstallerDeployment.Controllers;

[ApiController]
[Route("[controller]")]
public class InstallerController : Controller
{
    private readonly IFileStorage _fileStorage;

    public InstallerController(IFileStorage fileStorage)
    {
        _fileStorage = fileStorage;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var stream = await _fileStorage.Get(id);
        
        return File(stream, "application/octet-stream");
    }
    
    [HttpGet("list")]
    public async Task<IActionResult> List()
    {
        var builder = new StringBuilder();
        foreach (var file in await _fileStorage.List())
            builder.AppendLine(file);

        return Ok(builder.ToString());
    }
    
    [HttpPost("upload/{id}")]
    [RequestSizeLimit(bytes: 100_000_000)]
    public async Task<IActionResult> Upload(string id, IFormFile file)
    {
        await _fileStorage.Add(file.OpenReadStream(), id);
        return Ok();
    }
}